using Dal;
using DalApi;
using DO;
namespace DalTest
{
    public class Program
    {
        ////private static DalOrder DalOrderObj = new DalOrder();
        ////private static DalOrderItem DalOrderItemObj = new DalOrderItem();
        ////private static DalProduct DalProductObj = new DalProduct();
        
         static DalApi.IDal? dal = DalApi.Factory.Get();
        
        static void Main(string[] args)
        {

            int a;//, b;

            Order order;
            Product product;
            OrderItem orderItem;
            OrderItem? or;
            string? actChoice;
            Console.WriteLine($@"Enter one of the options:
        0- to Exit
        1- to Order
        2- to Product
        3- to OrderItem\n");
            string? firstChoice = Console.ReadLine();
            while (firstChoice != "0")
            {
                try
                {
                    switch (firstChoice)
                    {
                        case ("1"):
                            Console.WriteLine($@"Choose one of the following Options:
        1 - Add an order
        2 - View an existing order
        3 - View all orders
        4 - Update an existing order
        5 - Delete an order");
                            actChoice = Console.ReadLine();
                            switch (actChoice)
                            {
                                case "1":
                                    order = new Order();
                                    Console.WriteLine("Enter your name: \n");
                                    order.CustomerName = Console.ReadLine();
                                    Console.WriteLine("Enter your E-mail: \n");
                                    order.CustomerEmail = Console.ReadLine();
                                    Console.WriteLine("Enter your address: \n");
                                    order.CustomerAdress = Console.ReadLine();
                                    order.OrderDate = DateTime.Now;
                                    dal?.Order.Add(order);
                                    break;

                                case "2":
                                    Console.WriteLine("Enter order ID: \n");

                                    bool n = int.TryParse(Console.ReadLine(), out a);
                                    order = dal!.Order.GetById(a);
                                    Console.WriteLine(order.ToString());
                                    break;

                                case "3":
                                    IEnumerable<Order?> list = dal!.Order.GetAll();
                                    foreach (var item in list)
                                    {
                                        Console.WriteLine(item?.ToString());
                                    }
                                    break;

                                case "4":
                                    order = new Order();
                                    Console.WriteLine("Enter your order ID: \n");
                                    bool k = int.TryParse(Console.ReadLine(), out a);
                                    order = dal!.Order.GetById(a);
                                    Console.WriteLine(order.ToString());
                                    order.ID = a;            //Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter your full name: \n");
                                    order.CustomerName = Console.ReadLine();
                                    Console.WriteLine("Enter your E-mail: \n");
                                    order.CustomerEmail = Console.ReadLine();
                                    Console.WriteLine("Enter your address: \n");
                                    order.CustomerAdress = Console.ReadLine();
                                    order.OrderDate = DateTime.Now;
                                    dal?.Order.UpDate(order);
                                    break;

                                case "5":
                                    Console.WriteLine("Enter order ID: \n");
                                    bool l = int.TryParse(Console.ReadLine(), out a);
                                    // a = Convert.ToInt32(Console.ReadLine());
                                    dal?.Order.Delete(a);
                                    break;
                            };
                            break;

                        case ("2"):
                            Console.WriteLine(($@"Choose one of the following Options:
        1 - Add an Product
        2 - View an existing Product
        3 - View all Product
        4 - Update an existing Product
        5 - Delete an Product"));
                            actChoice = Console.ReadLine();
                            switch (actChoice)
                            {
                                case "1":
                                    product = new Product();
                                    Console.WriteLine("Enter product ID: \n");
                                    bool n = int.TryParse(Console.ReadLine(), out a);
                                    product.ID = a;
                                    Console.WriteLine("Enter product name: \n");
                                    product.Name = Console.ReadLine();
                                    Console.WriteLine("Enter product price: \n");
                                    bool p = int.TryParse(Console.ReadLine(), out a);
                                    product.Price = a;//Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter product category: \n");
                                    bool j = int.TryParse(Console.ReadLine(), out a);//a= Convert.ToInt32(Console.ReadLine());
                                    product.Category = (Category)a;
                                    Console.WriteLine("Enter product amount in stock: \n");
                                    bool c = int.TryParse(Console.ReadLine(), out a);
                                    product.Amount = a;//Convert.ToInt32(Console.ReadLine());
                                    dal?.Product.Add(product);
                                    break;
                                case "2":
                                    Console.WriteLine("Enter product ID: \n");
                                    bool m = int.TryParse(Console.ReadLine(), out a);//a = Convert.ToInt32(Console.ReadLine());
                                    product = dal!.Product.GetById(a);
                                    Console.WriteLine(product.ToString());
                                    break;
                                case "3":
                                    IEnumerable<Product?> list = dal?.Product.GetAll();
                                    foreach (var item in list)
                                    {
                                        Console.WriteLine(item?.ToString());
                                    }
                                    break;
                                case "4":
                                    product = new Product();
                                    Console.WriteLine("Enter product ID: \n");
                                    bool e = int.TryParse(Console.ReadLine(), out a);
                                    product = dal!.Product.GetById(a);
                                    Console.WriteLine(product.ToString());
                                    product.ID = a; //Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter product name: \n");
                                    product.Name = Console.ReadLine();
                                    Console.WriteLine("Enter product price: \n");
                                    bool v = int.TryParse(Console.ReadLine(), out a);
                                    product.Price = a; //Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter product category:(number between 0-4) \n");
                                    bool x = int.TryParse(Console.ReadLine(), out a);//a = Convert.ToInt32(Console.ReadLine());
                                    product.Category = (Category)a;
                                    Console.WriteLine("Enter product amount in stock: \n");
                                    bool r = int.TryParse(Console.ReadLine(), out a);
                                    product.Amount = a;//Convert.ToInt32(Console.ReadLine());
                                    dal?.Product.UpDate(product);
                                    break;
                                case "5":
                                    Console.WriteLine("Enter order ID: \n");
                                    bool y = int.TryParse(Console.ReadLine(), out a);//a = Convert.ToInt32(Console.ReadLine());
                                    dal?.Product.Delete(a);
                                    break;
                            };
                            break;
                        case ("3"):
                            Console.WriteLine($@"Choose one of the following actions:
        1 - Add an OrderItem
        2 - View an existing OrderItem
        3 - View all OrderItem
        4 - Update an existing OrderItem
        5 - Deleting an OrderItem
        6 - display all items belonging to the same order
        7 - Find an item on the order");
                            actChoice = Console.ReadLine();
                            switch (actChoice)
                            {
                                case "1":
                                    orderItem = new OrderItem();
                                    Console.WriteLine("Enter order ID: \n");
                                    bool n = int.TryParse(Console.ReadLine(), out a);
                                    orderItem.OrderID = a;//Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter product ID: \n");
                                    bool h = int.TryParse(Console.ReadLine(), out a);
                                    orderItem.ProductID = a;//Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter amount of this product: \n");
                                    bool m = int.TryParse(Console.ReadLine(), out a);
                                    orderItem.Amount = a;//Convert.ToInt32(Console.ReadLine());
                                                         // product = DalProductObj.GetById(orderItem.ProductID);
                                    Console.WriteLine("Enter price: \n");
                                    double pt;
                                    bool z = double.TryParse(Console.ReadLine(), out pt);
                                    orderItem.Price = pt;
                                    //orderItem.Price = pt * product.Price;//Convert.ToDouble(orderItem.Amount)
                                    dal?.OrderItem.Add(orderItem);
                                    break;
                                case "2":
                                    Console.WriteLine("Enter orderItem ID: \n");
                                    bool y = int.TryParse(Console.ReadLine(), out a);//a = Convert.ToInt32(Console.ReadLine());
                                    orderItem = dal.OrderItem.GetById(a);
                                    Console.WriteLine(orderItem.ToString());
                                    break;
                                case "3":
                                    IEnumerable<OrderItem?> list = dal!.OrderItem.GetAll();
                                    foreach (var item in list)
                                    {
                                        Console.WriteLine(item?.ToString());
                                    }
                                    break;
                                case "4":

                                    orderItem = new OrderItem();
                                    Console.WriteLine("Enter ID: \n");
                                    bool o = int.TryParse(Console.ReadLine(), out a);
                                    //orderItem.ID = a;// Convert.ToInt32(Console.ReadLine());
                                    orderItem = dal!.OrderItem.GetById(a);
                                    Console.WriteLine(orderItem.ToString());
                                    Console.WriteLine("Enter product ID: \n");
                                    bool s = int.TryParse(Console.ReadLine(), out a);
                                    orderItem.ProductID = a;
                                    Console.WriteLine("Enter order ID: \n");
                                    bool m1 = int.TryParse(Console.ReadLine(), out a);
                                    orderItem.OrderID = a; //Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter amount of this product: \n");
                                    bool q = int.TryParse(Console.ReadLine(), out a);
                                    orderItem.Amount = a;//Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter amount price this product: \n");
                                    double q2;
                                    bool q6 = double.TryParse(Console.ReadLine(), out q2);
                                    orderItem.Price = a;
                                    dal?.OrderItem.UpDate(orderItem);
                                    break;
                                case "5":
                                    Console.WriteLine("Enter orderItem ID: \n");
                                    bool m3 = int.TryParse(Console.ReadLine(), out a);//a = Convert.ToInt32(Console.ReadLine());
                                    dal?.OrderItem.Delete(a);
                                    break;
                                case "6":
                                    Console.WriteLine("Enter order ID: \n");
                                    bool q1 = int.TryParse(Console.ReadLine(), out a);//a = Convert.ToInt32(Console.ReadLine());
                                    list = dal?.OrderItem.GetAll();
                                    foreach (var item in list)
                                    {
                                        Console.WriteLine(item?.ToString());
                                    }
                                    break;
                                case "7":
                                    Console.WriteLine("Enter order ID: \n");
                                    bool k = int.TryParse(Console.ReadLine(), out a);//a = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter product ID: \n");
                                    int e;
                                    bool b1 = int.TryParse(Console.ReadLine(), out e);//b = Convert.ToInt32(Console.ReadLine());
                                    or = dal?.OrderItem.SearchOrderItemId(e, a);
                                    Console.WriteLine(or?.ToString());
                                    break;
                            };
                            break;
                    };

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }
                Console.WriteLine($@"Enter one of the options:
        press 0 to exit
        press 1 for Order
        press 2 for Product
        press 3 for OrderItem\n");
                firstChoice = Console.ReadLine();
            }

        }



    }
}

