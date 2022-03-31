using FluentValidation;
using MediatR;
using Mre.Visas.Pago.Application.Pago.Requests;
using Mre.Visas.Pago.Application.Pago.Responses;
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

namespace Mre.Visas.Pago.Application.Pago.Queries
{
  public class ValidarComprobanteQuery : ValidarComprobanteRequest, IRequest<ApiResponseWrapper>
  {

    #region Constructors

    public ValidarComprobanteQuery(List<ValidarComprobante> request) : base(request)
    {
      ListaComprobante = request;
    }

    #endregion Constructors


    #region Handlers

    public class ValidarComprobanteQueryHandler : BaseHandler, IRequestHandler<ValidarComprobanteQuery, ApiResponseWrapper>
    {
      #region Constructors

      public ValidarComprobanteQueryHandler(IUnitOfWork unitOfWork)
          : base(unitOfWork)
      {

      }

      #endregion Constructors

      #region Methods

      public async Task<ApiResponseWrapper> Handle(ValidarComprobanteQuery query, CancellationToken cancellationToken)
      {

        // Validar si el numero de transacción está siendo utilizada en otro pago
        #region ValidarTransaccion
        // Armando la respuesta
        var validacionResponse = new RegistrarPagoResponse(Guid.Empty.ToString());

        foreach (var item in query.ListaComprobante.Where(c => !string.IsNullOrEmpty(c.NumeroTransaccion)))
        {
          //Obtener la orden de pago
          var detalle = await UnitOfWork.PagoDetalleRepository.ObtenerPagoDetallePorTransaccionEnOtroMovimiento(item.NumeroTransaccion, item.IdPagoDetalle).ConfigureAwait(false);
          if (detalle != null)
          {
            //Agregar datos de detalle a la respuesta
            validacionResponse.ListaDetalle.Add(new RegistrarPagoDetalleResponse { Id = detalle.Id, NumeroTransaccion = item.NumeroTransaccion, ValorTotal = detalle.ValorTotal, Observacion = "Error, el número de transacción ya fue registrado." });
          }
        }

        if (validacionResponse.ListaDetalle.Count > 0)
          return new ApiResponseWrapper(HttpStatusCode.BadRequest, validacionResponse);

        #endregion

        var response = new ApiResponseWrapper(HttpStatusCode.OK, validacionResponse);

        return response;
      }

      #endregion Methods
    }

    #endregion Handlers



  }

  public class ValidarComprobanteQueryValidator : AbstractValidator<ValidarComprobanteQuery>
  {
    public ValidarComprobanteQueryValidator()
    {
      //RuleFor(e => e.ProjectId).Must(e => e.Length.Equals(38)).When(e => !string.IsNullOrEmpty(e.ProjectId)).WithMessage("{PropertyName} must be exactly 38 characters.");
    }
  }

}
