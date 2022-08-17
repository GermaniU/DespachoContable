using Contracts;

namespace DespachoContable.Services.Position
{
    public interface IPositionServices
    {
        Task<IEnumerable<PositionDTO>> GetAllAsync();
    }
}