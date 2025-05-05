using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Develop02 World!");
        int number = 10;
        List<int> numbers = new();
        int min = 1000000000;
        int max = 0;
        double sum = 0;
        double avg;
        string allinputs = "Your List:";

        while (number >= 1){
            Console.WriteLine();
            Console.Write("Please enter an integer (quit by typing 0): ");
            string userinput = Console.ReadLine();

            number = int.Parse(userinput);

            if (number != 0){
                allinputs = allinputs + ", " + userinput;
                numbers.Add(number);
                Console.WriteLine($"added: {number}");
            }

            if (number > max){
                max = number;
                Console.WriteLine($"New max number! {number}");
            }
            if (number != 0 && number < min){
                min = number;
                Console.WriteLine($"New min number! {number}");
            }

            sum = sum + number;
        }
        avg = Math.Round((sum / numbers.Count),2);
        Console.WriteLine();
        Console.WriteLine($"{allinputs}");
        Console.WriteLine($"Your avg provided number is: {avg}");
        Console.WriteLine($"Your min: {min}, Your max: {max}");

    }
}