using System.Collections.Generic;

/// <summary>
/// Represents the data for a pie chart, encapsulating a collection of pie slice data.
/// Implements the <see cref="IProcessedData"/> interface.
/// </summary>
class PieChartData : IProcessedData
{
    /// <summary>
    /// Gets the list of pie slice data.
    /// </summary>
    public List<PieSliceData> PieSliceData { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PieChartData"/> class with the specified pie slice data.
    /// </summary>
    /// <param name="pieSliceData">A list of pie slice data to initialize the instance with. If null, an empty list is created.</param>
    public PieChartData(List<PieSliceData> pieSliceData)
    {
        PieSliceData = pieSliceData ?? new List<PieSliceData>();
    }

    /// <summary>
    /// Adds a new pie slice data to the collection.
    /// </summary>
    /// <param name="data">The pie slice data to add.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when the provided data is null.</exception>
    public void AddPieSliceData(PieSliceData data)
    {
        if (data == null)
        {
            throw new System.ArgumentNullException(nameof(data), "Pie slice data cannot be null.");
        }
        PieSliceData.Add(data);
    }

    /// <summary>
    /// Clears all pie slice data from the collection.
    /// </summary>
    public void ClearData()
    {
        PieSliceData.Clear();
    }
}
