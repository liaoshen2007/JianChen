using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using DataModel;
using FrameWork.JianChen.Core;

public class NPCPreviewView : View
{
    private Transform m_BG = null;
    private Text m_RoleName = null;
    private Text m_Content = null;
    private RawImage m_RoleTex = null;
    private LoopVerticalScrollRect m_TaskScrollViewList;
    private List<UserMissionVo> _userMissionVos;

    void Awake()
    {
        m_BG = transform.Find("BG").GetComponent<Transform>();
        m_RoleName = transform.Find("NPCDiaPanel/RoleNameBtm/RoleName").GetComponent<Text>();
        m_Content = transform.Find("NPCDiaPanel/Content").GetComponent<Text>();
        m_RoleTex = transform.Find("NPCDiaPanel/RoleFrame/RoleTex").GetComponent<RawImage>();
        m_TaskScrollViewList = transform.Find("NPCDiaPanel/TaskList/TaskContent").GetComponent<LoopVerticalScrollRect>();
        m_TaskScrollViewList.poolSize = 6;
        m_TaskScrollViewList.prefabName = "NPCPreview/Prefabs/TaskDialogItem";
        m_TaskScrollViewList.UpdateCallback = OnTaskListCallBack;
    }

    private void OnTaskListCallBack(GameObject gameobj, int index)
    {
        if (index>=_userMissionVos.Count||index<0)
        {
           Debug.LogError(index); 
        }
        else
        {
            if (_userMissionVos[index]==null||GlobalData.MissionData.MissionRuleDic[_userMissionVos[index].MissionId]==null)
            {
                Debug.LogError(index+""+_userMissionVos[index].MissionId);
            }
            else
            {
                gameobj.GetComponent<NPCTaskDiaItem>().SetData(_userMissionVos[index],GlobalData.MissionData.MissionRuleDic[_userMissionVos[index].MissionId]); 
            }

        }

    }


    public void SetData(NPCData npcData,List<UserMissionVo> userMissionVos)
    {
        m_RoleName.text = npcData.Name;
        m_Content.text = npcData.DefaultDialog;
        SetMissionData(userMissionVos);
    }

    private void SetMissionData(List<UserMissionVo> userMissionVos)
    {
        Debug.Log("userMissionVos.Count"+userMissionVos.Count);
        _userMissionVos = userMissionVos;
        m_TaskScrollViewList.totalCount = userMissionVos.Count;
        m_TaskScrollViewList.RefreshCells();
    }
    
}
