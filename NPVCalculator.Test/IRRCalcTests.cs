namespace NPVCalculator.Test
{
    public class IRRCalcTests
    {
        [Fact]
        public void StdIRRCalcTest()
        {
            // Create a list of NCFs
            var NCFs = new List<decimal> { 45000, 35000, 20000, 20000, 15000 };

            // Get an IRR calculation based on min of 10% and max of 30% with 1% intervals
            var irr = IRRCalc.GetIRR(NCFs, 100000, 10, 30, 1, out var irrNpv);

            // A IRR NPV should have been found
            Assert.NotNull(irrNpv);

            // Using the above figures, an IRR of 14% should have been found
            Assert.Equal(14, irr);
        }

        [Fact]
        public void FractionalIRRCalcTest()
        {
            // Create a list of NCFs
            var NCFs = new List<decimal> { 85000, 85000, 110000, 115000, 135000 };

            // Get an IRR calculation based on min of 10% and max of 30% with 0.5% intervals
            var irr = IRRCalc.GetIRR(NCFs, 275000, 10, 30, 0.5, out var irrNpv, true);

            // A IRR NPV should have been found
            Assert.NotNull(irrNpv);

            // Using the above figures, an IRR of 14% should have been found
            Assert.Equal(24, irr);
        }
    }
}
