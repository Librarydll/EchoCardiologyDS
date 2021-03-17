using EchoCardiologyDS.Models;
using EchoCardiologyDS.Models.PatientAction;
using EchoCardiologyDS.PfdImplementation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xceed.Words.NET;
using Xceed.Document.NET;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using EchoCardiologyDS.Models.ModelView;
using Image = Xceed.Document.NET.Image;

namespace EchoCardiologyDS.WordImplementation
{
	public class WordCreator : IView
	{
		#region Global v
		IPatientRepository repository;
		DBConn dB = null;
		private DefaultIndex _defaultIndex;
		private PatientMV _patient;
		private string _patientName;
		private readonly string _savePdfDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Отчеты";
		private  string _pathToBinFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
		DocX _document;
		IDictionary<string, TypeString> cellData = new Dictionary<string, TypeString>();
		int paragraphSize = 10;
		int cellParagraphSize = 9;
		string date = string.Empty;
		IList<Table> tables = new List<Table>();
		NormaStorage normaStorage = null;
		#endregion
		public WordCreator(PatientMV pp, DBConn conn)
		{
			if (pp == null|| conn == null) return;
			dB = conn;
			_patient = pp;
			_defaultIndex = new DefaultIndex
			{
				Height = _patient.Patient.Height,
				Weight = _patient.Patient.Weigth,
				LastDiastolSizeLeftStomach = _patient.PatienValue?.LastDiastolSizeLeftStomach,
				ThicknessLowerWallLeftStomach = _patient.PatienValue?.ThicknessLowerWallLeftStomach,
				Arise = _patient.Aorta?.Arise,
				//LastVolumeLJRSF = _patient.RightStomachFunction?.LastVolumeLJ,
				ThicknessMejPereMJP = _patient.PatienValue?.ThicknessMejPereMJP,
				KDO = _patient.RightStomachFunction?.LastVolumeLJ,
				LP = _patient.PatienValue?.LeftAtrium,
				Volume = _patient.RightStomachFunctionAddition?.MaxValomeLeftAtrium,
				AverageGradientAortValue = _patient.RightStomachFunction?.AverageGradientAortValue,
				AverageGradientPressureTrucuspidil = _patient.RightStomachFunction?.AverageGradientPressureTrucuspidil,
				AverageGradientMitralValve = _patient.LeftStomachFunction?.AverageGradientMitralValve,
				MaxGradientAortValue = _patient.RightStomachFunction?.MaxGradientAortValue,
				MaxGradientTricuspidil = _patient.RightStomachFunction?.MaxGradientTricuspidil,
				MaxGradientLeftStomach = _patient.RightStomachFunction?.MaxGradientLeftStomach,
				MaxGradientMitralValve = _patient.LeftStomachFunction?.MaxGradientMitralValve,
				MaxGradientPressurePulmonaryArtery = _patient.PatienValue?.MaxGradientPressurePulmonaryArtery,
				RightAtriumVolume = _patient.RightStomachFunctionAddition?.MaxValomeRightAtrium
			};
			_patient.LeftStomachMV = new LeftStomachMV
			{
				LastDiastolSizeLeftStomach = _patient.PatienValue?.LastDiastolSizeLeftStomach,
				LastSislotSizeLeftStomach = _patient.PatienValue?.LastSislotSizeLeftStomach,
				RelativeThicknessLeftStomach = _patient.PatienValue?.RelativeThicknessLeftStomach,
				ThicknessLowerWallLeftStomach = _patient.PatienValue?.ThicknessLowerWallLeftStomach,
				ThicknessMejPereMJP = _patient.PatienValue?.ThicknessMejPereMJP,
				ThicknessSegment = _patient.PatienValue?.ThicknessSegment,
				MovementMJP = _patient.PatienValue?.MovementMJP,
				FractionAcceleration = _patient.PatienValue?.FractionAcceleration,
				FVRSF = _patient.RightStomachFunction?.FV,
				LastVolumeLJ = _patient.RightStomachFunction?.LastVolumeLJ,
				INS = _patient.PatienValue?.INS,
				UOK = _patient.RightStomachFunction?.UOK
			};
			_patient.Dopper = new Dopper
			{
				Aortha = _patient.RightStomachFunction?.VelocityAortValve,
				LeaveTract = _patient.RightStomachFunction?.VelocityLeftStomach,
				MitralValve = _patient.LeftStomachFunction?.PickE,
				TricValve = _patient.RightStomachFunction?.PickE,
				ValveLight = _patient.PatienValue?.VelocityVavlvePulmonaryArtery
			};
			_patient.LightValve = new LightValve
			{
				MaxGradientTricuspidil = _patient.RightStomach?.MaxGradientTricuspidil,
				PressurePP = _patient.RightStomach?.PressurePP,
				SistolPressureLA = _patient.RightStomach?.SistolPressureLA
			};
			_patient.DiametrNpv = new DiametrNpv
			{
				CollNPV = _patient.RightStomach?.SelectedColl,
				WidthNPV = _patient.RightStomach?.Width
			};
			
			_patient.LeftAtrium = new LeftAtrium
			{
				LP = _patient.PatienValue?.LeftAtrium,
				Volume = _patient.RightStomachFunctionAddition?.MaxValomeLeftAtrium
			};
			_patient.RightAtrium = new RightAtrium
			{
				RightAtriumVolume = _patient.RightStomachFunctionAddition?.MaxValomeRightAtrium
			};
			_patient.CommentaryMV = new CommentaryMV
			{
				Commentary = _patient.Patient?.Commentary
			};
			_defaultIndex.Initialize();
			_patient.LeftStomachMV.WeightMokardLj = _defaultIndex.indexes["Mass"];
			date = _patient.Patient.ResearchDateTime.ToString().Replace(" ", "_").Replace(":", "_");
			_patientName = string.IsNullOrWhiteSpace(_patient.Patient.FIO) ? date : $"{_patient.Patient.FIO}_{date}";
			var gender = _patient.Patient.Jender == "Ж" ? Gender.Female : Gender.Male;
			normaStorage =new NormaStorage(gender);
			CreateFolder();
		}
		public void InsertData(DBConn conn,PatientMV p)
		{
			if (conn == null||p==null) return;
			repository = new PatientRepository(conn);
			repository.Initialize(p);
		}
		public void CreateOrOpenDocument()
		{
			if (_patient == null || dB == null) return;
			try
			{
				if (File.Exists(_savePdfDocumentsPath + $@"\{_patientName}.docx"))
				{
					using (_document = DocX.Load(_savePdfDocumentsPath + $@"\{_patientName}.docx"))
					{

					}
				}
				else
				{
					using (_document = DocX.Create(_savePdfDocumentsPath + $@"\{_patientName}.docx"))
					{
						WriteMainParagraphs();
						_document.Save();
					}
				}

				System.Diagnostics.Process.Start(_savePdfDocumentsPath + $@"\{_patientName}.docx");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}
		public void CreateDocument()
		{
			if (_patient == null) return;
			try
			{
				using (_document = DocX.Create(_savePdfDocumentsPath + $@"\{_patientName}.docx"))
				{
					WriteMainParagraphs();
					_document.Save();
				}

				System.Diagnostics.Process.Start(_savePdfDocumentsPath + $@"\{_patientName}.docx");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}
		public void WriteMainParagraphs()
		{
			 _document.MarginTop=25;
			  repository = new PatientRepository(dB);
			Hospital hospital = repository.Hospital.FirstOrDefault();
			Image image;
			Picture picture;
			Table headerTable = _document.InsertTable(2, 3);
			var path = _pathToBinFolder + @"\Icon.png";
			headerTable.MergeCellsInColumn(1, 0, 1);
			if (File.Exists(path.Substring(6)))
			{
				image = _document.AddImage(path.Substring(6));
				picture = image.CreatePicture();
				headerTable.Rows[0].Cells[1].Paragraphs[0].InsertPicture(picture);
			}
			headerTable.SetColumnWidth(0, 3300);
			headerTable.SetColumnWidth(1, 3300);
			headerTable.SetColumnWidth(2, 3300);
			headerTable.Rows[0].Cells[0].Paragraphs[0].Append("Тел: ").Bold().FontSize(11).SpacingAfter(2).Append(hospital?.Phone).FontSize(paragraphSize);
			headerTable.Rows[1].Cells[0].Paragraphs[0].Append("Адрес: ").Bold().FontSize(11).SpacingAfter(2).Append(hospital?.Address).FontSize(paragraphSize);
			headerTable.Rows[0].Cells[0].Paragraphs[0].Alignment = Alignment.center;
			headerTable.Rows[1].Cells[0].Paragraphs[0].Alignment = Alignment.center;

			headerTable.Rows[0].Cells[2].Paragraphs[0].Append("Tel: ").Bold().FontSize(11).SpacingAfter(2).Append(hospital?.Phone).FontSize(paragraphSize);
			headerTable.Rows[1].Cells[2].Paragraphs[0].Append("Manzil: ").Bold().FontSize(11).SpacingAfter(2).Append(hospital?.AddressUz).FontSize(paragraphSize);
			headerTable.Rows[0].Cells[2].Paragraphs[0].Alignment = Alignment.center;
			headerTable.Rows[1].Cells[2].Paragraphs[0].Alignment = Alignment.center;

			ClearBorder(headerTable);
			_document.InsertParagraph().FontSize(9);
			Table mainTable = _document.InsertTable(6, 2);
			string jender = _patient.Patient.Jender == "Ж" ? "Женский" : "Мужской";
			mainTable.SetColumnWidth(0, 4000);
			mainTable.SetColumnWidth(1, 5900);
			mainTable.Rows[0].Cells[0].Paragraphs[0].Append("Ф.И.О пациента").Bold().FontSize(paragraphSize);
			mainTable.Rows[0].Cells[1].Paragraphs[0].Append(_patient.Patient.FIO?.ToUpper() + " " + _patient.Patient.CardNumber).Bold().FontSize(paragraphSize).UnderlineStyle(UnderlineStyle.singleLine);

			mainTable.Rows[1].Cells[0].Paragraphs[0].Append("Дата рождения").Bold().FontSize(paragraphSize);
			mainTable.Rows[1].Cells[1].Paragraphs[0].Append(_patient.Patient.BirthDay).FontSize(paragraphSize);

			mainTable.Rows[2].Cells[0].Paragraphs[0].Append("Пол").Bold().FontSize(paragraphSize);
			mainTable.Rows[2].Cells[1].Paragraphs[0].Append(jender).FontSize(paragraphSize);

			mainTable.Rows[3].Cells[0].Paragraphs[0].Append("Ф.И.О врача").Bold().FontSize(paragraphSize);
			mainTable.Rows[3].Cells[1].Paragraphs[0].Append(_patient.Patient.DoctorName).FontSize(paragraphSize);

			mainTable.Rows[4].Cells[0].Paragraphs[0].Append("Дата обследования").Bold().FontSize(paragraphSize);
			mainTable.Rows[4].Cells[1].Paragraphs[0].Append(_patient.Patient.ResearchDateTime.Value.ToString()).FontSize(paragraphSize);

			mainTable.Rows[5].Cells[0].Paragraphs[0].Append("Аппарат").Bold().FontSize(paragraphSize);
			mainTable.Rows[5].Cells[1].Paragraphs[0].Append(_patient.Patient.SelectedApparat).FontSize(paragraphSize);
			_document.InsertParagraph().FontSize(9);
			var p = _document.InsertParagraph("Эхокардиография").Bold().UnderlineStyle(UnderlineStyle.singleLine).FontSize(15).SpacingAfter(20);
			p.Alignment = Alignment.center;
			Table table = _document.InsertTable(2,2);
			table.SetColumnWidth(0, 4950); table.SetColumnWidth(1, 4950);
			table.Rows[0].Cells[0].Paragraphs[0].Append($"ЭКГ ритм: {_patient.ECG.Rhythm} {_patient.ECG.FirstTextBox} {_patient.ECG.SecondTextBox} в мин").FontSize(cellParagraphSize).SpacingAfter(2);
			table.Rows[1].Cells[0].Paragraphs[0].Append($"Рост: {_patient.Patient.Height} см вес: {_patient.Patient.Weigth} кг").FontSize(cellParagraphSize).SpacingAfter(2);
			table.Rows[1].Cells[1].Paragraphs[0].Append($"качество визуализации: {_patient.Patient.SelectedQualityVizuality}").FontSize(cellParagraphSize).SpacingAfter(2);
			table.Rows[0].Cells[1].Paragraphs[0].Alignment = Alignment.right;
			table.Rows[1].Cells[1].Paragraphs[0].Alignment = Alignment.right;
			ClearBorder(table);

			_document.InsertParagraph().FontSize(2);
			WriteTable();
			AddImage();
			_document.InsertParagraph().FontSize(9);
			p = _document.InsertParagraph($"ЗАКЛЮЧЕНИЕ:").FontSize(cellParagraphSize).SpacingAfter(6).Bold();
			p = _document.InsertParagraph(_patient.Patient.Conclusion?.ToUpper()).FontSize(cellParagraphSize).SpacingAfter(1).Bold();
			_document.InsertParagraph().FontSize(9);

			p = _document.InsertParagraph($"РЕКОМЕНДАЦИИ:").FontSize(cellParagraphSize).SpacingAfter(6).Bold();
			p = _document.InsertParagraph(_patient.Patient.Recomendation).FontSize(cellParagraphSize).SpacingAfter(1);
			_document.InsertParagraph().FontSize(9);
			p = _document.InsertParagraph($"Подпись_____________________________/{_patient.Patient.DoctorName}").FontSize(cellParagraphSize).SpacingBefore(5);

		}
		private void ClearBorder(Table table)
		{
			table.SetBorder(TableBorderType.Top, new Border(BorderStyle.Tcbs_none, BorderSize.two, 0, Color.Black));
			table.SetBorder(TableBorderType.Right, new Border(BorderStyle.Tcbs_none, BorderSize.two, 0, Color.Black));
			table.SetBorder(TableBorderType.Bottom, new Border(BorderStyle.Tcbs_none, BorderSize.two, 0, Color.Black));
			table.SetBorder(TableBorderType.Left, new Border(BorderStyle.Tcbs_none, BorderSize.two, 0, Color.Black));
			table.SetBorder(TableBorderType.InsideH, new Border(BorderStyle.Tcbs_none, BorderSize.two, 0, Color.Black));
			table.SetBorder(TableBorderType.InsideV, new Border(BorderStyle.Tcbs_none, BorderSize.two, 0, Color.Black));
		}	
		private void WriteTable()
		{
			SetNull();
			Table table = _document.InsertTable(1, 5);
			table.Rows[0].Cells[0].Paragraphs[0].Append("Название параметра").FontSize(11).Bold();
			table.Rows[0].Cells[1].Paragraphs[0].Append("Значение").FontSize(11).Bold();
			table.Rows[0].Cells[2].Paragraphs[0].Append("Норма (см)").FontSize(11).Bold();
			table.Rows[0].Cells[3].Paragraphs[0].Append("Индекс").FontSize(11).Bold();
			table.Rows[0].Cells[4].Paragraphs[0].Append("Нормы \nиндексов").FontSize(11).Bold();		
			table.Rows[0].Cells[0].VerticalAlignment = Xceed.Document.NET.VerticalAlignment.Center;
			table.Rows[0].Cells[1].VerticalAlignment = Xceed.Document.NET.VerticalAlignment.Center;
			table.Rows[0].Cells[2].VerticalAlignment = Xceed.Document.NET.VerticalAlignment.Center;
			table.Rows[0].Cells[3].VerticalAlignment = Xceed.Document.NET.VerticalAlignment.Center;
			table.Rows[0].Cells[4].VerticalAlignment = Xceed.Document.NET.VerticalAlignment.Center;
			SetTableStyle(table);

			cellData = _patient.LeftStomachMV.GetNotNullField("");
			CreateAndFillTable(cellData);

			//_document.InsertParagraph().FontSize(2);
			//cellData = _patient.KDOMV.GetNotNullField("");
			//CreateAndFillTable(cellData,true);

			_document.InsertParagraph().FontSize(2);
			cellData = _patient.Aorta.GetNotNullField("");
			CreateAndFillTable(cellData);
			_document.InsertParagraph().FontSize(2);

			cellData = _patient.LeftAtrium.GetNotNullField("");
			CreateAndFillTable(cellData);
			_document.InsertParagraph().FontSize(2);

			cellData.Add("MaxVolumeRightAtriumVolume", new TypeString { Message = "", TypeName = nameof(RightAtrium) });
			cellData = _patient.RightAtrium.GetNotNullField("");
			CreateAndFillTable(cellData);
			_document.InsertParagraph().FontSize(2);

			cellData = _patient.RightStomachFunctionAddition.GetNotNullField("RSF");
			CreateAndFillTable(cellData);
			_document.InsertParagraph().FontSize(2);

			cellData = _patient.RightStomach.GetNotNullField("RS");
			CreateAndFillTable(cellData);
			_document.InsertParagraph().FontSize(2);

			cellData = _patient.RightStomachMain.GetNotNullField("");
			CreateAndFillTable(cellData);
			_document.InsertParagraph().FontSize(2);

			cellData = _patient.LightValve.GetNotNullField("LV");
			CreateAndFillTable(cellData);
			_document.InsertParagraph().FontSize(2);

			cellData = _patient.DiametrNpv.GetNotNullField("");
			CreateAndFillTable(cellData);
			_document.InsertParagraph().FontSize(2);

			cellData = _patient.PatienValue.GetNotNullField("");
			CreateAndFillTable(cellData);
			
			AddValveDefenitionValve();
			_document.InsertParagraph().FontSize(1);
			AddTable();
		}
		private void CreateAndFillTable(IDictionary<string, TypeString> dict, bool fill = false)
		{
			if (dict.Count == 0) return;
			bool isWrite = false;
			int rowCount = 0;
			if (ConstTableName.parametrs.ContainsKey(dict.FirstOrDefault().Value.TypeName))
				rowCount = 1;
			Table currentTable =_document.InsertTable(dict.Count+ rowCount, 5);
			
			int i = 0;
			foreach (var kvp in dict)
			{
				if (!isWrite&& ConstTableName.parametrs.ContainsKey(kvp.Value.TypeName))
				{
					WriteMainRow(i, kvp,ConstTableName.parametrs[kvp.Value.TypeName],currentTable); i += 2;
					isWrite = true;
				}
				else
				{
					WriteSecondaryRow(i, kvp,currentTable); i++;
				}
			}
			tables.Add(currentTable);
			SetTableStyle(currentTable, fill);
		}
		private void WriteMainRow(int i, KeyValuePair<string, TypeString> kvp, string text,Table table)
		{

			table.Rows[i].MergeCells(0, 4);
			table.Rows[i].Cells[0].Paragraphs[0].Append(text).FontSize(11).Bold();
			i++;
			table.Rows[i].Cells[0].Paragraphs[0].Append(ConstTableName.parametrs[kvp.Key]).FontSize(cellParagraphSize);
			table.Rows[i].Cells[1].Paragraphs[0].Append(kvp.Value.Message).FontSize(cellParagraphSize);
			//норма
			if (normaStorage.Norma.ContainsKey(kvp.Key))
			{
				table.Rows[i].Cells[2].Paragraphs[0].Append(normaStorage.Norma[kvp.Key]).FontSize(cellParagraphSize);
				if (ConstTableName.IsBold(normaStorage.Norma[kvp.Key], kvp.Value.Message))
				{
					table.Rows[i].Cells[1].Paragraphs[0].Bold();
				}

			}
			//норма индексов
			if (normaStorage.NormaIndex.ContainsKey(kvp.Key))
			{
				table.Rows[i].Cells[4].Paragraphs[0].Append(normaStorage.NormaIndex[kvp.Key]).FontSize(cellParagraphSize);
				if (!_defaultIndex.indexes.ContainsKey(kvp.Key)) return;
				if (ConstTableName.IsBold(normaStorage.NormaIndex[kvp.Key], _defaultIndex.indexes[kvp.Key]))
				{
					table.Rows[i].Cells[3].Paragraphs[0].Append(_defaultIndex.indexes[kvp.Key]).FontSize(cellParagraphSize).Bold();
				}
				else
				{
					table.Rows[i].Cells[3].Paragraphs[0].Append(_defaultIndex.indexes[kvp.Key]).FontSize(cellParagraphSize);
				}
			}

		}
		private void WriteSecondaryRow(int i, KeyValuePair<string, TypeString> kvp,Table table)
		{
			table.Rows[i].Cells[0].Paragraphs[0].Append(ConstTableName.parametrs[kvp.Key]).FontSize(cellParagraphSize);
			table.Rows[i].Cells[1].Paragraphs[0].Append(kvp.Value.Message).FontSize(cellParagraphSize);
			//норма
			if (normaStorage.Norma.ContainsKey(kvp.Key))
			{
				table.Rows[i].Cells[2].Paragraphs[0].Append(normaStorage.Norma[kvp.Key]).FontSize(cellParagraphSize);
				if (ConstTableName.IsBold(normaStorage.Norma[kvp.Key], kvp.Value.Message))
				{
					table.Rows[i].Cells[1].Paragraphs[0].Bold();
				}
			}
			//норма индексов
			if (normaStorage.NormaIndex.ContainsKey(kvp.Key))
			{
				table.Rows[i].Cells[4].Paragraphs[0].Append(normaStorage.NormaIndex[kvp.Key]).FontSize(cellParagraphSize);
				if (!_defaultIndex.indexes.ContainsKey(kvp.Key)) return;
				if (ConstTableName.IsBold(normaStorage.NormaIndex[kvp.Key], _defaultIndex.indexes[kvp.Key]))
				{
					table.Rows[i].Cells[3].Paragraphs[0].Append(_defaultIndex.indexes[kvp.Key]).FontSize(cellParagraphSize).Bold();
				}
				else
				{
					table.Rows[i].Cells[3].Paragraphs[0].Append(_defaultIndex.indexes[kvp.Key]).FontSize(cellParagraphSize);
				}
			}
			i++;

		}
		public void AddTable()
		{
			IDictionary<string, TypeString> temp = new Dictionary<string, TypeString>();
			_patient.Dopper.GetNotNullField("", ref temp);
			temp.Add("1", new TypeString { Message = "", TypeName = nameof(RightStomachFunction) });
			temp.Add("2", new TypeString { Message = "", TypeName = nameof(LeftStomachFunction) });
			if (temp.Count == 0) return;
			int count = temp.Where(x => x.Value.TypeName == nameof(Dopper)).Count();
			Table additionTable = _document.InsertTable(count + 1, 5);
			additionTable.Rows[0].Cells[0].Paragraphs[0].Append("Допплер КС:").FontSize(paragraphSize).Bold();
			additionTable.Rows[0].Cells[1].Paragraphs[0].Append("Пиковая скорость").FontSize(paragraphSize).Bold();
			additionTable.Rows[0].Cells[2].Paragraphs[0].Append("Норма\n(см.сек)").FontSize(paragraphSize).Bold();
			additionTable.Rows[0].Cells[3].Paragraphs[0].Append("МГД \n(мм рт.ст.)").FontSize(paragraphSize).Bold();
			additionTable.Rows[0].Cells[4].Paragraphs[0].Append("CГД \n(мм рт.ст.)").FontSize(paragraphSize).Bold();
			SetTableStyle(additionTable);
			foreach (var cell in additionTable.Rows.Skip(1).Take(temp.Count))
			{
				cell.Cells[2].FillColor = Color.LightGray;
				cell.Cells[4].FillColor = Color.LightGray;
			}
			int i = 1;
			bool isWrite = false;
			var first = temp.FirstOrDefault().Value.TypeName;
			string typeName = first;	

			foreach (var kvp in temp)
			{
				if (typeName != kvp.Value.TypeName)
				{
					if (typeName == nameof(DegreeReguliration)) i++;
					typeName = kvp.Value.TypeName;
					isWrite = false;
				}
				switch (kvp.Value.TypeName)
				{
					case nameof(Dopper):
						additionTable.Rows[i].Cells[0].Paragraphs[0].Append(ConstTableName.parametrs[kvp.Key]).FontSize(cellParagraphSize);

						additionTable.Rows[i].Cells[1].Paragraphs[0].Append(kvp.Value.Message).FontSize(cellParagraphSize);
						if (ConstTableName.IsBold(normaStorage.normaDopper[kvp.Key], kvp.Value.Message))
						{
							additionTable.Rows[i].Cells[1].Paragraphs[0].Bold();
						}
						additionTable.Rows[i].Cells[2].Paragraphs[0].Append(normaStorage.normaDopper[kvp.Key]).FontSize(cellParagraphSize);
						additionTable.Rows[i].Cells[3].Paragraphs[0].Append(_defaultIndex.mgd[kvp.Key]).FontSize(cellParagraphSize);
						additionTable.Rows[i].Cells[4].Paragraphs[0].Append(_defaultIndex.sgd[kvp.Key]).FontSize(cellParagraphSize);
						i++;
						break;
					case nameof(LeftStomachFunction):
						cellData = _patient.LeftStomachFunction.GetNotNullField("LS");
						if (string.IsNullOrWhiteSpace(_patient.LeftStomachFunction.PickA)) break;
						_document.InsertParagraph().FontSize(2);
						CreateAndFillTable(cellData);
						break;
					case nameof(RightStomachFunction):
						cellData = _patient.RightStomachFunction.GetNotNullField("RSF");
						if (string.IsNullOrWhiteSpace(_patient.RightStomachFunction.PickA)) break;
						_document.InsertParagraph().FontSize(2);
						CreateAndFillTable(cellData);
						break;
					default:
						break;
				}

			}
			if (count == 0)
				additionTable.RemoveRow(0);
			int dgCount = 0;
			int dg = 0;
			temp.Clear();
			_patient.DegreeReguliration.GetNotNullField("DR", ref temp);
			dgCount = temp.Count();

			_patient.CommentaryMV.GetNotNullField("", ref temp);
			if (temp.Count == 0) return;
			_document.InsertParagraph().FontSize(2);
			var cc = temp.Select(x => x.Value.TypeName).Distinct();
				Table tt = _document.InsertTable(cc.Count(), 5);
			i = 0;
			SetTableStyle(tt);
			foreach (var kvp in temp)
			{
				if (typeName != kvp.Value.TypeName)
				{
					if (typeName == nameof(DegreeReguliration)) i++;
					typeName = kvp.Value.TypeName;
					isWrite = false;
				}
				switch (kvp.Value.TypeName)
				{      
					case nameof(DegreeReguliration):
						dg++;
						if (!isWrite)
						{
							tt.Rows[i].MergeCells(0, 4);
							tt.Rows[i].Cells[0].Paragraphs[0].Append("Регургитации на клапанах").FontSize(cellParagraphSize).Bold();
							tt.Rows[i].Paragraphs[0].Append("\n").Append(ConstTableName.parametrs[kvp.Key]).FontSize(cellParagraphSize).Append("\n");
							isWrite = true;
							continue;
						}
						tt.Rows[i].Paragraphs[0].Append(ConstTableName.parametrs[kvp.Key]).FontSize(cellParagraphSize);
						if(dg!=dgCount)
						tt.Rows[i].Paragraphs[0].Append("\n");
						break;
					case nameof(CommentaryMV):
						tt.Rows[i].MergeCells(0, 4);
						tt.Rows[i].Cells[0].Paragraphs[0].Append("Комментарии:").FontSize(cellParagraphSize).Bold();
						tt.Rows[i].Paragraphs[0].Append("\n").Append(kvp.Value.Message).FontSize(cellParagraphSize);
						break;				
					default:
						break;
				}

			}

		}
		public void AddValveDefenitionValve()
		{
			IDictionary<string, TypeString> temp = new Dictionary<string, TypeString>();
			_patient.MitralValve.GetNotNullField("MV", ref temp);
			if (string.IsNullOrWhiteSpace(_patient.MitralValve.Comment))
				temp.Add("CommentMV", new TypeString { Message = "", TypeName = nameof(MitralValve) });
			_patient.PulmonaryValve.GetNotNullField("PV", ref temp);
			if (string.IsNullOrWhiteSpace(_patient.PulmonaryValve.Comment))
				temp.Add("CommentPV", new TypeString { Message = "", TypeName = nameof(PulmonaryValve) });
			_patient.MKMR.GetNotNullField("MKMR", ref temp);
			_patient.AorticValve.GetNotNullField("AV", ref temp);
			if (string.IsNullOrWhiteSpace(_patient.AorticValve.Comment))
				temp.Add("CommentAV", new TypeString { Message = "", TypeName = nameof(AorticValve) });
			_patient.TricuspidialValve.GetNotNullField("TV", ref temp);
			if (string.IsNullOrWhiteSpace(_patient.TricuspidialValve.Comment))
				temp.Add("CommentTV", new TypeString { Message = "", TypeName = nameof(TricuspidialValve) });
			if (temp.Count == 0) return;
			Table additionTable = _document.InsertTable(1, 5);
			SetTableStyle(additionTable);
			additionTable.Rows[0].MergeCells(0, 4);
			additionTable.Rows[0].Cells[0].Paragraphs[0].Append("Описание клапанов:").FontSize(paragraphSize).Bold();
			additionTable.Rows[0].Paragraphs[0].Append("\n");
			int i = 0;
			bool isWrite = false;
			var first = temp.FirstOrDefault().Value.TypeName;
			string typeName = first;
			foreach (var kvp in temp)
			{
				if (typeName != kvp.Value.TypeName)
				{
					additionTable.Rows[0].Paragraphs[0].Append("\n");
					typeName = kvp.Value.TypeName;
					isWrite = false;
				}
				if (!isWrite)
				{
					//additionTable.Rows[0].Paragraphs[0].Append("\n");

					additionTable.Rows[i].Cells[0].Paragraphs[0].Append(ConstTableName.parametrs[typeName]).FontSize(cellParagraphSize).Bold();
					if (kvp.Key.Contains("Comment"))
						additionTable.Rows[i].Cells[0].Paragraphs[0].Append(kvp.Value.Message).FontSize(cellParagraphSize);
					else
					{
						additionTable.Rows[0].Paragraphs[0].Append("\n");
						additionTable.Rows[i].Cells[0].Paragraphs[0].Append(ConstTableName.parametrs[kvp.Key] + ": ").FontSize(cellParagraphSize).Bold();
						additionTable.Rows[i].Cells[0].Paragraphs[0].Append(kvp.Value.Message).FontSize(cellParagraphSize);
					}
					isWrite = true;
				}
				else
				{
					additionTable.Rows[0].Paragraphs[0].Append("\n");
					additionTable.Rows[i].Cells[0].Paragraphs[0].Append(ConstTableName.parametrs[kvp.Key]+": ").FontSize(cellParagraphSize).Bold();
					additionTable.Rows[i].Cells[0].Paragraphs[0].Append(kvp.Value.Message).FontSize(cellParagraphSize);
				}
			}

		}
		public void AddImage()
		{
			if (_patient.Segment == null || _patient.Segment.FirstSegment == null) return;

			Paragraph p = _document.InsertParagraph("Оценка локальной сократимости левого желудочка").Bold().FontSize(paragraphSize);
			p.Alignment = Alignment.center;
			Paragraph p1 = _document.InsertParagraph();
			Table imgTable = _document.InsertTable(2, 4);
			//imgTable.SetColumnWidth(0, 7425); imgTable.SetColumnWidth(1, 2475);
			imgTable.SetColumnWidth(0, 2500); imgTable.SetColumnWidth(1, 2300);
			imgTable.SetColumnWidth(2, 2300);
			imgTable.SetColumnWidth(3, 2500);

			//imgTable.Rows[0].MergeCells(0, 1);
			//imgTable.Rows[0].Cells[0].Paragraphs[0].InsertPicture(InsertImg(7, _patient.Segment?.MiddleCircleSegment,100,70));
			//imgTable.Rows[0].Cells[0].Paragraphs[0].InsertPicture(InsertImg(5, _patient.Segment?.FirstSegment));
			//imgTable.Rows[0].Cells[0].Paragraphs[0].InsertPicture(InsertImg(3, _patient.Segment?.SecondSegment));
			//imgTable.Rows[0].Cells[0].Paragraphs[0].InsertPicture(InsertImg(1, _patient.Segment?.ThirdSegment));
			imgTable.Rows[0].Cells[0].Paragraphs[0].InsertPicture(InsertImg(1, _patient.Segment?.FirstSegment));
			imgTable.Rows[0].Cells[1].Paragraphs[0].InsertPicture(InsertImg(3, _patient.Segment?.ThirdSegment));
			imgTable.Rows[0].Cells[2].Paragraphs[0].InsertPicture(InsertImg(5, _patient.Segment?.FifthSegment));
			imgTable.Rows[0].Cells[3].Paragraphs[0].InsertPicture(InsertImg(7, _patient.Segment?.MiddleCircleSegment, 100, 70));

			//imgTable.Rows[1].Cells[0].Paragraphs[0].InsertPicture(InsertImg(6, _patient.Segment?.ForthSegment));
			//imgTable.Rows[1].Cells[0].Paragraphs[0].InsertPicture(InsertImg(4, _patient.Segment?.FifthSegment));
			//imgTable.Rows[1].Cells[0].Paragraphs[0].InsertPicture(InsertImg(2, _patient.Segment?.SixSegment));
			imgTable.Rows[1].Cells[2].Paragraphs[0].InsertPicture(InsertImg(6, _patient.Segment?.SixSegment));
			imgTable.Rows[1].Cells[1].Paragraphs[0].InsertPicture(InsertImg(4, _patient.Segment?.ForthSegment));
			imgTable.Rows[1].Cells[0].Paragraphs[0].InsertPicture(InsertImg(2, _patient.Segment?.SecondSegment));

			imgTable.Rows[1].Cells[3].Paragraphs[0].InsertPicture(InsertImg(8, _patient.Segment?.CirlceSegment,150, 110));
			ClearBorder(imgTable);
			if (!string.IsNullOrWhiteSpace(_patient.Patient.CommentarySegment)) {
				_document.InsertParagraph().FontSize(9);
				_document.InsertParagraph("Комментарии").Bold().FontSize(paragraphSize).SpacingAfter(2).Append("\n").
				Append(_patient.Patient.CommentarySegment).SpacingAfter(1).FontSize(paragraphSize);
				_document.InsertParagraph().FontSize(9);
			}
	
			p1.Alignment = Alignment.center;
			Paragraph p2 = _document.InsertParagraph();
			var path = _pathToBinFolder + @"\Color.png";
			if (File.Exists(path.Substring(6)))
			{
				Image image;
				image = _document.AddImage(path.Substring(6));
				Picture picture = image.CreatePicture(42,170);
				p2.AppendPicture(picture);
			}
			_document.InsertParagraph().FontSize(11);

		}
		private Picture InsertImg(int i,byte[]bytes,int width=150,int height=100)
		{
			Image image;
			Picture picture;
			if (bytes == null || bytes.Count() == 0) return null;
			if (File.Exists(_savePdfDocumentsPath + $@"\{date}{i}.jpg"))
			{
				image = _document.AddImage(_savePdfDocumentsPath + $@"\{date}{i}.jpg");
				picture = image.CreatePicture(height, width);
			}
			else
			{
				var img = bytes.ConvertToImage();
				img.Save(_savePdfDocumentsPath + $@"\{date}{i}.jpg");
				image = _document.AddImage(_savePdfDocumentsPath + $@"\{date}{i}.jpg");
				picture = image.CreatePicture(height, width);
			}
			return picture;
		}
		private void SetNull()
		{
			if (_patient.RightStomach != null)
			{
				_patient.RightStomach.MaxGradientTricuspidil = null;
				_patient.RightStomach.PressurePP = null;
				_patient.RightStomach.SistolPressureLA = null;

				_patient.RightStomach.SelectedColl = null;
				_patient.RightStomach.Width = null;
			}

			if (_patient.RightStomachFunction != null)
			{
				_patient.RightStomachFunction.SelectedFv = null;
				_patient.RightStomachFunction.FV = null;
				_patient.RightStomachFunction.LastVolumeLJ = null;

				_patient.RightStomachFunction.KSO = null;
				_patient.RightStomachFunction.UOK = null;
				_patient.RightStomachFunction.MOK = null;
				_patient.RightStomachFunction.GLS = null;
				_patient.RightStomachFunction.DPDT = null;
				_patient.RightStomachFunction.VelocityAortValve = null;
				_patient.RightStomachFunction.VelocityLeftStomach = null;
				//_patient.RightStomachFunction.PickE = null;

				_patient.RightStomachFunction.AverageGradientAortValue = null;
				_patient.RightStomachFunction.AverageGradientPressureTrucuspidil = null;
				_patient.RightStomachFunction.MaxGradientAortValue = null;
				_patient.RightStomachFunction.MaxGradientTricuspidil = null;
				_patient.RightStomachFunction.MaxGradientLeftStomach = null;
			}
			if (_patient.PatienValue != null)
			{
				_patient.PatienValue.LastDiastolSizeLeftStomach = null;
				_patient.PatienValue.LastSislotSizeLeftStomach = null;
				_patient.PatienValue.RelativeThicknessLeftStomach = null;
				_patient.PatienValue.ThicknessLowerWallLeftStomach = null;
				_patient.PatienValue.ThicknessMejPereMJP = null;
				_patient.PatienValue.VelocityVavlvePulmonaryArtery = null;
				_patient.PatienValue.SelectedINS = null;
				//_patient.PatienValue.SelectedTypeThicknessMiocard = null;
				_patient.PatienValue.ThicknessSegment = null;
				_patient.PatienValue.MovementMJP = null;
				_patient.PatienValue.LeftAtrium = null;
				_patient.PatienValue.MaxGradientPressurePulmonaryArtery = null;
				_patient.PatienValue.INS = null;
				_patient.PatienValue.FractionAcceleration = null;
			}
			if (_patient.LeftStomachFunction != null)
			{
				//_patient.LeftStomachFunction.PickE = null;
				_patient.LeftStomachFunction.AverageGradientMitralValve = null;
				_patient.LeftStomachFunction.MaxGradientMitralValve = null;
			}

			if(_patient.RightStomachMain!=null)
				_patient.RightStomachMain.SelectedRightStomach = null;

			if (_patient.ECG != null)
				_patient.ECG.Rhythm = null;

			if (_patient.RightStomachFunctionAddition != null)
			{
				_patient.RightStomachFunctionAddition.MaxValomeLeftAtrium = null;
				_patient.RightStomachFunctionAddition.MaxValomeLeftAtrium = null;
				_patient.RightStomachFunctionAddition.MaxValomeRightAtrium = null;
			}

			if (_patient.PulmonaryValve != null)
				_patient.PulmonaryValve.SelectedText = null;

			if (_patient.Patient != null)
				_patient.Patient.Commentary = null;

		}
		private void SetTableStyle(Table table,bool fill=false)
		{
			int skipCount = 1;
			table.SetColumnWidth(0, 4200); table.SetColumnWidth(1, 1600);
			table.SetColumnWidth(2, 1400); table.SetColumnWidth(3, 1300);
			table.SetColumnWidth(4, 1400);
			table.SetBorder(TableBorderType.Left, new Border(BorderStyle.Tcbs_thick, BorderSize.five, 0, Color.Black));
			table.SetBorder(TableBorderType.Bottom, new Border(BorderStyle.Tcbs_thick, BorderSize.five, 0, Color.Black));
			table.SetBorder(TableBorderType.Right, new Border(BorderStyle.Tcbs_thick, BorderSize.five, 0, Color.Black));
			table.SetBorder(TableBorderType.Top, new Border(BorderStyle.Tcbs_thick, BorderSize.five, 0, Color.Black));
			if (fill) skipCount = 0;
			foreach (var cell in table.Rows.Skip(skipCount))
			{
				cell.Cells[2].FillColor = Color.LightGray;
				cell.Cells[4].FillColor = Color.LightGray;
			}
		}
		private void CreateFolder()
		{
			_savePdfDocumentsPath.CreateFolder();
		}
	}

	public static class WordExtenstion
	{
		public static void CreateFolder(this string str)
		{
			if (Directory.Exists(str))
				return;
			Directory.CreateDirectory(str);
		}

		public static System.Drawing.Image ConvertToImage(this byte[] bytes)
		{
			MemoryStream ms = new MemoryStream(bytes);
			System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
			return returnImage;

		}
	}
}
