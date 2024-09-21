using AutoMapper;
using DynamicConfiguration.Dtos;
using DynamicConfiguration.Models;

namespace DynamicConfiguration.Mapping
{
	public class ConfigurationMapping : Profile
	{
		public ConfigurationMapping()
		{
			CreateMap<Configuration, ConfigurationListDto>().ReverseMap();
			CreateMap<Configuration, CreateConfigurationDto>().ReverseMap();
			CreateMap<Configuration, UpdateConfigurationDto>().ReverseMap();
			CreateMap<Configuration, ConfigurationDetailDto>().ReverseMap();
		}
	}
}
