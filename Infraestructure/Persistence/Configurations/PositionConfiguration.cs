using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class PositionConfiguration
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.HasKey(position => position.IdPuesto);

            builder.Property(employee => employee.Nombre).IsRequired();
        }
    }
}
