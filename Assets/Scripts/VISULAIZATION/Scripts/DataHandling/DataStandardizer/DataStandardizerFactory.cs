public class DataStandardizerFactory
{
    public IDataStandardizer CreateStandardizer(VisualizationType visualization)
    {
        switch (visualization)
        {
            case VisualizationType.ScatterPlot:
                return new ScatterPlotStandardizer();
            // case VisualizationType.BarChart:
            //     return new BarChartStandardizer();
        }
        // return the apppropriate visualization (standardizer) according to server type

        throw new System.ArgumentException("Unsupported server type");
    }
}