using EchoCardiologyDS.Models.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace EchoCardiologyDS.Models
{
    public class PatientMV
    {
        public  Patient Patient { get; set; }
        public  RightStomachMain RightStomachMain { get; set; }
        public  PatienValue PatienValue { get; set; }
        public  ECG ECG { get; set; }
        public  Aorta Aorta { get; set; }
        public  LeftStomachFunction LeftStomachFunction { get; set; }
        public  RightStomachFunction RightStomachFunction { get; set; }
        public  RightStomachFunctionAddition RightStomachFunctionAddition { get; set; }
        public  RightStomach RightStomach { get; set; }
        public  DegreeReguliration DegreeReguliration { get; set; }
        public  MitralValve MitralValve { get; set; }
        public  PulmonaryValve PulmonaryValve { get; set; }
        public  MKMR MKMR { get; set; }
        public  AorticValve AorticValve { get; set; }
        public  TricuspidialValve TricuspidialValve { get; set; }
       // public  IndexValve Index { get; set; }
		public  LeftStomachMV LeftStomachMV { get; set; }
		public  Dopper Dopper { get; set; }
		public  LightValve LightValve { get; set; }
		public  DiametrNpv DiametrNpv { get; set; }
		public  KDOMV KDOMV { get; set; }
		public  LeftAtrium LeftAtrium { get; set; }
		public  Segment Segment { get; set; }
		public  RightAtrium RightAtrium { get; set; }
		public  CommentaryMV CommentaryMV { get; set; }
	}

	public static class PatientMVExtension
	{

		public static void GetNotNullField<T>(this T model,string key,ref IDictionary<string,TypeString> dict) where T:class
		{if (model == null) return;
			foreach (var prop in typeof(T).GetProperties())
			{
				try
				{
					var value = prop.GetValue(model, null).ToString();
                    if (prop.Name == "Id"|| prop.Name == "PatientId") continue;
					if (!string.IsNullOrWhiteSpace(value))
					{
						dict.Add(prop.Name+key, new TypeString { TypeName = typeof(T).Name, Message = value });
					}
				}
				catch (NullReferenceException)
				{
					continue;
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		public static IDictionary<string, TypeString> GetNotNullField<T>(this T model, string key) where T : class
		{
			IDictionary<string, TypeString> dict = new Dictionary<string, TypeString>();
			if (model == null) return dict;
			foreach (var prop in typeof(T).GetProperties())
			{
				try
				{
					var value = prop.GetValue(model, null).ToString();
					if (prop.Name == "Id" || prop.Name == "PatientId") continue;
					if (!string.IsNullOrWhiteSpace(value))
					{
						dict.Add(prop.Name + key, new TypeString { TypeName = typeof(T).Name, Message = value });
					}
				}
				catch (NullReferenceException)
				{
					continue;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
			return dict;
		}

	}
}
