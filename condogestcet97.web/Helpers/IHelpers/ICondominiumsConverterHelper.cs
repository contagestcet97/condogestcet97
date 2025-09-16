using System.Diagnostics.Metrics;
using condogestcet97.web.Data.Entities.Condominium;
using condogestcet97.web.Models;

namespace condogestcet97.web.Helpers.IHelpers
{
    public interface ICondominiumsConverterHelper
    {
        Condo ToCondo(CondoViewModel model, bool isNew);

        CondoViewModel ToCondoViewModel(Condo condo);

        Apartment ToApartment(ApartmentViewModel model, bool isNew);

        ApartmentViewModel ToApartmentViewModel(Apartment apartment);


        Incident ToIncident(IncidentViewModel model, bool isNew);

        IncidentViewModel ToIncidentViewModel(Incident incident);

        Intervention ToIntervention(InterventionViewModel model, bool isNew);

        InterventionViewModel ToInterventionViewModel(Intervention intervention);

        Meeting ToMeeting(MeetingViewModel model, bool isNew);

        MeetingViewModel ToMeetingViewModel(Meeting meeting);

        Document ToDocument(DocumentViewModel model, bool isNew);

        DocumentViewModel ToDocumentViewModelFromMeetingDoc(MeetingDocument document);

        DocumentViewModel ToDocumentViewModelFromInterventionDoc(InterventionDocument document);

        Vote ToVote(VoteViewModel model, bool isNew);

        VoteViewModel ToVoteViewModel(Vote vote);
    }

}
