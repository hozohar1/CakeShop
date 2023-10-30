using BO;

namespace BlApi;

public interface IOrder
{
    public IEnumerable<OrderForList?> GetListedOrderP();
    public Order GetOrderInfo(int id);
    public Order UpDateOrderShipP(int id);
    public Order UpDateOrderDeliveryP(int id);
    public OrderTracking OrderTrackP(int id);
   // public DateTime GetOrderLastDate(int id);
}

