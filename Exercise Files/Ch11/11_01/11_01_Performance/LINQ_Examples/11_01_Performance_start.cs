using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_Examples
{
    class Program
    {
        #region Class Definitions
        public class Customer
        {
            public string First { get; set; }
            public string Last { get; set; }
            public string State { get; set; }
            public double Price { get; set; }
            public string[] Purchases { get; set; }
        }

       
        #endregion

        #region Create data sources

        static List<Customer> customers = new List<Customer>
        {
            new Customer {First = "Cailin", Last = "Alford", State = "GA", Price = 930.00, Purchases = new string[] {"Panel 625", "Panel 200"}},
            new Customer {First = "Theodore", Last = "Brock", State = "AR", Price = 2100.00, Purchases = new string[] {"12V Li"}},
            new Customer {First = "Jerry", Last = "Gill", State = "MI", Price = 585.80, Purchases = new string[] {"Bulb 23W", "Panel 625"}},
            new Customer {First = "Owens", Last = "Howell", State = "GA", Price = 512.00, Purchases = new string[] {"Panel 200", "Panel 180"}},
            new Customer {First = "Adena", Last = "Jenkins", State = "OR", Price = 2267.80, Purchases = new string[] {"Bulb 23W", "12V Li", "Panel 180"}},
            new Customer {First = "Medge", Last = "Ratliff", State = "GA", Price = 1034.00, Purchases = new string[] {"Panel 625"}},
            new Customer {First = "Sydney", Last = "Bartlett", State = "OR", Price = 2105.00, Purchases = new string[] {"12V Li", "AA NiMH"}},
            new Customer {First = "Malik", Last = "Faulkner", State = "MI", Price = 167.80, Purchases = new string[] {"Bulb 23W", "Panel 180"}},
            new Customer {First = "Serena", Last = "Malone", State = "GA", Price = 512.00, Purchases = new string[] {"Panel 180", "Panel 200"}},
            new Customer {First = "Hadley", Last = "Sosa", State = "OR", Price = 590.20, Purchases = new string[] {"Panel 625", "Bulb 23W", "Bulb 9W"}},
            new Customer {First = "Nash", Last = "Vasquez", State = "OR", Price = 10.20, Purchases = new string[] {"Bulb 23W", "Bulb 9W"}},
            new Customer {First = "Joshua", Last = "Delaney", State = "WA", Price = 350.00, Purchases = new string[] {"Panel 200"}}
        };

        #endregion

        static void Main(string[] args)
        {
            #region generate large product list
            IEnumerable<string> products =
                from c in customers
                from p in c.Purchases
                select p;

            for (int i = 0; i < 10; ++i)
            {
                products = products.Concat(products);
            }
            #endregion

            const int RUNS = 1000;

            //Perform a Where query with LINQ
            DateTime start = DateTime.Now;
            for (int i = 0; i<RUNS; ++i)
            {
                var panels = (products
                    .Where(p => p.Contains("Panel"))
                    .Select(p => p)).ToArray();
            }
            TimeSpan spent = DateTime.Now - start;
            Console.WriteLine(string.Format("LINQ: {0}, avg. {1}",
                spent, new TimeSpan(spent.Ticks / RUNS)));

            //Perform a Where query with a for loop
            DateTime start2 = DateTime.Now;
            for (int i = 0; i < RUNS; ++i)
            {
                var pQuery = new List<string>();
                foreach (var p in products)
                    if (p.Contains("Panel"))
                        pQuery.Add(p);
                var panels = pQuery.ToArray();
            }

            TimeSpan spent2 = DateTime.Now - start2;
            Console.WriteLine(string.Format("Loop: {0}, avg. {1}",
                spent2, new TimeSpan(spent2.Ticks / RUNS)));

    
            Console.ReadKey();
        }

    }
}
