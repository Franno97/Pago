using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mre.Visas.Pago.Application.Pago.Dtos
{
  public class ArancelDto
  {
    public Guid ServicioId { get; set; }
    public string PartidaArancelaria { get; set; }
    public string DescripcionArancelaria { get; set; }
    public string Arancel { get; set; }
    public decimal ValorArancelario { get; set; }
    public Guid ArancelId { get; set; }
    public Guid PartidaArancelariaId { get; set; }
    public string Servicio { get; set; }
    public string NumeroPartida { get; set; }

    public Guid JerarquiaArancelariaId { get; set; }

    public string JerarquiaArancelaria { get; set; }

    public Guid ConvenioId { get; set; }

    public string TipoServicio { get; set; }

    public string TipoExoneracionId { get; set; }

    public string TipoExoneracion { get; set; }

    public string FacturarEn { get; set; }

    public ArancelDto()
    {
      ServicioId = Guid.Empty;
      PartidaArancelaria = string.Empty;
      DescripcionArancelaria = string.Empty;
      Arancel = string.Empty;
      ValorArancelario = 0;
      ArancelId = Guid.Empty;
      PartidaArancelariaId = Guid.Empty;
      Servicio = string.Empty;
      NumeroPartida = string.Empty;
      JerarquiaArancelariaId = Guid.Empty;
      JerarquiaArancelaria = string.Empty;
      ConvenioId = Guid.Empty;
      TipoServicio = string.Empty;
      TipoExoneracionId = string.Empty;
      TipoExoneracion = string.Empty;
      FacturarEn = string.Empty;
    }

  }
}
