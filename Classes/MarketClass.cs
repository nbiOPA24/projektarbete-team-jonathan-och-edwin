using System.Runtime.InteropServices;

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

    // Detta är en metod du kan kalla på var du vill om du vill "högerjustera" texten! Du måste dock slänga in en siffra för att välja vart på y-axeln den ska hamna
    public static void AdjustTextToTheRight(int y)
    {
        Console.SetCursorPosition(90, y);
    }

    public static void AdjustTextToTheBottom(int x)
    {
        Console.SetCursorPosition(x, 27);
    }

    public static void DisplayInfo()
    {
        AdjustTextToTheBottom(0);
        System.Console.WriteLine("Spelets regler:");
        System.Console.WriteLine();
        DisplayRules();



    }

    public static void DisplayRules()
    {
        List<string> ruleList = new List<string>();

        string text1 = "  Spelet pågår i 10 rundor...";
        string text2 = "* Mellan varje runda kommer priset för varje metall slumpas...";
        string text3 = "* Vissa metaller svänger mer än andra i pris.";
        string text4 = "* För att läsa mer om volatiliteten, gå till ❓";
        string text6 = "* För att gå till nästa runda kan du alltid gå till sängen.";
        string text7 = "  Då sover marknaden och nästa runda startar efter en kort stund...";
        ruleList.Add(text1);
        ruleList.Add(text2);
        ruleList.Add(text3);
        ruleList.Add(text4);
        ruleList.Add(text6);
        ruleList.Add(text7);

        int y = 1;

        foreach (var r in ruleList)
        {

            System.Console.WriteLine(r);
            y++;
        }

        Console.SetCursorPosition(0, 45);
        System.Console.WriteLine("KOMMANDON");
        Console.SetCursorPosition(0, 46);
        System.Console.WriteLine("i = öppna inventory");
        Console.SetCursorPosition(0, 47);
        System.Console.WriteLine("p = se din kontobalans");
        Console.SetCursorPosition(0, 48);
        System.Console.WriteLine("esc = avsluta spelet");

    }   
}
