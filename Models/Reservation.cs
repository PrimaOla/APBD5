using System.ComponentModel.DataAnnotations;

namespace APBD5.Models
{
    public class Reservation : IValidatableObject
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue)]
        public int RoomId { get; set; }

        [Required]
        [StringLength(100)]
        public required string OrganizerName { get; set; }

        [Required]
        [StringLength(200)]
        public required string Topic { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        [Required]
        [RegularExpression("^(planned|confirmed|cancelled)$",
            ErrorMessage = "Status must be one of: planned, confirmed, cancelled.")]
        public required string Status { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndTime <= StartTime)
            {
                yield return new ValidationResult(
                    "EndTime must be later than StartTime.",
                    new[] { nameof(EndTime) });
            }
        }
    }
}
