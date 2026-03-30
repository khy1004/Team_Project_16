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
                itemSO.name = unit.name;
                itemSO.nameEng = unit.nameEng;
                itemSO.description = unit.description;
                
                if(System.Enum.TryParse(UnitData.itemTypeString ))
                
                itemSO.price = unit.price;
                itemSO.power = unit.power;



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
