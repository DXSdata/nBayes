namespace nBayes
{
    using System;

    public class Analyzer
    {
        private float I = 0;
        private float invI = 0;

        private float _prediction;

        private Entry item;
        private Index first;
        private Index second;
        private float tolerance;

        bool calculated = false;

        public Analyzer(Entry item, Index first, Index second, float tolerance = .05f)
        {
            this.item = item;
            this.first = first;
            this.second = second;
            this.tolerance = tolerance;
        }        

        public float Prediction
        {
            get
            {
                if (!calculated)
                    _prediction = GetPrediction();

                return _prediction;
            }
        }

        public CategorizationResult Category
        {
            get
            {
                if (Prediction <= .5f - tolerance)
                    return CategorizationResult.Second;

                if (Prediction >= .5 + tolerance)
                    return CategorizationResult.First;
                
                return CategorizationResult.Undetermined;
            }
        }

        private float GetPrediction()
        {
            I = 0;
            invI = 0;

            foreach (string token in item)
            {
                int firstCount = first.GetTokenCount(token);
                int secondCount = second.GetTokenCount(token);

                float probability = CalcProbability(firstCount, first.EntryCount, secondCount, second.EntryCount);

                Console.WriteLine("{0}: [{1}] ({2}-{3}), ({4}-{5})",
                    token,
                    probability,
                    firstCount,
                    first.EntryCount,
                    secondCount,
                    second.EntryCount);
            }

            float prediction = CombineProbability();

            calculated = true;

            return prediction;
        }

        #region Private Methods

        /// <summary>
        /// Calculates a probablility value based on statistics from two categories
        /// </summary>
        private float CalcProbability(float cat1count, float cat1total, float cat2count, float cat2total)
        {
            float bw = cat1count / cat1total;
            float gw = cat2count / cat2total;
            float pw = ((bw) / ((bw) + (gw)));
            float
                s = 1f,
                x = .5f,
                n = cat1count + cat2count;
            float fw = ((s * x) + (n * pw)) / (s + n);

            LogProbability(fw);

            return fw;
        }

        private void LogProbability(float prob)
        {
            if (!float.IsNaN(prob))
            {
                I = I == 0 ? prob : I * prob;
                invI = invI == 0 ? (1 - prob) : invI * (1 - prob);
            }
        }

        private float CombineProbability()
        {
            return I / (I + invI);
        }

        #endregion
    }
}
