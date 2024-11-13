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
            character.DisplayAccountBalance(character);
            DisplayAllItems();
            Market.AdjustTextToTheRight(17);
            System.Console.WriteLine("Vilken ädelmetall vill du köpa?");
            Market.AdjustTextToTheRight(18);
            System.Console.WriteLine("Skriv 1-4 beroende plats på listan.");
            Market.AdjustTextToTheRight(20);
            System.Console.WriteLine("Ditt svar (ange 1-4): ");
            Market.AdjustTextToTheRight(21);
            int chosenMetal = int.Parse(Console.ReadLine());


            if (chosenMetal < 1 || chosenMetal > 4)
            {
                Market.AdjustTextToTheRight(24);
                System.Console.WriteLine("Välj en siffra mellan 1-4.");
                Market.AdjustTextToTheRight(25);
                System.Console.WriteLine("Tryck på en knapp för att få välja igen.");
                Console.ReadKey();
                CleanTextToTheRight();
                continue;
            }

            Market.AdjustTextToTheRight(23);
            System.Console.WriteLine($"Okej! Du vill köpa {ItemsForSale[chosenMetal - 1].Name}!");
            Market.AdjustTextToTheRight(24);
            System.Console.WriteLine("Vi får se om det är ett bra köp...");
            Market.AdjustTextToTheRight(26);
            System.Console.WriteLine($"Hur många {ItemsForSale[chosenMetal - 1].Name} vill du köpa?");
            Market.AdjustTextToTheRight(27);
            System.Console.WriteLine($"Just nu finns {ItemsForSale[chosenMetal - 1].AmountAvailable} kvar i lager.");
            Market.AdjustTextToTheRight(28);
            System.Console.WriteLine("Ditt svar (går ej att köpa fler än det finns i lager): ");
            Market.AdjustTextToTheRight(29);

            int amountOfMetal = int.Parse(Console.ReadLine());

            // Kontrollerar att det finns tillräckligt många i lager...
            if (amountOfMetal > ItemsForSale[chosenMetal - 1].AmountAvailable)
            {
                CleanTextToTheRight();
                Market.AdjustTextToTheRight(6);
                System.Console.WriteLine("Du har köpt för många...");
                Market.AdjustTextToTheRight(7);
                System.Console.WriteLine($"Just nu har vi bara {ItemsForSale[chosenMetal - 1].AmountAvailable} {ItemsForSale[chosenMetal - 1].Name} i lager.");
                Market.AdjustTextToTheRight(8);
                System.Console.WriteLine("Klicka på en tangent för att försöka igen.");
                Console.ReadKey();
                CleanTextToTheRight();
                break;
            }

            // Kontrollerar att värdet på mängden metall användaren köper inte överstiger användarens kontobalans
            else if (amountOfMetal * ItemsForSale[chosenMetal - 1].Value > character.AccountBalance)
            {
                double missingMoney = amountOfMetal * ItemsForSale[chosenMetal - 1].Value - character.AccountBalance;

                CleanTextToTheRight();
                Market.AdjustTextToTheRight(6);
                System.Console.WriteLine($"Du har för lite pengar. Det saknas tyvärr {missingMoney}kr för att du ska ha råd.");
                Market.AdjustTextToTheRight(7);
                System.Console.WriteLine("Klicka på en tangent för att försöka igen.");
                Console.ReadKey();
                CleanTextToTheRight();
                break;
            }

            else
            {
                CleanTextToTheRight();
                Market.AdjustTextToTheRight(6);
                MenuClass.TypeWrite($"Okej, du köpte {amountOfMetal} st. {ItemsForSale[chosenMetal - 1].Name}."); // visar hur många du köpt av vad
                Market.AdjustTextToTheRight(7);
                MenuClass.TypeWrite($"Denna kostade {ItemsForSale[chosenMetal - 1].Value}kr/st."); // visar vad det kostade

                int amountLeft = ItemsForSale[chosenMetal - 1].AmountAvailable - amountOfMetal;
                ItemsForSale[chosenMetal - 1].AmountAvailable = amountLeft;

                int updatedAccountBalance = character.AccountBalance - ItemsForSale[chosenMetal - 1].Value * amountOfMetal;
                character.AccountBalance = updatedAccountBalance; // sätter egenskapen "AccountBalance" till variabeln updatedAccountBalance, så att det uppdateras varje varv

                double updatedMerchantAccountBalance = MerchantAccountBalance + ItemsForSale[chosenMetal - 1].Value * amountOfMetal;


                Thread.Sleep(1500);
                Market.AdjustTextToTheRight(9);
                MenuClass.TypeWrite($"Ditt konto: {updatedAccountBalance}kr.");
                Market.AdjustTextToTheRight(10);
                MenuClass.TypeWrite($"Säljarens konto: {updatedMerchantAccountBalance}kr.");
                

                Merchandise chosenMerchandise = ItemsForSale[chosenMetal - 1];
                

                bool containsMetal = character.PlayerInventory.Any(m => m.Name == ItemsForSale[chosenMetal - 1].Name);

                if (containsMetal)
                {
                    character.PlayerInventory[chosenMetal - 1].Quantity = character.PlayerInventory[chosenMetal - 1].Quantity + amountOfMetal;
                    Character.SaveToJson(character, "JsonHandler.json");
                }
                else if (!containsMetal)
                {
                    character.PlayerInventory.Add(ItemsForSale[chosenMetal - 1]);
                    character.PlayerInventory[chosenMetal - 1].Quantity = character.PlayerInventory[chosenMetal - 1].Quantity + amountOfMetal;
                    Character.SaveToJson(character, "JsonHandler.json");
                }

                Thread.Sleep(1500);
                Market.AdjustTextToTheRight(12);
                MenuClass.TypeWrite($"Kolla din inventory! Nu har du köpt {ItemsForSale[chosenMetal - 1].Name}!");
                character.AccountBalance = character.AccountBalance - ItemsForSale[chosenMetal - 1].Value * amountOfMetal;

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
        if (chosenMetalIndex < 1 || chosenMetalIndex >= ItemsForSale.Count)
        {
            System.Console.WriteLine("Välj ett giltigt alternativ mellan 1 och " + ItemsForSale.Count);
            return false;
        }

        Merchandise chosenMetal = ItemsForSale[chosenMetalIndex];

        if (amountOfMetal > chosenMetal.AmountAvailable)
        {
            System.Console.WriteLine($"Det finns endast {chosenMetal.AmountAvailable} kvar i lager.");
            return false;
        }

        int totalCost = chosenMetal.Value * amountOfMetal;

        if (totalCost > character.AccountBalance)
        {
            int missingMoney = totalCost - character.AccountBalance;
            System.Console.WriteLine($"Du har för lite pengar. Det saknas {missingMoney}kr.");
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
}