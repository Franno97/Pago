using Mre.Visas.Pago.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mre.Visas.Pago.Application.ConfiguracionPago.Repositories
{
  public interface IConfiguracionPagoRepository : IRepository<Domain.Entities.ConfiguracionPago>
  {
    Task<List<Domain.Entities.ConfiguracionPago>> GetByServicioIdAsync(Guid servicioId);
  }

}