using System.ComponentModel.DataAnnotations;

namespace UKParliament.CodeTest.Data
{
    public class Person
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = false)]
        public string? Title { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = false)]
        [RegularExpression(@"^[a-zA-Z\s]{2,50}$", ErrorMessage = "Please enter a valid first Name")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = false)]
        [RegularExpression(@"^[a-zA-Z\s]{2,50}$", ErrorMessage = "Please enter a valid first Name")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = false)]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = false)]
        public string? Gender { get; set; }
    }
}