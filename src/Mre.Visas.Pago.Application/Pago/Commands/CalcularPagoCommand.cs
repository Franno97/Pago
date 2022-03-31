using FluentValidation;
using MediatR;
using Mre.Visas.Pago.Application.Wrappers;
using Mre.Visas.Pago.Application.Pago.Requests;
using Mre.Visas.Pago.Application.Pago.Responses;
using Mre.Visas.Pago.Application.Shared.Handlers;
using Mre.Visas.Pago.Application.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using Mre.Visas.Pago.Application.Externos.Responses;
using Newtonsoft.Json;
using System.Linq;
using Mre.Visas.Pago.Application.Pago.Dtos;

namespace Mre.Visas.Pago.Application.Pago.Commands
{
  public class CalcularPagoCommand : CalcularPagoRequest, IRequest<ApiResponseWrapper>
  {
    public string Token { get; set; }
    public string UrlUnidadAdministrativa { get; set; }

    #region Constructors

    public CalcularPagoCommand(CalcularPagoRequest request, string tokenAcceso, string urlUnidadAdministrativa)
        : base(request.Id, request.IdUsuario, request.Edad, request.PorcentajeDiscapacidad, request.TieneVisa, request.ServicioId, request.Solicitante, request.DocumentoIdentificacion, request.Banco, request.NumeroCuenta, request.TipoCuenta, request.TitularCuenta, request.ConfiguracionAranceles)
    {
      UrlUnidadAdministrativa = urlUnidadAdministrativa;
      Token = tokenAcceso;
    }

    #endregion Constructors

    #region Handlers

    public class CalcularPagoCommandHandler : BaseHandler, IRequestHandler<CalcularPagoCommand, ApiResponseWrapper>
    {
      #region Constructors

      public CalcularPagoCommandHandler(IUnitOfWork unitOfWork)
          : base(unitOfWork)
      {
      }

      #endregion Constructors

      #region Methods


      public async Task<ApiResponseWrapper> Handle(CalcularPagoCommand command, CancellationToken cancellationToken)
      {
        var pago = new Domain.Entities.Pago();
        pago.AssignId();

        /*Variables*/
        #region variables


        Guid idUsuario = new Guid(command.IdUsuario);


        #endregion

        #region Poblar variables
        /*Preparar objetos*/
        pago.IdTramite = new Guid(command.Id);
        pago.ServicioId = command.ServicioId;
        pago.FormaPago = Domain.Enums.FormaPago.OtrosSistemaFinanciero;
        pago.Observacion = "";
        pago.Estado = "OK";
        pago.DocumentoIdentificacion = command.DocumentoIdentificacion;
        pago.Solicitante = command.Solicitante;
        pago.Banco = command.Banco;
        pago.NumeroCuenta = command.NumeroCuenta;
        pago.TipoCuenta = command.TipoCuenta;
        pago.TitularCuenta = command.TitularCuenta;
        pago.Created = DateTime.Now;
        pago.LastModified = DateTime.Now;
        pago.LastModifierId = idUsuario;
        pago.CreatorId = idUsuario;
        pago.IsDeleted = false;

        #region Cálculos

        // Generar pagos
        List<Domain.Entities.PagoDetalle> ListaDetalle = new();
        // Obtenemos configuracion de pagos
        //var listaConfiguracion = await UnitOfWork.ConfiguracionPagoRepository.GetByServicioIdAsync(command.ServicioId);

        //Tuple<string, List<ArancelDto>> lista = await ObtenerValoresAPagarAsync(command.Edad, command.PorcentajeDiscapacidad, command.ServicioId, command.UrlUnidadAdministrativa, command.Token, listaConfiguracion);

        //if (lista.Item1.Length > 0)
        //  throw new Exception(lista.Item1);

        int i = 1;
        foreach (var item in command.ConfiguracionAranceles)
        {
          ListaDetalle.Add(CalcularPago(pago.IdTramite, pago.Id, i, item.ArancelId, item.DescripcionArancelaria, item.ValorArancelario, idUsuario,
            item.PartidaArancelariaId, item.ServicioId, item.Servicio, item.PartidaArancelaria, item.NumeroPartida, item.JerarquiaArancelariaId, item.JerarquiaArancelaria,
            item.Arancel, item.ConvenioId, item.TipoServicio, item.TipoExoneracionId, item.TipoExoneracion, item.FacturarEn));
          i++;
        }

        //if (command.TieneVisa)
        //  ListaDetalle.Add(CalcularPago(pago.IdTramite, pago.Id, 4, idArancelCancelacion, command.CodigoArancelCancelacion, descripcionArancelCancelacion, valorArancelCancelacion, command.PorcentajeDiscapacidad, command.Edad, idUsuario));

        #endregion


        #endregion

        #region Guardar
        try
        {
          var respuestaPago = await UnitOfWork.PagoRepository.InsertAsync(pago).ConfigureAwait(false);
          if (respuestaPago.Item1)
          {
            foreach (var pagoDetalle in ListaDetalle)
            {
              var respuestaPagoDetalle = await UnitOfWork.PagoDetalleRepository.InsertAsync(pagoDetalle).ConfigureAwait(false);
              if (!respuestaPagoDetalle.Item1)
                throw new Exception("Error al guardar la entidad PagoDetalle.");

            }
            var respuestaActualizacion = await UnitOfWork.SaveChangesAsync().ConfigureAwait(false);

            if (!respuestaActualizacion.Item1)
              throw new Exception(respuestaActualizacion.Item2);


          }
          else
          { throw new Exception("Error al guardar la entidad Pago."); }

          var valoresPagosResponse = new CalcularPagoResponse(pago.Id.ToString(), "Proceso Exitoso.");

          var response = new ApiResponseWrapper(HttpStatusCode.OK, valoresPagosResponse);
          return response;
        }
        catch (Exception ex)
        {
          string mensaje = ex.Message ?? ex.InnerException.ToString();
          var result = new CalcularPagoResponse(null, mensaje);

          return new ApiResponseWrapper(HttpStatusCode.BadRequest, result);
        }

        #endregion

      }

      private Domain.Entities.PagoDetalle CalcularPago(Guid idTramite, Guid idPago, int orden, Guid idArancel, string descripcionArancel, decimal valorArancel, Guid idUsuario,
        Guid partidaArancelariaId, Guid servicioId, string servicio, string partidaArancelaria, string numeroPartida, Guid jerarquiaArancelariaId, string jerarquiaArancelaria,
                        string arancel, Guid convenioId, string tipoServicio, string tipoExoneracionId, string tipoExoneracion, string facturarEn)
      {
        /*Cálculos*/

        decimal porcentajeDiscapacidad = 0, edad = 0;

        decimal porcentajeDescuento = 0;
        if (porcentajeDiscapacidad >= 30)
        {
          porcentajeDescuento = 100;
        }
        else
        {
          if (edad >= 65)
          {
            porcentajeDescuento = 50;
          }
        }

        decimal valorDescuento = 0;
        decimal valorTotalArancel = valorArancel - valorDescuento; ;

        Domain.Entities.PagoDetalle pagoDetalle = new Domain.Entities.PagoDetalle(Guid.Empty,
            idTramite, idPago, orden, descripcionArancel, idArancel, valorArancel, porcentajeDescuento, valorDescuento, valorTotalArancel, "0", "OK", idUsuario, string.Empty, string.Empty, false, string.Empty,
            partidaArancelariaId, servicioId, servicio, partidaArancelaria, numeroPartida, jerarquiaArancelariaId, jerarquiaArancelaria,
            arancel, convenioId, tipoServicio, tipoExoneracionId, tipoExoneracion, facturarEn, new DateTime(1900, 1, 1));


        return pagoDetalle;
      }

      private async Task<Tuple<string, List<ArancelDto>>> ObtenerValoresAPagarAsync(int edad, decimal porcentajeDiscapacidad, Guid servicioId, string urlUnidadAdministrativa, string tokenAcceso, List<Domain.Entities.ConfiguracionPago> listaConfiguracion)
      {
        HttpClient Client = new();
        Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenAcceso);
        String Uri = string.Empty;
        string PlacesJson = string.Empty;

        HttpResponseMessage Response;
        List<ArancelDto> aranceles = new List<ArancelDto>();

        // Verificando que existan configuraciones de pagos a realizar
        if (listaConfiguracion.Count == 0)
          return new Tuple<string, List<ArancelDto>>("Error al obtener configuración de aranceles.", null);

        // Generando los aranceles a cobrar
        foreach (var configuracionPago in listaConfiguracion)
        {
          // Obtener datos del arancel
          #region Obtener Arancel
          Uri = $"{urlUnidadAdministrativa}api/unidad-administrativa/partida-arancelaria-servicio/{configuracionPago.ServicioIdPago}";
          Response = await Client.GetAsync(Uri);
          if (Response.StatusCode != HttpStatusCode.OK)
          {
            return new Tuple<string, List<ArancelDto>>($"Error al obtener la configuración de las partidas arancelarias.{Environment.NewLine}" +
              $"{Response.StatusCode}{Environment.NewLine}{Response.RequestMessage}", null);
          }

          PlacesJson = Response.Content.ReadAsStringAsync().Result;
          ListaPartidaArancelaria listaPartidasArancelarias = new ListaPartidaArancelaria();
          listaPartidasArancelarias = JsonConvert.DeserializeObject<ListaPartidaArancelaria>(PlacesJson);

          if (listaPartidasArancelarias.Items.Count == 0)
          {
            return new Tuple<string, List<ArancelDto>>("No existen partidas arancelarias configuradas.", null);
          }

          var arancel = new ArancelDto
          {
            ServicioId = configuracionPago.ServicioIdPago,
            DescripcionArancelaria = configuracionPago.Descripcion,
            PartidaArancelaria = listaPartidasArancelarias.Items[0].PartidaArancelaria,
            ValorArancelario = listaPartidasArancelarias.Items[0].Valor,
            ArancelId = listaPartidasArancelarias.Items[0].ArancelId,
            PartidaArancelariaId = listaPartidasArancelarias.Items[0].PartidaArancelariaId,
            Servicio = listaPartidasArancelarias.Items[0].Servicio,
            NumeroPartida = listaPartidasArancelarias.Items[0].NumeroPartida,
            JerarquiaArancelariaId = listaPartidasArancelarias.Items[0].JerarquiaArancelariaId,
            JerarquiaArancelaria = listaPartidasArancelarias.Items[0].JerarquiaArancelaria,
            Arancel = listaPartidasArancelarias.Items[0].Arancel,
            FacturarEn = configuracionPago.FacturarEn
          };

          #endregion

          #region Obtener Exoneraciones

          Uri = $"{urlUnidadAdministrativa}api/unidad-administrativa/convenio/exoneration/{configuracionPago.ServicioIdPago}";

          Response = await Client.GetAsync(Uri);
          if (Response.StatusCode != HttpStatusCode.OK)
          {
            return new Tuple<string, List<ArancelDto>>("Error al obtener las configuraciones de exoneraciones.", null);
          }

          PlacesJson = Response.Content.ReadAsStringAsync().Result;
          ListaConvenios listaConvenios = new ListaConvenios();
          listaConvenios = JsonConvert.DeserializeObject<ListaConvenios>(PlacesJson);

          if (listaConvenios.Items.Count > 0)
          {
            var convenioTeceraEdad = listaConvenios.Items.FirstOrDefault(x => edad >= x.EdadInicial && !x.Discapacitado.Value && x.EdadInicial > 0);
            if (convenioTeceraEdad != null)
            {
              arancel.ValorArancelario = convenioTeceraEdad.Valor;
              arancel.ConvenioId = convenioTeceraEdad.ConvenioId;
              arancel.TipoServicio = convenioTeceraEdad.TipoServicio;
              arancel.TipoExoneracionId = convenioTeceraEdad.TipoExoneracionId;
              arancel.TipoExoneracion = convenioTeceraEdad.TipoExoneracion;
            }

            if (porcentajeDiscapacidad >= 30)
            {
              var convenioDiscapacidad = listaConvenios.Items.FirstOrDefault(x => x.Discapacitado.Value);
              if (convenioDiscapacidad != null)
              {
                arancel.ValorArancelario = convenioDiscapacidad.Valor;
                arancel.ConvenioId = convenioDiscapacidad.ConvenioId;
                arancel.TipoServicio = convenioDiscapacidad.TipoServicio;
                arancel.TipoExoneracionId = convenioDiscapacidad.TipoExoneracionId;
                arancel.TipoExoneracion = convenioDiscapacidad.TipoExoneracion;

              }
            }

          }

          aranceles.Add(arancel);
          #endregion
        }

        return new Tuple<string, List<ArancelDto>>("", aranceles);
      }
      #endregion Methods
    }

    #endregion Handlers
  }

  public class CalcularPagoCommandValidator : AbstractValidator<CalcularPagoCommand>
  {
    public CalcularPagoCommandValidator()
    {
      RuleFor(e => e.ConfiguracionAranceles)
          .NotEmpty().When(e => e.ConfiguracionAranceles == null).WithMessage("{PropertyName} is required.")
          .NotNull().When(e => e.ConfiguracionAranceles.Count > 0).WithMessage("{PropertyName} must not be null.");
      //    .GreaterThanOrEqualTo(e => e.StartDate).When(e => !string.IsNullOrEmpty(e.RecurrenceId)).WithMessage("{PropertyName} must be greater than or equal to the start date.");
    }
  }

}
