//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Core.DAL;
using System.Reflection;
using Common.Interfaces;
using Common.Interfaces.MapperServices;
using Common.DependencyInjectionHelpers;

namespace Core.Modules.PatientModule
{
    public static class DI
    {
        public static void AddApplicationServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var connectionString = configuration.GetConnectionString("connectionString");


            RegisterDatabaseContext(services, connectionString);
            ResgistersIMapperServices(services, assembly);
            ResgistersHangfireServices(services, connectionString);
            Registers(services, assembly, typeof(IBaseRepository));
            Registers(services, assembly, typeof(IBaseMapperService));
            Registers(services, assembly, typeof(IBaseService));
            Registers(services, assembly, typeof(IBaseValidationInputService));
            Registers(services, assembly, typeof(IJWTHandlerService<>));
            Registers(services, assembly, typeof(IBaseJob));
            Registers(services, assembly, typeof(IBaseModule));




            //RegisterImplementationsOfServiceType(
            //    services, assembly, typeof(IAsyncRepository<>));
        }

        private static void RegisterDatabaseContext(IServiceCollection services, string connectionString)
        {
            //-- add dbcontext
            services.AddDbContext<DatabaseContext>(config =>
            {
                config.UseSqlServer(connectionString, 
                    options => options.MigrationsAssembly("Core"));
                //options =>
                //{
                //    options.EnableRetryOnFailure()
                //    .MigrationsAssembly(migrationAssembly);
                //    //options.ExecutionStrategy(dependencies =>
                //    //{
                //    //    return new SqlServerRetryingExecutionStrategy(dependencies,
                //    //        maxRetryCount: 3,
                //    //        maxRetryDelay: TimeSpan.FromSeconds(5),
                //    //        errorNumbersToAdd: new List<int> { 4060 });
                //    //});
                //});
            }
            //,ServiceLifetime.Transient
            );
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //-- add repositories

        }
        private static void ResgistersIMapperServices(IServiceCollection services, Assembly assembly)
        {

            services.AddAutoMapper(cfg =>
            {
                //cfg.CreateMap<string, DateTime>().ConvertUsing<IDateTimeFormatter>();
                //cfg.CreateMap<DateTime, string>().ConvertUsing<IDateTimeFormatter>();
            }, assembly);
        }
        private static void ResgistersHangfireServices(IServiceCollection services, string connectionString)
        {
            //-Add Hangfier
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            services.AddHangfireServer();


        }
        private static void Registers(IServiceCollection services, Assembly assembly, Type type)
        {
            IEnumerable<string> baseTypes = assembly.DefinedTypes.Where(x => x.IsInterface && x.Namespace == type.Namespace)
                .Select(x => x.Name.Trim().ToLower())
                .ToList();

            var implementationsType = assembly.GetTypes()
            .Where(x => !x.IsInterface && x.GetInterface(type.Name) != null);

            foreach (var implementationType in implementationsType)
            {
                var servicesType = implementationType.GetInterfaces()
                    .Where(r => !baseTypes.Contains(r.Name.Trim().ToLower()));

                foreach (var serviceType in servicesType)
                {

                    var dontAutomaticDependencyInjectionAttribute = implementationType.GetCustomAttribute<IgnoreDI>();
                    if (dontAutomaticDependencyInjectionAttribute != null)
                        continue;

                    var scopeAttribute = implementationType.GetCustomAttribute<ScopeAttribute>();
                    if (scopeAttribute == null)
                        scopeAttribute = new ScopeAttribute();
                    switch (scopeAttribute.ScopeType)
                    {
                        case EScopetype.Transiant:
                            services.AddTransient(serviceType, implementationType);
                            break;
                        case EScopetype.Singleton:
                            services.AddSingleton(serviceType, implementationType);
                            break;
                        case EScopetype.Scope:
                        default:
                            services.AddScoped(serviceType, implementationType);
                            break;
                    }
                }
            }

        }

    }
}
