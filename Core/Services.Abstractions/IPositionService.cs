using Contracts;

namespace Services.Abstractions
{
    public  interface IPositionService
    {
        Task<IEnumerable<PositionDTO>> GetAllAsync();

        Task<PositionDTO> GetByIdAsync(Guid positionId);

        Task<PositionDTO> CreateAsync(PositionForPersitenceDto positionForCreationDto);

        Task UpdateAsync(Guid positionId, PositionForPersitenceDto positionForUpdateDto);

    }
}
