using DalApi;
using DO;
namespace Dal;
internal class DalOrderItem : IOrderItem
{

    /// <summary>
    /// Adding an order item
    /// </summary>
    /// <param name="orderItem"></param>
    /// <returns></returns>
    public int Add(OrderItem orderItem)
    {
        orderItem.ID = DataSourse.Config.NextOrderNumber;
        DataSourse.OrderItemList.Add(orderItem);
        return orderItem.ID;
    }

    /// <summary>
    /// eturning item values ​​in an order by  ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem GetById(int id)
    {

        OrderItem ord = DataSourse.OrderItemList.Find(x => x?.ID == id) ?? throw new DalIDNotExistException(id, "ORDER ID NOT FOUND");
        return ord;

    }

    /// <summary>
    /// pdating an order item
    /// </summary>
    /// <param name="order"></param>
    /// <exception cref="Exception"></exception>
    public void UpDate(OrderItem order)
    {
        int index = DataSourse.OrderItemList.FindIndex(x => x?.ID == order.ID);
        if (index != -1)
        {
            DataSourse.OrderItemList[index] = order;
        }
        else
            throw new DalIDNotExistException(order.ID, "ORDER ITEM NOT FOUND");

    }

    /// <summary>
    /// deleting an order item
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        if (DataSourse.OrderItemList.Exists(x => x?.ID == id))
        {
            DataSourse.OrderItemList.Remove(GetById(id));
        }
        else
            throw new DalIDNotExistException(id, "ORDER ITEM ID NOT FOUND");
    }

    /// <summary>
    /// returning the values ​​of order items
    /// </summary>
    /// <returns></returns>
    /// 
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter = null)
    {
        if (filter == null)
            return DataSourse.OrderItemList.Select(x => x);
        else
            return DataSourse.OrderItemList.Where(x => filter(x));

    }



    /// <summary>
    /// Search order item by Order id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public IEnumerable<OrderItem?> SearchKey(int id)
    {
        List<OrderItem?> ord = new List<OrderItem?>();
        for (int i = 0; i < DataSourse.OrderItemList.Count; i++)
        {
            if (DataSourse.OrderItemList[i]?.OrderID == id)
                ord.Add(DataSourse.OrderItemList[i]);

        }
        return ord;
    }

    /// <summary>
    /// spesific item in specific order
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="OrderId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem? SearchOrderItemId(int productId, int OrderId)
    {

        for (int i = 0; i < DataSourse.OrderItemList.Count; i++)
        {
            if (DataSourse.OrderItemList[i]?.OrderID == OrderId && DataSourse.OrderItemList[i]?.ProductID == productId)
                return DataSourse.OrderItemList[i];
        }
        throw new DalIDNotExistException(OrderId, "ORDER ITEM NOT FOUND");
        //return null;    
    }


}
