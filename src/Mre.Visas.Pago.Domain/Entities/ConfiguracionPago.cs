using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mre.Visas.Pago.Domain.Entities
{
  public class ConfiguracionPago : AuditableEntity
  {
    public Guid UnidadAdministrativaId { get; set; }
    public Guid ServicioId { get; set; }
    public Guid ServicioIdPago { get; set; }
    public string Descripcion { get; set; }
    public string FacturarEn { get; set; }
  }
}
