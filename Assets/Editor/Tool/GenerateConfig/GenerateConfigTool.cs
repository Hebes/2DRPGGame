using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Debug = UnityEngine.Debug;

/*--------脚本描述-----------
				
电子邮箱：
	1607388033@qq.com
作者:
	暗沉
描述:
    生成配置文件
    请遵守命名规范

-----------------------*/

namespace ACEditor
{
    public class GenerateConfigTool : EditorWindow
    {
        private static string namespaceName = "ACFrameworkCore";    //命名空间


        private static string CommonPath = $"{Application.dataPath}\\";
        private static string GenerateConfigPath = $"{Application.dataPath}/HotUpdate/GameMain/Config/Common/";


        /// <summary>
        /// 过滤关键词
        /// </summary>
        private static string FilterKeyword(string str, params string[] filterSuffix)
        {
            foreach (string key in filterSuffix)
                str = str.Replace(key, "");
            return str;
        }

        /// <summary>
        /// 生成配置文件(不带路径写入)
        /// </summary>
        /// <param name="path">需要获取的文件的路径</param>
        /// <param name="creatPath">创建脚本的路径</param>
        /// <param name="ConfigName">脚本名称</param>
        /// <param name="filterSuffix">需要的文件的后缀</param>
        private static void CreatConfig(string path, string creatPath, string ConfigName, params string[] filterSuffix)
        {
            Debug.Log("执行不带路径写入!");
            string className = $"{ConfigName}Config";//脚本名称

            //寻找需要的文件
            List<string> pathsList = new List<string>();
            foreach (string key in filterSuffix)
            {
                string[] strings = Directory.GetFiles(path, $"*{key}", SearchOption.AllDirectories);
                pathsList.AddRange(strings.ToList());
            }
            //拼接字符串
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"namespace {namespaceName}\r\n{{");
            sb.AppendLine($"    public class {className}\r\n    {{");
            foreach (string pathTemp in pathsList)
            {
                //文件名称(未过滤的)
                string fileNameOld = Path.GetFileNameWithoutExtension(pathTemp);
                //文件名称(过滤后的)
                string fileNameNew = Path.GetFileNameWithoutExtension(pathTemp).
                    Replace("@", "_").
                    Replace("(", "").
                    Replace(")", "").
                    Replace("-", "_").
                    Replace(" ", "");
                //获取后缀
                string extendedName = Path.GetExtension(pathTemp);
                sb.AppendLine($"        public const string {fileNameNew}{ConfigName} = \"{fileNameOld}\";");
            }
            sb.AppendLine("    }\r\n}");

            //删除原来的文件
            string classPath = $"{creatPath}/{className}.cs";
            string classPathMeta = $"{creatPath}/{className}.{filterSuffix}.meta";

            if (File.Exists(classPath))
            {
                Debug.Log("文件存在开始删除!");
                File.Delete(classPath);
                File.Delete(classPathMeta);
                Debug.Log("文件删除成功!");
            }
            File.WriteAllText(classPath, sb.ToString());
            Debug.Log("文件写入成功!");
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 生成配置文件(全路径写入)
        /// </summary>
        /// <param name="path">需要获取的文件的路径</param>
        /// <param name="creatPath">创建脚本的路径</param>
        /// <param name="ConfigName">脚本名称</param>
        /// <param name="filterSuffix">需要的文件的后缀</param>
        private static void CreatConfigAllPath(string path, string creatPath, string ConfigName, params string[] filterSuffix)
        {
            string className = $"{ConfigName}Config";//脚本名称
            //寻找需要的文件
            List<string> pathsList = new List<string>();
            foreach (string key in filterSuffix)
            {
                string[] strings = Directory.GetFiles(path, $"*{key}", SearchOption.AllDirectories);
                pathsList.AddRange(strings.ToList());
            }
            //拼接字符串
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"namespace {namespaceName}\r\n{{");
            sb.AppendLine($"    public class {className}\r\n    {{");
            foreach (string pathTemp in pathsList)
            {
                //文件名称
                string fileName = Path.GetFileNameWithoutExtension(pathTemp).
                    Replace("@", "_").
                    Replace("(", "").
                    Replace(")", "").
                    Replace("-", "_").
                    Replace(" ", "");
                //文件路径
                string extendedName = Path.GetExtension(pathTemp);//如不需要请直接添加上去,这个是获取拓展名称
                //string assetsPath = pathTemp.Replace(extendedName, "").Replace(path, "").Replace("\\", "/");//文件路径
                string assetsPath = pathTemp.Replace(path, "").Replace("\\", "/");//文件路径
                sb.AppendLine($"        public const string {fileName}{ConfigName} = \"{assetsPath}\";");
            }
            sb.AppendLine("    }\r\n}");
            //删除原来的文件
            string classPath = $"{creatPath}/{className}.cs";
            string classPathMeta = $"{creatPath}/{className}.{filterSuffix}.meta";
            if (File.Exists(classPath))
            {
                Debug.Log("文件存在开始删除!");
                File.Delete(classPath);
                File.Delete(classPathMeta);
                Debug.Log("文件删除成功!");
            }
            File.WriteAllText(classPath, sb.ToString());
            Debug.Log("文件写入成功!");
            AssetDatabase.Refresh();
        }



        //配置文件
        [MenuItem("Tool/GenerateConfig/生成Prefab配置文件")]
        public static void GeneratePrefabConfig()
        {
            string path = $"{CommonPath}/Resources/";
            string CreatPath = $"{CommonPath}/Scripts/GameModel/Config";
            CreatConfigAllPath(path, CreatPath, "Prefab", ".prefab");
        }
        [MenuItem("Tool/GenerateConfig/生成UIPanel配置文件")]
        public static void GenerateUIPanelConfig()
        {
            //WriteData("UIPanel", string.Empty, ".prefab");
        }
        [MenuItem("Tool/GenerateConfig/生成Scenes配置文件")]
        public static void GenerateScenesConfig()
        {
            string path = $"{CommonPath}/Scenes/";
            string CreatPath = $"{CommonPath}/Scripts/GameModel/Config";
            CreatConfigAllPath(path, CreatPath, "Scenes", ".unity");
        }
        [MenuItem("Tool/GenerateConfig/生成Tag配置文件")]
        public static void GenerateTagConfig()
        {
            string[] tags = InternalEditorUtility.tags;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("namespace ACFrameworkCore\r\n{");
            sb.AppendLine("    public class ConfigTag\r\n    {");
            foreach (string s in tags)
                sb.AppendLine($"        public const string {s}Tag = \"{s}\";");
            sb.AppendLine("    }\r\n}");
            string classPath = $"{GenerateConfigPath}ConfigTag.cs";
            if (File.Exists(classPath))
                File.Delete(classPath);
            File.WriteAllText(classPath, sb.ToString());
            AssetDatabase.Refresh();
        }
        [MenuItem("Tool/GenerateConfig/生成Layer配置文件")]
        public static void GenerateLayerConfig()
        {
            var tags = InternalEditorUtility.layers;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("namespace ACFrameworkCore\r\n{");
            sb.AppendLine("    public class ConfigLayer\r\n    {");

            foreach (string s in tags)
            {
                string tempstr = s;
                sb.AppendLine($"        public const string Layer{tempstr.Replace(" ", "").Trim()} = \"{tempstr}\";");
            }
            sb.AppendLine("    }\r\n}");
            string classPath = $"{GenerateConfigPath}ConfigLayer.cs";
            if (File.Exists(classPath))
            {
                Debug.Log("文件存在开始删除!");
                File.Delete(classPath);
            }

            File.WriteAllText(classPath, sb.ToString());
            AssetDatabase.Refresh();
        }
        [MenuItem("Tool/GenerateConfig/生成SortingLayer配置文件")]
        public static void GenerateSortingLayerConfig()
        {
            Type internalEditorUtilityType = typeof(InternalEditorUtility);
            PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
            string[] sortingLayers = (string[])sortingLayersProperty.GetValue(null, new object[0]);


            StringBuilder sb = new StringBuilder();
            sb.AppendLine("namespace ACFrameworkCore\r\n{");
            sb.AppendLine("    public class ConfigSortingLayer\r\n    {");

            foreach (string s in sortingLayers)
            {
                string tempstr = s;
                sb.AppendLine($"        public const string SortingLayer{tempstr.Replace(" ", "").Trim()} = \"{tempstr}\";");
            }
            sb.AppendLine("    }\r\n}");
            string classPath = $"{GenerateConfigPath}ConfigSortingLayer.cs";
            if (File.Exists(classPath))
            {
                Debug.Log("文件存在开始删除!");
                File.Delete(classPath);
            }

            File.WriteAllText(classPath, sb.ToString());
            AssetDatabase.Refresh();
        }
        [MenuItem("Tool/GenerateConfig/生成bytes配置文件")]
        public static void GeneratebytesConfig()
        {
            //WriteData("ConfigData", string.Empty, ".bytes");
        }
        [MenuItem("Tool/GenerateConfig/生成Sprites配置文件")]
        public static void GenerateSpritesConfig()
        {
            //WriteData("Sprites", string.Empty, ".png");
        }
        [MenuItem("Tool/GenerateConfig/生成Animations配置文件")]
        public static void GenerateAnimationsConfig()
        {
            //WriteData("Animations", string.Empty, ".overrideController");
        }
        [MenuItem("Tool/GenerateConfig/生成Effects配置文件")]
        public static void GenerateEffectsConfig()
        {
            //WriteData("Effects", string.Empty, ".prefab");
        }
        [MenuItem("Tool/GenerateConfig/生成Sound配置文件")]
        public static void GenerateSoundConfig()
        {
            //WriteData("Sound", string.Empty, ".wav", ".ogg");
        }
    }
}
