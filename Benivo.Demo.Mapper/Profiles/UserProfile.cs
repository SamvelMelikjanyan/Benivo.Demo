using AutoMapper;
using Benivo.Demo.Entities.Entities;
using Benivo.Demo.Models.Inputs;
using Benivo.Demo.Models.Outputs;

namespace Benivo.Demo.Mapper.Profiles
{
    internal class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterInput, User>();
        
            CreateMap<User, RegisterOutput>();
        }
    }
}
