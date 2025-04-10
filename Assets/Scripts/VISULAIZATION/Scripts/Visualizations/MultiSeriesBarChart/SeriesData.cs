using System.Collections.Generic;
using System;

/// <summary>
/// Represents a series of data values at a specific altitude.
/// </summary>
[Serializable]
public class SeriesData
{
    /// <summary>
    /// The altitude value for the series.
    /// </summary>
    public double Altitude { get; private set; }

    /// <summary>
    /// A list of data values for the series.
    /// </summary>
    public List<double> Values { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SeriesData"/> class with a specified altitude and values.
    /// </summary>
    /// <param name="Altitude">The altitude associated with the data series.</param>
    /// <param name="Values">The list of data values for the series.</param>
    public SeriesData(double Altitude, List<double> Values)
    {
        this.Altitude = Altitude;
        this.Values = Values ?? new List<double>();
    }
}


