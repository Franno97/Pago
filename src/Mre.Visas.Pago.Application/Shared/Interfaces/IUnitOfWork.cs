using Mre.Visas.Pago.Application.ConfiguracionPago.Repositories;
using Mre.Visas.Pago.Application.Pago.Repositories;
using System.Threading.Tasks;

namespace Mre.Visas.Pago.Application.Shared.Interfaces
{
  public interface IUnitOfWork
  {
    IPagoRepository PagoRepository { get; }

    IPagoDetalleRepository PagoDetalleRepository { get; }

    IConfiguracionPagoRepository ConfiguracionPagoRepository { get; }

    Task<(bool, string)> SaveChangesAsync();
  }
}