using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Founder
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "TIN is required.")]
        [StringLength(12, ErrorMessage = "Individual tax number must consist of 12 characters")]
        public string TIN { get; set; } = null!;

        [Required(ErrorMessage = "Full name is required.")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Invalid TIN")]
        public required string FullName { get; set; } = null!;
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime UpdateDate { get; set; }

        [Required(ErrorMessage = "Client id is required.")]
        public int ClientId {  get; set; }
    }
}
