using Audit.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Mre.Visas.Pago.Domain.Entities;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Mre.Visas.Pago.Infrastructure.Persistence.Contexts
{
  public class ApplicationDbContext : DbContext
  {
    #region Properties

    public DbSet<Domain.Entities.Pago> Pagos { get; set; }
    public DbSet<Domain.Entities.PagoDetalle> PagoDetalles { get; set; }
    public DbSet<Domain.Entities.ConfiguracionPago> ConfiguracionesPagos { get; set; }

    #endregion Properties

    #region Constructors

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
    {
    }

        #endregion Constructors

        #region Methods

        //Configure Db
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    if (!options.IsConfigured)
        //    {
        //        options.UseSqlServer("Server=172.31.3.34; Database=Mre.Visas.Pago; User Id=sa; Password=2xUWheya$kR7ZBJew*a5;");
        //    }
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new AuditSaveChangesInterceptor());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

      base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
      foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
      {
        switch (entry.State)
        {
          case EntityState.Added:
            entry.Entity.Created = DateTime.UtcNow;
            break;

          case EntityState.Modified:
            entry.Entity.LastModified = DateTime.UtcNow;
            break;
        }
      }

      return base.SaveChangesAsync(cancellationToken);
    }

    #endregion Methods
  }
}