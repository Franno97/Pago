using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Mre.Visas.Pago.Api.Controllers;
using Mre.Visas.Pago.Application.Pago.Commands;
using Mre.Visas.Pago.Application.Pago.Queries;
using Mre.Visas.Pago.Application.Pago.Requests;
using System.Threading.Tasks;
using Mre.Visas.Pago.Application.Pago.Responses;
using System;
using System.Collections.Generic;
using Mre.Visas.Pago.Domain.Entities;
using Mre.Visas.Pago.Application.ConfiguracionPago.Requests;
using Mre.Visas.Pago.Application.ConfiguracionPago.Queries;

namespace Mre.Visas.Api.Controllers
{
  public class PagoController : BaseController
  {
    private IConfiguration configuration;
    public const string BearerPrefix = "Bearer ";
    public const string CabeceraAutentificacion = "Authorization";
    public string TokenAcceso = string.Empty;

    public PagoController(IConfiguration iconfiguration)
    {
      configuration = iconfiguration;
    }


    [HttpPost("CalcularPago")]
    [ActionName(nameof(CalcularPagoAsync))]
    public async Task<IActionResult> CalcularPagoAsync(CalcularPagoRequest request)
    {
      string urlUnidadAdministrativa = configuration.GetSection("RemoteServices").GetSection("UnidadAdministrativa").GetSection("BaseUrl").Value;
      var re = HttpContext.Request;
      var headers = re.Headers;
      string autentificacionCabecera = headers[CabeceraAutentificacion];

      if (autentificacionCabecera != null && autentificacionCabecera.StartsWith(BearerPrefix) && !string.IsNullOrEmpty(autentificacionCabecera))
      {
        TokenAcceso = autentificacionCabecera.Substring(BearerPrefix.Length);
      }
      else
      {
        TokenAcceso = configuration.GetSection("RemoteServices").GetSection("Token").Value;
      }

      return Ok(await Mediator.Send(new CalcularPagoCommand(request, TokenAcceso, urlUnidadAdministrativa)).ConfigureAwait(false));
    }


    [HttpPost("ObtenerPago")]
    [ActionName(nameof(ObtenerPagoAsync))]
    public async Task<ActionResult<ObtenerPagoResponse>> ObtenerPagoAsync(string idTramite, bool valoresMayoraCero, string facturarEn = "0")//[FromHeader] string paginacionDTO)
    {
      ObtenerPagoRequest p = new ObtenerPagoRequest(idTramite, valoresMayoraCero, facturarEn);
      return Ok(await Mediator.Send(new ObtenerPagoQuery(p)));

    }

    [HttpPost("GuardarFormaPago")]
    [ActionName(nameof(GuardarFormaPagoAsync))]
    public async Task<IActionResult> GuardarFormaPagoAsync(GuardarFormaPagoRequest request)
    {
      return Ok(await Mediator.Send(new GuardarFormaPagoCommand(request)).ConfigureAwait(false));
    }


    [HttpPost("RegistrarPago")]
    [ActionName(nameof(RegistrarPagoAsync))]
    public async Task<IActionResult> RegistrarPagoAsync(RegistrarPagoRequest request)
    {
      return Ok(await Mediator.Send(new RegistrarPagoCommand(request)).ConfigureAwait(false));
    }

    [HttpPost("ActualizarPago")]
    [ActionName(nameof(ActualizarPagoAsync))]
    public async Task<IActionResult> ActualizarPagoAsync(ActualizarPagoRequest request)
    {
      return Ok(await Mediator.Send(new ActualizarPagoCommand(request)).ConfigureAwait(false));
    }

    [HttpPost("ValidarComprobante")]
    [ActionName(nameof(ValidarComprobanteAsync))]
    public async Task<IActionResult> ValidarComprobanteAsync(List<ValidarComprobante> request)
    {
      return Ok(await Mediator.Send(new ValidarComprobanteQuery(request)).ConfigureAwait(false));
    }


    [HttpGet("ObtenerConfiguracionPago")]
    [ActionName(nameof(ObtenerConfiguracionPagoAsync))]
    public async Task<IActionResult> ObtenerConfiguracionPagoAsync(Guid servicioId)
    {
      ObtenerConfiguracionPagoRequest request = new(servicioId);
      return Ok(await Mediator.Send(new ObtenerConfiguracionPagoQuery(request)));
    }

    [HttpGet]
    public IActionResult Test()
    {
      return Ok(configuration.GetConnectionString("ApplicationDbContext"));
    }

  }
}