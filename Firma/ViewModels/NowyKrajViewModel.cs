using Firma.Models.Entities;
using Firma.ViewModels.Abstract;
using System.ComponentModel;
using Firma.Models.Validators;

namespace Firma.ViewModels
{
    public class NowyKrajViewModel : JedenViewModel<Kraj>, IDataErrorInfo
    {
        #region Konstruktor
        public NowyKrajViewModel() : base("Kraj")
        {
            Item = new Kraj();
        }
        #endregion

        #region Properties
        public string Nazwa
        {
            get => Item.Nazwa;
            set
            {
                if (value != Item.Nazwa)
                {
                    Item.Nazwa = value;
                    base.OnPropertyChanged(() => Nazwa);
                }
            }
        }
        #endregion

        #region MyRegion
        protected override void Save()
        {
            Item.IsActive = true;
            Db.Kraj.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
        #region Walidacja
        protected override bool IsValid()
        {
            return
                this[nameof(Nazwa)] == string.Empty &&
                NazwaValidator.SprawdzNazwe(Nazwa) == string.Empty;
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Nazwa):
                        return NazwaValidator.SprawdzNazwe(Nazwa);
                    default:
                        return string.Empty;
                }
            }
        }
        
        public string Error => string.Empty;
        #endregion
    }
}
