﻿using AutoMapper;
using Business.Dtos;
using Business.Services.Abstract;
using DataAccess.Context;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Services.Concrete
{
	public class ConfigurationService : IConfigurationService
	{


		private readonly ConfigurationDbContext _context;
		private readonly IMapper _mapper;

		public ConfigurationService(ConfigurationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<IEnumerable<ConfigurationListDto>> GetConfigurations(string applicationName)
		{
			var configs = await _context.Configurations
				.Where(c => c.ApplicationName == applicationName && c.IsActive)
				.ToListAsync();

			return _mapper.Map<IEnumerable<ConfigurationListDto>>(configs);
		}

		public async Task<ConfigurationDetailDto> GetConfigurationById(int id)
		{
			var config = await _context.Configurations.FindAsync(id);
			return _mapper.Map<ConfigurationDetailDto>(config);
		}

		public async Task<Configuration?> GetConfigurationByName(string name, string applicationName)
		{
			return await _context.Configurations
				.Where(c => c.ApplicationName == applicationName && c.Name == name && c.IsActive)
				.FirstOrDefaultAsync();
		}
		public async Task<ConfigurationDetailDto> AddConfiguration(CreateConfigurationDto createConfigurationDto)
		{
			var config = _mapper.Map<Configuration>(createConfigurationDto);
			_context.Configurations.Add(config);
			await _context.SaveChangesAsync();

			var response = _mapper.Map<ConfigurationDetailDto>(config);
			return response;
		}


		public async Task UpdateConfiguration(UpdateConfigurationDto updateConfigurationDto)
		{
			var config = await _context.Configurations.FindAsync(updateConfigurationDto.Id);

			_mapper.Map(updateConfigurationDto, config);
			await _context.SaveChangesAsync();
		}


		public async Task DeleteConfiguration(int id)
		{
			var config = await _context.Configurations.FindAsync(id);
			config.IsActive = false;
			await _context.SaveChangesAsync();
		}
		public async Task<IEnumerable<ConfigurationListDto>> GetAllConfigurations()
		{
			var configurations = await _context.Configurations
				.Where(c => c.IsActive)
				.ToListAsync();
			return _mapper.Map<IEnumerable<ConfigurationListDto>>(configurations);
		}


	}
}

