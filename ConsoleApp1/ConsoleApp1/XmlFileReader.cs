using ConsoleApp1;
using System;

using System.IO;

using System.Linq;

using System.Xml.Linq;


public class XmlFileReader : BaseFileReader

{

    public override string SupportedFormat => "XML";


    protected override void ParseContent(string filePath)

    {

        Console.WriteLine(" -> Opening XML stream...");


        XDocument doc = XDocument.Load(filePath);


        string rootName = doc.Root.Name.ToString();

        int nodeCount = doc.Descendants().Count();


        Console.WriteLine($" -> Root element: <{rootName}> with {nodeCount} descendant nodes.");


        string xmlString = doc.ToString();

        string preview = xmlString.Length > 100 ? xmlString.Substring(0, 100) : xmlString;


        Console.WriteLine("\n--- First 100 Characters ---");

        Console.WriteLine(preview);

        Console.WriteLine("----------------------------");

    }

}