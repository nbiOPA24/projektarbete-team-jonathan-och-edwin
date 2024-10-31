using System;
using System.Threading;

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

        // Resten av introduktion
        System.Console.Clear();
        Console.WriteLine("\nSpelet kommer pågå någonstans mellan 10-20 rundor.\nI varje runda kan du köpa, sälja eller passa.\n\n... Randomiserar antalet rundor...");
        Thread.Sleep(6000);
        market.RandomizeNumberOfRounds();
        System.Console.Clear();
        Console.WriteLine("***************************************");
        Console.WriteLine("* Välkommen till Market Master! *");
        Console.WriteLine("***************************************");
        Thread.Sleep(3500);
        Console.WriteLine("\n");
        TypeWrite("I en värld där guld skimrar, silver lockar och platina står på spel finns det mycket som kan gå fel...\n");
        Thread.Sleep(1800);
        TypeWrite("Står du redo att göra ditt drag? Var försiktig; marknaden kan vara nyckfull,\n");
        TypeWrite("men för den listige väntar stora vinster!\n");
        Thread.Sleep(1800);
        System.Console.WriteLine("Press [Enter] för att kliva in i marknadens djungel...");
        Console.ReadKey();
        TypeWrite("Du står vid marknadens port, en tyngd av mynt klirrar i fickan.\nHandlare viskar om dagens bästa fynd, men vem kan du lita på?");
        Thread.Sleep(900);
        TypeWrite("'Kom och köp,' ropar en man. 'Endast de smartaste överlever här.'");
        Thread.Sleep(600);
        TypeWrite("Törs du satsa stort eller väljer du att spela försiktigt? \nPress[Enter] för att pröva lyckan...");
        Console.ReadKey();

        Thread.Sleep(900);



    }

    // Metod för skrivmaskinseffekt
    public static void TypeWrite(string text, int delay = 36)
    {
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(delay);
        }
    }
}
