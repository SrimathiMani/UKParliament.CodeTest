using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
        [JsonConverter(typeof(StringEnumConverter))]
        public GenderEnum Gender { get; set; }
    }

    public enum GenderEnum
    {
        Male = 1,
        Female = 2
    }

    public class EnumValue
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    public static class EnumExtensions
    {
        public static List<EnumValue> GetValues<T>()
        {
            List<EnumValue> values = new List<EnumValue>();
            foreach (var itemType in Enum.GetValues(typeof(T)))
            {
                //For each value of this enumeration, add a new EnumValue instance
                values.Add(new EnumValue()
                {
                    Name = Enum.GetName(typeof(T), itemType),
                    Value = (int)itemType
                });
            }
            return values;
        }
    }
}