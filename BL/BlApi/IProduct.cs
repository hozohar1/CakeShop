using BO;

namespace BlApi;

public interface IProduct
{
    public IEnumerable<ProductForList?> GetListedProducts(); // A
     public void AddProductP(Product item);//A
    public void DeleteProductP(int id);//A
    public Product UpdateProductP(Product item);//A
    public Product GetById(int id);//A
    BO.ProductItem GetProductItem(int id, Cart cart);
    BO.ProductItem GetProductItem(int id, int i);
    public IEnumerable<ProductForList?> GetListedProducts(Func<BO.ProductForList?, bool>? filter = null);
    ProductForList GetProductForList(int productId);
   public IEnumerable<ProductItem?> GetProductItem(Func<BO.ProductItem?, bool>? filter = null);

}


