using System;
using System.Threading;

Character TheWealthyBuyer = new Character("The Wealthy (but dumb) Buyer", 1500);
Character TheSkillfulNegotiator = new Character("The Skillful Negotiator", 700);
Character TheBalancedTrader = new Character("The Balanced Trader", 1000);


Merchant StableMetalMerchant = new Merchant("Stable metal merchant", 10, 10, 10000);
Merchant VolatileMetalMerchant = new Merchant("Volatile metal merchant", 20, 20, 10000);

Market market = new Market();


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



System.Console.WriteLine("Spelet kommer pågå någonstans mellan 10-20 rundor.\nI varje runda kan du köpa, sälja eller passa.\n\n... Randomiserar antalet rundor...");
Thread.Sleep(3500);
market.RandomizeNumberOfRounds();
