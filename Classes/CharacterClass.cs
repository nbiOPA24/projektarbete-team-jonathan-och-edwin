// Jonathan jobbar här

using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using MarketMaster1.Classes;

public class Character
{
    //Deklarerar Namn,Pengar på användarens konto samt skapar en lista för användarens inventory
    public string Name {get; set;}
    public double AccountBalance {get; set;}
  
    public static List<Merchandise> PlayerInventory {get; set;}

    //Konstruktor för Character(användaren)
    public Character(string name, double accountBalance)
    {
        Name = name;
        AccountBalance = accountBalance;
        PlayerInventory = new List<Merchandise>();
       
    }

    //Metod för att lägga till items i spelarens inventory
    public static void AddToInventory(Merchandise item, int quantity)
    {
        item.Quantity = quantity;
        PlayerInventory.Add(item);
    }

    // Visar spelarens inventory
    public static void DisplayPlayerInventory()
    {
        Console.SetCursorPosition(81,1);                                          
        System.Console.WriteLine("╔══════════════════════════ Spelarens Inventory ══════════════════════════╗");
        Console.SetCursorPosition(81,Console.CursorTop);
        System.Console.WriteLine("║    Item Name     ║ Quantity ║    Värde   ║Totalt värde ║   Volatilitet  ║");
        Console.SetCursorPosition(81,Console.CursorTop);
        System.Console.WriteLine("║------------------║----------║------------║-------------║----------------║");
        foreach (var metal in Character.PlayerInventory)
        {
            double totalValue = metal.Value * metal.Quantity; //totala värdet av varje item gånger antalet av varje item
            Console.SetCursorPosition(81,Console.CursorTop);
            //Nedan skriver vi ut alla metaller/items i spelarens inventory med namn, antal, värde, totalt värde samt vilken volatilitet varje item har.
            System.Console.WriteLine($"║ {metal.Name,-16} ║ {metal.Quantity,8} ║ {metal.Value,10:F2} ║ {totalValue,11:F2} ║ {metal.VolatilityNumLow * 100,6:F0}% - {metal.VolatilityNumHigh * 100,3:F0}% |");
        }
        Console.SetCursorPosition(81, Console.CursorTop);
        System.Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════╝");
        Console.SetCursorPosition(81, Console.CursorTop);
        Console.WriteLine("Klicka på valfri knapp för att få bort inventoryt.");
        Console.ReadKey(true);
        //Rensar texten som är bredvid spelplanen
        MenuClass.CleanTextToTheRight();
    }

    public static void DisplayAccountBalance(Character character) //Visar spelarens account balance/hur mycket pengar som finns på ens konto
    {
        Console.SetCursorPosition(81, 1);
        System.Console.Write     ("╔═══════════════════════╗");
        Console.SetCursorPosition(81, 2);
        System.Console.WriteLine($"║ Kontobalans: {character.AccountBalance}kr ║");
        Console.SetCursorPosition(81, 3);
        System.Console.Write     ("╚═══════════════════════╝");
    }

    // Låter spelaren sälja saker, just nu kommer de tas bort från "PlayerInventory" och läggas till i "ItemsForSale"
    public void Sell()
    {
        while (true)
        {
            
            DisplayPlayerInventory();   
            System.Console.WriteLine("Vad vill du sälja?");
            int itemToSell = int.Parse(Console.ReadLine());
            

            System.Console.WriteLine($"Okej, du vill sälja {PlayerInventory[itemToSell - 1].Name}.");
            System.Console.WriteLine($"Hur många {PlayerInventory[itemToSell - 1].Name} vill du sälja?");

            int amountToSell = int.Parse(Console.ReadLine());
            double valueOfSoldItems = PlayerInventory[itemToSell - 1].Value * amountToSell;

            if (!int.TryParse(Console.ReadLine(), out amountToSell))
            {
                System.Console.WriteLine("Du måste skriva en siffra!");
                continue;
            }

            if (amountToSell > PlayerInventory[itemToSell - 1].AmountAvailable)
            {
                System.Console.WriteLine("Du kan inte sälja fler än du har... eller hur?");
                System.Console.WriteLine("Du får några chanser till att sälja... ;)");
                continue;
            }
            else
            {
                System.Console.WriteLine($"Du har sålt {amountToSell} st. {PlayerInventory[itemToSell - 1].Name}.");
                System.Console.WriteLine($"Detta ger dig {valueOfSoldItems}kr kvar.");
                break;
            }
        }
    }
}