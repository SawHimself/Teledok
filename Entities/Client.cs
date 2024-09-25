using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public enum ClientType
    {
        IE,
        LE
    }

    public class Client
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Type is required.")]
        public ClientType Type { get; set; }

        [Required(ErrorMessage = "TIN is required.")]
        [RegularExpression(@"^\d{12}|\d{10}$", ErrorMessage = "Invalid TIN")]
        public string TIN { get; set; } = null!;

        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime UpdateDate { get; set; }
        public ICollection<Founder>? Founders { get; set; }
    }
}
