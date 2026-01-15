using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSCSProofOfConcept.Models
{
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public bool IsActive { get; set; }

        [Required]
        public string Specifications { get; set; }

        public int DistributionCenterId { get; set; }
        public DistributionCenter DistributionCenter { get; set; } //When this isn't nullable, it won't save but when it is it won't populate it
    }
}
