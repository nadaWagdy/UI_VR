
/// <summary>
/// Represents the data for a single slice of a pie chart.
/// </summary>
public class PieSliceData
{
    /// <summary>
    /// Gets the label associated with the pie slice.
    /// </summary>
    public string Label { get; private set; } 

    /// <summary>
    /// Gets the value of the pie slice, representing its size relative to the whole pie.
    /// </summary>
    public double Value { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PieSliceData"/> class with the specified label and value.
    /// </summary>
    /// <param name="label">The label for the pie slice.</param>
    /// <param name="value">The value of the pie slice.</param>
    public PieSliceData(string label, double value)
    {
        Label = label;
        Value = value;
    }
}

