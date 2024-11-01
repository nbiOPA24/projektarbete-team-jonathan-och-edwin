using System;
using System.Security;
using System.Threading;
using System.Media;

<<<<<<< HEAD
public class Program
{
    public static void Main()
    {
        
        // Skapa karaktärer
        Character TheWealthyBuyer = new Character("The Wealthy (but dumb) Buyer", 1500);
        Character TheSkillfulNegotiator = new Character("The Skillful Negotiator", 700);
        Character TheBalancedTrader = new Character("The Balanced Trader", 1000);

        // Skapa handlare
        Merchant StableMetalMerchant = new Merchant("Stable metal merchant", 10, 10, 10000);
        Merchant VolatileMetalMerchant = new Merchant("Volatile metal merchant", 20, 20, 10000);

        // Skapa marknad och metaller
        Market market = new Market();

        Merchandise Gold = new Merchandise("Gold", 200, 0.95, 1.05, 10);
        Merchandise Silver = new Merchandise("Silver", 180, 0.93, 1.07, 10);
        Merchandise Bronze = new Merchandise("Bronze", 70, 0.85, 1.15, 10);
        Merchandise Copper = new Merchandise("Copper", 120, 0.85, 1.15, 10);
        Merchandise Platinum = new Merchandise("Platinum", 300, 0.92, 1.08, 10);
        Merchandise Palladium = new Merchandise("Palladium", 250, 0.9, 1.1, 10);
        Merchandise Indium = new Merchandise("Indium", 150, 0.7, 1.3, 10);
        Merchandise Tin = new Merchandise("Tin", 100, 0.85, 1.15, 10);

        // Lägg till metaller till handlare
        StableMetalMerchant.ItemsForSale.Add(Gold);
        StableMetalMerchant.ItemsForSale.Add(Silver);
        VolatileMetalMerchant.ItemsForSale.Add(Bronze);
        VolatileMetalMerchant.ItemsForSale.Add(Copper);
        StableMetalMerchant.ItemsForSale.Add(Platinum);
        StableMetalMerchant.ItemsForSale.Add(Palladium);
        VolatileMetalMerchant.ItemsForSale.Add(Indium);
        VolatileMetalMerchant.ItemsForSale.Add(Tin);

        // Introduktion
        Console.Clear();
        Console.WriteLine("\nSpelet kommer pågå någonstans mellan 10-20 rundor.\nI varje runda kan du köpa, sälja eller passa.\n\n... Randomiserar antalet rundor...");
        Thread.Sleep(6000);
        market.RandomizeNumberOfRounds();
        Console.Clear();
        Console.WriteLine("****************************************");
        Console.WriteLine("   * Välkommen till Market Master! *");
        Console.WriteLine("****************************************");
        Thread.Sleep(4500);
        Console.Clear();
        Console.WriteLine("\n");
        TypeWrite("I en värld där guld skimrar, ");
        Thread.Sleep(360);
        TypeWrite("silver lockar");
        Thread.Sleep(360);
        TypeWrite(" och platina står på spel ");
        Thread.Sleep(360);
        TypeWrite("finns det mycket som kan gå fel...\n");
        Thread.Sleep(1800);
        TypeWrite("Står du redo för att göra ditt drag?\n");
        Thread.Sleep(1200);
        TypeWrite("Var försiktig; marknaden kan vara nyckfull, ");
        Thread.Sleep(360);
        TypeWrite("men för den listige väntar stora vinster!\n");
        Thread.Sleep(2100);
        Console.WriteLine("Press [Enter] för att kliva in i marknadens djungel...");
        Console.ReadKey();
        Thread.Sleep(1200);
        Console.Clear();
        string audioFile = @"C:\Users\edwin\Documents\Projects\MarketMasterConsole\projektarbete-team-jonathan-och-edwin\MarketPirate.wav";
        using (SoundPlayer player = new SoundPlayer(audioFile))
        {
            player.Load();    // Load the file
            player.PlayLooping();    // Play the audio (PlaySync() to wait until it's finished)
        }
        TypeWrite("Du står vid marknadens port,");
        Thread.Sleep(900);
        TypeWrite(" en tyngd av mynt klirrar i fickan.\n");
        Thread.Sleep(900);
        TypeWrite("Handlare viskar om dagens bästa fynd, men vem kan du lita på?\n");
        Thread.Sleep(900);
        TypeWrite("'Kom och köp,' ropar en man. 'Endast de smartaste överlever här!'.\n");
        Thread.Sleep(900);
        TypeWrite("Törs du satsa stort eller väljer du att spela försiktigt? \n");
        Thread.Sleep(900);
        TypeWrite("Press [Enter] för att pröva lyckan...");
        Console.ReadKey();
=======
//Skapar två olika handlare som säljer olika metaller
Merchant StableMetalMerchant = new Merchant("Stable metal merchant", 10, 10, 10000);
Merchant VolatileMetalMerchant = new Merchant("Volatile metal merchant", 20, 20, 10000);

// Skapar nya objekt av klassen Merchandise, med egenskaper Namn, värde, lägsta siffra (procent den kan minska med), högsta siffra och mängd som finns i lager
Merchandise Gold = new Merchandise("Gold", 200, 0.95, 1.05, 10);
Merchandise Silver = new Merchandise("Silver", 180, 0.93, 1.07, 10);
Merchandise Bronze = new Merchandise("Bronze", 70, 0.85, 1.15, 10);
Merchandise Copper = new Merchandise("Copper", 120, 0.85, 1.15, 10);
Merchandise Platinum = new Merchandise("Platinum", 300, 0.92, 1.08, 10);
Merchandise Palladium = new Merchandise("Palladium", 250, 0.9, 1.1, 10);
Merchandise Indium = new Merchandise("Indium", 150, 0.7, 1.3, 10);
Merchandise Tin = new Merchandise("Tin", 100, 0.85, 1.15, 10);
>>>>>>> 37a2d3a73c011a88ba170e6c2ec7acd65cfa8bd8

        Thread.Sleep(1800);

        

Market market = new Market(40, 80); // skapar en instans av Market som heter market

Character character = new Character("Busiga investeraren", 1000);
int posX = 2;
int posY = 2;

ConsoleKeyInfo keyInfo;

// Kontrollerar att spelaren inte går på någon av försäljarna
bool IsCollision(int newX, int newY)
{
    if ((newX == 70 && newY == 3) || (newX == 70 && newY == 28))
    {
        return true;
    }

    return false;
}

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
    Console.WriteLine();
    // ritat färdigt ramen

    // Trollkarlen
    Market.PlaceMerchantsBuilding(66, 1);
    Market.PlaceMerchantsBuilding(66, 5);

    // Gubben
    Market.PlaceMerchantsBuilding(66, 26);
    Market.PlaceMerchantsBuilding(66, 30);

    Market.PlaceDecoration(11, 38);

    // Målar ut försäljare av volatila metaller
    Console.SetCursorPosition(70, 3);
    System.Console.WriteLine("🧙‍♂️");

    // Målar ut försäljare av stabila metaller
    Console.SetCursorPosition(70, 28);
    System.Console.WriteLine("👴");

    Console.SetCursorPosition(posX, posY); // detta sätter muspekaren på olika platser varje varv i loopen efter det uppdateras nedan (posX++, posY++ osv.)
    System.Console.WriteLine("🧑");

    keyInfo = Console.ReadKey(true); // Console.ReadKey(true) gör här att vi läser in ett ENSKILT tangenttryck från användaren. "true" gör att tangenten som trycks in skrivs ut på skärmen

  

    switch (keyInfo.Key)
    {
        case ConsoleKey.UpArrow: // Så länge muspekarens Y-värde (lodrätt) är större än 1 får spelaren gå uppåt. Detta kontrollerar att användaren inte går utanför banan uppåt
            if (posY > 1 && posY != 3 || posX != 70) posY--;
            break;

        case ConsoleKey.DownArrow: // Så länge muspekarens Y-värde (lodrätt) är större eller lika med 0 får spelaren gå nedåt. Detta kontrollerar att användaren inte går utanför banan nedåt
            if (posY >= 0 && posY <= 39) posY++;
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

    

    if (Math.Abs(posX - 70) < 1 && Math.Abs(posY - 3) <= 1)
    {
        VolatileMetalMerchant.Sell();
    }

     if (Math.Abs(posX - 70) < 1 && Math.Abs(posY - 28) <= 1)
    {
        StableMetalMerchant.Sell();
    }

    int newX = posX;
    int newY = posY;

    if (!IsCollision(newX, newY))
    {
        posX = newX;
        posY = newY;
    }
}


<<<<<<< HEAD
    }

    // Metod för skrivmaskinseffekt
    public static void TypeWrite(string text, int delay = 45)
    {
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(delay);
        }
    }
}
=======







// int bufferHeight = Console.BufferHeight;
// int bufferWidth = Console.BufferWidth;
// int windowHeight = Console.WindowHeight;
// int windowWidth = Console.WindowWidth;

// System.Console.WriteLine($"{bufferHeight} {bufferWidth} {windowHeight} {windowWidth}");
>>>>>>> 37a2d3a73c011a88ba170e6c2ec7acd65cfa8bd8
