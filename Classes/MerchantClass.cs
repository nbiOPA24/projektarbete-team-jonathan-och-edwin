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
            Market.AdjustTextToTheRight(2);
            System.Console.WriteLine("Vilken ädelmetall vill du köpa?");
            Market.AdjustTextToTheRight(3);
            System.Console.WriteLine("Skriv 1-4 beroende plats på listan.");
            
            DisplayAllItems();
            System.Console.WriteLine();
            int chosenMetal = int.Parse(Console.ReadLine());

            if (chosenMetal < 1 || chosenMetal > 4)
            {
                Market.AdjustTextToTheRight(28);
                System.Console.WriteLine("Välj en siffra mellan 1-4.");
                continue;
            }



            Market.AdjustTextToTheRight(20);
            System.Console.WriteLine($"Okej! Du vill köpa {ItemsForSale[chosenMetal - 1].Name}!");
            Market.AdjustTextToTheRight(21);
            System.Console.WriteLine("Vi får se om det är ett bra köp...");
            Market.AdjustTextToTheRight(22);
            System.Console.WriteLine($"Hur många {ItemsForSale[chosenMetal - 1].Name} vill du köpa?");

            int amountOfMetal = int.Parse(Console.ReadLine());

            // Kontrollerar att det finns tillräckligt många i lager...
            if (amountOfMetal > ItemsForSale[chosenMetal - 1].AmountAvailable)
            {
                System.Console.WriteLine("Du har köpt för många...");
                System.Console.WriteLine($"Just nu har vi bara {ItemsForSale[chosenMetal - 1].AmountAvailable} {ItemsForSale[chosenMetal - 1].Name} i lager.");
                continue;
            }

            // Kontrollerar att värdet på mängden metall användaren köper inte överstiger användarens kontobalans
            else if (amountOfMetal * ItemsForSale[chosenMetal - 1].Value > Character.AccountBalance)
            {
                double missingMoney = amountOfMetal * ItemsForSale[chosenMetal - 1].Value - Character.AccountBalance;
                System.Console.WriteLine($"Du har för lite pengar. Det saknas tyvärr {missingMoney}kr för att du ska ha råd.");
                continue;
            }

            else
            {
                System.Console.WriteLine($"Okej, du vill köpa {amountOfMetal} st. {ItemsForSale[chosenMetal - 1].Name}.");
                System.Console.WriteLine($"Den kostar just nu {ItemsForSale[chosenMetal - 1].Value}.");

                int amountLeft = ItemsForSale[chosenMetal - 1].AmountAvailable - amountOfMetal;
                ItemsForSale[chosenMetal - 1].AmountAvailable = amountLeft;

                double updatedAccountBalance = Character.AccountBalance - ItemsForSale[chosenMetal - 1].Value * amountOfMetal;
                double updatedMerchantAccountBalance = MerchantAccountBalance + ItemsForSale[chosenMetal - 1].Value * amountOfMetal;

                System.Console.WriteLine($"Ditt konto: {updatedAccountBalance}kr");
                System.Console.WriteLine($"Säljarens konto: {updatedMerchantAccountBalance}kr");

                Character.PlayerInventory.Add(ItemsForSale[chosenMetal - 1]);
                System.Console.WriteLine($"Kolla din inventory! Nu har du köpt {ItemsForSale[chosenMetal - 1].Name}!");
                break;
            }
        }
    }


}