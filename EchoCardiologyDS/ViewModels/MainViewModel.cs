using Caliburn.Micro;
using EchoCardiologyDS.Models;
using EchoCardiologyDS.Models.MysqlModel;
using EchoCardiologyDS.PfdImplementation;
using EchoCardiologyDS.WordImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Reflection;
using System.IO;
using EchoCardiologyDS.Core;

namespace EchoCardiologyDS.ViewModels
{
	public class MainViewModel : Screen, IHandle<DBConn>
	{
		public DateTime Now = DateTime.Now;
		DBConn _db = null;
		IView view = null;
		IEventAggregator _eventAggregator;
		PatientMV patient = null;
		IWindowManager window = new WindowManager();
		private readonly string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Отчеты";
		#region ComboBoxex
		//Комбобох для наименование аппаратов
		private IEnumerable<string> _apparatComboBox;

		public IEnumerable<string> ApparatComboBox
		{
			get { return _apparatComboBox; }
			set { _apparatComboBox = value; NotifyOfPropertyChange(() => ApparatComboBox); }
		}
		//Комбобох для качество визуализации
		private IEnumerable<string> _visualizationQualityComboBox;

		public IEnumerable<string> VisualizationQualityComboBox
		{
			get { return _visualizationQualityComboBox; }
			set { _visualizationQualityComboBox = value; NotifyOfPropertyChange(() => VisualizationQualityComboBox); }
		}
		private IEnumerable<string> _eCGComboBox;

		//Комбобох для ЭГК ритма
		public IEnumerable<string> ECGComboBox
		{
			get { return _eCGComboBox; }
			set { _eCGComboBox = value; NotifyOfPropertyChange(() => ECGComboBox); }
		}
		private IEnumerable<string> _rigthStomachComboBox;

		//Комбобох для правого желудочка
		public IEnumerable<string> RigthStomachComboBox
		{
			get { return _rigthStomachComboBox; }
			set { _rigthStomachComboBox = value; NotifyOfPropertyChange(() => RigthStomachComboBox); }
		}

		//Комбобох для миокарда
		private IEnumerable<string> _miokardComboBox;

		public IEnumerable<string> MiokardComboBox
		{
			get { return _miokardComboBox; }
			set { _miokardComboBox = value; NotifyOfPropertyChange(() => MiokardComboBox); }
		}

		private IEnumerable<string> _evaluateIndexComboBox;

		//Комбобох для Максимальный градиент давлени на клапане легоной артерии
		public IEnumerable<string> EvaluateIndexComboBox
		{
			get { return _evaluateIndexComboBox; }
			set { _evaluateIndexComboBox = value; NotifyOfPropertyChange(() => EvaluateIndexComboBox); }
		}

		private IEnumerable<string> _movementComboBox;

		public IEnumerable<string> MovementComboBox
		{
			get { return _movementComboBox; }
			set { _movementComboBox = value; NotifyOfPropertyChange(() => MovementComboBox); }
		}
		private IEnumerable<string> _fVComboBox;

		public IEnumerable<string> FVComboBox
		{
			get { return _fVComboBox; }
			set { _fVComboBox = value; NotifyOfPropertyChange(() => FVComboBox); }
		}
		private IEnumerable<string> _iNSComboBox;

		public IEnumerable<string> INSComboBox
		{
			get { return _iNSComboBox; }
			set { _iNSComboBox = value; NotifyOfPropertyChange(() => INSComboBox); }
		}
		private IEnumerable<string> _valveComboBox;

		public IEnumerable<string> ValveComboBox
		{
			get { return _valveComboBox; }
			set { _valveComboBox = value; NotifyOfPropertyChange(() => ValveComboBox); }
		}


		private IEnumerable<string> _mKLevel;

		public IEnumerable<string> MKLevel
		{
			get { return _mKLevel; }
			set { _mKLevel = value; NotifyOfPropertyChange(() => MKLevel); }
		}
		private IEnumerable<string> _tKLevel;

		public IEnumerable<string> TKLevel
		{
			get { return _tKLevel; }
			set { _tKLevel = value; NotifyOfPropertyChange(() => TKLevel); }
		}

		private IEnumerable<string> _lKLevel;
		public IEnumerable<string> LKLevel
		{
			get { return _lKLevel; }
			set { _lKLevel = value; NotifyOfPropertyChange(() => LKLevel); }
		}

		private IEnumerable<string> _aKLevel;

		public IEnumerable<string> AKLevel
		{
			get { return _aKLevel; }
			set { _aKLevel = value; NotifyOfPropertyChange(() => AKLevel); }
		}
		private IEnumerable<string> _doctorComboBox;

		public IEnumerable<string> DoctorComboBox
		{
			get { return _doctorComboBox; }
			set { _doctorComboBox = value; NotifyOfPropertyChange(() => DoctorComboBox); }
		}

		private IEnumerable<string> _echoMiokardComboBox;

		public IEnumerable<string> EchoMiokardComboBox
		{
			get { return _echoMiokardComboBox; }
			set { _echoMiokardComboBox = value; NotifyOfPropertyChange(() => EchoMiokardComboBox); }
		}

		private IEnumerable<string> _collComboBox;

		public IEnumerable<string> CollComboBox
		{
			get { return _collComboBox; }
			set { _collComboBox = value; NotifyOfPropertyChange(() => CollComboBox); }
		}

		private IEnumerable<AgeList> _ageComboBox;

		public IEnumerable<AgeList> AgeComboBox
		{
			get { return _ageComboBox; }
			set { _ageComboBox = value; NotifyOfPropertyChange(() => AgeComboBox); }
		}
		#endregion
		#region ComboBoxeSelectedIndex
		private int _apparatIndex;

		public int ApparatIndex
		{
			get { return _apparatIndex; }
			set { _apparatIndex = value; NotifyOfPropertyChange(() => ApparatIndex); }
		}
		private int _qualityIndex;

		public int QualityIndex
		{
			get { return _qualityIndex; }
			set { _qualityIndex = value; NotifyOfPropertyChange(() => QualityIndex); }
		}
		private int _eCGIndex;

		public int ECGIndex
		{
			get { return _eCGIndex; }
			set { _eCGIndex = value; NotifyOfPropertyChange(() => ECGIndex); }
		}

		private int _rightStomachIndex;

		public int RightStomachIndex
		{
			get { return _rightStomachIndex; }
			set { _rightStomachIndex = value; NotifyOfPropertyChange(() => RightStomachIndex); }
		}
		private int _miokardIndex;

		public int MiokardIndex
		{
			get { return _miokardIndex; }
			set { _miokardIndex = value; NotifyOfPropertyChange(() => MiokardIndex); }
		}
		private int _echoMiokardIndex;

		public int EchoMiokardIndex
		{
			get { return _echoMiokardIndex; }
			set { _echoMiokardIndex = value; NotifyOfPropertyChange(() => EchoMiokardIndex); }
		}
		private int _movementIndex;

		public int MovementIndex
		{
			get { return _movementIndex; }
			set { _movementIndex = value; NotifyOfPropertyChange(() => MovementIndex); }
		}
		private int _evaluateIndex;

		public int EvaluateIndex
		{
			get { return _evaluateIndex; }
			set { _evaluateIndex = value; NotifyOfPropertyChange(() => EvaluateIndex); }
		}
		private int _fVIndex;

		public int FVIndex
		{
			get { return _fVIndex; }
			set { _fVIndex = value; NotifyOfPropertyChange(() => FVIndex); }
		}
		private int _collIndex;

		public int CollIndex
		{
			get { return _collIndex; }
			set { _collIndex = value; NotifyOfPropertyChange(() => CollIndex); }
		}
		private int _iNSIndex;

		public int INSIndex
		{
			get { return _iNSIndex; }
			set { _iNSIndex = value; NotifyOfPropertyChange(() => INSIndex); }
		}
		private int _valveIndex;

		public int ValveIndex
		{
			get { return _valveIndex; }
			set { _valveIndex = value; NotifyOfPropertyChange(() => ValveIndex); }
		}

		private int _mKIndex;

		public int MKIndex
		{
			get { return _mKIndex; }
			set { _mKIndex = value; NotifyOfPropertyChange(() => MKIndex); }
		}
		private int _tKIndex;

		public int TKIndex
		{
			get { return _tKIndex; }
			set { _tKIndex = value; NotifyOfPropertyChange(() => TKIndex); }
		}

		private int _aKIndex;

		public int AKIndex
		{
			get { return _aKIndex; }
			set { _aKIndex = value; NotifyOfPropertyChange(() => AKIndex); }
		}
		private int _lKIndex;

		public int LKIndex
		{
			get { return _lKIndex; }
			set { _lKIndex = value; NotifyOfPropertyChange(() => LKIndex); }
		}


		#endregion
		#region Check
		private bool _rightFunctionChecked;

		public bool RightFunctionChecked
		{
			get { return _rightFunctionChecked; }
			set
			{
				_rightFunctionChecked = value;
				if (value)
					RightFunctionVisibility = Visibility.Visible;
				else
				{
					RightFunctionVisibility = Visibility.Collapsed;
				}
				NotifyOfPropertyChange(() => RightFunctionChecked);
			}
		}
		private bool _leftStomachChecked;

		public bool LeftStomachChecked
		{
			get { return _leftStomachChecked; }
			set
			{
				_leftStomachChecked = value;
				if (value)
					LeftStomachVisibility = Visibility.Visible;
				else
				{
					LeftStomachVisibility = Visibility.Collapsed;
				}
				NotifyOfPropertyChange(() => LeftStomachChecked);
			}
		}
		private bool _rightChecked;

		public bool RightChecked
		{
			get { return _rightChecked; }
			set
			{
				if (value)
					RightVisibility = Visibility.Visible;
				else
				{
					RightVisibility = Visibility.Collapsed;
				}
				NotifyOfPropertyChange(() => LeftStomachChecked);
				_rightChecked = value;
				NotifyOfPropertyChange(() => RightChecked);
			}
		}

		#endregion
		#region Visibilty
		private Visibility _rightFunctionVisibility = Visibility.Collapsed;

		public Visibility RightFunctionVisibility
		{
			get { return _rightFunctionVisibility; }
			set { _rightFunctionVisibility = value; NotifyOfPropertyChange(() => RightFunctionVisibility); }
		}

		private Visibility _leftStomachVisibility = Visibility.Collapsed;

		public Visibility LeftStomachVisibility
		{
			get { return _leftStomachVisibility; }
			set { _leftStomachVisibility = value; NotifyOfPropertyChange(() => LeftStomachVisibility); }
		}
		private Visibility _rightVisibility = Visibility.Collapsed;

		public Visibility RightVisibility
		{
			get { return _rightVisibility; }
			set { _rightVisibility = value; NotifyOfPropertyChange(() => RightVisibility); }
		}

		#endregion
		public MainViewModel(IEventAggregator aggregator, DBConn db =null)
		{
			_eventAggregator = aggregator;
			_eventAggregator.Subscribe(this);
			_db = db;	
			SegmentRepository.RefreshItmes();
			Thread[] th = new Thread[2];
			th[0] = new Thread(() => Initialize());
			th[0].Start();
			th[1]= new Thread(() => RefreshPolygon());
			th[1].Start();
			string date = Now.ToString().Replace(" ", "_").Replace(":", "_");
			ConstTableName.filePath = date;
			new Task(() => ClearFolder()).Start();
			AgeComboBox = AgeList.GetAgeList().ToList();
		}
		public string FIOtb { get; set; }
		public bool JenderMRb { get; set; }
		public bool JenderWRb { get; set; }
		private double _weigthtb;

		public double Weigthtb
		{
			get { return _weigthtb; }
			set
			{
				_weigthtb = value;
				NotifyOfPropertyChange(() => Weigthtb);
				CalculateBSA();

			}
		}

		private double _heighttb;

		public double Heighttb
		{
			get { return _heighttb; }
			set
			{
				_heighttb = value;
				NotifyOfPropertyChange(() => Heighttb);
				CalculateBSA();
			}
		}

		private double _bSAtb;

		public double BSAtb
		{
			get
			{
				return _bSAtb;
			}
			set
			{
				_bSAtb = value;
				NotifyOfPropertyChange(() => BSAtb);
				//Индекс КДР
				GetIndex(LastDiastolSizeLeftStomach, nameof(KDR));
				//Индекс Массы тела
				string v = GetWeightMiokard();
				GetIndex(v, nameof(MassaLJ));
				//Индекс Левого предсердия
				GetIndex(LeftAtrium, nameof(LP));
				//Индекс Восходящего отдела
				GetIndex(AriseDepartament, nameof(AO));
				//Индекс КДО
				GetIndex(RSFPLastVolumeLJ, nameof(KDO));
				//Индекс КСО
				GetIndex(RSFPKSO, nameof(KSO));
				//Индекс макс Объема левого предсердия
				GetIndex(RSFPMaxValomeLeftAtrium, nameof(VolumeLP));
				//Индекс макс Объема правого предсердия
				GetIndex(RSFPMaxValomeRightAtrium, nameof(VolumeLP));

			}
		}

		public string TodayDate { get => Now.ToString(); }

		#region Brushes
		public Brush Polygon1 { get; set; } = SegmentRepository.Polygon1;
		public Brush Polygon2 { get; set; } = SegmentRepository.Polygon2;
		public Brush Polygon3 { get; set; } = SegmentRepository.Polygon3;
		public Brush Polygon4 { get; set; } = SegmentRepository.Polygon4;
		public Brush Polygon5 { get; set; } = SegmentRepository.Polygon5;
		public Brush Polygon6 { get; set; } = SegmentRepository.Polygon6;
		public Brush Polygon7 { get; set; } = SegmentRepository.Polygon7;
		public Brush Polygon8 { get; set; } = SegmentRepository.Polygon8;
		public Brush Polygon9 { get; set; } = SegmentRepository.Polygon9;
		public Brush Polygon10 { get; set; } = SegmentRepository.Polygon10;
		public Brush Polygon11 { get; set; } = SegmentRepository.Polygon11;
		public Brush Polygon12 { get; set; } = SegmentRepository.Polygon12;
		public Brush Polygon13 { get; set; } = SegmentRepository.Polygon13;
		public Brush Polygon14 { get; set; } = SegmentRepository.Polygon14;
		public Brush Polygon15 { get; set; } = SegmentRepository.Polygon15;
		public Brush Polygon16 { get; set; } = SegmentRepository.Polygon16;
		public Brush Polygon17 { get; set; } = SegmentRepository.Polygon17;
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
		#region ALL_NOT_NEED_NOTIFICATION_TEXTBOXES
		public string BirthDay { get; set; }
		public string DirectDepartment { get; set; }
		public string ResearchName { get; set; }
		public string CardNumber { get; set; }
		public string ResearchAim { get; set; }
		private string _ECG1;

		public string ECG1
		{
			get
			{
				return _ECG1;
			}
			set
			{
				_ECG1 = value;
				CalculateMOK();
			}
		}

		public string ECG2 { get; set; }
		private string _leftAtrium;

		public string LeftAtrium
		{
			get
			{
				return _leftAtrium;
			}
			set
			{
				_leftAtrium = value;
				GetIndex(value, nameof(LP));
			}
		}

		public string RightStomach { get; set; }
		public string Circle { get; set; }
		public string SinusValsav { get; set; }
		public string SinobulyarContact { get; set; }
		private string _ariseDepartament;

		public string AriseDepartament
		{
			get { return _ariseDepartament; }
			set
			{
				_ariseDepartament = value;
				GetIndex(value, nameof(AO));
			}
		}

		public string Arc { get; set; }
		public string AortaDepartament { get; set; }
		public string ThicknessWallRightStomach { get; set; }
		private string _ThicknessMejPereMJP;

		public string ThicknessMejPereMJP
		{
			get { return _ThicknessMejPereMJP; }
			set
			{
				_ThicknessMejPereMJP = value;
				string v = GetWeightMiokard();
				GetIndex(v, nameof(MassaLJ));
			}
		}

		public string ThicknessBazalSegment { get; set; }
		public string TypeThicknessMiokard { get; set; }
		public string ThicknessMJPSislotu { get; set; }
		public string AmplitudeMovementMJP { get; set; }
		private string _ThicknessLowerWallLeftStomach;

		public string ThicknessLowerWallLeftStomach
		{
			get { return _ThicknessLowerWallLeftStomach; }
			set
			{
				_ThicknessLowerWallLeftStomach = value;
				CalculateRelative();
				string v = GetWeightMiokard();
				GetIndex(v, nameof(MassaLJ));
			}
		}

		public string ThicknessLowerWallLeftStomachSislotu { get; set; }
		public string AmplitudeMovementLowerWall { get; set; }
		private string _LastSislotSizeLeftStomach;
		//КСР
		public string LastSislotSizeLeftStomach
		{
			get { return _LastSislotSizeLeftStomach; }
			set
			{
				_LastSislotSizeLeftStomach = value;
				CalculateKSObyKSR();
				CalculateFA();

			}
		}

		private string _fractionAcceleration;

		public string FractionAcceleration
		{
			get { return _fractionAcceleration; }
			set { _fractionAcceleration = value; }
		}

		private string _relativeThicknessLeftStomach;

		public string RelativeThicknessLeftStomach
		{
			get { return _relativeThicknessLeftStomach; }
			set
			{
				_relativeThicknessLeftStomach = value;
			}
		}

		public string VelocityVavlvePulmonaryArtery { get; set; }
		public string MaxGradientPressurePulmonaryArtery { get; set; }
		private string _lSPickE;

		public string LSPickE
		{
			get { return _lSPickE; }
			set { _lSPickE = value; CalculateEA(_lSPickE, _lSPickA, nameof(LSEA)); }
		}

		private string _lSPickA;

		public string LSPickA
		{	
			get { return _lSPickA; }
			set { _lSPickA = value; CalculateEA(_lSPickE, _lSPickA, nameof(LSEA)); }
		}

		private string _lSEA;

		public string LSEA
		{
			get { return _lSEA; }
			set
			{
				_lSEA = value;
				NotifyOfPropertyChange(() => LSEA);
				//CalculateEA(LSPickE, LSPickA, LSEA);
			}
		}

		public string LSES { get; set; }
		public string LSEL { get; set; }
		public string LSEE { get; set; }
		public string LSDT { get; set; }
		public string LSIVRS { get; set; }
		public string LSS { get; set; }
		public string LSD { get; set; }
		public string LSSD { get; set; }
		public string LSZ { get; set; }
		public string LSMaxGradientMitralValve { get; set; }
		public string LSAverageGradientMitralValve { get; set; }
		private string _rSFPickE;

		public string RSFPickE
		{
			get { return _rSFPickE; }
			set { _rSFPickE = value; CalculateEA(_rSFPickE, _rSFPickA, nameof(RSFPickEA)); }
		}

		private string _rSFPickA;

		public string RSFPickA
		{
			get { return _rSFPickA; }
			set { _rSFPickA = value; CalculateEA(_rSFPickE, _rSFPickA, nameof(RSFPickEA)); }
		}

		private string _rSFPickEA;

		public string RSFPickEA
		{
			get { return _rSFPickEA; }
			set { _rSFPickEA = value; NotifyOfPropertyChange(() => RSFPickEA); }
		}

		public string RSFPickDT { get; set; }
		public string RSFPickEL { get; set; }
		public string RSFPickEEL { get; set; }
		public string RSFPMaxGradientTricuspidil { get; set; }
		public string RSFPAverageGradientPressureTrucuspidil { get; set; }
		public string RSFPVelocityLeftStomach { get; set; }
		public string RSFPMaxGradientLeftStomach { get; set; }
		public string RSFPVelocityAortValve { get; set; }
		public string RSFPMaxGradientAortValue { get; set; }
		public string RSFPAverageGradientAortValue { get; set; }
		private string _rSFPLastVolumeLJ;

		public string RSFPLastVolumeLJ
		{
			get
			{
				return _rSFPLastVolumeLJ;
			}
			set
			{
				_rSFPLastVolumeLJ = value;
				CalculateFV();
				CalculateUOK();
				GetIndex(value, nameof(KDO));
			}
		}

		private string _rSFPFV = string.Empty;

		public string RSFPFV
		{
			get
			{
				return _rSFPFV;
			}
			set
			{
				_rSFPFV = value;
			}
		}

		private string _rSFPKSO;

		public string RSFPKSO
		{
			get
			{
				return _rSFPKSO;
			}
			set
			{
				_rSFPKSO = value;
				CalculateFV();
				CalculateUOK();
				GetIndex(value, nameof(KSO));
			}
		}


		private string _rSFPUOK;

		public string RSFPUOK
		{
			get
			{
				return _rSFPUOK;
			}
			set
			{
				_rSFPUOK = value;
				CalculateMOK();
			}
		}

		private string _RSFPMOK;

		public string RSFPMOK
		{
			get
			{
				return _RSFPMOK;
			}
			set
			{
				_RSFPMOK = value;
			}
		}

		public string RSFPKSOGLS { get; set; }
		public string RSFPdpdt { get; set; }
		private string _RSFPMaxValomeLeftAtrium;

		public string RSFPMaxValomeLeftAtrium
		{
			get { return _RSFPMaxValomeLeftAtrium; }
			set
			{
				_RSFPMaxValomeLeftAtrium = value;
				GetIndex(value, nameof(VolumeLP));
			}
		}

		private string _rSFPMaxValomeRightAtrium;

		public string RSFPMaxValomeRightAtrium
		{
			get { return _rSFPMaxValomeRightAtrium; }
			set
			{
				_rSFPMaxValomeRightAtrium = value;
				GetIndex(value, nameof(VolumePP));
			}
		}

		public string RSFPLengthLJ { get; set; }
		public string RSFPWidthLJ { get; set; }
		public string RSFPSquareRigthAtrium { get; set; }
		public string RSFPLengthLp { get; set; }
		public string RSFPWidthLp { get; set; }
		public string RSFPLengthPj { get; set; }
		public string RSFPWidthPJ { get; set; }
		public string RSFPLengthPP { get; set; }
		public string RSFPWidthPP { get; set; }
		public string RABazal { get; set; }
		public string RAProksiVTPJ { get; set; }
		public string RAS { get; set; }
		public string RAMaxdPJ { get; set; }
		public string RADistalVTPJ { get; set; }
		public string RAFac { get; set; }
		public string RAActionFKTK { get; set; }
		public string RAWidthLa { get; set; }
		public string RAGLS { get; set; }
		public string RAWidth { get; set; }
		public string RARigthLA { get; set; }
		public string RAdpdt { get; set; }
		private string _RAMaxGradientTricuspidil;

		public string RAMaxGradientTricuspidil
		{
			get
			{
				return _RAMaxGradientTricuspidil;
			}
			set
			{
				_RAMaxGradientTricuspidil = value;
				SUMValue();
				WritePulnomoryValve();
			}
		}

		private string _RAPressurePP;

		public string RAPressurePP
		{
			get { return _RAPressurePP; }
			set
			{
				_RAPressurePP = value;
				SUMValue();
				WritePulnomoryValve();
			}
		}

		private string _RASistolPressureLA;

		public string RASistolPressureLA
		{
			get { return _RASistolPressureLA; }
			set
			{
				_RASistolPressureLA = value;
				WritePulnomoryValve();
			}
		}

		public string RAAverageDLA { get; set; }
		public string RADDLA { get; set; }
		public string RADZLK { get; set; }
		public string MVComment { get; set; } = ConstTableName.mitralDefault;
		public string MVMethod { get; set; }
		public string MVFlow { get; set; }
		public string MVPISA { get; set; }
		public string MVVenaContracta { get; set; }
		public string MVFK { get; set; }
		public string MVEROA { get; set; }
		public string AVComment { get; set; } = ConstTableName.aorthalDefault;
		public string AVMethod { get; set; }
		public string AVFlow { get; set; }
		public string AVPHT { get; set; }
		public string AVVenaContaracta { get; set; }
		public string TVComment { get; set; } = ConstTableName.tricuspidalDefault;
		public string TVVenaContracta { get; set; }
		public string TVPisa { get; set; }
		public string TVFk { get; set; }
		public string TVEroa { get; set; }
		public string TVRvol { get; set; }
		public string MKSideSizeFK { get; set; }
		public string MKIntercommissioningSizeFK { get; set; }
		public string MKLengthDiastol { get; set; }
		public string MKLengthSistol { get; set; }
		public string MKHeadZMPM { get; set; }
		public string MKDepth { get; set; }
		public string MKProt { get; set; }
		public string MKCSept { get; set; }
		public string MKSTening { get; set; }
		public string MKRvol { get; set; }
		public string MKRF { get; set; }
		public string MKFLGap { get; set; }
		public string KLAComment { get; set; }

		private string _KDR;
		public string KDR
		{
			get
			{
				return _KDR;
			}
			set
			{
				_KDR = value;
				NotifyOfPropertyChange(() => KDR);
			}
		}
		//ThicknessMejPereMJP

		private string _lP;

		public string LP
		{
			get
			{
				return _lP;
			}
			set
			{
				_lP = value; NotifyOfPropertyChange(() => LP);
			}
		}

		private string _aO;

		public string AO
		{
			get { return _aO; }
			set { _aO = value; NotifyOfPropertyChange(() => AO); }
		}

		private string _MassaLJ;

		public string MassaLJ
		{
			get
			{
				return _MassaLJ;
			}
			set
			{
				_MassaLJ = value;
				NotifyOfPropertyChange(() => MassaLJ);
			}
		}

		private string _kDO;

		public string KDO
		{
			get
			{
				return _kDO;
			}
			set
			{
				_kDO = value; NotifyOfPropertyChange(() => KDO);
			}
		}

		private string _kSO;

		public string KSO
		{
			get
			{
				return _kSO;
			}
			set
			{
				_kSO = value; NotifyOfPropertyChange(() => KSO);
			}
		}

		private string _volumeLP;

		public string VolumeLP
		{
			get { return _volumeLP; }
			set { _volumeLP = value; NotifyOfPropertyChange(() => VolumeLP); }
		}

		private string _volumePP;

		public string VolumePP
		{
			get { return _volumePP; }
			set { _volumePP = value; NotifyOfPropertyChange(() => VolumePP); }
		}

		public string STK { get; set; }
		public string NA { get; set; }
		public string INS { get; set; }
		private string _conclusion = ConstTableName.conclusion;

		public string Conclusion
		{
			get { return _conclusion; }
			set { _conclusion = value; NotifyOfPropertyChange(() => Conclusion); }
		}

		public string Recomendation { get; set; } = ConstTableName.recomendation;
		private string _LastDiastolSizeLeftStomach;

		//КДР
		public string LastDiastolSizeLeftStomach
		{
			get { return _LastDiastolSizeLeftStomach; }
			set
			{
				_LastDiastolSizeLeftStomach = value;
				CalculateKDObyKDR();
				CalculateFA();
				CalculateRelative();
				GetIndex(value, nameof(KDR));
				string v = GetWeightMiokard();
				GetIndex(v, nameof(MassaLJ));
			}
		}

		public string OpenAorthValve { get; set; }
		public string Commentary { get; set; }
		public string CommentarySegment { get; set; } = ConstTableName.photoComment;
		#endregion

		#region SelectedItemInComboBox
		public string SelectedDoctor { get; set; }
		public string SelectedApparat { get; set; }
		public string SelectedVisualizationQuality { get; set; }
		public string SelectedECG { get; set; }
		public string SelectedRigthStomach { get; set; }
		public string SelectedMiokard { get; set; }
		public string SelectedEchoMiokard { get; set; }
		public string SelectedMovement { get; set; }
		public string SelectedEvaluateIndex { get; set; }
		public string SelectedColl { get; set; }
		//
		private string _selectedValve;

		public string SelectedValve
		{
			get { return _selectedValve; }
			set { _selectedValve = value; NotifyOfPropertyChange(() => SelectedValve); WritePulnomoryValve(); }
		}

		public string SelectedINS { get; set; }
		public string SelectedFV { get; set; }
		public string SelectedMK { get; set; }
		public string SelectedAK { get; set; }
		public string SelectedTK { get; set; }
		public string SelectedLK { get; set; }

		public AgeList SelectedChildAge { get; set; }
		#endregion

		//Open setting window
		public void SettingButton()
		{
			var s = SelectedINS;
			window.ShowDialog(new SettingViewModel(_db));
			Thread thread = new Thread(Initialize);
			thread.Start();
		}

		public void ConnectToDB()
		{
			window.ShowDialog(new ConnectViewModel(_eventAggregator));
			Thread thread = new Thread(Initialize);
			thread.Start();
		}

		public void PhotoButton()
		{
			window.ShowDialog(new PhotoViewModel());
			new Task(() => RefreshPolygon()).Start();

		}
		private void RefreshPolygon()
		{
			Polygon1 = SegmentRepository.Polygon1;
			Polygon2 = SegmentRepository.Polygon2;
			Polygon3 = SegmentRepository.Polygon3;
			Polygon4 = SegmentRepository.Polygon4;
			Polygon5 = SegmentRepository.Polygon5;
			Polygon6 = SegmentRepository.Polygon6;
			Polygon7 = SegmentRepository.Polygon7;
			Polygon8 = SegmentRepository.Polygon8;
			Polygon9 = SegmentRepository.Polygon9;
			Polygon10 = SegmentRepository.Polygon10;
			Polygon11 = SegmentRepository.Polygon11;
			Polygon12 = SegmentRepository.Polygon12;
			Polygon13 = SegmentRepository.Polygon13;
			Polygon14 = SegmentRepository.Polygon14;
			Polygon15 = SegmentRepository.Polygon15;
			Polygon16 = SegmentRepository.Polygon16;
			Polygon17 = SegmentRepository.Polygon17;

			NotifyOfPropertyChange(() => Polygon1);
			NotifyOfPropertyChange(() => Polygon2);
			NotifyOfPropertyChange(() => Polygon3);
			NotifyOfPropertyChange(() => Polygon4);
			NotifyOfPropertyChange(() => Polygon5);
			NotifyOfPropertyChange(() => Polygon6);
			NotifyOfPropertyChange(() => Polygon7);
			NotifyOfPropertyChange(() => Polygon8);
			NotifyOfPropertyChange(() => Polygon9);
			NotifyOfPropertyChange(() => Polygon10);
			NotifyOfPropertyChange(() => Polygon11);
			NotifyOfPropertyChange(() => Polygon12);
			NotifyOfPropertyChange(() => Polygon13);
			NotifyOfPropertyChange(() => Polygon14);
			NotifyOfPropertyChange(() => Polygon15);
			NotifyOfPropertyChange(() => Polygon16);
			NotifyOfPropertyChange(() => Polygon17);

			PolygonText1 = SegmentRepository.PolygonText1;
			PolygonText2 = SegmentRepository.PolygonText2;
			PolygonText3 = SegmentRepository.PolygonText3;
			PolygonText4 = SegmentRepository.PolygonText4;
			PolygonText5 = SegmentRepository.PolygonText5;
			PolygonText6 = SegmentRepository.PolygonText6;
			PolygonText7 = SegmentRepository.PolygonText7;
			PolygonText8 = SegmentRepository.PolygonText8;
			PolygonText9 = SegmentRepository.PolygonText9;
			PolygonText10 = SegmentRepository.PolygonText10;
			PolygonText11 = SegmentRepository.PolygonText11;
			PolygonText12 = SegmentRepository.PolygonText12;
			PolygonText13 = SegmentRepository.PolygonText13;
			PolygonText14 = SegmentRepository.PolygonText14;
			PolygonText15 = SegmentRepository.PolygonText15;
			PolygonText16 = SegmentRepository.PolygonText16;
			PolygonText17 = SegmentRepository.PolygonText17;

			NotifyOfPropertyChange(() => PolygonText1);
			NotifyOfPropertyChange(() => PolygonText2);
			NotifyOfPropertyChange(() => PolygonText3);
			NotifyOfPropertyChange(() => PolygonText4);
			NotifyOfPropertyChange(() => PolygonText5);
			NotifyOfPropertyChange(() => PolygonText6);
			NotifyOfPropertyChange(() => PolygonText7);
			NotifyOfPropertyChange(() => PolygonText8);
			NotifyOfPropertyChange(() => PolygonText9);
			NotifyOfPropertyChange(() => PolygonText10);
			NotifyOfPropertyChange(() => PolygonText11);
			NotifyOfPropertyChange(() => PolygonText12);
			NotifyOfPropertyChange(() => PolygonText13);
			NotifyOfPropertyChange(() => PolygonText14);
			NotifyOfPropertyChange(() => PolygonText15);
			NotifyOfPropertyChange(() => PolygonText16);
			NotifyOfPropertyChange(() => PolygonText17);

		}
		private void Initialize()
		{
			if (_db == null) return;
			MKLevel = _db.GetColumNameByString(ConstTableName.degree);
			LKLevel = _db.GetColumNameByString(ConstTableName.degree);
			TKLevel = _db.GetColumNameByString(ConstTableName.degree);
			AKLevel = _db.GetColumNameByString(ConstTableName.degree);
			ApparatComboBox = _db.GetColumNameByString(ConstTableName.apparat);
			VisualizationQualityComboBox = _db.GetColumNameByString(ConstTableName.quality);
			DoctorComboBox = _db.GetColumNameByString(ConstTableName.doctor);
			ECGComboBox = _db.GetColumNameByString(ConstTableName.ecg);
			RigthStomachComboBox = _db.GetColumNameByString(ConstTableName.rigthstomach);
			MovementComboBox = _db.GetColumNameByString(ConstTableName.movement);
			EvaluateIndexComboBox = _db.GetColumNameByString(ConstTableName.evaluate);
			EchoMiokardComboBox = _db.GetColumNameByString(ConstTableName.echo);
			MiokardComboBox = _db.GetColumNameByString(ConstTableName.type);
			FVComboBox = _db.GetColumNameByString(ConstTableName.fv);
			CollComboBox = _db.GetColumNameByString(ConstTableName.coll);
			INSComboBox = _db.GetColumNameByString(ConstTableName.ins);
			ValveComboBox = _db.GetColumNameByString(ConstTableName.valve);

			ApparatIndex = -1;
			QualityIndex = -1;
			FVIndex = -1;
			EvaluateIndex = -1;
			EchoMiokardIndex = -1;
			INSIndex = -1;
			MiokardIndex = -1;
			RightStomachIndex = -1;
			MovementIndex = -1;
			CollIndex = -1;
			ECGIndex = -1;
			ValveIndex = -1;
			MKIndex = -1;
			TKIndex = -1;
			LKIndex = -1;
			AKIndex = -1;
		}

		public void CreateWordButton()
		{
			InitializePatient();
			view = new WordCreator(patient,_db);
	    	

			new Thread(() => view.CreateDocument()).Start();
		}

		public void SavePatientButton()
		{
			if (view == null) {

				view = new WordCreator(null, null);
			}
			InitializePatient(false);
			view.InsertData(_db, patient);
			MessageBox.Show("Пациент добавлен");
		}

		public void PatientListButton()
		{
			window.ShowWindow(new PatientListViewModel(_db, _eventAggregator));
		}
		public void Handle(DBConn message)
		{
			_db = message;
		}

		public void CreateNewWindowButton()
		{
			window.ShowWindow(new MainViewModel(_eventAggregator,_db));
		}

		private void ClearFolder()
		{
			if (!Directory.Exists(path)) return;

			DirectoryInfo directory = new DirectoryInfo(path);
			FileInfo[] files = directory.GetFiles();
			if (files.Count() < 500) return;
			try
			{
				foreach (var file in files)
				{
					File.Delete(file.FullName);
				}
			}
			catch (Exception)
			{
				return;
			}
			
		}
		private void InitializePatient(bool rewrite = true)
		{
			string jender = JenderMRb == true ? "М" : "Ж";
			if (!LeftStomachChecked)
				SetNullLeftStomach();
			if (!RightFunctionChecked)
				SetNullRightFunctionStomach();
			if (!RightChecked)
				SetNullRightStomach();
			WriteConclusion(rewrite);
			ConstTableName.parametrs["FVRSF"] = "ФВ ЛЖ по " + SelectedFV;
			ConstTableName.parametrs["TKDR"] = "Трикуспидальная регургитация " + SelectedTK + " степени";
			ConstTableName.parametrs["AKDR"] = "Аортальная регургитация " + SelectedAK + " степени";
			ConstTableName.parametrs["MKDR"] = "Митральная регургитация " + SelectedMK + " степени";
			ConstTableName.parametrs["LKDR"] = "Легочная регургитация " + SelectedLK + " степени";

			ConstTableName.parametrs["RightStomachComment"] = "Передне-задний размер " + SelectedRigthStomach;
			ConstTableName.parametrs["INS"] = "Индекс сократимости миокарда ЛЖ(ИНС) " + SelectedINS;

			ConstTableName.parametrs["FirstTextBox"] = "ЭКГ ритм " + SelectedECG;
			ConstTableName.parametrs["SecondTextBox"] = "ЭКГ ритм " + SelectedECG;

			ConstTableName.parametrs["SelectedTypeThicknessMiocard"] = "Тип утолщение миокарда " + SelectedMiokard;

			patient = new PatientMV
			{
				Patient = new Patient
				{
					FIO = FIOtb,
					Jender = jender,
					Weigth = Weigthtb,
					Height = Heighttb,
					DirectDepartment = DirectDepartment,
					ResearchName = ResearchName,
					CardNumber = CardNumber,
					ResearchDateTime = DateTime.Parse(Now.ToString()),
					DoctorName = SelectedDoctor,
					ResearchAim = ResearchAim,
					Conclusion = Conclusion,
					BirthDay = BirthDay,
					SelectedApparat = SelectedApparat,
					SelectedQualityVizuality = SelectedVisualizationQuality,
					Recomendation = Recomendation,
					Commentary = Commentary,
					CommentarySegment =CommentarySegment,
					ChildAgeCode = SelectedChildAge != null ? SelectedChildAge.Code : -1
				},
				RightStomachMain = new RightStomachMain
				{
					SelectedRightStomach = SelectedRigthStomach,
					RightStomachComment = RightStomach,
					ThicknessWallRightStomach = ThicknessWallRightStomach
				},
				ECG = new ECG
				{
					Rhythm = SelectedECG,
					FirstTextBox = ECG1,
					SecondTextBox = ECG2,
				},
				Aorta = new Aorta
				{
					Arc = Arc,
					Sinus = SinusValsav,
					Contact = SinobulyarContact,
					Arise = AriseDepartament,
					Circle = Circle,
					Departament = AortaDepartament,
					OpenAorthValve = OpenAorthValve

				},
				LeftStomachFunction = new LeftStomachFunction
				{
					PickE = LSPickE,
					PickA = LSPickA,
					EA = LSEA,
					ES = LSES,
					El = LSEL,
					EE = LSEE,
					DT = LSDT,
					IVRS = LSIVRS,
					S = LSS,
					D = LSD,
					SD = LSSD,
					Z = LSZ,
					MaxGradientMitralValve = LSMaxGradientMitralValve,
					AverageGradientMitralValve = LSAverageGradientMitralValve
				},
				RightStomachFunction = new RightStomachFunction
				{
					PickE = RSFPickE,
					PickA = RSFPickA,
					EA = RSFPickEA,
					DT = RSFPickDT,
					EL = RSFPickEL,
					EEL = RSFPickEEL,
					MaxGradientTricuspidil = RSFPMaxGradientTricuspidil,
					AverageGradientPressureTrucuspidil = RSFPAverageGradientPressureTrucuspidil,
					VelocityLeftStomach = RSFPVelocityLeftStomach,
					MaxGradientLeftStomach = RSFPMaxGradientLeftStomach,
					VelocityAortValve = RSFPVelocityAortValve,
					MaxGradientAortValue = RSFPMaxGradientAortValue,
					AverageGradientAortValue = RSFPAverageGradientAortValue,
					LastVolumeLJ = RSFPLastVolumeLJ,
					FV = RSFPFV,
					KSO = RSFPKSO,
					UOK = RSFPUOK,
					MOK = RSFPMOK,
					GLS = RSFPKSOGLS,
					DPDT = RSFPdpdt,
					SelectedFv = SelectedFV
				},
				RightStomachFunctionAddition = new RightStomachFunctionAddition
				{
					MaxValomeLeftAtrium = RSFPMaxValomeLeftAtrium,
					MaxValomeRightAtrium = RSFPMaxValomeRightAtrium,
					LengthLJ = RSFPLengthLJ,
					WidthLJ = RSFPWidthLJ,
					SquareRigthAtrium = RSFPSquareRigthAtrium,
					LengthLP = RSFPLengthLp,
					WidthLP = RSFPWidthLp,
					LengthPJ = RSFPLengthPj,
					WidthPJ = RSFPWidthPJ,
					LengthPP = RSFPLengthPP,
					WidthPP = RSFPWidthPP,
				},
				RightStomach = new RightStomach
				{
					BazalPj = RABazal,
					ProksiVTPJ = RAProksiVTPJ,
					S = RAS,
					MaxdPJ = RAMaxdPJ,
					DistalVTPJ = RADistalVTPJ,
					Fac = RAFac,
					ActionFKTK = RAActionFKTK,
					WidthLa = RAWidthLa,
					GLS = RAGLS,
					Width = RAWidth,
					RigthLA = RARigthLA,
					Dpdt = RAdpdt,
					SelectedColl = SelectedColl,
					MaxGradientTricuspidil = RAMaxGradientTricuspidil,
					AverageDLA = RAAverageDLA,
					PressurePP = RAPressurePP,
					DDLA = RADDLA,
					SistolPressureLA = RASistolPressureLA,
					DZLK = RADZLK
				},
				PatienValue = new PatienValue
				{
					ThicknessMejPereMJP = ThicknessMejPereMJP,
					ThicknessSegment = ThicknessBazalSegment,
					SelectedTypeThicknessMiocard = SelectedMiokard,
					TypeComment = TypeThicknessMiokard,
					ExoMiocard = SelectedEchoMiokard,
					ThicknessMJPSislotu = ThicknessMJPSislotu,
					AmplitudeMovementMJP = AmplitudeMovementMJP,
					ThicknessLowerWallLeftStomach = ThicknessLowerWallLeftStomach,
					ThicknessLowerWallLeftStomachSislotu = ThicknessLowerWallLeftStomachSislotu,
					AmplitudeMovementLowerWall = AmplitudeMovementLowerWall,
					LastSislotSizeLeftStomach = LastSislotSizeLeftStomach,
					FractionAcceleration = FractionAcceleration,
					RelativeThicknessLeftStomach = RelativeThicknessLeftStomach,
					MovementMJP = SelectedMovement,
					VelocityVavlvePulmonaryArtery = VelocityVavlvePulmonaryArtery,
					MaxGradientPressurePulmonaryArtery = MaxGradientPressurePulmonaryArtery,
					SovleIndexWeight = SelectedEvaluateIndex,
					INS = INS,
					SelectedINS = SelectedINS,
					LastDiastolSizeLeftStomach = LastDiastolSizeLeftStomach,
					LeftAtrium = LeftAtrium,
				},
				MKMR = new MKMR
				{
					CSept = MKCSept,
					IntercommissioningSizeFK = MKIntercommissioningSizeFK,
					LengthDiastol = MKLengthDiastol,
					LengthSistol = MKLengthSistol,
					HeadZMPM = MKHeadZMPM,
					Depth = MKDepth,
					Prot = MKProt,
					SideSizeFK = MKSideSizeFK,
					STening = MKSTening,
					FLGap = MKFLGap,
					RF = MKRF,
					Rvol = MKRvol,
				},
				TricuspidialValve = new TricuspidialValve
				{
					Comment = TVComment,
					EROA = TVEroa,
					VenaContracta = TVVenaContracta,
					FK = TVFk,
					PISA = TVPisa,
					RVOL = TVRvol
				},
				MitralValve = new MitralValve
				{
					Comment = MVComment,
					Method = MVMethod,
					Flow = MVFlow,
					PISA = MVPISA,
					VenaContracta = MVVenaContracta,
					FK = MVFK,
					EROA = MVEROA
				},
				AorticValve = new AorticValve
				{
					Comment = AVComment,
					Method = AVMethod,
					Flow = AVFlow,
					PHT = AVPHT,
					VenaContrcata = AVVenaContaracta
				},

				DegreeReguliration = new DegreeReguliration
				{
					AK = SelectedAK,
					LK = SelectedLK,
					MK = SelectedMK,
					TK = SelectedTK
				},
				PulmonaryValve = new PulmonaryValve
				{
					Comment = KLAComment,
					SelectedText = SelectedValve
				},
				Segment = ConstTableName.segment
			};
		}
		private void SetNullLeftStomach()
		{
			LSPickA = null;
			LSEA = null;
			LSES = null;
			LSEL = null;
			LSEE = null;
			LSDT = null;
			LSIVRS = null;
			LSS = null;
			LSD = null;
			LSSD = null;
			LSZ = null;
		}
		private void SetNullRightFunctionStomach()
		{
			RSFPickA = null;
			RSFPickEA = null;
			RSFPickDT = null;
			RSFPickEL = null;
			RSFPickEEL = null;

		}
		private void SetNullRightStomach()
		{
			RABazal = null;
			RAProksiVTPJ = null;
			RAS = null;
			RAMaxdPJ = null;
			RADistalVTPJ = null;
			RAFac = null;
			RAActionFKTK = null;
			RAWidthLa = null;
			RAGLS = null;
			RAWidth = null;
			RARigthLA = null;
			RAdpdt = null;
			SelectedColl = null;
		}
		private void WritePulnomoryValve()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(SelectedValve);
			if (!string.IsNullOrWhiteSpace(RAMaxGradientTricuspidil))
			{
				sb.Append(", ");
				sb.Append("МГД ТР ");
				sb.Append(RAMaxGradientTricuspidil);
				sb.Append(" мм рт. ст.");
			}
			if (!string.IsNullOrWhiteSpace(RAPressurePP))
			{
				sb.Append(", ");
				sb.Append("давление ПП ");
				sb.Append(RAPressurePP);
				sb.Append(" мм рт. ст.");
			}
			if (!string.IsNullOrWhiteSpace(RASistolPressureLA))
			{
				sb.Append(", ");
				sb.Append("СДЛА ");
				sb.Append(RASistolPressureLA);
				sb.Append(" (N <36 мм рт. ст.)");
			}
			KLAComment = sb.ToString();
			NotifyOfPropertyChange(() => KLAComment);

		}
		public void WriteConclusion(bool rewrite=true)
		{
			if (!rewrite) return;

			StringBuilder sb = new StringBuilder(Conclusion);
			if (!string.IsNullOrWhiteSpace(ECG1))
			{
				sb.Append("ЧСС ");
				sb.Append(ECG1);
				sb.Append(" Уд в мин,");
			}
			if (!string.IsNullOrWhiteSpace(SelectedECG))
			{
				sb.Append("Ритм ");
				if (SelectedECG.Contains("с ЧСС"))
				{
					sb.Append(SelectedECG.Replace("с ЧСС", ""));
				}
				else
				{
					sb.Append(SelectedECG);
				}
				sb.Append(",");
			}

			if (!string.IsNullOrWhiteSpace(RSFPFV))
			{
				sb.Append("ФВ ");
				sb.Append(RSFPFV);
				sb.Append("%");
			}
			Conclusion = sb.ToString();
		}
		#region Calculate Region
		private void CalculateFV()
		{

			if (string.IsNullOrWhiteSpace(RSFPLastVolumeLJ) || string.IsNullOrWhiteSpace(RSFPKSO))
			{
				RSFPFV = "";
				NotifyOfPropertyChange(() => RSFPFV);
				return;
			}

			if (double.TryParse(RSFPLastVolumeLJ.Replace(".", ","), out double kdo) && double.TryParse(RSFPKSO.Replace(".", ","), out double kso))
			{
				if (kdo == 0) return;
				RSFPFV = (Math.Round(((kdo - kso) / kdo) * 100, 1)).ToString();
				NotifyOfPropertyChange(() => RSFPFV);
				return;
			}
			RSFPFV = "";
			NotifyOfPropertyChange(() => RSFPFV);
			return;
		}
		private string GetWeightMiokard()
		{
			if (string.IsNullOrWhiteSpace(ThicknessMejPereMJP)
				|| string.IsNullOrWhiteSpace(LastDiastolSizeLeftStomach)
				|| string.IsNullOrWhiteSpace(ThicknessLowerWallLeftStomach))
			{
				return "";
			}
			if (double.TryParse(ThicknessMejPereMJP.Replace(".", ","), out double tm) &&
				double.TryParse(LastDiastolSizeLeftStomach.Replace(".", ","), out double kd) &&
				double.TryParse(ThicknessLowerWallLeftStomach.Replace(".", ","), out double tz))
				return Math.Round((0.8 * (1.04 * Math.Pow(tm + kd + tz, 3) - Math.Pow(kd, 3)) + 0.6), 2).ToString();
			return "";
		}
		private void SUMValue()
		{

			if (string.IsNullOrWhiteSpace(RAPressurePP)
				|| string.IsNullOrWhiteSpace(RAMaxGradientTricuspidil))
			{
				RASistolPressureLA = "";
				NotifyOfPropertyChange(() => RASistolPressureLA);

				return;
			}
			if (double.TryParse(RAPressurePP.Replace(".", ","), out double tm) &&
			   double.TryParse(RAMaxGradientTricuspidil.Replace(".", ","), out double kd))
			{
				double temp = tm + kd;
				RASistolPressureLA = temp.ToString();
				NotifyOfPropertyChange(() => RASistolPressureLA);
				return;
			}
			RASistolPressureLA = "";
			NotifyOfPropertyChange(() => RASistolPressureLA);
			return;

		}
		private void CalculateUOK()
		{
			if (string.IsNullOrWhiteSpace(RSFPKSO)
				|| string.IsNullOrWhiteSpace(RSFPLastVolumeLJ))
			{
				RSFPUOK = "";
				NotifyOfPropertyChange(() => RSFPUOK);
				return;
			}
			if (double.TryParse(RSFPKSO.Replace(".", ","), out double kso) &&
			  double.TryParse(RSFPLastVolumeLJ.Replace(".", ","), out double kdo))
			{
				double temp = kdo - kso;
				RSFPUOK = temp.ToString();
				NotifyOfPropertyChange(() => RSFPUOK);
				return;
			}
			RSFPUOK = "";
			NotifyOfPropertyChange(() => RSFPUOK);
			return;
		}
		private void CalculateRelative()
		{

			if (string.IsNullOrWhiteSpace(ThicknessLowerWallLeftStomach)
			|| string.IsNullOrWhiteSpace(LastDiastolSizeLeftStomach))
			{
				RelativeThicknessLeftStomach = "";
				NotifyOfPropertyChange(() => RelativeThicknessLeftStomach);
				return;
			}
			if (double.TryParse(ThicknessLowerWallLeftStomach.Replace(".", ","), out double a) &&
			  double.TryParse(LastDiastolSizeLeftStomach.Replace(".", ","), out double b))
			{
				double temp = Math.Round((a * 2) / b, 2);
				RelativeThicknessLeftStomach = temp.ToString();
				NotifyOfPropertyChange(() => RelativeThicknessLeftStomach);
				return;
			}
			RelativeThicknessLeftStomach = "";
			NotifyOfPropertyChange(() => RelativeThicknessLeftStomach);
			return;
		}
		private void GetIndex(string value, string name)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				this.GetType().GetProperty(name).SetValue(this, "", null);
				NotifyOfPropertyChange(() => name);
				return;
			}
			if (double.TryParse(value.Replace(".", ","), out double t))
			{
				double result = Math.Round(t / BSAtb, 2);
				this.GetType().GetProperty(name).SetValue(this, result.ToString(), null);
				NotifyOfPropertyChange(() => name);
				return;
			}
			this.GetType().GetProperty(name).SetValue(this, "", null);
			NotifyOfPropertyChange(() => name);
			return;
		}
		private void CalculateFA()
		{
			if (string.IsNullOrWhiteSpace(LastDiastolSizeLeftStomach)
			|| string.IsNullOrWhiteSpace(LastSislotSizeLeftStomach))
			{
				FractionAcceleration = "";
				NotifyOfPropertyChange(() => FractionAcceleration);
				return;
			}
			if (double.TryParse(LastDiastolSizeLeftStomach.Replace(".", ","), out double dis) &&
			  double.TryParse(LastSislotSizeLeftStomach.Replace(".", ","), out double sist))
			{
				double temp = Math.Round(((dis - sist) / dis) * 100, 1);
				FractionAcceleration = temp.ToString();
				NotifyOfPropertyChange(() => FractionAcceleration);
				return;
			}

			FractionAcceleration = "";
			NotifyOfPropertyChange(() => FractionAcceleration);
			return;
		}
		private void CalculateMOK()
		{
			if (string.IsNullOrWhiteSpace(ECG1)
			|| string.IsNullOrWhiteSpace(RSFPUOK))
			{
				RSFPMOK = "";
				NotifyOfPropertyChange(() => RSFPMOK);

				return;
			}
			if (double.TryParse(ECG1.Replace(".", ","), out double hr) &&
			  double.TryParse(RSFPUOK.Replace(".", ","), out double sv))
			{
				double temp = Math.Round((sv * hr) / 1000, 1);
				RSFPMOK = temp.ToString();
				NotifyOfPropertyChange(() => RSFPMOK);
				return;
			}
			RSFPMOK = "";
			NotifyOfPropertyChange(() => RSFPMOK);
			return;
		}
		private void CalculateKSObyKSR()
		{
			if (string.IsNullOrWhiteSpace(LastSislotSizeLeftStomach))
			{
				RSFPKSO = "";
				NotifyOfPropertyChange(() => RSFPKSO);
				return;

			}
			if (double.TryParse(LastSislotSizeLeftStomach.Replace(".", ","), out double hr))
			{
				double temp = Math.Round((7 * Math.Pow(hr, 3)) / (2.4 + hr), 1);
				RSFPKSO = temp.ToString();
				NotifyOfPropertyChange(() => RSFPKSO);
				return;

			}
			RSFPLastVolumeLJ = "";
			NotifyOfPropertyChange(() => RSFPLastVolumeLJ);
			return;
		}
		private void CalculateKDObyKDR()
		{
			if (string.IsNullOrWhiteSpace(LastDiastolSizeLeftStomach))
			{
				RSFPLastVolumeLJ = "";
				NotifyOfPropertyChange(() => RSFPLastVolumeLJ);
				return;

			}
			if (double.TryParse(LastDiastolSizeLeftStomach.Replace(".", ","), out double hr))
			{
				double temp = Math.Round((7 * Math.Pow(hr, 3)) / (2.4 + hr), 1);
				RSFPLastVolumeLJ = temp.ToString();
				NotifyOfPropertyChange(() => RSFPLastVolumeLJ);
				return;

			}
			RSFPLastVolumeLJ = "";
			NotifyOfPropertyChange(() => RSFPLastVolumeLJ);
			return;

		}
		private void CalculateBSA()
		{
			BSAtb = Math.Round(Math.Sqrt(Heighttb * Weigthtb) / 60.0, 3);
			NotifyOfPropertyChange(() => BSAtb);
		}

		private void CalculateEA(string pickE,string pickA,string name)
		{
			if (string.IsNullOrWhiteSpace(pickE) || string.IsNullOrWhiteSpace(pickA))
			{
			this.GetType().GetProperty(name).SetValue(this, "", null);
				NotifyOfPropertyChange(() => name);
				return;
			}
			if (double.TryParse(pickE.Replace(".", ","), out double t)&& double.TryParse(pickA.Replace(".", ","), out double hr))
			{
				double result = Math.Round(t / hr, 1);
				this.GetType().GetProperty(name).SetValue(this, result.ToString(), null);
				NotifyOfPropertyChange(() => name);
				return;
			}
			this.GetType().GetProperty(name).SetValue(this, "", null);
			NotifyOfPropertyChange(() => name);
			return;
		}
		#endregion
	}
}
