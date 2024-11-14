// Jonathan jobbar här

using System;
using System.Collections.Generic;
using MarketMaster1.Classes;

public class Merchant
{

    public string Name { get; set; }
    public int MerchantAccountBalance { get; set; }

    // Lista som lagrar alla metaller som säljs av handlarn
    public List<Merchandise> ItemsForSale { get; set; }
    public static List<Merchandise> ItemsForDisplay = new List<Merchandise>();


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

    //denna metod randomiserar vad nuvarande "värde" på ädelmetallen ska multipliceras med. Algoritmen gör även att det är större chans att priset går UPP än NER
    public double RandomizePercentage(Random random, double minValue, double maxValue)
    {
        Random random1 = new Random();
        int randomNum = random1.Next(0, 101);

        if (randomNum > 30)
        {
            return random.NextDouble() * (maxValue - 1) + 1;
        }
        else
        {
            return random.NextDouble() * (maxValue - minValue) + minValue;
        }
    }



    // denna metod använder "RandomizePercentage" ovan för att räkna ut ett nytt pris för varje ädelmetall i listan "ItemsForSale" i Market-klassen.
    public void UpdatePrice(Merchandise merchandise)
    {
        Random random = new Random();
        double randomValue = RandomizePercentage(random, merchandise.VolatilityNumLow, merchandise.VolatilityNumHigh);
        double newPrice = randomValue * merchandise.Value;
        double roundedNewPrice = Math.Round(newPrice, 2);
        merchandise.Value = (int)roundedNewPrice;
            }






    // Nedanstående metod gör att "Merchanten" kan sälja produkter. Saker tas från "ItemsForSale" och läggs i "PlayerInventory"
    // Summa subtraheras från "AccountBalance" och summa adderas till "MerchantAccountBalance", för att visualisera flödet av pengar

    // VIKTIGT - Denna metod används för både "Merchant"-sell och "Character"-buy
    public void Sell(Character character)
    {
        while (true)
        {
            // GetUserSelection(); // kallar inte på denna ännu

            DisplayAllItems();
            character.DisplayAccountBalance(character);

            Market.AdjustTextToTheRight(17);
            System.Console.WriteLine("Vilken ädelmetall vill du köpa (1-4)?");

            Market.AdjustTextToTheRight(18);
            int chosenMetal = int.Parse(Console.ReadLine()) - 1;

            Market.AdjustTextToTheRight(19);
            System.Console.WriteLine("Hur många vill du köpa?");

            Market.AdjustTextToTheRight(20);
            int amountOfMetal = int.Parse(Console.ReadLine());

            if (!ValidatePurchase(chosenMetal, amountOfMetal, character))
            {
                Console.ReadKey();
                return;
            }

            if (ValidatePurchase(chosenMetal, amountOfMetal, character))
            {
                CleanTextToTheRight();
                Market.AdjustTextToTheRight(6);
                MenuClass.TypeWrite($"Okej, du köpte {amountOfMetal} st. {ItemsForSale[chosenMetal].Name}."); // visar hur många du köpt av vad
                Market.AdjustTextToTheRight(7);
                MenuClass.TypeWrite($"Denna kostade {ItemsForSale[chosenMetal].Value}kr/st."); // visar vad det kostade

                int amountLeft = ItemsForSale[chosenMetal].AmountAvailable - amountOfMetal;
                ItemsForSale[chosenMetal].AmountAvailable = amountLeft;

                int updatedAccountBalance = character.AccountBalance - ItemsForSale[chosenMetal].Value * amountOfMetal;
                character.AccountBalance = updatedAccountBalance; // sätter egenskapen "AccountBalance" till variabeln updatedAccountBalance, så att det uppdateras varje varv

                double updatedMerchantAccountBalance = MerchantAccountBalance + ItemsForSale[chosenMetal].Value * amountOfMetal;

                Thread.Sleep(1500);
                Market.AdjustTextToTheRight(9);
                MenuClass.TypeWrite($"Ditt konto: {updatedAccountBalance}kr.");
                Market.AdjustTextToTheRight(10);
                MenuClass.TypeWrite($"Säljarens konto: {updatedMerchantAccountBalance}kr.");

                Merchandise chosenMerchandise = ItemsForSale[chosenMetal];

                // denna bool är skriven för att se om metallen redan finns i spelarens inventory, isåfall vill vi ju bara plusa på quantity och inte lägga till en extra sådan
                bool containsMetal = character.PlayerInventory.Any(m => m.Name == ItemsForSale[chosenMetal].Name);

                if (containsMetal)
                {
                    character.PlayerInventory[chosenMetal].Quantity = character.PlayerInventory[chosenMetal].Quantity + amountOfMetal;
                    Character.SaveToJson(character, "JsonHandler.json");
                }
                else if (!containsMetal)
                {
                    character.PlayerInventory.Add(ItemsForSale[chosenMetal]);
                    character.PlayerInventory[chosenMetal].Quantity = character.PlayerInventory[chosenMetal].Quantity + amountOfMetal;
                    Character.SaveToJson(character, "JsonHandler.json");
                }

                Thread.Sleep(1500);
                Market.AdjustTextToTheRight(12);
                MenuClass.TypeWrite($"Kolla din inventory! Nu har du köpt {ItemsForSale[chosenMetal].Name}!");
                character.AccountBalance = character.AccountBalance - ItemsForSale[chosenMetal].Value * amountOfMetal;

                Market.AdjustTextToTheRight(13);
                Thread.Sleep(1000);
                System.Console.WriteLine("Klicka [ENTER] för att fortsätta spela.");
                Console.ReadKey();
                CleanTextToTheRight();

                break;
            }
        }
    }

    // min tanke är att kapsla in denna metod, för den ska bara kunna användas i sell-metoden och ingen annanstans. Denna metod gör koden mer modulär och minskar redundans.
    private bool ValidatePurchase(int chosenMetalIndex, int amountOfMetal, Character character)
    {
        if (chosenMetalIndex < 0 || chosenMetalIndex >= ItemsForSale.Count)
        {
            Market.AdjustTextToTheRight(22);
            System.Console.WriteLine("Felkod: Vänligen välj ett giltigt alternativ mellan 1 och " + ItemsForSale.Count);
            Console.ReadKey();
            CleanTextToTheRight();
            return false;
        }

        Merchandise chosenMetal = ItemsForSale[chosenMetalIndex];

        if (amountOfMetal > chosenMetal.AmountAvailable || amountOfMetal < 1)
        {
            if (amountOfMetal > chosenMetal.AmountAvailable)
            {
                Market.AdjustTextToTheRight(22);
                System.Console.WriteLine($"Felkod: Det finns endast {chosenMetal.AmountAvailable} kvar i lager.");
                Console.ReadKey();
                CleanTextToTheRight();
                return false;
            }
            else if (amountOfMetal < 1)
            {
                Market.AdjustTextToTheRight(22);
                System.Console.WriteLine("Du kan inte köpa 0 st.");
                Console.ReadKey();
                CleanTextToTheRight();
                return false;
            }
        }

        int totalCost = chosenMetal.Value * amountOfMetal;

        if (totalCost > character.AccountBalance)
        {
            int missingMoney = totalCost - character.AccountBalance;
            Market.AdjustTextToTheRight(22);
            System.Console.WriteLine($"Felkod: Du har för lite pengar. Det saknas {missingMoney}kr.");
            Console.ReadKey();
            CleanTextToTheRight();
            return false;
        }

        // Om spelaren köp går igenom alla tre ovan kontroller så genomförs köpet. Detta gör koden mer modulär genom att man endast kallar på metoden för att validera köpet.
        return true;
    }

    // Metod för att rensa köpinformationen specifikt utan att röra spelplanen
    public static void CleanTextToTheRight()
    {
        for (int i = 0; i <= 39; i++) // Radintervallet för köpinformation
        {
            Market.AdjustTextToTheRight(i); // Justerar för att rensa texten till höger
            Console.Write(new string(' ', Console.WindowWidth - 81)); // Rensar varje rad under handlarens text
        }
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
}