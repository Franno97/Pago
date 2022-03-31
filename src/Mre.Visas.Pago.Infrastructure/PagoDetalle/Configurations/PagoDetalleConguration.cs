using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mre.Visas.Pago.Infrastructure.PagoDetalle.Configurations
{
  public class PagoDetalleConfiguration : IEntityTypeConfiguration<Domain.Entities.PagoDetalle>
  {
    public void Configure(EntityTypeBuilder<Domain.Entities.PagoDetalle> builder)
    {
      builder.ToTable("PagoDetalle");

      builder.HasKey(e => e.Id);

      builder.Property(e => e.Created).IsRequired(true);

      builder.Property(e => e.CreatorId)
          .IsRequired(true);

      builder.Property(e => e.LastModified)
      .IsRequired(true);

      builder.Property(e => e.LastModifierId)
      .IsRequired(true);

      builder.Property(e => e.IdTramite).IsRequired(true);
      builder.Property(e => e.IdPago).IsRequired(true);
      builder.Property(e => e.Orden).IsRequired(true);
      builder.Property(e => e.Descripcion).IsRequired(true).HasMaxLength(250);
      builder.Property(e => e.ArancelId).IsRequired(true);
      builder.Property(e => e.ValorArancel).IsRequired(true);
      builder.Property(e => e.PorcentajeDescuento).IsRequired(true);
      builder.Property(e => e.ValorDescuento).IsRequired(true);
      builder.Property(e => e.ValorTotal).IsRequired(true);
      builder.Property(e => e.Estado).IsRequired(true).HasMaxLength(50);
      builder.Property(e => e.OrdenPago).IsRequired(true).HasMaxLength(50);
      builder.Property(e => e.NumeroTransaccion).IsRequired(true).HasMaxLength(50);
      builder.Property(e => e.ComprobantePago).IsRequired(true).HasColumnType("nvarchar(max)");
      builder.Property(e => e.IsDeleted).IsRequired(true);
      builder.Property(e => e.PartidaArancelariaId).IsRequired(true);
      builder.Property(e => e.ServicioId).IsRequired(true);
      builder.Property(e => e.Servicio).IsRequired(true);
      builder.Property(e => e.PartidaArancelaria).IsRequired(true);
      builder.Property(e => e.NumeroPartida).IsRequired(true);
      builder.Property(e => e.JerarquiaArancelariaId).IsRequired(true);
      builder.Property(e => e.JerarquiaArancelaria).IsRequired(true);
      builder.Property(e => e.ArancelId).IsRequired(true);
      builder.Property(e => e.Arancel).IsRequired(true);
      builder.Property(e => e.ConvenioId).IsRequired(true);
      builder.Property(e => e.TipoServicio).IsRequired(true);
      builder.Property(e => e.TipoExoneracionId).IsRequired(true);
      builder.Property(e => e.TipoExoneracion).IsRequired(true);
      builder.Property(e => e.EstaFacturado).IsRequired(true);
      builder.Property(e => e.FacturarEn).IsRequired(true);
      builder.Property(e => e.FechaPago).IsRequired(true);
    }
  }
}
