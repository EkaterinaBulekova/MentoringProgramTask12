using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALEntetyFW.Abstract;
using DALEntetyFW.Concrete;
using Ninject;
using Ninject.Modules;

namespace DALEntetyFW
{
    public class DalModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataContext>().To<DataContext>();
            Bind<IRepository<IFilter>>().To<OrderRepository>();
        }
    }
}
