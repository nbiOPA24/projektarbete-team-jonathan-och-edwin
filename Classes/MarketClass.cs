using System.Runtime.InteropServices;
using MarketMaster1.Classes;
using System;
using System.Security.Cryptography.X509Certificates;

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

        Console.SetCursorPosition(xPos, yPos + 5);
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
        Console.SetCursorPosition(xPos, yPos + 5);
        Console.Write("║____________");

    }

    public static void PlaceDecoration(int xPos, int yPos)
    {
        Console.SetCursorPosition(xPos, yPos);
        for (int x = 0; x < 30; x++)
        {
            Console.Write("🪙");
        }



    }




    public static void DisplayInfo() // skriver ut spelets regler samt vilken position det ska skrivas ut på.
    {
        HelpClass.AdjustTextToTheBottom(0);
        System.Console.WriteLine("Spelets regler:");
        System.Console.WriteLine();
        DisplayRules();



    }

    public static void DisplayRules() //Lista med regler och info om kommandon som skrivs ut i DisplayInfo metoden.
    {
        List<string> ruleList = new List<string>();

        string text1 = "  Spelet pågår i 10 rundor...";
        string text2 = "* Mellan varje runda kommer priset för varje metall slumpas...";
        string text3 = "* Vissa metaller svänger mer än andra i pris.";
        string text6 = "* För att gå till nästa runda kan du alltid gå till sängen.";
        string text7 = "  Då sover marknaden och nästa runda startar efter en kort stund...";
        ruleList.Add(text1);
        ruleList.Add(text2);
        ruleList.Add(text3);
        ruleList.Add(text6);
        ruleList.Add(text7);

        int y = 1;

        foreach (var r in ruleList) //Skriver ut listan med en foreach-loop och bestämmer på vilka rader de ska skrivas ut.
        {

            System.Console.WriteLine(r);
            y++;
        }

        Console.SetCursorPosition(0, 39);
        System.Console.WriteLine("KOMMANDON");
        Console.SetCursorPosition(0, 40);
        System.Console.WriteLine("i = öppna inventory");
        Console.SetCursorPosition(0, 41);
        System.Console.WriteLine("p = se din kontobalans");
        Console.SetCursorPosition(0, 42);
        System.Console.WriteLine("esc = avsluta spelet");

    }   
    
    //Kollisionsmetod som kontrollerar att användaren inte kan gå på hus, handlare eller säng. samt inte kunna gå utanför spelplanen.
    public static bool IsCollision(int newX, int newY)
    {
        if ((newX == 70 && newY == 3) || (newX == 70 && newY == 28))
        {
            return true;
        }

        return false;
    }
}
