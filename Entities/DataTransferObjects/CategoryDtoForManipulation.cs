using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public abstract record CategoryDtoForManipulation
    {
        [Required(ErrorMessage = "Category name is a required field.")]
        public String CategoryName { get; init; }
    }
}
