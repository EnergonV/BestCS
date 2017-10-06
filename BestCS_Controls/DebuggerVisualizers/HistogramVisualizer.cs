// BestCS debugging visualizers
// BestCS.NET framework
// http://www.aforgenet.com/framework/
//
// Copyright © BestCS.NET, 2011
// contacts@aforgenet.com
//

using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: System.Diagnostics.DebuggerVisualizer(
    typeof( BestCS.DebuggerVisualizers.HistogramVisualizer ),
    typeof( VisualizerObjectSource ),
    Target = typeof(  BestCS.Math.Histogram ),
    Description = "Histogram Visualizer" )]

namespace BestCS.DebuggerVisualizers
{
    public class HistogramVisualizer : DialogDebuggerVisualizer
    {
        override protected void Show( IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider )
        {
             BestCS.Math.Histogram histogram = ( BestCS.Math.Histogram) objectProvider.GetObject( );

            HistogramView histogramView = new HistogramView( );
            histogramView.SetHistogram( histogram );

            windowService.ShowDialog( histogramView );
        }
    }
}
