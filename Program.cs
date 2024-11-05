using System;
using System.Security;
using System.Threading;
using System.Media;
using System.Security.Cryptography;

public class Program
{
    public static List<Merchandise> ItemsForDisplay = new List<Merchandise>(); 
    public static void Main()
    {
        // skapar en instans av Market som heter market
        Market market = new Market(25, 80);
        // Skapa karaktärer
        Character TheWealthyBuyer = new Character("The Wealthy (but dumb) Buyer", 1500);
        Character TheSkillfulNegotiator = new Character("The Skillful Negotiator", 700);
        Character TheBalancedTrader = new Character("The Balanced Trader", 1000);

        // Skapa handlare
        Merchant StableMetalMerchant = new Merchant("Stable metal merchant", 10, 10, 10000);
        Merchant VolatileMetalMerchant = new Merchant("Volatile metal merchant", 20, 20, 10000);

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

        // Lägg till metaller i en lista som sedan ska agera som "display" för användaren
        ItemsForDisplay.Add(Gold);
        ItemsForDisplay.Add(Silver);
        ItemsForDisplay.Add(Bronze);
        ItemsForDisplay.Add(Copper);
        ItemsForDisplay.Add(Platinum);
        ItemsForDisplay.Add(Palladium);
        ItemsForDisplay.Add(Indium);
        ItemsForDisplay.Add(Tin);

        // Kommentera tillbaka detta nedan

        // // Introduktion
        // Console.Clear();
        // Console.WriteLine("\nSpelet kommer pågå någonstans mellan 10-20 rundor.\nI varje runda kan du köpa, sälja eller passa.\n\n... Randomiserar antalet rundor...");
        // Thread.Sleep(6000);
        // market.RandomizeNumberOfRounds();
        // Console.Clear();
        // Console.WriteLine("****************************************");
        // Console.WriteLine("   * Välkommen till Market Master! *");
        // Console.WriteLine("****************************************");
        // Thread.Sleep(4500);
        // Console.Clear();
        // Console.WriteLine("\n");
        // TypeWrite("I en värld där guld skimrar, ");
        // Thread.Sleep(360);
        // TypeWrite("silver lockar");
        // Thread.Sleep(360);
        // TypeWrite(" och platina står på spel ");
        // Thread.Sleep(360);
        // TypeWrite("finns det mycket som kan gå fel...\n");
        // Thread.Sleep(1800);
        // TypeWrite("Står du redo för att göra ditt drag?\n");
        // Thread.Sleep(1200);
        // TypeWrite("Var försiktig; marknaden kan vara nyckfull, ");
        // Thread.Sleep(360);
        // TypeWrite("men för den listige väntar stora vinster!\n");
        // Thread.Sleep(2100);
        // Console.WriteLine("Press [Enter] för att kliva in i marknadens djungel...");
        // Console.ReadKey();
        // Thread.Sleep(1200);
        // Console.Clear();
        // string audioFile = @"C:\Users\jonat\AProject\MarketMaster1\MarketPirate.wav";
        // using (SoundPlayer player = new SoundPlayer(audioFile))
        // {
        //     player.Load();    // Load the file
        //     player.PlayLooping();    // Play the audio (PlaySync() to wait until it's finished)
        // }
        // TypeWrite("Du står vid marknadens port,");
        // Thread.Sleep(900);
        // TypeWrite(" en tyngd av mynt klirrar i fickan.\n");
        // Thread.Sleep(900);
        // TypeWrite("Handlare viskar om dagens bästa fynd, men vem kan du lita på?\n");
        // Thread.Sleep(900);
        // TypeWrite("'Kom och köp,' ropar en man. 'Endast de smartaste överlever här!'.\n");
        // Thread.Sleep(900);
        // TypeWrite("Törs du satsa stort eller väljer du att spela försiktigt? \n");
        // Thread.Sleep(900);
        // TypeWrite("Press [Enter] för att pröva lyckan...");
        // Console.ReadKey();

        Thread.Sleep(1800);

        Character character = new Character("Busiga investeraren", 1000);
        int posX = 2;
        int posY = 2;
    
        while (true)
        {
            Console.Clear();


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

            // Market.PlaceDecoration(11, 38);

          

            // Målar ut försäljare av volatila metaller
            Console.SetCursorPosition(70, 3);
            System.Console.WriteLine("🧙‍♂️");

            // Målar ut försäljare av stabila metaller
            Console.SetCursorPosition(70, 15);
            System.Console.WriteLine("👴");

            // Målar ut ett frågetecken där spelaren kan läsa om varje metall
            Console.SetCursorPosition(70, 22);
            System.Console.WriteLine("❓");

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

                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    return;
            }


            if (Math.Abs(posX - 68) < 1 && Math.Abs(posY - 3) <= 1)
            {
                VolatileMetalMerchant.Sell();
            }

            if (Math.Abs(posX - 68) < 1 && Math.Abs(posY - 15) <= 1)
            {
                StableMetalMerchant.Sell();
            }

            if (posX == 68 && (posY == 21 || posY == 22 || posY == 23))
            {
                Merchant.DisplayDetailedProductInfo();
                Merchant.DisplayDetailedProductInfo();
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

    
}









// nedanstående ligger här för att snabbt kunna se vad bredden och höjden är på fönstret och buffern 

// int bufferHeight = Console.BufferHeight;
// int bufferWidth = Console.BufferWidth;
// int windowHeight = Console.WindowHeight;
// int windowWidth = Console.WindowWidth;

// System.Console.WriteLine($"{bufferHeight} {bufferWidth} {windowHeight} {windowWidth}");
