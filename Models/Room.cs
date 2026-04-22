using System.ComponentModel.DataAnnotations;

namespace APBD5.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [Required]
        [StringLength(10)]
        public required string BuildingCode { get; set; }

        [Range(-5, 100)]
        public int Floor { get; set; }

        [Range(1, int.MaxValue)]
        public int Capacity { get; set; }

        public bool HasProjector { get; set; }

        public bool IsActive { get; set; }
    }
}

