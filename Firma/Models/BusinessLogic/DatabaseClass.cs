using Firma.Models.Entities;

namespace Firma.Models.BusinessLogic
{
    public class DatabaseClass
    {
        public ERPEntities ErpEntities { get; set; }

        public DatabaseClass()
        {
            ErpEntities = new ERPEntities();
        }

        public DatabaseClass(ERPEntities erpEntities)
        {
            ErpEntities = erpEntities;
        }
    }
}
