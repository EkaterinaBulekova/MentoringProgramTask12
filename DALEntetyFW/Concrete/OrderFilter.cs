using System;
using DALEntetyFW.Abstract;

namespace DALEntetyFW.Concrete
{
    public class OrderFilter : IFilter
    {
        public string Customer { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public int? Take { get; set; }

        public int? Skip { get; set; }
    }
}