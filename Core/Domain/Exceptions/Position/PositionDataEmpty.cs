namespace Domain.Exceptions.Position
{
    public class PositionDataEmpty: BadRequestException
    {
        public PositionDataEmpty()
           : base("Ocurrió un error al capturar los datos.")
        {
        }
    }
}
