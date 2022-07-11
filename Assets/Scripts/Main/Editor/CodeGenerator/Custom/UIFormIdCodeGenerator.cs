using System;
using System.Collections.Generic;
using System.IO;
using DE.Editor;
using DE.Editor.DataTableTools;
using GameFramework;
using OfficeOpenXml;
using UnityEditor;

namespace MetaArea
{
    [CodeGenerator()]
    public class UIFormIdCodeGenerator : CodeGeneratorBase
    {
        private class UIFormIDData
        {
            public int EnumId { get; set; }
            public string Name { get; set; }
            public string Comment { get; set; }
        }
        
        public UIFormIdCodeGenerator() : base("Assets/Scripts/Main/Runtime/Definition/Enum",
            "Definition/Enum")
        {
        }

        public override void Draw()
        {
            IsHotfix = EditorGUILayout.ToggleLeft("热更层代码", IsHotfix);
       
            EditorGUILayout.LabelField("自动生成代码路径", GetCodeFolder());
        }


        private List<UIFormIDData> GetUiFormIds(bool isHotfix)
        {
            List<UIFormIDData>uiFormIds = new List<UIFormIDData>
            {
                new UIFormIDData()
                {
                    EnumId = 0,
                    Comment = "未定义",
                    Name = "Undefined"
                }
            };
            string dataTableFolderPath =
                isHotfix ? HotfixDataTableConfig.DataTableFolderPath : DataTableConfig.DataTableFolderPath;

            string dataTablePath = Utility.Path.GetRegularPath(Path.Combine(dataTableFolderPath, "UIForm.txt"));
            if (File.Exists(dataTablePath))
            {
                string[] lines = File.ReadAllLines(dataTablePath);
                for (int i = 4; i < lines.Length; i++)
                {
                    string[] dataArray = lines[i].Split('\t');
                    uiFormIds.Add(new UIFormIDData()
                    {
                        EnumId = int.Parse(dataArray[1]),
                        Comment = dataArray[2],
                        Name = dataArray[3],
                    });
                }
                uiFormIds.Sort((x, y) => x.EnumId.CompareTo(y.EnumId));
                return uiFormIds;
            }
            
            
            string excelsFolderPath =
                isHotfix ? HotfixDataTableConfig.ExcelsFolder : DataTableConfig.ExcelsFolder;

            string excelPath = Utility.Path.GetRegularPath(Path.Combine(excelsFolderPath, "UIForm.xlsx"));

            if (File.Exists(excelPath))
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (FileStream fileStream =
                       new FileStream(excelPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (ExcelPackage excelPackage = new ExcelPackage(fileStream))
                    {
                        ExcelWorksheet uiFormSheet = null; 
                        for (int i = 0; i < excelPackage.Workbook.Worksheets.Count; i++)
                        {
                            ExcelWorksheet sheet = excelPackage.Workbook.Worksheets[i];
                            if (sheet.Name == "UIForm")
                            {
                                uiFormSheet = sheet;
                            }
                        }

                        if (uiFormSheet == null)
                        {
                            throw new Exception("UIForm excel can not have UIForm sheet.");
                        }
                        int columnCount = uiFormSheet.Dimension.End.Column;
                        for (int i = 5; i <= uiFormSheet.Dimension.End.Row; i++)
                        {
                            uiFormIds.Add(new UIFormIDData()
                            {
                                EnumId = ((IConvertible)uiFormSheet.Cells[i,2].Value).ToInt32(null),
                                Comment = (string)uiFormSheet.Cells[i,3].Value,
                                Name =(string)uiFormSheet.Cells[i,4].Value,
                            });
                        }
                        uiFormIds.Sort((x, y) => x.EnumId.CompareTo(y.EnumId));
                        return uiFormIds;
                    }
                }
            }

            throw new Exception("Can not find UiForm Datatable.");
        }

        public override bool GenCode()
        {
            return GenCode(IsHotfix);
        }
        public bool GenCode(bool isHotfix)
        {
            IsHotfix = isHotfix;
            List<UIFormIDData> uiFormIds = GetUiFormIds(isHotfix);
          
            SetCodeName("UIFormId");

            string codePath = GetCodePath();
            string nameSpace = isHotfix ? "MetaArea.Hotfix" : "MetaArea";
            if (!Directory.Exists(GetCodeFolder()))
            {
                Directory.CreateDirectory(GetCodeFolder());
            }

            using (StreamWriter sw = new StreamWriter(codePath))
            {
                //命名空间
                sw.WriteLine("namespace " + nameSpace);
                sw.WriteLine("{");

                //类名
                sw.WriteLine($"\tpublic enum {CodeName} : int");
                sw.WriteLine("\t{");

                foreach (var uiFormIdData in uiFormIds)
                {
                    sw.WriteLine($"\t\t/// <summary>");
                    sw.WriteLine($"\t\t/// {uiFormIdData.Comment}");
                    sw.WriteLine($"\t\t/// </summary>");  
                    sw.WriteLine($"\t\t{uiFormIdData.Name} = {uiFormIdData.EnumId},");
                    sw.WriteLine("");
                }
                sw.WriteLine("\t}");
                sw.WriteLine("}");
            }

            return true;
        }
    }
}