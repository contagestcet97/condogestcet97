using System.Diagnostics.Metrics;
using condogestcet97.web.Data.Entities.Condominium;
using condogestcet97.web.Models;

namespace condogestcet97.web.Helpers.IHelpers
{
    public interface ICondiminiumsConverterHelper
    {
        Condo ToCondo(CondoViewModel model, bool isNew);

        CondoViewModel ToCondoViewModel(Condo condo);


        Apartment ToApartment(ApartmentViewModel model, bool isNew, Condo condo);

        ApartmentViewModel ToApartmentViewModel(Apartment apartment);


        Incident ToIncident(IncidentViewModel model, bool isNew, Apartment apartment, Condo condo);

        IncidentViewModel ToIncidentViewModel(Incident incident);

    }

}
