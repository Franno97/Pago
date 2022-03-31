using Mre.Visas.Pago.Application.ConfiguracionPago.Repositories;
using Mre.Visas.Pago.Infrastructure.Persistence.Contexts;
using Mre.Visas.Pago.Infrastructure.Shared.Repositories;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mre.Visas.Pago.Infrastructure.ConfiguracionPago.Repositories
{
  public class ConfiguracionPagoRepository : Repository<Domain.Entities.ConfiguracionPago>, IConfiguracionPagoRepository
  {
    #region Constructors

    public ConfiguracionPagoRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    #endregion Constructors

    public async Task<List<Domain.Entities.ConfiguracionPago>> GetByServicioIdAsync(Guid servicioId)
    {
      return await _context.ConfiguracionesPagos.Where(x => x.ServicioId == servicioId && !x.IsDeleted).ToListAsync();
    }

  }

}