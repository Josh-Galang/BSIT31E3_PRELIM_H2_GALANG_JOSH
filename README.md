# IT-ELECTIVE 2 — Prelim Assignment 2: File Ingestion Engine

## Overview

The File Ingestion Engine reads files in different formats (TXT, CSV, JSON, XML) using a **Strategy + Factory** pattern. Each file format has its own reader class, all sharing a common interface (`IFileReader`) and base class (`BaseFileReader`). A `FileReaderResolver` (factory) returns the correct reader for a chosen format.

Currently only `TextFileReader` is implemented. You will add three more readers: **XmlFileReader**, **JsonFileReader**, and **CsvFileReader**.

---

## ⚠️ Important Rules — Read First

| Rule | Reason |
|---|---|
| **DO NOT modify** `Program.cs` | It already handles all four formats (A=TXT, B=CSV, C=JSON, D=XML). No changes needed. |
| **DO NOT modify** `IFileReader.cs` | The interface and base class are already complete. They define the contract all readers must follow. |
| **You must CREATE** three new `.cs` files | `XmlFileReader.cs`, `JsonFileReader.cs`, and `CsvFileReader.cs` — one class per file. |
| **You must UPDATE** `FileReaderResolver.cs` | Register your new readers in the `_availableReaders` list so the program can find them. |
| **You must write `ParseContent()` yourself** | The logic for reading and displaying each file format is yours to implement. Follow the pattern in `TextFileReader.cs`. |

### What you are allowed to change:
- ✔ Create **new** class files (`XmlFileReader.cs`, `JsonFileReader.cs`, `CsvFileReader.cs`)
- ✔ Edit `FileReaderResolver.cs` to register your new readers

### What you must NOT change:
- ✘ `Program.cs` — leave it as-is
- ✘ `IFileReader.cs` — leave it as-is
- ✘ `TextFileReader.cs` — leave it as-is (use it as a reference)

---

## Understanding the Code Flow (For Beginners)

This is how the program works from start to finish:

```
User enters file path + format letter
              │
              ▼
Program.cs asks FileReaderResolver:
  "Give me a reader for format 'CSV'"
              │
              ▼
FileReaderResolver looks through its list
of registered readers, finds one whose
SupportedFormat matches "CSV"
              │
              ▼
Program.cs calls CanRead() to verify the
file extension matches the reader
              │
              ▼
Program.cs calls ProcessFile() on the reader
              │
              ▼
The reader's ProcessFile() (inherited from
BaseFileReader) calls ParseContent(), which
YOU write — this is where you read and
display the file contents
```

---

## Class Diagram

```
┌─────────────────────────────────────────────────────────────┐
│                     <<interface>>                           │
│                     IFileReader                             │
├─────────────────────────────────────────────────────────────┤
│  + SupportedFormat  (string)                                │
│  + CanRead(filePath)  (bool)                               │
│  + ProcessFile(filePath)                                    │
└────────────────────────────────┬────────────────────────────┘
                                 │  implements
                    ┌────────────▼────────────────────────────┐
                    │         <<abstract>>                    │
                    │        BaseFileReader                   │
                    ├─────────────────────────────────────────┤
                    │  + CanRead(filePath)  (bool)            │
                    │  + ProcessFile(filePath)                │
                    │  # ParseContent(filePath)*  (abstract)  │
                    └─────┬──────────┬──────────┬─────────────┘
                          │          │          │
                       extends    extends    extends
                    ┌───▼────┐ ┌───▼────┐ ┌───▼────┐
                    │ Text   │ │ Xml    │ │ Json   │ │ Csv   │
                    │ File   │ │ File   │ │ File   │ │ File  │
                    │ Reader │ │ Reader │ │ Reader │ │ Reader│
                    ├────────┤ ├────────┤ ├────────┤ ├───────┤
                    │Fmt:TXT │ │Fmt:XML │ │Fmt:JSON│ │Fmt:CSV│
                    │ +Parse │ │ +Parse │ │ +Parse │ │ +Parse│
                    │Content │ │Content │ │Content │ │Content│
                    └────────┘ └────────┘ └────────┘ └───────┘

                      ┌─────────────────────────────┐
                      │     FileReaderResolver      │
                      ├─────────────────────────────┤
                      │ - _availableReaders         │
                      │   : List<IFileReader>       │
                      │ + GetReaderForFormat(format) │
                      │   : IFileReader             │
                      └──────────┬──────────────────┘
                                 │  instantiates &
                                 │  returns by format
                      ┌──────────▼──────────┐
                      │  TextFileReader     │
                      │  XmlFileReader      │
                      │  JsonFileReader     │
                      │  CsvFileReader      │
                      └─────────────────────┘
```

**Relationships explained:**

| Arrow | Meaning |
|---|---|
| `IFileReader` ──▶ `BaseFileReader` | `BaseFileReader` **implements** the `IFileReader` interface |
| `BaseFileReader` ──▶ `TextFileReader` / `XmlFileReader` / `JsonFileReader` / `CsvFileReader` | Each concrete reader **extends** the abstract `BaseFileReader` |
| `FileReaderResolver` ──▶ `IFileReader` (via list) | The resolver **depends on** the `IFileReader` interface (polymorphism) |
| `FileReaderResolver` ──▶ concrete readers | The resolver's constructor **instantiates** each concrete reader and stores them as `IFileReader` |

---

## Step-by-Step Instructions

Follow these steps **in order**:

### Step 1 — Study the existing `TextFileReader.cs`

Open `TextFileReader.cs`. This is your template. Every reader class follows this exact structure:

```
class SomeReader : BaseFileReader
├── override string SupportedFormat → returns the format string
└── override void ParseContent(filePath)
    ├── read the file
    ├── parse the content
    └── display the results
```

### Step 2 — Create three new class files

In the same folder as `TextFileReader.cs`, create:

| File | Class Name | Format |
|---|---|---|
| `XmlFileReader.cs` | `XmlFileReader` | XML |
| `JsonFileReader.cs` | `JsonFileReader` | JSON |
| `CsvFileReader.cs` | `CsvFileReader` | CSV |

### Step 3 — Implement each class

Each class must follow the same blueprint. Below are the specifications. **You must write the code yourself** — use the hints and `TextFileReader.cs` as a guide.

## Class Specifications

---

### XmlFileReader (`XmlFileReader.cs`)

- **`SupportedFormat`** → `"XML"`
- **`CanRead`** logic is inherited: matches `.xml` extension.
- **`ParseContent`** should:
  - Load the XML file (use `XDocument.Load` or `XmlDocument`).
  - Display the root element name and total descendant node count.
  - Show the first 100 characters of the XML string.

**Key C# tools:** `System.Xml.Linq.XDocument`, `XDocument.Load()`, `XDocument.ToString()`, `Descendants()`.

---

### JsonFileReader (`JsonFileReader.cs`)

- **`SupportedFormat`** → `"JSON"`
- **`CanRead`** logic is inherited: matches `.json` extension.
- **`ParseContent`** should:
  - Read all text from the file.
  - Parse it (use `JsonDocument.Parse` from `System.Text.Json`).
  - Display the total number of root elements or properties.
  - Show the first 100 characters of the raw JSON string.

**Key C# tools:** `System.Text.Json.JsonDocument`, `JsonDocument.Parse()`, `RootElement`, `EnumerateObject()`, `EnumerateArray()`.

---

### CsvFileReader (`CsvFileReader.cs`)

- **`SupportedFormat`** → `"CSV"`
- **`CanRead`** logic is inherited: matches `.csv` extension.
- **`ParseContent`** should:
  - Read all lines from the file.
  - Split each line by comma.
  - Display the number of rows and columns detected.
  - Show the first 100 characters of the raw content.

**Key C# tools:** `File.ReadAllLines()`, `string.Split()`, basic string/array operations.

---

### Step 4 — Update `FileReaderResolver.cs`

Open `FileReaderResolver.cs`. Currently it only registers `TextFileReader`:

```csharp
_availableReaders = new List<IFileReader>
{
    new TextFileReader()
    // TODO: Student must register CsvFileReader, JsonFileReader, etc. here
};
```

**Your task:** Add your new readers to this list, separated by commas:

```csharp
_availableReaders = new List<IFileReader>
{
    new TextFileReader(),
    new XmlFileReader(),
    new JsonFileReader(),
    new CsvFileReader()
};
```

**Line-by-line explanation of `FileReaderResolver.cs`:**

| Code | Purpose |
|---|---|
| `List<IFileReader> _availableReaders` | A list that stores all registered readers. Because it uses the `IFileReader` interface type, it can hold **any** class that implements that interface — including your new ones. |
| Constructor `FileReaderResolver()` | Runs when the resolver is created. It fills the list with one instance of each reader. If a reader is not in this list, the program will never know it exists. |
| `GetReaderForFormat(string format)` | Loops through `_availableReaders` and returns the **first** reader whose `SupportedFormat` matches what the user requested (case-insensitive). Returns `null` if nothing matches. |

---

## How the Program Uses Your Readers

Look at `Program.cs` to understand how your readers get called:

| `Program.cs` Lines | What Happens |
|---|---|
| 14–25 | User picks A, B, C, or D. The `switch` maps it to `"TXT"`, `"CSV"`, `"JSON"`, or `"XML"`. |
| 29 | Creates a `FileReaderResolver` (which holds your registered readers). |
| 30 | Calls `resolver.GetReaderForFormat(targetFormat)` — this is where your reader gets selected. |
| 38 | Calls `activeReader.CanRead(filePath)` — verifies the file extension matches. |
| 46 | Calls `activeReader.ProcessFile(filePath)` — this triggers your `ParseContent()` code. |

**Reminder:** `Program.cs` is **off-limits**. You do not need to touch it. It already knows about all four formats. Your only job is to create the reader classes and register them in the resolver.

---

## Testing Your Implementation

1. Create sample files in the project directory (e.g., `sample.xml`, `sample.json`, `sample.csv`).
2. Build the project:
   ```
   dotnet build
   ```
3. Run the project:
   ```
   dotnet run
   ```
4. Enter the full path to your sample file and select the corresponding format letter.
5. Verify the output displays correctly.

---

## Sample Console Output

Below is what the output should look like for each reader (assuming a valid file is provided):

### TXT (TextFileReader)
```
=== Interactive File Ingestion Engine ===
Enter the full file path to read: C:\sample.txt

Select format: A (.txt) | B (.csv) | C (.json) | D (.xml)
Choice: A

--------------------------------------------------
[TXT Engine] Processing sample.txt...
 -> Opening plain text stream...
 -> Successfully parsed 42 lines of text.

--- First 100 Characters ---
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore...
----------------------------
--------------------------------------------------
```

### CSV (CsvFileReader)
```
=== Interactive File Ingestion Engine ===
Enter the full file path to read: C:\sample.csv

Select format: A (.txt) | B (.csv) | C (.json) | D (.xml)
Choice: B

--------------------------------------------------
[CSV Engine] Processing sample.csv...
 -> Opening CSV stream...
 -> Detected 10 rows and 4 columns.

--- First 100 Characters ---
Name,Age,City,Occupation
John,25,New York,Engineer
Jane,30,Los Angeles,Designer
...
----------------------------
--------------------------------------------------
```

### JSON (JsonFileReader)
```
=== Interactive File Ingestion Engine ===
Enter the full file path to read: C:\sample.json

Select format: A (.txt) | B (.csv) | C (.json) | D (.xml)
Choice: C

--------------------------------------------------
[JSON Engine] Processing sample.json...
 -> Opening JSON stream...
 -> Parsed 3 root properties.

--- First 100 Characters ---
{"name":"John","age":30,"city":"New York","hobbies":["reading","cycling","coding"]}
...
----------------------------
--------------------------------------------------
```

### XML (XmlFileReader)
```
=== Interactive File Ingestion Engine ===
Enter the full file path to read: C:\sample.xml

Select format: A (.txt) | B (.csv) | C (.json) | D (.xml)
Choice: D

--------------------------------------------------
[XML Engine] Processing sample.xml...
 -> Opening XML stream...
 -> Root element: <catalog> with 15 descendant nodes.

--- First 100 Characters ---
<?xml version="1.0" encoding="UTF-8"?>
<catalog>
  <book id="1">
    <title>Introduction to
...
----------------------------
--------------------------------------------------
```

---

## Final Checklist

### Files you need to CREATE (3 new files):

| File | Class | Format |
|---|---|---|
| `XmlFileReader.cs` | `XmlFileReader : BaseFileReader` | `.xml` — XML |
| `JsonFileReader.cs` | `JsonFileReader : BaseFileReader` | `.json` — JSON |
| `CsvFileReader.cs` | `CsvFileReader : BaseFileReader` | `.csv` — CSV |

### File you need to UPDATE (1 file):

| File | What to do |
|---|---|
| `FileReaderResolver.cs` | Add `new XmlFileReader(), new JsonFileReader(), new CsvFileReader()` to the `_availableReaders` list |

### Files you must NOT change:

| File | Reason |
|---|---|
| `Program.cs` | Already handles all formats — leave it alone |
| `IFileReader.cs` | Interface and base class are complete — leave them alone |
| `TextFileReader.cs` | Existing implementation — use as reference only |
