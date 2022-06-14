using Model;

namespace Data
{
    public static class FreqAnalysis
    {
        private static Dictionary<string,int> FreqAnalysisFromString(string input)
        {
            Dictionary<string,int> result = new Dictionary<string,int>();

            var parts = input.Split(Environment.NewLine);

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

        public static async Task<FAResult> FreqAnalysisFromUrl(string url)
        {
            var httpClient=new HttpClient();
            var content = await httpClient.GetStringAsync(url);
            var dict = FreqAnalysisFromString(content);

            return new FAResult()
            {
                Source = url,
                SourceType = SourceType.URL,
                Words = dict
            };
        }

        public static FAResult FreqAnalysisFromFile(String file)
        {
            var content = File.ReadAllText(file);
            var dict = FreqAnalysisFromString(content);

            return new FAResult()
            {
                Source = file,
                SourceType = SourceType.FILE,
                Words = dict
            };
        }
    }
}