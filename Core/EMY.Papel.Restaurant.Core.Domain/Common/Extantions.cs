namespace EMY.Papel.Restaurant.Core.Domain.Common
{
    public static class Extantions
    {
        public static Guid ToGuid(this string value)
        {
            return Guid.Parse(value);
        }
    }
}
