using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(employee => employee.Id);

            builder.Property(employee => employee.Nombre).IsRequired();
            
            builder.Property(employee => employee.ApellidoPaterno).IsRequired();
            
            builder.Property(employee => employee.ApellidoMaterno).IsRequired();
            
            builder.Property(employee => employee.FechaNacimiento).IsRequired();
            
            builder.Property(employee => employee.Genero).IsRequired();
            
            builder.Property(employee => employee.EstadoCivil).IsRequired();
            
            builder.Property(employee => employee.Rfc).IsRequired();
            
            builder.Property(employee => employee.Direccion).IsRequired();

            builder.Property(employee => employee.Email).IsRequired();

            builder.Property(employee => employee.Telefono).IsRequired();

            builder.Property(employee => employee.IdPuesto).IsRequired();

            builder.Property(employee => employee.FechaAlta).IsRequired();

            builder.HasOne(a => a.Position)
                .WithMany();
        }

    }
}
