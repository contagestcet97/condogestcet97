using AutoMapper;
using condogestcet97.web.Data.Entities.Users;
using condogestcet97.web.Data.ViewModels.RoleViewModels;

namespace condogestcet97.web.Services
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            //maps the Role entity to the listing view model
            CreateMap<Role, RoleListViewModel>();

            //maps the Role entity to the details view model
            CreateMap<Role, RoleDetailsViewModel>();

            //maps the Role entity to the create view model
            CreateMap<RoleCreateViewModel, Role>();

            //maps the Role entity to the edit view model and vice versa
            CreateMap<Role, RoleEditViewModel>().ReverseMap();

            //maps the Role entity to the delete view model
            CreateMap<Role, RoleDeleteViewModel>();
        }
    }
}
