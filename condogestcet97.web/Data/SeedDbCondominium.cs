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
                          NotifierId = "fakeUser",
                          Title = "Burst water pipe",
                          Description = "Water pipe in the corridor on floor 4 burst and flooded. Water entered several flats.",
                          Condo = condo,
                          Date = DateTime.Now,
                          IsResolved = false,
                    };

                    _context.Incidents.Add(incident);
                }

                await _context.SaveChangesAsync();
            }


        }


    }
}
