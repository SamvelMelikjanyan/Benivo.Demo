using AutoMapper;
using Benivo.Demo.Entities.Entities;
using Benivo.Demo.Models.Inputs;

namespace Benivo.Demo.Mapper.Profiles
{
    internal class UserJobAnnouncementProfile : Profile
    {
        public UserJobAnnouncementProfile()
        {
            CreateMap<ChangeBookmarkStatusInput, UserJobAnnouncement>()
                .ForMember(dest => dest.JobAnnouncementId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
