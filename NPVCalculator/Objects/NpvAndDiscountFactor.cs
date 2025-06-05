namespace NPVCalculator.Objects
{
    /// <summary>
    /// Simple record for NPV and its discount factor.
    /// </summary>
    /// <param name="Npv">NPV value.</param>
    /// <param name="DiscountFactor">Discount factor value.</param>
    public record NpvAndDiscountFactor(decimal Npv, decimal? DiscountFactor);
}
