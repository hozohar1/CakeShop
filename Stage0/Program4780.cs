namespace Stage0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome4780();
            Welcome9990();
            Console.ReadKey();
        }
        static partial void Welcome9990();
        private static void Welcome4780()
        {
            Console.WriteLine("Enter your name:");
            string myStringVar = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", myStringVar);
        }
    }
}
