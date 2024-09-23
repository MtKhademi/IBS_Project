using Core.DAL;
using Core.UserManagement.Abstractions.Services;
using Core.UserManagement.Models;
using MDF.Test.Fixtures;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.MsSql;

namespace MDF.Test.SUTs.APIs.V2.Common
{
    public partial class WebAppFactory : WebApplicationFactory<Api.Program>,
        IAsyncLifetime
    {
        private MsSqlContainer _dbContainer;
        public UserManagementModel User = new UserManagementModel
        {
            UserName = "Test-User"
        };
        public WebAppFactory()
        {
            _dbContainer = new MsSqlBuilder()
                .Build();

        }
        public Mock<IJWTUserHandlerService> IjwtUserHandlerServiceMoq
        {
            get
            {
                var IjwtUserMock = new Mock<IJWTUserHandlerService>();
                IjwtUserMock.Setup(x => x.GetModelFromToken(It.IsAny<string>())).Returns(User);

                return IjwtUserMock;
            }
        }

        internal DatabaseRepositoryFixture Repositories
        {
            get
            {
                var scope = Services.CreateScope();
                return
                    new DatabaseRepositoryFixture(scope.ServiceProvider.GetService<DatabaseContext>());
            }
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var connnectionString = _dbContainer.GetConnectionString()
                .Replace("master", "test_db");

            base.ConfigureWebHost(builder);
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll<DbContextOptions<DatabaseContext>>();
                services.AddDbContext<DatabaseContext>(options =>
                {
                    options.UseSqlServer(connnectionString,
                        options =>
                        {
                            //options.AddWindowFunctionsSupport();
                        });
                });

                //services.RemoveAll<IJWTUserHandlerService>();
                //services.AddScoped(_ => IjwtUserHandlerServiceMoq.Object);
            });
        }
        public async Task DisposeAsync()
        {
            await _dbContainer.StopAsync();
        }
        public async Task InitializeAsync()
        {

            await _dbContainer.StartAsync();

            var connnectionString = _dbContainer.GetConnectionString();

            using (var scope = Services.CreateScope())
            {
                var scopeProvider = scope.ServiceProvider;
                var _context = scopeProvider.GetRequiredService<DatabaseContext>();

                await _context.Database.EnsureCreatedAsync();
                var repository = new DatabaseRepositoryFixture(_context);


                await RegisterApiTestRequierAsync(repository);
                await LoginApiTestRequierAsync(repository);
                await QuestionGetsApiTestRequierAsync(repository);
                await QuestionAnswerSetApiTestRequierAsync(repository);
                await SymptomAddOrUpdateApiTestRequierAsync(repository);

            }

        }
    }

    [CollectionDefinition("Collection tests V1")]
    public class DatabaseCollection : ICollectionFixture<WebAppFactory>
    {
    }

}


