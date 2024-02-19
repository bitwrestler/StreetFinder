namespace StreetFinder.Code
{
    [Serializable]
    public struct ZipCodeRange
    {
        public int Start;
        public int End;
    }

    [Serializable]
    public struct StreetRecord
    {
        public string Name;
        public ZipCodeRange ZipCodeRange;
    }
}
