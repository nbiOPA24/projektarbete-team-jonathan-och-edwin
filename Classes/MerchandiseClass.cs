// Jonathan jobbar här

public class Merchandise
{
    public string Name { get; set; } 
    public double Value { get; set; } 
    public double VolatilityNumLow { get; set; } // Används för att räkna ut nytt pris på valfri ädelmetall, detta används som den lägre siffran för att räkna ut procent
    public double VolatilityNumHigh { get; set; } // Används för att räkna ut nytt pris på valfri ädelmetall, detta används som den högre siffran för att räkna ut procent
    public string VolatilityInAString {get; set;} // för att visa användaren hur mycket/lite en metall svänger i pris
    public int AmountAvailable { get; set; }
    public int Quantity { get; set; }
    //Konstruktor för Varor/Metaller.
    public Merchandise(string name, int value, double volatilityNumLow, double volatilityNumHigh, string volatilityInAString, int amountAvailable)
    {
        Name = name;
        Value = value;
        VolatilityNumLow = volatilityNumLow;
        VolatilityNumHigh = volatilityNumHigh;
        VolatilityInAString = volatilityInAString;
        AmountAvailable = amountAvailable;
        Quantity = 0;   
    }

    // Såhär representeras en ädelmetall när vi "skriver ut" klassen
    public override string ToString()
    {
        return $"Metall: {Name}\nVärde: {Value}\nMängd hos handlare: {AmountAvailable}\nMängd i spelarens inventory: {Quantity} st.";
    }
}