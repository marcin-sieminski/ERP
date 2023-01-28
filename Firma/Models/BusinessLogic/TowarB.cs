using System;
using System.Linq;
using Firma.Models.Entities;

namespace Firma.Models.BusinessLogic
{
    public class TowarB : DatabaseClass
    {
        public DateTime DataOd { get; set; }
        public DateTime DataDo { get; set; }
        public Towar WybranyTowar { get; set; }

        public TowarB()
        {

        }

        public TowarB(ERPEntities erpEntities) : base(erpEntities) 
        {
            
        }

        public decimal ObliczWartoscTowarow()
        {
            return ErpEntities.Towar.AsQueryable()
                .Where(x => x.IsActive == true)
                .Select(x => x.Cena * x.Ilosc)
                .Sum();
        }
    }
}
