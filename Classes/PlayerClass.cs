// Jonathan jobbar här

using System;
using System.Drawing;
using MarketMaster1.Classes;

public class Player
{
    // Deklarerar Namn, Pengar på användarens konto samt skapar en lista för användarens inventory
    public string Name { get; set; }
    public int AccountBalance { get; set; }
    public List<Merchandise> PlayerInventory { get; set; }

    public Player()
    {
        Name = "Busiga investeraren";
        AccountBalance = 1000;
        PlayerInventory = new List<Merchandise>();
    }



    public static void AddToInventory(Player player, Merchandise item, int quantity)
    {
        item.QuantityInPlayerInventory = quantity;
        player.PlayerInventory.Add(item);
    }

    // Visar spelarens inventory
    // Jag valde att skicka in en parameter "controlNr". Skickar man in 0 så får man utan extra texten under inventoryt, alla andra siffror så får du inventoryt som det ska vara
    public void DisplayPlayerInventory(Player player, int controlNr)
    {
        Console.SetCursorPosition(81, 1);
        System.Console.WriteLine("╔══════════════════════════ Spelarens Inventory ══════════════════════════╗");
        Console.SetCursorPosition(81, Console.CursorTop);
        System.Console.WriteLine("║    Item Name     ║ Quantity ║    Värde   ║Totalt värde ║   Volatilitet  ║");
        Console.SetCursorPosition(81, Console.CursorTop);
        System.Console.WriteLine("║------------------║----------║------------║-------------║----------------║");

        int indexInInventory = 1;

        foreach (var metal in player.PlayerInventory)
        {
            double totalValue = metal.Value * metal.QuantityInPlayerInventory; //totala värdet av varje item gånger antalet av varje item
            Console.SetCursorPosition(81, Console.CursorTop);
            //Nedan skriver vi ut alla metaller/items i spelarens inventory med namn, antal, värde, totalt värde samt vilken volatilitet varje item har.
            System.Console.WriteLine($"║ {indexInInventory}. {metal.Name,-13} ║ {metal.QuantityInPlayerInventory,8} ║ {metal.Value,10:F2} ║ {totalValue,11:F2} ║ {metal.VolatilityNumLow * 100,6:F0}% - {metal.VolatilityNumHigh * 100,3:F0}% ║");
            indexInInventory++;
        }

        Console.SetCursorPosition(81, Console.CursorTop);
        System.Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════╝");
        Console.SetCursorPosition(81, Console.CursorTop);

        if (controlNr == 0)
        {
            return;
        }
        else
        {
            Console.WriteLine("Klicka på valfri knapp för att få bort inventoryt.");
            Console.ReadKey(true);

            //Rensar texten som är bredvid spelplanen
            MenuClass.CleanTextToTheRight();
            return;
        }


    }

    public void DisplayAccountBalance(Player player) //Visar spelarens account balance/hur mycket pengar som finns på ens konto
    {
        Console.SetCursorPosition(81, 1);
        System.Console.Write("╔═══════════════════════╗");
        Console.SetCursorPosition(81, 2);
        System.Console.WriteLine($"║ Kontobalans: {player.AccountBalance}kr");
        Console.SetCursorPosition(105, 2);
        System.Console.WriteLine("║");
        Console.SetCursorPosition(81, 3);
        System.Console.Write("╚═══════════════════════╝");
    }

    // Låter spelaren sälja saker,  de tas bort från "PlayerInventory" och läggs till i "ItemsForSale" som är en icke statisk lista som tillhör en specifik merchant
    public void Sell(Player player, Merchant merchant)
    {
        while (true)
        {
            player.DisplayPlayerInventory(player, 0);
            HelpClass.AdjustTextToTheRight(Console.CursorTop);
            System.Console.WriteLine("Vad vill du sälja (ange endast siffran)?");
            HelpClass.AdjustTextToTheRight(Console.CursorTop);
            int itemToSell = int.Parse(Console.ReadLine());

            if (!merchant.ItemsForSale.Any(item => item.Name == player.PlayerInventory[itemToSell - 1].Name))
            {
                HelpClass.AdjustTextToTheRight(Console.CursorTop);
                System.Console.WriteLine("Jag kan tyvärr inte handla med såna där varor...");
                HelpClass.AdjustTextToTheRight(Console.CursorTop);
                System.Console.WriteLine("Tryck [ENTER] för att gå vidare...");
                Console.ReadKey(true);
                HelpClass.CleanTextToTheRight();
                return;
            }
            else if (merchant.ItemsForSale.Any(item => item.Name == player.PlayerInventory[itemToSell - 1].Name))
            {
                HelpClass.AdjustTextToTheRight(Console.CursorTop);
                System.Console.WriteLine($"Hur många {PlayerInventory[itemToSell - 1].Name} vill du sälja?");
                HelpClass.AdjustTextToTheRight(Console.CursorTop);
                int amountToSell = int.Parse(Console.ReadLine());
                HelpClass.AdjustTextToTheRight(Console.CursorTop);

                int valueOfSoldItems = PlayerInventory[itemToSell - 1].Value * amountToSell;
                double amountInAccount = player.AccountBalance + valueOfSoldItems;

                if (amountToSell == PlayerInventory[itemToSell - 1].QuantityInPlayerInventory)
                {
                    HelpClass.CleanTextToTheRight();
                    HelpClass.AdjustTextToTheRight(0);
                    System.Console.WriteLine($"Du sålde alla dina {PlayerInventory[itemToSell - 1].Name}.");
                    HelpClass.AdjustTextToTheRight(1);
                    System.Console.WriteLine($"Ditt konto: {amountInAccount}kr");
                    HelpClass.AdjustTextToTheRight(2);
                    PlayerInventory.RemoveAt(itemToSell - 1);


                    System.Console.WriteLine("Klicka på [ENTER] för att komma vidare");
                    Console.ReadKey(true);

                    HelpClass.CleanTextToTheRight();
                    HelpClass.SaveToJson(player, "JsonHandler.json");

                    return;
                }
                else if (amountToSell < PlayerInventory[itemToSell - 1].QuantityInPlayerInventory)
                {
                    int amountLeft = PlayerInventory[itemToSell - 1].QuantityInPlayerInventory - amountToSell;
                    HelpClass.CleanTextToTheRight();
                    HelpClass.AdjustTextToTheRight(0);
                    System.Console.WriteLine($"Du sålde {amountToSell} {PlayerInventory[itemToSell - 1].Name}.");
                    HelpClass.AdjustTextToTheRight(1);
                    System.Console.WriteLine($"Detta gör att du har {amountLeft} kvar.");
                    HelpClass.AdjustTextToTheRight(2);


                    System.Console.WriteLine("Klicka på [ENTER] för att komma vidare");
                    Console.ReadKey(true);

                    HelpClass.CleanTextToTheRight();
                    HelpClass.SaveToJson(player, "JsonHandler.json");
                    return;
                }
                else if (amountToSell < 0 || amountToSell > PlayerInventory[itemToSell - 1].QuantityInPlayerInventory)
                {
                    HelpClass.CleanTextToTheRight();
                    HelpClass.AdjustTextToTheRight(Console.CursorTop);
                    System.Console.WriteLine("Du kan inte sälja fler än du har... eller hur?");
                    HelpClass.AdjustTextToTheRight(Console.CursorTop);
                    System.Console.WriteLine("Du får några chanser till att sälja... ;)");
                    HelpClass.AdjustTextToTheRight(Console.CursorTop);
                    System.Console.WriteLine("Klicka på [ENTER] för att försöka igen");
                    Console.ReadKey(true);
                    continue;
                }
            }
        }
    }

    public void Buy(Player player, Merchant merchant)
    {
        while (true)
        {
            // GetUserSelection(); // kallar inte på denna ännu

            merchant.DisplayAllItems();
            player.DisplayAccountBalance(player);

            HelpClass.AdjustTextToTheRight(17);
            System.Console.WriteLine("Vilken ädelmetall vill du köpa (1-4)?");

            HelpClass.AdjustTextToTheRight(18);
            int chosenMetal = int.Parse(Console.ReadLine()) - 1;

            HelpClass.AdjustTextToTheRight(19);
            System.Console.WriteLine("Hur många vill du köpa?");

            HelpClass.AdjustTextToTheRight(20);
            int amountOfMetal = int.Parse(Console.ReadLine());

            if (!merchant.ValidatePurchase(chosenMetal, amountOfMetal, player))
            {
                Console.ReadKey();
                return;
            }

            if (merchant.ValidatePurchase(chosenMetal, amountOfMetal, player))
            {
                HelpClass.CleanTextToTheRight();
                HelpClass.AdjustTextToTheRight(6);
                MenuClass.TypeWrite($"Okej, du köpte {amountOfMetal} st. {merchant.ItemsForSale[chosenMetal].Name}."); // visar hur många du köpt av vad
                HelpClass.AdjustTextToTheRight(7);
                MenuClass.TypeWrite($"Denna kostade {merchant.ItemsForSale[chosenMetal].Value}kr/st."); // visar vad det kostade

                int amountLeft = merchant.ItemsForSale[chosenMetal].AmountAvailableAtMerchant - amountOfMetal;
                merchant.ItemsForSale[chosenMetal].AmountAvailableAtMerchant = amountLeft;

                int updatedAccountBalance = player.AccountBalance - merchant.ItemsForSale[chosenMetal].Value * amountOfMetal;
                player.AccountBalance = updatedAccountBalance; // sätter egenskapen "AccountBalance" till variabeln updatedAccountBalance, så att det uppdateras varje varv

                double updatedMerchantAccountBalance = merchant.MerchantAccountBalance + merchant.ItemsForSale[chosenMetal].Value * amountOfMetal;

                merchant.ItemsForSale[chosenMetal].QuantityInPlayerInventory += amountOfMetal;

                Thread.Sleep(1500);
                HelpClass.AdjustTextToTheRight(9);
                MenuClass.TypeWrite($"Ditt konto: {updatedAccountBalance}kr.");
                HelpClass.AdjustTextToTheRight(10);
                MenuClass.TypeWrite($"Säljarens konto: {updatedMerchantAccountBalance}kr.");

                // denna bool är skriven för att se om metallen redan finns i spelarens inventory, isåfall vill vi ju bara plusa på quantity och inte lägga till en extra sådan
                bool containsMetal = player.PlayerInventory.Any(m => m.Name == merchant.ItemsForSale[chosenMetal].Name);

                if (containsMetal)
                {
                    // Hitta objektet i inventoryt och uppdatera mängden
                    var metal = player.PlayerInventory.First(m => m.Name == merchant.ItemsForSale[chosenMetal].Name);
                    metal.QuantityInPlayerInventory += amountOfMetal;
                    HelpClass.SaveToJson(player, "JsonHandler.json");
                }
                else
                {
                    // Lägg till objektet i inventoryt (referens, inte kopia)
                    var metalToAdd = merchant.ItemsForSale[chosenMetal];
                    player.PlayerInventory.Add(metalToAdd); // Detta garanterar att inventoryt får samma referens som marknaden.
                    HelpClass.SaveToJson(player, "JsonHandler.json");
                }


                Thread.Sleep(1500);
                HelpClass.AdjustTextToTheRight(12);
                MenuClass.TypeWrite($"Kolla din inventory! Nu har du köpt {merchant.ItemsForSale[chosenMetal].Name}!");
                player.AccountBalance = player.AccountBalance - merchant.ItemsForSale[chosenMetal].Value * amountOfMetal;

                HelpClass.AdjustTextToTheRight(13);
                Thread.Sleep(1000);
                System.Console.WriteLine("Klicka [ENTER] för att fortsätta spela.");
                Console.ReadKey();
                HelpClass.CleanTextToTheRight();

                break;
            }
        }
    }
    // public void Sell(Player player, Merchant merchant, List<Merchandise> itemsForSale)
    // {
    //     while (true)
    //     {
    //         //Visa spelarens inventory och fråga om vilken vara kunden/användaren vill sälja.
    //         player.DisplayPlayerInventory(player, 0);
    //         System.Console.WriteLine($"{Name}-'Vad har vill du försöka sälja tillbaka idag då?'");
    //         System.Console.WriteLine("Ange ett nummer eller 0 för att avbryta säljet. ");
    //         if (!int.TryParse(Console.ReadLine(), out int itemIndex) || itemIndex < 0 || itemIndex > player.PlayerInventory.Count)
    //         {
    //             //Skickar ett felmeddelande om användaren angivit ett felaktigt val.
    //             System.Console.WriteLine("Du har angett ett ogiltigt val. Testa en gång till :).");
    //             continue;
    //         }
    //         if (itemIndex == 0) break;

    //         Merchandise selectedItem = player.PlayerInventory[itemIndex - 1];

    //         //Fråga hur många items användaren vill sälja till handlaren inklusive felhantering
    //         System.Console.WriteLine($"Hur många {selectedItem.Name} vill du sälja?");
    //         if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0 || quantity > selectedItem.QuantityInPlayerInventory)
    //         {
    //             System.Console.WriteLine("Du har angett ett ogiltigt val. Testa en gång till :).");
    //             continue;
    //         }
    //         //Kolla om säljaren faktiskt kan köpa varorna användaren vill sälja
    //         int totalSaleValue = selectedItem.Value * quantity;

    //         if (totalSaleValue > merchant.MerchantAccountBalance)
    //         {
    //             System.Console.WriteLine($"{Name} har tyvärr inte tillräckligt med pengar i sin kassa för att genomföra köpet...");
    //             continue;//Avbryter transaktionen och börjar om från början.
    //         }
    //         //Formler för att genomföra transaktionen
    //         selectedItem.QuantityInPlayerInventory -= quantity;
    //         player.AccountBalance += totalSaleValue;
    //         merchant.MerchantAccountBalance -= totalSaleValue;

    //         //Justerar säljarens lagersaldo med all nödvändig info.
    //         Merchandise merchantMetal = merchant.ItemsForSale.Find(metal => metal.Name == selectedItem.Name); //Söker efter om varan redan finns genom att använda "Find" metoden.
    //         if (merchantMetal != null)
    //         {
    //             merchantMetal.AmountAvailableAtMerchant += quantity; //Om varan redan finns i säljarens inventory så lägger vi till så många items som köpts från användaren/spelaren.
    //         }
    //         else
    //         {   //Finns inte varan så lägger vi bara till varan samt all info om den.
    //             itemsForSale.Add
    //             (new Merchandise(selectedItem.Name, (int)selectedItem.Value, selectedItem.VolatilityNumLow, selectedItem.VolatilityNumHigh, selectedItem.VolatilityInAString, selectedItem.AmountAvailableAtMerchant, selectedItem.QuantityInPlayerInventory));
    //         }
    //         //Skriver ut text för vilken vara användaren sålt, samt antalet och för hur mycket.  
    //         System.Console.WriteLine($"Du sålde {quantity} {selectedItem.Name} för {totalSaleValue} kr.");
    //         //Visar uppdaterat saldo och handlarens saldo.
    //         System.Console.WriteLine($"Ditt nya saldo är: {player.AccountBalance} kr. {Name}'s nya saldo {merchant.MerchantAccountBalance} kr.");

    //         if (selectedItem.QuantityInPlayerInventory == 0)//Om man inte har någon vara kvar av den typen(tex guld) så tas den bort ur ens inventory.
    //         {
    //             player.PlayerInventory.Remove(selectedItem);
    //         }
    //         System.Console.WriteLine("Vill du sälja något mer eller är du nöjd? Svara ja eller nej.");
    //         if (Console.ReadLine()?.ToLower() != "ja")
    //         {
    //             break; //Avslutar metoden helt om användaren inte skriver ja.
    //         }


    //     }




    // public void UpdateInventoryPrices(Merchant merchant)
    // {
    //     foreach (var m in PlayerInventory)
    //     {
    //         merchant.UpdatePrice(m);
    //     }
    // }

    // public static string CheckForBankruptcy()
    // {
    //     if (player.AccountBalance < 0)
    //     {
    //         string text = "Tyvärr... du kan inte gå in i en ny dag. Du saknar pengar...";
    //         return text;
    //     }

    //     string text1 = "Du har pengar, välkommen in!";
    //     return text1;
    // }
}