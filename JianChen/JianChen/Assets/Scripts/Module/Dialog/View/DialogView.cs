using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using FrameWork.JianChen.Core;

public class DialogView : View
{
    private Transform m_BG = null;
    private Text m_NameText = null;
    private Text m_Content = null;
    private Button m_DialogBtn = null;
    private Text _dialogBtnText;
    private int diaindex = 0;
    private List<DialogData> _dialogDatas;
    private DialogEventType curEvent = 0;
    private int curStep = 0;

    void Awake()
    {
        m_BG = transform.Find("BG").GetComponent<Transform>();
        m_NameText = transform.Find("DialogPanel/NameBg/NameText").GetComponent<Text>();
        m_Content = transform.Find("DialogPanel/Content").GetComponent<Text>();
        m_DialogBtn = transform.Find("DialogPanel/DialogBtn").GetComponent<Button>();
        _dialogBtnText = transform.Find("DialogPanel/DialogBtn/label").GetComponent<Text>();
    }

    void Start()
    {
        InitUIEvent();
    }

    private void InitUIEvent()
    {
        m_DialogBtn.onClick.AddListener(OnDialogBtnClick);
    }

    private void OnDialogBtnClick()
    {
        switch (curStep)
        {
            case 0:
                diaindex ++;
                SetDialogView(_dialogDatas[diaindex]);
                break;
            case 1:
                diaindex ++;
                SetDialogView(_dialogDatas[diaindex]);
                break;
            case 2:
                //2的时候表示对话结束，接受任务！
                Debug.Log("dialog End");
                SendMessage(new Message(MessageConst.CMD_DIALOG_RECEIVETASK));
                break;
            case 3:
                //3的时候表示对话结束，接受任务！
                Debug.Log("dialog FinishTaskState");

                SendMessage(new Message(MessageConst.CMD_DIALOG_GETREWARD));
//                diaindex ++;
//                SetDialogView(_dialogDatas[diaindex]);
                break;
            case 4:
                //4表示任务未完成。
                Debug.Log("unFinishTaskState");
                SendMessage(new Message(MessageConst.CMD_DIALOG_UNFINISHTASK));
                break;
            
            default:
                Debug.LogError("dialog End");
                break;
        }
        
        
    }

    public void SetData(List<DialogData> dialogDatas)
    {
        Debug.Log("Star dialog:"+dialogDatas.Count);
        _dialogDatas = dialogDatas;
        if (_dialogDatas.Count>0)
        {
            diaindex = 0;
            SetDialogView(_dialogDatas[diaindex]);
        }
  
    }

    public void SetChooseDialog(DialogData dialogData)
    {
        SetDialogView(dialogData);
    }
    

    private void SetDialogView(DialogData data)
    {
        m_NameText.text = data.RoleName == "0" ? "我" : data.RoleName;
        //todo 将来可以做UGUI的Dotween打字机效果，这个简单
        m_Content.text = data.DialogContent;
        curEvent = data.DialogEvent;
        curStep = data.DialogSetp;
        switch (data.DialogSetp)
        {
            case 0:
                _dialogBtnText.text = "下一步";
                break;
            case 1:
                _dialogBtnText.text = "下一步";
                break;
            case 2:
                _dialogBtnText.text = "接受任务";
                break;
            case 3:             
                _dialogBtnText.text = "完成任务";
                break;
            case 4:
                _dialogBtnText.text = "继续任务";
                break;           
            default:
                Debug.LogError("dialog End");
                break;     
        }

    }
    
}
