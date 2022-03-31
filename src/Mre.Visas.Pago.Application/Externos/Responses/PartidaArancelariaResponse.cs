using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mre.Visas.Pago.Application.Externos.Responses
{

  public class ListaPartidaArancelaria
  {
    public List<PartidaArancelariaResponse> Items { get; set; }
  }
  public class PartidaArancelariaResponse
  {
    public Guid PartidaArancelariaId { get; set; }
    public Guid ServicioId { get; set; }
    public string Servicio { get; set; }
    public string PartidaArancelaria { get; set; }
    public string NumeroPartida { get; set; }
    public decimal Valor { get; set; }
    public Guid JerarquiaArancelariaId { get; set; }
    public string JerarquiaArancelaria { get; set; }
    public Guid ArancelId { get; set; }
    public string Arancel { get; set; }
  }


  public class ListaConvenios
  {
    public List<ConvenioArancelRespopnse> Items { get; set; }
  }
  public class ConvenioArancelRespopnse
  {

    public Guid ConvenioId { get; set; }
    public Guid ServicioId { get; set; }
    public string Servicio { get; set; }
    public string TipoServicio { get; set; }
    public decimal Valor { get; set; }
    public string EntidadAuspicianteId { get; set; }
    public string EntidadAuspiciante { get; set; }
    public string PaisId { get; set; }

    private int? _edadInicial;
    public int? EdadInicial
    {
      get => _edadInicial;
      set => _edadInicial = value == null ? 0 : value;
    }

    private int? _edadFinal;
    public int? EdadFinal
    {
      get => _edadFinal;
      set => _edadFinal = value == null ? 0 : value;
    }


    private bool? _discapacitado;
    public bool? Discapacitado
    {
      get => _discapacitado;
      set => _discapacitado = value == null ? false : value;
    }
    public string TipoExoneracionId { get; set; }
    public string TipoExoneracion { get; set; }


  }
}
