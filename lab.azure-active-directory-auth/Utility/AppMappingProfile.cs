using AutoMapper;
using lab.azure_active_directory_auth.Models;
using lab.azure_active_directory_auth.ViewModels;

namespace lab.azure_active_directory_auth.Utility
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Member, MemberViewModel>().ReverseMap();
        }
    }
}
