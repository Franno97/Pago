using AutoMapper;
using Mre.Visas.Pago.Application.Responses;
using Mre.Visas.Pago.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mre.Visas.Pago.Application.Shared.Interfaces
{
  public class IPagoDto : BaseResponse
  {
    #region Propiedades
    public Guid IdTramite { get; set; }

    public int FormaPago { get; set; }

    public string Solicitante { get; set; }

    public string DocumentoIdentificacion { get; set; }

    public Guid ServicioId { get; set; }

    public string Observacion { get; set; }

    public string Estado { get; set; }
    public string Banco { get; set; }
    public string NumeroCuenta { get; set; }
    public string TipoCuenta { get; set; }
    public string TitularCuenta { get; set; }

    public List<PagoDetalle> ListaPagoDetalle { get; set; }

    #endregion
  }
}
