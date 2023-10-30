using DalApi;
using DO;
namespace Dal;
internal class DalProduct : IProduct
{
    /// <summary>
    /// Adding a new product
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int Add(Product item)
    {

        if (DataSourse.ProductList.Exists(x => x?.ID == item.ID) == true)
            throw new DO.DalIDAlreadyExistException(item.ID, "product is already exist");
        else
        {
            DataSourse.ProductList.Add(item);
        }
        return item.ID;
    }


    /// <summary>
    /// returning a product by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Product GetById(int id)
    {
        Product ord = DataSourse.ProductList.Find(x => x?.ID == id) ?? throw new DalIDNotExistException(id, "PRODUCT ID NOT FOUND");
        return ord;

    }


    /// <summary>
    /// updating an existing product
    /// </summary>
    /// <param name="item"></param>
    /// <exception cref="Exception"></exception>
    public void UpDate(Product item)
    {
        int index = DataSourse.ProductList.FindIndex(x => x?.ID == item.ID);

        if (index != -1)
        {
            DataSourse.ProductList[index] = item;
        }
        else
            throw new DalIDNotExistException(item.ID, "PRODUCR NOT FOUND");

    }


    /// <summary>
    /// deleting an existing product
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        if (DataSourse.ProductList.Exists(x => x?.ID == id))
        {
            DataSourse.ProductList.Remove(GetById(id));
        }
        else
            throw new DalIDNotExistException(id, "PRODUCR ID NOT FOUND");
    }


    /// <summary>
    /// returning all products
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
    {
        if (filter == null)
            return DataSourse.ProductList.Select(x => x);
        else
            return DataSourse.ProductList.Where(x => filter(x));

    }
}
