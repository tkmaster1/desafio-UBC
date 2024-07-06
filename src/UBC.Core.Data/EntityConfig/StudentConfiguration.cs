using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UBC.Core.Domain.Entities;

namespace UBC.Core.Data.EntityConfig
{
    public class StudentConfiguration : IEntityTypeConfiguration<StudentEntity>
    {
        public void Configure(EntityTypeBuilder<StudentEntity> builder)
        {
            builder.ToTable("Student", "dbo");

            builder
                .Property(p => p.Code)
                .HasColumnName("Id");

            builder
                .HasKey(p => p.Code)
                .HasAnnotation("SqlServer:Identity", "1, 1")
                .HasName("PK_Students");

            builder
                .Property(p => p.Name)
                .HasColumnType("varchar(256)")
                .IsRequired()
                .HasColumnName("Nome");

            builder
                .Property(p => p.Age)
                .IsRequired()
                .HasColumnName("Idade");

            builder
                .Property(p => p.Series)
                .HasColumnName("Serie");

            builder
                .Property(p => p.AverageGrade)
                .HasColumnName("NotaMedia");

            builder
                .Property(p => p.Address)
                .HasColumnType("varchar(Max)")
                .HasColumnName("Endereco");

            builder
                .Property(p => p.FatherName)
                .HasColumnType("varchar(256)")
                .HasColumnName("NomePai");

            builder
                .Property(p => p.MotherName)
                .HasColumnType("varchar(256)")
                .HasColumnName("NomeMae");

            builder
                .Property(p => p.DateBirth)
                .IsRequired()
                .HasColumnName("DataNascimento");
        }
    }
}
