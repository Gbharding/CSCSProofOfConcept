namespace CSCSProofOfConcept.Data
{
    public static class CacheData
    {
        //Stores the current user as a string
        static string _user;
        public static string User
        {
            get { return _user; } 
            set { _user = value; }
        }

        //Stores the choice from the create page for an in-progress Project
        static int _inProgPrj;
        public static int InProgPrj
        {
            get { return _inProgPrj; }
            set { _inProgPrj = value; }
        }

        //prj state arrays
        static string[] _states = { "Waiting on Logistics for Freight Requirements", 
                                    "Needs Supplier to submit to Logistics", //New items only
                                    "Waiting on Logistics approval", //New items only
                                    "Waiting on Pricing approval", 
                                    "Waiting on CM for DC connections", //New and transition
                                    "Needs SSM Approval", 
                                    "In Ramp-up", //New only 
                                    "In Runout", //Discontinue only 
                                    "Waiting on CM to end pricing",
                                    "Finished" };
        public static string[] States
        {
            get { return _states; } 
        }

        static string[] _stageRequirements = { "Log",
                                               "Supp", //New items only
                                               "Log", //New items only
                                               "Price",
                                               "CM", //New and transition
                                               "SSM",
                                               "SSM", //New only 
                                               "SSM", //Discontinue only 
                                               "CM",
                                               "Done" };
        public static string[] StageRequirements
        {
            get { return _stageRequirements; }
        }
    }
}
