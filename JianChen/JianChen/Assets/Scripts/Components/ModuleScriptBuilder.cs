using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public partial class ViewScriptBuilder : MonoBehaviour {

    static private string m_modulescriptsPath =""; //路径可以自定义


    static void CreatModuleScript(string ModuleName)
    {
        m_modulescriptsPath = PathUtil.GetProjectRoot()+ "Scripts/" + ModuleName; 
        CheckAndCreateDir(m_modulescriptsPath);
        string moduleName = ModuleName + "Module";
        string panelClass = ModuleName + "Panel";
        string panelName = "_" + ModuleName.ToLower() + "panel";
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("using System;");
        sb.AppendLine("using FrameWork.JianChen.Core;");
        sb.AppendLine();
        sb.Append("public class ");
        sb.Append(moduleName);
        sb.Append(" : ModuleBase");
        sb.AppendLine();
        sb.AppendLine("{");
        sb.Append(m_tabStr);
        sb.AppendLine(panelClass+" "+panelName+";");
        sb.AppendLine();
        sb.Append(m_tabStr);
        sb.AppendLine("public override void Init()");
        sb.Append(m_tabStr);
        sb.AppendLine("{");
        sb.Append(m_tabStr);
        sb.Append(m_tabStr);
        sb.AppendLine(panelName+" = new "+panelClass+"();");
        sb.Append(m_tabStr);
        sb.Append(m_tabStr);
        sb.AppendLine(panelName+".Init(this);");
        sb.Append(m_tabStr);
        sb.Append(m_tabStr);
        sb.AppendLine(panelName+".Show(0);");
        sb.Append(m_tabStr);
        sb.AppendLine("}");
        sb.AppendLine();
        sb.Append(m_tabStr);
        sb.AppendLine("public override void OnShow(float delay)");
        sb.Append(m_tabStr);
        sb.AppendLine("{");
        sb.Append(m_tabStr);
        sb.Append(m_tabStr);
        sb.AppendLine("base.OnShow(delay);");
        sb.Append(m_tabStr);
        sb.Append(m_tabStr);
        sb.AppendLine(panelName+".Show(0);");
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
        
        
        
        sb.AppendLine("}");
        
        Debug.LogError("ModuleScript ok!");
        WriteStrToFile(sb.ToString(), m_modulescriptsPath, moduleName);


    }
    
    
    
}
