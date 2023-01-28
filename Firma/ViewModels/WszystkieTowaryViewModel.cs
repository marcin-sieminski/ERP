using System.Collections.ObjectModel;
using System.Linq;
using Firma.Models.Entities;
using Firma.Models.EntitiesForView;

namespace Firma.ViewModels
{
    public class WszystkieTowaryViewModel : WszystkieViewModel<TowaryForAllView>
    {
        #region  Constructor
        public  WszystkieTowaryViewModel() : base("Towary")
        {
        }
        #endregion 
        
        #region  Helpers
        public override void Load()
        {
            List  =  new  ObservableCollection<TowaryForAllView>
                (
                    from towar in  ERPEntities.Towar  select new TowaryForAllView
                    {
                        Id = towar.Id,
                        KodCN = towar.KodCN.Kod,
                        NumerKatalogowy = towar.NumerKat,
                        Nazwa = towar.Nazwa,
                        URL= towar.URL,
                        KategoriaKod = towar.Kategoria.Kod,
                        Opis = towar.Opis,
                        JM = towar.JM,
                        Waluta = towar.Waluta,
                        Cena = towar.Cena,
                        NazwaKontrahenta = towar.Kontrahent.Nazwa,
                        NazwaProducenta = towar.Producent.Nazwa,
                        Waga = towar.WagaKG.Value,
                        OpiekunImieNazwisko = towar.Pracownik.Imie1 + " " + towar.Pracownik.Nazwisko
                    }
                );
        }
        #endregion

    }
}
