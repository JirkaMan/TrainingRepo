namespace Model
{
    /// <summary>
    /// Výsledek frekvenční analýzi pro jeden zdroj (soubor nebo url)
    /// </summary>
    public class FAResult
    {
        /// <summary>
        /// Zdroj textu
        /// </summary>
        public string Source { get; set; }
        
        /// <summary>
        /// Typ zdroje (url,file,db ...)
        /// </summary>
        public SourceType SourceType { get; set; }

        /// <summary>
        /// Vysledna frekvencni analyaza slov
        /// </summary>
        public Dictionary<string, int> Words { get; set; } = new Dictionary<string, int>();

        /// <summary>
        /// Metoda vrátí top 10 s největším počtem výskytů
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetTopTen()
            => (Dictionary<string,int>)Words.OrderByDescending(kv => kv.Value).Take(10);

        public override string ToString() => $"{Source} , pocet: {Words?.Count}";    // zkrácený zápis přes lambdu

    }

    public enum SourceType
    {
        URL,
        FILE
    }
}