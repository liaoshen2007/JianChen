using System;
using System.Collections.Generic;
using System.IO;
using game.main;
using LitJson;
using UnityEngine;

public class DialogDataBuilder : MonoBehaviour
{
    //这个类同时要兼顾运行对话框查看对话脚本的职责！
    
    public string DialogId;
    public List<DialogData> DialogDataList;
    private DialogView _dialogView;

    private void Awake()
    {
        //这个只有在运行时才会有作用的！
        _dialogView = transform.GetComponent<DialogView>();
        Debug.Log("Why:"+DialogDataList?.Count);
    }

    /// <summary>
    /// unity编辑器一级BUG！！！你在编辑器填完对话的时候，必须要Apply一下才会运行的时候生效！！
    /// </summary>
    private void Start()
    {
        //Debug.Log("Why:"+DialogDataList?.Count);
        var dialogLost = DialogDataList;
        
        _dialogView.SetData(dialogLost);
    }


    //保存json!
    public void SaveDialogData()
    {
        if (DialogDataList==null)
        {
            DialogDataList=new List<DialogData>();
        }
        string jsondata = JsonMapper.ToJson(DialogDataList);
        string path = AssetLoader.GetDialogDataPath(DialogId);
        StreamWriter sw = new StreamWriter(path);
        sw.Write(jsondata);
        sw.Close();
        sw.Dispose();

        Debug.LogError("save success " + DialogId);
        
    }

    //读取json！
    public void ReadDialogData()
    {
        ConfigDataManager.LoadDialogDataById<DialogData>(DialogId, data =>
        {
            Debug.LogError(data.Count);
            DialogDataList = data;
        });
        
    }
    
    
}

