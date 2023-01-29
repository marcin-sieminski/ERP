using Firma.Helpers;
using Firma.Models.BusinessLogic;
using Firma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Firma.ViewModels
{
    public class RaportSprzedazyViewModel : WorkspaceViewModel
    {
        public TowarB TowarB { get; set; }
        public List<Towar> Towary { get; set; }

        public DateTime DataOd 
        {
            get => TowarB.DataOd;
            set
            {
                if(value != TowarB.DataOd)
                {
                    TowarB.DataOd = value;
                    OnPropertyChanged(() => DataOd);
                }
            }
        }
        public DateTime DataDo
        {
            get => TowarB.DataDo;
            set
            {
                if (value != TowarB.DataDo)
                {
                    TowarB.DataDo = value;
                    OnPropertyChanged(() => DataDo);
                }
            }
        }

        public Towar WybranyTowar 
        {
            get => TowarB.WybranyTowar;
            set
            {
                if(value != TowarB.WybranyTowar)
                {
                    TowarB.WybranyTowar = value;
                    OnPropertyChanged(() => WybranyTowar);
                }
            }
        }


        private decimal _Utarg;
        public decimal Utarg 
        {
            get => _Utarg;
            set
            {
                if(value != _Utarg)
                {
                    _Utarg = value;
                    OnPropertyChanged(() => Utarg);
                }
            }
        }

        private ICommand _ObliczCommand;
        public ICommand ObliczCommand
        {
            get
            {
                if(_ObliczCommand == null)
                {
                    _ObliczCommand = new BaseCommand(ObliczUtarg);
                }
                return _ObliczCommand;
            }
        }

        private void ObliczUtarg()
        {
            Utarg = TowarB.ObliczWartoscTowarow();
        }

        #region Konstruktor
        public RaportSprzedazyViewModel()
        {
            base.DisplayName = "Raport sprzedaży";
            TowarB = new TowarB();
            Towary = TowarB.ErpEntities.Towar
                .Where(item => item.IsActive == true)
                .ToList();
            DataOd = DateTime.Now;
            DataDo = DateTime.Now;
            WybranyTowar = Towary.FirstOrDefault();
        }
        #endregion
    }
}

