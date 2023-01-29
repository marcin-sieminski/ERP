using Firma.Helpers;
using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Firma.ViewModels
{
    public class NowaFakturaViewModel : JedenWszystkieViewModel<Faktura, PozycjaFakturyForAllView>
    {
        #region Pola i właściwości
        public string Numer 
        {
            get => Item.Numer;
            set
            {
                if(Item.Numer != value)
                {
                    Item.Numer = value;
                    OnPropertyChanged(() => Numer);
                }
            }
        }
        public DateTime? DataWystawienia
        {
            get => Item.DataWystawienia;
            set
            {
                if (Item.DataWystawienia != value)
                {
                    Item.DataWystawienia = value;
                    OnPropertyChanged(() => DataWystawienia);
                }
            }
        }
        public int? IdKontrahenta
        {
            get => Item.IdKontrahenta;
            set
            {
                if (Item.IdKontrahenta != value)
                {
                    Item.IdKontrahenta = value;
                    OnPropertyChanged(() => IdKontrahenta);
                }
            }
        }
        public DateTime? TerminPlatnosci
        {
            get => Item.TerminPlatnosci;
            set
            {
                if (Item.TerminPlatnosci != value)
                {
                    Item.TerminPlatnosci = value;
                    OnPropertyChanged(() => TerminPlatnosci);
                }
            }
        }
        public int? IdSposobuPlatnosci
        {
            get => Item.IdSposobuPlatnosci;
            set
            {
                if (Item.IdSposobuPlatnosci != value)
                {
                    Item.IdSposobuPlatnosci = value;
                    OnPropertyChanged(() => IdSposobuPlatnosci);
                }
            }
        }

        private string _DaneKontrahenta;
        public string DaneKontrahenta
        {
            get => _DaneKontrahenta;
            set
            {
                if(_DaneKontrahenta != value)
                {
                    _DaneKontrahenta = value;
                    OnPropertyChanged(() => DaneKontrahenta);
                }    
            }
        }
        public List<SposobPlatnosci> SposobyPlatnosci { get; set; }

        private ICommand _ShowKontrahneciCommand;
        public ICommand ShowKontrahenciCommand 
        { 
            get
            {
                if (_ShowKontrahneciCommand == null)
                    _ShowKontrahneciCommand = new BaseCommand(() => ShowKontrahenci());
                return _ShowKontrahneciCommand;
            }
        }
        #endregion

        #region Konstruktor
        public NowaFakturaViewModel() 
            : base("Faktura", "Pozycje faktury", "Dodaj pozycję faktury")
        {
            Item = new Faktura()
            {
                CzyAktywna = true,
                CzyZatwierdzona = false,
                DataWystawienia = DateTime.Now
            };

            SposobyPlatnosci = Db.SposobPlatnosci
                .Where(item => item.CzyAktywny == true)
                .ToList();

            WszystkieList = new ObservableCollection<PozycjaFakturyForAllView>
                (
                    Item.PozycjaFaktury
                    .Where(item => item.CzyAktywny == true)
                    .Select(item => new PozycjaFakturyForAllView
                    {
                        Cena = item.Cena,
                        Ilosc = item.Ilosc,
                        Rabat = item.Rabat,
                        TowarKod = item.Towar.Kod,
                        TowarNazwa = item.Towar.Nazwa
                    })
                    .ToList()
                );

            IdSposobuPlatnosci = SposobyPlatnosci.FirstOrDefault()?.IdSposobuPlatnosci;

            Messenger.Default.Register<Kontrahent>(this, PrzypiszKontrahenta);

            Messenger.Default.Register<PozycjaFaktury>(this, PrzypiszPozycjeFaktury);
        }

        #endregion

        #region Metody
        private void PrzypiszPozycjeFaktury(PozycjaFaktury pozycjaFaktury)
        {
            Item.PozycjaFaktury.Add(pozycjaFaktury);
            var towar = Db.Towar.First(item => item.Id == pozycjaFaktury.IdTowar);
            WszystkieList.Add(new PozycjaFakturyForAllView
            {
                Cena = pozycjaFaktury.Cena,
                Ilosc = pozycjaFaktury.Ilosc,
                Rabat = pozycjaFaktury.Rabat,
                TowarKod = towar.Kod,
                TowarNazwa = towar.Nazwa
            });
        }

        private void ShowKontrahenci() => Messenger.Default.Send("Kontrahenci Show");

        protected override void Save()
        {
            Db.Faktura.AddObject(Item);
            Db.SaveChanges();
        }

        private void PrzypiszKontrahenta(Kontrahent kontrahent)
        {
            DaneKontrahenta = $"{kontrahent.Nazwa} {kontrahent.Nip}";
            IdKontrahenta = kontrahent.Id;
        }

        protected override void ShowAddView()
        {
            Messenger.Default.Send(WszystkieDisplayName + " Add");
        }
        #endregion
    }
}
