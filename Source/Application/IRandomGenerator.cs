namespace Application;

public interface IRandomGenerator
{
    int Next(int minValue, int maxValue);
}

public class RandomGenerator : IRandomGenerator
{
    // Random.Shared is thread-safe; a plain Random instance is not, and this
    // class is registered as a singleton used by concurrent requests
    public int Next(int minValue, int maxValue)
    {
        return Random.Shared.Next(minValue, maxValue);
    }
}