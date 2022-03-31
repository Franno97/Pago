using Mre.Visas.Pago.Application.Responses;

namespace Mre.Visas.Pago.Application.Pago.Responses
{
  public class CalcularPagoResponse : BaseResponse
  {
    public string Mensaje { get; set; }

    public CalcularPagoResponse(string id, string mensaje) : base(id)
    {
      Mensaje = mensaje;
    }
  }
}
