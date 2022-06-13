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
        public Dictionary<string, int> Words { get; set; }

        public override string ToString()
        {
            return $"{Source} {Words?.Count}";   //  Zápis stringu od .NET5
        }
    }

    public enum SourceType
    {
        URL,
        FILE
    }
}