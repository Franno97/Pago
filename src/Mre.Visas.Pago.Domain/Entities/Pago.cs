using Mre.Visas.Pago.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Mre.Visas.Pago.Domain.Entities
{
  public class Pago : AuditableEntity
  {
    #region Constructors

    public Pago()
    {

    }

    #endregion Constructors

    #region Properties

    /// <summary>
    /// Id del trámite
    /// </summary>
    public Guid IdTramite { get; set; }

    /// <summary>
    /// Id del Servicio del trámite
    /// </summary>
    public Guid ServicioId { get; set; }

    /// <summary>
    /// Forma de Pago
    /// </summary>
    public int FormaPago { get; set; }

    /// <summary>
    /// Observacion
    /// </summary>
    public string Observacion { get; set; }

    /// <summary>
    /// Estado
    /// </summary>
    public string Estado { get; set; }

    /// <summary>
    /// Solicitante
    /// </summary>
    public string Solicitante { get; set; }

    /// <summary>
    /// DocumentoIdentificacion
    /// </summary>
    public string DocumentoIdentificacion { get; set; }
    
    /// <summary>
    /// Banco
    /// </summary>
    public string Banco { get; set; }
    
    /// <summary>
    /// Numeor de cuenta
    /// </summary>
    public string NumeroCuenta { get; set; }
    
    /// <summary>
    /// Tipo de cuenta
    /// </summary>
    public string TipoCuenta { get; set; }
    
    /// <summary>
    /// Titular de la cuenta
    /// </summary>
    public string TitularCuenta { get; set; }

    #endregion Properties
  }

}