// Jonathan jobbar här
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
public class Market
{
    public int RandomizeNumberOfRounds()
    {
        Random random = new Random();
        int numberOfRounds = random.Next(10, 21);

        return numberOfRounds;
    }
}