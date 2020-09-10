using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapDataBuilder))]
public class MapDataEditor : Editor {
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        MapDataBuilder mapDataSave = (MapDataBuilder) target;
        if (GUILayout.Button("获取地图编辑数据"))
        {
            mapDataSave.SetMapData();
            
        }
        if (GUILayout.Button("保存地图数据"))
        {
            mapDataSave.SaveJsonData();
        }

        if (GUILayout.Button("读取地图数据"))
        {
            mapDataSave.ReadJsonData();
        }

    }
}
