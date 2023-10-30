using DO;
namespace Dal;

internal static class DataSourse
{
    static DataSourse()
    {
        //Function for initializing the lists
        s_Initialize();
    }

    //Object for random values
    private static readonly Random s_rand = new Random();

    //Configuration
    internal static class Config
    {
        internal const int s_startOredernumber = 1000;

        private static int s_nextOrderNumber = s_startOredernumber;

        internal static int NextOrderNumber { get => s_nextOrderNumber++; }

        internal const int s_startOrderItem = 1;

        private static int s_nextOrderItem = s_startOrderItem;
        internal static int NextOrderItem { get => s_nextOrderItem++; }

    }


    //Set up lists for entities
    internal static List<Product?> ProductList { get; } = new List<Product?>();
    internal static List<Order?> OrderList { get; } = new List<Order?>();
    internal static List<OrderItem?> OrderItemList { get; } = new List<OrderItem?>();


    ////Function for initializing the lists
    private static void s_Initialize()
    {
        creatAndInitProducts();
        creatAndInitOrder();
        creatAndInitOrderItem();
        
        XMLTools.SaveListToXMLSerializer(OrderList, "Orders");
        XMLTools.SaveListToXMLSerializer(OrderItemList, "OrderItem");
        XMLTools.SaveListToXMLSerializer(ProductList, "Products");



    }

    //Create and initialize products
    private static void creatAndInitProducts()
    {
        //Array for product names
        string[] productName = new string[] { "Chocolate balls", "Carrot Cake", "Amsterdam cookie", "Chocolate Chips Cookies", "Muffins", "Macarons", "wedding cake rose design", "unicorn bat mitzvah cake", "bar mitzvah tefillin cake", "Shabbat package", " wedding anniversary package", "donuts", "apple pie", "Cheesecake", "honey cake" };

        //Array for product prices
        string[] howMuch = new string[] { "15", "65", "45", "40", "30", "25", "800", "450", "460", "85", "97", "95", "55", "63", "120", "72" };

        //initial value for product ID
        int productId = 100000;
        for (int i = 0; i < 14; i++)
        {
            if (i == 0 || i == 1)//adding a product with category 0
            {
                ProductList.Add
               (new Product
               {
                   ID = (productId + i),
                   Name = productName[i],
                   Price = Convert.ToDouble(howMuch[i]),
                   Category = (Category)1,
                   Amount = s_rand.Next(1, 40)
               });



            }
            if (i >= 2 && i < 6)//adding a product with category 1
            {
                ProductList.Add
          (new Product
          {
              ID = (productId + i),
              Name = productName[i],
              Price = Convert.ToDouble(howMuch[i]),
              Category = (Category)2,
              Amount = s_rand.Next(1, 100)
          });


            }
            if (i >= 6 && i < 9)//adding a product with category 2
            {
                ProductList.Add
                    (new Product
                    {
                        ID = (productId + i),
                        Name = productName[i],
                        Price = Convert.ToDouble(howMuch[i]),
                        Category = (Category)3,
                        Amount = s_rand.Next(1, 7)

                    }); //s_rand.Next(productName.Length)
            }
            if (i == 9)//|| i == 10)
            {
                ProductList.Add
              (new Product
              {
                  ID = (productId + i),
                  Name = productName[i],
                  Price = Convert.ToDouble(howMuch[i]),
                  Category = (Category)4,
                  Amount = s_rand.Next(1, 50)


              });
            }
            if (i == 10)//adding a product with category 4
            {
                ProductList.Add(new Product
                {
                    ID = productId + i,
                    Name = productName[11],
                    Price = Convert.ToDouble(howMuch[11]),
                    Category = Category.specials,
                    Amount = 0
                });
            }

        }
    }

    //Create and initialize orders
    private static void creatAndInitOrder()
    {
        string[] customerName = new string[] { "Dina", "Sara", "Michal", "Dan", "Hodaya", "Chaya", "Efrat", "Jacob", "Shilat", "Shira", "Ori", "Orit", "Tova", "Rivka", "Matan", "Sarit" };
        string[] customerEmail = new string[] { "sweet@gmail.com", "love@gmail.com", "h123@gmail.com", "m99@gmail.com", "3245111j@gmail.com", "pink@gmail.com", "family@gmail.com", "j111@gmail.com", "sy23488@gmail.com", "sh22@gmail.com", "ori9@gmail.com", "oritgh@gmail.com", "tov10@gmail.com", "riv12@gmail.com", "222333@gmail.com", "bluesky12@gmail.com" };
        string[] customerAdress = new string[] { "Magen David 2 Tel Aviv", "Rothschild Avenue 6 Tel Aviv", "Jerusalem Blvd 11 Rosh HaAyin ", "Rabbi Tam 8 Elad", "Elijah the Prophet 17 Ra'anana", "Ibn Gvirol 9 Shoham", "Allenby 1 Tel Aviv", "Menachem Begin 11 Herzliya", "Dizengoff 4 Tel Aviv", "King Shlomo 14 Ramat Gan", "Yehuda Halevi 23 Bnei Brak", "Yehuda Halevi 15 Bnei Brak" };
        // int Id = 100000;

        for (int i = 0; i <= 20; i++)
        {
            DateTime? randomOrderDate = new DateTime();
            DateTime? randomShipDate = new DateTime();
            DateTime? randomDeliveryDate = new DateTime();
            randomOrderDate = DateTime.Now - new TimeSpan(s_rand.Next(7), s_rand.Next(23), s_rand.Next(59), 0);
            if (i < 0.8 * 20)
            {
                randomShipDate = randomOrderDate - new TimeSpan(s_rand.Next(7), s_rand.Next(23), s_rand.Next(59), 0);
                randomOrderDate = randomShipDate - new TimeSpan(s_rand.Next(7), s_rand.Next(23), s_rand.Next(59), 0);
                if (i < 0.6 * 20)
                {
                    // randomOrderDate = randomShipDate - new TimeSpan(s_rand.Next(7), s_rand.Next(23), s_rand.Next(59), 0);//.Value.AddDays(-4).AddHours(-7); //?? delivervalue
                    randomDeliveryDate = randomShipDate + new TimeSpan(s_rand.Next(7), s_rand.Next(23), s_rand.Next(59), 0);
                    while (randomDeliveryDate >= DateTime.Now)
                    {
                        randomDeliveryDate = randomShipDate + new TimeSpan(s_rand.Next(7), s_rand.Next(23), s_rand.Next(59), 0);
                    }
                }
                else
                {
                    randomDeliveryDate = null;
                    randomOrderDate = randomOrderDate.Value.AddDays(-15).AddHours(-16);
                }

            }
            else
            {

                randomOrderDate = randomOrderDate - new TimeSpan(s_rand.Next(7), s_rand.Next(23), s_rand.Next(59), 0);
                randomShipDate = randomDeliveryDate = null;
            }
            OrderList.Add
                  (new Order
                  {
                      ID = Config.NextOrderNumber,
                      CustomerName = customerName[s_rand.Next(customerName.Length)],
                      CustomerEmail = customerEmail[s_rand.Next(customerEmail.Length)],
                      CustomerAdress = customerAdress[s_rand.Next(customerAdress.Length)],
                      OrderDate = randomOrderDate,
                      ShipDate = randomShipDate,
                      DeliveryDate = randomDeliveryDate
                  });
        }
    }

    //Create and initialize order items
    private static void creatAndInitOrderItem()
    {



        for (int i = 0; i < 40; i++)
        {
            Product? product = ProductList[i % 10];

            OrderItemList.Add(new OrderItem()
            {
                ID = Config.NextOrderItem,
                ProductID = product.Value.ID,
                OrderID = i < 21 ? OrderList[i % 21].Value.ID : OrderList[s_rand.Next(20)].Value.ID,
                Price = product.Value.Price,
                Amount = s_rand.Next(1, 3)
            });

        }

    }
}




