using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Please enter the radius: ");
        string rtext = Console.ReadLine();
        double r = double.Parse(rtext);

        // Compute the area of the circle
        double area = Math.PI * r*r;

        Console.WriteLine($"Area of the circle: {area}");
        // fun practice unrelated
        // int x = 3;
        // string s = "Goodbye";
        // float f = 3.14F;// int64, 3.14 is a double F 
        // double d = 5.21;
        // long n = 2555555555555555555;

        // Console.WriteLine($"Hello {x} Sandbox World! {f} {n}");
    }
}