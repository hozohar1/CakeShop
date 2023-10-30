namespace BlApi;
using BO;
public interface ICart
{
    public Cart AddProductCart(Cart cart, int id);
    public Cart UpdateAmountProductCart(Cart cart, int id, int NewAmount);
    public void ConfirmCartOrder(Cart cart, string name, string email, string adress);

}
