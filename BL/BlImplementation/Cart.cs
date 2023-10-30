using BlApi;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace BlImplementation;

internal class Cart : ICart
{
    DalApi.IDal? dal = DalApi.Factory.Get();


    /// <summary>
    /// Adding a product to the shopping cart (for catalog buyer screen, product details screen)
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Cart AddProductCart(BO.Cart? cart, int id)
    {
        try { dal.Product.GetById(id); }
        catch (DO.DalIDNotExistException ex) { throw new BO.BlIdNotExistException(" ", id, ex); }

        DO.Product product = dal.Product.GetById(id);
        if (cart?.OrderItems != null && cart?.OrderItems.Count > 0)
        {
            BO.OrderItem? o = (from orderItem in cart.OrderItems
                               where orderItem.ProductID == id
                               select orderItem).FirstOrDefault();

            // check if product exists in cart-if so, return the first object
            BO.OrderItem? itemInCart = cart.OrderItems.FirstOrDefault(item => item.ID == id);

            if (itemInCart != null)//product exists
            {
                if (product.Amount > 0)
                {
                    o.Amount++;
                    o.TotalPriceForItem += o.Price;
                    cart.TotalPrice += o.Price;
                }

                return cart;

            }
        }
        if (product.GetType != null && product.Amount > 0) //item not found in cart
        {
            if (cart == null)
            {
                cart = new BO.Cart();
                cart.OrderItems = new List<BO.OrderItem?>();
            }
            BO.OrderItem? np = new BO.OrderItem();
            np.ID = product.ID;
            np.Name = product.Name;
            np.Price = product.Price;
            np.Amount = 1;
            np.TotalPriceForItem = product.Price * 1;
            np.ProductID = product.ID;
            cart?.OrderItems?.Add(np);
            cart.TotalPrice = cart.TotalPrice + np.TotalPriceForItem;

        }

        return cart;
    }

    /// <summary>
    /// Updating the quantity of a product in the shopping cart (for the shopping cart screen)
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <param name="NewAmount"></param>
    /// <returns></returns>
    public BO.Cart UpdateAmountProductCart(BO.Cart cart, int id, int NewAmount)
    {
        //Input integrity checks
        if (id < 0)
        {
            throw new BO.BlInvalidInputException("product ID");
        }
        if (NewAmount < 0)
        {
            throw new BO.BlInvalidInputException(" amount");

        }

        try { dal?.Product.GetById(id); }
        catch (DO.DalIDNotExistException ex) { throw new BO.BlIdNotExistException("product id not exist"); }
        DO.Product product = dal!.Product.GetById(id);
        BO.OrderItem o = (from orderItem in cart.OrderItems
                          where orderItem.ProductID == id
                          select orderItem).First();

        if (product.Amount <= 0)
        {
            throw new BO.BlNullPropertyException("this product is sold out");
        }
        if (o == null)
        { throw new BO.BlEmptyException("OrderItem", id); }
        if (o.Amount == 0)
        {
            var p = o.TotalPriceForItem;
            cart.OrderItems.Remove(o); //if amount=0 delete product 
            cart.TotalPrice = cart.TotalPrice - p;
        }
        if (NewAmount == 0)
        {
            cart.OrderItems.Remove(o); //if amount=0 delete product 
        }
        if (NewAmount > o.Amount)
        {
            var old = o.TotalPriceForItem;
            o.Amount = NewAmount;
            o.TotalPriceForItem = o.Price * o.Amount;
            cart.TotalPrice = cart.TotalPrice + o.TotalPriceForItem - old;
        }
        if (NewAmount < o.Amount)
        {
            var old = o.TotalPriceForItem;
            o.Amount = NewAmount;
            o.TotalPriceForItem = o.Price * o.Amount;
            cart.TotalPrice = cart.TotalPrice + o.TotalPriceForItem - old;
        }
        return cart;
    }

    /// <summary>
    /// Confirmation and execution of the order
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="name"></param>
    /// <param name="email"></param>
    /// <param name="adress"></param>
    /// <exception cref="BO.BlInvalidInputException"></exception>
    /// <exception cref="BO.BlInCorrectIntException"></exception>
    /// <exception cref="BO.BlNullPropertyException"></exception>
    public void ConfirmCartOrder(BO.Cart cart, string name, string email, string adress)
    {
        //Input integrity checks
        if (name == null || name == " ")
        { throw new BO.BlInCorrectStringException("wrong name"); }
        if (adress == null || adress == " ")
        {
            { throw new BO.BlInCorrectStringException("wrong adress"); }

        }
        if (!(email).Contains("@") && email.Contains("."))
        { throw new BO.BlInvalidInputException("wrong email"); }
        if (new EmailAddressAttribute().IsValid(email) == false)
        {
            { throw new BO.BlInvalidInputException("wrong Email"); }


        }
        if (cart.TotalPrice <= 0)
        { throw new BO.BlInCorrectIntException("Total Price in Cart"); }

        IEnumerable<BO.OrderItem?> OrderItemList = from BO.OrderItem? orderItem in cart.OrderItems
                                                   select orderItem;
        if (OrderItemList.FirstOrDefault(s => s.Amount < 0) != null) //if there is an order item with negative amount
        {
            throw new BO.BlInvalidInputException(" amount");
        }
        if (OrderItemList.FirstOrDefault(s => dal.Product.GetById(s.ProductID).Amount < 0) != null) // if out of stock
        {
            throw new BO.BlNullPropertyException("out of stock");
        }



        DO.Order order = new DO.Order();
        order.CustomerName = cart.CustomerName;
        order.CustomerAdress = cart.CustomerAdress;
        order.CustomerEmail = cart.CustomerEmail;
        order.OrderDate = DateTime.Now;
        order.ShipDate = null;
        order.DeliveryDate = null;
        order.ID = dal!.Order.Add(order);

        OrderItemList.ToList().ForEach(x =>
        {
            DO.OrderItem p = new DO.OrderItem();
            p.OrderID = order.ID;
            p.ProductID = x.ProductID;
            p.Amount = x.Amount;
            p.Price = x.Price;
            p.ID = dal.OrderItem.Add(p);
        });

        //update new valid amount for each item that sold
        OrderItemList.ToList().ForEach(x =>
        {
            int id = x.ProductID;
            int howMany = x.Amount;
            DO.Product prod = dal.Product.GetById(id);
            prod.Amount = prod.Amount - howMany;
            dal.Product.UpDate(prod);
        });


    }

}

