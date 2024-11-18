// vi skapar denna klassen för att centralisera alla priser till ett och samma ställe. Allt som har med priser hanteras här.

public class PriceHandler
{
    public static double CalculateNewPrice(Merchandise merchandise)
    {
            Random random = new Random();

            double randomizedValue = random.NextDouble() * (merchandise.VolatilityNumHigh - merchandise.VolatilityNumLow) + merchandise.VolatilityNumLow;

            double merchandiseNewValue = merchandise.Value * randomizedValue;

            merchandise.Value = (int)merchandiseNewValue;  

            return merchandiseNewValue;    
    }

    // public static void SetNewPrice(Merchandise merchandise)
    // {
    //     merchandise.Value = CalculateNewPrice(merchandise);
    // }

    // public static void SettNewPrice(Player player,Merchant merchant)
    // {
    //     foreach (var metal in player.PlayerInventory)
    //     {
    //         foreach (var metall in merchant.ItemsForSale)
    //         {
    //             metal.Value = CalculateNewPrice(metal);
    //         }
    //     }
    // }
}