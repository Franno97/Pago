using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mre.Visas.Pago.Domain.Entities
{
  public class PagoDetalle : AuditableEntity
  {

    /// <summary>
    /// Id del trámite
    /// </summary>
    public Guid IdTramite { get; set; }
    
    /// <summary>
    /// Id del Pago
    /// </summary>
    public Guid IdPago { get; set; }
    /// <summary>
    /// Orden de generación del pago dentro del detalle
    /// </summary>
    public int Orden { get; set; }

    /// <summary>
    /// Descripción del pago, es el nombre que consta en la configuración de pago a ser realizados en el servicio
    /// </summary>
    public string Descripcion { get; set; }

    /// <summary>
    /// Valor a pagar
    /// </summary>
    public decimal ValorArancel { get; set; }

    /// <summary>
    /// Porcentaje Descuento
    /// </summary>
    public decimal PorcentajeDescuento { get; set; }

    /// <summary>
    /// Valor Descuento
    /// </summary>
    public decimal ValorDescuento { get; set; }

    /// <summary>
    /// Valor a Pagar
    /// </summary>
    public decimal ValorTotal { get; set; }

    /// <summary>
    /// Estado del registro
    /// </summary>
    public string Estado { get; set; }

    /// <summary>
    /// Orden de Pago
    /// </summary>
    public string OrdenPago { get; set; }

    /// <summary>
    /// Número de transacción bancaria
    /// </summary>
    public string NumeroTransaccion { get; set; }

    /// <summary>
    /// Bandera quie indica si el pago está facturado
    /// </summary>
    public bool EstaFacturado { get; set; }

    /// <summary>
    /// Clave de Acceso de la factura electrónica
    /// </summary>
    public string ClaveAcceso { get; set; }

    /// <summary>
    /// String Base 64 del comprobante de pago
    /// </summary>
    public string ComprobantePago { get; set; }

    /// <summary>
    /// PartidaArancelariaId 
    /// </summary>
    public Guid PartidaArancelariaId { get; set; }

    /// <summary>
    /// ServicioId que se factura, configurado en los servicios que se facturan
    /// </summary>
    public Guid ServicioId { get; set; }

    /// <summary>
    /// Servicio
    /// </summary>
    public string Servicio { get; set; }

    /// <summary>
    /// PartidaArancelaria 
    /// </summary>
    public string PartidaArancelaria { get; set; }

    /// <summary>
    /// NumeroPartida 
    /// </summary>
    public string NumeroPartida { get; set; }

    /// <summary>
    /// JerarquiaArancelariaId 
    /// </summary>
    public Guid JerarquiaArancelariaId { get; set; }

    /// <summary>
    /// JerarquiaArancelaria
    /// </summary>
    public string JerarquiaArancelaria { get; set; }

    /// <summary>
    /// ArancelId
    /// </summary>
    public Guid ArancelId { get; set; }

    public string Arancel { get; set; }

    /// <summary>
    /// Arancel
    /// </summary>
    public Guid ConvenioId { get; set; }

    /// <summary>
    /// TipoServicio
    /// </summary>
    public string TipoServicio { get; set; }

    /// <summary>
    /// TipoExoneracionId
    /// </summary>
    public string TipoExoneracionId { get; set; }

    /// <summary>
    /// TipoExoneracion
    /// </summary>
    public string TipoExoneracion { get; set; }

    /// <summary>
    /// En qué paso del flujo se va a realizar la factura
    /// </summary>
    public string FacturarEn { get; set; }

    /// <summary>
    /// Fecha de pago del comprobante
    /// </summary>
    public DateTime FechaPago { get; set; }

    #region Constructores

    public PagoDetalle()
    {

    }

    public PagoDetalle(Guid id,
        Guid idTramite, Guid idPago, int orden, string descripcion, Guid idArancel,
                        decimal valorArancel, decimal porcentajeDescuento,
                        decimal valorDescuento, decimal valorTotal, string ordenPago, string estado, Guid idUsuario, string comprobantePago, string numeroTransaccion, bool estaFacturado, string claveAcceso,
                        Guid partidaArancelariaId, Guid servicioId, string servicio, string partidaArancelaria, string numeroPartida, Guid jerarquiaArancelariaId, string jerarquiaArancelaria,
                        string arancel, Guid convenioId, string tipoServicio, string tipoExoneracionId, string tipoExoneracion, string facturarEn, DateTime fechaPago)
    {
      if (id == Guid.Empty)
        AssignId();
      else Id = id;

      IdTramite = idTramite;
      IdPago = idPago;
      Orden = orden;
      Descripcion = descripcion;
      ArancelId = idArancel;
      ValorArancel = valorArancel;
      PorcentajeDescuento = porcentajeDescuento;
      ValorDescuento = valorDescuento;
      ValorTotal = valorTotal;
      OrdenPago = ordenPago;
      Estado = estado;
      Created = DateTime.Now;
      LastModified = DateTime.Now;
      LastModifierId = idUsuario;
      CreatorId = idUsuario;
      IsDeleted = false;
      ComprobantePago = comprobantePago;
      NumeroTransaccion = numeroTransaccion;
      EstaFacturado = estaFacturado;
      ClaveAcceso = claveAcceso;
      PartidaArancelariaId = partidaArancelariaId;
      ServicioId = servicioId;
      Servicio = servicio;
      PartidaArancelaria = partidaArancelaria;
      NumeroPartida = numeroPartida;
      JerarquiaArancelariaId = jerarquiaArancelariaId;
      JerarquiaArancelaria = jerarquiaArancelaria;
      Arancel = arancel;
      ConvenioId = convenioId;
      TipoServicio = tipoServicio;
      TipoExoneracionId = tipoExoneracionId;
      TipoExoneracion = tipoExoneracion;
      FacturarEn = facturarEn;
      FechaPago = fechaPago;
    }

    #endregion
  }
}
