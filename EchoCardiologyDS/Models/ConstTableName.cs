using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EchoCardiologyDS.Models
{
	public static class ConstTableName
	{
		public static Segment segment = new Segment();
		public static void RefreshSegment()
		{
			segment = new Segment();
		}
		public static string filePath = "";
		public static string apparat = "Аппарат";
		public static string ins = "ИНС";
		public static string fv = "Фракция Выброса";
		public static string doctor = "Доктора";
		public static string quality = "Качество визуализации";
		public static string ecg = "Экг ритм";
		public static string rigthstomach = "Правый желудочек";
		public static string thickness = "Толщина нижне-боковой стенки левого желудочка";
		public static string type = "Тип утолщения миокарда";
		public static string echo = "Эхогенность миокарда";
		public static string movement = "Движение МЖП";
		public static string evaluate = "Расчет индекса массы МЖ";
		public static string coll = "Коллабирование";
		public static string valve = "Клапан Легочной артерии";
		public static string degree = "Степень регургитации";

		public static string fvSelected = "Симпсону";



		public static IDictionary<string, string> parametrs = new Dictionary<string, string>
		{
			["UOK"] = "УО",
			["DiametrNpv"] = "Диаметр НПВ",
			["LightValve"] = "Легочная артерия",
			["RightStomach"] = "Оценка правого желудочка",
			["RightStomachFunctionAddition"] = "Размеры",
			["RightStomachFunction"] = "Оценка диастолической функции правого желудочка",
			["LeftStomachFunction"] = "Оценка диастолической функции левого желудочка",
			["PatienValue"] = "",
			["RightStomachMain"] = "Правый желудочек",
			["Aorta"] = "Аорта",
			["LeftStomachMV"] = "Левый желудочек",
			["RightAtrium"] = "Правое предсердие",
			["RightAtriumVolume"] = "Объем",
			["PulmonaryValve"] = "Клапан легочной артерии: ",
			["MKMR"] = "MKMR: ",
			["TricuspidialValve"] = "Трикуспидальный клапан: ",
			["AorticValve"] = "Аортальный клапан: ",
			["Volume"] = "Объем",
			["LP"] = "Передне-задний размер",
			["WidthNPV"] = "Ширина НПВ",
			["CollNPV"] = "Коллабирование НПВ",
			["MaxGradientTricuspidilLV"] = "МГД ТР",
			["PressurePPLV"] = "Давление в ПП",
			["SistolPressureLALV"] = "СДЛА",
			["TricValve"] = "Трикуспидальный клапан",
			["Aortha"] = "Аорта",
			["LeaveTract"] = "Выходной тракт левого желудочка",
			["ValveLight"] = "Клапан легочной артерии",
			["MitralValve"] = "Митральный клапан: ",
			["MKDR"] = "Митральная регургитация",
			["TKDR"] = "Трикуспидальная регургитация",
			["AKDR"] = "Аортальная регургитация",
			["LKDR"] = "Легочная регургитация",
			["OpenAorthValve"] = "Раскрытие аортального клапана",
			["LastDiastolSizeLeftStomach"] = "Диастолический размер полости ЛЖ (КДР)",
			["SelectedApparat"] = "Аппарат",
			["SelectedQualityVizuality"] = "Качество визуализации",
			["LeftAtrium"] = "Левое предсердие",
			["SelectedRightStomach"] = "Правый желудочек",
			["RightStomachComment"] = "передне-задний размер ",
			["ThicknessLeftStomach"] = "Толщина нижне-боковой стенки левого желудочка",
			["RelativeThicknessLeftStomach"] = "Относительная толщина стенки ЛЖ",
			["Circle"] = "Фиброзное кольцо",
			["Sinus"] = "Синусы Вальсальвы",
			["Contact"] = "Синотубулярный контакт",
			["Arise"] = "Восходящий отдел",
			["Arc"] = "Дуга",
			["Departament"] = "Нисходящий отдел",
			["PickELS"] = "Пик Е",
			["PickALS"] = "Пик А",
			["EALS"] = "E/A",
			["ESLS"] = "e`s",
			["ElLS"] = "e'l",
			["EELS"] = "E/e'",
			["DTLS"] = "DT",
			["IVRSLS"] = "IVRS",
			["SLS"] = "S",
			["DLS"] = "D",
			["SDLS"] = "S/D",
			["ZLS"] = "Z",
			["MaxGradientMitralValveLS"] = "Максимальный градиент на митральном клапане",
			["AverageGradientMitralValveLS"] = "Средний градиент на митральном клапане",
			["PickERSF"] = "Пик Е",
			["PickARSF"] = "Пик А",
			["EARSF"] = "E/A",
			["DTRSF"] = "DT",
			["ELRSF"] = "e'l",
			["EELRSF"] = "E/e'l",
			["MaxGradientTricuspidilRSF"] = "Максимальный градиент на трикуспидальном клапане",
			["AverageGradientPressureTrucuspidilRSF"] = "Средний градиент давления на трикуспидальном клапане",
			["VelocityLeftStomachRSF"] = "Скорость в выходном тракте левого желудочка",
			["MaxGradientLeftStomachRSF"] = "Максимальный градиент в выходном тракте левого желудочка",
			["VelocityAortValveRSF"] = "Скорость на аортальном клапане",
			["MaxGradientAortValueRSF"] = "Максимальный градиент на аортальном клапане",
			["AverageGradientAortValueRSF"] = "Средний градиент на аортальном клапане",
			["LastVolumeLJRSF"] = "КДО ЛЖ",
			["LastVolumeLJ"] = "КДО ЛЖ",
			["FVRSF"] = "ФВ ЛЖ по " + fvSelected,
			["SelectedFvRSF"] = "ФВ",
			["KSORSF"] = "КСО",
			["UOKRSF"] = "УОК",
			["MOKRSF"] = "MOK",
			["GLSRSF"] = "GLS",
			["DPDTRSF"] = "dp/dt",
			["MaxValomeLeftAtriumRSF"] = "Максимальный объем левого предсердия(ЛП)",
			["MaxVolumeRightAtriumVolume"] = "Передне-задний размер",
			["LengthLJRSF"] = "Размер ЛЖ : длина",
			["WidthLJRSF"] = "Размер ЛЖ : ширина",
			["LengthLPRSF"] = "Размер ЛП : длина",
			["WidthLPRSF"] = "Размер ЛП : ширина",
			["LengthPJRSF"] = "Размер ПЖ : длина",
			["WidthPJRSF"] = "Размер ПЖ : ширина",
			["LengthPPRSF"] = "Размер ПП : длина",
			["WidthPPRSF"] = "Размер ПП : ширина",
			["SquareRigthAtriumRSF"] = "Площадь правого предсердия",
			["BazalPjRS"] = "Базальный d ПЖ",
			["ProksiVTPJRS"] = "Проксимальный d ВТПЖ",
			["SRS"] = "S`",
			["MaxdPJRS"] = "Максимильный d ПЖ",
			["DistalVTPJRS"] = "Дистальный d ВТПЖ",
			["FacRS"] = "Fac",
			["ActionFKTKRS"] = "Движ. ФК ТК (TAPSE)",
			["WidthLaRS"] = "Ширина ствола ЛА",
			["GLSRS"] = "GLS",
			["WidthRS"] = "Ширина НПВ",
			["RigthLARS"] = "Правая ветвь ЛА",
			["DpdtRS"] = "dp/dt",
			["SelectedCollRS"] = "Коллабирование",
			["AverageDLARS"] = "Ср. ДЛА",
			["PressurePPRS"] = "Давление в ПП",
			["DDLARS"] = "ДДЛА",
			["SistolPressureLARS"] = "Систолическое давление в ЛА",
			["DZLKRS"] = "ДЗЛК",
			["MaxGradientTricuspidilRS"] = "Макс градиент трикуспидальной регургитации",
			["ThicknessWallRightStomach"] = "Толщина стенки правого желудочка в диастолу",
			["ThicknessPreStomach"] = "Толщина межжелудочковой перегородки(МЖП)",
			["ThicknessSegment"] = "Толщина базального сегмента МЖП",
			["SelectedTypeThicknessMiocard"] = "Тип утолщения миокарда",
			["TypeComment"] = "Тип утолщения миокарда",
			["ExoMiocard"] = "Эхогенность миокарда",
			["ThicknessMJPSislotu"] = "Толщина МЖП в систолу",
			["ThicknessMejPereMJP"] = "Толщина МЖП в диастолу",
			["AmplitudeMovementMJP"] = "Амплитуда движения МЖП",
			["ThicknessLowerWallLeftStomach"] = "Толщина нижне-боковой стенки левого желудочка в диастолу",
			["ThicknessLowerWallLeftStomachSislotu"] = "Толщина нижне-боковой стенки левого желудочка в систолу",
			["AmplitudeMovementLowerWall"] = "Амплитуда движения нижне-боковой стенки ЛЖ",
			["LastSislotSizeLeftStomach"] = "Систолический размер полости ЛЖ (КСР)",
			["FractionAcceleration"] = "Фракция ускорения (ФУ)",
			["VelocityVavlvePulmonaryArtery"] = "Скорость на клапане легочной (ЛК) артерии",
			["MaxGradientPressurePulmonaryArtery"] = "Максимальный градиент давления на клапане легоной артерии",
			["SovleIndexWeight"] = "Расчет индекса массы МЖ",
			["MovementMJP"] = "Движение МЖП",
			["INS"] = "ИНС",
			["SelectedINS"] = "ИНС",
			["KDR"] = "КДР/ППТ",
			["LPP"] = "ЛП/ППТ",
			["AO"] = "Ао/ППТ",
			["WeigthLJ"] = "Масса ЛЖ/ППТ",
			["KDO"] = "КДО/ППТ",
			["KSO"] = "КСО/ППТ",
			["VolumeLP"] = "Объем ЛП/ППТ",
			["VolumePP"] = "Объем ПП/ППТ",
			["STK"] = "СТК/ППТ",
			["NA"] = "НА/ППТ",
			["CommentMV"] = "Митральный клапан(МК)",
			["PISAMV"] = "PISA",
			["VenaContractaMV"] = "VenaContracta",
			["MethodMV"] = "Планим. методом",
			["FlowMV"] = "По транс-му потоку",
			["FKMV"] = "ФК",
			["EROAMV"] = "EROA",
			["SelectedTextPV"] = "Клапан Легочной артерии",
			["CommentPV"] = "Клапан Легочной артерии",
			["SideSizeFKMKMR"] = "Стептально боковой размер ФК",
			["IntercommissioningSizeFKMKMR"] = "Межкомиссуральный размер ФК",
			["LengthDiastolMKMR"] = "Межпапиллярное расст. в диастолу",
			["LengthSistolMKMR"] = "Межпапиллярное расст. в систолу",
			["HeadZMPMMKMR"] = "Расстояние от головки ЗМПМ до ФК",
			["DepthMKMR"] = "Глуб.",
			["ProtMKMR"] = "Прот.",
			["CSeptMKMR"] = "C-sept",
			["STeningMKMR"] = "S тенинга",
			["RvolMKMR"] = "Rvol",
			["RFMKMR"] = "RF",
			["FLGapMKMR"] = "Fl gap",
			["CommentAV"] = "Аортальный клапан(АК)",
			["MethodAV"] = "Планим. методом",
			["FlowAV"] = "по ур-ию непр. пот.",
			["PHTAV"] = "PHT регургитации на АК",
			["VenaContrcataAV"] = "Vena contaracta",
			["CommentTV"] = "Трикуспидальный клапан(ТК)",
			["PISATV"] = "PISA",
			["VenaContractaTV"] = "Vena contracta",
			["RVOLTV"] = "RVOL",
			["FKTV"] = "FK",
			["EROATV"] = "EROA",
			["Rhythm"] = "Экг ритм",
			["ECG"] = "Экг ритм",
			["FirstTextBox"] = "Экг ритм",
			["SecondTextBox"] = "Экг ритм",
			["WeightMokardLj"] = "Масса миокарда ЛЖ"

		};

		public static bool IsBold(string norma,string value)
		{
			double val;
			if (!double.TryParse(value.Replace(".",","), out val)) return false;
			if (string.IsNullOrWhiteSpace(norma)) return false;
			string[] splitedNorma;
			if (norma.Contains("-"))
			{
				splitedNorma = norma.Split('-');
				if (norma.Contains("ППТ"))
				{
					splitedNorma[0] = "49";
					splitedNorma[1] = "115";
				}
				if (double.TryParse(splitedNorma[0].Replace(".", ",").GetStringDigit(), out double f)&&double.TryParse(splitedNorma[1].Replace(".", ",").GetStringDigit(), out double s))
				{
					if (val > f && val < s) return false;
				}
				return true;
			}
			else
			{
				if (!double.TryParse(norma.GetStringDigit(), out double result))
					return false;
				if (norma.Contains("="))
				{


					if (norma.Contains("<"))
					{
						if (result <= val)
							return true;
						return false;
					}
					else
					{
						if (val >= result)
							return false;
						return true;
					}						
					
				}
				else
				{
					if (norma.Contains("<"))
					{
						if (val < result)
							return true;
						return false;
					}
					else
					{
						if (result > val)
							return false;
						return true;
					}

				}
			}
		}

		public static string GetStringDigit(this string str)
		{
			var result = str.SkipWhile(c => !char.IsDigit(c)).TakeWhile(c => !char.IsLetter(c)).ToList();
			if(!Char.IsDigit(result[result.Count - 1]))
			result.RemoveAt(result.Count - 1);
			return new string (result.ToArray());
		}

		public static string mitralDefault = "утолщения, деформации, пролапса створок, вегитации, спайки комиссур нет";
		public static string aorthalDefault = "не изменен, систолическое раскрытие створок 1,9 (1,6-2,6)";
		public static string tricuspidalDefault = "не изменен";

		public static string conclusion = "нарушение локальной сократимости левого желудочка : гипокинез нижней и нижне-боковой " +
			"cтенок базальных,медиальных и верхушечных сегментов. камеры сердца расширены,гипертрофия стенок не отмечается." +
			"наличие выпота в перикарде : полностью окружает сердце с толщиной \nсепарации 0,3-0,4см (150-200 мл)\n" +
			"клапаны интактны\nперегородки без дефектов.\nдопплер:митральная регургитация 1ст,легочная гипертензия\n";

		public static string recomendation = "1) Консультация и лечение у кардиолога\n2) Контроль через 6 месяцев";

		public static string photoComment = "нарушение локальной сократимости левого желудочка гипокинез нижней и нежне-боковой стенки\n" +
			"базального,медиального и верхушечного сегментов\n" +
			"наличие выпота в перикарде : полностью окружает сердце с толщиной сепарации 0,3-0,4см (150-200мл) " +
			"в доступных визуализации полостях сердца наличие тромбов не выявлено";
	}


}
