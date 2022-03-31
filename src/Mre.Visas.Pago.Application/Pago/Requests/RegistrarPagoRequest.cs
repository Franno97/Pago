using Mre.Visas.Pago.Application.Requests;
using System;
using System.Collections.Generic;

namespace Mre.Visas.Pago.Application.Pago.Requests
{
  public class RegistrarPagoRequest
  {
    //IdPago
    public Guid Id { get; set; }

    //IdTramite
    public Guid IdTramite { get; set; }

    //IdUsuario
    public Guid IdUsuario { get; set; }

    public List<RegistroPagoDetalleRequest> ListaRegistroPagoDetalle { get; set; }

  }

  public class RegistroPagoDetalleRequest
  {
    public Guid Id { get; set; }
    public string NumeroTransaccion { get; set; }
    public string ComprobantePago { get; set; }
    public DateTime FechaPago { get; set; }
  }

}
