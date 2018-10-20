using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class c2taskseed_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Excels/C2/c2taskseed.xlsx";
	private static readonly string exportPath = "Assets/Excels/C2/c2taskseed.asset";
	private static readonly string[] sheetNames = { "Sheet1","Sheet2","Sheet3","Sheet4","Sheet5","Sheet6","Sheet7","Sheet8","Sheet9","Sheet10","Sheet11","Sheet12","Sheet13","Sheet14","Sheet15","Sheet16","Sheet17","Sheet18","Sheet19","Sheet20", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			taskNumSheet data = (taskNumSheet)AssetDatabase.LoadAssetAtPath (exportPath, typeof(taskNumSheet));
			if (data == null) {
				data = ScriptableObject.CreateInstance<taskNumSheet> ();
				AssetDatabase.CreateAsset ((ScriptableObject)data, exportPath);
				data.hideFlags = HideFlags.NotEditable;
			}
			
			data.sheets.Clear ();
			using (FileStream stream = File.Open (filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
				IWorkbook book = null;
				if (Path.GetExtension (filePath) == ".xls") {
					book = new HSSFWorkbook(stream);
				} else {
					book = new XSSFWorkbook(stream);
				}
				
				foreach(string sheetName in sheetNames) {
					ISheet sheet = book.GetSheet(sheetName);
					if( sheet == null ) {
						Debug.LogError("[QuestData] sheet not found:" + sheetName);
						continue;
					}

					taskNumSheet.Sheet s = new taskNumSheet.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						taskNumSheet.Param p = new taskNumSheet.Param ();
						
					cell = row.GetCell(0); p.col1 = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.col2 = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(2); p.col3 = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(3); p.col4 = (int)(cell == null ? 0 : cell.NumericCellValue);
						s.list.Add (p);
					}
					data.sheets.Add(s);
				}
			}

			ScriptableObject obj = AssetDatabase.LoadAssetAtPath (exportPath, typeof(ScriptableObject)) as ScriptableObject;
			EditorUtility.SetDirty (obj);
		}
	}
}
