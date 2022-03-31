using Mre.Visas.Pago.Application.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mre.Visas.Pago.Application.ConfiguracionPago.Requests
{
  public class ObtenerConfiguracionPagoRequest : BaseByIdRequest
  {
    public Guid ServicioId { get; set; }
    public ObtenerConfiguracionPagoRequest(Guid servicioId)
    {
      ServicioId = servicioId;
    }
  }
}
