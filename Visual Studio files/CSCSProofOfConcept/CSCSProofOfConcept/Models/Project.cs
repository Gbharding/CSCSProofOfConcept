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
        [Display(Name = "Distribution Center")]
        public DistributionCenter? DistributionCenter { get; set; }

        [Display(Name = "Type")]
        public PrjType PrjType { get; set; }

        [Display(Name = "Stage")]
        public int PrjStage { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Due")]
        public DateTime? DueDate { get; set; }
        [Display(Name = "Updated Specifications")]
        public string? NewSpec { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Price")]
        public double? PrjPrice { get; set; }
        [Display(Name = "Freight Strategy")]
        public string? FreightStrat { get; set; }
        public string? History { get; set; }
    }
}
