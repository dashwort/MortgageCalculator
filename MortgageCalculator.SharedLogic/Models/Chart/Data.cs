namespace MortgageCalculator.SharedLogic.Models.Chart
{
    public class Data
    {
        public string[] labels { get; set; }
        public Dataset[] datasets { get; set; }

        public void AddDataSet(Dataset dataset)
        {
            if (dataset == null)
                return;

            var sets = new List<Dataset>();

            if (datasets != null)
                sets.AddRange(datasets);

            sets.Add(dataset);

            datasets = sets.ToArray();
        }
    }
}