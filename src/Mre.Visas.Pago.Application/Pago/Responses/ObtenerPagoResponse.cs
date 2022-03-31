using Mre.Visas.Pago.Application.Responses;
using Mre.Visas.Pago.Domain.Entities;
using Mre.Visas.Pago.Application.Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace Mre.Visas.Pago.Application.Pago.Responses
{
    public class ObtenerPagoResponse : IPagoDto
    {
        
        public ObtenerPagoResponse(string id)
        {
            Id = id;
        }


        public ObtenerPagoResponse()
        {
            ListaPagoDetalle = new List<PagoDetalle>();
        }
    }
}