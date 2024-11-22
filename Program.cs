using System;
using System.ComponentModel;
using System.Media;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using MarketMaster1.Classes;


public class Program
{
    
    private static Player player;
    public static void Main()
    {
        //Tar bort "muspekaren"
        Console.CursorVisible = false;
        
        Console.Clear();
        // MenuClass.StartMenu();
        Console.ReadKey();
        Console.Clear();
        NewDayLoop();

        Thread.Sleep(1800);
    }
    public static void NewDayLoop()
    {
        Random random = new Random(); //Random för att randomisera vilket av 5 meddelanden som ska returneras när man väljer ja eller nej.
        int NumberOfRounds = 10; //Antar dagar som spelet pågår.
        for (int day = 1; day <= NumberOfRounds; day++) // En for-loop som pågår tills rundorna är slut
        {

            Console.Clear();
            System.Console.WriteLine($"======================= DAG {day} =======================");
            System.Console.WriteLine("Vill du gå till marknaden idag eller inte tro?\nSkriv ja eller nej.");

            //Ändrar om inmatningen till små bokstäver för att minska redundans.
            string answer = Console.ReadLine().ToLower();
            if (answer == "ja")
            {
                string[] yesMessage = { //En array med utskrifter som randomiseras vid ja svar.
                    "Perfekt, solen skiner och marknaden väntar på dig! Nu är det dags att göra några riktigt smarta affärer!",
                    "Modigt val! Marknadens dörrar öppnas för dig, och spänningen av köp och sälj ligger i luften. Låt oss se vad du kan göra!",
                    "Du hör hur marknadens sorl växer när du närmar dig. Handlarna är redo att förhandla, och guldet lockar. Lycka till!",
                    "Fantastiskt! Marknaden är full av möjligheter, och det är upp till dig att gripa dem. Låt äventyret börja!",
                    "Marknaden öppnar upp som en färgstark värld fylld av ljud och dofter. Du känner adrenalinet pumpa - det är dags för handel!"
                };

                System.Console.WriteLine(yesMessage[random.Next(yesMessage.Length)]); // Skriver ut ett av meddelandena i Arrayn.
                System.Console.WriteLine("Tryck [ENTER] för att äntra marknaden...");

                Console.ReadKey();

                GameLoop(); //Här skriver vi ut själva game-loopen.
                continue;

            }
            else if (answer == "nej")
            {
                string[] noMessage = { //Array med alla randomiserade svar om användaren skriver nej.
                    "Kanske är det bäst att ta en lugn dag. Vem vet, marknaden är en riskabel plats och ibland är det bättre att hålla pengarna i fickan.",
                    "Ett klokt beslut, alla dagar behöver inte innebära äventyr. En dag att vila kan vara precis vad du behöver.",
                    "Du ser mot marknaden, men något säger dig att idag inte är dagen. Du vänder tillbaka för att samla dina tankar inför framtida affärer.",
                    "Att inte gå till marknaden kan ibland vara det smartaste draget av alla. Ingen risk idag, bara trygghet. Imorgon kan vara din dag.",
                    "Du väljer att stanna borta från marknadens tumult. Lugnet idag kan ge dig fördelar när du nästa gång kliver in i handelsvärlden."
            };
                System.Console.WriteLine(noMessage[random.Next(noMessage.Length)]); //Slumpar fram ett svar ur Arrayn.
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
        //Avslut på spelet. Skriver ut inventoryt så man kan se hur bra det gick för användaren.
        System.Console.WriteLine("Nu är alla spelrundor slut! Hur bra gick det för dig? Press [Enter] för att få ditt slutgiltiga inventory, sedan [Enter] igen för att avsluta spelet!");
        MenuClass.EndGameScreen(player);
        System.Console.ReadKey();
        System.Console.Clear();
        System.Console.ReadKey();

    }


    public static void GameLoop()
    {
        player = HelpClass.LoadFromJson("JsonHandler.json");
                Merchant StableMetalMerchant = new Merchant("Stable metal merchant", 10000);
        Merchant VolatileMetalMerchant = new Merchant("Volatile metal merchant", 10000);

        Merchandise Gold = new Merchandise("Guld", 200, 0.95, 1.05, "-5% - +5%", 10, 0);
        Merchandise Silver = new Merchandise("Silver", 180, 0.93, 1.07, "-7% - +7%", 10, 0);
        Merchandise Bronze = new Merchandise("Brons", 70, 0.85, 1.15, "-15% - +15%", 10, 0);
        Merchandise Copper = new Merchandise("Koppar", 120, 0.85, 1.15, "-15% - +15%", 10, 0);
        Merchandise Platinum = new Merchandise("Platinum", 300, 0.92, 1.08, "-8% - +8%", 10, 0);
        Merchandise Palladium = new Merchandise("Palladium", 250, 0.9, 1.1, "-10% - +10%", 10, 0);
        Merchandise Indium = new Merchandise("Indium", 150, 0.7, 1.3, "-30% - +30%", 10, 0);
        Merchandise Tin = new Merchandise("Tenn", 100, 0.85, 1.15, "-15% - +15%", 10, 0);

        StableMetalMerchant.ItemsForSale.Add(Gold);
        StableMetalMerchant.ItemsForSale.Add(Silver);
        VolatileMetalMerchant.ItemsForSale.Add(Bronze);
        VolatileMetalMerchant.ItemsForSale.Add(Copper);
        StableMetalMerchant.ItemsForSale.Add(Platinum);
        StableMetalMerchant.ItemsForSale.Add(Palladium);
        VolatileMetalMerchant.ItemsForSale.Add(Indium);
        VolatileMetalMerchant.ItemsForSale.Add(Tin);

        Gold.Value = (int)PriceHandler.CalculateNewPrice(Gold);
        Silver.Value = (int)PriceHandler.CalculateNewPrice(Silver);
        Bronze.Value = (int)PriceHandler.CalculateNewPrice(Bronze);
        Copper.Value = (int)PriceHandler.CalculateNewPrice(Copper);
        Platinum.Value = (int)PriceHandler.CalculateNewPrice(Platinum);
        Palladium.Value = (int)PriceHandler.CalculateNewPrice(Palladium);
        Indium.Value = (int)PriceHandler.CalculateNewPrice(Indium);
        Tin.Value = (int)PriceHandler.CalculateNewPrice(Tin);
        
        Market market = new Market(27, 80);

        int posX = 2, posY = 2;
        int previousPosX = posX, previousPosY = posY;



        foreach (var m in player.PlayerInventory)
        {
            if (m.Name == "Guld")
            {
                m.Value = Gold.Value;
            }
            else if (m.Name == "Silver")
            {
                m.Value = Silver.Value;
            }
            else if (m.Name == "Brons")
            {
                m.Value = Bronze.Value;
            }
            else if (m.Name == "Koppar")
            {
                m.Value = Copper.Value;
            }
            else if (m.Name == "Platinum")
            {
                m.Value = Platinum.Value;
            }
            else if (m.Name == "Palladium")
            {
                m.Value = Palladium.Value;
            }
            else if (m.Name == "Indium")
            {
                m.Value = Indium.Value;
            }
            else if (m.Name == "Tin")
            {
                m.Value = Tin.Value;
            }
        }

        Gold.UpdatePriceHistory(Gold.Value);
        Silver.UpdatePriceHistory(Silver.Value);
        Bronze.UpdatePriceHistory(Bronze.Value);
        Copper.UpdatePriceHistory(Copper.Value);
        Platinum.UpdatePriceHistory(Platinum.Value);
        Palladium.UpdatePriceHistory(Palladium.Value);
        Indium.UpdatePriceHistory(Indium.Value);
        Tin.UpdatePriceHistory(Tin.Value);

        HelpClass.SaveToJson(player, "JsonHandler.json");

        //Skriv ut spelplanen:
        DrawGameBoard(market);

        Market.DisplayInfo(market);
        Market.DisplayRules(market);

        // detta sätter muspekaren på olika platser varje varv i loopen efter det uppdateras nedan (posX++, posY++ osv.)
        Console.SetCursorPosition(posX, posY);
        System.Console.WriteLine("🧑");

        //SpelKaraktärens Hus
        Market.PlacePlayerHome(0, 13);

        // Trollkarlen
        Market.PlaceMerchantsBuildings(66, 1);

        // Gubben
        Market.PlaceMerchantsBuildings(66, 13);

        // Målar ut försäljare av volatila metaller
        Console.SetCursorPosition(72, 5);
        System.Console.WriteLine("🧙‍♂️");

        // Målar ut försäljare av stabila metaller
        Console.SetCursorPosition(72, 17);
        System.Console.WriteLine("👴");

        // Målar ut säng dit spelaren kan gå för att sova och starta en ny dag, med nya priser
        Console.SetCursorPosition(2, 17);
        System.Console.WriteLine("🛏️");

        // dessa saker händer om och om igen tills användaren trycker på escape
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
                    player.DisplayPlayerInventory(player, 1);
                    System.Console.WriteLine();
                    System.Console.ReadKey(true);
                    break;

                case ConsoleKey.P:
                    player.DisplayAccountBalance(player);
                    System.Console.ReadKey(true);
                    HelpClass.CleanTextToTheRight();
                    break;

                case ConsoleKey.D1:
                    Gold.DisplayPriceGraph(Gold, market, player);
                    Console.ReadKey(true);
                    HelpClass.CleanTextToTheRight();
                    break;

                case ConsoleKey.D2:
                    Silver.DisplayPriceGraph(Silver, market, player);
                    Console.ReadKey(true);
                    HelpClass.CleanTextToTheRight();
                    break;

                case ConsoleKey.D3:
                    Bronze.DisplayPriceGraph(Bronze, market, player);
                    Console.ReadKey(true);
                    HelpClass.CleanTextToTheRight();
                    break;

                case ConsoleKey.D4:
                    Copper.DisplayPriceGraph(Copper, market, player);
                    Console.ReadKey(true);
                    HelpClass.CleanTextToTheRight();
                    break;

                case ConsoleKey.D5:
                    Platinum.DisplayPriceGraph(Platinum, market, player);
                    Console.ReadKey(true);
                    HelpClass.CleanTextToTheRight();
                    break;

                case ConsoleKey.D6:
                    Palladium.DisplayPriceGraph(Palladium, market, player);
                    Console.ReadKey(true);
                    HelpClass.CleanTextToTheRight();
                    break;

                case ConsoleKey.D7:
                    Indium.DisplayPriceGraph(Indium, market, player);
                    Console.ReadKey(true);
                    HelpClass.CleanTextToTheRight();
                    break;

                case ConsoleKey.D8:
                    Tin.DisplayPriceGraph(Tin, market, player);
                    Console.ReadKey(true);
                    HelpClass.CleanTextToTheRight();
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
                HelpClass.SaveToJson(player, "JsonHandler.json");
                HelpClass.AdjustTextToTheRight(0);
                System.Console.WriteLine("Vill du Köpa eller sälja? Skriv '1' för att köpa eller '2' för att sälja.");
                HelpClass.AdjustTextToTheRight(1);

                int input = int.Parse(Console.ReadLine());
                if (input == 1)
                {
                    HelpClass.CleanTextToTheRight();
                    player.Buy(player, VolatileMetalMerchant);
                }
                else if (input == 2)
                {
                    HelpClass.CleanTextToTheRight();
                    player.Sell(player, VolatileMetalMerchant);
                }
                // else
                // {
                //     Market.AdjustTextToTheRight(0);
                //     System.Console.WriteLine("Du har skrivit fel. Vänligen skriv Köpa eller Sälja.");
                // }
            }

            if (Math.Abs(posX - 70) < 2 && Math.Abs(posY - 17) <= 1)
            {
                HelpClass.SaveToJson(player, "JsonHandler.json");
                HelpClass.AdjustTextToTheRight(0);
                System.Console.WriteLine("Vill du Köpa eller sälja? Skriv '1' för att köpa eller '2' för att sälja.");
                HelpClass.AdjustTextToTheRight(1);

                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    if (input < 0 || input > 2)
                    {
                        System.Console.WriteLine("Ange 1 eller 2.");
                        return;
                    }
                    else if (input == 1)
                    {
                        HelpClass.CleanTextToTheRight();
                        player.Buy(player, StableMetalMerchant);
                    }
                    else if (input == 2)
                    {
                        HelpClass.CleanTextToTheRight();
                        player.Sell(player, StableMetalMerchant);
                    }
                }
            }

            if (Math.Abs(posX - 6) < 1 && Math.Abs(posY - 17) <= 1)
            {
                HelpClass.SaveToJson(player, "JsonHandler.json");

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
        HelpClass.AdjustTextToTheRight(0);
        System.Console.WriteLine("Vill du starta nästa dag?");
        HelpClass.AdjustTextToTheRight(1);
        string answer = Console.ReadLine();

        if (answer.ToLower() == "ja" || answer.ToLower() == "yes")
        {   

            Merchandise.xOnGraph += 5;
            Console.Clear();
            MenuClass.TypeWrite("Marknaden sover nu...");
            Thread.Sleep(1000);
            System.Console.WriteLine();
            MenuClass.TypeWrite("Uppdaterar priser på marknadens metaller...");
            Thread.Sleep(1000);
            System.Console.WriteLine();
            MenuClass.TypeWrite("Tryck [ENTER] för att starta nästa dag... med nya priser");
            Console.ReadKey();
        }
        else if (answer.ToLower() == "NEJ" || answer.ToLower() == "NO")
        {
            GameLoop();
        }
    }

    private static void DrawGameBoard(Market market)
    {
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
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
}









