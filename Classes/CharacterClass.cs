// Jonathan jobbar här

using System;

using System.Collections.Generic;
using System.Drawing;

public class Character
{
    
    public string Name { get; set; }
    public static double AccountBalance { get; set; }
    public static List<Merchandise> PlayerInventory { get; set; }

    public Character(string name, double accountBalance)
    {
        Name = name;
        AccountBalance = accountBalance;
        PlayerInventory = new List<Merchandise>();
    }

    // public void Buy()
    // {

    // }

    // Visar spelarens inventory
    public void DisplayPlayerInventory()
    {
        foreach (var m in PlayerInventory)
        {
            System.Console.WriteLine(m);
        }
    }

    // Låter spelaren sälja saker, just nu kommer de tas bort från "PlayerInventory" och läggas till i "ItemsForSale"
    public void Sell()
    {
        while (true)
        {
            System.Console.WriteLine("Vad vill du sälja?");
            DisplayPlayerInventory();

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