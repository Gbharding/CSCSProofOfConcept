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
        static string[] _states = { "Waiting on Freight Requirements", 
                                    "Needs to be submitted to Logistics", //New items only
                                    "Waiting on Logistics approval", //New items only
                                    "Waiting on Pricing", 
                                    "Waiting for DC connections", //New and transition
                                    "Needs SSM Approval", 
                                    "In Ramp-up", //New only 
                                    "In Runout", //Discontinue only 
                                    "Waiting on CM to end pricing",
                                    "Finished" };
        public static string[] States
        {
            get { return _states; } 
        }
    }
}
