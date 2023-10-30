namespace BO
{
    public class Order
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
        /// The status of the order
        /// </summary>
        public OrderStatus OrderStatus { get; set; }


        /// <summary>
        /// Final price payable
        /// </summary>
        public double TotalPrice { get; set; }


        /// <summary>
        /// The list of items in the order
        /// </summary>
        public IEnumerable<OrderItem?> OrderItems { get; set; }

        /// <summary>
        /// A function that prints the values of the order details
        /// </summary>
        /// <returns>print order values</returns>
        public override string ToString()
        { return this.ToStringProperty(); }

    }


}
