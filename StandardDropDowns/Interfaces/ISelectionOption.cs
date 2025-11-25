namespace StandardDropdowns.Interfaces
{
    /// <summary>
    /// Represents a generic option for select/dropdown lists.
    /// All dropdown data models implement this interface for easy conversion to UI controls.
    /// </summary>
    public interface ISelectOption
    {
        /// <summary>
        /// The value to be submitted when this option is selected (e.g., "TX", "US", "1").
        /// </summary>
        string Value { get; }

        /// <summary>
        /// The display text shown to users (e.g., "Texas", "United States", "January").
        /// </summary>
        string Text { get; }
    }
}