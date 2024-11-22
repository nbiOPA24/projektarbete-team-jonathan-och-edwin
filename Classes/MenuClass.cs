using System;
using System.Media;
using System.Runtime.InteropServices;
namespace MarketMaster1.Classes;


public class MenuClass
{
    public static void StartMenu() 
    
    {    
        //Menytexten ligger i en Array för att man ska kunna använda sig utan "Skrivmaskinseffekten" som skriver ut en bokstav åt gången med en liten paus mellan varje bokstav.
        string[] menuArray =
        [
            "****************************************",
            "   * Välkommen till Market Master! *",
            "****************************************",
            "I en värld där guld skimrar, silver lockar och platina står på spel...\n", 
            "finns det mycket som kan gå fel\n",
            "Står du redo att göra ditt drag? Var försiktig;\n", 
            "marknaden kan vara nyckfull men för den listige väntar stora vinster\n",
            "Du står vid marknadens port...\n",
            "en tyngd av mynt klirrar i fickan...\n",
            "Handlare viskar om dagens bästa fynd, men vem kan du lita på?\n",
            "'Kom och köp,' ropar en man. 'Endast de smartaste överlever här!'.\n",
            "Törs du satsa stort eller väljer du att spela försiktigt? \n",
            "Tryck [ENTER] för att äntra spelplanen..."
        ];
        
        // Här ligger kod för att kunna använda sig utav System.Media, dvs låtar och liknande direkt i konsollen.
        string audioFile = @"C:\Users\jonat\AProject\MarketMaster1\random stuff\MarketPirate.wav";
        using (SoundPlayer player = new SoundPlayer(audioFile))
        {
            player.Load();    // Laddar in ljudfilen
            player.PlayLooping();    //Spelar upp ljudet i en loop.
        }

        int j = 0;
        for (int i = 0; i < 3; i++)
        {
            Console.SetCursorPosition(35, j);
            System.Console.WriteLine(menuArray[i]);
            j++;
        }

        for (int i = 3; i < menuArray.Length; i++)
        {
            TypeWrite(menuArray[i]);
        }

    }


    //Metod för att få skrivmaskinseffekten på text. dvs en bokstav(char) i taget med 45 millisekunders mellanrum mellan varje utskrift.
    public static void TypeWrite(string text, int delay = 45)
    {
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(delay);
        }
    }

    // Metod för att rensa köpinformationen specifikt utan att röra spelplanen
    public static void CleanTextToTheRight()
    {
        for (int i = 0; i <= 39; i++) // Radintervallet för köpinformation
        {
            HelpClass.AdjustTextToTheRight(i); // Justerar för att rensa texten till höger
            Console.Write(new string(' ', Console.WindowWidth - 81)); // Rensar varje rad under handlarens text
        }
    }

    public static void EndGameScreen(Player player)
    {
        Console.Clear();
        Console.WriteLine("✦✦✦ MARKNADENS DOM ✦✦✦");
        TypeWrite("\nEfter 10 intensiva dagar på marknaden så känner du dig nu redo att påbörja din resa hem...");
        Thread.Sleep(3600);
        Console.WriteLine("Tryck på enter för att se ditt resultat!");
        Console.ReadKey();
        Thread.Sleep(3600);
        Console.Clear();

        Console.WriteLine("✦✦ Dina slutgiltiga resultat ✦✦\n");
        Thread.Sleep(3600);
        TypeWrite($"💰 Du har {player.AccountBalance}kr kvar i plånboken");

        int totalInventoryValue = 0;
        System.Console.WriteLine();
        TypeWrite("📦 Du hade kvar dessa metaller i ditt inventory:");
        System.Console.WriteLine();
        foreach (var metal in player.PlayerInventory)
        {
            System.Console.WriteLine("Namn: " + metal.Name);
            System.Console.WriteLine("Värde: " + metal.Value + "kr styck.");
            System.Console.WriteLine("Så här många har du: " + metal.QuantityInPlayerInventory);

            int sum = metal.Value * metal.QuantityInPlayerInventory;
            System.Console.WriteLine("Såhär mycket blev det värt till slut: " + sum);

            System.Console.WriteLine();
        }
        int totalWealth = player.AccountBalance + totalInventoryValue;
        TypeWrite($"🪙 Totala värdet på hela inventoryt inklusive pengar och metallvärden: {totalWealth}kr\n.");
        Thread.Sleep(1800);

        TypeWrite("Vilken rank får du?....");
        Thread.Sleep(3600);
        if (totalWealth >= 1500)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(GenerateAsciiArt("legend"));
            Thread.Sleep(900);
            Console.WriteLine("Ryktet om dina briljanta investeringar sprids som en löpeld. Du ses nu som en\n"
            + "ikon för handelshus över hela världen, och andra handlare bugar sig när du går förbi.");
            Console.ResetColor();
        }
        else if (totalWealth >= 1000)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(GenerateAsciiArt("champion"));
            Thread.Sleep(900);
            TypeWrite($"✨ Grymt jobbat! ✨");
            TypeWrite("Din förmåga att navigera marknadens vågor har belönats rikligt. Kanske är det dags\n"
            + "att köpa den där herrgården på kullarna och fira med en flaska dyr champagne!");
            Console.ResetColor();
        }
        else if (totalWealth >= 500)
        {
            Console.WriteLine(GenerateAsciiArt("mediocre"));
            Thread.Sleep(900);
            TypeWrite("💡 Helt okej insats...💡");
            TypeWrite("Du har klarat dig bra, men du märker att marknaden fortfarande har sina hemligheter.\n"
                + "Nästa gång kommer du tillbaka starkare, eller hur? 😊");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(GenerateAsciiArt("failure"));
            Thread.Sleep(900);
            TypeWrite("🤔 Ibland är det banne mig tufft...🤔");
            TypeWrite("Dina affärer har inte gått som planerat, och kanske är det dags att omvärdera\n"
            + "dina strategier. Men misströsta inte – varje mästare har en gång börjat på botten!");
            Console.ResetColor();
        }
        Console.WriteLine("\nTryck [ENTER] för att avsluta programmet.");
        Console.ReadKey();
    }

    public static string GenerateAsciiArt(string type)
    {
        switch (type)
        {
            case "legend":
                return @"

                    /$$       /$$$$$$$$  /$$$$$$  /$$$$$$$$ /$$   /$$ /$$$$$$$ 
                    | $$      | $$_____/ /$$__  $$| $$_____/| $$$ | $$| $$__  $$
                    | $$      | $$      | $$  \__/| $$      | $$$$| $$| $$  \ $$
                    | $$      | $$$$$   | $$ /$$$$| $$$$$   | $$ $$ $$| $$  | $$
                    | $$      | $$__/   | $$|_  $$| $$__/   | $$  $$$$| $$  | $$
                    | $$      | $$      | $$  \ $$| $$      | $$\  $$$| $$  | $$
                    | $$$$$$$$| $$$$$$$$|  $$$$$$/| $$$$$$$$| $$ \  $$| $$$$$$$/
                    |________/|________/ \______/ |________/|__/  \__/|_______/ 
                    ";

            case "champion":
                return @"
                     ██████╗██╗  ██╗ █████╗ ███╗   ███╗██████╗ ██╗ ██████╗ ███╗   ██╗
                    ██╔════╝██║  ██║██╔══██╗████╗ ████║██╔══██╗██║██╔═══██╗████╗  ██║
                    ██║     ███████║███████║██╔████╔██║██████╔╝██║██║   ██║██╔██╗ ██║
                    ██║     ██╔══██║██╔══██║██║╚██╔╝██║██╔═══╝ ██║██║   ██║██║╚██╗██║
                    ╚██████╗██║  ██║██║  ██║██║ ╚═╝ ██║██║     ██║╚██████╔╝██║ ╚████║
                     ╚═════╝╚═╝  ╚═╝╚═╝  ╚═╝╚═╝     ╚═╝╚═╝     ╚═╝ ╚═════╝ ╚═╝  ╚═══╝
                    ";
            case "mediocre":


                return @"
                    ___ ___    ___  ___    ____   ___      __  ____     ___ 
                    |   |   |  /  _]|   \  |    | /   \    /  ]|    \   /  _]
                    | _   _ | /  [_ |    \  |  | |     |  /  / |  D  ) /  [_ 
                    |  \_/  ||    _]|  D  | |  | |  O  | /  /  |    / |    _]
                    |   |   ||   [_ |     | |  | |     |/   \_ |    \ |   [_ 
                    |   |   ||     ||     | |  | |     |\     ||  .  \|     |
                    |___|___||_____||_____||____| \___/  \____||__|\_||_____|
                    ";
            case "failure":


                return @"
                        █████▒ ▄▄▄       ██▓ ██▓     █    ██  ██▀███  ▓█████ 
                     ▓██   ▒ ▒████▄    ▓██ ▒▓██▒     ██  ▓██▒▓██ ▒ ██▒▓█   ▀ 
                    ▒████ ░ ▒██  ▀█▄  ▒██▒ ▒██░    ▓██  ▒██░▓██ ░▄█ ▒▒███   
                    ░▓█▒  ░ ░██▄▄▄▄██ ░██░ ▒██░    ▓▓█  ░██░▒██▀▀█▄  ▒▓█  ▄ 
                    ░▒█░     ▓█   ▓██▒░██░ ░██████▒▒▒█████▓ ░██▓ ▒██▒░▒████▒
                     ▒ ░     ▒▒   ▓▒█░░▓   ░ ▒░▓  ░░▒▓▒ ▒ ▒ ░ ▒▓ ░▒▓░░░ ▒░ ░
                    ░        ▒   ▒▒ ░ ▒ ░░  ░ ▒  ░░░▒░ ░ ░   ░▒ ░ ▒░ ░ ░  ░
                     ░ ░      ░   ▒    ▒ ░   ░ ░    ░░░ ░ ░   ░░   ░    ░   
                                  ░  ░ ░      ░  ░   ░        ░        ░  ░
                    ";
            default:
                return "";
        }
    }
}


