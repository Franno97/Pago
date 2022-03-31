using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mre.Visas.Pago.Infrastructure.Pago.Configurations
{
  public class PagoConfiguration : IEntityTypeConfiguration<Domain.Entities.Pago>
  {
    public void Configure(EntityTypeBuilder<Domain.Entities.Pago> builder)
    {
      builder.ToTable("Pago");

      builder.HasKey(e => e.Id);

      builder.Property(e => e.ServicioId).IsRequired(true);
      builder.Property(e => e.IdTramite).IsRequired(true);
      builder.Property(e => e.FormaPago).IsRequired(true);
      builder.Property(e => e.Observacion).IsRequired(true).HasColumnType("nvarchar(max)");
      builder.Property(e => e.Estado).IsRequired(true).HasMaxLength(50);
      builder.Property(e => e.IsDeleted).IsRequired(true);
      builder.Property(e => e.Created).IsRequired(true);
      builder.Property(e => e.CreatorId).IsRequired(true);
      builder.Property(e => e.LastModified).IsRequired(true);
      builder.Property(e => e.LastModifierId).IsRequired(true);
      builder.Property(e => e.Banco).IsRequired(true);
      builder.Property(e => e.NumeroCuenta).IsRequired(true);
      builder.Property(e => e.TipoCuenta).IsRequired(true);
      builder.Property(e => e.TitularCuenta).IsRequired(true);

    }
  }


}


//add-migration AddEstadoToTramite -s Mre.Visas.Tramite.Infrastructure
//update-database -s Mre.Visas.Tramite.Infrastructure