using ConsoleApp1;
using System;

using System.IO;

using System.Linq;


public class CsvFileReader : BaseFileReader

{

    public override string SupportedFormat => "CSV";


    protected override void ParseContent(string filePath)

    {

        Console.WriteLine(" -> Opening CSV stream...");


        var lines = File.ReadAllLines(filePath);


        int rowCount = lines.Length;

        int columnCount = 0;


        if (rowCount > 0)

        {

            columnCount = lines[0].Split(',').Length;

        }


        Console.WriteLine($" -> Detected {rowCount} rows and {columnCount} columns.");


        string raw = File.ReadAllText(filePath);

        string preview = raw.Length > 100 ? raw.Substring(0, 100) : raw;


        Console.WriteLine("\n--- First 100 Characters ---");

        Console.WriteLine(preview);

        Console.WriteLine("----------------------------");

    }

}