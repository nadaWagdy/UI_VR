using System;

/// <summary>
/// Represents a single data point in a 3D heat map, with x and z coordinates, and intensity representing the heat value.
/// </summary>
[Serializable]
public class HeatMapUsingBarsCellData
{
    /// <summary>
    /// Gets or sets the unique identifier for this cell data point.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Gets or sets the x-coordinate of the data point in the 3D heat map.
    /// </summary>
    public double X { get; private set; }

    /// <summary>
    /// Gets or sets the z-coordinate of the data point in the 3D heat map.
    /// </summary>
    public double Z { get; private set; }

    /// <summary>
    /// Gets or sets the intensity (heat value) of the data point, which influences the bar height or color.
    /// </summary>
    public double Intensity { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="HeatMapCellData"/> class with specified X, Z coordinates and intensity.
    /// </summary>
    /// <param name="X">The X-coordinate of the cell.</param>
    /// <param name="Z">The Z-coordinate of the cell.</param>
    /// <param name="Intensity">The heat intensity value for the cell.</param>
    public HeatMapUsingBarsCellData(double X, double Z, double Intensity)
    {
        this.X = X; 
        this.Z = Z;
        this.Intensity = Intensity;
    }
}


