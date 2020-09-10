using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FrameWork.JianChen.Core.Event;
#if UNITY_EDITOR && !USE_BUNDLE
using UnityEditor;
#endif
using UnityEngine;

public partial class ViewScriptBuilder : MonoBehaviour//, ISerializationCallbackReceiver
{
    static private string m_scriptsPath =""; //路径可以自定义
    static private Dictionary<string, UIType> m_interactionUIDic = new Dictionary<string, UIType>();
    static private Dictionary<string, string> m_varNameDic = new Dictionary<string, string>();
    static private readonly string m_initUIEventFunctionName = "InitUIEvent";
    static private readonly string m_varPrefix = "m_";
    static private Dictionary<UIType, string> m_EventFunctionNameDic = new Dictionary<UIType, string>();
    static private readonly string m_tabStr = "    ";
    static private Dictionary<UIType, string> m_cacheFunction = new Dictionary<UIType, string>();

    public string ModuleName;
    public List<ViewData> _ViewDatas;


    public void CreatModuleScripts()
    {
        //生成模块需要的其他所有文件！
        CreatModuleScript(ModuleName);
        CreatPanelScript(ModuleName);
        CreatControllerScript(ModuleName);
        CreateScriptsToFile(_ViewDatas,transform,ModuleName);
        
    }
    
    public void CreatViewScripts()
    {
        //todo 要判断命名是否为空啊！
        
        
        CreateScriptsToFile(_ViewDatas,transform,ModuleName);
    }

    static void CreateScriptsToFile(List<ViewData> datas,Transform root,string viewname="test")
    {
        m_scriptsPath =  PathUtil.GetProjectRoot()+ "Scripts/" + viewname+"/View";//Application.dataPath;
        CheckAndCreateDir(m_scriptsPath);
        var m_dataScriptPaht=PathUtil.GetProjectRoot()+ "Scripts/" + viewname+"/Data";//Application.dataPath;
        CheckAndCreateDir(m_dataScriptPaht);
        
        if (datas.Count == 0)
        {
            return;
        }

        m_interactionUIDic.Clear();
        m_varNameDic.Clear();
        GetUITypeSignDic(datas,root);
        InitEventFunctionNameDic();
        string scriptName = viewname+"View";
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("using UnityEngine;");
        sb.AppendLine("using UnityEngine.UI;");
        sb.AppendLine("using System;");
        sb.AppendLine("using FrameWork.JianChen.Core;");
        sb.AppendLine();
        sb.Append("public class ");
        sb.Append(scriptName);
        sb.Append(" : View");
        sb.AppendLine();
        sb.AppendLine("{");

        if (m_interactionUIDic != null && m_interactionUIDic.Count > 0)
        {
            foreach (var item in m_interactionUIDic)
            {
                string typeName = GetStrByUIType(item.Value);
                if (!string.IsNullOrEmpty(typeName))
                {
                    StringBuilder sb_variable = new StringBuilder();
                    sb_variable.Append("private ");
                    sb_variable.Append(typeName);
                    sb_variable.Append(" ");
                    sb_variable.Append(m_varPrefix); //变量名前缀
                    string name = null;
                    if (item.Key.Contains("/"))
                    {
                        int index = item.Key.LastIndexOf("/");
                        name = item.Key.Substring(index + 1);
                    }
                    else
                    {
                        name = item.Key;
                    }

                    //保存变量名，以path为key
                    m_varNameDic.Add(item.Key, m_varPrefix + name);
                    sb_variable.Append(name);
                    sb_variable.Append(" = null;");
                    sb.Append(m_tabStr);
                    sb.AppendLine(sb_variable.ToString());
                }
            }
        }

        sb.AppendLine();

        //插入Awake函数
        sb.Append(m_tabStr);
        sb.AppendLine("void Awake()");
        sb.Append(m_tabStr);
        sb.AppendLine("{");

        //插入ui变量的FindChild
        if (m_interactionUIDic != null && m_interactionUIDic.Count > 0)
        {
            foreach (var item in m_interactionUIDic)
            {
                sb.Append(m_tabStr);
                sb.Append(m_tabStr);
//                Debug.LogError(item.Key);
                sb.Append(m_varNameDic[item.Key]);
                sb.Append(" = transform.FindChild(");
                sb.Append("\"");
                sb.Append(item.Key);
                sb.Append("\").GetComponent<");
                sb.Append(GetStrByUIType(item.Value));
                sb.AppendLine(">();");
            }
        }

        sb.Append(m_tabStr);
        sb.AppendLine("}");

        //插入start函数
        sb.AppendLine();
        sb.Append(m_tabStr);
        sb.AppendLine("void Start()");
        sb.Append(m_tabStr);
        sb.AppendLine("{");

        sb.Append(m_tabStr);
        sb.Append(m_tabStr);
        sb.Append(m_initUIEventFunctionName);
        sb.AppendLine("();");
        sb.Append(m_tabStr);
        sb.AppendLine("}");

        //插入InitUIEvent函数
        sb.AppendLine();
        sb.Append(m_tabStr);
        sb.Append("private ");
        sb.Append("void ");
        sb.Append(m_initUIEventFunctionName);
        sb.AppendLine("()");
        sb.Append(m_tabStr);
        sb.AppendLine("{");

        m_cacheFunction.Clear();
        if (m_interactionUIDic != null && m_interactionUIDic.Count > 0)
        {
            foreach (var item in m_interactionUIDic)
            {
                if (m_EventFunctionNameDic.ContainsKey(item.Value))
                {
                    string functionName =
                        GetFunctionNameByTypeVarName(item.Value, m_varNameDic[item.Key].Substring(2));
                    if (null != functionName)
                    {
                        sb.Append(m_tabStr);
                        sb.Append(m_tabStr);
                        sb.Append(m_varNameDic[item.Key]);
                        sb.Append(".");

                        sb.Append(m_EventFunctionNameDic[item.Value]);
                        sb.Append(".AddListener(");
                        sb.Append(functionName);
                        sb.AppendLine(");");

                        //缓存函数名
                        m_cacheFunction.Add(item.Value, functionName);
                    }
                }
            }
        }

        sb.Append(m_tabStr);
        sb.AppendLine("}");

        //插入缓存的函数名
        if (m_cacheFunction != null && m_cacheFunction.Count > 0)
        {
            foreach (var item in m_cacheFunction)
            {
                sb.AppendLine();
                sb.Append(m_tabStr);
                sb.Append("private ");
                sb.Append("void ");
                sb.Append(item.Value);
                sb.Append("(");

                //参数
                switch (item.Key)
                {
                    case UIType.InputField:
                        sb.Append("string arg0");
                        break;
                    case UIType.ScrollRect:
                    case UIType.Toggle:
                    case UIType.Slider:
                    case UIType.Scrollbar:
                    case UIType.Dropdown:
                        sb.Append("bool arg0");
                        break;
                }

                sb.AppendLine(")");
                sb.Append(m_tabStr);
                sb.AppendLine("{");
                sb.Append(m_tabStr);
                sb.Append(m_tabStr);
                sb.AppendLine("throw new NotImplementedException();");
                sb.Append(m_tabStr);
                sb.AppendLine("}");
            }
        }

        sb.AppendLine("}");
        WriteStrToFile(sb.ToString(), m_scriptsPath, scriptName);
        Debug.LogError("ViewScript ok!");
        //刷新资源
#if UNITY_EDITOR && !USE_BUNDLE
        AssetDatabase.Refresh();
#endif
    }

    static void InitEventFunctionNameDic()
    {
        m_EventFunctionNameDic.Clear();
        m_EventFunctionNameDic.Add(UIType.Button, "onClick");
        m_EventFunctionNameDic.Add(UIType.Toggle, "onValueChanged");
        m_EventFunctionNameDic.Add(UIType.Scrollbar, "onValueChanged");
        m_EventFunctionNameDic.Add(UIType.ScrollRect, "onValueChanged");
        m_EventFunctionNameDic.Add(UIType.Slider, "onValueChanged");
        m_EventFunctionNameDic.Add(UIType.Dropdown, "onValueChanged");
        m_EventFunctionNameDic.Add(UIType.InputField, "onEndEdit");
    }

    /// <summary>
    /// 根据类别和变量名返回事件函数名
    /// </summary>
    /// <param name="type">类别</param>
    /// <param name="varName">变量名</param>
    static string GetFunctionNameByTypeVarName(UIType type, string varName)
    {
        if (string.IsNullOrEmpty(varName))
        {
            return null;
        }

        if (!m_EventFunctionNameDic.ContainsKey(type))
        {
            return null;
        }

        string eventStr = m_EventFunctionNameDic[type];
        //这里的命名规范是On + varName + eventName
        //举例：type = UIType.Button,varName = CloseBtn，结果是OnCloseBtnClick
        return "On" + varName + eventStr.Substring(2); //截掉eventStr开头的on
    }

    static void InsertFunction(StringBuilder sb, string functionName)
    {
        if (sb == null || string.IsNullOrEmpty(functionName))
        {
            return;
        }

        sb.Append(m_tabStr);
        sb.Append("private void ");
        sb.Append(functionName);
        sb.AppendLine("()");
        sb.Append(m_tabStr);
        sb.AppendLine("{");
        sb.AppendLine();
        sb.Append(m_tabStr);
        sb.AppendLine("}");
    }

    static string GetStrByUIType(UIType type)
    {
        string str = null;
        switch (type)
        {
            case UIType.Transform:
                str = "Transform";
                break;
            case UIType.Text:
                str = "Text";
                break;
            case UIType.Image:
                str = "Image";
                break;
            case UIType.RawImage:
                str = "RawImage";
                break;
            case UIType.Button:
                str = "Button";
                break;
            case UIType.Toggle:
                str = "Toggle";
                break;
            case UIType.Slider:
                str = "Slider";
                break;
            case UIType.Scrollbar:
                str = "Scrollbar";
                break;
            case UIType.Dropdown:
                str = "Dropdown";
                break;
            case UIType.InputField:
                str = "InputField";
                break;
            case UIType.ScrollRect:
                str = "ScrollRect";
                break;
        }

        return str;
    }

    static void GetUITypeSignDic(List<ViewData> viewDatas,Transform root)
    {
        if (viewDatas.Count==0)
        {
            return;
        }

        for (int i = 0; i < viewDatas.Count; i++)
        {
            m_interactionUIDic.Add(GetChildPahth(root.gameObject,viewDatas[i].ViewObj),viewDatas[i].ViewType);
        }
        
        
        
        
//        if (root == null)
//        {
//            return;
//        }
//
//        UITypeSign sign = root.GetComponent<UITypeSign>();
//        if (sign != null && sign.Type != UIType.UIRoot)
//        {
//            m_interactionUIDic.Add(path, sign);
//        }
//
//        if (root.childCount > 0)
//        {
//            for (int i = 0; i < root.childCount; i++)
//            {
//                GetUITypeSignDic(root.GetChild(i),
//                    string.IsNullOrEmpty(path) ? root.GetChild(i).name : path + "/" + root.GetChild(i).name);
//            }
//        }
//
//        return;
    }
    
    public static string GetChildPahth(GameObject obj1,GameObject obj2)
    {
        GameObject gmObj0 = obj1;
        GameObject gmObj1 = obj2;
        List<Transform> listGameParent0 = new List<Transform>(gmObj0.transform.GetComponentsInParent<Transform>(true));
        List<Transform> listGameParent1 = new List<Transform>(gmObj1.transform.GetComponentsInParent<Transform>(true));
        System.Text.StringBuilder strBd = new System.Text.StringBuilder("");
        //gmObj0.transform.FindChild("");
        //string findCode = "gmObj0"
        if (listGameParent0.Contains(gmObj1.transform))
        {
            int startIndex = listGameParent0.IndexOf(gmObj1.transform);
            Debug.Log(startIndex);
            for (int i = startIndex; i >= 0; i--)
            {
                if (i != startIndex)
                {
                    strBd.Append(listGameParent0[i].gameObject.name).Append(i != 0 ? "/" : "");
                }
                    
            }
        }
            
        if (listGameParent1.Contains(gmObj0.transform))
        {
            int startIndex = listGameParent1.IndexOf(gmObj0.transform);
            for (int i = startIndex; i >= 0; i--)
            {
                if (i != startIndex)
                {
                    strBd.Append(listGameParent1[i].gameObject.name).Append(i != 0 ? "/" : "");
                }
                    
            }
        }

//        Debug.LogError(strBd.ToString());
        return strBd.ToString();


    }

    static void WriteStrToFile(string txt, string path, string fileName)
    {
        if (string.IsNullOrEmpty(txt) || string.IsNullOrEmpty(path))
        {
            return;
        }

        File.WriteAllText(path + "/" + fileName + ".cs", txt, Encoding.UTF8);
    }

    static void CheckAndCreateDir(string dir)
    {
        if (!Directory.Exists(dir))
        {
//            Debug.LogError(dir);
            Directory.CreateDirectory(dir);
        }
    }
    
    static void AddUIType(UIType type)
    {
#if UNITY_EDITOR && !USE_BUNDLE
        if (Selection.gameObjects != null && Selection.gameObjects.Length >= 1)
        {
            int count = Selection.gameObjects.Length;
            for (int i = 0; i < count; i++)
            {
                GameObject go = Selection.gameObjects[i];
                UITypeSign sign = go.AddComponent<UITypeSign>();
                switch (type)
                {
//                    case UIType.UIRoot:
//                        sign.Type = UIType.UIRoot;
//                        break;
                    case UIType.Transform:
                        sign.Type = UIType.Transform;
                        break;
                    case UIType.Text:
                        sign.Type = UIType.Text;
                        break;
                    case UIType.Image:
                        sign.Type = UIType.Image;
                        break;
                    case UIType.RawImage:
                        sign.Type = UIType.RawImage;
                        break;
                    case UIType.Button:
                        sign.Type = UIType.Button;
                        break;
                    case UIType.Toggle:
                        sign.Type = UIType.Toggle;
                        break;
                    case UIType.Slider:
                        sign.Type = UIType.Slider;
                        break;
                    case UIType.Scrollbar:
                        sign.Type = UIType.Scrollbar;
                        break;
                    case UIType.Dropdown:
                        sign.Type = UIType.Dropdown;
                        break;
                    case UIType.InputField:
                        sign.Type = UIType.InputField;
                        break;
                    case UIType.ScrollRect:
                        sign.Type = UIType.ScrollRect;
                        break;
                    default:
                        break;
                }
            }
        }
#endif
    }

}

[Serializable]
public class ViewData
{
    public string ObjName;

//    [SerializeField, SetProperty("ViewType")]
//    private string _objName;

    public GameObject ViewObj;
//    {
//        get { return _viewObj; }
//        private set
//        {
//            ObjName = _viewObj.name;
//        }
//        
//    }

//    [SerializeField, SetProperty("ViewObj")]
//    private GameObject _viewObj;


    public UIType ViewType;
//    {
//        get { return _viewType; }
//        set
//        {
//            _viewType = value;
//            Debug.LogError("viewt");
//        }
//    }
//
//    [SerializeField, SetProperty("ViewType")]
//    private UIType _viewType;
}



//那个脚本只能用一个？！
[Serializable]
public class testData
{

    public enum ThisEnum
    {
        good,
        bad
        
    }


    public ThisEnum ViewType
    {
        get { return _viewType; }
        set
        {
            _viewType = value;
            Debug.LogError("viewt");
        }
    }

    [SerializeField, SetProperty("ViewType")]
    private ThisEnum _viewType;
}


