using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

Console.WriteLine("Enter your name:");
string? name = Console.ReadLine();

if (string.IsNullOrEmpty(name))
{
    Console.WriteLine("Name cannot be empty. Please enter a valid name.");
    return;
}

string birthdate = "";
DateTime parsedBirthdate = DateTime.MinValue;
bool validDate = false;

while (!validDate)
{
    Console.WriteLine("Enter your birthdate (MM/dd/yy):");
    birthdate = Console.ReadLine() ?? "";

    if (string.IsNullOrEmpty(birthdate))
    {
        Console.WriteLine("Birthdate cannot be empty. Please enter a valid date.");
        continue;
    }

   
    string birthdatePattern = @"^(0[1-9]|1[0-2])/(0[1-9]|[12]\d|3[01])/(\d{2})$";

  
    if (!Regex.IsMatch(birthdate, birthdatePattern))
    {
        Console.WriteLine("Incorrect format. Please enter your birthdate in MM/dd/yy format.");
    }
    else
    {
        
        if (DateTime.TryParseExact(birthdate, "MM/dd/yy", null, DateTimeStyles.None, out parsedBirthdate))
        {
            validDate = true;
        }
        else
        {
            Console.WriteLine("Invalid date. Please ensure the date exists in the format MM/dd/yy.");
        }
    }
}

int age = CalculateAge(parsedBirthdate);
Console.WriteLine($"Hello, {name}. You are {age} years old.");

string filePath = "user_info.txt";
File.WriteAllText(filePath, $"Name: {name}\nBirthdate: {parsedBirthdate.ToString("MM/dd/yy")}\nAge: {age}");
Console.WriteLine($"User information saved to '{filePath}'.");

string fileContent = File.ReadAllText(filePath);
Console.WriteLine("\nContent of the file:");
Console.WriteLine(fileContent);

Console.WriteLine("\nEnter a directory path to list all files:");
string? directoryPath = Console.ReadLine();

if (string.IsNullOrEmpty(directoryPath) || !Directory.Exists(directoryPath))
{
    Console.WriteLine("The specified directory does not exist or is invalid.");
}
else
{
    Console.WriteLine($"\nFiles in directory '{directoryPath}':");
    string[] files = Directory.GetFiles(directoryPath);
    foreach (string file in files)
    {
        Console.WriteLine(Path.GetFileName(file));
    }
}

Console.WriteLine("\nEnter a string to format to title case:");
string? inputString = Console.ReadLine();

if (!string.IsNullOrEmpty(inputString))
{
    string titleCaseString = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(inputString.ToLower());
    Console.WriteLine($"Title Case: {titleCaseString}");
}
else
{
    Console.WriteLine("Input string cannot be empty.");
}

Console.WriteLine("\nTriggering garbage collection explicitly...");
GC.Collect();
GC.WaitForPendingFinalizers();
Console.WriteLine("Garbage collection completed.");

Console.WriteLine("Application finished. Press any key to exit.");
Console.ReadKey();

int CalculateAge(DateTime birthdate)
{
    DateTime today = DateTime.Today;
    int age = today.Year - birthdate.Year;

   
    if (birthdate.Date > today.AddYears(-age)) age--;

    return age;
}


