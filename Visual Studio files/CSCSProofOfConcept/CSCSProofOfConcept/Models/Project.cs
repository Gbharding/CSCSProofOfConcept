using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CSCSProofOfConcept.Models
{
    public enum PrjType
    {
        New,
        Transition,
        Discontinue
    }
    public class Project
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public int? ItemId { get; set; }
        public Item? Item { get; set; }

        public int? DistributionCenterId { get; set; }
        public DistributionCenter? DistributionCenter { get; set; }

        public PrjType PrjType { get; set; }
        public int PrjStage { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }
        public string? NewSpec { get; set; }

        [DataType(DataType.Currency)]
        public double? PrjPrice { get; set; }
        public string? FreightStrat { get; set; }
        public ICollection<String>? History { get; set; }
    }
}
