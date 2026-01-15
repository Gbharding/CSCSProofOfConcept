using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CSCSProofOfConcept.Models;

namespace CSCSProofOfConcept.Data
{
    public class CSCSProofOfConceptContext : DbContext
    {
        public CSCSProofOfConceptContext (DbContextOptions<CSCSProofOfConceptContext> options)
            : base(options)
        {
        }

        public DbSet<CSCSProofOfConcept.Models.DistributionCenter> DistributionCenter { get; set; } = default!;
        public DbSet<CSCSProofOfConcept.Models.Item> Item { get; set; } = default!;
        public DbSet<CSCSProofOfConcept.Models.Project> Project { get; set; } = default!;
    }
}
