using System.Collections.Generic;

/// <summary>
/// Represents the data for a stacked multi-series bar chart.
/// </summary>
public class StackedMultiSeriesData : IProcessedData
{
    public List<StackedSeriesData> StackedSeriesData { get; set; } = new List<StackedSeriesData>();
}

/// <summary>
/// Represents a single series of data in the stacked multi-series chart.
/// </summary>
public class StackedSeriesData
{
    public List<StackedBarData> Bars { get; set; } = new List<StackedBarData>();
}

/// <summary>
/// Represents data for a single stacked bar, including segment values.
/// </summary>
public class StackedBarData
{
    public List<double> SegmentValues { get; set; } = new List<double>();
}
