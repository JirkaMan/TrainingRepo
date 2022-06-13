
using Model;
using Data;
using System.Linq;


string dirBooks= @"d:\Data\SkoleniICTpro\Books";

var files = Directory.EnumerateFiles(dirBooks, "*.txt");

foreach (var file in files)
{
    var result = FreqAnalysis.FreqAnalysisFromFile(file);

    FileInfo fileInfo = new FileInfo(file); ;
    Console.WriteLine(fileInfo.Name);

    var orderedTopTen = result.OrderByDescending(kv => kv.Value).Take(10);

    foreach (var item in orderedTopTen)
    {

        Console.WriteLine(item.Key + " : "+ item.Value);

    }

    Console.WriteLine();
}

