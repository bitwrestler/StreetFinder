namespace StreetFinder.Code
{
    public class AzureAddressStruct
    {
        public resultObj[] results {get;set;}
        public summaryObj summary { get;set;}

        public class resultObj
        {
            public positionObj position { get;set;} 

            public class positionObj
            {
                public double lat { get; set; }
                public double lon { get; set; }

                public override string ToString()
                {
                    return $"{lat}+{lon}";
                }
            }
        }

    public class summaryObj
        {
            public int numResults { get; set; }
            public int totalResults { get; set; }
        }

    }
}

