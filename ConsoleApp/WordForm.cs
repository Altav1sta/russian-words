namespace ConsoleApp
{
    internal class WordForm
    {
        public string Text { get; set; }

        public string[] MorphologicalCharacteristics { get; set; }

        public string Spelling { get; set; }

        public double? Frequency { get; set; }

        public bool IsName { get; set; }

        public string NameKind { get; set; }

        public int Id { get; set; }

        public bool IsLemma { get; set; }

        public int LemmaId { get; set; }

        public int BaseFormId { get; set; }

        public bool NotInUse { get; set; }
    }
}
