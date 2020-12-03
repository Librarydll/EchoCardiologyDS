using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EchoCardiologyDS.Models
{
	public class DefaultIndex
	{
		public IDictionary<string, string> indexes = new Dictionary<string, string>();

		public IDictionary<string, string> mgd = new Dictionary<string, string>();

		public IDictionary<string, string> sgd = new Dictionary<string, string>();

		public string ThicknessMejPereMJP { get; set; }
		public string LastDiastolSizeLeftStomach { get; set; }
		public string ThicknessLowerWallLeftStomach { get; set; }
		public string WeightMokardLj { get; set; }
		public double Height { get; set; }
		public double Weight { get; set; }
		public string Arise { get; set; }
		public string KDO { get; set; }
		public string LP { get; set; }
		public string Volume { get; set; }
		public string RightAtriumVolume { get; set; }

		public string MaxGradientTricuspidil { get; set; }
		public string MaxGradientAortValue { get; set; }
		public string MaxGradientMitralValve { get; set; }
		public string MaxGradientPressurePulmonaryArtery { get; set; }
		public string MaxGradientLeftStomach { get; set; }

		public string AverageGradientPressureTrucuspidil { get; set; }
		public string AverageGradientAortValue { get; set; }
		public string AverageGradientMitralValve { get; set; }


		public void Initialize()
		{
			indexes.Add("Mass", GetWeightMiokard());
			
			indexes.Add(nameof(LastDiastolSizeLeftStomach), GetIndex());
			indexes.Add(nameof(WeightMokardLj), GetWeightIndex());
			indexes.Add(nameof(Arise), GetIndex(Arise));
			indexes.Add(nameof(KDO), GetIndex(KDO));
			indexes.Add(nameof(LP), GetIndex(LP));
			indexes.Add(nameof(Volume), GetIndex(Volume));
			indexes.Add(nameof(RightAtriumVolume), GetIndex(RightAtriumVolume));
			indexes.Add("LastVolumeLJ", GetIndex(KDO));

			mgd["MitralValve"] = MaxGradientMitralValve;
			mgd["TricValve"] = MaxGradientTricuspidil;
			mgd["LeaveTract"] = MaxGradientLeftStomach;
			mgd["Aortha"] = MaxGradientAortValue;
			mgd["ValveLight"] = MaxGradientPressurePulmonaryArtery;

			sgd["MitralValve"] = AverageGradientMitralValve;
			sgd["TricValve"] = AverageGradientPressureTrucuspidil;
			sgd["LeaveTract"] = "";
			sgd["Aortha"] = AverageGradientAortValue;
			sgd["ValveLight"] = "";
		}
		public string GetWeightMiokard()
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

		public string GetIndex()
		{
			if (!indexes.ContainsKey("Mass"))
				return "";
			double temp = (Math.Exp(0.425 * Math.Log(Weight) + 0.725 * Math.Log(Height * 100.0) + Math.Log(71.84)));
			if (double.TryParse(indexes["Mass"], out double t) && temp != 0)
			{
				double result = Math.Round((t / temp) * 10000, 2);
				return result.ToString();

			}
			return "";
		}
	
		public string GetWeightIndex()
		{
			if (!indexes.ContainsKey("Mass"))
				return "";

			if (double.TryParse(indexes["Mass"], out double t))
			{
				double bsa = GetBSA();
				double result = Math.Round((t / bsa), 2);
				return result.ToString();

			}
			return "";

		}

		public string GetIndex(string field)
		{
			if (string.IsNullOrWhiteSpace(field)) return "";
			if (double.TryParse(field.Replace(".", ","), out double t))
			{
				double result = Math.Round(t / GetBSA(), 2);
				return result.ToString();
			}
			return "";
		}
	
		public double GetBSA()
		{
			double bsa = Math.Round(Math.Sqrt(Height * Weight) / 60.0, 4);
			return bsa;
		}

	}
}
