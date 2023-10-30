using DalApi;
using DO;

namespace Dal;

internal class DalOrder : IOrder
{
    /// <summary>
    /// Adding a new order
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public int Add(Order order)
    {
        order.ID = DataSourse.Config.NextOrderNumber;
        DataSourse.OrderList.Add(order);
        return order.ID;
    }


    /// <summary>
    /// returning order values ​​by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Order GetById(int id)
    {

        Order ord = DataSourse.OrderList.Find(x => x?.ID == id) ?? throw new DalIDNotExistException(id, "ORDER ID NOT FOUND");
        return ord;

    }

    /// <summary>
    /// updating an existing order
    /// </summary>
    /// <param name="order"></param>
    /// <exception cref="Exception"></exception>
    public void UpDate(Order order)
    {
        int index = DataSourse.OrderList.FindIndex(x => x?.ID == order.ID);
        if (index != -1)
        {
            DataSourse.OrderList[index] = order;
        }
        else
            throw new DalIDNotExistException(order.ID, "ORDER NOT FOUND");

    }


    /// <summary>
    /// deleting an order
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {

        if (DataSourse.OrderList.Exists(x => x?.ID == id))
        {
            DataSourse.OrderList.Remove(GetById(id));
        }
        else
            throw new DalIDNotExistException(id, "ORDER ID NOT FOUND");
    }


    /// <summary>
    /// returning the values ​​of all orders made
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter = null)
    {
        if (filter == null)
            return DataSourse.OrderList.Select(x => x);
        else
            return DataSourse.OrderList.Where(x => filter(x));

    }


}
