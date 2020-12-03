using EchoCardiologyDS.Models;
using EchoCardiologyDS.WordImplementation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EchoCardiologyDS.Views
{
	/// <summary>
	/// Interaction logic for PhotoView.xaml
	/// </summary>
	public partial class PhotoView : Window
    {
		public  int width = 200;
		public  int height = 150;
		private readonly string _savePdfDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Отчеты";
		public PhotoView()
        {
            InitializeComponent();
			CreateFolder();
        }
		private void CreateFolder()
		{
			_savePdfDocumentsPath.CreateFolder();
		}
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			SaveImages();
			this.Close();

		}
		public void SaveImages()
		{
			Segment segment = new Segment();
			List<byte[]> bytes = new List<byte[]>();
			List<UserControl> segments = new List<UserControl>
			{
				FirstSegmnet,SecondSegment,ThirdSegment,ForthSegment,FifthSegment,SixSegment,MiddleCircleSegment,CircleSegment
			};
			int i = 0;
			foreach (var item in segments)
			{				
				var bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				RenderTargetBitmap renderTargetBitmap =
				new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);

				renderTargetBitmap.Render(item);

				PngBitmapEncoder pngImage = new PngBitmapEncoder();
				pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
				i++;
				
				using (Stream fileStream = File.Create($@"{_savePdfDocumentsPath}\{ConstTableName.filePath}{i}.jpg"))
				{
					pngImage.Save(fileStream);

				}
				using (FileStream fs = new FileStream($@"{_savePdfDocumentsPath}\{ConstTableName.filePath}{i}.jpg", FileMode.Open, FileAccess.Read))
				{
					byte[] myByte = new byte[fs.Length];
					fs.Read(myByte, 0, Convert.ToInt32(fs.Length));
					bytes.Add(myByte);
				}
			}

			segment.FirstSegment = bytes[0];
			segment.SecondSegment = bytes[1];
			segment.ThirdSegment = bytes[2];
			segment.ForthSegment = bytes[3];
			segment.FifthSegment = bytes[4];
			segment.SixSegment = bytes[5];
			segment.MiddleCircleSegment = bytes[6];
			segment.CirlceSegment = bytes[7];
			ConstTableName.segment = segment;
		}
		

	}
}
