using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ACEditor
{
    public class ConfigToolUI : EditorWindow
    {
        private bool isOpenPreview = true;
        private static string CommonPath = $"{Application.dataPath}\\";
        private string CreatPath = $"{Application.dataPath}/Script/Model/Config";
        private string content;
        private string LoadPath = $"{CommonPath}/Resources/";


        [MenuItem("Tool/生成配置文件#C #C")]
        public static void ShowConfigToolUI()
        {
            if (!EditorWindow.HasOpenInstances<ConfigToolUI>())
                GetWindow(typeof(ConfigToolUI), false, "生成配置文件").Show();
            else
                GetWindow(typeof(ConfigToolUI)).Close();
        }

        private void OnGUI()
        {
            Repaint();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("生成Prefab数据预览"))
            {
                content = GenerateConfigTool.ReadDataString(LoadPath, "ConfigPrefab", DataReadType.CommonSuffixation, ".prefab");
            }
            if (GUILayout.Button("生成Prefab数据预览(全路径)"))
            {
                content = GenerateConfigTool.ReadDataString(LoadPath, "ConfigPrefab", DataReadType.AllPathSuffixation, ".prefab");
            }
            if (GUILayout.Button("生成Prefab数据文件"))
            {
                content = GenerateConfigTool.ReadDataString(LoadPath, "ConfigPrefab", DataReadType.CommonSuffixation, ".prefab");
                string creatFilePath = $"{CreatPath}/ConfigPrefab.cs";
                GenerateConfigTool.WriteData(content, creatFilePath);
            }
            if (GUILayout.Button("生成Prefab数据文件(全路径)"))
            {
                content = GenerateConfigTool.ReadDataString(LoadPath, "ConfigPrefab", DataReadType.AllPathSuffixation, ".prefab");
                string creatFilePath = $"{CreatPath}/ConfigPrefab.cs";
                GenerateConfigTool.WriteData(content, creatFilePath);
            }
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("生成Material数据预览(全路径)"))
            {
                string LoadPath = $"{CommonPath}/Resources/";
                content = GenerateConfigTool.ReadDataString(LoadPath, "ConfigMaterial", DataReadType.AllPathSuffixation, ".mat");
            }
            if (GUILayout.Button("生成Material数据文件(全路径)"))
            {
                string LoadPath = $"{CommonPath}/Resources/";
                content = GenerateConfigTool.ReadDataString(LoadPath, "ConfigMaterial", DataReadType.AllPathSuffixation, ".mat");
                string creatFilePath = $"{CreatPath}/ConfigMaterial.cs"; 
                GenerateConfigTool.WriteData(content, creatFilePath);
            }
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("生成Tag数据预览"))
            {
                content = GenerateConfigTool.ReadTagData();
            }
            if (GUILayout.Button("生成Tag数据文件"))
            {
                content = GenerateConfigTool.ReadTagData();
                string creatFilePath = $"{CreatPath}/ConfigTag.cs";
                GenerateConfigTool.WriteData(content, creatFilePath);
            }
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("生成Layer数据预览"))
            {
                content = GenerateConfigTool.ReadLayerData();
            }
            if (GUILayout.Button("生成Layer数据文件"))
            {
                content = GenerateConfigTool.ReadLayerData();
                string creatFilePath = $"{CreatPath}/ConfigLayer.cs";
                GenerateConfigTool.WriteData(content, creatFilePath);
            }
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("生成SortingLayer数据预览"))
            {
                content = GenerateConfigTool.ReadSortingLayerData();
            }
            if (GUILayout.Button("生成SortingLayer数据文件"))
            {
                content = GenerateConfigTool.ReadSortingLayerData();
                string creatFilePath = $"{CreatPath}/ConfigSortingLayer.cs";
                GenerateConfigTool.WriteData(content, creatFilePath);
            }
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("生成Scenes数据预览"))
            {
                string LoadPath = $"{CommonPath}/Scenes/";
                content = GenerateConfigTool.ReadDataString(LoadPath, "ConfigScenes", DataReadType.CommonSuffixation, ".unity");
            }
            if (GUILayout.Button("生成Scenes数据文件"))
            {
                string LoadPath = $"{CommonPath}/Scenes/";
                content = GenerateConfigTool.ReadDataString(LoadPath, "ConfigScenes", DataReadType.CommonSuffixation, ".unity");
                string creatFilePath = $"{CreatPath}/ConfigScenes.cs";
                GenerateConfigTool.WriteData(content, creatFilePath);
            }
            EditorGUILayout.EndHorizontal();


            GUILayout.Space(5f);
            EditorGUILayout.LabelField("预览", EditorStyles.label);
            isOpenPreview = EditorGUILayout.ToggleLeft("是否开启预览", isOpenPreview, GUILayout.Width(130f));
            if (isOpenPreview && !string.IsNullOrEmpty(content))
                EditorGUILayout.TextArea(content);
        }
    }
}
