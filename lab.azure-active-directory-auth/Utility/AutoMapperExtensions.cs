using AutoMapper;

namespace lab.azure_active_directory_auth.Utility
{
    public static class AutoMapperExtensions
    {
        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AppMappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
