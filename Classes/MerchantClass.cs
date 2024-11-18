// Jonathan jobbar här

using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Xml.Serialization;
using MarketMaster1.Classes;

public class Merchant
{

    public string Name { get; set; }
    public int MerchantAccountBalance { get; set; }

    // Lista som lagrar alla metaller som säljs av handlarn
    public List<Merchandise> ItemsForSale { get; set; }
    public static List<Merchandise> ItemsForDisplay = new List<Merchandise>();
    private static Random random = new Random();

    public Merchant(string name, int merchantAccountBalance)
    {
        Name = name;
        MerchantAccountBalance = merchantAccountBalance;
        ItemsForSale = new List<Merchandise>();

    }

    public void DisplayAllItems()
    {
        int y = 1;
        int j = 6;

        foreach (var i in ItemsForSale)
        {
            Market.AdjustTextToTheRight(j);
            System.Console.Write(y + ". " + "Metall: " + i.Name + ", Nuvarande värde: " + i.Value + "kr");

            j += 3;
            y++;
        }
    }



    public bool ValidatePurchase(int chosenMetalIndex, int amountOfMetal, Player player)
    {
        if (chosenMetalIndex < 0 || chosenMetalIndex >= ItemsForSale.Count)
        {
            Market.AdjustTextToTheRight(22);
            System.Console.WriteLine("Felkod: Vänligen välj ett giltigt alternativ mellan 1 och " + ItemsForSale.Count);
            Console.ReadKey();
            HelpClass.CleanTextToTheRight();
            return false;
        }

        Merchandise chosenMetal = ItemsForSale[chosenMetalIndex];

        if (amountOfMetal > chosenMetal.AmountAvailableAtMerchant || amountOfMetal < 1)
        {
            if (amountOfMetal > chosenMetal.AmountAvailableAtMerchant)
            {
                Market.AdjustTextToTheRight(22);
                System.Console.WriteLine($"Felkod: Det finns endast {chosenMetal.AmountAvailableAtMerchant} kvar i lager.");
                Console.ReadKey();
                HelpClass.CleanTextToTheRight();
                return false;
            }
            else if (amountOfMetal < 1)
            {
                Market.AdjustTextToTheRight(22);
                System.Console.WriteLine("Du kan inte köpa 0 st.");
                Console.ReadKey();
                HelpClass.CleanTextToTheRight();
                return false;
            }
        }

        double totalCost = chosenMetal.Value * amountOfMetal;

        if (totalCost > player.AccountBalance)
        {
            double missingMoney = totalCost - player.AccountBalance;
            Market.AdjustTextToTheRight(22);
            System.Console.WriteLine($"Felkod: Du har för lite pengar. Det saknas {missingMoney}kr.");
            Console.ReadKey();
            HelpClass.CleanTextToTheRight();
            return false;
        }

        // Om spelaren köp går igenom alla tre ovan kontroller så genomförs köpet. Detta gör koden mer modulär genom att man endast kallar på metoden för att validera köpet.
        return true;
    }




    public static void DisplayDetailedProductInfo(Merchandise merchandise)
    {
        for (int j = 0; j < ItemsForDisplay.Count; j++)
        {
            ConsoleKeyInfo keyInfo;
            keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.Spacebar:
                    int x = 0; // används för att kolla varje index i itemsfordisplay-listan

                    foreach (var i in ItemsForDisplay)
                    {
                        Market.AdjustTextToTheRight(0);
                        System.Console.WriteLine($"Metall: {ItemsForDisplay[x].Name}");
                        Market.AdjustTextToTheRight(2);
                        System.Console.WriteLine($"Värde denna runda: {ItemsForDisplay[x].Value}kr");
                        Market.AdjustTextToTheRight(4);
                        System.Console.WriteLine($"Prisförändring varje runda: {ItemsForDisplay[x].VolatilityInAString}");

                    }
                    x++;
                    break;
            }
        }

    }

    public void GetUserSelection()
    {
        while (true)
        {
            DisplayAllItems();

            Market.AdjustTextToTheRight(17);
            System.Console.WriteLine("Vilken metall vill du köpa (1-4)?");

            Market.AdjustTextToTheRight(18);
            string input = Console.ReadLine();

            if (int.TryParse(input, out int chosenMetal))
            {
                if (chosenMetal >= 1 && chosenMetal <= ItemsForSale.Count)
                {
                    Market.AdjustTextToTheRight(20);
                    System.Console.WriteLine($"Du har valt att köpa {ItemsForSale[chosenMetal - 1].Name}.");
                    AskForAmount(chosenMetal);

                    break;
                }
                else
                {
                    System.Console.WriteLine("Ogiltigt val. Ange mellan 1-4.");
                }
            }
            else
            {
                System.Console.WriteLine("Ange en siffra.");
            }
        }
    }

    public void AskForAmount(int chosenMetal)
    {
        Market.AdjustTextToTheRight(22);
        System.Console.WriteLine($"Hur många {ItemsForSale[chosenMetal - 1].Name} vill du köpa?");
    }

    // public static double GenerateDailyUpdateFactor(Merchandise merchandise)
    // {
    //     Random random = new Random();

    //     double randomFactor = random.NextDouble() * (merchandise.VolatilityNumHigh - merchandise.VolatilityNumLow) + merchandise.VolatilityNumLow;

    //     return randomFactor;
    // }
}