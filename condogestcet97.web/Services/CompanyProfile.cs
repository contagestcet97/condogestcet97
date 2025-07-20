using AutoMapper;
using condogestcet97.web.Data.Entities.Users;
using condogestcet97.web.Data.ViewModels.CompanyViewModels;

namespace condogestcet97.web.Services
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            //Automapper needs to be configured for bi-directional mapping or else it will only map in one direction.

            //// mapping between CompanyListViewModel and Company
            //CreateMap<CompanyListViewModel, Company>();

            //// mapping between Company and CompanyListViewModel
            //CreateMap<Company, CompanyListViewModel>();

            //mapping in both directions
            CreateMap<Company, CompanyListViewModel>().ReverseMap();
        }
    }
}
