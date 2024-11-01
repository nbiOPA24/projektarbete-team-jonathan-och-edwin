public class Market
{
    public int Height { get; set; }
    public int Width { get; set; }
    private string[,] market;

    public Market(int height, int width)
    {
        Height = height;
        Width = width;

    }

    public void DisplayMarket()
    {

    }

    public void PlaceMerchant(int xPos, int yPos, string symbol)
    {
        if (xPos >= 0 && xPos < Width && yPos >= 0 && yPos < Height)
        {
            market[yPos, xPos] = symbol; // Placera handlarens symbol på marknaden
        }
    }

    public int RandomizeNumberOfRounds()
    {
        Random random = new Random();
        int numberOfRounds = random.Next(10, 21);
        return numberOfRounds;
    }

    public static void PlaceMerchantsBuilding(int xPos, int yPos)
    {
        Console.SetCursorPosition(xPos, yPos);
        for (int x = 0; x < 12; x++)
        {
            Console.Write("-");
        }
    }

    public static void PlaceDecoration(int xPos, int yPos)
    {
        Console.SetCursorPosition(xPos, yPos);
        for (int x = 0; x < 30; x++)
        {
            Console.Write("🪙");
        }
    }

   
}
