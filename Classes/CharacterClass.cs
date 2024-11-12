// Jonathan jobbar här

using System;
using Newtonsoft.Json; // använder oss av Newtonsoft för att kunna använda JSON för att spara/ladda data
using System.Collections.Generic;
using System.Drawing;

public class Character
{

    public string Name { get; set; }
    public int AccountBalance { get; set; }
    public List<Merchandise> PlayerInventory { get; set; }

    public Character()
    {
        Name = "Busiga investeraren";
        AccountBalance = 1000;
        PlayerInventory = new List<Merchandise>();

    }

    public static void SaveToJson(Character character, string fileName)
    {
        string json = JsonConvert.SerializeObject(character, Formatting.Indented);
        File.WriteAllText(fileName, json);
    }



    public static Character LoadFromJson(string fileName)
    {   
        if (!File.Exists(fileName))
        {
            return null;
        }

        var json = File.ReadAllText(fileName);
        var character = JsonConvert.DeserializeObject<Character>(json);

        return character;
    }

    public static void AddToInventory(Character character, Merchandise item, int quantity)
    {
        item.Quantity = quantity;
        character.PlayerInventory.Add(item);
    }

    // Visar spelarens inventory
    public void DisplayPlayerInventory(Character character)
    {
        Console.SetCursorPosition(81, 1);
        System.Console.WriteLine("=========================== Spelarens Inventory ===========================");
        Console.SetCursorPosition(81, Console.CursorTop);
        System.Console.WriteLine("|    Item Name     | Quantity |    Värde   |Totalt värde |   Volatilitet  |");
        Console.SetCursorPosition(81, Console.CursorTop);
        System.Console.WriteLine("|------------------|----------|------------|-------------|----------------|");
        foreach (var metal in character.PlayerInventory)
        {
            double totalValue = metal.Value * metal.Quantity;
            Console.SetCursorPosition(81, Console.CursorTop);
            System.Console.WriteLine($"| {metal.Name,-16} | {metal.Quantity,8} | {metal.Value,10:F2} | {totalValue,11:F2} | {metal.VolatilityNumLow * 100,6:F0}% - {metal.VolatilityNumHigh * 100,3:F0}% |");
        }
        Console.SetCursorPosition(81, Console.CursorTop);
        System.Console.WriteLine("===========================================================================");
        Console.SetCursorPosition(81, Console.CursorTop);
        Console.WriteLine("Klicka på valfri knapp för att få bort inventoryt.");
        Console.ReadKey();
        Merchant.CleanTextToTheRight();
    }

    public void DisplayAccountBalance(Character character)
    {
        Console.SetCursorPosition(81, 1);

        for (int i = 0; i < 23; i++)
        {
            System.Console.Write("=");
        }

        Console.SetCursorPosition(81, 2);
        System.Console.WriteLine($"| Kontobalans: {character.AccountBalance}kr |");

        Console.SetCursorPosition(81, 3);
        for (int i = 0; i < 23; i++)
        {
            System.Console.Write("=");
        }
    }

    // Låter spelaren sälja saker, just nu kommer de tas bort från "PlayerInventory" och läggas till i "ItemsForSale"
    public void Sell(Character character)
    {
        while (true)
        {
            System.Console.WriteLine("Vad vill du sälja?");
            DisplayPlayerInventory(character);

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

    // public static string CheckForBankruptcy()
    // {
    //     if (character.AccountBalance < 0)
    //     {
    //         string text = "Tyvärr... du kan inte gå in i en ny dag. Du saknar pengar...";
    //         return text;
    //     }

    //     string text1 = "Du har pengar, välkommen in!";
    //     return text1;
    // }
}