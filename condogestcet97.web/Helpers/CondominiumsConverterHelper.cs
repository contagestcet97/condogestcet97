using System.Diagnostics.Metrics;
using condogestcet97.web.Data.Entities.Condominium;
using condogestcet97.web.Helpers.IHelpers;
using condogestcet97.web.Models;

namespace condogestcet97.web.Helpers
{
    public class CondominiumsConverterHelper : ICondominiumsConverterHelper
    {
        public Apartment ToApartment(ApartmentViewModel model, bool isNew, Condo condo)
        {
            return new Apartment
            {
                Id = isNew ? 0 : model.Id,
                Condo = condo,
                Flat = model.Flat,
                Divisions = model.Divisions
            };
        }

        public ApartmentViewModel ToApartmentViewModel(Apartment apartment)
        {
            return new ApartmentViewModel
            {
                Id = apartment.Id,
                CondoId = apartment.Condo.Id,
                CondoAddress = apartment.Condo.Address,
                Flat = apartment.Flat,
                Divisions = apartment.Divisions
            };
        }

        public Condo ToCondo(CondoViewModel model, bool isNew)
        {
            return new Condo
            {
                Id = isNew ? 0 : model.CondoId,
                Address = model.Address
            };
        }

        public CondoViewModel ToCondoViewModel(Condo condo)
        {
            return new CondoViewModel
            {
                CondoId = condo.Id,
                Address = condo.Address            
            };
        }

        public Document ToDocument(DocumentViewModel model, bool isNew, Meeting? meeting, Intervention? intervention)
        {
            if (model.Type == DocumentType.Intervention)
            {
                return new InterventionDocument
                {
                    Id = isNew ? 0 : model.Id,
                    Subject = model.Subject,
                    Description = model.Description,
                    EmissionDate = model.EmissionDate,
                    Intervention = intervention,
                    Type = DocumentType.Intervention
                    
                };
            }

            return new MeetingDocument
            {
                Id = isNew ? 0 : model.Id,
                Subject = model.Subject,
                Description = model.Description,
                EmissionDate = model.EmissionDate,
                Meeting = meeting,
                Type = DocumentType.Meeting
            };
        }



        public DocumentViewModel ToDocumentViewModelFromMeetingDoc(MeetingDocument document)
        {
            return new DocumentViewModel
            {
                Id = document.Id,
                Subject = document.Subject,
                Description = document.Description,
                EmissionDate = document.EmissionDate,
                Type = document.Type,
                MeetingId = document.Meeting.Id,
            };

        }

        public DocumentViewModel ToDocumentViewModelFromInterventionDoc(InterventionDocument document)
        {
            return new DocumentViewModel
            {
                Id = document.Id,
                Subject = document.Subject,
                Description = document.Description,
                EmissionDate = document.EmissionDate,
                Type = document.Type,
                InterventionId = document.Intervention.Id,
            };
        }


        public Incident ToIncident(IncidentViewModel model, bool isNew, Apartment? apartment, Condo condo)
        {
            return new Incident
            {
                Id = isNew ? 0 : model.Id,
                NotifierId = model.NotifierId,
                Title = model.Title,
                Description = model.Description,
                Date = model.Date,
                IsResolved = model.IsResolved,
                Apartment = apartment,
                Condo = condo
            };
        }

        public IncidentViewModel ToIncidentViewModel(Incident incident)
        {
            if (incident.Apartment != null)
            {
                return new IncidentViewModel
                {
                    Id = incident.Id,
                    NotifierId = incident.NotifierId,
                    Title = incident.Title,
                    Description = incident.Description,
                    Date = incident.Date,
                    IsResolved = incident.IsResolved,
                    ApartmentFlat = incident.Apartment.Flat,
                    CondoId = incident.Condo.Id,
                    ApartmentId = incident.Apartment.Id,
                    CondoAddress = incident.Condo.Address,
                };
            }

            return new IncidentViewModel
            {
                Id = incident.Id,
                NotifierId = incident.NotifierId,
                Title = incident.Title,
                Description = incident.Description,
                Date = incident.Date,
                IsResolved = incident.IsResolved,
                CondoId = incident.Condo.Id,
                CondoAddress = incident.Condo.Address,
            };

        }

        public Intervention ToIntervention(InterventionViewModel model, bool isNew, Incident incident)
        {
            return new Intervention
            {
                Id = isNew ? 0 : model.Id,
                Title = model.Title,
                Description = model.Description,
                Date = model.Date,
                CompanyName = model.CompanyName,
                Incident = incident,
                IsCompleted = model.IsCompleted,
            };
        }

        public InterventionViewModel ToInterventionViewModel(Intervention intervention)
        {
            return new InterventionViewModel
            {
                Id = intervention.Id,
                Title = intervention.Title,
                Description = intervention.Description,
                Date = intervention.Date,
                CompanyName = intervention.CompanyName,
                IncidentId = intervention.Incident.Id,
                IsCompleted = intervention.IsCompleted,
            };
        }

        public Meeting ToMeeting(MeetingViewModel model, bool isNew, Condo condo)
        {
            return new Meeting
            {
                Id = isNew ? 0 : model.Id,
                Topic = model.Topic,
                Type = model.Type,
                Date = model.Date,
                Condo = condo,
            };
        }

        public MeetingViewModel ToMeetingViewModel(Meeting meeting)
        {
            return new MeetingViewModel
            {
                Id = meeting.Id,
                Topic = meeting.Topic,
                Type = meeting.Type,
                Date = meeting.Date,
                CondoId = meeting.Condo.Id
            };
        }

        public Vote ToVote(VoteViewModel model, bool isNew, Meeting meeting)
        {
            return new Vote
            {
                Id = isNew ? 0 : model.Id,
                Description = model.Description,
                VotesAbstained = model.VotesAbstained,
                VotesAgainst = model.VotesAgainst,
                VotesInFavour = model.VotesInFavour,
                IsApproved = model.IsApproved,
                Meeting = meeting
            };
        }

        public VoteViewModel ToVoteViewModel(Vote vote)
        {
            return new VoteViewModel
            {
                Id = vote.Id,
                MeetingId = vote.Meeting.Id,
                Description = vote.Description,
                VotesAbstained = vote.VotesAbstained,
                VotesAgainst = vote.VotesAgainst,
                VotesInFavour = vote.VotesInFavour,
                IsApproved = vote.IsApproved,
            };
        }
    }
}
