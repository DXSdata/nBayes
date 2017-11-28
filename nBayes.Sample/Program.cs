using System;

namespace nBayes.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Index spam = Index.CreateMemoryIndex();
            Index notspam = Index.CreateMemoryIndex();

            // train the indexes
            spam.Add(Entry.FromString("want some viagra?"));
            spam.Add(Entry.FromString("cialis can make you larger"));
            notspam.Add(Entry.FromString("Hello, how are you?"));
            notspam.Add(Entry.FromString("Did you go to the park today?"));

            Analyzer analyzer = new Analyzer(
                Entry.FromString("cialis viagra"),
                spam,
                notspam);

            float prediction = analyzer.Prediction;
            Console.WriteLine("Prediction (good < 0.5 > spam): " + prediction);

            CategorizationResult result = analyzer.Category;

            switch (result)
            {
                case CategorizationResult.First:
                    Console.WriteLine("Spam");
                    break;
                case CategorizationResult.Undetermined:
                    Console.WriteLine("Undecided");
                    break;
                case CategorizationResult.Second:
                    Console.WriteLine("Not Spam");
                    break;
            }

            Console.ReadKey();
        }
    }
}