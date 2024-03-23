namespace Application;

public interface IRandomGenerator
{
    int Next(int minValue, int maxValue);
}

public class RandomGenerator : IRandomGenerator
{
    private readonly Random random;

    public RandomGenerator()
    {
        random = new Random(DateTime.Now.Millisecond);
    }

    public int Next(int minValue, int maxValue)
    {
        return random.Next(minValue, maxValue);
    }
}