namespace BO;

public class OrderItem
{
    /// <summary>
    /// Unique ID number 
    /// </summary>
    public int ID { set; get; }

    /// <summary>
    /// Product ID number
    /// </summary>
    public int ProductID { set; get; }

    /// <summary>
    /// Product Name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Price per unit
    /// </summary>
    public double Price { set; get; }

    /// <summary>
    /// Quantity-amount
    /// </summary>
    public int Amount { set; get; }

    /// <summary>
    /// Final price of this Item in order
    /// </summary>
    public double TotalPriceForItem { get; set; }

    /// <summary>
    /// Function to print object values
    /// </summary>
    /// <returns>print orderItem values</returns>
    public override string ToString()
    { return this.ToStringProperty(); }
}

