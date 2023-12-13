using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Students.DataProvider.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDatabaseProvider(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddScoped<ISqlConnectionFactory>(_ => new SqlConnectionFactory(configuration));
            serviceCollection.AddScoped(sp => sp.GetService<ISqlConnectionFactory>()!.CreateConnection());
            serviceCollection.AddScoped<IStudentsRepository, StudentsRepository>();
        }
    }
}
