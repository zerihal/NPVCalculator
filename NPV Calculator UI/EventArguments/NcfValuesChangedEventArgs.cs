namespace NPV_Calculator_UI.EventArguments
{
    public class NcfValuesChangedEventArgs : EventArgs
    {
        /// <summary>
        /// NCF collection change type.
        /// </summary>
        public NcfCollectionChangeType Change { get; }

        /// <summary>
        /// Count of NCFs currently in the collection.
        /// </summary>
        public int NewValuesCount { get; }

        public NcfValuesChangedEventArgs(NcfCollectionChangeType change, int valuesCount)
        {
            Change = change;
            NewValuesCount = valuesCount;
        }
    }
}
