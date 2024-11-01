using System;
using System.Security;
using System.Threading;

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

// Adderar alla nya objekt till marknadens lista "ItemsForSale"
StableMetalMerchant.ItemsForSale.Add(Gold);
StableMetalMerchant.ItemsForSale.Add(Silver);
VolatileMetalMerchant.ItemsForSale.Add(Bronze);
VolatileMetalMerchant.ItemsForSale.Add(Copper);
StableMetalMerchant.ItemsForSale.Add(Platinum);
StableMetalMerchant.ItemsForSale.Add(Palladium);
VolatileMetalMerchant.ItemsForSale.Add(Indium);
VolatileMetalMerchant.ItemsForSale.Add(Tin);

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









// int bufferHeight = Console.BufferHeight;
// int bufferWidth = Console.BufferWidth;
// int windowHeight = Console.WindowHeight;
// int windowWidth = Console.WindowWidth;

// System.Console.WriteLine($"{bufferHeight} {bufferWidth} {windowHeight} {windowWidth}");
