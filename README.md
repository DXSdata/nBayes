# nBayes for .NET Core / .NET Standard 2.0
This is a fork of https://github.com/joelmartinez/nBayes.
While the original repos is used with .NET Framework, you can use this one with .NET Core / .NET Standard.
A Nuget package is also available.


# Sample
With v3, I did a few analyzer bugfixes and changed methods and properties for simpler, consistent usage, different to the original project.



```CSharp
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
```

# Links
- Website http://www.dxsdata.com/de/2017/07/bayes-classifier-library-for-net-core/