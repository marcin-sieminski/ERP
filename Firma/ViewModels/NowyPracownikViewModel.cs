using System;
using System.Collections.Generic;
using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;
using Firma.ViewResources;
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel;
using System.Linq;
using Firma.Helpers;
using Firma.Models.Validators;
using System.Windows.Input;

namespace Firma.ViewModels
{
    internal class NowyPracownikViewModel : JedenViewModel<Pracownik>, IDataErrorInfo
    {
        #region Konstruktory

        public NowyPracownikViewModel() : base(BaseResources.NowyPracownik)
        {
            Item = new Pracownik()
            {
                IsActive = true
            };

            Kraje = Db.Kraj
                .Where(item => item.IsActive)
                .ToList();

            Miasta = Db.Miasto
                .Where(item => item.IsActive)
                .ToList();

            Wojewodztwa = Db.Wojewodztwo
                .Where(item => item.IsActive)
                .ToList();
            
            Gminy = Db.Gmina
                .Where(item => item.IsActive == true)
                .ToList();

            KrajId = Kraje.FirstOrDefault()?.Id;
            MiastoId = Miasta.FirstOrDefault()?.Id;
            WojewodztwoId = Wojewodztwa.FirstOrDefault()?.Id;
            GminaId = Gminy.FirstOrDefault()?.Id;

        }
        #endregion

        #region Properties

        public string Imie
        {
            get => Item.Imie;
            set
            {
                if (value != Item.Imie)
                {
                    Item.Imie = value;
                    base.OnPropertyChanged(() => Imie);
                }
            }
        }

        public string Nazwisko
        {
            get => Item.Nazwisko;
            set
            {
                if (value != Item.Nazwisko)
                {
                    Item.Nazwisko = value;
                    base.OnPropertyChanged(() => Nazwisko);
                }
            }
        }

        public string NazwiskoRodowe
        {
            get => Item.NazwRodowe;
            set
            {
                if (value != Item.NazwRodowe)
                {
                    Item.NazwRodowe = value;
                    base.OnPropertyChanged(() => NazwiskoRodowe);
                }
            }
        }

        public string NIP
        {
            get => Item.NipKraj;
            set
            {
                if (value != Item.NipKraj)
                {
                    Item.NipKraj = value;
                    base.OnPropertyChanged(() => NIP);
                }
            }
        }

        public string Pesel
        {
            get => Item.Pesel;
            set
            {
                if (value != Item.Pesel)
                {
                    Item.Pesel = value;
                    base.OnPropertyChanged(() => Pesel);
                }
            }
        }

        public DateTime? DataUrodzenia
        {
            get => Item.DataUr;
            set
            {
                if (value != Item.DataUr)
                {
                    Item.DataUr = value;
                    OnPropertyChanged(() => DataUrodzenia);
                }
            }
        }

        public DateTime? ZatrudnionyOd
        {
            get => Item.ZatrudnionyOd;
            set
            {
                if (value != Item.ZatrudnionyOd)
                {
                    Item.ZatrudnionyOd = value;
                    OnPropertyChanged(() => ZatrudnionyOd);
                }
            }
        }
        public DateTime? ZatrudnionyDo
        {
            get => Item.ZatrudnionyDo;
            set
            {
                if (value != Item.ZatrudnionyDo)
                {
                    Item.ZatrudnionyDo = value;
                    OnPropertyChanged(() => ZatrudnionyDo);
                }
            }
        }

        public List<Kraj> Kraje { get; set; }
        public int? KrajId
        {
            get => Item.KrajId;
            set
            {
                if (Item.KrajId != value)
                {
                    Item.KrajId = value;
                    OnPropertyChanged(() => KrajId);
                }
            }
        }
        public List<Miasto> Miasta { get; set; }
        public int? MiastoId
        {
            get => Item.MiastoId;
            set
            {
                if (value != Item.MiastoId)
                {
                    Item.MiastoId = value;
                    OnPropertyChanged(() => MiastoId);
                }
            }
        }
        public List<Wojewodztwo> Wojewodztwa { get; set; }
        public int? WojewodztwoId
        {
            get => Item.WojewodztwoId;
            set
            {
                if (value != Item.WojewodztwoId)
                {
                    Item.WojewodztwoId = value;
                    OnPropertyChanged(() => WojewodztwoId);
                }
            }
        }
        public List<Gmina> Gminy { get; set; }
        public int? GminaId
        {
            get => Item.GminaId;
            set
            {
                if (value != Item.GminaId)
                {
                    Item.GminaId = value;
                    OnPropertyChanged(() => GminaId);
                }
            }
        }
        
        #endregion

        #region Metody
        protected override void Save()
        {
            Item.IsActive = true;
            Db.Pracownik.AddObject(Item);
            Db.SaveChanges();
        }

        #endregion

        #region Walidacja
        protected override bool IsValid()
        {
            return
                PracownikValidator.SprawdzCzyPuste(Imie) == string.Empty &&
                PracownikValidator.SprawdzCzyPuste(Nazwisko) == string.Empty &&
                PracownikValidator.SprawdzNip(NIP) == string.Empty &&
                PracownikValidator.SprawdzPesel(Pesel) == string.Empty;
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(NIP):
                        return PracownikValidator.SprawdzNip(NIP);
                    case nameof(Pesel):
                        return PracownikValidator.SprawdzPesel(Pesel);
                    case nameof(Imie):
                        return PracownikValidator.SprawdzCzyPuste(Imie);
                    case nameof(Nazwisko):
                        return PracownikValidator.SprawdzCzyPuste(Nazwisko);
                    default:
                        return string.Empty;
                }
            }
        }

        public string Error => string.Empty;
        #endregion

    }
}