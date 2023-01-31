using System;
using System.Linq;
using Firma.Models.Entities;

namespace Firma.Models.BusinessLogic
{
    public class RaportLogic : DatabaseClass
    {
        public DateTime DataOd { get; set; }

        public RaportLogic()
        {

        }

        public RaportLogic(ERPEntities erpEntities) : base(erpEntities) 
        {
            
        }

        public int ObliczIloscPracownikow()
        {
            return ErpEntities.Pracownik
                .AsQueryable()
                .Count(x => x.ZatrudnionyOd <= DataOd && x.ZatrudnionyDo >= DataOd);
        }
    }
}
