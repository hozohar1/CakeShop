using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    const string s_OrderItems = @"OrderItem"; //XML Serializer
    public int Add(OrderItem item)
    {
        List<DO.OrderItem?> listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems);

        item.ID = Config.GetNextOrderItemIDfromXMLConfig();
        listOrderItems.Add(item);
        Config.saveListToXMLElementOrderItem(item.ID + 1);
        XMLTools.SaveListToXMLSerializer(listOrderItems, s_OrderItems);

        return item.ID;

    }
    public void Delete(int id)
    {
        List<DO.OrderItem?> listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems);
        if (listOrderItems.Exists(x => x?.ID == id))
        {
            listOrderItems.Remove(GetById(id));
        }
        else
            throw new DalIDNotExistException(id, "ORDER ITEM ID NOT FOUND");

        XMLTools.SaveListToXMLSerializer(listOrderItems, s_OrderItems);
    }

    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter = null)
    {
        List<DO.OrderItem?> listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems);
        if (filter == null)
            return listOrderItems.Select(x => x);
        else
            return listOrderItems.Where(x => filter(x));

    }

    public OrderItem GetById(int id)
    {
        List<DO.OrderItem?> listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems);
        OrderItem ord = listOrderItems.Find(x => x?.ID == id) ?? throw new DalIDNotExistException(id, "ORDER ID NOT FOUND");
        return ord;
    }

    public IEnumerable<OrderItem?> SearchKey(int id)
    {
        List<DO.OrderItem?> listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems);
        List<OrderItem?> ord = new List<OrderItem?>();
        for (int i = 0; i < listOrderItems.Count; i++)
        {
            if (listOrderItems[i]?.OrderID == id)
                ord.Add(listOrderItems[i]);
        }
        return ord;
    }

    public OrderItem? SearchOrderItemId(int productId, int OrderId)
    {
        List<DO.OrderItem?> listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems);
        for (int i = 0; i < listOrderItems.Count; i++)
        {
            if (listOrderItems[i]?.OrderID == OrderId && listOrderItems[i]?.ProductID == productId)
                return listOrderItems[i];
        }
        throw new DalIDNotExistException(OrderId, "ORDER ITEM NOT FOUND");
    }

    public void UpDate(OrderItem item)
    {
        Delete(item.ID);
        List<DO.OrderItem?> listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems);
        listOrderItems.Add(item);
        XMLTools.SaveListToXMLSerializer(listOrderItems, s_OrderItems);

    }
}

