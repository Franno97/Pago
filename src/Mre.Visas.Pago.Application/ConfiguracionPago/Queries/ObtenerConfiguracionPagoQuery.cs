using FluentValidation;
using MediatR;
using AutoMapper;
using Mre.Visas.Pago.Application.Wrappers;
using Mre.Visas.Pago.Application.ConfiguracionPago.Requests;
using Mre.Visas.Pago.Application.Pago.Responses;
using Mre.Visas.Pago.Application.Shared.Handlers;
using Mre.Visas.Pago.Application.Shared.Interfaces;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Mre.Visas.Pago.Application.ConfiguracionPago.Queries
{
    public class ObtenerConfiguracionPagoQuery : ObtenerConfiguracionPagoRequest, IRequest<ApiResponseWrapper>
    {

        #region Constructors

        public ObtenerConfiguracionPagoQuery(ObtenerConfiguracionPagoRequest request) : base(request.ServicioId)
        {
            ServicioId = request.ServicioId;
        }

        #endregion Constructors

        #region Handlers

        public class ObtenerConfiguracionPagoQueryHandler : BaseHandler, IRequestHandler<ObtenerConfiguracionPagoQuery, ApiResponseWrapper>
        {
            #region Constructors

            public ObtenerConfiguracionPagoQueryHandler(IUnitOfWork unitOfWork)
                : base(unitOfWork)
            {
                
            }

            #endregion Constructors

            #region Methods

            public async Task<ApiResponseWrapper> Handle(ObtenerConfiguracionPagoQuery query, CancellationToken cancellationToken)
            {

                var configuracion = await UnitOfWork.ConfiguracionPagoRepository.GetByServicioIdAsync(query.ServicioId);

                var response = new ApiResponseWrapper(HttpStatusCode.OK, configuracion);
                
                return response;
            }

            #endregion Methods
        }

        #endregion Handlers
    }

    public class ObtenerConfiguracionPagoQueryValidator : AbstractValidator<ObtenerConfiguracionPagoQuery>
    {
        public ObtenerConfiguracionPagoQueryValidator()
        {
            //RuleFor(e => e.ProjectId).Must(e => e.Length.Equals(38)).When(e => !string.IsNullOrEmpty(e.ProjectId)).WithMessage("{PropertyName} must be exactly 38 characters.");
        }
    }
}