using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Repositories;
using Entities;

namespace DataAccess.Concrete.EntityFramework
{
	public class EfConfigurationDal : GenericRepository<Configuration>, IGenericDal<Configuration>, IConfigurationDal
	{
		public EfConfigurationDal(ConfigurationDbContext context) : base(context)
		{
		}
	}
}
