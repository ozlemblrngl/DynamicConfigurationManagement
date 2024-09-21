using AutoMapper;
using Business.Dtos;
using Entities;


namespace Business.Mapping
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
