using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DALEntetyFW.Abstract;
using DALEntetyFW.Concrete;

namespace DALEntetyFW.Tests
{
    [TestClass]
    public class RepositoryTests
    {
        private readonly IRepository<IFilter> _repo = new OrderRepository(new DataContext());

        [TestMethod]
        public void CanGetAllOrdersByCategory()
        {
            var category = _repo.GetCategoryById(2);
            var orders = _repo.GetOrdersByCategory(category).ToList();
            foreach (var order in orders)
            {
                Console.WriteLine(
                    Messages.Output, 
                    order.OrderID, order.Customer.ContactName, 
                    string.Join(",",order.Order_Details.Select(d => d.Product.ProductName +"category-"+ d.Product.Category.CategoryName)));
            }
            if(orders.Count > 0)
                Assert.IsTrue(orders.All(o => o.Order_Details.All(d => d.Product.Category.CategoryID == category.CategoryID)));
            else
                Console.WriteLine(Messages.NoOrders);
        }

        [TestMethod]
        public void CanGetAllOrdersByCategoryId()
        {
            var orders = _repo.GetOrdersByCategoryId(2).ToList();

            foreach (var order in orders)
            {
                Console.WriteLine(
                    Messages.Output, 
                    order.OrderID, order.Customer.ContactName, 
                    string.Join(",", order.Order_Details.Select(d => d.Product.ProductName + "category-" + d.Product.Category.CategoryName)));
            }

            if (orders.Count > 0)
                Assert.IsTrue(orders.All(o => o.Order_Details.All(d => d.Product.Category.CategoryID == 2)));
            else
                Console.WriteLine(Messages.NoOrders);
        }

        [TestMethod]
        public void CanGetOrdersByFilter()
        {
            var filter = new OrderFilter
            {
                Customer = "VINET",
                DateTo = DateTime.Parse("11/11/1997")
            };

            _repo.Filter = filter;
            
            var orders = _repo.GetOrdersByFilter().ToList();

            foreach (var order in orders)
            {
                Console.WriteLine(Messages.Output, order.OrderID, order.CustomerID, order.OrderDate);
            }

            if (orders.Count <= 0)
                Console.WriteLine(Messages.NoOrders);
        }
    }
}
