
using Model;
using Data;
using System.Linq;

Console.WriteLine("Start konzolovky!");

//  Nově lze místo datového typu DateTime používat TimeOnly nebo DateOnly 
DateOnly dateNow = DateOnly.FromDateTime(DateTime.Now);

var dataSet = Data.Serialization.LoadFromXML(@"d:\Data\SkoleniICTpro\PersonDataset\dataset.xml");

Console.WriteLine(dataSet.Count);

LINQCviceniPokrocile(dataSet);

Console.ReadLine();

//TestInterface();

//LINQcviceni();

//  Spustení frekvencní analyzy slov v textovych souborech
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

static void TestInterface()
{
    Client client1 = new Client() { Name = "Petr" };
    Client client2 = new Client() { Name = "Alena" };
    VIPClient client3 = new VIPClient() { Name = "Monika", Status = "GOLD" };

    List<IGreetable> clients = new List<IGreetable> { client1, client2, client3 };

    foreach (IGreetable client in clients)
    {
        GreetClient(client);
    }
}

/// Test pro Interface
static void GreetClient(IGreetable client)
{
    Console.WriteLine(client.SayHello());
}

static void LINQCviceniPokrocile(List<Person> dataSet)
{
    //  kolik lidí má smlouvu
    var result = dataSet.Where(p => p.Contracts.Any()).Count();
    Console.WriteLine(result);

    //  Kolik lidi bydli v Brně
    var result2 = dataSet.Where(p => p.HomeAddress.City.ToLower().Contains("brno")).Count();

    // Vypsat lidi z Brna
    var result3 = dataSet.Where(p => p.HomeAddress.City.ToLower() == "brno").ToList();

    foreach (var person in result3)
    {
        Console.WriteLine(person);
    }

    //  nejstarsi a nejmladsi klient a vypsat jmeno a vek
    var result4 = dataSet.OrderBy(p => p.DateOfBirth);
    var youngest = result4.Last();
    var oldest = result4.First();

    Console.WriteLine($"Nejmladší: {youngest} ({youngest.Age()})");
    Console.WriteLine($"Nejstarší: {oldest} ({oldest.Age()})");


    //  Použití anonymního typu
    var result5 = dataSet.Select(p => new { p.FullName, p.DateOfBirth });

    // Alternativa s přiřazením metody do vlastnostim anonymního typu
    var result6 = dataSet.Select(p => new { p.FullName, Age = p.Age() });

    //  Použití s TUPLE
    var result7 = dataSet.Select(p => (Name: p.FullName, Age: p.Age()));

    foreach (var item in result5)
    {
        Console.WriteLine(item.FullName + " - Datum narození: " + item.DateOfBirth.ToString("dd.MM.yyyy"));
    }

    //  GROUP BY
    //  GROUPBY není key jako kolekce klíčů ale je to vlastnost kolekce! Tím pádem vznikne kolekce kolekcí kdy každá má klíč jako vlastnost
    var result8 = dataSet.GroupBy(p => p.HomeAddress.City);

    foreach (var item in result8)
    {
        Console.WriteLine($"Město: {item.Key}, počet lidí: {item.Count()}");
    }

    foreach (var item in result8)
    {
        Console.WriteLine($"Město: {item.Key}, počet lidí: {item.Count()} :");
        foreach (var item2 in item)
        {
            Console.WriteLine(item2.FullName);
        }
    }

    //  SELECT MANY
    //  Vybere a sjednotí kolekce na kolekci do jedné, viz smlouvy do jednorozměrné kolekce
    var result9 = dataSet.SelectMany(p => p.Contracts);
    Console.WriteLine($"Počet smluv celkem: {result9.Count()}");

    //  Zjistit kdo naposledy uzavřel smlouvu
    //  musim ziskat osobu
    var withContract = dataSet.Where(p => p.Contracts.Any());
    var result11 = withContract.OrderByDescending(p => p.Contracts.OrderByDescending(c => c.Signed).First().Signed).First();

    Console.WriteLine($"{Environment.NewLine}Poslední smlouvu podepsal: {result11.FullName}");
}