using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mre.Visas.Pago.Infrastructure.ConfiguracionPago.Configurations
{
  public class ConfiguracionPagoConfiguration : IEntityTypeConfiguration<Domain.Entities.ConfiguracionPago>
  {
    public void Configure(EntityTypeBuilder<Domain.Entities.ConfiguracionPago> builder)
    {
      builder.ToTable("ConfiguracionPago");

      builder.HasKey(e => e.Id);

      builder.Property(e => e.UnidadAdministrativaId).IsRequired(true);
      builder.Property(e => e.ServicioId).IsRequired(true);
      builder.Property(e => e.ServicioIdPago).IsRequired(true);
      builder.Property(e => e.Descripcion).IsRequired(true).HasMaxLength(50); ;
      builder.Property(e => e.FacturarEn).IsRequired(true).HasMaxLength(10); ;
      builder.Property(e => e.IsDeleted).IsRequired(true);
      builder.Property(e => e.Created).IsRequired(true);
      builder.Property(e => e.CreatorId).IsRequired(true);
      builder.Property(e => e.LastModified).IsRequired(true);
      builder.Property(e => e.LastModifierId).IsRequired(true);

    }
  }

}


//add-migration AddEstadoToTramite -s Mre.Visas.Tramite.Infrastructure
//update-database -s Mre.Visas.Tramite.Infrastructure