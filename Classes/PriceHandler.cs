// vi skapar denna klassen för att centralisera alla priser till ett och samma ställe. Allt som har med priser hanteras här.

public class PriceHandler
{
    
        Merchandise Gold = new Merchandise("Guld", 200, 0.95, 1.05, "-5% - +5%", 10, 0);
        Merchandise Silver = new Merchandise("Silver", 180, 0.93, 1.07, "-7% - +7%", 10, 0);
        Merchandise Bronze = new Merchandise("Brons", 70, 0.85, 1.15, "-15% - +15%", 10, 0);
        Merchandise Copper = new Merchandise("Koppar", 120, 0.85, 1.15, "-15% - +15%", 10, 0);
        Merchandise Platinum = new Merchandise("Platinum", 300, 0.92, 1.08, "-8% - +8%", 10, 0);
        Merchandise Palladium = new Merchandise("Palladium", 250, 0.9, 1.1, "-10% - +10%", 10, 0);
        Merchandise Indium = new Merchandise("Indium", 150, 0.7, 1.3, "-30% - +30%", 10, 0);
        Merchandise Tin = new Merchandise("Tenn", 100, 0.85, 1.15, "-15% - +15%", 10, 0);
    

    public static double CalculateNewPrice(Merchandise merchandise)
    {
            Random random = new Random();

            double randomizedValue = random.NextDouble() * (merchandise.VolatilityNumHigh - merchandise.VolatilityNumLow) + merchandise.VolatilityNumLow;

            double merchandiseNewValue = merchandise.Value * randomizedValue;

            merchandise.Value = merchandiseNewValue;  

            return merchandiseNewValue;    
    }

    public static void SetNewPrice(Merchandise merchandise)
    {
        merchandise.Value = CalculateNewPrice(merchandise);
    }

    public static void SettNewPrice(Player player,Merchant merchant)
    {
        foreach (var metal in player.PlayerInventory)
        {
            foreach (var metall in merchant.ItemsForSale)
            {
                metal.Value = CalculateNewPrice(metal);
            }
        }
    }








}