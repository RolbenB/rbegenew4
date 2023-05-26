using System;
using System.Collections.Generic;
using System.IO;

class Entry
{
    public string Prompt { get; set; } // Represents the prompt/question for the entry
    public string Response { get; set; } //Represents the user's response to the prompt
    public string Date { get; set; } // Represents the date of the entry

}

class Journal
{
    public List<Entry> Entries { get; private set; }// List to store journal entries

    public Journal()
    {
        Entries = new List<Entry>(); // Initializes the list of entries in the constructor
    }

    public void AddEntry(Entry entry)
    {
        Entries.Add(entry); // Adds a new entry to the list
    }

    public void DisplayEntries()
    {
        foreach (var entry in Entries)
        {
            Console.WriteLine($"Date: {entry.Date}"); // Displays the date of the entry
            Console.WriteLine($"Prompt: {entry.Prompt}"); // Displays the prompt/question
            Console.WriteLine($"Response: {entry.Response}\n"); // Displays the user's response
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var entry in Entries)
            {
                writer.WriteLine(entry.Date); // Write the date to the file
                writer.WriteLine(entry.Prompt); // Writes the prompt to the file
                writer.WriteLine(entry.Response); // Writes the response to the file
                writer.WriteLine(); // here we Write an empty line to separate entries
            }
        }

        Console.WriteLine("Journal saved successfully.");
    }

    public void LoadFromFile(string filename)
    {
        Entries.Clear(); // Clears the existing entries before loading from the file

        using (StreamReader reader = new StreamReader(filename))
        {
            while (!reader.EndOfStream)
            {
                string date = reader.ReadLine(); // Reads the date from the file
                string prompt = reader.ReadLine(); // Reads the prompt from the file
                string response = reader.ReadLine(); // Reads the response from the file


                reader.ReadLine(); // Skip the empty line

                Entries.Add(new Entry
                {
                    Date = date,
                    Prompt = prompt,
                    Response = response
                });
            }
        }

        Console.WriteLine("Journal loaded successfully.");
    }
}

class Program
{
    static void Main()
    {
        Journal journal = new Journal(); // Creates a new instance of the Journal class

        string[] prompts = {
            "What is the most interesting game played in your life?",
            "What is the name of your best friend?",
            "How did you see the blessing of the Lord in your life ?",
            "Explain a short story of your life?",
            "What country would you like to visit next year?"
        };

        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("Journal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");
            Console.Write("Please, choose a number from 1-5: ");


            string choice = Console.ReadLine(); // Reads the user's choice
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    Entry newEntry = new Entry(); // We Create a new instance of the Entry class
                    newEntry.Prompt = prompts[journal.Entries.Count % prompts.Length]; // Selects a prompt based on the number of entries
                    Console.WriteLine($"Prompt: {newEntry.Prompt}");
                    Console.Write("Response: ");
                    newEntry.Response = Console.ReadLine();// Reads the user's response
                    journal.AddEntry(newEntry);// Adds the new entry to the journal
                    Console.WriteLine("Entry added successfully.\n");
                    break;
                case "2":
                    if (journal.Entries.Count > 0)
                        journal.DisplayEntries();// Displays the journal entries
                    else
                        Console.WriteLine("No entries found.\n");
                    break;
                case "3":
                    Console.Write("Enter filename to save the journal: ");
                    string saveFilename = Console.ReadLine();// Reads the filename from the user
                    journal.SaveToFile(saveFilename); // Saves the journal to a file name "Journal.txt"
                    Console.WriteLine();
                    break;
                case "4":
                    Console.Write("Enter filename to load the journal: ");
                    string loadFilename = Console.ReadLine(); // Reads the filename from the user
                    journal.LoadFromFile(loadFilename);// Loads the journal from a file
                    Console.WriteLine();
                    break;
                case "5":
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.\n");// Exits the loop and ends the program
                    Console.WriteLine("Invalid number, please choose a number from 1-5.\n");
                    break;
            }
        }

        Console.WriteLine("Thank you, have a good day, good bye!");
    }
}