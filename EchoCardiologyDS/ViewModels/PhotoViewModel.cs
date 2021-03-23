using Caliburn.Micro;
using EchoCardiologyDS.Models;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media;
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;

namespace EchoCardiologyDS.ViewModels
{
	public class PhotoViewModel :Screen,IScreen
    {
		private Brush nomokinez = Brushes.Green;
		private Brush gipokinez = Brushes.BurlyWood;
		private Brush diokinez = Brushes.Blue;
		private Brush akinez = Brushes.Red;
		private Brush giper = Brushes.YellowGreen;

		//private Brush nomokinez = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ffaacc"));

		private string H = "H";
		private string Gi = "Гп";
		private string G = "Г";
		private string D = "Д";
		private string A = "А";

		private ObservableCollection<string> _colorComboBox =new ObservableCollection<string> { "Н","Гп", "Г", "Д", "А" };

		public ObservableCollection<string> ColorComboBox
		{
			get { return _colorComboBox; }
			set { _colorComboBox = value; }
		}
		//0-Номокинез
		//1-Гипокинез
		//2-Диcкинез
		//3-Акинез
		#region SelectedComboBoxIndex
		private int _selectedPolygon1;

		public int SelectedPolygon1
		{
			get { return _selectedPolygon1; }
			set { _selectedPolygon1 = value; FillPolygon(nameof(Polygon1), value); NotifyOfPropertyChange(() => SelectedPolygon1); }
		}
		private int _selectedPolygon2;

		public int SelectedPolygon2
		{
			get { return _selectedPolygon2; }
			set { _selectedPolygon2 = value; FillPolygon(nameof(Polygon2), value); NotifyOfPropertyChange(() => SelectedPolygon2); }
		}
		private int _selectedPolygon3;

		public int SelectedPolygon3
		{
			get { return _selectedPolygon3; }
			set { _selectedPolygon3 = value; FillPolygon(nameof(Polygon3), value); NotifyOfPropertyChange(() => SelectedPolygon3); }
		}
		private int _selectedPolygon4;

		public int SelectedPolygon4
		{
			get { return _selectedPolygon4; }
			set { _selectedPolygon4 = value; FillPolygon(nameof(Polygon4), value); NotifyOfPropertyChange(() => SelectedPolygon4); }
		}
		private int _selectedPolygon5;

		public int SelectedPolygon5
		{
			get { return _selectedPolygon5; }
			set { _selectedPolygon5 = value; FillPolygon(nameof(Polygon5), value); NotifyOfPropertyChange(() => SelectedPolygon5); }
		}
		private int _selectedPolygon6;

		public int SelectedPolygon6
		{
			get { return _selectedPolygon6; }
			set { _selectedPolygon6 = value; FillPolygon(nameof(Polygon6), value); NotifyOfPropertyChange(() => SelectedPolygon6); }
		}
		private int _selectedPolygon7;

		public int SelectedPolygon7
		{
			get { return _selectedPolygon7; }
			set { _selectedPolygon7 = value; FillPolygon(nameof(Polygon7), value); NotifyOfPropertyChange(() => SelectedPolygon7); }
		}
		private int _selectedPolygon8;

		public int SelectedPolygon8
		{
			get { return _selectedPolygon8; }
			set { _selectedPolygon8 = value; FillPolygon(nameof(Polygon8), value); NotifyOfPropertyChange(() => SelectedPolygon8); }
		}
		private int _selectedPolygon9;

		public int SelectedPolygon9
		{
			get { return _selectedPolygon9; }
			set { _selectedPolygon9 = value; FillPolygon(nameof(Polygon9), value); NotifyOfPropertyChange(() => SelectedPolygon9); }
		}
		private int _selectedPolygon10;

		public int SelectedPolygon10
		{
			get { return _selectedPolygon10; }
			set { _selectedPolygon10 = value; FillPolygon(nameof(Polygon10), value); NotifyOfPropertyChange(() => SelectedPolygon10); }
		}
		private int _selectedPolygon11;

		public int SelectedPolygon11
		{
			get { return _selectedPolygon11; }
			set { _selectedPolygon11 = value; FillPolygon(nameof(Polygon11), value); NotifyOfPropertyChange(() => SelectedPolygon11); }
		}
		private int _selectedPolygon12;

		public int SelectedPolygon12
		{
			get { return _selectedPolygon12; }
			set { _selectedPolygon12 = value; FillPolygon(nameof(Polygon12), value); NotifyOfPropertyChange(() => SelectedPolygon12); }
		}
		private int _selectedPolygon13;

		public int SelectedPolygon13
		{
			get { return _selectedPolygon13; }
			set { _selectedPolygon13 = value; FillPolygon(nameof(Polygon13), value); NotifyOfPropertyChange(() => SelectedPolygon13); }
		}
		private int _selectedPolygon14;

		public int SelectedPolygon14
		{
			get { return _selectedPolygon14; }
			set { _selectedPolygon14 = value; FillPolygon(nameof(Polygon14), value); NotifyOfPropertyChange(() => SelectedPolygon14); }
		}
		private int _selectedPolygon15;

		public int SelectedPolygon15
		{
			get { return _selectedPolygon15; }
			set { _selectedPolygon15 = value; FillPolygon(nameof(Polygon15), value); NotifyOfPropertyChange(() => SelectedPolygon15); }
		}
		private int _selectedPolygon16;

		public int SelectedPolygon16
		{
			get { return _selectedPolygon16; }
			set { _selectedPolygon16 = value; FillPolygon(nameof(Polygon16), value); NotifyOfPropertyChange(() => SelectedPolygon16); }
		}
		private int _selectedPolygon17;

		public int SelectedPolygon17
		{
			get { return _selectedPolygon17; }
			set { _selectedPolygon17 = value; FillPolygon(nameof(Polygon17), value); NotifyOfPropertyChange(() => SelectedPolygon17); }
		}

		#endregion
		#region Brushes
		private Brush _polygon1 = SegmentRepository.Polygon1;

		public Brush Polygon1
		{
			get { return _polygon1; }
			set { _polygon1 = value;NotifyOfPropertyChange(() => Polygon1); }
		}
		private Brush _polygon2 = SegmentRepository.Polygon2;

		public Brush Polygon2
		{
			get { return _polygon2; }
			set { _polygon2 = value; NotifyOfPropertyChange(() => Polygon2); }
		}
		private Brush _polygon3 = SegmentRepository.Polygon3;

		public Brush Polygon3
		{
			get { return _polygon3; }
			set { _polygon3 = value; NotifyOfPropertyChange(() => Polygon3); }
		}
		private Brush _polygon4 = SegmentRepository.Polygon4;

		public Brush Polygon4
		{
			get { return _polygon4; }
			set { _polygon4 = value; NotifyOfPropertyChange(() => Polygon4); }
		}
		private Brush _polygon5 = SegmentRepository.Polygon5;

		public Brush Polygon5
		{
			get { return _polygon5; }
			set { _polygon5 = value; NotifyOfPropertyChange(() => Polygon5); }
		}
		private Brush _polygon6 = SegmentRepository.Polygon6;

		public Brush Polygon6
		{
			get { return _polygon6; }
			set { _polygon6 = value; NotifyOfPropertyChange(() => Polygon6); }
		}
		private Brush _polygon7 = SegmentRepository.Polygon7;

		public Brush Polygon7
		{
			get { return _polygon7; }
			set { _polygon7 = value; NotifyOfPropertyChange(() => Polygon7); }
		}
		private Brush _polygon8 = SegmentRepository.Polygon8;

		public Brush Polygon8
		{
			get { return _polygon8; }
			set { _polygon8 = value; NotifyOfPropertyChange(() => Polygon8); }
		}
		private Brush _polygon9 = SegmentRepository.Polygon9;

		public Brush Polygon9
		{
			get { return _polygon9; }
			set { _polygon9 = value; NotifyOfPropertyChange(() => Polygon9); }
		}
		private Brush _polygon10 = SegmentRepository.Polygon10;

		public Brush Polygon10
		{
			get { return _polygon10; }
			set { _polygon10 = value; NotifyOfPropertyChange(() => Polygon10); }
		}
		private Brush _polygon11 = SegmentRepository.Polygon11;

		public Brush Polygon11
		{
			get { return _polygon11; }
			set { _polygon11 = value; NotifyOfPropertyChange(() => Polygon11); }
		}
		private Brush _polygon12 = SegmentRepository.Polygon12;

		public Brush Polygon12
		{
			get { return _polygon12; }
			set { _polygon12 = value; NotifyOfPropertyChange(() => Polygon12); }
		}
		private Brush _polygon13 = SegmentRepository.Polygon13;

		public Brush Polygon13
		{
			get { return _polygon13; }
			set { _polygon13 = value; NotifyOfPropertyChange(() => Polygon13); }
		}
		private Brush _polygon14 = SegmentRepository.Polygon14;

		public Brush Polygon14
		{
			get { return _polygon14; }
			set { _polygon14 = value; NotifyOfPropertyChange(() => Polygon14); }
		}
		private Brush _polygon15 = SegmentRepository.Polygon15;

		public Brush Polygon15
		{
			get { return _polygon15; }
			set { _polygon15 = value; NotifyOfPropertyChange(() => Polygon15); }
		}
		private Brush _polygon16 = SegmentRepository.Polygon16;

		public Brush Polygon16
		{
			get { return _polygon16; }
			set { _polygon16 = value; NotifyOfPropertyChange(() => Polygon16); }
		}
		private Brush _polygon17 = SegmentRepository.Polygon17;

		public Brush Polygon17
		{
			get { return _polygon17; }
			set { _polygon17 = value; NotifyOfPropertyChange(() => Polygon17); }
		}

		#endregion
		#region CircleLabelValue
		private string _PolygonText1 = SegmentRepository.PolygonText1;

		public string PolygonText1
		{
			get { return _PolygonText1; }
			set { _PolygonText1 = value; NotifyOfPropertyChange(() => PolygonText1); }
		}
		private string _PolygonText2 = SegmentRepository.PolygonText2;

		public string PolygonText2
		{
			get { return _PolygonText2; }
			set { _PolygonText2 = value; NotifyOfPropertyChange(() => PolygonText2); }
		}
		private string _PolygonText3 = SegmentRepository.PolygonText3;

		public string PolygonText3
		{
			get { return _PolygonText3; }
			set { _PolygonText3 = value; NotifyOfPropertyChange(() => PolygonText3); }
		}
		private string _PolygonText4 = SegmentRepository.PolygonText4;

		public string PolygonText4
		{
			get { return _PolygonText4; }
			set { _PolygonText4 = value; NotifyOfPropertyChange(() => PolygonText4); }
		}
		private string _PolygonText5 = SegmentRepository.PolygonText5;

		public string PolygonText5
		{
			get { return _PolygonText5; }
			set { _PolygonText5 = value; NotifyOfPropertyChange(() => PolygonText5); }
		}
		private string _PolygonText6 = SegmentRepository.PolygonText6;

		public string PolygonText6
		{
			get { return _PolygonText6; }
			set { _PolygonText6 = value; NotifyOfPropertyChange(() => PolygonText6); }
		}
		private string _PolygonText7 = SegmentRepository.PolygonText7;

		public string PolygonText7
		{
			get { return _PolygonText7; }
			set { _PolygonText7 = value; NotifyOfPropertyChange(() => PolygonText7); }
		}
		private string _PolygonText8 = SegmentRepository.PolygonText8;

		public string PolygonText8
		{
			get { return _PolygonText8; }
			set { _PolygonText8 = value; NotifyOfPropertyChange(() => PolygonText8); }
		}
		private string _PolygonText9 = SegmentRepository.PolygonText9;

		public string PolygonText9
		{
			get { return _PolygonText9; }
			set { _PolygonText9 = value; NotifyOfPropertyChange(() => PolygonText9); }
		}
		private string _PolygonText10 = SegmentRepository.PolygonText10;

		public string PolygonText10
		{
			get { return _PolygonText10; }
			set { _PolygonText10 = value; NotifyOfPropertyChange(() => PolygonText10); }
		}
		private string _PolygonText11 = SegmentRepository.PolygonText11;

		public string PolygonText11
		{
			get { return _PolygonText11; }
			set { _PolygonText11 = value; NotifyOfPropertyChange(() => PolygonText11); }
		}
		private string _PolygonText12 = SegmentRepository.PolygonText12;

		public string PolygonText12
		{
			get { return _PolygonText12; }
			set { _PolygonText12 = value; NotifyOfPropertyChange(() => PolygonText12); }
		}
		private string _PolygonText13 = SegmentRepository.PolygonText13;

		public string PolygonText13
		{
			get { return _PolygonText13; }
			set { _PolygonText13 = value; NotifyOfPropertyChange(() => PolygonText13); }
		}
		private string _PolygonText14 = SegmentRepository.PolygonText14;

		public string PolygonText14
		{
			get { return _PolygonText14; }
			set { _PolygonText14 = value; NotifyOfPropertyChange(() => PolygonText14); }
		}
		private string _PolygonText15 = SegmentRepository.PolygonText15;

		public string PolygonText15
		{
			get { return _PolygonText15; }
			set { _PolygonText15 = value; NotifyOfPropertyChange(() => PolygonText15); }
		}
		private string _PolygonText16 = SegmentRepository.PolygonText16;

		public string PolygonText16
		{
			get { return _PolygonText16; }
			set { _PolygonText16 = value; NotifyOfPropertyChange(() => PolygonText16); }
		}
		private string _PolygonText17 = SegmentRepository.PolygonText17;

		public string PolygonText17
		{
			get { return _PolygonText17; }
			set { _PolygonText17 = value; NotifyOfPropertyChange(() => PolygonText17); }
		}

		#endregion
		public void Save()
		{
		}

		public void FillPolygon(string name,int index)
		{
			string t = "PolygonText" + GetPolygonNum(name);
			switch (index)
			{
				case 0:
					this.GetType().GetProperty(name).SetValue(this, nomokinez, null);
					typeof(SegmentRepository).GetField(name).SetValue(typeof(SegmentRepository), nomokinez);
					this.GetType().GetProperty(t).SetValue(this, H, null);
					typeof(SegmentRepository).GetField(t).SetValue(typeof(SegmentRepository), H);

					break;
				case 1:
					this.GetType().GetProperty(name).SetValue(this, giper, null);
					typeof(SegmentRepository).GetField(name).SetValue(typeof(SegmentRepository), giper);
					this.GetType().GetProperty(t).SetValue(this, Gi, null);
					typeof(SegmentRepository).GetField(t).SetValue(typeof(SegmentRepository), Gi);
					break;
				case 2:
					this.GetType().GetProperty(name).SetValue(this, gipokinez, null);
					typeof(SegmentRepository).GetField(name).SetValue(typeof(SegmentRepository), gipokinez);
					this.GetType().GetProperty(t).SetValue(this, G, null);
					typeof(SegmentRepository).GetField(t).SetValue(typeof(SegmentRepository), G);

					break;
				case 3:
					this.GetType().GetProperty(name).SetValue(this, diokinez, null);
					typeof(SegmentRepository).GetField(name).SetValue(typeof(SegmentRepository), diokinez);
					this.GetType().GetProperty(t).SetValue(this, D, null);
					typeof(SegmentRepository).GetField(t).SetValue(typeof(SegmentRepository), D);

					break;
				case 4:
					this.GetType().GetProperty(name).SetValue(this, akinez, null);
					typeof(SegmentRepository).GetField(name).SetValue(typeof(SegmentRepository), akinez);
					this.GetType().GetProperty(t).SetValue(this, A, null);
					typeof(SegmentRepository).GetField(t).SetValue(typeof(SegmentRepository), A);

					break;
				default:
					this.GetType().GetProperty(name).SetValue(this, nomokinez, null);
					typeof(SegmentRepository).GetField(name).SetValue(typeof(SegmentRepository), nomokinez);
					this.GetType().GetProperty(t).SetValue(this, H, null);
					typeof(SegmentRepository).GetField(t).SetValue(typeof(SegmentRepository), H);

					break;
			}

		}

		public static string GetPolygonNum(string str,string removeString= "Polygon")
		{
			return str.Substring(removeString.Length);
		}
	}
}
