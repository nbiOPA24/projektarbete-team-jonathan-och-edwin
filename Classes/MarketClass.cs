// Jonathan jobbar här
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;
public class Market
{

    public int Height { get; set; }
    public int Width { get; set; }
    private char[,] market;

    public Market(int height, int width)
    {
        Height = height;
        Width = width;
    }

    public void DisplayMarket()
    {
        for (int x = 0; x < Width; x++)
        {
            System.Console.Write("-");
        }

        System.Console.WriteLine();

        for (int z = 0; z < 40; z++)
        {
            System.Console.Write("|");

            for (int u = 0; u < 1; u++)
            {
                System.Console.WriteLine("                                                                              |");
            }
        }

        for (int x = 0; x < Width; x++)
        {
            System.Console.Write("-");
        }

        System.Console.WriteLine();
        System.Console.WriteLine("");
    }
    public int RandomizeNumberOfRounds()
    {
        Random random = new Random();
        int numberOfRounds = random.Next(10, 21);

        return numberOfRounds;
    }

    public static void PlaceMerchantsBuilding(int xPos, int yPos)
    {
        Console.SetCursorPosition(xPos, yPos);

        for (int x = 0; x < 12; x++)
        {
            System.Console.Write("-");
        }
    }

    public static void PlaceDecoration(int xPos, int yPos)
    {
        Console.SetCursorPosition(xPos, yPos);

        for (int x = 0; x < 10; x++)
        {
            Console.Write("🪙");
        }
    }

    
}