namespace Data
{
    public static class FreqAnalysis
    {
        public static Dictionary<string,int> FreqAnalysisFromString(string input)
        {
            Dictionary<string,int> result = new Dictionary<string,int>();

            string[] separators = { ".", ",", " ", Environment.NewLine };
            string[] parts = input.Split(separators,StringSplitOptions.RemoveEmptyEntries);

            foreach (string part in parts)
            {
                if (result.ContainsKey(part))
                {
                    result[part] += result[part];
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