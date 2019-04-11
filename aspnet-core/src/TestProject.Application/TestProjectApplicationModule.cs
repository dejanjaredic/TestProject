using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TestProject.Authorization;
using TestProject.Dto.DeviceDtos;
using TestProject.Dto.DeviceTypeDtos;
using TestProject.Dto.DeviceTypePropertyDtos;
using TestProject.Models;

namespace TestProject
{
    [DependsOn(
        typeof(TestProjectCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class TestProjectApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<TestProjectAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(TestProjectApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg =>
                {
                    cfg.AddProfiles(thisAssembly);
                    cfg.CreateMap<DeviceTypeProperty, DeviceTypePropertyDto>()
                        .ForMember(dest => dest.NameProperty, source => source.MapFrom(src => src.Name))
                        .ForMember(dest => dest.Required, source => source.MapFrom(src => src.IsRequired))
                        .ForMember(dest => dest.Type, source => source.MapFrom(src => src.Type));
                    cfg.CreateMap<DeviceTypeProperty, DeviceTypePropertyCreateDto>()
                        .ForMember(dest => dest.NameProperty, source => source.MapFrom(src => src.Name))
                        .ForMember(dest => dest.Required, source => source.MapFrom(src => src.IsRequired))
                        .ForMember(dest => dest.Type, source => source.MapFrom(src => src.Type));
                    cfg.CreateMap<DeviceTypePropertyCreateDto, DeviceTypeProperty>()
                        .ForMember(dest => dest.Name, source => source.MapFrom(src => src.NameProperty))
                        .ForMember(dest => dest.IsRequired, source => source.MapFrom(src => src.Required))
                        .ForMember(dest => dest.Type, source => source.MapFrom(src => src.Type));
                    cfg.CreateMap<DeviceType, DeviceTypePropertiesNestedDto>()
                        .ForMember(dest => dest.Name, source => source.MapFrom(src => src.Name))
                        .ForMember(dest => dest.Description, source => source.MapFrom(src => src.Description))
                        .ForMember(dest => dest.ParentId, source => source.MapFrom(src => src.Parent.Id))
                        .ForMember(dest => dest.Properties, source => source.MapFrom(src => src.DeviceTypeProperty));
                    cfg.CreateMap<DeviceCreateDto, Device>()
                        .ForMember(dest => dest.Id, source => source.MapFrom(src => src.DeviceId));

                });
        }
    }
}
