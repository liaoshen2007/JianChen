using System;
using System.Collections.Generic;
using DataModel;
using FrameWork.JianChen.Core;
using UnityEngine;

public class MissionController : Controller
{

    public MissionView View;
    public List<UserMissionVo> _curMissionVos;

    public override void Start()
    {
        //todo 第一次打开的时候读取globaldata的数据。需要一个监听任务数据改变的Eventlisten!
        //todo 后续要考虑点击面板的时候直接出现目标任务的类型！参考莲藕的商城切换
        _curMissionVos = GlobalData.MissionData.UserMissionVos;

        List<UserMissionVo> targetlist=new List<UserMissionVo>();
        foreach (var vo in _curMissionVos)
        {
            //todo 之后还要加一个条件，就是前置任务要做完的才能出现在未开始任务列表里。
            if (vo.MissionState==MissionState.StatusUnStarted&&GlobalData.MissionData.MissionRuleDic[vo.MissionId].Level<=GlobalData.PlayerData.PlayerVo.Level)
            {
                targetlist.Add(vo);
            }
            
        }
        
        DefaultUserMissionList(targetlist);
        
        
    }

    private void DefaultUserMissionList(List<UserMissionVo> targetMissionVos)
    {
        View.SetData(targetMissionVos);
        
        
    }
    

    public override void OnMessage(Message message)
    {
        string name = message.Name;
        object[] body = message.Params;
        switch (name)
        {
            case MessageConst.CMD_MISSION_CHOSETASKTYPE:
                string missiontype = (string)body[0];
                Debug.Log(missiontype+" controller");
                List<UserMissionVo> targetlist=new List<UserMissionVo>();
                switch (missiontype)
                {
                    case "Todo":
                        foreach (var vo in _curMissionVos)
                        {
                            //todo 之后还要加一个条件，就是前置任务要做完的才能出现在未开始任务列表里。
                            if (vo.MissionState==MissionState.StatusUnStarted&&GlobalData.MissionData.MissionRuleDic[vo.MissionId].Level<=GlobalData.PlayerData.PlayerVo.Level)
                            {
                                targetlist.Add(vo);
                            }
            
                        }
                        DefaultUserMissionList(targetlist);
                        
                        break;
                    case "Doing":
                        foreach (var vo in _curMissionVos)
                        {
                            //todo 之后还要加一个条件，就是前置任务要做完的才能出现在未开始任务列表里。
                            if (vo.MissionState==MissionState.StatusUnsUnfinished||vo.MissionState==MissionState.StatusUnclaimed)
                            {
                                targetlist.Add(vo);
                            }
            
                        }
                        DefaultUserMissionList(targetlist);
                        break;
                    case "Done":
                        foreach (var vo in _curMissionVos)
                        {
                            //todo 之后还要加一个条件，就是前置任务要做完的才能出现在未开始任务列表里。
                            if (vo.MissionState==MissionState.StatusBeRewardedWith)
                            {
                                targetlist.Add(vo);
                            }
            
                        }
                        DefaultUserMissionList(targetlist);
                        break;
                    
                    
                    
                }
                
                
                
                break;
            
            
        }
    }

    public override void Destroy()
    {
        base.Destroy();
    }
}
