using Mre.Visas.Pago.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mre.Visas.Pago.Application.Pago.Repositories
{
  public interface IPagoRepository : IRepository<Domain.Entities.Pago>
  {
    Task<Domain.Entities.Pago> GetByIdTramite(string id);

    Task<Domain.Entities.Pago> ObtenerPagoPorIdAsync(Guid id);
    Task<Responses.ObtenerPagoResponse> ObtenerPagoIdTramiteAsync(Guid id, bool valoresMayoresaCero, string facturarEn);
  }

  public interface IPagoDetalleRepository : IRepository<Domain.Entities.PagoDetalle>
  {
    Task<List<Domain.Entities.PagoDetalle>> GetByIdTramite(string id);

    Task<Domain.Entities.PagoDetalle> ObtenerPagoDeallePorId(Guid id);

    Task<Domain.Entities.PagoDetalle> ObtenerPagoDetallePorTransaccionEnOtroMovimiento(string numeroTransaccion, Guid idPagoDetalle);

    Task<List<Domain.Entities.PagoDetalle>> GetByPagoIdAsync(Guid pagoId);

    //Task<Domain.Entities.PagoDetalle> GetById(string id);

    //Task<List<Domain.Entities.PagoDetalle>> ObtenerPagoDetalleIdTramiteAsync(Guid idTramite);
  }

}