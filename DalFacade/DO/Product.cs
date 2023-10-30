
//using System.ComponentModel;
namespace DO;


/// <summary>
/// struct for <see cref="Product"/>
/// </summary>
public struct Product
{
    /// <summary>
    /// Unique ID number 
    /// </summary>
    public int ID { set; get; }

    /// <summary>
    /// Product Name
    /// </summary>
    public string? Name { set; get; }

    /// <summary>
    /// price of product
    /// </summary>
    public double Price { set; get; }

    /// <summary>
    /// category of product
    /// </summary>
    public Category Category { set; get; }

    /// <summary>
    /// amount of available product
    /// </summary>
    public int Amount { set; get; }

    


    /// <summary>
    /// Function to print object values
    /// </summary>
    /// <returns>print orderItem values</returns>
    public override string ToString()
    { return this.ToStringProperty(); }

}
