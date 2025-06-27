using System.Diagnostics.Metrics;
using condogestcet97.web.Data.Entities.Condominium;
using condogestcet97.web.Helpers.IHelpers;
using condogestcet97.web.Models;

namespace condogestcet97.web.Helpers
{
    public class CondominiumsConverterHelper : ICondiminiumsConverterHelper
    {
        public Apartment ToApartment(ApartmentViewModel model, bool isNew)
        {
            return new Apartment
            {
                Id = isNew ? 0 : model.Id,
                Condo = model.Condo,
                Flat = model.Flat,
                Divisions = model.Divisions
            };
        }

        public ApartmentViewModel ToApartmentViewModel(Apartment apartment)
        {
            return new ApartmentViewModel
            {
                Id = apartment.Id,
                Condo = apartment.Condo,
                Flat = apartment.Flat,
                Divisions = apartment.Divisions
            };
        }

        public Condo ToCondo(CondoViewModel model, bool isNew)
        {
            return new Condo
            {
                Id = isNew ? 0 : model.Id,
                Address = model.Address
            };
        }

        public CondoViewModel ToCondoViewModel(Condo condo)
        {
            return new CondoViewModel
            {
                Id = condo.Id,
                Address = condo.Address            
            };
        }


    }
}
