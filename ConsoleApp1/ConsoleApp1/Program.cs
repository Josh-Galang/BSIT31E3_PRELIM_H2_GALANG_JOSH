/// <summary>
/// Entry point for the Interactive File Ingestion Engine.
/// Prompts the user for a file path and format, resolves the appropriate reader,
/// and processes the file.
/// </summary>
using ConsoleApp1;

Console.WriteLine("=== Interactive File Ingestion Engine ===");

Console.Write("Enter the full file path to read: ");
string filePath = Console.ReadLine();

if (!File.Exists(filePath))
{
    Console.WriteLine("[ERROR] File not found.");
    return;
}

Console.WriteLine("\nSelect format: A (.txt) | B (.csv) | C (.json) | D (.xml)");
Console.Write("Choice: ");
string choice = Console.ReadLine()?.Trim().ToUpper();

string targetFormat = choice switch
{
    "A" => "TXT",
    "B" => "CSV",
    "C" => "JSON",
    "D" => "XML",
    _ => string.Empty,
};

FileReaderResolver resolver = new FileReaderResolver();
IFileReader activeReader = resolver.GetReaderForFormat(targetFormat);

if (activeReader == null)
{
    Console.WriteLine($"[CRITICAL] Resolver could not find a reader for format '{targetFormat}'.");
    return;
}

if (!activeReader.CanRead(filePath))
{
    string extension = Path.GetExtension(filePath);
    Console.WriteLine($"[REJECTED] The resolved reader does not support {extension} files.");
    return;
}

Console.WriteLine("\n--------------------------------------------------");
activeReader.ProcessFile(filePath);
Console.WriteLine("--------------------------------------------------");
