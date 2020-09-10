using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogDataBuilder))]
public class DialogDataEditor : Editor {
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		DialogDataBuilder dialogDataSave = (DialogDataBuilder) target;
		if (GUILayout.Button("读取对话数据"))
		{
			dialogDataSave.ReadDialogData();
            
		}
		if (GUILayout.Button("保存对话数据"))
		{
			dialogDataSave.SaveDialogData();
		}




	}
}
