using System;
using System.Security.Cryptography;
using condogestcet97.web.Data.Entities.Condominium;
using Microsoft.EntityFrameworkCore;

namespace condogestcet97.web.Data
{
    public class SeedDbCondominium
    {
        private readonly DataContextCondominium _context;

        public SeedDbCondominium(DataContextCondominium context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            if (!_context.Condos.Any())
            {
                Condo condo = new Condo
                {
                    Address = "Rua do Olival 100"
                };

                _context.Condos.Add(condo);
                await _context.SaveChangesAsync();
            }

            if (!_context.Apartments.Any())
            {
                var condo = _context.Condos.FirstOrDefault(a => a.Id == 1);

                if (condo != null)
                {
                    Apartment apartment = new Apartment
                    {
                        Flat = "4E",
                        Divisions = "T6",
                        Condo = condo
                    };

                    _context.Apartments.Add(apartment);
                }

                await _context.SaveChangesAsync();
            }

            if (!_context.Incidents.Any())
            {
                var condo = _context.Condos.FirstOrDefault(a => a.Id == 1); 
                
                if (condo != null)
                {
                    Incident incident = new Incident
                    {
                          NotifierId = 3,
                          Title = "Burst water pipe",
                          Description = "Water pipe in the corridor on floor 4 burst and flooded. Water entered several flats.",
                          CondoId = condo.Id,
                          Date = DateTime.Now.Date.AddDays(-10).AddHours(15),
                          IsResolved = false,
                    };

                    _context.Incidents.Add(incident);
                }

                await _context.SaveChangesAsync();
            }

            if (!_context.Interventions.Any())
            {
                var incident = _context.Incidents.FirstOrDefault(i => i.Id == 1);

                if (incident != null)
                {
                    Intervention intervention = new Intervention
                    {
                        Title = "Water cut off.",
                        Description = "Water was completely cut off and a plumber has been scheduled to replace the burst pipe",
                        CompanyName = "UrgentPlumber",
                        Date = DateTime.Now,
                        IsCompleted = true,
                        Incident = incident
                    };

                    _context.Interventions.Add(intervention);
                }

                await _context.SaveChangesAsync();

            }

            if (!_context.Meetings.Any())
            {
                var condo = _context.Condos.FirstOrDefault(i => i.Id == 1);

                if (condo != null)
                {
                    Meeting meeting = new Meeting
                    {
                        Topic = "New lift",
                        Condo = condo,
                        Type = MeetingType.InPerson,
                        Date = DateTime.Now,
                    };

                    _context.Meetings.Add(meeting);
                }

                await _context.SaveChangesAsync();
            }

            if (!_context.Documents.Any())
            {
                var meeting = _context.Meetings.FirstOrDefault(m => m.Id == 1);
                var intervention = _context.Interventions.FirstOrDefault(i => i.Id == 1);   

                if (meeting != null && intervention != null)
                {
                    MeetingDocument meetingDocument1 = new MeetingDocument
                    {
                        Subject = "Attendance List",
                        Description = "Attendance List",
                        EmissionDate = DateTime.Now,
                        Type = DocumentType.Meeting,
                        Meeting = meeting,
                    };

                    _context.Documents.Add(meetingDocument1);

                    MeetingDocument meetingDocument2 = new MeetingDocument
                    {
                        Subject = "Meeting Agenda",
                        Description = "List of discussion topics",
                        EmissionDate = DateTime.Now,
                        Type = DocumentType.Meeting,
                        Meeting = meeting,
                    };

                    _context.Add(meetingDocument2);

                    InterventionDocument interventionDocument1 = new InterventionDocument
                    {
                        Subject = "Intervention Report",
                        Description = "Report made by contractor",
                        EmissionDate = DateTime.Now,
                        Type = DocumentType.Intervention,
                        Intervention = intervention
                    };

                    _context.Documents.Add(interventionDocument1);

                    InterventionDocument interventionDocument2 = new InterventionDocument
                    {
                        Subject = "Residents communication",
                        Description = "Printed document placed on wall in foyer",
                        EmissionDate = DateTime.Now,
                        Type = DocumentType.Intervention,
                        Intervention = intervention
                    };

                    _context.Documents.Add(interventionDocument2);
                }
                await _context.SaveChangesAsync();
            }

            if (!_context.Votes.Any())
            {
                var meeting = _context.Meetings.FirstOrDefault(m => m.Id == 1);

                if (meeting != null)
                {
                    Vote vote = new Vote
                    {
                        Description = "Vote on whether to install a lift in the building.",
                        Meeting = meeting,
                        VotesInFavour = 12,
                        VotesAgainst = 10,
                        VotesAbstained = 1,
                        IsApproved = true,
                    };

                    _context.Votes.Add(vote);   
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
