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

        [Fact]
        public void RoundedNpvCalcTests()
        {
            var NCFs = new List<decimal> { 85000, 85000, 110000, 115000, 135000 };

            var npvCalc = new NpvCalc(NCFs, 25) { UseRoundedDiscountingFactor = true };
            Assert.True(npvCalc.NCFs.Count == 5);

            var npvs = npvCalc.GetNPVs(275000);
        }
    }
}
