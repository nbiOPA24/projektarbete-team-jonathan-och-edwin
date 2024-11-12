// Jonathan jobbar här

using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Xml.Serialization;
using MarketMaster1.Classes;

public class Merchant
{

    public string Name { get; set; }
    public double MerchantAccountBalance { get; set; }

    // Lista som lagrar alla metaller som säljs av handlarn
    public List<Merchandise> ItemsForSale { get; set; }
    public static List<Merchandise> ItemsForDisplay = new List<Merchandise>();


    public Merchant(string name, double merchantAccountBalance)
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
        merchandise.Value = roundedNewPrice;
    }


    public void BuyFromCharacter(Character character)
    {
        while (true)
        {
            //Visa spelarens inventory och fråga om vilken vara kunden/användaren vill sälja.
            Character.DisplayPlayerInventory();
            System.Console.WriteLine($"{Name}-'Vad har vill du försöka sälja tillbaka idag då?'");
            System.Console.WriteLine("Ange ett nummer eller 0 för att avbryta säljet. ");
            if (!int.TryParse(Console.ReadLine(),out int itemIndex) || itemIndex < 0 || itemIndex > Character.PlayerInventory.Count)
            {
                //Skickar ett felmeddelande om användaren angivit ett felaktigt val.
                System.Console.WriteLine("Du har angett ett ogiltigt val. Testa en gång till :).");
                continue;
            }
            if (itemIndex == 0) break;

            Merchandise selectedItem = Character.PlayerInventory[itemIndex - 1];

            //Fråga hur många items användaren vill sälja till handlaren inklusive felhantering
            System.Console.WriteLine($"Hur många {selectedItem.Name} vill du sälja?");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0 || quantity > selectedItem.Quantity)
            {
                System.Console.WriteLine("Du har angett ett ogiltigt val. Testa en gång till :).");
                continue;
            }
            //Kolla om säljaren faktiskt kan köpa varorna användaren vill sälja
            double totalSaleValue = selectedItem.Value * quantity;
            if (totalSaleValue > MerchantAccountBalance)
            {
                System.Console.WriteLine($"{Name} har tyvärr inte tillräckligt med pengar i sin kassa för att genomföra köpet...");
                continue;//Avbryter transaktionen och börjar om från början.
            }
            //Formler för att genomföra transaktionen
            selectedItem.Quantity -= quantity;
            character.AccountBalance += totalSaleValue;
            MerchantAccountBalance -= totalSaleValue;

            //Justerar säljarens lagersaldo
            Merchandise merchantMetal = ItemsForSale.Find(metal => metal.Name == selectedItem.Name);
            if (merchantMetal != null)
            {
                merchantMetal.AmountAvailable += quantity;
            }
            else
            {
                ItemsForSale.Add(new Merchandise(selectedItem.Name, 
                (int)selectedItem.Value, 
                selectedItem.VolatilityNumLow, 
                selectedItem.VolatilityNumHigh, 
                selectedItem.VolatilityInAString, 
                quantity));
            }
            System.Console.WriteLine($"Du sålde {quantity} {selectedItem.Name} för {totalSaleValue} kr.");
            System.Console.WriteLine($"Ditt nya saldo är: {character.AccountBalance} kr. {Name}'s nya saldo {MerchantAccountBalance} kr.");

            if (selectedItem.Quantity == 0)
            {
                Character.PlayerInventory.Remove(selectedItem);
            }
            System.Console.WriteLine("Vill du sälja något mer eller är du nöjd? Svara ja eller nej.");
            if (Console.ReadLine()?.ToLower() != "ja")
            {
                break; //Avslutar metoden helt om användaren inte skriver ja.
            }


        }
    }




    // Nedanstående metod gör att "Merchanten" kan sälja produkter. Saker tas från "ItemsForSale" och läggs i "PlayerInventory"
    // Summa subtraheras från "AccountBalance" och summa adderas till "MerchantAccountBalance", för att visualisera flödet av pengar

    // VIKTIGT - Denna metod används för både "Merchant"-sell och "Character"-buy
    public void Sell(Character Character)
    {
        while (true)
        {
            Character.DisplayAccountBalance(Character);
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
            else if (amountOfMetal * ItemsForSale[chosenMetal - 1].Value > Character.AccountBalance)
            {
                double missingMoney = amountOfMetal * ItemsForSale[chosenMetal - 1].Value - Character.AccountBalance;
                // Market.AdjustTextToTheRight(26);
                // System.Console.WriteLine("                                                             ");
                // Market.AdjustTextToTheRight(27);
                // System.Console.WriteLine("                                                             ");
                // Market.AdjustTextToTheRight(29);
                // System.Console.WriteLine("                                                             ");
                // Market.AdjustTextToTheRight(30);
                // System.Console.WriteLine("                                                             ");
                // Market.AdjustTextToTheRight(31);
                // System.Console.WriteLine("                                                             ");
                // Market.AdjustTextToTheRight(32);
                // System.Console.WriteLine("                                                             ");
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

                double updatedAccountBalance = Character.AccountBalance - ItemsForSale[chosenMetal - 1].Value * amountOfMetal;
                Character.AccountBalance = Math.Round(updatedAccountBalance, 1); // sätter egenskapen "AccountBalance" till variabeln updatedAccountBalance, så att det uppdateras varje varv

                double updatedMerchantAccountBalance = MerchantAccountBalance + ItemsForSale[chosenMetal - 1].Value * amountOfMetal;


                Thread.Sleep(1500);
                Market.AdjustTextToTheRight(9);
                MenuClass.TypeWrite($"Ditt konto: {updatedAccountBalance}kr.");
                Market.AdjustTextToTheRight(10);
                MenuClass.TypeWrite($"Säljarens konto: {updatedMerchantAccountBalance}kr.");


                Merchandise chosenMerchandise = ItemsForSale[chosenMetal - 1];


                bool containsMetal = Character.PlayerInventory.Any(m => m.Name == ItemsForSale[chosenMetal - 1].Name);

                if (containsMetal)
                {
                    Character.PlayerInventory[chosenMetal - 1].Quantity += amountOfMetal;
                }
                else if (!containsMetal)
                {
                    Character.PlayerInventory.Add(ItemsForSale[chosenMetal - 1]);
                    Character.PlayerInventory[chosenMetal - 1].Quantity += amountOfMetal;
                }

                Thread.Sleep(1500);
                Market.AdjustTextToTheRight(12);
                MenuClass.TypeWrite($"Kolla din inventory! Nu har du köpt {ItemsForSale[chosenMetal - 1].Name}!");
                Character.AccountBalance = Character.AccountBalance - ItemsForSale[chosenMetal - 1].Value * amountOfMetal;

                Market.AdjustTextToTheRight(13);
                Thread.Sleep(1000);
                System.Console.WriteLine("Klicka [ENTER] för att fortsätta spela.");
                Console.ReadKey();
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