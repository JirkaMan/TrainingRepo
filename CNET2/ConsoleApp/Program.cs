
using Model;
using Data;
using System.Linq;

Console.WriteLine("Hello, World!");


int[] numbers = { 11, 2, 13, 44, -5, 6, 127, -99, 0, 256 };



//  Where - FILTRACE
//var result = numbers.Where(n => n > 0 && n <= 99);

//var result2 = numbers.Where(n => n > 0)
//                     .Where(n => n <= 99);

//  ORDERBY
//var result = numbers.OrderBy(n => n);

//  AGREGACE - Min, Max, Sum,Avg, Count
//var number1 = numbers.Max();
//var number2 = numbers.Min();
//var number3 = numbers.Average();
//var number4 = numbers.Sum();


// TAKE SKIP
//var result = numbers.TakeWhile(n => n > 1000);

//  SELECT + transformace
var result = numbers.Select(n => Math.Abs(n));


Console.WriteLine(string.Join(", ", result));





//foreach (var item in result)
//{
//    Console.WriteLine(item);
//}

















//FreqWords();

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

