using ConsoleApp1;
using System;

using System.IO;

using System.Text.Json;


public class JsonFileReader : BaseFileReader

{

    public override string SupportedFormat => "JSON";


    protected override void ParseContent(string filePath)

    {

        Console.WriteLine(" -> Opening JSON stream...");


        string jsonString = File.ReadAllText(filePath);


        using JsonDocument doc = JsonDocument.Parse(jsonString);


        int count = 0;


        if (doc.RootElement.ValueKind == JsonValueKind.Object)

        {

            count = doc.RootElement.EnumerateObject().Count();

        }

        else if (doc.RootElement.ValueKind == JsonValueKind.Array)

        {

            count = doc.RootElement.EnumerateArray().Count();

        }


        Console.WriteLine($" -> Parsed {count} root elements.");


        string preview = jsonString.Length > 100 ? jsonString.Substring(0, 100) : jsonString;


        Console.WriteLine("\n--- First 100 Characters ---");

        Console.WriteLine(preview);

        Console.WriteLine("----------------------------");

    }

}