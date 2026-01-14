using System.ComponentModel.DataAnnotations;

namespace CSCSProofOfConcept.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public bool IsActive { get; set; }

        [Required]
        public string Specifications { get; set; }
        public DistributionCenter? DistributionCenter { get; set; }
    }
}
