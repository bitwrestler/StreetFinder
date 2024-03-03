
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

                public override bool Equals(object? obj)
                {
                    return obj is positionObj obj2 &&
                           lat == obj2.lat &&
                           lon == obj2.lon;
                }

                public override int GetHashCode()
                {
                    return HashCode.Combine(lat, lon);
                }

                public override string ToString()
                {
                    return $"{lat}+{lon}";
                }

                public static bool operator ==(positionObj? left, positionObj? right)
                {
                    return EqualityComparer<positionObj>.Default.Equals(left, right);
                }

                public static bool operator !=(positionObj? left, positionObj? right)
                {
                    return !(left == right);
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

