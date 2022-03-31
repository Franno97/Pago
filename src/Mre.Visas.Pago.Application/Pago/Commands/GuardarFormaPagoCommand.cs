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
using System.Linq;

namespace Mre.Visas.Pago.Application.Pago.Commands
{
  public class GuardarFormaPagoCommand : GuardarFormaPagoRequest, IRequest<ApiResponseWrapper>
  {
    #region Constructors

    public GuardarFormaPagoCommand(GuardarFormaPagoRequest request)
    {
      Id = request.Id;
      FormaPago = request.FormaPago;
      ListaDetalle = new List<Requests.GuardarFormaPagoDetalle>();
      IdUsuario = request.IdUsuario;
      Banco = request.Banco;
      NumeroCuenta = request.NumeroCuenta;
      TipoCuenta = request.TipoCuenta;
      TitularCuenta = request.TitularCuenta;
      request.ListaDetalle.ForEach(x => ListaDetalle.Add(x));
    }

    #endregion Constructors

    #region Handlers

    public class GuardarFormaPagoCommandHandler : BaseHandler, IRequestHandler<GuardarFormaPagoCommand, ApiResponseWrapper>
    {
      #region Constructors

      public GuardarFormaPagoCommandHandler(IUnitOfWork unitOfWork)
          : base(unitOfWork)
      {
      }

      #endregion Constructors

      #region Methods


      public async Task<ApiResponseWrapper> Handle(GuardarFormaPagoCommand command, CancellationToken cancellationToken)
      {
        try
        {
          var pago = await UnitOfWork.PagoRepository.ObtenerPagoPorIdAsync(command.Id).ConfigureAwait(false);
          pago.FormaPago = command.FormaPago;
          pago.Banco = command.Banco;
          pago.NumeroCuenta = command.NumeroCuenta;
          pago.TipoCuenta = command.TipoCuenta;
          pago.TitularCuenta = command.TitularCuenta;
          pago.LastModified = DateTime.Now;
          pago.LastModifierId = command.IdUsuario;

          var pagoDetalles = new List<Domain.Entities.PagoDetalle>();

          // Pago con depósito o transferencia
          if (command.FormaPago == Domain.Enums.FormaPago.OtrosSistemaFinanciero)
          {
            foreach (var item in command.ListaDetalle.Where(x => x.ValorTotal > 0))
            {
              //Generamos la orden de pago
              var detalle = await UnitOfWork.PagoDetalleRepository.ObtenerPagoDeallePorId(item.Id).ConfigureAwait(false);
              detalle.OrdenPago = "-1";//DateTime.Now.Ticks.ToString(); //Obteniendo un numero unico para la orden de pago
              pagoDetalles.Add(detalle);
            }
          }
          #region Guardar

          // Armamos la respuesta
          var valoresPagosResponse = new GuardarFormaPagoResponse(pago.Id.ToString());

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
              valoresPagosResponse.ListaDetalle.Add(new Responses.GuardarFormaPagoDetalle { Id = pagoDetalle.Id, Descripcion = pagoDetalle.Descripcion, OrdenPago = pagoDetalle.OrdenPago, ValorTotal = pagoDetalle.ValorTotal });
            }
            var respuestaActualizacion = await UnitOfWork.SaveChangesAsync().ConfigureAwait(false);

            if (!respuestaActualizacion.Item1)
              throw new Exception(respuestaActualizacion.Item2);

            /*Enviar notificación al usuario*/

            // Devolvemos respuesta del servicio
            var response = new ApiResponseWrapper(HttpStatusCode.OK, valoresPagosResponse);
            return response;


          }
          else
          {
            throw new Exception("Error al guardar la entidad Pago.");
          }

          #endregion
        }
        catch (Exception ex)
        {
          return new ApiResponseWrapper(HttpStatusCode.BadRequest, ex.Message == null ? ex.InnerException : ex.Message);
        }

      }

      #endregion Methods
    }

    #endregion Handlers
  }

  public class GuardarFormaPagoCommandValidator : AbstractValidator<GuardarFormaPagoCommand>
  {
    public GuardarFormaPagoCommandValidator()
    {
      //RuleFor(e => e.Deadline)
      //    .NotEmpty().When(e => !string.IsNullOrEmpty(e.RecurrenceId)).WithMessage("{PropertyName} is required.")
      //    .NotNull().When(e => !string.IsNullOrEmpty(e.RecurrenceId)).WithMessage("{PropertyName} must not be null.")
      //    .GreaterThanOrEqualTo(e => e.StartDate).When(e => !string.IsNullOrEmpty(e.RecurrenceId)).WithMessage("{PropertyName} must be greater than or equal to the start date.");
    }
  }
}
