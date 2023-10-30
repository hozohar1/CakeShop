namespace BO
{
    public class ProductForList
    {
        /// <summary>
        /// Unique ID number 
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// Product Name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// price of product
        /// </summary>
        public double Price { set; get; }

         /// <summary>
        /// category of product
        /// </summary>
        public Category Category { set; get; }

        

        public override string ToString()
        {
            return this.ToStringProperty();
        }

    }
}
