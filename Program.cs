﻿using System;

using System.Media;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using MarketMaster1.Classes;


public class Program
{
    public static List<Merchandise> ItemsForDisplay = new List<Merchandise>();
    public static void Main()
    {

        // Skapa karaktärer
        Character TheWealthyBuyer = new Character("The Wealthy (but dumb) Buyer", 1500);
        Character TheSkillfulNegotiator = new Character("The Skillful Negotiator", 700);
        Character TheBalancedTrader = new Character("The Balanced Trader", 1000);

        // Introduktion
        Console.Clear();
        MenuClass.StartMenu();
        NewDayLoop();
        //Skapar två olika handlare som säljer olika metaller



        Thread.Sleep(1800);


    }

    //Kod för meddelande som visas efter ny påbörjad dag.
    //slumpar fram ett av följande meddelanden beroende på om personen vill gå till marknaden idag eller inte.
    //Även en loop inlagd så att det blir en ny dag.
    public static void NewDayLoop()
    {
        Random random = new Random();
        int NumberOfRounds = 10;
        for (int day = 1; day <= NumberOfRounds; day++)
        {
            Console.Clear();

            System.Console.WriteLine($"======================= DAG {day} =======================");
            System.Console.WriteLine("Vill du gå till marknaden idag eller inte tro?\nSkriv ja eller nej. Små bokstäver.");
            //Ändrar om inmatningen till små bokstäver för att minska redundans.
            string answer = Console.ReadLine().ToLower();
            if (answer == "ja")
            {
                string[] yesMessage = {
                    "Perfekt, solen skiner och marknaden väntar på dig! Nu är det dags att göra några riktigt smarta affärer!",
                    "Modigt val! Marknadens dörrar öppnas för dig, och spänningen av köp och sälj ligger i luften. Låt oss se vad du kan göra!",
                    "Du hör hur marknadens sorl växer när du närmar dig. Handlarna är redo att förhandla, och guldet lockar. Lycka till!",
                    "Fantastiskt! Marknaden är full av möjligheter, och det är upp till dig att gripa dem. Låt äventyret börja!",
                    "Marknaden öppnar upp som en färgstark värld fylld av ljud och dofter. Du känner adrenalinet pumpa - det är dags för handel!"
                };

                System.Console.WriteLine(yesMessage[random.Next(yesMessage.Length)]);
                GameLoop();


            }
            else if (answer == "nej")
            {
                string[] noMessage = {
                    "Kanske är det bäst att ta en lugn dag. Vem vet, marknaden är en riskabel plats och ibland är det bättre att hålla pengarna i fickan.",
                    "Ett klokt beslut, alla dagar behöver inte innebära äventyr. En dag att vila kan vara precis vad du behöver.",
                    "Du ser mot marknaden, men något säger dig att idag inte är dagen. Du vänder tillbaka för att samla dina tankar inför framtida affärer.",
                    "Att inte gå till marknaden kan ibland vara det smartaste draget av alla. Ingen risk idag, bara trygghet. Imorgon kan vara din dag.",
                    "Du väljer att stanna borta från marknadens tumult. Lugnet idag kan ge dig fördelar när du nästa gång kliver in i handelsvärlden."
            };
                System.Console.WriteLine(noMessage[random.Next(noMessage.Length)]);
                System.Console.WriteLine("Press [Enter] för att gå vidare till nästa dag...");
                System.Console.ReadKey();
                //continue för att gå vidare till nästa dag.
                continue;
            }
            else
            {
                System.Console.WriteLine("Ogiltigt svar. Skriv 'ja' eller 'nej'");
                //Här lägger vi en 'day--' för att inte gå vidare till nästa dag om användaren skrivit fel val.
                day--;
                continue;
            }
        }
        System.Console.WriteLine("Nu är alla spelrundor slut! Hur bra gick det för dig? Press [Enter] för att få ditt slutgiltiga inventory, sedan [Enter] igen för att avsluta spelet!");
        System.Console.ReadKey();
        System.Console.Clear();
        Character.DisplayPlayerInventory();
        System.Console.ReadKey();





    }
    //Metod för att få skrivmaskinseffekten på text. dvs en bokstav(char) i taget med 45 millisekunders mellanrum mellan varje utskrift.

    public static void GameLoop()
    {
        // skapar en instans av Market som heter market
        Market market = new Market(27, 80);

        // Skapa handlare
        Merchant StableMetalMerchant = new Merchant("Stable metal merchant", 10, 10, 10000);
        Merchant VolatileMetalMerchant = new Merchant("Volatile metal merchant", 20, 20, 10000);
        //Skapa player character samt ger den en position på spelbrädet.
        Character character = new Character("Busiga investeraren", 1000);
        int posX = 2, posY = 2;
        int previousPosX = posX, previousPosY = posY;

        // Skapa marknad och metaller


        Merchandise Gold = new Merchandise("Gold", 200, 0.95, 1.05, "-5% - +5%", 10);
        Merchandise Silver = new Merchandise("Silver", 180, 0.93, 1.07, "-7% - +7%", 10);
        Merchandise Bronze = new Merchandise("Bronze", 70, 0.85, 1.15, "-15% - +15%", 10);
        Merchandise Copper = new Merchandise("Copper", 120, 0.85, 1.15, "-15% - +15%", 10);
        Merchandise Platinum = new Merchandise("Platinum", 300, 0.92, 1.08, "-8% - +8%", 10);
        Merchandise Palladium = new Merchandise("Palladium", 250, 0.9, 1.1, "-10% - +10%", 10);
        Merchandise Indium = new Merchandise("Indium", 150, 0.7, 1.3, "-30% - +30%", 10);
        Merchandise Tin = new Merchandise("Tin", 100, 0.85, 1.15, "-15% - +15%", 10);

        // Lägg till metaller till handlare
        StableMetalMerchant.ItemsForSale.Add(Gold);
        StableMetalMerchant.ItemsForSale.Add(Silver);
        VolatileMetalMerchant.ItemsForSale.Add(Bronze);
        VolatileMetalMerchant.ItemsForSale.Add(Copper);
        StableMetalMerchant.ItemsForSale.Add(Platinum);
        StableMetalMerchant.ItemsForSale.Add(Palladium);
        VolatileMetalMerchant.ItemsForSale.Add(Indium);
        VolatileMetalMerchant.ItemsForSale.Add(Tin);

        //Skriv ut spelplanen:
        DrawGameBoard(market);

        Market.DisplayInfo();

        // detta sätter muspekaren på olika platser varje varv i loopen efter det uppdateras nedan (posX++, posY++ osv.)
        Console.SetCursorPosition(posX, posY);
        System.Console.WriteLine("🧑");

        //SpelKaraktärens Hus
        Market.PlaceCharacterHome(0, 13);

        // Trollkarlen
        Market.PlaceMerchantsBuildings(66, 1);


        // Gubben
        Market.PlaceMerchantsBuildings(66, 13);


        // Market.PlaceDecoration(11, 38);



        // Målar ut försäljare av volatila metaller
        Console.SetCursorPosition(72, 5);
        System.Console.WriteLine("🧙‍♂️");

        // Målar ut försäljare av stabila metaller
        Console.SetCursorPosition(72, 17);
        System.Console.WriteLine("👴");

        Console.SetCursorPosition(2, 17);
        System.Console.WriteLine("🛏️");

        // Målar ut ett frågetecken där spelaren kan läsa om varje metall
        Console.SetCursorPosition(70, 22);
        System.Console.WriteLine("❓");

            // Spelloopen
    while (true)
    {
        // Läs tangenttryckning
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);

        // Hantera rörelser
        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow:
                if (posY > 1 && !Market.IsCollision(posX, posY - 1)) posY--;
                break;
            case ConsoleKey.DownArrow:
                if (posY < market.Height - 2 && !Market.IsCollision(posX, posY + 1)) posY++;
                break;
            case ConsoleKey.LeftArrow:
                if (posX > 1 && !Market.IsCollision(posX - 1, posY)) posX--;
                break;
            case ConsoleKey.RightArrow:
                if (posX < market.Width - 2 && !Market.IsCollision(posX + 1, posY)) posX++;
                break;
            case ConsoleKey.I:
                Character.DisplayPlayerInventory();
                System.Console.WriteLine();
                System.Console.ReadKey();
                break;
            case ConsoleKey.Escape:
                Environment.Exit(0);
                return;
        }

        // Endast uppdatera om spelaren flyttat sig
        if (posX != previousPosX || posY != previousPosY)
        {
            // Rensa den tidigare positionen
            Console.SetCursorPosition(previousPosX, previousPosY);
            Console.Write(" ");

            // Rita ut spelaren på den nya positionen
            Console.SetCursorPosition(posX, posY);
            Console.Write("🧑");

            // Uppdatera tidigare position
            previousPosX = posX;
            previousPosY = posY;
        }
                // Kontrollera om spelaren är nära någon av handlarna för att möjliggöra köp
        if (Math.Abs(posX - 70) < 2 && Math.Abs(posY - 5) <= 1)
        {
            VolatileMetalMerchant.Sell();
        }

        if (Math.Abs(posX - 70) < 2 && Math.Abs(posY - 17) <= 1)
        {
            StableMetalMerchant.Sell();
        }
        
    }
}


    public static void TypeWrite(string text, int delay = 45)
    {
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(delay);
        }
    }

    private static void DrawGameBoard(Market market)
    {   Console.Clear();
        // Övre kant
        Console.Write("╔");
        for (int x = 0; x < market.Width - 2; x++) Console.Write("═");
        Console.WriteLine("╗");

        // Sidor
        for (int z = 0; z < market.Height - 2; z++)
        {
            Console.Write("║");
            for (int x = 0; x < market.Width - 2; x++) Console.Write(" ");
            Console.WriteLine("║");
        }

        // Nedre kant
        Console.Write("╚");
        for (int x = 0; x < market.Width - 2; x++) Console.Write("═");
        Console.WriteLine("╝");
    }

    public void CheckFault()
    {

    }
}









// nedanstående ligger här för att snabbt kunna se vad bredden och höjden är på fönstret och buffern 

// int bufferHeight = Console.BufferHeight;
// int bufferWidth = Console.BufferWidth;
// int windowHeight = Console.WindowHeight;
// int windowWidth = Console.WindowWidth;

// System.Console.WriteLine($"{bufferHeight} {bufferWidth} {windowHeight} {windowWidth}");
