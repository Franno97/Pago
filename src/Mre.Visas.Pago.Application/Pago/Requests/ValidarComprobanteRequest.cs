using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mre.Visas.Pago.Application.Pago.Requests
{
  public class ValidarComprobante
  {
    public Guid IdPagoDetalle { get; set; }
    public string NumeroTransaccion { get; set; }

    public ValidarComprobante(Guid idPagoDetalle, string numeroTransaccion)
    {
      IdPagoDetalle = idPagoDetalle;
      NumeroTransaccion = numeroTransaccion;
    }

  }

  public class ValidarComprobanteRequest
  {
    public List<ValidarComprobante> ListaComprobante { get; set; }

    public ValidarComprobanteRequest(List<ValidarComprobante> listaComprobante)
    {
      ListaComprobante = listaComprobante;
    }
  }

}
