using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brushes = System.Windows.Media.Brushes;
using Brush = System.Windows.Media.Brush;
using System.Windows.Media;

namespace EchoCardiologyDS.Models
{
	public static class SegmentRepository
	{
		public static Brush green = (SolidColorBrush)(new BrushConverter().ConvertFrom("#66ff00"));

		public static string initialText = "H";

		public static Brush Polygon1 = green;
		public static Brush Polygon2 = green;
		public static Brush Polygon3 = green;
		public static Brush Polygon4 = green;
		public static Brush Polygon5 = green;
		public static Brush Polygon6 = green;
		public static Brush Polygon7 = green;
		public static Brush Polygon8 = green;
		public static Brush Polygon9 = green;
		public static Brush Polygon10 = green;
		public static Brush Polygon11 = green;
		public static Brush Polygon12 = green;
		public static Brush Polygon13 = green;
		public static Brush Polygon14 = green;
		public static Brush Polygon15 = green;
		public static Brush Polygon16 = green;
		public static Brush Polygon17 = green;

		public static string PolygonText1 = initialText;
		public static string PolygonText2 = initialText;
		public static string PolygonText3 = initialText;
		public static string PolygonText4 = initialText;
		public static string PolygonText5 = initialText;
		public static string PolygonText6 = initialText;
		public static string PolygonText7 = initialText;
		public static string PolygonText8 = initialText;
		public static string PolygonText9 = initialText;
		public static string PolygonText10 = initialText;
		public static string PolygonText11 = initialText;
		public static string PolygonText12 = initialText;
		public static string PolygonText13 = initialText;
		public static string PolygonText14 = initialText;
		public static string PolygonText15 = initialText;
		public static string PolygonText16 = initialText;
		public static string PolygonText17 = initialText;

		public static void RefreshItmes()
		{
			var fields = typeof(SegmentRepository).GetFields();
			var segmentType = typeof(SegmentRepository);
			foreach (var f in fields)
			{
				if (f.FieldType.Name == nameof(String))
				{
					f.SetValue(segmentType, "H");
				}
				else
				{
					f.SetValue(segmentType, green);
				}
			}
		}

	}
}
