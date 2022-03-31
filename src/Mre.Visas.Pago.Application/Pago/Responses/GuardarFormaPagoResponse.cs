using Mre.Visas.Pago.Application.Responses;
using System;
using System.Collections.Generic;

namespace Mre.Visas.Pago.Application.Pago.Responses
{
    public class GuardarFormaPagoResponse : BaseResponse
    {
        public GuardarFormaPagoResponse(string id) : base(id)
        {
            ListaDetalle = new List<GuardarFormaPagoDetalle>();
        }

        public List<GuardarFormaPagoDetalle> ListaDetalle { get; set; }

    }

    public class GuardarFormaPagoDetalle
    {
        public Guid Id { get; set; }

        public string Descripcion { get; set; }
        public string OrdenPago { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
