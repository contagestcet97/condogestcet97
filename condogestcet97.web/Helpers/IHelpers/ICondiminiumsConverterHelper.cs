using System.Diagnostics.Metrics;
using condogestcet97.web.Data.Entities.Condominium;
using condogestcet97.web.Models;

namespace condogestcet97.web.Helpers.IHelpers
{
    public interface ICondiminiumsConverterHelper
    {
        Condo ToCondo(CondoViewModel model, bool isNew);

        CondoViewModel ToCondoViewModel(Condo condo);


        Apartment ToApartment(ApartmentViewModel model, bool isNew);

        ApartmentViewModel ToApartmentViewModel(Apartment apartment);
    }
}
