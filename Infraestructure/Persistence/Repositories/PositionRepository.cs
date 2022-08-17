using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal sealed class PositionRepository : IPositionRepository
    {
        private readonly RepositoryDbContext _dbContext;

        public PositionRepository(RepositoryDbContext dbContext) => _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public async Task<IEnumerable<Position>> GetAllAsync()
        {
            return await _dbContext.Positions.ToListAsync();
        }

        public async Task<Position> GetByIdAsync(Guid positionId)
        {
            return await _dbContext.Positions.FirstOrDefaultAsync(x => x.IdPuesto == positionId);
        }

        public void Insert(Position position)
        {
            _dbContext.Positions.Add(position);
        }

        public void Remove(Position position)
        {
            _dbContext.Positions.Remove(position);
        }
    }
}
