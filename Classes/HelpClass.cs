using Newtonsoft.Json; // använder oss av Newtonsoft för att kunna använda JSON för att spara/ladda data
using System;
using MarketMaster1.Classes;

public class HelpClass
{
    // Metod för att rensa köpinformationen specifikt utan att röra spelplanen
    public static void CleanTextToTheRight()
    {
        for (int i = 0; i <= 39; i++) // Radintervallet för köpinformation
        {
            AdjustTextToTheRight(i); // Justerar för att rensa texten till höger
            Console.Write(new string(' ', Console.WindowWidth - 81)); // Rensar varje rad under handlarens text
        }
    }

        public static void SaveToJson(Player player, string fileName)
    {
        string json = JsonConvert.SerializeObject(player, Formatting.Indented);
        File.WriteAllText(fileName, json);
    }
    public static Player LoadFromJson(string fileName)
    {
        if (!File.Exists(fileName))
        {
            return null;
        }

        var json = File.ReadAllText(fileName);
        var player = JsonConvert.DeserializeObject<Player>(json);

        return player;
    }

        // Detta är en metod du kan kalla på var du vill om du vill "högerjustera" texten! Du måste dock slänga in en siffra för att välja vart på y-axeln den ska hamna
    public static void AdjustTextToTheRight(int y)
    {
        Console.SetCursorPosition(81, y);
    }
    //Samma som ovan fast man skickar inparameter om x-axeln.
    public static void AdjustTextToTheBottom(int x)
    {
        Console.SetCursorPosition(x, 27);
    }
}