namespace ConsoleApp1;

/// <summary>
/// Defines the contract for all file readers.
/// Every reader must specify its supported format, validate files, and process content.
/// </summary>
public interface IFileReader
{
    /// <summary>
    /// Gets the file format this reader supports (e.g., "TXT", "CSV", "JSON", "XML").
    /// </summary>
    string SupportedFormat { get; }

    /// <summary>
    /// Determines whether the specified file can be read by this reader.
    /// </summary>
    /// <param name="filePath">The full path to the file.</param>
    /// <returns><c>true</c> if the file extension matches the supported format; otherwise, <c>false</c>.</returns>
    bool CanRead(string filePath);

    /// <summary>
    /// Processes the specified file by reading and displaying its contents.
    /// </summary>
    /// <param name="filePath">The full path to the file.</param>
    void ProcessFile(string filePath);
}

/// <summary>
/// Provides shared logic for all file readers.
/// Implements <see cref="IFileReader"/> and delegates format-specific parsing to derived classes.
/// </summary>
public abstract class BaseFileReader : IFileReader
{
    /// <summary>
    /// Gets the file format this reader supports. Must be overridden by derived classes.
    /// </summary>
    public abstract string SupportedFormat { get; }

    /// <summary>
    /// Checks whether the file extension matches <see cref="SupportedFormat"/>.
    /// </summary>
    /// <param name="filePath">The full path to the file.</param>
    /// <returns><c>true</c> if the extension matches (case-insensitive); otherwise, <c>false</c>.</returns>
    public bool CanRead(string filePath)
    {
        string extension = Path.GetExtension(filePath).TrimStart('.');
        return extension.Equals(SupportedFormat, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Processes the file by logging the start, calling <see cref="ParseContent"/>,
    /// and catching any exceptions that occur.
    /// </summary>
    /// <param name="filePath">The full path to the file.</param>
    public void ProcessFile(string filePath)
    {
        Console.WriteLine($"[{SupportedFormat} Engine] Processing {Path.GetFileName(filePath)}...");

        try
        {
            ParseContent(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] Failed to read file: {ex.Message}");
        }
    }

    /// <summary>
    /// When overridden in a derived class, reads and parses the file contents,
    /// then displays the result to the console.
    /// </summary>
    /// <param name="filePath">The full path to the file.</param>
    protected abstract void ParseContent(string filePath);
}