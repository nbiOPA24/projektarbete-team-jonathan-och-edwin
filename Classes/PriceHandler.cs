// vi skapar denna klassen för att centralisera alla priser till ett och samma ställe. Allt som har med priser hanteras här.

public class PriceHandler
{
    public static double CalculateNewPrice(Merchandise merchandise)
    {
            Random random = new Random();

            double randomizedValue = random.NextDouble() * (merchandise.VolatilityNumHigh - merchandise.VolatilityNumLow) + merchandise.VolatilityNumLow;

            double merchandiseNewValue = merchandise.Value * randomizedValue;

            merchandise.Value = (int)merchandiseNewValue;

            merchandise.PriceHistory.Add((int)merchandiseNewValue); // en lista som lagrar gamla värden på metaller, för att kunna skapa en graf

            return merchandiseNewValue;
    }  
}
  