using AutoMapper;
using System;
using Mre.Visas.Pago.Application.Requests;
using Mre.Visas.Pago.Application.Shared.Interfaces;
using System.Collections.Generic;

namespace Mre.Visas.Pago.Application.Pago.Requests
{
  public class CalcularPagoRequest : BaseByIdRequest
  {
    public Guid ServicioId { get; set; }
    public string Solicitante { get; set; }
    public string DocumentoIdentificacion { get; set; }
    public bool TieneVisa { get; set; }
    public int Edad { get; set; }
    public decimal PorcentajeDiscapacidad { get; set; }
    public string IdUsuario { get; set; }
    public string Banco { get; set; }
    public string NumeroCuenta { get; set; }
    public string TipoCuenta { get; set; }
    public string TitularCuenta { get; set; }
    public List<Dtos.ArancelDto> ConfiguracionAranceles { get; set; }


    public CalcularPagoRequest(string id, string idUsuario, int edad, decimal porcentajeDiscapacidad, bool tieneVisa, Guid servicioId, string solicitante, string documentoIdentificacion, string banco, string numeroCuenta, string tipoCuenta, string titularCuenta,
      List<Dtos.ArancelDto> configuracionAranceles) : base(id)
    {
      Id = id;
      IdUsuario = idUsuario;
      Edad = edad;
      PorcentajeDiscapacidad = porcentajeDiscapacidad;
      TieneVisa = tieneVisa;
      ServicioId = servicioId;
      Solicitante = solicitante;
      DocumentoIdentificacion = documentoIdentificacion;
      Banco = banco;
      NumeroCuenta = numeroCuenta;
      TipoCuenta = tipoCuenta;
      TitularCuenta = titularCuenta;
      ConfiguracionAranceles = configuracionAranceles;
    }

  }


  public class ObtenerPagoRequest : BaseByIdRequest
  {
    //IdTramite
    public string IdTramite { get; set; }
    public bool ValoresMayoraCero { get; set; }
    public string FacturarEn { get; set; }

    public ObtenerPagoRequest(string id, bool valoresMayoraCero, string facturarEn) : base(id)
    {
      IdTramite = id;
      ValoresMayoraCero = valoresMayoraCero;
      FacturarEn = facturarEn;
    }

  }

  public class GuardarFormaPagoRequest
  {
    public Guid Id { get; set; }

    public int FormaPago { get; set; }

    public Guid IdUsuario { get; set; }

    private string _banco;
    public string Banco { get { return _banco; } set { _banco = value ?? ""; } }

    private string _numeroCuenta;
    public string NumeroCuenta { get { return _numeroCuenta; } set { _numeroCuenta = value ?? ""; } }

    private string _tipoCuenta;
    public string TipoCuenta { get { return _tipoCuenta; } set { _tipoCuenta = value ?? ""; } }

    private string _titularCuenta;
    public string TitularCuenta { get { return _titularCuenta; } set { _titularCuenta = value ?? ""; } }

    public List<GuardarFormaPagoDetalle> ListaDetalle { get; set; }

  }
  public class GuardarFormaPagoDetalle
  {
    public Guid Id { get; set; }
    public decimal ValorTotal { get; set; }
  }

}
