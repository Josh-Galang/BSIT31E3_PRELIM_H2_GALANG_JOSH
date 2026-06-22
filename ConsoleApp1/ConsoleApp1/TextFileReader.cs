namespace ConsoleApp1;

/// <summary>
/// Reads and displays the contents of plain text (.txt) files.
/// </summary>
public class TextFileReader : BaseFileReader
{
    /// <summary>
    /// Gets the supported format identifier for this reader.
    /// </summary>
    public override string SupportedFormat => "TXT";

    /// <summary>
    /// Reads the entire text file, counts the lines, and displays the first 100 characters.
    /// </summary>
    /// <param name="filePath">The full path to the .txt file.</param>
    protected override void ParseContent(string filePath)
    {
        Console.WriteLine(" -> Opening plain text stream...");

        string fullText = File.ReadAllText(filePath);
        int lineCount = fullText.Split('\n').Length;

        Console.WriteLine($" -> Successfully parsed {lineCount} lines of text.");

        string displayContent = fullText.Length > 100
            ? fullText.Substring(0, 100) + "..."
            : fullText;

        Console.WriteLine("\n--- First 100 Characters ---");
        Console.WriteLine(displayContent);
        Console.WriteLine("----------------------------\n");
    }
}