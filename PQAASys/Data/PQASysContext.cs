using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PQAASys
{
    public partial class PQASysContext : DbContext
    {
        public PQASysContext()
        {
        }

        public PQASysContext(DbContextOptions<PQASysContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        public  DbSet<Characteristic> Characteristics { get; set; }
        public  DbSet<Condition> Conditions { get; set; }
        public  DbSet<Department> Departments { get; set; }
        public  DbSet<Organization> Organizations { get; set; }
        public  DbSet<Position> Positions { get; set; }
        public  DbSet<Request> Requests { get; set; }
        //public  DbSet<RequestStandart> RequestStandarts { get; set; }
        public  DbSet<Role> Roles { get; set; }
        public  DbSet<RoleUser> RoleUsers { get; set; }
        public  DbSet<SampleSet> SampleSets { get; set; }
        public  DbSet<Standart> Standarts { get; set; }
        //public  DbSet<StandartTest> StandartTests { get; set; }
        public  DbSet<Status> Statuses { get; set; }
        public  DbSet<Test> Tests { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PQASys;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Characteristic>(entity =>
            {
                entity.ToTable("Characteristic");

                entity.Property(e => e.CharacteristicId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("characteristic_id");

                entity.Property(e => e.CharacteristicName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("characteristic_name");

                entity.Property(e => e.Units)
                    .HasMaxLength(10)
                    .HasColumnName("units");
                entity.Property(e => e.Test).HasColumnName("Test");

                entity.HasOne(d => d.TestNavigation)
                    .WithMany(p => p.Characteristics)
                    .HasForeignKey(d => d.Test)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Characteri__Test__02FC7413");
            });

            modelBuilder.Entity<Condition>(entity =>
            {
                entity.ToTable("Condition");

                entity.Property(e => e.ConditionId)
                    .ValueGeneratedNever()
                    .HasColumnName("condition_id");

                entity.Property(e => e.Environment)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("environment");

                entity.Property(e => e.Temperature).HasColumnName("temperature");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.DepartmentId)
                    .ValueGeneratedNever()
                    .HasColumnName("department_id");

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("department_name");

                entity.Property(e => e.Description).HasColumnName("description");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.Property(e => e.OrganizationId)
                    .ValueGeneratedNever()
                    .HasColumnName("organization_id");

                entity.Property(e => e.OrganizationAddress)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("organization_address");

                entity.Property(e => e.OrganizationDescription).HasColumnName("organization_description");

                entity.Property(e => e.OrganizationName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("organization_name");

                entity.Property(e => e.OrganizationPhonenum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("organization_phonenum");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.ToTable("Position");

                entity.Property(e => e.PositionId)
                    .ValueGeneratedNever()
                    .HasColumnName("position_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.PositionName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("position_name");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasKey(e => e.requestNumber);

                entity.ToTable("Request");

                entity.Property(e => e.requestNumber)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("requestNumber");

                entity.Property(e => e.innerCustomer).HasColumnName("inner_customer");

                entity.Property(e => e.outerCustomer).HasColumnName("outer_customer");

                entity.Property(e => e.typeOfTest).HasColumnName("typeOfTest");

                entity.HasOne(d => d.InfoAboutConditionsNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.InfoAboutConditions)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Request__InfoAbo__628FA481");

                entity.HasOne(d => d.InfoAboutSamplesNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.InfoAboutSamples)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Request__InfoAbo__534D60F1");

                entity.HasOne(d => d.InnerCustomerNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.innerCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Request__inner_c__5070F446");

                entity.HasOne(d => d.OuterCustomerNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.outerCustomer)
                    .HasConstraintName("FK__Request__outer_c__5165187F");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Request__Status__571DF1D5");

                entity.HasOne(d => d.TypeOfTestNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.typeOfTest)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Request__typeOfT__52593CB8");
                entity.HasOne(d => d.StandartNavigation)
                .WithMany(p => p.Requests)
                .HasForeignKey(d => d.Standart)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Request__Standar__160F4887");
                entity.Property(e => e.RequestDate).HasColumnName("RequestDate");
            });

            //modelBuilder.Entity<RequestStandart>(entity =>
            //{
            //    entity.HasKey(e=>new { e.RequestNumber,e.StandartSeries,e.StandartNumber});

            //    entity.ToTable("RequestStandart");
            //    entity.Property(e => e.RequestNumber)
            //    .IsRequired();
            //    entity.Property(e => e.StandartNumber)
            //        .IsRequired()
            //        .HasMaxLength(20);

            //    entity.Property(e => e.StandartSeries)
            //        .IsRequired()
            //        .HasMaxLength(20);

            //    entity.HasOne(d => d.RequestNumberNavigation)
            //        .WithMany()
            //        .HasForeignKey(d => d.RequestNumber)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK__RequestSt__Reque__5BE2A6F2");

            //    entity.HasOne(d => d.StandartNavigation)
            //        .WithMany()
            //        .HasForeignKey(d => new { d.StandartSeries, d.StandartNumber })
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK__RequestStandart__5CD6CB2B");
            //});

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("role_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<RoleUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("RoleUser");

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RoleUser__Role__60A75C0F");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RoleUser__User__619B8048");
            });

            modelBuilder.Entity<SampleSet>(entity =>
            {
                entity.ToTable("SampleSet");

                entity.Property(e => e.sampleSetId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("sampleSet_id");

                entity.Property(e => e.Marking)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.MaterialGrade)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.numOfSamples).HasColumnName("numOfSamples");

                entity.Property(e => e.TypeOfProduct)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Standart>(entity =>
            {
                entity.HasKey(e => e.Standart_id);

                entity.ToTable("Standart");
                entity.Property(e => e.Standart_id)
                .ValueGeneratedOnAdd()
                .HasColumnName("Standart_id");

                entity.Property(e => e.StandartSeries)
                    .HasMaxLength(20)
                    .HasColumnName("standartSeries");

                entity.Property(e => e.StandartNumber)
                    .HasMaxLength(20)
                    .HasColumnName("standartNumber");

                entity.Property(e => e.StandartName)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("standart_name");

                entity.Property(e => e.YearOfIssue)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("YearOfIssue");
            });

            //modelBuilder.Entity<StandartTest>(entity =>
            //{
            //    entity.HasNoKey();

            //    entity.ToTable("StandartTest");

            //     entity.HasOne(d => d.TestNavigation)
            //        .WithMany()
            //        .HasForeignKey(d => d.Test)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK__StandartTe__Test__5EBF139D");

            //    entity.HasOne(d => d.StandartNavigation)
            //        .WithMany()
            //        .HasForeignKey(d =>d.Standart)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK__StandartT__Stand__17036CC0");
            //});

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.StatusId)
                    .ValueGeneratedNever()
                    .HasColumnName("status_id");

                entity.Property(e => e.Status1)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("Status1");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.ToTable("Test");

                entity.Property(e => e.TestId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("test_id");

               // entity.Property(e => e.DetermineCharacteristic).HasColumnName("determine_characteristic");

                entity.Property(e => e.TestName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("test_name");

                
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("user_id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Patronymic)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.SurName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.DepartmentNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Department)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User__Department__4E88ABD4");

                entity.HasOne(d => d.PositionNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Position)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User__Position__4D94879B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
