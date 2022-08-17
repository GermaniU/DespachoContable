namespace Domain.Exceptions.Position
{
    public class PositionNotFoundException: NotFoundException
    {
        public PositionNotFoundException(Guid positionId)
          : base($"El puesto con el identificador {positionId} no se encuentra.")
        {
        }
    }
}
