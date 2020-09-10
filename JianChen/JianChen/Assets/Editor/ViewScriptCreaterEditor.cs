using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ViewScriptBuilder))]
public class ViewScriptCreaterEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		ViewScriptBuilder scriptBuilder = (ViewScriptBuilder) target;
		if (GUILayout.Button("生成View脚本"))
		{
			//mapDataSave.SetMapData();
			scriptBuilder.CreatViewScripts();
            
		}
		if (GUILayout.Button("生成Module整体脚本"))
		{
			//此处是把脚本生成到Asset资源外
			//mapDataSave.SaveJsonData();
			scriptBuilder.CreatModuleScripts();
		}




	}
}
