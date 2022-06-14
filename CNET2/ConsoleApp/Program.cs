
using Model;
using Data;
using System.Linq;

Console.WriteLine("Hello, World!");



//  Metoda spustí cvičení s LINQ
LINQcviceni();

//  Spustení frekvencní analyzy slov v textovych souborech
FreqWords();

static void FreqWords()
{
    string dirBooks = @"d:\Data\SkoleniICTpro\Books";

    var files = Directory.EnumerateFiles(dirBooks, "*.txt");

    foreach (var file in files)
    {
        var result = FreqAnalysis.FreqAnalysisFromFile(file);

        FileInfo fileInfo = new FileInfo(file); ;
        Console.WriteLine(fileInfo.Name);

        var orderedTopTen = result.Words.OrderByDescending(w => w.Value).Take(10);

        foreach (var item in orderedTopTen)
        {

            Console.WriteLine(item.Key + " : " + item.Value);

        }

        Console.WriteLine();
    }
}


static void LINQcviceni()
{
    int[] numbers = { 11, 2, 13, 44, -5, 6, 127, -99, 0, 256 };

    //Where - FILTRACE
    var result = numbers.Where(n => n > 0 && n <= 99);

    var result2 = numbers.Where(n => n > 0)
                         .Where(n => n <= 99);

    //ORDERBY
    var result3 = numbers.OrderBy(n => n);

    //AGREGACE - Min, Max, Sum,Avg, Count
    var number1 = numbers.Max();
    var number2 = numbers.Min();
    var number3 = numbers.Average();
    var number4 = numbers.Sum();


    //TAKE SKIP
    var result4 = numbers.TakeWhile(n => n > 1000);

    //SELECT + transformace
    var result5 = numbers.Select(n => Math.Abs(n));



    //*********************************************************************************
    //CVICENI
    //* ********************************************************************************

    //1.Zjistit pocet kladnych hodnot v numbers
    var result6 = numbers.Where(x => x > 0).Count();




    //2.Ignorujte nejvetsi a nejmensi cislo a spocitejte prumer
    var result7 = numbers.OrderBy(n => n).Skip(1).SkipLast(1).Average();




    //3.Spocitat pocet lichych a sudych

    var countEven = numbers.Where(c => c % 2 != 0).Count();
    var countOdd = numbers.Where(c => c % 2 == 0).Count();

    Console.WriteLine($"Suda: {countEven} , Licha: {countOdd}");


    //  4.  vypište čísla v poli numbers jako slova
    var numbers2 = new[] { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
    var strings = new[] { "zero", "one", "two", "three", "four",
    "five", "six", "seven", "eight", "nine" };

    var result8 = numbers2.Select(n => strings[n]);


    Console.WriteLine(string.Join(", ", result));

    //foreach (var item in result)
    //{
    //    Console.WriteLine(item);
    //}
}