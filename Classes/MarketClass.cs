using System.Runtime.InteropServices;
using MarketMaster1.Classes;
using System;
using System.Security.Cryptography.X509Certificates;

public class Market
{
    public int Height { get; set; }
    public int Width { get; set; }
    private string[,] market;
    public int[,] array = new int[3, 10];

    public Market(int height, int width)
    {
        Height = height;
        Width = width;

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

    public static void PlaceMerchantsBuildings(int xPos, int yPos)
    {
        Console.SetCursorPosition(xPos, yPos);
        Console.Write("  _________ ");
        Console.SetCursorPosition(xPos, yPos + 1);
        Console.Write(" /    /    \\");
        Console.SetCursorPosition(xPos, yPos + 2);
        Console.Write("/____/______\\");
        Console.SetCursorPosition(xPos, yPos + 3);
        Console.Write("|            ║");

        Console.SetCursorPosition(xPos, yPos + 6);
        Console.Write("_____________║");


    }

    public static void PlacePlayerHome(int xPos, int yPos)
    {
        Console.SetCursorPosition(xPos, yPos);
        Console.Write("║ _________ ");
        Console.SetCursorPosition(xPos, yPos + 1);
        Console.Write("║/    /    \\");
        Console.SetCursorPosition(xPos, yPos + 2);
        Console.Write("║____/______\\");
        Console.SetCursorPosition(xPos, yPos + 3);
        Console.Write("║           |");
        Console.SetCursorPosition(xPos, yPos + 4);
        Console.SetCursorPosition(xPos, yPos + 6);
        Console.Write("║____________");

    }
    public static void HousePositions()
    {

    }

    public static void PlaceDecoration(int xPos, int yPos)
    {
        Console.SetCursorPosition(xPos, yPos);
        for (int x = 0; x < 30; x++)
        {
            Console.Write("🪙");
        }
    }
    public static void DisplayInfo(Market market) // skriver ut spelets regler samt vilken position det ska skrivas ut på.
    {
        HelpClass.AdjustTextToTheBottom(0);
        System.Console.WriteLine("Spelets regler:");
        System.Console.WriteLine();
        DisplayRules(market);
    }

    public static void DisplayRules(Market market) //Lista med regler och info om kommandon som skrivs ut i DisplayInfo metoden.
    {
        string[] ruleArray = {"Spelet pågår i 10 rundor...",
        "* Mellan varje runda kommer priset för varje metall slumpas...",
        "* Vissa metaller svänger mer än andra i pris.",
        "* För att gå till nästa runda kan du alltid gå till sängen.",
        "  Då sover marknaden och nästa runda startar efter en kort stund..."};


        string[] commandoArray = {
        "Kommandon:",
        "1 = Guldgraf,",
        "2 = Silvergraf",
        "3 = Bronsgraf",
        "4 = Koppargraf",
        "5 = Platinumgraf",
        "6 = Palladiumgraf",
        "7 = Indiumgraf",
        "8 = Tingraf",
        "i = inventory",
        "p = kontobalans",
        "esc = avsluta spelet"};

        //Skriver ut listan med en foreach-loop och bestämmer på vilka rader de ska skrivas ut.
        int j = market.Height + 1;
        for (int i = 0; i < ruleArray.Length; i++)
        {
            Console.SetCursorPosition(0, j);
            Console.BackgroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine(ruleArray[i]);
            j++;
        }

        int y = market.Height + 7;
        foreach (var c in commandoArray)
        {
            Console.SetCursorPosition(0, y);
            Console.BackgroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine(c);
            y++;
        }
    }

    //Kollisionsmetod som kontrollerar att användaren inte kan gå på hus, handlare eller säng. samt inte kunna gå utanför spelplanen.
    public static bool IsCollision(int newX, int newY)
    {
        if (newX >= 0 && newX <= 5 && newY >= 13 && newY <= 18 && !(newX == 0 && newY == 17))
        {
            return true; // Kollision med spelarens hus
        }
        if (newX >= 70 && newX <= 78 && newY >= 1 && newY <= 8 && !(newX == 70 && newY == 5))
        {
            return true; // Kollision med trollkarlens hus
        }
        if (newX >= 70 && newX <= 78 && newY >= 13 && newY <= 20 && !(newX == 65 && newY == 17) && !(newX == 65 && newY == 19))
        {
            return true; // Kollision med gubbens hus
        }
        return false; // Ingen kollision
    }
}
