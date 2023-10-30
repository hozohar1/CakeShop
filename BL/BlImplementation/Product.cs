using BlApi;
using BO;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Reflection;

namespace BlImplementation;
internal class Product : IProduct
{
    DalApi.IDal? dal = DalApi.Factory.Get();

    /// <summary>
    /// Product details request by ID (for admin screen and for)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Product GetById(int id)
    {

        if (id < 0)
        { throw new BO.BlInvalidInputException("product id invalid"); }
        try
        {

            dal?.Product.GetById(id);
        }
        catch (DO.DalIDNotExistException)
        {
            throw new BO.BlIdNotExistException(" ", id);
        }
        DO.Product doProduct = dal.Product.GetById(id);
        return new BO.Product()
        {

            ID = doProduct.ID,
            Category = (BO.Category)doProduct.Category,
            Price = doProduct.Price,
            Name = doProduct.Name,
            Amount = doProduct.Amount, ///??stock amount..
            ImageRelativeName = @"\img\IMG" + doProduct.ID + ".jpg",
        };


    }

    /// <summary>
    /// Product deletion (for admin screen)
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void DeleteProductP(int id)
    {
        if (id < 0)
        { throw new BO.BlInvalidInputException("product id"); }

        IEnumerable<DO.OrderItem?> OrderItemList = from OrderItem in dal.OrderItem.GetAll()
                                                   where OrderItem.Value.ProductID == id
                                                   select OrderItem;
        try
        {
            dal.Product.GetById(id);
        }

        catch (DO.DalIDNotExistException ex)
        {

            throw new BO.BlIdNotExistException(" ", id, ex);
        }
        if (!OrderItemList.Any())
        {
            dal.Product.Delete(id);
        }
        else
        { throw new Exception("PRODUCT ALREADY IN ORDER PROCESS"); }

    }

    /// <summary>
    /// Adding a product (for admin screen)
    /// </summary>
    /// <param name="item"></param>
    /// <exception cref="NullReferenceException"></exception>
    public void AddProductP(BO.Product item)
    {
        //Input integrity checks
        if (item.ID <= 0)
        {
            throw new BO.BlInvalidInputException("product id");
        }
        if (item.Name == null)
        {
            throw new BO.BlInCorrectStringException("ERROR IN NAME", item.ID);
        }
        if (item.Price <= 0)
        {
            throw new BO.BlInvalidInputException("product price");
        }
        if (item.Amount < 0)
        {
            throw new BO.BlInvalidInputException("product amount ");
        }

        DO.Product products = new DO.Product();
        products.ID = item.ID;
        products.Name = item.Name;
        products.Price = item.Price;
        products.Amount = item.Amount;
        products.Category = (DO.Category)item.Category;

        try
        {
            dal?.Product.Add(products);
        }
        catch (DO.DalIDAlreadyExistException)
        {
            throw new BO.BlIdAlreadyExistException("  ", item.ID);
        }
    }

    /// <summary>
    /// Update product data (for admin screen)
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public BO.Product UpdateProductP(BO.Product item)
    {
        //Input integrity checks
        if (item.ID <= 0)
        {
            throw new BO.BlInvalidInputException("product id");
        }
        if (item.Name == null)
        {
            throw new BO.BlInCorrectStringException("ERROR IN NAME", item.ID);
        }
        if (item.Price <= 0)
        {
            throw new BO.BlInvalidInputException("product price");
        }
        if (item.Amount < 0)
        {

            throw new BO.BlInvalidInputException("product amount");
        }


        DO.Product products = new DO.Product();

        products.ID = item.ID;
        products.Name = item.Name;
        products.Price = item.Price;
        products.Amount = item.Amount;
        products.Category = (DO.Category)item.Category;
       
        try
        {
            dal.Product.UpDate(products);
        }
        catch (DO.DalIDNotExistException ex)
        {
            throw new BO.BlIdNotExistException(" ", products.ID, ex);
        }

        return item;




    }

    /// <summary>
    /// Returning a list of products
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<ProductForList?> GetListedProducts(Func<ProductForList?, bool>? filter = null)
    {
        IEnumerable<BO.ProductForList?> bList = from DO.Product doProduct in dal.Product.GetAll()
                                                select new BO.ProductForList
                                                {
                                                    ID = doProduct.ID,
                                                    Price = doProduct.Price,
                                                    Name = doProduct.Name,
                                                     Category = (BO.Category)doProduct.Category,
                                                };
        return filter is null ? bList : bList.Where(filter);


    }

    /// <summary>
    /// Product list request (for manager screen and for buyer's catalog screen)
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public IEnumerable<BO.ProductForList> GetListedProducts()
    {
        return NewMethod();
    }

    /// <summary>
    /// help function
    /// </summary>
    /// <returns></returns>
    private IEnumerable<ProductForList> NewMethod()
    {
        return from DO.Product? doProduct in dal.Product.GetAll()
               select doToBoProductForList(doProduct);
    }

    /// <summary>
    /// returns ProductForList
    /// </summary>
    /// <param name="doProduct"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    private ProductForList doToBoProductForList(DO.Product? doProduct)
    => new BO.ProductForList
    {
        ID = doProduct?.ID ?? throw new NullReferenceException("Missing ID"),
        Name = doProduct?.Name ?? throw new NullReferenceException("Missing Name"),
        Category = (BO.Category?)doProduct?.Category ?? throw new NullReferenceException("Missing Category"),
        Price = doProduct?.Price ?? 0
    };

    /// <summary>
    /// returns ProductForList by id
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    public ProductForList GetProductForList(int productId) => doToBoProductForList(dal?.Product.GetById(productId));

    /// <summary>
    /// returns Product item by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="i"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlInvalidInputException"></exception>
    /// <exception cref="BO.BlIdNotExistException"></exception>
    public BO.ProductItem GetProductItem(int id, int i)
    {

        if (id < 0)
            throw new BO.BlInvalidInputException("product id");

        try
        {
            dal.Product.GetById(id);
        }
        catch (DO.DalIDNotExistException ex)
        {
            throw new BO.BlIdNotExistException(" ", id, ex);
        }

        DO.Product p = dal.Product.GetById(id);
        BO.ProductItem productItem = new BO.ProductItem
        {
            ID = p.ID,
            Name = p.Name,
            Price = p.Price,
            IsAvailable = p.Amount > 0,
            Category = (BO.Category)p.Category,
            CartQuantity = i,//p.Amount,
            ImageRelativeName = @"\img\IMG" + p.ID + ".jpg"
        };

        return productItem;
    }

    /// <summary>
    /// Id productItem request (for buyer screen - from the catalog)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cart"></param>
    /// <returns></returns>
    public BO.ProductItem GetProductItem(int id, BO.Cart cart)
    {

        if (id < 0)
            throw new BO.BlInvalidInputException("product id");

        try
        {
            dal.Product.GetById(id);
        }
        catch (DO.DalIDNotExistException ex)
        {
            throw new BO.BlIdNotExistException(" ", id, ex);
        }
        int amunt = 0;
        DO.Product p = dal.Product.GetById(id);
        var y = cart.OrderItems.Find(x => x.ProductID == id);
        if (y != null)
        {
            amunt = y.Amount;
        }
        BO.ProductItem productItem = new BO.ProductItem
        {
            ID = p.ID,
            Name = p.Name,
            Price = p.Price,
            IsAvailable = p.Amount > 0,
            Category = (BO.Category)p.Category,
            CartQuantity = amunt,
            ImageRelativeName = @"\img\IMG" + p.ID + ".jpg"
        };

        return productItem;
    }

    /// <summary>
    /// returns productItem with filter
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<ProductItem?> GetProductItem(Func<ProductItem?, bool>? filter = null)
    {
        IEnumerable<BO.ProductItem?> bList = from DO.Product doProduct in dal.Product.GetAll()
                                             select new BO.ProductItem
                                             {
                                                 ID = doProduct.ID,
                                                 Price = doProduct.Price,
                                                 Name = doProduct.Name,
                                                 Category = (BO.Category)doProduct.Category,
                                                 IsAvailable = (doProduct.Amount > 0),
                                                 ImageRelativeName = @"\img\IMG" + doProduct.ID + ".jpg",
                                                 CartQuantity = 0,
                                             };
        return filter is null ? bList : bList.Where(filter);


    }
}



