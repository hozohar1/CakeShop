namespace BO
{
    public class ProductItem
    {
        /// <summary>
        /// Unique ID number 
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// Product Name
        /// </summary>
        public String? Name { get; set; }

        /// <summary>
        /// price of product
        /// </summary>
        public double Price { set; get; }

        /// <summary>
        /// category of product
        /// </summary>
        public Category Category { set; get; }


        /// <summary>
        /// Product availability
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// Quantity in the customer's shopping cart
        /// </summary>
        public int CartQuantity { get; set; }
        public string? ImageRelativeName { get; set; }


      
        public override string ToString()
        {
            return this.ToStringProperty();
        }






    }
}
