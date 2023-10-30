using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dal;

internal class DalOrder : IOrder
{
    const string s_orders = @"Orders";
    public int Add(Order item)
    {
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

        item.ID = Config.GetNextOrderIDfromXMLConfig();
        listOrders.Add(item);
        Config.saveListToXMLElementOrders(item.ID + 1);
        XMLTools.SaveListToXMLSerializer(listOrders, s_orders);

        return item.ID;
    }

    public void Delete(int id)
    {
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

        if (listOrders.Exists(x => x?.ID == id))
        {
            listOrders.Remove(GetById(id));
        }
        else
            throw new DalIDNotExistException(id, "ORDER ID NOT FOUND");


        XMLTools.SaveListToXMLSerializer(listOrders, s_orders);


    }

    public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter = null)
    {
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

        if (filter == null)
            return listOrders.Select(x => x);
        else
            return listOrders.Where(x => filter(x));
    }

    public Order GetById(int id)
    {
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        Order ord = listOrders.Find(x => x?.ID == id) ?? throw new DalIDNotExistException(id, "ORDER ID NOT FOUND");
        return ord;
    }

    public void UpDate(Order item)
    {

        Delete(item.ID);
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

        listOrders.Add(item);

        XMLTools.SaveListToXMLSerializer(listOrders, s_orders);



    }
}
