﻿using System;

using System.Media;
using System.Security.Cryptography;
using MarketMaster1.Classes;


public class Program
{
    public static void Main()
    {

        Console.Clear();
        // MenuClass.StartMenu();
        Console.ReadKey();
        Console.Clear();
        NewDayLoop();

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
                System.Console.WriteLine("Tryck [ENTER] för att äntra marknaden...");
                
                Console.ReadKey();
                GameLoop();
                continue;

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
    

    public static void GameLoop()
    {
        // skapar en instans av Market som heter market
        Market market = new Market(25, 80);

        //Skapa player character samt ger den en position på spelbrädet.
        Character character = new Character("Busiga investeraren", 1000);
        int posX = 2; // här startar spelaren (bredd)
        int posY = 2; // här startar spelaren (höjd)

         // Skapa handlare
        Merchant StableMetalMerchant = new Merchant("Stable metal merchant", 10000);
        Merchant VolatileMetalMerchant = new Merchant("Volatile metal merchant", 10000);

        // Skapa marknad och metaller
        Merchandise Gold = new Merchandise("Gold", 200, 0.95, 1.05, "-5% - +5%", 10);
        Merchandise Silver = new Merchandise("Silver", 180, 0.93, 1.07, "-7% - +7%", 10);
        Merchandise Bronze = new Merchandise("Bronze", 70, 0.85, 1.15, "-15% - +15%", 10);
        Merchandise Copper = new Merchandise("Copper", 120, 0.85, 1.15, "-15% - +15%", 10);
        Merchandise Platinum = new Merchandise("Platinum", 300, 0.92, 1.08, "-8% - +8%", 10);
        Merchandise Palladium = new Merchandise("Palladium", 250, 0.9, 1.1, "-10% - +10%", 10);
        Merchandise Indium = new Merchandise("Indium", 150, 0.7, 1.3, "-30% - +30%", 10);
        Merchandise Tin = new Merchandise("Tin", 100, 0.85, 1.15, "-15% - +15%", 10);

        StableMetalMerchant.UpdatePrice(Gold);
        StableMetalMerchant.UpdatePrice(Silver);
        StableMetalMerchant.UpdatePrice(Platinum);
        StableMetalMerchant.UpdatePrice(Palladium);

        VolatileMetalMerchant.UpdatePrice(Bronze);  
        VolatileMetalMerchant.UpdatePrice(Copper);
        VolatileMetalMerchant.UpdatePrice(Indium);
        VolatileMetalMerchant.UpdatePrice(Tin);

        // Lägg till metaller till handlare
        StableMetalMerchant.ItemsForSale.Add(Gold);
        StableMetalMerchant.ItemsForSale.Add(Silver);
        VolatileMetalMerchant.ItemsForSale.Add(Bronze);
        VolatileMetalMerchant.ItemsForSale.Add(Copper);
        StableMetalMerchant.ItemsForSale.Add(Platinum);
        StableMetalMerchant.ItemsForSale.Add(Palladium);
        VolatileMetalMerchant.ItemsForSale.Add(Indium);
        VolatileMetalMerchant.ItemsForSale.Add(Tin);

        Merchant.DisplayDetailedProductInfo(Gold);
        Merchant.DisplayDetailedProductInfo(Silver);
        Merchant.DisplayDetailedProductInfo(Bronze);
        Merchant.DisplayDetailedProductInfo(Copper);
        Merchant.DisplayDetailedProductInfo(Platinum);
        Merchant.DisplayDetailedProductInfo(Palladium);
        Merchant.DisplayDetailedProductInfo(Indium);
        Merchant.DisplayDetailedProductInfo(Tin);

        while (true)
        {
            System.Console.Clear();

            // Ritar ut ramen
            for (int x = 0; x < market.Width; x++)
            {
                Console.Write("-");
            }
            Console.WriteLine();

            for (int z = 0; z < market.Height; z++)
            {
                Console.Write("|");
                System.Console.WriteLine("                                                                              |");
            }

            for (int x = 0; x < market.Width; x++)
            {
                Console.Write("-");
            }

            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine();
            // ritat färdigt ramen
            Market.AdjustTextToTheBottom(0);
            Market.DisplayInfo();

            // detta sätter muspekaren på olika platser varje varv i loopen efter det uppdateras nedan (posX++, posY++ osv.)
            Console.SetCursorPosition(posX, posY);
            System.Console.WriteLine("🧑");

            // Trollkarlen
            Market.PlaceMerchantsBuilding(66, 1);
            Market.PlaceMerchantsBuilding(66, 5);

            // Gubben
            Market.PlaceMerchantsBuilding(66, 13);
            Market.PlaceMerchantsBuilding(66, 17);

            // Målar ut försäljare av volatila metaller
            Console.SetCursorPosition(70, 3);
            System.Console.WriteLine("🧙‍♂️");

            // Målar ut försäljare av stabila metaller
            Console.SetCursorPosition(70, 15);
            System.Console.WriteLine("👴");

            ConsoleKeyInfo keyInfo;
            keyInfo = Console.ReadKey(true); // Console.ReadKey(true) gör här att vi läser in ett ENSKILT tangenttryck från användaren. "true" gör att tangenten som trycks in skrivs ut på skärmen

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow: // Så länge muspekarens Y-värde (lodrätt) är större än 1 får spelaren gå uppåt. Detta kontrollerar att användaren inte går utanför banan uppåt
                    if (posY > 1) posY--;
                    break;

                case ConsoleKey.DownArrow: // Så länge muspekarens Y-värde (lodrätt) är större eller lika med 0 får spelaren gå nedåt. Detta kontrollerar att användaren inte går utanför banan nedåt
                    if (posY >= 0 && posY <= 24) posY++;
                    break;

                case ConsoleKey.RightArrow: // Samma som "DownArrow" fast vågrätt
                    if (posX >= 0 && posX <= 76) posX++;
                    break;

                case ConsoleKey.LeftArrow: // Samma som "UpArrow" fast vågrätt
                    if (posX > 1) posX--;
                    break;

                case ConsoleKey.I:
                    Character.DisplayPlayerInventory();
                    System.Console.WriteLine();
                    System.Console.ReadKey();
                    break;

                case ConsoleKey.P:
                    Character.DisplayAccountBalance(character);
                    Console.ReadKey();
                    break;

                case ConsoleKey.Z:
                    Merchant.DisplayDetailedProductInfo(Gold);
                    break;

                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    return;
            }


            if (Math.Abs(posX - 68) < 1 && Math.Abs(posY - 3) <= 1)
            {
                VolatileMetalMerchant.Sell(character);
            }

            if (Math.Abs(posX - 68) < 1 && Math.Abs(posY - 15) <= 1)
            {
                StableMetalMerchant.Sell(character);
            }

            if (Math.Abs(posX - 35) < 1 && Math.Abs(posY - 15) <= 1)
            {
                MakeTheMarketSleep();
                return;
            }
        }
    }

    public bool IsCollision(int newX, int newY)
    {
        if ((newX == 70 && newY == 3) || (newX == 70 && newY == 28))
        {
            return true;
        }

        return false;
    }

    public static void MakeTheMarketSleep()
    {
        
        Market.AdjustTextToTheRight(0);
        System.Console.WriteLine("Vill du starta nästa dag?");
        Market.AdjustTextToTheRight(1);
        string answer = Console.ReadLine();

        if (answer.ToLower() == "ja" || answer.ToLower() == "yes")
        {
            Console.Clear();
            MenuClass.TypeWrite("Marknaden sover nu...");
            System.Console.WriteLine();
            MenuClass.TypeWrite("Tryck [ENTER] för att starta nästa dag... med nya priser");
            Console.ReadKey();
        }
        else if (answer.ToLower() == "NEJ" || answer.ToLower() == "NO")
        {
            GameLoop();
        }
    }
}









// nedanstående ligger här för att snabbt kunna se vad bredden och höjden är på fönstret och buffern 

// int bufferHeight = Console.BufferHeight;
// int bufferWidth = Console.BufferWidth;
// int windowHeight = Console.WindowHeight;
// int windowWidth = Console.WindowWidth;

// System.Console.WriteLine($"{bufferHeight} {bufferWidth} {windowHeight} {windowWidth}");
