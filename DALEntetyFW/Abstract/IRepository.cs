using System.Collections.Generic;
using DALEntetyFW.DataModels;

namespace DALEntetyFW.Abstract
{
    public interface IRepository<Tfilter>
    {
        Tfilter Filter { get; set; }
        IEnumerable<Order> GetOrdersByCategory(Category category);

        IEnumerable<Order> GetOrdersByCategoryId(int categoryId);

        Category GetCategoryById(int categoryId);

        IEnumerable<Order> GetOrdersByFilter();
    }
}
