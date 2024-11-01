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


Character character = new Character("Player1", 1000);
int posX = 1;
int posY = 1;

ConsoleKeyInfo keyInfo;

while (true)
{
    Console.Clear(); // Rensar brädet inför varje nytt varv i loopen, annars blir det ett nytt bräde varje gång spelaren interagerar med spelet

    Market market = new Market(40, 80); // skapar en instans av Market som heter market
    market.DisplayMarket(); // kallar på metoden "DisplayMarket()" som skriver ut spelplanen

    // Placerar hus för handlarna
    Market.PlaceMerchantsBuilding(67, 34);
    Market.PlaceMerchantsBuilding(67, 38);
    Market.PlaceMerchantsBuilding(67, 1);
    Market.PlaceMerchantsBuilding(67, 5);

    Market.PlaceDecoration(58, 40); // placerar guldmynt längst ner på skärmen

    Console.SetCursorPosition(75, 3); // placerar handlaren som säljer volatila metaller
    System.Console.WriteLine("🧙");

    Console.SetCursorPosition(75, 36); // placerar handlaren som säljer stabila metaller
    System.Console.WriteLine("👴");

    Console.SetCursorPosition(posX, posY); // detta sätter muspekaren på olika platser varje varv i loopen efter det uppdateras nedan (posX++, posY++ osv.)
    System.Console.WriteLine("🧑");

    keyInfo = Console.ReadKey(true); // Console.ReadKey(true) gör här att vi läser in ett ENSKILT tangenttryck från användaren. "true" gör att tangenten som trycks in skrivs ut på skärmen

    switch (keyInfo.Key)
    {
        case ConsoleKey.UpArrow: // Så länge muspekarens Y-värde (lodrätt) är större än 1 får spelaren gå uppåt. Detta kontrollerar att användaren inte går utanför banan uppåt
            if (posY > 1) posY--;
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

    if (Math.Abs(posX - 74) < 1 && Math.Abs(posY - 3) <= 1)
    {
        VolatileMetalMerchant.Sell();
    }
}




