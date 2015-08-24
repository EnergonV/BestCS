// BestCS debugging visualizers
// BestCS.NET framework
// http://www.aforgenet.com/framework/
//
// Copyright © BestCS.NET, 2011
// contacts@aforgenet.com
//

using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: System.Diagnostics.DebuggerVisualizer(
    typeof( BestCS.DebuggerVisualizers.BitmapDataVisualizer ),
    typeof( BestCS.DebuggerVisualizers.BitmapDataObjectSource ),
    Target = typeof( System.Drawing.Imaging.BitmapData ),
    Description = "Bitmap Data Visualizer" )]

namespace BestCS.DebuggerVisualizers
{
    class BitmapDataVisualizer : DialogDebuggerVisualizer
    {
        override protected void Show( IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider )
        {
            Image image = (Image) objectProvider.GetObject( );

            ImageView imageViewer = new ImageView( );
            imageViewer.SetImage( image );

            windowService.ShowDialog( imageViewer );
        }
    }

    public class BitmapDataObjectSource : VisualizerObjectSource
    {
        public override void GetData( object target, Stream outgoingData )
        {
            BinaryFormatter bf = new BinaryFormatter( );
            bf.Serialize( outgoingData,
                ( new BestCS.Imaging.UnmanagedImage( (BitmapData) target ) ).ToManagedImage( ) );
        }
    }
}
