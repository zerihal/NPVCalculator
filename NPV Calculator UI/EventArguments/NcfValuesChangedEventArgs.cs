namespace NPV_Calculator_UI.EventArguments
{
    public class NcfValuesChangedEventArgs : EventArgs
    {
        public NcfCollectionChangeType Change { get; }

        public int NewValuesCount { get; }

        public NcfValuesChangedEventArgs(NcfCollectionChangeType change, int valuesCount)
        {
            Change = change;
            NewValuesCount = valuesCount;
        }
    }
}
