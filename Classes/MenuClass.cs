using System;
using System.Media;
namespace MarketMaster1.Classes;


public class MenuClass
{
    // public static void StartMenu() //Startmeny som är det första som visas.
    // {    //Menytexten ligger i en Array för att man ska kunna använda sig utan "Skrivmaskinseffekten" som skriver ut en bokstav åt gången med en liten paus mellan varje bokstav.
    //     string[] menuArray =
    //     [
    //         "****************************************",
    //         "   * Välkommen till Market Master! *",
    //         "****************************************",
    //         "I en värld där guld skimrar, silver lockar och platina står på spel...\n", 
    //         "finns det mycket som kan gå fel\n",
    //         "Står du redo att göra ditt drag? Var försiktig;\n", 
    //         "marknaden kan vara nyckfull men för den listige väntar stora vinster\n",
    //         "Du står vid marknadens port...\n",
    //         "en tyngd av mynt klirrar i fickan...\n",
    //         "Handlare viskar om dagens bästa fynd, men vem kan du lita på?\n",
    //         "'Kom och köp,' ropar en man. 'Endast de smartaste överlever här!'.\n",
    //         "Törs du satsa stort eller väljer du att spela försiktigt? \n",
    //         "Tryck [ENTER] för att äntra spelplanen..."
    //     ];
            //Här ligger kod för att kunna använda sig utav System.Media, dvs låtar och liknande direkt i konsollen.
    //     string audioFile = @"C:\Users\jonat\AProject\MarketMaster1\MarketPirate.wav";
    //     using (SoundPlayer player = new SoundPlayer(audioFile))
    //     {
    //         player.Load();    // Laddar in ljudfilen
    //         player.PlayLooping();    //Spelar upp ljudet i en loop.
    //     }

    //     int j = 0;
    //     for (int i = 0; i < 3; i++)
    //     {
    //         Console.SetCursorPosition(35, j);
    //         System.Console.WriteLine(menuArray[i]);
    //         j++;
    //     }
        
    //     for (int i = 3; i < menuArray.Length; i++)
    //     {
    //         TypeWrite(menuArray[i]);
    //     }

    // }


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

}

