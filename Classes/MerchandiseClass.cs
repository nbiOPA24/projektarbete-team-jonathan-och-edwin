// Jonathan jobbar här

public class Merchandise
{
    public string Name { get; set; }
    public double Value { get; set; }
    public double VolatilityNumLow { get; set; } // Används för att räkna ut nytt pris på valfri ädelmetall, detta används som den lägre siffran för att räkna ut procent
    public double VolatilityNumHigh { get; set; } // Används för att räkna ut nytt pris på valfri ädelmetall, detta används som den högre siffran för att räkna ut procent
    public int AmountAvailable { get; set; }
    public int Quantity { get; set; }
    public Merchandise(string name, int value, double volatilityNumLow, double volatilityNumHigh, int amountAvailable)
    {
        Name = name;
        Value = value;
        VolatilityNumLow = volatilityNumLow;
        VolatilityNumHigh = volatilityNumHigh;
        AmountAvailable = amountAvailable;
        Quantity = 0;
    }

    // Såhär representeras en ädelmetall när vi "skriver ut" klassen
    public override string ToString()
    {
        return $"Metall: {Name}\nVärde: {Value}\nMängd hos handlare: {AmountAvailable}\nMängd i spelarens inventory: {Quantity} st.";
    }

}