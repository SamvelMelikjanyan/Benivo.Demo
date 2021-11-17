using AutoMapper;

namespace Benivo.Demo.Mapper.Infrastructure
{
    internal class Mapper
    {
        public static IMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MapperConfiguration(MappingConfiguration.Configure)
                                   .CreateMapper();
                }
                return instance;
            }
        }
        private static IMapper instance;
    }
}
