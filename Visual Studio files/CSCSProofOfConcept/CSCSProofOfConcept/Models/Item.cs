using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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
        public DistributionCenter? DistributionCenter { get; set; } 
    }
}
