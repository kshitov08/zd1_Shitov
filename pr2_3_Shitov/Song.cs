namespace pr2_3_Shitov
{
    public struct Song
    {
        public string Author;
        public string Title;
        public string Filename;
        public override string ToString()
        {
            return $"{Author} - {Title}";
        }
    }
}