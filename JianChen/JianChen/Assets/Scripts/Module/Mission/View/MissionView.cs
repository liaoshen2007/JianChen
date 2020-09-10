using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using DataModel;
using FrameWork.JianChen.Core;
using game.main;
using UnityEngine.EventSystems;

public class MissionView : Window
{
    private Button m_CloseBtn = null;
    private Transform m_ToggleList = null;
    private Text m_Text = null;

    private LoopVerticalScrollRect _missionVerticalScrollRect;
    private List<UserMissionVo> _userMissionVos;
    
    
    void Awake()
    {
        m_CloseBtn = transform.Find("MissionPanel/CloseBtn").GetComponent<Button>();
        _missionVerticalScrollRect = transform.Find("MissionPanel/MissionList/MissionContent").GetComponent<LoopVerticalScrollRect>();
        _missionVerticalScrollRect.poolSize = 6;
        _missionVerticalScrollRect.prefabName = "Mission/Prefabs/MissionItem";
        _missionVerticalScrollRect.UpdateCallback = UserMissionDataUpdateCallBack;
        m_ToggleList = transform.Find("MissionPanel/ToggleList").GetComponent<Transform>();
        for (int i = 0; i < m_ToggleList.childCount; i++)
        {
            m_ToggleList.GetChild(i).GetComponent<Toggle>().onValueChanged.AddListener(ChangePanel);
        }
        
        
        m_Text = transform.Find("MissionPanel/Panel/Text").GetComponent<Text>();
    }

    private void ChangePanel(bool Ison)
    {
        if (Ison == false)
        {
            return;
        }
        
        string name = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log("OnTabChange===>" + name);
        
        //在这里输出不同的List
        switch (name)
        {
            case "Todo":
                //OnDailyTaskClick();
                SendMessage(new Message(MessageConst.CMD_MISSION_CHOSETASKTYPE,Message.MessageReciverType.CONTROLLER,name));
                break;
            case "Doing":
                SendMessage(new Message(MessageConst.CMD_MISSION_CHOSETASKTYPE,Message.MessageReciverType.CONTROLLER,name));
                //OnWeekTaskClick();
                break;
            case "Done":
                SendMessage(new Message(MessageConst.CMD_MISSION_CHOSETASKTYPE,Message.MessageReciverType.CONTROLLER,name));
                break;
        }
        
        
    }

    public void SetData(List<UserMissionVo> targetMissionList)
    {
        _userMissionVos = targetMissionList;
        //_missionVerticalScrollRect.RefillCells();
        _missionVerticalScrollRect.totalCount = targetMissionList.Count;
        _missionVerticalScrollRect.RefreshCells();


    }
    

    private void UserMissionDataUpdateCallBack(GameObject gameobj, int index)
    {
        gameobj.GetComponent<MissionItem>().SetData(_userMissionVos[index]);
    }

    void Start()
    {
        InitUIEvent();
    }

    private void InitUIEvent()
    {
        m_CloseBtn.onClick.AddListener(OnCloseBtnClick);
    }

    private void OnCloseBtnClick()
    {
        SendMessage(new Message(MessageConst.CMD_MISSION_CLOSE,Message.MessageReciverType.DEFAULT));
    }
    
    protected override void OpenAnimation()
    {
    }


    protected override void AddBgMask()
    {
    }
}
