namespace NPVCalculator.Test
{
    public class NPVCalcTests
    {
        [Fact]
        public void StdNpvCalcTests()
        {
            // Create a list of NCFs
            var NCFs = new List<decimal> { 45000, 35000, 20000, 20000, 15000 };

            // Create a NPV calculator with a discount rate of 10% - this should have 5 NCFs
            var npvCalc = new NpvCalc(NCFs, 10);
            Assert.True(npvCalc.NCFs.Count == 5);

            var npvs = npvCalc.GetNPVs(100000);
        }
    }
}
