using DALEntetyFW.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALEntetyFW.Abstract
{
    public interface IDataContext
    {
        DbSet<Order> Orders { get; set; }
        DbSet<Category> Categories { get; set; }

    }
}
