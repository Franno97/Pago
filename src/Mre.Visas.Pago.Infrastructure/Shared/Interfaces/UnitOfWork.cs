using Mre.Visas.Pago.Application.ConfiguracionPago.Repositories;
using Mre.Visas.Pago.Application.Pago.Repositories;
using Mre.Visas.Pago.Application.Shared.Interfaces;
using Mre.Visas.Pago.Infrastructure.Persistence.Contexts;
using System;
using System.Threading.Tasks;

namespace Mre.Visas.Pago.Infrastructure.Shared.Interfaces
{
  public class UnitOfWork : IUnitOfWork
  {
    #region Constructors

    public UnitOfWork(
        ApplicationDbContext context,
        IPagoRepository pagoRepository, IPagoDetalleRepository pagoDetalleRepository,
        IConfiguracionPagoRepository configuracionPagoRepository)
    {
      _context = context;
      PagoRepository = pagoRepository;
      PagoDetalleRepository = pagoDetalleRepository;
      ConfiguracionPagoRepository = configuracionPagoRepository;
    }

    #endregion Constructors

    #region Attributes

    protected readonly ApplicationDbContext _context;

    #endregion Attributes

    #region Properties

    public IPagoRepository PagoRepository { get; }
    public IPagoDetalleRepository PagoDetalleRepository { get; }
    public IConfiguracionPagoRepository ConfiguracionPagoRepository { get; }

    #endregion Properties

    #region Methods

    public async Task<(bool, string)> SaveChangesAsync()
    {
      try
      {
        await _context.SaveChangesAsync().ConfigureAwait(false);

        return (true, null);
      }
      catch (Exception ex)
      {
        return (false, ex.InnerException is null ? ex.Message : ex.InnerException.Message);
      }
    }

    #endregion Methods
  }
}