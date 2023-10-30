namespace DO;

/// <summary>
/// Structure for Order
/// </summary>


public struct Order
{
    /// <summary>
    /// Unique ID for an order
    /// </summary>
    public int ID { set; get; }

    /// <summary>
    /// The name of the customer
    /// </summary>
    public string? CustomerName { set; get; }

    /// <summary>
    /// The E-mail of the customer
    /// </summary>
    public string? CustomerEmail { set; get; }

    /// <summary>
    /// The address of 
    /// </summary>
    public string? CustomerAdress { set; get; }

    /// <summary>
    /// The date the order was made
    /// </summary>
    public DateTime? OrderDate { set; get; }

    /// <summary>
    /// The date the order was sent
    /// </summary>
    public DateTime? ShipDate { set; get; }

    /// <summary>
    /// The date the order was delivered to the customer
    /// </summary>
    public DateTime? DeliveryDate { set; get; }

    /// <summary>
    /// Function to print object values
    /// </summary>
    /// <returns>print orderItem values</returns>
    public override string ToString()
    { return this.ToStringProperty(); }




}