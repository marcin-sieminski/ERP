using Firma.Helpers;
using Firma.Models.BusinessLogic;
using Firma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Firma.ViewModels
{
    public class RaportViewModel : WorkspaceViewModel
    {
        public RaportLogic RaportLogic { get; set; }
        public List<Towar> Towary { get; set; }

        public DateTime DataOd 
        {
            get => RaportLogic.DataOd;
            set
            {
                if(value != RaportLogic.DataOd)
                {
                    RaportLogic.DataOd = value;
                    OnPropertyChanged(() => DataOd);
                }
            }
        }
        private decimal _Zatrudnenie;
        public decimal Zatrudnienie 
        {
            get => _Zatrudnenie;
            set
            {
                if(value != _Zatrudnenie)
                {
                    _Zatrudnenie = value;
                    OnPropertyChanged(() => Zatrudnienie);
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
                    _ObliczCommand = new BaseCommand(ObliczZatrudnienie);
                }
                return _ObliczCommand;
            }
        }

        private void ObliczZatrudnienie()
        {
            Zatrudnienie = RaportLogic.ObliczIloscPracownikow();
        }

        #region Konstruktor
        public RaportViewModel()
        {
            base.DisplayName = "Raport kadrowy";
            RaportLogic = new RaportLogic();
            
            DataOd = DateTime.Now;
        }
        #endregion
    }
}

