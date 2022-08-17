using AutoMapper;
using Contracts;
using Domain.Entities;
using Domain.Exceptions.Employee;
using Domain.Exceptions.Position;
using Domain.Repositories;
using Services.Abstractions;

namespace Services
{
    internal sealed class PositionServices :IPositionService
    {
        private readonly IRepositoryManager _repositoryManager;

        private readonly IMapper _mapper;

        public PositionServices(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<PositionDTO>> GetAllAsync()
        {
            var position = await _repositoryManager.PositionRepository.GetAllAsync();

            var positionDto = _mapper.Map<IEnumerable<PositionDTO>>(position); 

            return positionDto;
        }

        public async Task<PositionDTO> GetByIdAsync(Guid positionId)
        {
            var position = await _repositoryManager.PositionRepository.GetByIdAsync(positionId);

            if (position is null)
            {
                throw new PositionNotFoundException(positionId);
            }

            var positionDto = _mapper.Map<PositionDTO>(position);

            return positionDto;
        }

        public async Task<PositionDTO> CreateAsync(PositionForPersitenceDto positionForCreationDto)
        {
            if (positionForCreationDto is null)
            {
                throw new PositionDataEmpty();
            }

            var position = _mapper.Map<Position>(positionForCreationDto);

            _repositoryManager.PositionRepository.Insert(position);

            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<PositionDTO>(position);
        }

        public async Task UpdateAsync(Guid positionId, PositionForPersitenceDto positionForUpdateDto)
        {
            if (positionForUpdateDto is null)
            {
                throw new EmployeeNotFoundException(positionId);
            }

            var position = await _repositoryManager.PositionRepository.GetByIdAsync(positionId);

            position.Nombre = positionForUpdateDto.Nombre;

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }
    }
}
