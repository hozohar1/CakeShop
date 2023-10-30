namespace DO;
/// <summary>
/// struct for OrderItem
/// </summary>
public struct OrderItem
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
    /// Order ID number
    /// </summary>
    public int OrderID { set; get; }
    /// <summary>
    /// Price per unit
    /// </summary>
    public double Price { set; get; }

    /// <summary>
    /// Quantity-amount
    /// </summary>
    public int Amount { set; get; }

    /// <summary>
    /// Function to print object values
    /// </summary>
    /// <returns>print orderItem values</returns>
    public override string ToString()
    { return this.ToStringProperty(); }
}