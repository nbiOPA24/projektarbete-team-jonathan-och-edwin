using Newtonsoft.Json; // använder oss av Newtonsoft för att kunna använda JSON för att spara/ladda data


public class HelpClass
{
    // Metod för att rensa köpinformationen specifikt utan att röra spelplanen
    public static void CleanTextToTheRight()
    {
        for (int i = 0; i <= 39; i++) // Radintervallet för köpinformation
        {
            Market.AdjustTextToTheRight(i); // Justerar för att rensa texten till höger
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
}