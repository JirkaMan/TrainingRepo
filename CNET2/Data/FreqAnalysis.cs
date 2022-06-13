namespace Data
{
    public static class FreqAnalysis
    {
        public static Dictionary<string,int> FreqAnalysisFromString(string input)
        {
            Dictionary<string,int> result = new Dictionary<string,int>();

            var parts = input.Replace("."," ")
                             .Replace(","," ")
                             .Replace(":"," ")
                             .Replace("("," ")
                             .Replace(")"," ")
                             .Replace(Environment.NewLine," ")
                             .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);


            foreach (string part in parts)
            {
                if (result.ContainsKey(part))
                {
                    result[part] += 1;
                }
                else
                {
                    result.Add(part, 1);
                }
            }

            return result;
        }

        public static async Task<Dictionary<string, int>> FreqAnalysisFromUrl(string url)
        {
            var httpClient=new HttpClient();

            var content = await httpClient.GetStringAsync(url);

            return FreqAnalysisFromString(content);
        }

        public static Dictionary<string, int> FreqAnalysisFromFile(String file)
        {
            var content = File.ReadAllText(file);

            return FreqAnalysisFromString(content);
        }
    }
}