namespace StreetFinder.Code
{
    public interface IMapService
    {
        Task<string> GetLatAndLongAsync(StreetRecord record);
    }
}
