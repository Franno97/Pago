using FluentValidation;
using MediatR;
using AutoMapper;
using Mre.Visas.Pago.Application.Wrappers;
using Mre.Visas.Pago.Application.Pago.Requests;
using Mre.Visas.Pago.Application.Pago.Responses;
using Mre.Visas.Pago.Application.Shared.Handlers;
using Mre.Visas.Pago.Application.Shared.Interfaces;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Mre.Visas.Pago.Application.Pago.Queries
{
  public class ObtenerPagoQuery : ObtenerPagoRequest, IRequest<ApiResponseWrapper>
  {

    #region Constructors

    public ObtenerPagoQuery(ObtenerPagoRequest request) : base(request.Id, request.ValoresMayoraCero, request.FacturarEn)
    {
      Id = request.Id;
      ValoresMayoraCero = request.ValoresMayoraCero;
      FacturarEn = request.FacturarEn;
    }

    #endregion Constructors

    #region Handlers

    public class ObtenerPagoQueryHandler : BaseHandler, IRequestHandler<ObtenerPagoQuery, ApiResponseWrapper>
    {
      #region Constructors

      public ObtenerPagoQueryHandler(IUnitOfWork unitOfWork)
          : base(unitOfWork)
      {

      }

      #endregion Constructors

      #region Methods

      public async Task<ApiResponseWrapper> Handle(ObtenerPagoQuery query, CancellationToken cancellationToken)
      {
        var resultadoPago = await UnitOfWork.PagoRepository.ObtenerPagoIdTramiteAsync(new Guid(query.IdTramite), query.ValoresMayoraCero, query.FacturarEn);

        var response = new ApiResponseWrapper(HttpStatusCode.OK, resultadoPago);

        return response;
      }

      #endregion Methods
    }

    #endregion Handlers
  }

  public class ObtenerPagoQueryValidator : AbstractValidator<ObtenerPagoQuery>
  {
    public ObtenerPagoQueryValidator()
    {
      //RuleFor(e => e.ProjectId).Must(e => e.Length.Equals(38)).When(e => !string.IsNullOrEmpty(e.ProjectId)).WithMessage("{PropertyName} must be exactly 38 characters.");
    }
  }
}