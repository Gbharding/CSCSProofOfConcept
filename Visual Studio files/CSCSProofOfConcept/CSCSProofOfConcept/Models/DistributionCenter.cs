using System.ComponentModel.DataAnnotations;

namespace CSCSProofOfConcept.Models
{
    public class DistributionCenter
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Address { get; set; }
        public int NumTrucks { get; set; }
    }
}
