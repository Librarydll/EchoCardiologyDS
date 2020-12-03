using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brushes = System.Windows.Media.Brushes;
using Brush = System.Windows.Media.Brush;

namespace EchoCardiologyDS.Models
{
	public static class SegmentRepository
	{
		public static string initialText = "H";

		public static Brush Polygon1 = Brushes.Green;
		public static Brush Polygon2 = Brushes.Green;
		public static Brush Polygon3 = Brushes.Green;
		public static Brush Polygon4 = Brushes.Green;
		public static Brush Polygon5 = Brushes.Green;
		public static Brush Polygon6 = Brushes.Green;
		public static Brush Polygon7 = Brushes.Green;
		public static Brush Polygon8 = Brushes.Green;
		public static Brush Polygon9 = Brushes.Green;
		public static Brush Polygon10 = Brushes.Green;
		public static Brush Polygon11 = Brushes.Green;
		public static Brush Polygon12 = Brushes.Green;
		public static Brush Polygon13 = Brushes.Green;
		public static Brush Polygon14 = Brushes.Green;
		public static Brush Polygon15 = Brushes.Green;
		public static Brush Polygon16 = Brushes.Green;
		public static Brush Polygon17 = Brushes.Green;

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
					f.SetValue(segmentType, Brushes.Green);
				}
			}
		}

	}
}
