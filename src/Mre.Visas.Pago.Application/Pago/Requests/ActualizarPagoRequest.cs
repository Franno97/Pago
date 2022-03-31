using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mre.Visas.Pago.Application.Pago.Requests
{
  public class ActualizarPagoRequest
  {
    public Guid IdPagoDetalle { get; set; }
    public string ClaveAcceso { get; set; }
  }
}
