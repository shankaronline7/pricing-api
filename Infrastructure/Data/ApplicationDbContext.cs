using Domain.Entities.UserManagement;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql;
using Pricing.Domain.Constants;
using Pricing.Domain.Entities;
using System.Diagnostics;
using System.Reflection;

namespace Pricing.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IDisposable
{
    private readonly IMediator _mediator;
 

    public ApplicationDbContext(
        DbContextOptions options,
        IMediator mediator
       ) : base(options)
    {
        _mediator = mediator;
     
    }

    public DbSet<Role> Roles { get; set; }
    public DbSet<users> users { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<LeasingCalculationResults> LeasingCalculationResults { get; set; }
    public DbSet<LeasingPriceGroupDefination> LeasingPriceGroupDefinitions { get; set; }
    public DbSet<LeasingPricingConditions> LeasingPricingConditions { get; set; }

    public DbSet<FuelTypeModel> FuelTypes { get; set; }
 
    
    public DbSet<MileageModel> Mileages { get; set; }
    public DbSet<ModelBaseData> ModelBaseDatas { get; set; }
    
    public DbSet<ModelDescriptionModel> ModelDescriptions { get; set; }
    public DbSet<ModelPriceData> ModelPriceData { get; set; }
    
    public DbSet<ModelRangeModel> ModelRanges { get; set; }
    public DbSet<ModelRangeSegmentAssignment> ModelRangeSegmentAssignments { get; set; }
    public DbSet<ModelRangeSeriesAssignment> ModelRangeSeriesAssignments { get; set; }
    
    public DbSet<PriceGroupDefinition> PriceGroupDefinitions { get; set; }
    public DbSet<RetailCostAssignment> RetailCostAssignments { get; set; }
    public DbSet<Segment> Segments { get; set; }
    public DbSet<OriginalSegment> OriginalSegments { get; set; }
    public DbSet<SeriesModel> Series { get; set; }
    public DbSet<Term> Terms { get; set; }
    public DbSet<TermMileage> TermMileages { get; set; }
    public DbSet<TermMileageSpread> TermMileageSpreads { get; set; }


    // public DbSet<AuditableEntitySaveChangesInterceptor> AuditableEntitySaveChangesInterceptors { get;set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<UserStatus>("user_status");

        // ROLE TABLE
        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("roles");

            entity.HasKey(r => r.RoleId);

            entity.Property(r => r.RoleId)
                  .HasColumnName("role_id");

            entity.Property(r => r.RoleName)
                  .HasColumnName("role_name")
                  .IsRequired();
        });


        // USERS TABLE
        modelBuilder.Entity<users>(entity =>
        {
            entity.ToTable("users");

            entity.HasKey(u => u.Id);

            entity.Property(u => u.Id)
                  .HasColumnName("id");

            entity.Property(u => u.RoleId)
                  .HasColumnName("role_id");   // 🔥 VERY IMPORTANT

            entity.HasOne(u => u.Role)
                  .WithMany()
                  .HasForeignKey(u => u.RoleId);
        });





        modelBuilder.Entity<LeasingCalculationResults>(entity =>
        {
            entity.ToTable("BP#LeasingCalculationResults.LCR");
            entity.HasKey(e => e.ID)
                  .HasName("BP#LeasingCalculation####_pkey");
            entity.HasOne(e => e.LeasingPricingCondition)
                  .WithMany(e => e.LeasingCalculationResults)
                  .HasForeignKey(e => e.LeasingPricingConditionsID)
                  .HasConstraintName("FK_BP#LeasingCalculationResult_BP#LeasingPricingConditions.LPC")
                  .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.TermMileage)
                  .WithMany(e => e.LeasingCalculationResults)
                  .HasForeignKey(e => e.TermMileageID)
                  .HasConstraintName("FK_TermMileage")
                  .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<LeasingPriceGroupDefination>(entity =>
        {
            entity.ToTable("BP#LeasingPriceGroupDefination");
            entity.HasKey(e => e.ID)
                  .HasName("BP#LeasingPriceGroupDefination_pkey");
            entity.HasOne(e => e.LeasingPricingCondition)
                  .WithMany(e => e.LeasingPriceGroupDefinitions)
                  .HasForeignKey(e => e.LeasingPricingConditionsID)
                  .HasConstraintName("FK_BP#LeasingPriceGroupDefination_BP#LeasingPricingCalculation.")
                  .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.PriceGroupDefinition)
                  .WithMany()
                  .HasForeignKey(e => e.PriceGroupDefinitionID)
                  .HasConstraintName("FK_BP#LeasingPriceGroupDefination_MD#PriceGroupDefinition.PGD")
                  .OnDelete(DeleteBehavior.NoAction);

        });

        modelBuilder.Entity<LeasingPricingConditions>(entity =>
        {
            entity.ToTable("BP#LeasingPricingConditions.LPC");
            entity.HasKey(e => e.ID)
                  .HasName("BP#LeasingPricingCalculation.LPC_pkey");
            entity.HasOne(e => e.ModelBaseData)
                  .WithMany(e => e.LeasingPricingConditions)
                  .HasForeignKey(e => e.ModelBaseDataID)
                  .HasConstraintName("FK_BP#LeasingPricingCalculation.LPC_MD#ModelBaseData.MBD")
                  .OnDelete(DeleteBehavior.NoAction);
        });


        modelBuilder.Entity<Brand>(entity =>
        {
            entity.ToTable("MD#Brand");
            entity.ToTable("MD#Brand");
            entity.HasKey(e => e.ID)
                  .HasName("MD#Brand_pkey");
        });

        modelBuilder.Entity<FuelTypeModel>(entity =>
        {
            entity.ToTable("MD#FuelType");
            entity.HasKey(e => e.ID)
                  .HasName("MD#FuelType_pkey");
        });


       


        modelBuilder.Entity<MileageModel>(entity =>
        {
            entity.ToTable("MD#Mileage");
            entity.HasKey(e => e.ID)
                  .HasName("MD#Mileage_pkey");
        });

        modelBuilder.Entity<ModelBaseData>(entity =>
        {
            entity.ToTable("MD#ModelBaseData.MBD");
            entity.HasKey(e => e.ID)
                  .HasName("MD#ModelBaseData.MBD_pkey");
            entity.HasOne(e => e.Brand)
                  .WithMany(e => e.ModelBaseDatas)
                  .HasForeignKey(e => e.BrandID)
                  .HasConstraintName("FK_MD#ModelBaseData.MBD_MD#Brand")
                  .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.FuelType)
                  .WithMany(e => e.ModelBaseDatas)
                  .HasForeignKey(e => e.FuelTypeID)
                  .HasConstraintName("FK_MD#ModelBaseData.MBD_MD#FuelType")
                  .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.ModelDescription)
                  .WithMany(e => e.ModelBaseDatas)
                  .HasForeignKey(e => e.ModelDescriptionID)
                  .HasConstraintName("FK_MD#ModelBaseData.MBD_MD#ModelDescription")
                  .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.ModelRange)
                  .WithMany(e => e.ModelBaseDatas)
                  .HasForeignKey(e => e.ModelRangeID)
                  .HasConstraintName("FK_MD#ModelBaseData.MBD_MD#ModelRange")
                  .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.Series)
                  .WithMany(e => e.ModelBaseDatas)
                  .HasForeignKey(e => e.SeriesID)
                  .HasConstraintName("FK_MD#ModelBaseData.MBD_MD#Series")
                  .OnDelete(DeleteBehavior.NoAction);
        });


        modelBuilder.Entity<ModelDescriptionModel>(entity =>
        {
            entity.ToTable("MD#ModelDescription");
            entity.HasKey(e => e.ID)
                  .HasName("MD#ModelDescription_pkey");
        });

        modelBuilder.Entity<ModelPriceData>(entity =>
        {
            entity.ToTable("MD#ModelPriceData.MPD");
            entity.HasKey(e => e.ID)
                  .HasName("MD#ModelPriceData.MPD_pkey");
            entity.HasOne(e => e.ModelBaseData)
                  .WithMany(e => e.ModelPriceDatas)
                  .HasForeignKey(e => e.ModelBaseDataID)
                  .HasConstraintName("FK_MD#ModelPriceData.MPD_MD#ModelBaseData.MBD")
                  .OnDelete(DeleteBehavior.NoAction);
        });

        

        modelBuilder.Entity<ModelRangeModel>(entity =>
        {
            entity.ToTable("MD#ModelRange");
            entity.HasKey(e => e.ID)
                  .HasName("MD#ModelRange_pkey");
        });

        modelBuilder.Entity<ModelRangeSegmentAssignment>(entity =>
        {
            entity.ToTable("MD#ModelRangeSegmentAssignment.MSA");
            entity.HasKey(e => e.ID)
                  .HasName("MD#ModelRangeSegmentAssignment.MSA_pkey");
            entity.HasOne(e => e.FuelType)
                  .WithMany(e => e.ModelRangeSegmentAssignments)
                  .HasForeignKey(e => e.FuelTypeID)
                  .HasConstraintName("FK_MD#ModelRangeSegmentAssignment.MSA_MD#FuelType")
                  .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.ModelRange)
                  .WithMany(e => e.ModelRangeSegmentAssignments)
                  .HasForeignKey(e => e.ModelRangeID)
                  .HasConstraintName("FK_MD#ModelRangeSegmentAssignment.MSA_MD#ModelRange")
                  .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.Segment)
                  .WithMany(e => e.ModelRangeSegmentAssignments)
                  .HasForeignKey(e => e.SegmentID)
                  .HasConstraintName("FK_MD#ModelRangeSegmentAssignment.MSA_MD#Segment")
                  .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.OriginalSegment)
                 .WithMany(e => e.ModelRangeSegmentAssignments)
                 .HasForeignKey(e => e.OriginalSegmentID)
                 .HasConstraintName("FK_MD#ModelRangeSegmentAssignment.MSA_MD#OriginalSegment")
                 .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<ModelRangeSeriesAssignment>(entity =>
        {
            entity.ToTable("MD#ModelRangeSeriesAssignment.MSA");
            entity.HasKey(e => e.ID)
                  .HasName("MD#ModelRangeSeriesAssignment.MSA_pkey");
            entity.HasOne(e => e.ModelRange)
                  .WithMany(e => e.ModelRangeSeriesAssignments)
                  .HasForeignKey(e => e.ModelRangeID)
                  .HasConstraintName("FK_MD#ModelRangeSeriesAssignment.MSA_MD#ModelRange")
                  .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.Series)
                  .WithMany(e => e.ModelRangeSeriesAssignments)
                  .HasForeignKey(e => e.SeriesID)
                  .HasConstraintName("FK_MD#ModelRangeSeriesAssignment.MSA_MD#Series")
                  .OnDelete(DeleteBehavior.NoAction);
        });

        

        modelBuilder.Entity<PriceGroupDefinition>(entity =>
        {
            entity.ToTable("MD#PriceGroupDefinition.PGD");
            entity.HasKey(e => e.ID)
                  .HasName("MD#PriceGroupDefinition.PGD_pkey");
        });


        modelBuilder.Entity<RetailCostAssignment>(entity =>
        {
            entity.ToTable("MD#RetailCostAssignment.RCA");
            entity.HasKey(e => e.ID)
                  .HasName("MD#RetailCostAssignment.RCA_pkey");
            entity.HasOne(e => e.Brand)
                  .WithMany(e => e.RetailCostAssignments)
                  .HasForeignKey(e => e.BrandID)
                  .HasConstraintName("FK_MD#RetailCostAssignment.RCA_MD#Brand")
                  .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Segment>(entity =>
        {
            entity.ToTable("MD#Segment");
            entity.HasKey(e => e.ID)
                  .HasName("MD#Segment_pkey");
        });
        modelBuilder.Entity<OriginalSegment>(entity =>
        {
            entity.ToTable("MD#OriginalSegment");
            entity.HasKey(e => e.ID)
                  .HasName("MD#OriginalSegment_pkey");
        });

        modelBuilder.Entity<SeriesModel>(entity =>
        {
            entity.ToTable("MD#Series");
            entity.HasKey(e => e.ID)
                  .HasName("MD#Series_pkey");
        });

        modelBuilder.Entity<Term>(entity =>
        {
            entity.ToTable("MD#Term");
            entity.HasKey(e => e.Id)
                  .HasName("MD#Term_pkey");
        });

        modelBuilder.Entity<TermMileage>(entity =>
        {
            entity.ToTable("MD#TermMileage");
            entity.HasKey(e => e.ID)
                  .HasName("MD#TermMileage_pkey");
            entity.HasOne(e => e.Mileage)
                  .WithMany(e => e.TermMileages)
                  .HasForeignKey(e => e.MileageID)
                  .HasConstraintName("FK_MD#TermMileage_MD#Mileage")
                  .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.Term)
                  .WithMany(e => e.TermMileages)
                  .HasForeignKey(e => e.TermID)
                  .HasConstraintName("FK_MD#TermMileage_MD#Term")
                  .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<TermMileageSpread>(entity =>
        {
            entity.ToTable("MD#TermMileageSpread.TMS");
            entity.HasKey(e => e.ID)
                  .HasName("MD#TermMileageSpread.TMS_pkey");
            entity.HasOne(e => e.Segment)
                  .WithMany(e => e.TermMileageSpreads)
                  .HasForeignKey(e => e.SegmentID)
                  .HasConstraintName("FK_MD#TermMileageSpread.TMS_MD#Segment")
                  .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.TermMileage)
                  .WithMany(e => e.TermMileageSpreads)
                  .HasForeignKey(e => e.TermMileageID)
                  .HasConstraintName("FK_TermMileage")
                  .OnDelete(DeleteBehavior.NoAction);
        });

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entity.GetProperties())
            {
                if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                {
                    property.SetValueConverter(
                        new ValueConverter<DateTime, DateTime>(
                            v => DateTime.SpecifyKind(v, DateTimeKind.Utc),
                            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                        )
                    );
                }
            }
        }
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }



    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {

            // Optional: you can temporarily remove the transaction to test DB speed
            await using var transaction = await Database.BeginTransactionAsync(System.Data.IsolationLevel.ReadCommitted, cancellationToken);

          

            var result = await base.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            stopwatch.Stop();
            Console.WriteLine($"✅ SaveChangesAsync completed in {stopwatch.ElapsedMilliseconds} ms");

            return result;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            Console.WriteLine($"❌ SaveChangesAsync failed after {stopwatch.ElapsedMilliseconds} ms: {ex.Message}");
            throw;
        }
    }



    public override int SaveChanges()
    {
        //await _mediator.DispatchDomainEvents(this);
        using (var transaction = Database.BeginTransaction())
        {
            var result = base.SaveChanges();
            transaction.Commit();
            return result;
        }



    }



}
