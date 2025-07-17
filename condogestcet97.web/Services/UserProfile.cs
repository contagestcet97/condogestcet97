using AutoMapper;
using condogestcet97.web.Data.Entities.Users;
using condogestcet97.web.Data.ViewModels.UserViewModels;

namespace condogestcet97.web.Services
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //Automapper needs to be configured for bi-directional mapping or else it will only map in one direction.

            // mapping between UserEditViewModel and User
            CreateMap<UserEditViewModel, User>();

            // mapping between User and UserEditViewModel
            CreateMap<User, UserEditViewModel>();
        }
    }
}
