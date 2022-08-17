using Domain.Entities;

namespace Domain.Repositories
{
    public  interface IPositionRepository
    {
        Task<IEnumerable<Position>> GetAllAsync();

        Task<Position> GetByIdAsync(Guid positionId);

        void Insert(Position position);

        void Remove(Position position);
    }
}
