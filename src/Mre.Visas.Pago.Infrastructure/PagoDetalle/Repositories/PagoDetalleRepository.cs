using Microsoft.EntityFrameworkCore;
using Mre.Visas.Pago.Application.Pago.Repositories;
using Mre.Visas.Pago.Infrastructure.Persistence.Contexts;
using Mre.Visas.Pago.Infrastructure.Shared.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mre.Visas.Pago.Infrastructure.PagoDetalle.Repositories
{
  public class PagoDetalleRepository : Repository<Domain.Entities.PagoDetalle>, IPagoDetalleRepository
  {
    #region Constructors

    public PagoDetalleRepository(ApplicationDbContext context)
        : base(context)
    {
    }
    #endregion Constructors

    public async Task<List<Domain.Entities.PagoDetalle>> GetByIdTramite(string id)
    {
      return await _context.PagoDetalles.Where(x => x.IdTramite.ToString() == id).ToListAsync();
    }

    public async Task<Domain.Entities.PagoDetalle> ObtenerPagoDeallePorId(Guid id)
    {
      return await _context.PagoDetalles.FirstOrDefaultAsync(x => x.Id == id);
    }

    /// <summary>
    /// Método para obtener un registro del detalle de pago verificando la transacción y si fue utilizada en otro movimiento.
    /// </summary>
    /// <param name="numeroTransaccion"></param>
    /// <param name="idPagoDetalle"></param>
    /// <returns></returns>
    public async Task<Domain.Entities.PagoDetalle> ObtenerPagoDetallePorTransaccionEnOtroMovimiento(string numeroTransaccion, Guid idPagoDetalle)
    {
      return await _context.PagoDetalles.FirstOrDefaultAsync(x => x.NumeroTransaccion == numeroTransaccion && x.IsDeleted == false && x.Id != idPagoDetalle);
    }

    public async Task<List<Domain.Entities.PagoDetalle>> GetByPagoIdAsync(Guid pagoId)
    {
      return await _context.PagoDetalles.Where(x => x.IdPago == pagoId && x.IsDeleted == false).ToListAsync();
    }
  }
}
