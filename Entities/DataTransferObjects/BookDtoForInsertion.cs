using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    //other way
    //public int Id { get; init; }
    //public string Title { get; init; }
    //public decimal Price { get; init; }
    public record BookDtoForInsertion : BookDtoForManipulation
    {
        [Required(ErrorMessage ="Category Id is required.")]
        public int CategoryId { get; init; }
    }
}
