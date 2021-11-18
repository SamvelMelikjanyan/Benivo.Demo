using AutoMapper;
using Benivo.Demo.Mapper.Profiles;
using System;

namespace Benivo.Demo.Mapper.Infrastructure
{
    internal static class MappingConfiguration
    {
        public static readonly Action<IMapperConfigurationExpression> Configure = (cfg) =>
        {
            cfg.AddProfile(new UserProfile());
            cfg.AddProfile(new UserJobAnnouncementProfile());
        };
    }
}
