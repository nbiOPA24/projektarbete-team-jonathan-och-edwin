// Jonathan jobbar här
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
public class Merchandise
{
    public string Name { get; set; }
    public int Value { get; set; }
    public double VolatilityNumLow { get; set; } // Används för att räkna ut nytt pris på valfri ädelmetall, detta används som den lägre siffran för att räkna ut procent
    public double VolatilityNumHigh { get; set; } // Används för att räkna ut nytt pris på valfri ädelmetall, detta används som den högre siffran för att räkna ut procent
    public string VolatilityInAString { get; set; } // för att visa användaren hur mycket/lite en metall svänger i pris
    public int AmountAvailableAtMerchant { get; set; }
    public int QuantityInPlayerInventory { get; set; }
    public static int xOnGraph = 88;
    public static int lastXOnGraph = xOnGraph - 5;

    public int yOnGraph = 24;
    public static int index = 0;
    public List<int> PriceHistory { get; set; } = new List<int>(); // tänker att vi vill kapsla in denna lista, så att listan endast kan modifieras inifrån Merchandise klassen, men läsas

    //Konstruktor för Varor/Metaller.
    public Merchandise(string name, int value, double volatilityNumLow, double volatilityNumHigh, string volatilityInAString, int amountAvailableAtMerchant, int quantityInPlayerInventory)
    {
        Name = name;
        Value = value;
        VolatilityNumLow = volatilityNumLow;
        VolatilityNumHigh = volatilityNumHigh;
        VolatilityInAString = volatilityInAString;
        AmountAvailableAtMerchant = amountAvailableAtMerchant;
        QuantityInPlayerInventory = quantityInPlayerInventory;
    }

    public void DisplayPriceGraph(Merchandise merchandise, Market market, Player player)
    {
        SetPriceOnGraph(merchandise);

        HelpClass.AdjustTextToTheRight(25);
        System.Console.WriteLine(" PRIS");

        for (int i = 135; i < 148; i++)
        {
            Console.SetCursorPosition(i, 2);
            Console.Write("_");
        }
        Console.SetCursorPosition(market.Width + 55, 1);
        System.Console.WriteLine($"Metall: {Name}");

        int x = market.Width + 7;
        for (int i = 1; i < market.Height - 1; i++)
        {
            Console.SetCursorPosition(x, i);
            System.Console.WriteLine("|");
            System.Console.WriteLine();
        }

        for (int i = 0; i < 63; i++)
        {
            Console.SetCursorPosition(x, market.Height - 1);
            Console.Write("_");
            x++;
        }

        Console.SetCursorPosition(market.Width + 7, 27);
        System.Console.WriteLine("DAGAR  1     2     3     4     5     6     7     8     9     10");
    }


    private int CalculateYPosition(int price)
    {
        int maxY = 10; // Högsta punkten på grafen
        int minY = 24; // Lägsta punkten på grafen

        int maxPrice = PriceHistory.Max();
        int minPrice = PriceHistory.Min();

        if (maxPrice == minPrice)
        {
            // Alla priser är lika, placera i mitten
            return (maxY + minY) / 2;
        }

        // Normalisera priset till ett värde mellan 0 och 1
        double normalizedPrice = (double)(price - minPrice) / (maxPrice - minPrice);

        // Mappa det normaliserade priset till y-skalan
        int yPosition = minY - (int)(normalizedPrice * (minY - maxY));

        return yPosition;
    }

    public void SetPriceOnGraph(Merchandise merchandise)
    {
        
        foreach (var p in merchandise.PriceHistory)
        {
            int y = CalculateYPosition(merchandise.Value);

            Console.SetCursorPosition(xOnGraph + 5, y);
            System.Console.WriteLine("❗");

            Console.SetCursorPosition(xOnGraph + 5, y + 1);
            System.Console.WriteLine(merchandise.PriceHistory[index]);
        }
    }

    public void UpdatePriceHistory(int price)
    {
        PriceHistory.Add(price);
    }

    // Såhär representeras en ädelmetall när vi "skriver ut" klassen
    public override string ToString()
    {
        return $"Metall: {Name}\nVärde: {Value}\nMängd hos handlare: {AmountAvailableAtMerchant}\nMängd i spelarens inventory: {QuantityInPlayerInventory} st.";
    }
}
