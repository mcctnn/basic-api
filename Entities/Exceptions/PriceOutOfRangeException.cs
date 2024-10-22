namespace Entities.Exceptions
{
    public class PriceOutOfRangeException : BadRequestException
    {
        public PriceOutOfRangeException() : 
            base("Max price less than 1000 and greater than 10")
        {
        }
    }
}
