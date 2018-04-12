using System;
using System.Collections.Generic;
using System.Linq;
using DALEntetyFW.Abstract;
using DALEntetyFW.DataModels;

namespace DALEntetyFW.Concrete
{
    public class OrderRepository : IRepository<IFilter>
    {
        private readonly IDataContext _context;
        public OrderRepository(IDataContext context)
        {
            _context = context;
            Filter = new OrderFilter();
        }

        public IFilter Filter { get; set; }

        public Category GetCategoryById(int categoryId)
        {
            return _context.Categories.FirstOrDefault(_ => _.CategoryID  == categoryId);
        }

        public IEnumerable<Order> GetOrdersByCategory(Category category) => _context.Orders
                .Where(_ => _.Order_Details
                    .All(d => d.Product.Category.CategoryID == category.CategoryID));

        public IEnumerable<Order> GetOrdersByCategoryId(int categoryId) => _context.Orders
                .Where(_ => _.Order_Details
                    .All(d => d.Product.Category.CategoryID == categoryId));

        public IEnumerable<Order> GetOrdersByFilter()
        {
            return !(Filter is OrderFilter filter) 
                ? _context.Orders 
                : _context.Orders
                .Where(_ => (filter.Customer == null || _.CustomerID == filter.Customer) 
                            && (filter.DateFrom ==null || _.OrderDate >= filter.DateFrom)
                            && (filter.DateTo==null || _.OrderDate <= filter.DateTo))
                .OrderBy(_=>_.OrderID)
                .Skip(filter.Skip?? 0)
                .Take(filter.Take?? int.MaxValue);
        }
    }
}
