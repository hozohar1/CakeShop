using DO;

namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
{
    IEnumerable<OrderItem?> SearchKey(int id);
    OrderItem? SearchOrderItemId(int productId, int OrderId);
  
}

