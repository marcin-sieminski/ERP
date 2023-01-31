using Firma.Models.Entities;
using Firma.ViewModels.Abstract;
using System.ComponentModel;
using System.Windows.Input;
using Firma.Helpers;
using Firma.Models.Validators;
using Firma.Models.EntitiesForView;
using GalaSoft.MvvmLight.Messaging;

namespace Firma.ViewModels
{
    public class NowyTowarViewModel : JedenViewModel<Towar>, IDataErrorInfo
    {
        #region Konstruktor
        public NowyTowarViewModel() : base("Towar")
        {
            Item = new Towar()
            {
                IsActive = true
            };

            Messenger.Default.Register<PracownikForAllView>(this, PrzypiszPracownika);
        }
        #endregion

        #region Properties
        public string Kod 
        {
            get => Item.Kod;
            set
            {
                if (value != Item.Kod)
                {
                    Item.Kod = value;
                    base.OnPropertyChanged(() => Kod);
                }
            }
        }

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

        public decimal? StawkaVatSprzedazy
        {
            get => Item.StawkaVatSprzedazy;
            set
            {
                if (value != Item.StawkaVatSprzedazy)
                {
                    Item.StawkaVatSprzedazy = value.Value;
                    base.OnPropertyChanged(() => StawkaVatSprzedazy);
                }

            }
        }

        public decimal? StawkaVatZakupu
        {
            get => Item.StawkaVatZakupu;
            set
            {
                if (value != Item.StawkaVatZakupu)
                {
                    Item.StawkaVatZakupu = value;
                    base.OnPropertyChanged(() => StawkaVatZakupu);
                }
            }
        }

        public decimal? Cena
        {
            get => Item.Cena;
            set
            {
                if (value != Item.Cena)
                {
                    Item.Cena = value.Value;
                    base.OnPropertyChanged(() => Cena);
                }
            }
        }

        public decimal? Marza
        {
            get => Item.Marza;
            set
            {
                if (value != Item.Marza)
                {
                    Item.Marza = value.Value;
                    base.OnPropertyChanged(() => Marza);
                }
            }
        }

        public int? IdPracownika
        {
            get => Item.OpiekunId;
            set
            {
                if (value != Item.OpiekunId)
                {
                    Item.OpiekunId = value.Value;
                    base.OnPropertyChanged(() => Marza);
                }
            }
        }

        private string _DanePracownika;
        public string DanePracownika
        {
            get => _DanePracownika;
            set
            {
                if(_DanePracownika != value)
                {
                    _DanePracownika = value;
                    OnPropertyChanged(() => DanePracownika);
                }    
            }
        }
        private ICommand _ShowPracownicyCommand;
        public ICommand ShowPracownicyCommand 
        { 
            get
            {
                if (_ShowPracownicyCommand == null)
                {
                    _ShowPracownicyCommand = new BaseCommand(() => ShowPracownicy());
                    return _ShowPracownicyCommand;
                }

                return _ShowPracownicyCommand;
            }
        }

        #endregion

        #region Metody
        protected override void Save()
        {
            Item.IsActive = true;
            Db.Towar.AddObject(Item);
            Db.SaveChanges();
        }
        
        private void ShowPracownicy() => Messenger.Default.Send("Pracownicy Show");

        private void PrzypiszPracownika(PracownikForAllView item)
        {
            DanePracownika = $"{item.Imie} {item.Nazwisko}";
            IdPracownika = item.Id;
        }

        #endregion

        #region Walidacja
        protected override bool IsValid()
        {
            return
                this[nameof(Kod)] == string.Empty &&
                KodTowarValidator.SprawdzKodTowaru(Kod) == string.Empty;
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Kod):
                        return KodTowarValidator.SprawdzKodTowaru(Kod);
                    default:
                        return string.Empty;
                }
            }
        }
        
        public string Error => string.Empty;
        #endregion

    }
}
