namespace BO
{
    public class Product
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
        /// amount of available product
        /// </summary>
        public int Amount { set; get; }
        public string? ImageRelativeName { get; set; }

       

        /// <summary>
        /// Function to print object values
        /// </summary>
        /// <returns>print product values </returns>
        public override string ToString()
        { return this.ToStringProperty(); }
    }
}

