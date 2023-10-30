namespace BO
{
    public class OrderForList
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
        /// The status of the order
        /// </summary>
        public OrderStatus OrderStatus { get; set; }

        /// <summary>
        /// Final price payable
        /// </summary>
        public double TotalPrice { get; set; }

        /// <summary>
        /// Quantity-amount
        /// </summary>
        public int Amount { set; get; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }




    }
}
