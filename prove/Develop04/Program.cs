using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Cryptography.X509Certificates;

using System.IO;

using JournalProject;
using System.Runtime.InteropServices;
class Program
{
    static void Main(string[] args)
    {
        // Create instance of journal class
        Journal journal = new Journal();
        // Call DisplayOptions() function
        while (true)
        {
          journal = DisplayOptions(journal);  
        }
        
    }
    static Journal DisplayOptions(Journal journal)
    {
        bool keepgoing = true;
        while (keepgoing)
        {
            List<string> Options = new List<string>(){
                "Write Entry",
                "Display Journal",
                "Save Journal to txt file",
                "Populate Journal from txt file",
                "Quit"
            };
            Console.WriteLine();
            foreach (string option in Options)
            {
                Console.WriteLine($"{Options.IndexOf(option) + 1}) {option}");
                // Console.WriteLine();
            }
            Console.WriteLine("Please select an option: ");
            string Response = Console.ReadLine();
            journal = GetResponse(Response, journal);
        }
        return journal;
                
    }

    static Journal GetResponse(string Response, Journal journal)
    {
        int number;
        if (int.TryParse(Response, out number) && number >= 1 && number <= 5)
        {
            switch (number)
            {
                case 1:
                    WriteEntry(journal);
                    break;
                case 2:
                    DisplayJournal(journal);
                    break;
                case 3:
                    SaveJournalToTxtFile(journal);
                    break;
                case 4:
                    journal = PopulateJournalFromFile();
                    break;
                case 5:
                    Environment.Exit(0); // clean exit
                    break;
            }
        }
        else
        {
            Console.WriteLine($"'{Response}' is not a valid option.");
        }

        return journal;
    }


    static void WriteEntry(Journal journal){
        // Save datetime as variable datetime
        DateTime now = DateTime.Now;
        // Call function to generate prompt
        string RandoPrompto = GeneratePrompt(journal);
        // Display prompt
        Console.WriteLine($"{RandoPrompto}");
        // ask user for response, save that to variable named text
        string text = Console.ReadLine();

        AddEntryToJournal(now,RandoPrompto,text,journal);
    }
    static void AddEntryToJournal(DateTime dateTime, string Prompt, string text, Journal journal){
        // Create instance of Entry and populate with values
        Entry entry = new Entry();
        entry.DT = dateTime;
        entry.Prompt = Prompt; // Question: FOR SOME REASON it kept doing the same prompt over and over.
        entry.Response = text;
        // Add Entry instance to Journal instance
        journal.Entries.Add(entry);
        // Display message saying Entry was added to Journal
        Console.WriteLine();
        // Call function to display options again.
        return;
    }
    static string GeneratePrompt(Journal journal){
        List<string> prompts = new List<string> {
            "On a scale of 1(bad) to 5(good) how was your day?",
            "What are three things you are grateful for?",
            "Did you discover any new hobbies, super powers, good habbits etc. today?",
            "Was someone else really nice to you today? Were you nice to someone else today?",
            "Did you have an adventure today? If so tell me about it.",
            "Freedom prompt! Just write about the first thing that comes to mind.",
            "What is the funniest thing that happened today?",
            "What are you most proud of today?",
            "Did you pray today? Who did you pray for?",
            "What was a small or big win you had today?",
            "What are you most excited about right now?",
            "Who is your best freind(s) right now?"
        };
        Random Rando = new Random();

        int RandoNumbo = Rando.Next(0, prompts.Count);
        // Console.WriteLine($"RandoNumbo = {RandoNumbo}, list length: {prompts.Count}");

        string RandoPrompto = prompts[RandoNumbo];

        return RandoPrompto;
    }

    static void DisplayJournal(Journal journal){
        // might need journal as parameter, but maybe not
        // Loop through journal
        foreach(Entry entry in journal.Entries){
            // convert journal entry as string if needed (use function for this)
            // Console.Write the stringified journal entry
            Console.WriteLine("__________________________________________________");
            Console.WriteLine();
            Console.WriteLine($"{entry.DT}");
            Console.WriteLine($"{entry.Prompt}");
            Console.WriteLine($"{entry.Response}");
            Console.WriteLine();
        }
        // Call function to display options again
        return;
    }

    static void SaveJournalToTxtFile(Journal journal){
        // might need journal as parameter, but maybe not
        // Ask user to name the file to store the journal
        Console.WriteLine("What filename do you want to save your journal to?");
        string FileName = Console.ReadLine();
        List<string> FileNames = new List<string>();
        // convert journal to string if need be use seperate function for this if needed
        // Check if the file name already exists
        // if it exists inform user it already exists and ask if they want to pick new name or not
        if (File.Exists(FileName)){
            Console.WriteLine($"{FileName} already exists:");
            Console.WriteLine($"1) overwrite");
            Console.WriteLine($"2) pick a different filename");
            string filepick = Console.ReadLine();
            if (filepick != "1"){
                SaveJournalToTxtFile(journal);
            }
            // if they choose keep name then rewrite file with that name with new journal info
            if (filepick == "1"){
                FileNames[FileNames.IndexOf(FileName)] = FileName;
            }
        }
        else{
            FileNames.Add(FileName);
        }
        // if they choose different name or it does not exist then create new filename and populate it with jounral information
        using (StreamWriter outputFile = new StreamWriter(FileName)){
            foreach (Entry j in journal.Entries){
                outputFile.WriteLine("_____________________________________________________");
                outputFile.WriteLine($"{j.DT}");
                outputFile.WriteLine($"{j.Prompt}");
                outputFile.WriteLine($"{j.Response}");
            }
        }
        // message user saying journal was saved to file ___.txt ask if they want to display that file or go to options
        Console.WriteLine($"The Journal was added to File: {FileName}");
        // if display then call DisplayJournal function
        // if not then go to options
        return;
    }

    static Journal PopulateJournalFromFile(){
        // ask user for file name
        Console.WriteLine("What file would you like to populate the journal with?");
        Console.WriteLine();
        Console.WriteLine("This is so you can display or add to the journal information etc.");
        string FileName = Console.ReadLine();
        // inside loop:
        if (!File.Exists(FileName)){
            // check if filename exists - if needed update a list with each filename in existance
            // if does not exist prompt again
            // if does exist save journal instance as info from that file.
            return new Journal();
        }
        Journal journal = new Journal();
        string[] rows = System.IO.File.ReadAllLines(FileName);
        int entryline = 0;
        string prompt = "";
        string text = "";
        DateTime datetime = DateTime.Now;
        foreach (string row in rows){
            // Console.WriteLine($"row: {row}, entryline: {entryline}, rows.Count: {rows.Count()}");
            if (string.IsNullOrWhiteSpace(row) || row.StartsWith("_"))
            {
                continue;
            }
            Console.WriteLine();
            Console.WriteLine(row);
            if (entryline == 0){
                datetime = DateTime.Parse(row);
            }
            else if (entryline == 1){
                prompt = row;
            }
            else if (entryline == 2){
                text = row;
                AddEntryToJournal(datetime, prompt, text, journal);// This is a problem
                entryline = -1;
            }
            entryline++;
        }
        // message user that Journal now contains info from filename ___.txt
        Console.WriteLine($"txt file {FileName} added.");
        // call display options
        return journal;
    }
}