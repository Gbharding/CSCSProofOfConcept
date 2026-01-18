using CSCSProofOfConcept.Models;

namespace CSCSProofOfConcept.Data
{
    public class SeedData
    {
        public static void Initialize(CSCSProofOfConceptContext context)
        {
            if (context.Project.Any())
            {
                return; //DB has data already
            }

            var MadisonDC = new DistributionCenter
            {
                Name = "Madison DC",
                Address = "15 Verona Ave",
                NumTrucks = 32
            };

            var MilwaukeeDC = new DistributionCenter
            {
                Name = "Milwaukee DC",
                Address = "23 Old Ward Way",
                NumTrucks = 75
            };

            var ChicagoDC = new DistributionCenter
            {
                Name = "Chicago DC",
                Address = "78 Michigan Ave",
                NumTrucks = 431
            };

            var DCs = new DistributionCenter[]
            {
                MadisonDC, MilwaukeeDC, ChicagoDC
            };

            context.AddRange( DCs );

            var ChickenBox = new Item
            {
                Name = "Box of Chicken wings",
                IsActive = true,
                Specifications = "15 / Box",
                DistributionCenter = MilwaukeeDC
            };

            var SalsaJar = new Item
            {
                Name = "Jars of Salsa",
                IsActive = true,
                Specifications = "25 jars / Box",
                DistributionCenter = ChicagoDC
            };

            var Chair = new Item
            {
                Name = "Palette of Chairs",
                IsActive = true,
                Specifications = "20 / Palette",
                DistributionCenter = MadisonDC
            };

            var CheeseSauce = new Item
            {
                Name = "Bags of Cheese Sauce",
                IsActive = true,
                Specifications = "10 / Box",
                DistributionCenter = ChicagoDC
            };

            var items = new Item[]
            {
                ChickenBox,
                SalsaJar,
                Chair,
                CheeseSauce
            };

            context.AddRange( items );

            var MoveChicken = new Project
            {
                Name = "Move Chicken to Madison",
                Description = "Madison needs more chicken!",
                Item = ChickenBox,
                DistributionCenter = MadisonDC,
                PrjType = PrjType.Transition,
                PrjStage = 6,
                DueDate = DateTime.Now.AddDays(1),
                NewSpec = "",
                PrjPrice = 817.55,
                FreightStrat = "We will use trucks"
            };

            var RemoveCheese = new Project
            {
                Name = "Discontinue the cheese bags",
                Description = "No recipes use them anymore",
                Item = CheeseSauce,
                DistributionCenter = ChicagoDC,
                PrjType = PrjType.Discontinue,
                PrjStage = 8,
                DueDate = DateTime.Now,
                NewSpec = "",
                PrjPrice = 473.65,
                FreightStrat = "Disposal"
            };

            var CreateMushrooms = new Project
            {
                Name = "Box of Mushrooms",
                Description = "We need to add mushrooms to Milwaukee!",
                Item = null,
                DistributionCenter = MilwaukeeDC,
                PrjType = PrjType.New,
                PrjStage = 2,
                DueDate = DateTime.Now.AddDays(3),
                NewSpec = "33 / Box",
                PrjPrice = null,
                FreightStrat = "Get them from the farms"
            };

            var projects = new Project[]
            {
                MoveChicken,
                RemoveCheese,
                CreateMushrooms
            };

            context.AddRange(projects);
            context.SaveChanges();
        }
    }
}
