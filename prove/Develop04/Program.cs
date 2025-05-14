using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Cryptography.X509Certificates;

using System.IO;

using JournalProject;
class Program
{
    static void Main(string[] args)
    {
        // Create instance of journal class
        Journal journal = new Journal();
        // Call DisplayOptions() function
        DisplayOptions(journal);

    }
    static void DisplayOptions(Journal journal)
        {
            Console.WriteLine();
            Console.WriteLine("1) Write Entry");
            Console.WriteLine("2) Display Journal");
            Console.WriteLine("3) Save Journal to txt file");
            Console.WriteLine("4) Populate Journal from txt file");
            Console.WriteLine("5) Quit");
            Console.WriteLine();
            Console.WriteLine("Please select an option: ");
            string Response = Console.ReadLine();
            GetResponse(Response, journal);
            
        }

        static void GetResponse(string Response, Journal journal){
            // Check if Response is a valid answer
            int number;
            if (int.TryParse(Response, out number) && number >= 1 && number <= 5){
                // Once valid, call function associated with response
                if (number == 1){
                    WriteEntry(journal);
                }
                if (number == 2){
                    DisplayJournal(journal);
                }
                if (number == 3){
                    SaveJournalToTxtFile(journal);
                }
                if (number == 4){
                    PopulateJournalFromFil(journal);
                }
                if (number == 5){
                    return;// might not actually work, not sure though.
                }
            }
            // If not, then call DisplayOptions again, the cycle will continue until true.
            else{
                Console.WriteLine($"Unfortunately {number} was not a valid response. Please try again.");
                Console.WriteLine();
                DisplayOptions(journal);
            }
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

            // Create instance of Entry and populate with values
            Entry entry = new Entry();
            entry.DT = now;
            entry.Prompt = RandoPrompto; // Question: FOR SOME REASON it kept doing the same prompt over and over.
            entry.Response = text;
            // Add Entry instance to Journal instance
            journal.Entries.Add(entry);
            // Display message saying Entry was added to Journal
            Console.WriteLine();
            // Call function to display options again.
            DisplayOptions(journal);
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

            int RandoNumbo = Rando.Next(0, prompts.Count-1);
            Console.WriteLine($"RandoNumbo = {RandoNumbo}, list length: {prompts.Count}");

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
            DisplayOptions(journal); // For some reason it did not run again after displaying
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
            if (FileNames.Contains(FileName)){
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
                    outputFile.WriteLine($"{j.Prompt}");
                }
            }
            // message user saying journal was saved to file ___.txt ask if they want to display that file or go to options
            Console.WriteLine($"The Journal was added to File: {FileName}");
            // if display then call DisplayJournal function
            // if not then go to options
            DisplayOptions(journal);
        }

        static void PopulateJournalFromFil(Journal journal){
            // ask user for file name
            Console.WriteLine("What file would you like to populate the journal with?)");
            Console.WriteLine("This is so you can display or add to the journal information etc.");
            string FileName = Console.ReadLine();
            // inside loop:
                // check if filename exists - if needed update a list with each filename in existance
                // if does not exist prompt again
            // if does exist save journal instance as info from that file.
            string[] rows = System.IO.File.ReadAllLines(FileName);
            foreach (string row in rows){
                Console.WriteLine($"{row}");
            }
            // message user that Journal now contains info from filename ___.txt
            Console.WriteLine($"txt file {FileName} added.");
            // call display options
            DisplayOptions(journal);
        }
}