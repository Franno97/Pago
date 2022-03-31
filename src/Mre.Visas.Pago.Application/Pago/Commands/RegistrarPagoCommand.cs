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

namespace Mre.Visas.Pago.Application.Pago.Commands
{
  public class RegistrarPagoCommand : RegistrarPagoRequest, IRequest<ApiResponseWrapper>
  {
    #region Constructors

    public RegistrarPagoCommand(RegistrarPagoRequest request)
    {
      Id = request.Id;
      IdTramite = request.IdTramite;
      IdUsuario = request.IdUsuario;
      ListaRegistroPagoDetalle = new List<RegistroPagoDetalleRequest>();
      request.ListaRegistroPagoDetalle.ForEach(x => ListaRegistroPagoDetalle.Add(x));
    }

    #endregion Constructors

    #region Handlers

    public class RegistrarPagoCommandHandler : BaseHandler, IRequestHandler<RegistrarPagoCommand, ApiResponseWrapper>
    {
      #region Constructors

      public RegistrarPagoCommandHandler(IUnitOfWork unitOfWork)
          : base(unitOfWork)
      {
      }

      #endregion Constructors

      #region Methods


      public async Task<ApiResponseWrapper> Handle(RegistrarPagoCommand command, CancellationToken cancellationToken)
      {
        var pago = await UnitOfWork.PagoRepository.ObtenerPagoPorIdAsync(command.Id).ConfigureAwait(false);

        //Obtener detalle de Pagos
        List<Domain.Entities.PagoDetalle> pagoDetalles = new List<Domain.Entities.PagoDetalle>();

        #region Guardando información
        //pago.IdTramite = pago_.Id;
        //pago.FormaPago = 1;// command.FormaPago;
        //pago.Observacion = pago_.Observacion;
        //pago.Estado = pago_.Estado;
        pago.LastModified = DateTime.Now;
        pago.LastModifierId = command.IdUsuario;
        //pago.CreatorId = pago_.CreatorId;
        //pago.Created = pago_.Created;

        foreach (var item in command.ListaRegistroPagoDetalle)
        {
          //Generamos la orden de pago
          var detalle = await UnitOfWork.PagoDetalleRepository.ObtenerPagoDeallePorId(item.Id).ConfigureAwait(false);
          detalle.NumeroTransaccion = item.NumeroTransaccion;
          detalle.ComprobantePago = item.ComprobantePago;
          detalle.FechaPago = item.FechaPago;
          pagoDetalles.Add(detalle);
        }


        #region Guardar
        try
        {
          // Armamos la respuesta
          var valoresPagosResponse = new RegistrarPagoResponse(pago.Id.ToString());

          // Actualizamos cabecera
          var respuestaPago = UnitOfWork.PagoRepository.Update(pago);
          if (respuestaPago.Item1)
          {
            //Actualizamos detalles
            foreach (var pagoDetalle in pagoDetalles)
            {
              var respuestaPagoDetalle = UnitOfWork.PagoDetalleRepository.Update(pagoDetalle);
              if (!respuestaPagoDetalle.Item1)
                throw new Exception("Error al guardar la entidad PagoDetalle.");

              //Agregar datos de detalle a la respuesta
              valoresPagosResponse.ListaDetalle.Add(new RegistrarPagoDetalleResponse { Id = pagoDetalle.Id, Descripcion = pagoDetalle.Descripcion, OrdenPago = pagoDetalle.OrdenPago, ValorTotal = pagoDetalle.ValorTotal, NumeroTransaccion = pagoDetalle.NumeroTransaccion });
            }
            var respuestaActualizacion = await UnitOfWork.SaveChangesAsync().ConfigureAwait(false);

            if (!respuestaActualizacion.Item1)
              throw new Exception(respuestaActualizacion.Item2);

          }
          else
          {
            throw new Exception("Error al guardar la entidad Pago.");
          }


          // Devolvemos respuesta del servicio
          var response = new ApiResponseWrapper(HttpStatusCode.OK, valoresPagosResponse);
          return response;
        }
        catch (Exception ex)
        {
          var response = new ApiResponseWrapper(HttpStatusCode.BadRequest, ex.Message == null ? ex.InnerException : ex.Message);
          return response;
        }

        #endregion

        #endregion


      }

      #endregion Methods
    }

    #endregion Handlers
  }

  public class RegistrarPagoCommandValidator : AbstractValidator<RegistrarPagoCommand>
  {
    public RegistrarPagoCommandValidator()
    {
      //RuleFor(e => e.Deadline)
      //    .NotEmpty().When(e => !string.IsNullOrEmpty(e.RecurrenceId)).WithMessage("{PropertyName} is required.")
      //    .NotNull().When(e => !string.IsNullOrEmpty(e.RecurrenceId)).WithMessage("{PropertyName} must not be null.")
      //    .GreaterThanOrEqualTo(e => e.StartDate).When(e => !string.IsNullOrEmpty(e.RecurrenceId)).WithMessage("{PropertyName} must be greater than or equal to the start date.");
    }
  }
}
