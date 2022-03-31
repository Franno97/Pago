using Mre.Visas.Pago.Application.Pago.Repositories;
using Mre.Visas.Pago.Infrastructure.Persistence.Contexts;
using Mre.Visas.Pago.Infrastructure.Shared.Repositories;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using AutoMapper;
using System;

namespace Mre.Visas.Pago.Infrastructure.Pago.Repositories
{
  public class PagoRepository : Repository<Domain.Entities.Pago>, IPagoRepository
  {
    #region Constructors

    public PagoRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    #endregion Constructors

    public async Task<Domain.Entities.Pago> GetByIdTramite(string id)
    {
      return await _context.Pagos.FirstOrDefaultAsync(x => x.IdTramite.ToString() == id);
    }


    public async Task<Domain.Entities.Pago> ObtenerPagoPorIdAsync(Guid id)
    {

      return await _context.Pagos.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Application.Pago.Responses.ObtenerPagoResponse> ObtenerPagoIdTramiteAsync(Guid idTramite, bool valoresMayoresaCero, string facturarEn)
    {
      try
      {
        var config = new MapperConfiguration(cfg =>
               cfg.CreateMap<Domain.Entities.Pago, Application.Pago.Responses.ObtenerPagoResponse>()
           );
        var mapper = new Mapper(config);

        var cabecera = await _context.Pagos.OrderByDescending(z => z.LastModified).FirstOrDefaultAsync(x => x.IdTramite == idTramite); //Trae el último proceso de pago del trámite
        if (cabecera == null)
        {
          throw new Exception("Error al obtener la cabecera del pago.");
        }
        List<Domain.Entities.PagoDetalle> detalle;

        detalle = await _context.PagoDetalles.Where(x => x.IdPago == cabecera.Id
        && (!valoresMayoresaCero || x.ValorTotal > 0)
        && (facturarEn == "0" || x.FacturarEn == facturarEn)).ToListAsync();

        var pago = mapper.Map<Application.Pago.Responses.ObtenerPagoResponse>(cabecera);
        pago.ListaPagoDetalle = new List<Domain.Entities.PagoDetalle>();

        foreach (var item in detalle)
        {
          pago.ListaPagoDetalle.Add(item);
        }

        return pago;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }

    }
  }
}