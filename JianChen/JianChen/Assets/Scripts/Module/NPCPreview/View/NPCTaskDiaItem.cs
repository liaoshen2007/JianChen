using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using DataModel;
using FrameWork.JianChen.Core.Event;
using UnityEngine;
using UnityEngine.UI;

public class NPCTaskDiaItem : MonoBehaviour
{

    private Text _taskItem;
    private Button _taskButton;
    private UserMissionVo _userMissionVo;

    private void Awake()
    {
        _taskItem = transform.GetComponent<Text>();
        _taskButton = transform.GetComponent<Button>();
        _taskButton.onClick.AddListener(OnTaskClick);
    }

    private void OnTaskClick()
    {


        switch (_userMissionVo.MissionState)
        {
            case  MissionState.StatusUnStarted:
                //如果是未开始，就开始显示对话！
                Debug.Log("start dialogSystem!");
                EventDispatcher.TriggerEvent(EventConst.ChooseTask,_userMissionVo.MissionId,0);
                break;
            case MissionState.StatusUnsUnfinished:
                Debug.Log("unfinish!");
                //todo 未完成任务的时候，提示完成任务的文案，并且继续判断是否需要对话完成任务的东西没有执行？
                EventDispatcher.TriggerEvent(EventConst.ChooseTask,_userMissionVo.MissionId,4);
                break;
            
            case MissionState.StatusUnclaimed:
                Debug.Log("get award!");
                //todo 本应该是继续进入DialogController的对话，然后直接读取完成任务时的状态的对话，测试期间，先直接打开奖励弹窗。
                EventDispatcher.TriggerEvent(EventConst.ChooseTask,_userMissionVo.MissionId,3);
                break;
            default:
                Debug.LogError("_userMissionVo.MissionId"+_userMissionVo.MissionId+"state:"+_userMissionVo.MissionState);
                break;
            
            
        }
        
        

        
        
    }

    public void SetData(UserMissionVo vo,MissionRule rule)
    {
        _userMissionVo = vo;
        switch (vo.MissionState)
        {
            case MissionState.StatusUnStarted:
                _taskItem.text = rule.MissionName+"(未开始)";
                break;
            case MissionState.StatusUnsUnfinished:
                _taskItem.text = rule.MissionName+"(未完成)";
                break;
            case MissionState.StatusUnclaimed:
                _taskItem.text = rule.MissionName+"(可领取)";
                break;
            case MissionState.StatusBeRewardedWith:
                _taskItem.text = rule.MissionName+"(已领取)";
                break;
            default:
                Debug.Log("New label:"+vo.MissionState);
                break;
        }
        
        
        

    }
    
}
