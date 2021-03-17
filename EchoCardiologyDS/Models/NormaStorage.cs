using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EchoCardiologyDS.Models
{
   public 	class NormaStorage
	{
		public IDictionary<string, string> Norma = null;
		public IDictionary<string, string> NormaIndex = null;
		public IDictionary<string, string> normaMale = new Dictionary<string, string>
		{
			["PickELS"] = "60-130 см/cек",
			["PickALS"] = "45-73 см/cек",
			["EALS"] = "0,9-1,4 cм/сек",
			["ESLS"] = ">7 cм/сек",
			["ElLS"] = ">10 см/cек",
			["EELS"] = "<14",
			["ELRSF"] = ">10 см/cек",
			["EARSF"] = "0,9-1,4 cм/сек",
			["PickARSF"] = "25-41 cм/сек",
			["PickERSF"] = "30-70 cм/сек",
			["LP"] = "3,0-4,0",
			["Volume"] = "18-58",
			["LastVolumeLJ"] = "67-155",
			["WeightMokardLj"] = "88-224г",
			["RightStomachComment"] = "<2,9",
			["CollNPV"] = ">50%",
			["MaxGradientTricuspidilLV"] = "<31 мм рт. ст.",
			["PressurePPLV"] = "<=5 мм рт. ст.",
			["SistolPressureLALV"] = "<36 мм рт. ст.",
			["LastDiastolSizeLeftStomach"] = "4,2-5,9",
			["LastSislotSizeLeftStomach"] = "2,1-4,0",
			["ThicknessMJPSislotu"] = "0,6-1,1",
			["RelativeThicknessLeftStomach"] = "<=0,42",
			["ThicknessMejPereMJP"] = "0,6-1,1",
			["LastVolumeLJRSF"] = "67-155",
			["ThicknessLowerWallLeftStomach"] = "0,6-1,1",
			["FVRSF"] = ">=52%",
			["Circle"] = "1,8-2,6",
			["OpenAorthValve"] = "1,6-2,6",
			["Sinus"] = "2,4-4,0",
			["MaxGradientTricuspidilRS"] = "<31 мм рт. ст.",
			["Arise"] = "2,5-3,8",
			["PressurePPRS"] = "<=5 мм рт. ст.",
			["SistolPressureLARS"] = "<36 мм рт. ст.",
			["SelectedCollRS"] = ">50%"
		};
		public IDictionary<string, string> normaFemale = new Dictionary<string, string>
		{
			["PickELS"] = "60-130 см/cек",
			["PickALS"] = "45-73 см/cек",
			["EALS"] = "0,9-1,4 cм/сек",
			["ESLS"] = ">7 cм/сек",
			["ElLS"] = ">10 см/cек",
			["EELS"] = "<14",
			["ELRSF"] = ">10 см/cек",
			["EARSF"] = "0,9-1,7 cм/сек",
			["PickARSF"] = "25-41 cм/сек",
			["PickERSF"] = "30-70 cм/сек",
			["LP"] = "2,7-3,8",
			["Volume"] = "32-52",
			["LastVolumeLJ"] = "56-104",
			["WeightMokardLj"] = "67-162г",
			["RightStomachComment"] = "<2,9",
			["CollNPV"] = ">50%",
			["MaxGradientTricuspidilLV"] = "<31 мм рт. ст.",
			["PressurePPLV"] = "<=5 мм рт. ст.",
			["SistolPressureLALV"] = "<36 мм рт. ст.",
			["LastDiastolSizeLeftStomach"] = "3,9-5,3",
			["LastSislotSizeLeftStomach"] = "2,1-4,0",
			["ThicknessMJPSislotu"] = "0,6-1,0",
			["RelativeThicknessLeftStomach"] = "<=0,42",
			["ThicknessMejPereMJP"] = "0,6-1,0",
			["LastVolumeLJRSF"] = "56-104",
			["ThicknessLowerWallLeftStomach"] = "0,6-1,0",
			["FVRSF"] = ">=52%",
			["Circle"] = "1,8-2,6",
			["OpenAorthValve"] = "1,6-2,6",
			["Sinus"] = "2,4-4,0",
			["MaxGradientTricuspidilRS"] = "<31 мм рт. ст.",
			["Arise"] = "2,5-3,8",
			["PressurePPRS"] = "<=5 мм рт. ст.",
			["SistolPressureLARS"] = "<36 мм рт. ст.",
			["SelectedCollRS"] = ">50%"
		};

		public IDictionary<string, string> normaIndexMale = new Dictionary<string, string>
		{
			["LP"] = "1,5-2,3",
			["Volume"] = "<=34",
			["LastVolumeLJ"] = "35-75",
			["LastDiastolSizeLeftStomach"] = "2,4-3,2",
			["LastVolumeLJRSF"] = "35-75",
			["Arise"] = "<=2.1",
			["WeightMokardLj"] = "по ППТ 49-115",
			["RightAtriumVolume"] = ""

		};
		public IDictionary<string, string> normaIndexFemale = new Dictionary<string, string>
		{
			["LP"] = "1,5-2,3",
			["Volume"] = "<=34",
			["LastVolumeLJ"] = "<=62",
			["LastDiastolSizeLeftStomach"] = "2,2-3,1",
			["LastVolumeLJRSF"] = "35-75",
			["Arise"] = "<=2.1",
			["WeightMokardLj"] = "по ППТ 43-95",
			["RightAtriumVolume"] = ""

		};
		public IDictionary<string, string> normaDopper = new Dictionary<string, string>
		{
			["TricValve"] = "30-70",
			["Aortha"] = "<=200",
			["LeaveTract"] = "70-110",
			["ValveLight"] = "60-110",
			["MitralValve"] = "60-130",

		};

		private Gender _gender;
		public NormaStorage(Gender gender)
		{
			_gender = gender;
			Initialize();
		}

		private void Initialize()
		{
			switch (_gender)
			{
				case Gender.Male:
					Norma = normaMale;
					NormaIndex = normaIndexMale;
					break;
				case Gender.Female:
					Norma = normaFemale;
					NormaIndex = normaIndexFemale;
					break;
				default:
					break;
			}
		}

	}
	public enum Gender
	{
		Male,
		Female
	}
}
