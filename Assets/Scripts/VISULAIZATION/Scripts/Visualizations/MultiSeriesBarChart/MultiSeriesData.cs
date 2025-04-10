using System.Collections.Generic;

/// <summary>
/// Represents processed data for a multi-series bar chart.
/// This class holds a collection of <see cref="SeriesData"/> objects, each representing a series in the chart.
/// </summary>
class MultiSeriesData : IProcessedData
{
    /// <summary>
    /// Gets the list of data for each series in the multi-series bar chart.
    /// Each entry in the list represents data for a specific series.
    /// </summary>
    public List<SeriesData> SeriesData { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MultiSeriesData"/> class
    /// with a provided list of series data.
    /// </summary>
    /// <param name="seriesData">A list of series data for the multi-series bar chart.</param>
    public MultiSeriesData(List<SeriesData> seriesData)
    {
        SeriesData = seriesData ?? new List<SeriesData>();
    }

    /// <summary>
    /// Adds a new series data to the multi-series data list.
    /// </summary>
    /// <param name="data">The series data to add to the list.</param>
    public void AddSeriesData(SeriesData data)
    {
        if (data == null)
        {
            throw new System.ArgumentNullException(nameof(data), "Series data cannot be null.");
        }
        SeriesData.Add(data);
    }

    /// <summary>
    /// Removes all series data from the multi-series data list.
    /// </summary>
    public void ClearData()
    {
        SeriesData.Clear();
    }
}




