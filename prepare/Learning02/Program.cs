using System;

class Program
{
    static void Main(string[] args)
    {
        Job job1 = new(); //new Job(); // either works
        job1._jobTitle = "Janitor";
        job1._company = "BYU-Idaho";
        job1._startYear = 2021;
        job1._endYear = 2023;

        job1.Display();
    }
}