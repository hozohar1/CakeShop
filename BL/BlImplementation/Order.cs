using BlApi;

using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace BlImplementation;



internal class Order : IOrder
{
    DalApi.IDal? dal = DalApi.Factory.Get();

    /// <summary>
    /// Order list request (admin screen)
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.OrderForList?> GetListedOrderP()
    {
        IEnumerable<DO.Order?> orderItemList = dal?.Order.GetAll();
        return from doOrder in orderItemList
               let orderItems = dal?.OrderItem.SearchKey(doOrder?.ID ?? 0)
               select new BO.OrderForList
               {
                   ID = doOrder?.ID ?? 0,
                   CustomerName = doOrder?.CustomerName,
                   OrderStatus = GetStatus((DO.Order)doOrder),
                   Amount = orderItems.Count(),
                   TotalPrice = GetOrderInfo(doOrder?.ID ?? 0).TotalPrice,
               };
    }

    /// <summary>
    /// help func-return the status of order
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    private BO.OrderStatus GetStatus(DO.Order order)
    {
        BO.OrderStatus status = (BO.OrderStatus.Initiated);
        if (order.OrderDate == null)
        {
            status = (BO.OrderStatus.Paid);
            // return status;
        }
        if (order.OrderDate != null)
        {
            status = (BO.OrderStatus.Ordered);//Ordered
            //return status;
        }
        if (order.ShipDate != null)
        {
            status = (BO.OrderStatus.Shipped);
            //return status;
        }
        if (order.DeliveryDate != null) // delvered
        {
            status = (BO.OrderStatus.Delivered);
            //return status;
        }
        return status;
    }

    /// <summary>
    /// Order details request (for manager screen and buyer screen)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public BO.Order GetOrderInfo(int id)
    {
        if (id <= 0)
        { throw new BO.BlInvalidInputException("order id"); }
        if (id > 0)
        {
            BO.Order order = new BO.Order();
        }
        try
        {
            dal?.Order.GetById(id);
        }
        catch (DO.DalIDNotExistException)
        {

            throw new BO.BlIdNotExistException("id not exist");
        }
        if (id > 0)
        {
            BO.Order order = new BO.Order();
            DO.Order od = dal!.Order.GetById(id);
            order.ID = od.ID;
            order.CustomerName = od.CustomerName;
            order.OrderDate = od.OrderDate;
            order.ShipDate = od.ShipDate;
            order.DeliveryDate = od.DeliveryDate;
            order.CustomerEmail = od.CustomerEmail;
            order.CustomerAdress = od.CustomerAdress;
            order.OrderStatus = GetStatus(od);
            IEnumerable<DO.OrderItem?> orderItemList = dal.OrderItem.SearchKey(od.ID);
            order.OrderItems = (from orderItem in orderItemList
                                select new BO.OrderItem
                                {
                                    ID = orderItem.Value.ID,
                                    Name = dal.Product.GetById(orderItem.Value.ProductID).Name,
                                    ProductID = orderItem.Value.ProductID,
                                    Amount = orderItem.Value.Amount,
                                    Price = orderItem.Value.Price,
                                    TotalPriceForItem = orderItem.Value.Price * orderItem.Value.Amount
                                }).ToList();
            order.TotalPrice = order.OrderItems.Sum(x => x.TotalPriceForItem);//orderItemList.Sum(x => x?.Price ?? 0 * x?.Amount ?? 0);
            return order;
        }
        throw new NotImplementedException();
    }
    /// <summary>
    /// Order Delivery Update (Admin Order Management Screen)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public BO.Order UpDateOrderDeliveryP(int id)
    {
        BO.Order order = new BO.Order();
        DO.Order d = dal!.Order.GetById(id);
        try
        {
            d = dal.Order.GetById(id);

        }
        catch (DO.DalIDNotExistException ex)
        {
            throw new BO.BlIdNotExistException("order", id, ex);
        }
        if (d.DeliveryDate == null) //order has not been delivered
        {
            if (d.ShipDate == null)
            {
                d.ShipDate = DateTime.Now;
            }
            if (id >= 1021)
            {
                d.DeliveryDate = DateTime.Now.AddDays(8);
            }
            else
                d.DeliveryDate = DateTime.Now;
            dal.Order.UpDate(d);
            order.ID = d.ID;
            order.CustomerName = d.CustomerName;
            order.OrderDate = d.OrderDate;
            order.ShipDate = d.ShipDate;
            order.DeliveryDate = d.DeliveryDate;
            order.CustomerEmail = d.CustomerEmail;
            order.CustomerAdress = d.CustomerAdress;
            order.OrderStatus = (BO.OrderStatus.Delivered);
            IEnumerable<DO.OrderItem?> orderItemList = dal.OrderItem.SearchKey(order.ID);
            order.OrderItems = (from orderItem in orderItemList
                                select new BO.OrderItem
                                {
                                    ID = orderItem?.ID ?? 0,
                                    Name = dal.Product.GetById(orderItem?.ProductID ?? 0).Name,
                                    ProductID = orderItem?.ProductID ?? 0,
                                    Amount = orderItem?.Amount ?? 0,
                                    Price = orderItem?.Price ?? 0,
                                    TotalPriceForItem = orderItem?.Price ?? 0 * orderItem?.Amount ?? 0
                                }).ToList();
            order.TotalPrice = orderItemList.Sum(x => x?.Price ?? 0 * x?.Amount ?? 0);
            return order;
        }
        else
        { throw new BO.BlIncorrectDateException("The order has already been delivered"); }
    }



    /// <summary>
    /// Order Shipping Update (Manager Order Management Screen)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public BO.Order UpDateOrderShipP(int id)
    {
        if (id <= 0)
        {
            throw new BO.BlInvalidInputException("order id");
        }
        try
        {
            dal?.Order.GetById(id);
        }
        catch (DO.DalIDNotExistException ex)
        {
            throw new BO.BlIdNotExistException(" ", id, ex);
        }
        BO.Order order = new BO.Order();
        DO.Order d = dal.Order.GetById(id);
        try
        {
            if (order.ShipDate > DateTime.MinValue)
                throw new DO.DatesException("Order:", order.ShipDate, DateTime.MinValue);
        }
        catch (DO.DatesException ex) { throw new BO.BlIncorrectDateException("Order:"); }

        if (d.GetType != null && d.ShipDate == null) //order exsist but doesnt shiped yet
        {
            if(id>=1021)
            {
                d.ShipDate = DateTime.Now.AddDays(4);
            }
            else
                d.ShipDate = DateTime.Now;

            dal.Order.UpDate(d);
            order.ID = d.ID;
            order.CustomerName = d.CustomerName;
            order.OrderDate = d.OrderDate;
            order.ShipDate = d.ShipDate;
            order.DeliveryDate = d.DeliveryDate;
            order.CustomerEmail = d.CustomerEmail;
            order.CustomerAdress = d.CustomerAdress;
            order.OrderStatus = (BO.OrderStatus.Shipped);
            IEnumerable<DO.OrderItem?> orderItemList = dal.OrderItem.SearchKey(order.ID);
            order.OrderItems = (from orderItem in orderItemList
                                select new BO.OrderItem
                                {
                                    ID = orderItem.Value.ID,
                                    Name = dal.Product.GetById(orderItem?.ProductID ?? 0).Name,
                                    ProductID = orderItem?.ProductID ?? 0,
                                    Amount = orderItem?.Amount ?? 0,
                                    Price = orderItem?.Price ?? 0,
                                    TotalPriceForItem = orderItem?.Price ?? 0 * orderItem?.Amount ?? 0
                                }).ToList();
            order.TotalPrice = orderItemList.Sum(x => x?.Price ?? 0 * x?.Amount ?? 0);
            return order;
        }
        else
        { throw new BO.BlIncorrectDateException("The order has already been sent"); }

    }
    //public DateTime GetOrderLastDate(int id)
    //{
    //    DateTime h= DateTime.Now;
    //    DO.Order d = dal.Order.GetById(id);
    //    if (d.GetType != null)
    //    {
    //        if (d.OrderDate != null)
    //        {
    //            h = (DateTime)d.OrderDate;
    //        }
    //        if (d.ShipDate != null)
    //        {
    //            h = (DateTime)d.ShipDate;
    //        }
    //        if (d.DeliveryDate != null)
    //        {
    //            h = (DateTime)d.DeliveryDate;
    //        }
    //        return h;
    //    }
    //    throw new NotImplementedException();
    //}


    /// <summary>
    /// Order Tracking (Manager Order Management Screen)
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="NotImplementedException"></exception>
    public BO.OrderTracking OrderTrackP(int id)
    {
        if (id <= 0)
        {
            throw new BO.BlInvalidInputException("order id");
        }

        DO.Order d = dal.Order.GetById(id);
        if (d.GetType != null)
        {
            List<Tuple<DateTime?, string>> tupleList = new List<Tuple<DateTime?, string>>();
            Tuple<DateTime?, string> tuple;
            BO.OrderTracking orderTracking = new BO.OrderTracking();// create new tracking cbjext
            if (d.OrderDate != null)
            {
                tuple = new(d.OrderDate, "The order has been created");
                tupleList.Add(tuple);
            }
            if (d.ShipDate != null)
            {
                tuple = new(d.ShipDate, "The order has been shiped");
                tupleList.Add(tuple);
            }
            if (d.DeliveryDate != null)
            {
                tuple = new(d.DeliveryDate, "The order has been delivered");
                tupleList.Add(tuple);
            }
            orderTracking.ID = id;
            orderTracking.Tracking = tupleList;
            orderTracking.Status = GetStatus(d);
            return orderTracking;
        }
        throw new NotImplementedException();
    }

}  

        
   
     

