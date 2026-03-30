#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using UnityEngine.UIElements;


public class JsonToScriptableConverter : EditorWindow
{
    private string jsonFilePath = "";                                           //JSON 파일 경로 문자열 값
    private string outputFolder = "Assets/ScriptableObjects";                   //출력 SO 파일 경로 값
    private bool createDatabase = true;                                         //데이터 베이스 활용 여부 체크 값

    [MenuItem("Tools/JSON to Scriptable Objects")]
    public static void ShowWindow()
    {
        GetWindow<JsonToScriptableConverter>("JSON to Scriptable Objects");
    }

    void OnGUI()
    {
        GUILayout.Label("JSON to Scriptable object Converter", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        if (GUILayout.Button("Select JSON File"))
        {
            jsonFilePath = EditorUtility.OpenFilePanel("Select JSON File", "", "json");
        }

        EditorGUILayout.LabelField("Selected File :", jsonFilePath);
        EditorGUILayout.Space();
        outputFolder = EditorGUILayout.TextField("Output Folder: ", outputFolder);
        createDatabase = EditorGUILayout.Toggle("Create Database", createDatabase);
        EditorGUILayout.Space();

        if(GUILayout.Button("Convert to Scriptable Objects"))
        {
            if(string.IsNullOrEmpty(jsonFilePath))
            {
                EditorUtility.DisplayDialog("Error", "Please select a JSON file first!", "OK");
                return;
            }
            ConvertJssonToScriptableObjects();
        }

    }

    private void ConvertJssonToScriptableObjects()
    {
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);

        }

        string jsonText = File.ReadAllText(jsonFilePath);

        try 
        {
            List<UnitData> unitDataList = JsonConvert.DeserializeObject<List<UnitData>>(jsonText);

            List<ItemSO> createdUnits = new List<ItemSO>();

            foreach(UnitData unit in unitDataList)
            {
                ItemSO itemSO = ScriptableObject.CreateInstance<ItemSO>();

                itemSO.id = unit.id;
                itemSO.name = unit.UnitName;
                itemSO.nameEng = unit.nameEng;
                itemSO.description = unit.description;
                
                if(System.Enum.TryParse(unit.itemTypeString, out ItemType parsedType ))
                {
                    itemSO.itemType = parsedType;
                }
                else
                {
                    Debug.LogWarning($"아이템 {unit.itemTypeString}의 유효하지 않은 타입 : {unit.itemTypeString}");
                }

                itemSO.price = unit.price;
                itemSO.power = unit.power;

                string assetPath = $"{outputFolder}/Item_{unit.id.ToString("D4")}_{unit.nameEng}.asset";
                AssetDatabase.CreateAsset(itemSO, assetPath );

                itemSO.name = $"Item_{unit.id.ToString("D4")} + {unit.nameEng}";
                createdUnits.Add( itemSO );

                EditorUtility.SetDirty( itemSO );

                if(createDatabase && createdUnits.Count > 0)
                {
                    ItemDataBaseSO dataBaseSO = ScriptableObject.CreateInstance<ItemDataBaseSO>();
                    dataBaseSO.items = createdUnits;

                    AssetDatabase.CreateAsset(dataBaseSO, $"{outputFolder}/ItemDatabase.asset");
                    EditorUtility.SetDirty(dataBaseSO);
                }

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                EditorUtility.DisplayDialog("Success", $"Successfully converted {createdUnits.Count} items to Scriptable Objects!", "OK");


            }

        }
        catch(System.Exception e)
        {
            EditorUtility.DisplayDialog("Error", $"Failed to Convert JSON : {e.Message}", "OK");
            Debug.LogError($",JSON 변환 오류 : {e}");
        }

    }
}

#endif
