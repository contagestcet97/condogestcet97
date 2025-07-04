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

        Intervention ToIntervention(InterventionViewModel model, bool isNew, Incident incident);

        InterventionViewModel ToInterventionViewModel(Intervention intervention);

        Meeting ToMeeting(MeetingViewModel model, bool isNew, Condo condo);

        MeetingViewModel ToMeetingViewModel(Meeting meeting);

        Document ToDocument(DocumentViewModel model, bool isNew, Meeting? meeting, Intervention? intervention);

        DocumentViewModel ToDocumentViewModelFromMeetingDoc(MeetingDocument document);

        DocumentViewModel ToDocumentViewModelFromInterventionDoc(InterventionDocument document);
    }

}
