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

            // With initial investment of 100,000, the total NPV should be 7835
            var npvs = npvCalc.GetNPVs(100000);
            Assert.True(npvs.TotalNPV == 7835);
        }

        [Fact]
        public void RoundedNpvCalcTests()
        {
            // Create a list of NCFs
            var NCFs = new List<decimal> { 85000, 85000, 110000, 115000, 135000 };

            // Create a NPV calculator with a discount rate of 10% - this should have 5 NCFs
            var npvCalc = new NpvCalc(NCFs, 10) { UseRoundedDiscountingFactor = true };
            Assert.True(npvCalc.NCFs.Count == 5);

            // With initial investment of 275,000, the total NPV should be 117,300
            var npvs = npvCalc.GetNPVs(275000);
            Assert.True(npvs.TotalNPV == 117300);
        }
    }
}
