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

            //mapping in both directions
            CreateMap<Company, CompanyEditViewModel>().ReverseMap();
            CreateMap<Company, CompanyCreateViewModel>().ReverseMap();

            // mapping in one direction, only need to create/edit
            CreateMap<Company, CompanyDetailsViewModel>();
            CreateMap<Company, CompanyDeleteViewModel>();
            CreateMap<Company, CompanyListViewModel>();
        }
    }
}
