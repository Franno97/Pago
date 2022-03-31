using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mre.Visas.Pago.Application.Repositories;
using Mre.Visas.Pago.Application.Pago.Repositories;
using Mre.Visas.Pago.Application.Shared.Interfaces;
using Mre.Visas.Pago.Infrastructure.Pago.Repositories;
using Mre.Visas.Pago.Infrastructure.Persistence.Contexts;
using Mre.Visas.Pago.Infrastructure.Shared.Interfaces;
using Mre.Visas.Pago.Infrastructure.Shared.Repositories;
using Mre.Visas.Pago.Infrastructure.PagoDetalle.Repositories;
using Mre.Visas.Pago.Infrastructure.ConfiguracionPago.Repositories;
using Mre.Visas.Pago.Application.ConfiguracionPago.Repositories;

namespace Mre.Visas.Pago.Infrastructure
{
  public static class ServiceRegistrations
  {
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<ApplicationDbContext>(
          options => options.UseSqlServer(configuration.GetConnectionString("ApplicationDbContext"),
          options => options.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

      services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

      services.AddTransient<IUnitOfWork, UnitOfWork>();

      services.AddTransient<IPagoRepository, PagoRepository>();
      services.AddTransient<IPagoDetalleRepository, PagoDetalleRepository>();
      services.AddTransient<IConfiguracionPagoRepository, ConfiguracionPagoRepository>();
    }
  }
}