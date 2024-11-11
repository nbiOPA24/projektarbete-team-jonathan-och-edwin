// Jonathan jobbar här

using System;
using System.Collections.Generic;
public class Merchant
{

    public string Name { get; set; }
    public int XPos { get; set; } // försäljarens placering på spelplanens x-axel
    public int YPos { get; set; } // försäljarens placering på spelplanens y-axel

    public double MerchantAccountBalance { get; set; }

    // Lista som lagrar alla metaller som säljs av handlarn
    public List<Merchandise> ItemsForSale { get; set; }


    public Merchant(string name, int xPos, int yPos, double merchantAccountBalance)
    {
        Name = name;
        XPos = xPos;
        YPos = yPos;
        ItemsForSale = new List<Merchandise>();
        MerchantAccountBalance = merchantAccountBalance;
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
    public void UpdatePrice()
    {
        foreach (var i in ItemsForSale)
        {
            Random random = new Random();
            double randomValue = RandomizePercentage(random, i.VolatilityNumLow, i.VolatilityNumHigh);
            double newPrice = randomValue * i.Value;
            System.Console.WriteLine($"Det nya priset för {i.Name} är {newPrice}.");
        }
    }




    // Nedanstående metod gör att "Merchanten" kan sälja produkter. Saker tas från "ItemsForSale" och läggs i "PlayerInventory"
    // Summa subtraheras från "AccountBalance" och summa adderas till "MerchantAccountBalance", för att visualisera flödet av pengar

    // VIKTIGT - Denna metod används för både "Merchant"-sell och "Character"-buy
    public void Sell()
    {
        while (true)
        {


            DisplayAllItems();
            Market.AdjustTextToTheRight(20);
            System.Console.WriteLine("Vilken ädelmetall vill du köpa?");
            Market.AdjustTextToTheRight(21);
            System.Console.WriteLine("Skriv 1-4 beroende plats på listan.");
            Market.AdjustTextToTheRight(23);
            System.Console.WriteLine("Ditt svar (ange 1-4): ");
            Market.AdjustTextToTheRight(24);
            int chosenMetal = int.Parse(Console.ReadLine());

            if (chosenMetal < 1 || chosenMetal > 4)
            {
                Market.AdjustTextToTheRight(28);
                System.Console.WriteLine("Välj en siffra mellan 1-4.");
                continue;
            }



            Market.AdjustTextToTheRight(26);
            System.Console.WriteLine($"Okej! Du vill köpa {ItemsForSale[chosenMetal - 1].Name}!");
            Market.AdjustTextToTheRight(27);
            System.Console.WriteLine("Vi får se om det är ett bra köp...");
            Market.AdjustTextToTheRight(29);
            System.Console.WriteLine($"Hur många {ItemsForSale[chosenMetal - 1].Name} vill du köpa?");
            Market.AdjustTextToTheRight(30);
            System.Console.WriteLine($"Just nu finns {ItemsForSale[chosenMetal - 1].AmountAvailable} kvar i lager.");
            Market.AdjustTextToTheRight(31);
            System.Console.WriteLine("Ditt svar (går ej att köpa fler än det finns i lager): ");
            Market.AdjustTextToTheRight(32);

            int amountOfMetal = int.Parse(Console.ReadLine());

            // Kontrollerar att det finns tillräckligt många i lager...
            if (amountOfMetal > ItemsForSale[chosenMetal - 1].AmountAvailable)
            {
                Market.AdjustTextToTheRight(34);
                System.Console.WriteLine("Du har köpt för många...");
                Market.AdjustTextToTheRight(35);
                System.Console.WriteLine($"Just nu har vi bara {ItemsForSale[chosenMetal - 1].AmountAvailable} {ItemsForSale[chosenMetal - 1].Name} i lager.");
                continue;
            }

            // Kontrollerar att värdet på mängden metall användaren köper inte överstiger användarens kontobalans
            else if (amountOfMetal * ItemsForSale[chosenMetal - 1].Value > Character.AccountBalance)
            {
                double missingMoney = amountOfMetal * ItemsForSale[chosenMetal - 1].Value - Character.AccountBalance;
                Market.AdjustTextToTheRight(34);
                System.Console.WriteLine($"Du har för lite pengar. Det saknas tyvärr {missingMoney}kr för att du ska ha råd.");
                continue;
            }

            else
            {
                Market.AdjustTextToTheRight(34);
                System.Console.WriteLine($"Okej, du vill köpa {amountOfMetal} st. {ItemsForSale[chosenMetal - 1].Name}.");
                Market.AdjustTextToTheRight(35);
                System.Console.WriteLine($"Den kostar just nu {ItemsForSale[chosenMetal - 1].Value}.");

                int amountLeft = ItemsForSale[chosenMetal - 1].AmountAvailable - amountOfMetal;
                ItemsForSale[chosenMetal - 1].AmountAvailable = amountLeft;

                double updatedAccountBalance = Character.AccountBalance - ItemsForSale[chosenMetal - 1].Value * amountOfMetal;
                double updatedMerchantAccountBalance = MerchantAccountBalance + ItemsForSale[chosenMetal - 1].Value * amountOfMetal;

                Market.AdjustTextToTheRight(36);
                System.Console.WriteLine($"Ditt konto: {updatedAccountBalance}kr");
                Market.AdjustTextToTheRight(37);
                System.Console.WriteLine($"Säljarens konto: {updatedMerchantAccountBalance}kr");

                Character.PlayerInventory.Add(ItemsForSale[chosenMetal - 1]);
                Market.AdjustTextToTheRight(38);
                System.Console.WriteLine($"Kolla din inventory! Nu har du köpt {ItemsForSale[chosenMetal - 1].Name}!");

                // Vänta på knapptryckning för att låta spelaren läsa köpinformationen
                Market.AdjustTextToTheRight(39);
                Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                Console.ReadKey();

                // Rensa köpinformationen
                CleanTextToTheRight();
                break;


            }
        }
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

    public static void DisplayDetailedProductInfo()
    {
        int x = 0; // används för att kolla varje index i itemsfordisplay-listan

        foreach (var i in Program.ItemsForDisplay)
        {
            Market.AdjustTextToTheRight(0);
            System.Console.WriteLine($"Metall: {Program.ItemsForDisplay[x].Name}");
            Market.AdjustTextToTheRight(2);
            System.Console.WriteLine($"Värde denna runda: {Program.ItemsForDisplay[x].Value}kr");
            Market.AdjustTextToTheRight(4);
            System.Console.WriteLine($"Prisförändring varje runda: {Program.ItemsForDisplay[x].VolatilityInAString}");
            x++;
        }
    }
}