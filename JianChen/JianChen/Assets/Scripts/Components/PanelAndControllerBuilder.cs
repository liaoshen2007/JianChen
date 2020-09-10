using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public partial class ViewScriptBuilder : MonoBehaviour {

	static private string m_panelscriptsPath =""; //路径可以自定义
	static private string m_controllerscriptsPath =""; //路径可以自定义


	static void CreatPanelScript(string ModuleName)
	{
		m_panelscriptsPath =PathUtil.GetProjectRoot()+ "Scripts/" + ModuleName+"/View"; 
		CheckAndCreateDir(m_panelscriptsPath);
		string moduleName = ModuleName + "Module";
		string panelClass = ModuleName + "Panel";
		string controllerClass = ModuleName + "Controller";
		string controllername="_"+moduleName.ToLower() + "Controller";
		string viewName = ModuleName + "View";
		StringBuilder sb = new StringBuilder();
		sb.AppendLine("using System;");
		sb.AppendLine("using FrameWork.JianChen.Core;");
		sb.AppendLine("using Assets.Scripts.Common;");
		sb.AppendLine("using FrameWork.JianChen.Interfaces;");
		sb.AppendLine();
		sb.Append("public class ");
		sb.Append(panelClass);
		sb.Append(" : ReturnablePanel");
		sb.AppendLine();
		sb.AppendLine("{");
		sb.Append(m_tabStr);
		sb.AppendLine(controllerClass+" "+controllername+";");
		sb.AppendLine();
		sb.Append(m_tabStr);
		sb.AppendLine("public override void Init(IModule module)");
		sb.Append(m_tabStr);
		sb.AppendLine("{");
		sb.Append(m_tabStr);
		sb.Append(m_tabStr);
		sb.AppendLine("base.Init(module);");
		sb.Append(m_tabStr);
		sb.Append(m_tabStr);
		sb.AppendLine($"var viewScript = ({viewName})InstantiateView<{viewName}>(\"{ModuleName}/Prefabs/{viewName}\");");
		sb.Append(m_tabStr);
		sb.Append(m_tabStr);
		sb.AppendLine(controllername+" = new "+controllerClass+"();");
		sb.Append(m_tabStr);
		sb.Append(m_tabStr);
		sb.AppendLine(controllername+".View"+" = "+"viewScript;");
		sb.Append(m_tabStr);
		sb.Append(m_tabStr);
		sb.AppendLine("RegisterView(viewScript);");
		sb.Append(m_tabStr);
		sb.Append(m_tabStr);
		sb.AppendLine($"RegisterController({controllername});");
		sb.Append(m_tabStr);
		sb.Append(m_tabStr);
		sb.AppendLine(controllername+".Start();");
		sb.Append(m_tabStr);
		sb.AppendLine("}");
        
		sb.AppendLine();
		sb.Append(m_tabStr);
		sb.AppendLine("public override void Show(float delay)");
		sb.Append(m_tabStr);
		sb.AppendLine("{");
		sb.Append(m_tabStr);
		sb.Append(m_tabStr);
		sb.AppendLine("base.Show(delay);");
		sb.Append(m_tabStr);
		sb.Append(m_tabStr);
		sb.AppendLine("Main.ChangeMenu(MainUIDisplayState.ShowTopBar);");
		sb.Append(m_tabStr);
		sb.Append(m_tabStr);
		sb.AppendLine("ShowBackBtn();");
		sb.Append(m_tabStr);
		sb.AppendLine("}");
		sb.AppendLine();
		sb.Append(m_tabStr);
		sb.AppendLine("public override void Hide()");
		sb.Append(m_tabStr);
		sb.AppendLine("{");
		sb.Append(m_tabStr);
		sb.Append(m_tabStr);
		sb.AppendLine("base.Hide();");
		sb.Append(m_tabStr);
		sb.AppendLine("}");
        
        
        
		sb.AppendLine("}");
        
		Debug.LogError("PanelScript ok!");
		WriteStrToFile(sb.ToString(), m_panelscriptsPath, panelClass);

	}
	
	static void CreatControllerScript(string ModuleName)
	{
		m_controllerscriptsPath =PathUtil.GetProjectRoot()+ "Scripts/" + ModuleName+"/Controller"; 
		CheckAndCreateDir(m_controllerscriptsPath);
		string controllerClass = ModuleName + "Controller";
		string viewClass = ModuleName + "View";
		
		StringBuilder sb = new StringBuilder();
		sb.AppendLine("using System;");
		sb.AppendLine("using FrameWork.JianChen.Core;");
		sb.AppendLine();
		sb.Append("public class ");
		sb.Append(controllerClass);
		sb.Append(" : Controller");
		sb.AppendLine();
		sb.AppendLine("{");
		sb.AppendLine();
		sb.Append(m_tabStr);
		sb.AppendLine("public " +viewClass+" View;");

		sb.AppendLine();
		sb.Append(m_tabStr);
		sb.AppendLine("public override void Start()");
		sb.Append(m_tabStr);
		sb.AppendLine("{");
		sb.AppendLine();
		sb.Append(m_tabStr);
		sb.AppendLine("}");
        
		sb.AppendLine();
		sb.Append(m_tabStr);
		sb.AppendLine("public override void OnMessage(Message message)");
		sb.Append(m_tabStr);
		sb.AppendLine("{");
		sb.Append(m_tabStr);
		sb.Append(m_tabStr);
		sb.AppendLine("string name = message.Name;");
		sb.Append(m_tabStr);
		sb.Append(m_tabStr);
		sb.AppendLine("object[] body = message.Params;");
		sb.Append(m_tabStr);
		sb.Append(m_tabStr);
		sb.AppendLine("switch (name)");
		sb.Append(m_tabStr);
		sb.Append(m_tabStr);
		sb.AppendLine("{");
		sb.AppendLine();
		sb.Append(m_tabStr);
		sb.Append(m_tabStr);
		sb.AppendLine("}");
		sb.Append(m_tabStr);
		sb.AppendLine("}");
		
		
		sb.AppendLine();
		sb.Append(m_tabStr);
		sb.AppendLine("public override void Destroy()");
		sb.Append(m_tabStr);
		sb.AppendLine("{");
		sb.Append(m_tabStr);
		sb.Append(m_tabStr);
		sb.AppendLine("base.Destroy();");
		sb.Append(m_tabStr);
		sb.AppendLine("}");
        
        
        
		sb.AppendLine("}");
        
		Debug.LogError("ControllerScript ok!");
		WriteStrToFile(sb.ToString(), m_controllerscriptsPath, controllerClass);

	}
    
    
    
}
