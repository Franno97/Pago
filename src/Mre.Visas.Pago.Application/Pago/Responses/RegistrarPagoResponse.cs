using Mre.Visas.Pago.Application.Responses;
using System;
using System.Collections.Generic;

namespace Mre.Visas.Pago.Application.Pago.Responses
{
    public class RegistrarPagoResponse : BaseResponse
    {
        public RegistrarPagoResponse(string id) : base(id)
        {
            ListaDetalle = new List<RegistrarPagoDetalleResponse>();
        }

        public List<RegistrarPagoDetalleResponse> ListaDetalle { get; set; }

    }

    public class RegistrarPagoDetalleResponse
    {
        public Guid Id { get; set; }

        public string Descripcion { get; set; }
        public string OrdenPago { get; set; }
        public decimal ValorTotal { get; set; }

        public string NumeroTransaccion { get; set; }

        public string Observacion { get; set; }
    }
}
