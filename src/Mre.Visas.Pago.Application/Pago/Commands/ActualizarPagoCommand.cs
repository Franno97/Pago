using FluentValidation;
using MediatR;
using Mre.Visas.Pago.Application.Pago.Requests;
using Mre.Visas.Pago.Application.Shared.Handlers;
using Mre.Visas.Pago.Application.Shared.Interfaces;
using Mre.Visas.Pago.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mre.Visas.Pago.Application.Pago.Commands
{
  public class ActualizarPagoCommand : ActualizarPagoRequest, IRequest<ApiResponseWrapper>
  {
    #region Constructors

    public ActualizarPagoCommand(ActualizarPagoRequest request)
    {
      IdPagoDetalle = request.IdPagoDetalle;
      ClaveAcceso = request.ClaveAcceso;
    }

    #endregion Constructors

    #region Handlers

    public class ActualizarPagoCommandHandler : BaseHandler, IRequestHandler<ActualizarPagoCommand, ApiResponseWrapper>
    {
      #region Constructors

      public ActualizarPagoCommandHandler(IUnitOfWork unitOfWork)
          : base(unitOfWork)
      {
      }

      #endregion Constructors

      #region Methods

      public async Task<ApiResponseWrapper> Handle(ActualizarPagoCommand command, CancellationToken cancellationToken)
      {
        var response = new Responses.ActualizarPagoResponse { Estado = "OK", Mensaje = "Actualizado correctamente" };
        var resultado = new ApiResponseWrapper();
        var detalle = await UnitOfWork.PagoDetalleRepository.ObtenerPagoDeallePorId(command.IdPagoDetalle).ConfigureAwait(false);

        #region Actualizar
        try
        {
          detalle.ClaveAcceso = command.ClaveAcceso;
          detalle.EstaFacturado = true;
          detalle.LastModified = DateTime.Now;


          var update = UnitOfWork.PagoDetalleRepository.Update(detalle);
          if (update.Item1)
          {
            var respuestaActualizacion = await UnitOfWork.SaveChangesAsync().ConfigureAwait(false);
            if (!respuestaActualizacion.Item1)
              throw new Exception(respuestaActualizacion.Item2);
          }
          else
          {
            throw new Exception("Error al Actualizar la entidad Pago.");
          }

          // Devolvemos respuesta del servicio
          resultado = new ApiResponseWrapper(HttpStatusCode.OK, response);

        }
        catch (Exception ex)
        {
          response.Mensaje = ex.Message;
          resultado = new ApiResponseWrapper(HttpStatusCode.BadGateway, response);
        }
        return resultado;
        #endregion

      }

      #endregion Methods
    }

    #endregion Handlers
  }

  public class ActualizarPagoCommandValidator : AbstractValidator<ActualizarPagoCommand>
  {
    public ActualizarPagoCommandValidator()
    {
      //RuleFor(e => e.Deadline)
      //    .NotEmpty().When(e => !string.IsNullOrEmpty(e.RecurrenceId)).WithMessage("{PropertyName} is required.")
      //    .NotNull().When(e => !string.IsNullOrEmpty(e.RecurrenceId)).WithMessage("{PropertyName} must not be null.")
      //    .GreaterThanOrEqualTo(e => e.StartDate).When(e => !string.IsNullOrEmpty(e.RecurrenceId)).WithMessage("{PropertyName} must be greater than or equal to the start date.");
    }
  }
}
