using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ToyApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection cn = new SqlConnection("Data Source=LAPTOP-TJ920A78;Initial Catalog=Company;Integrated Security=True");
            cn.Open();
            Program p = new Program();

            int choice;

            do
            {
                Console.WriteLine("Your are an 1.Admin 2.Customer 3.Exit");
                Console.WriteLine("Enter your choice");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        p.Admin();
                        break;
                    case 2:
                        p.Customer();
                        break;
                    case 3:
                        Console.WriteLine("Exit");
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }
            }
            while (choice != 3);
        }

        public void Admin()
        {
            int choice;
            string Name, Password;
            Console.WriteLine("Enter your name");
            Name = Console.ReadLine();
            Console.WriteLine("Enter Password");
            Password = Console.ReadLine();
            if (Name == "admin" && Password == "123456")
            {
                do
                {
                    Console.WriteLine(" 1.Add Manufacturing Plants 2.Add Toys Details 3. Exit");
                    Console.WriteLine("Enter your choice");
                    choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            AddManufacturingPlant();
                            break;
                        case 2:
                            AddToyDetails();
                            break;
                        case 3:
                            Console.WriteLine("Exit");
                            break;
                        default:
                            Console.WriteLine("Invalid Choice");
                            break;
                    }
                }
                while (choice != 3);
            }
            else
            {
                Console.WriteLine("Invalid Credentials");
            }

        }

        public void AddManufacturingPlant()
        {
            using (var database = new CompanyEntities())
            {
                var manufacturingplant = new ManufacturingPlant();
                Console.WriteLine("Enter name of manufacturing plant:" + manufacturingplant.PlantName);
                manufacturingplant.PlantName = Console.ReadLine();

                var mp = database.ManufacturingPlants.SingleOrDefault(t => t.PlantName == manufacturingplant.PlantName);
                if (mp == null)
                {
                    database.ManufacturingPlants.Add(manufacturingplant);
                    database.SaveChanges();
                    Console.WriteLine("Manufacturing Plant added to the list");
                }

                else
                {
                    Console.WriteLine("There is already such plant in the list");
                    throw new DuplicateWaitObjectException("There is already such plant in the list");
                }
            }
        }

        public void AddToyDetails()
        {
            using (var database = new CompanyEntities())
            {
                var toydetail = new Toy();
                Console.WriteLine("Enter name of toy:" + toydetail.ToyName);
                toydetail.ToyName = Console.ReadLine();

                Console.WriteLine("Enter price of toy:" + toydetail.Price);
                toydetail.Price = Convert.ToInt32(Console.ReadLine());

                var mplist = database.ManufacturingPlants;
                {
                    foreach (var mpl in mplist)
                    {
                        Console.WriteLine($"List of Maufacturing Plants - {mpl.ManufacturingPlantId} - {mpl.PlantName}");
                    }
                }
                Console.WriteLine($"Enter Plant Id from above list:" + toydetail.ManufacturingPlantId);
                toydetail.ManufacturingPlantId = Convert.ToInt32(Console.ReadLine());
                var manu = database.ManufacturingPlants.SingleOrDefault(t => t.ManufacturingPlantId == toydetail.ManufacturingPlantId);
                if (manu == null)
                {

                    Console.WriteLine("There is no such manufacturing plant");
                    Console.WriteLine("Please Enter valid details");

                }


                var toy = database.Toys.SingleOrDefault(t => t.ToyName == toydetail.ToyName);
                if (toy == null)
                {
                    database.Toys.Add(toydetail);
                    database.SaveChanges();
                    Console.WriteLine("Toy details added to the list");
                }

                else
                {
                    Console.WriteLine("There is already such toy in the list");
                    throw new DuplicateWaitObjectException("There is already such toy in the list");
                }
            }
        }

        public void Customer()
        {
            int choice;

            do
            {
                Console.WriteLine(" 1.New Customer 2.Existing Customer 3. Add Shipping Address 4. Exit");
                Console.WriteLine("Enter your choice");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AddCustomer();
                        break;
                    case 2:
                        ExistingCustomer();
                        break;
                    case 3:
                        AddShippingAddress();
                        break;
                    case 4:
                        Console.WriteLine("Exit");
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }
            }
            while (choice != 3);

        }

        public void AddCustomer()
        {
            using (var database = new CompanyEntities())
            {
                var customer = new Customer();
                Console.WriteLine("Enter your Name:" + customer.Name);
                customer.Name = Console.ReadLine();

                Console.WriteLine("Enter your mobile number" + customer.MobileNumber);
                customer.MobileNumber = Console.ReadLine();

                var cust = database.Customers.SingleOrDefault(t => t.Name == customer.Name && t.MobileNumber == customer.MobileNumber);
                if (cust == null)
                {
                    database.Customers.Add(customer);
                    database.SaveChanges();
                    Console.WriteLine("Customer Added to the list");
                }
                else
                {
                    Console.WriteLine("There is already such customer in the list");
                    throw new DuplicateWaitObjectException("There is already such customer in the list");
                }

                AddShippingAddress();
            }
        }

        public void ExistingCustomer()
        {
            int choice;

            do
            {
                Console.WriteLine(" 1.Place Order 2.View Order Details 3.View Shipping Details 4.Exit");
                Console.WriteLine("Enter your choice");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        BuyToys();
                        break;
                    case 2:
                        ViewOrder();
                        break;
                    case 3:
                        ShippingDetail();
                        break;
                    case 4:
                        Console.WriteLine("Exit");
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }
            }
            while (choice != 3);
        }

        public void AddShippingAddress()
        {
            using (var database = new CompanyEntities())
            {
                int customerId;
                var customer = new Customer();
                Console.WriteLine("Enter your Name:" + customer.Name);
                customer.Name = Console.ReadLine();

                var name = database.Customers.SingleOrDefault<Customer>(t => t.Name == customer.Name);

                if (name != null)
                {
                    Console.WriteLine("Please Fill your shipping Details:");
                    var shipping = new ShippingDetail();
                    customerId = name.CustomerId;
                    shipping.CustomerId = customerId;
                    Console.WriteLine("Enter your Address:" + shipping.Address);
                    shipping.Address = Console.ReadLine();

                    Console.WriteLine("Enter your City:" + shipping.City);
                    shipping.City = Console.ReadLine();

                    Console.WriteLine("Enter your State:" + shipping.State);
                    shipping.State = Console.ReadLine();

                    Console.WriteLine("Enter your Country:" + shipping.Country);
                    shipping.Country = Console.ReadLine();

                    database.ShippingDetails.Add(shipping);
                    database.SaveChanges();
                    Console.WriteLine("Shipping Address Added");

                }
                else
                {
                    Console.WriteLine("You are not registered");
                    Console.WriteLine("Please register yourself");
                    AddCustomer();
                }

            }
        }

        public void BuyToys()
        {
            using (var database = new CompanyEntities())
            {
                int customerId;
                int toyId;
                int price;
                var customer = new Customer();
                var toyprice = new Toy();

                Console.WriteLine("Enter your Name:" + customer.Name);
                customer.Name = Console.ReadLine();

                var name = database.Customers.SingleOrDefault<Customer>(t => t.Name == customer.Name);

                if (name != null)
                {
                    var toylist = database.Toys;
                    {
                        foreach (var toy in toylist)
                        {
                            Console.WriteLine($"List of Toys - {toy.ToyId} - {toy.ToyName} - {toy.Price}");
                        }
                    }

                    var orderlist = new OrderItem();

                    customerId = name.CustomerId;
                    orderlist.CustomerId = customerId;
                    Console.WriteLine("Select toy from above list:" + toyprice.ToyId);
                    toyprice.ToyId = Convert.ToInt32(Console.ReadLine());

                    var tl = database.Toys.SingleOrDefault<Toy>(t => t.ToyId == toyprice.ToyId);

                    if (tl == null)
                    {

                        Console.WriteLine("There is no such toy");
                        Console.WriteLine("Please Enter valid details");

                    }

                    toyId = tl.ToyId;
                    orderlist.ToyId = toyId;


                    Console.WriteLine("Enter Quantity for selected toy:" + orderlist.Quantity);
                    orderlist.Quantity = Convert.ToInt32(Console.ReadLine());

                    price = tl.Price;
                    orderlist.Price = price;

                    var to = database.OrderItems.SingleOrDefault(t => t.ToyId == orderlist.ToyId && t.CustomerId == orderlist.CustomerId);
                    if (to == null)
                    {
                        database.OrderItems.Add(orderlist);
                        database.SaveChanges();
                    }
                    var orders = database.spOrders(orderlist.ToyId, orderlist.CustomerId, orderlist.Quantity, 0);
                    var schemes = database.spSchemes(orderlist.CustomerId);
                    database.SaveChanges();

                    Console.WriteLine("Added to your order list");
                    ShippingDetail();
                }

                else
                {
                    Console.WriteLine("You are not registered");
                    Console.WriteLine("Please register yourself");
                    AddCustomer();
                }

            }
        }

        public void ShippingDetail()
        {
            using (var database = new CompanyEntities())
            {

                var shippingDetail = database.ShippingDetails;
                int customerId;
                var customer = new Customer();
                Console.WriteLine("Enter your Name:" + customer.Name);
                customer.Name = Console.ReadLine();

                var name = database.Customers.SingleOrDefault<Customer>(t => t.Name == customer.Name);
                customerId = name.CustomerId;

                if (name != null)
                {
                    var shippinglist = database.ShippingDetails.ToList().Where(t=>t.CustomerId == name.CustomerId);
                    Console.WriteLine("Select your shipping detail:");
                    {
                        foreach (var sd in shippinglist)
                        {
                            Console.WriteLine($"{sd.Address} - {sd.City} - {sd.State} - {sd.Country}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("You are not registered");
                    Console.WriteLine("Please register yourself");
                    AddCustomer();
                }

            }
        }

        public void ViewOrder()
        {
            using (var database = new CompanyEntities())
            {
                int customerId;
                var customer = new Customer();
                Console.WriteLine("Enter your Name:" + customer.Name);
                customer.Name = Console.ReadLine();
                var name = database.Customers.SingleOrDefault<Customer>(t => t.Name == customer.Name);
                customerId = name.CustomerId;
                
                if (name != null)
                {
                    var orderlist = database.vOrderItems.ToList().Where(t=>t.CustomerId == name.CustomerId);
                    {
                        Console.WriteLine("Your Orders");
                        foreach (var order in orderlist)
                        {
                            Console.WriteLine($"{order.ToyName} - {order.Price} - {order.Quantity} - {order.TotalPrice} - {order.OrderValue}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("You are not registered");
                    Console.WriteLine("Please register yourself");
                    AddCustomer();
                }
            }
        }


    }
}


