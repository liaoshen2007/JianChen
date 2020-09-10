using System;
using System.Collections.Generic;
using Common;
using DataModel;
using FrameWork.JianChen.Core;
using FrameWork.JianChen.Core.Event;
using Module;
using UnityEngine;

public class NPCPreviewController : Controller
{
    public NPCPreviewView View;
    public NPCData NpcData;

    public override void Start()
    {
        //筛选出NPC的对话数据！
        FindMissionData();
        EventDispatcher.AddEventListener<int,int>(EventConst.ChooseTask,StartDialogView);
        //EventDispatcher.AddEventListener<int>(EventConst.GetTaskReward,GetTaskReward);
        EventDispatcher.AddEventListener(EventConst.RefreshTaskState,FindMissionData);
    }

//    private void GetTaskReward(int missionId)
//    {
//        Debug.Log("Rewarddialog:"+missionId);
//        //后面传参1表示完成了任务！
//        ModuleManager.Instance.EnterModule(ModuleConfig.MODULE_DIALOG,true,true,missionId,NpcData,2);
   
//    }

    private void StartDialogView(int missionId,int dialogstate)
    {
        Debug.Log("Startdialog:"+missionId);
        ModuleManager.Instance.EnterModule(ModuleConfig.MODULE_DIALOG,true,true,missionId,NpcData,dialogstate);
    }

    //todo 将来如果有需求做一个任务列表的话，其实没必要初始化就把所有任务都加到UserMissionVo去，可以通过对比两张表来确定未进行的任务！
    private void FindMissionData()
    {
        List<UserMissionVo> targetMissionVos = new List<UserMissionVo>();
        foreach (var a in NpcData.TaskIdList)
        {
            int id = Convert.ToInt32(a);
            if (GlobalData.MissionData.MissionRuleDic.ContainsKey(id))
            {
                UserMissionVo vo = GlobalData.MissionData.UserMissionVos.Find(x => x.MissionId == id);
                if (vo != null)
                {
                    //如果任务是还属于已完成未领取状态，那么还是要显示的
                    if (vo.MissionState != MissionState.StatusBeRewardedWith&&vo.MissionState!=MissionState.StatusUnclaimed)
                    {
                        targetMissionVos.Add(vo);
                    }

                }
                else
                {
                    Debug.Log("new userMissionVo");
                    //todo 之后要限制等级，判断等级是否到达，并且是否满足主线前置的前提下才可以出现该NPC的任务！
                    
                    //在这里入手，把MissionRule的Target分解成User里的可读数据！！
                    var missionrule = GlobalData.MissionData.MissionRuleDic[id];
                    var taskdetailarr = GlobalData.MissionData.ParseData(missionrule.TargetDetail);
                    var finishtargetlist=new List<TaskDetail>();
                    var inittargetlist=new List<TaskDetail>();
                    foreach (var v in taskdetailarr)
                    {
                        string[] taskinfoarr = v.Split(',');
                        int detailtype =Convert.ToInt32(taskinfoarr[0]);
                        int detailId=Convert.ToInt32(taskinfoarr[1]);
                        int detailnum=Convert.ToInt32(taskinfoarr[2]);
                        TaskDetail taskDetail=new TaskDetail()
                        {
                            TaskDetailtype = detailtype,
                            DetailId = detailId,
                            TargetNum = detailnum,
                            
                        };
                        
                        TaskDetail initDetail=new TaskDetail()
                        {
                            TaskDetailtype = detailtype,
                            DetailId = detailId,
                            TargetNum = 0,
                            
                        };

                        Debug.Log(detailtype+" "+detailId+" "+detailnum );
                        
                        finishtargetlist.Add(taskDetail);
                        inittargetlist.Add(initDetail);
                        
                    }
                    //ParseData(missionrule.TargetDetail);
                    vo = new UserMissionVo()
                    {
                        UserId = GlobalData.PlayerData.PlayerVo.ID,
                        MissionId = id,
                        MissionState = MissionState.StatusUnStarted,
                        MissionType = (MissionType) missionrule.MissionType,
                        ProgressList = inittargetlist,
                        FinishList = finishtargetlist,
                        Progress = 0,
                        Finish = missionrule.Progress,//finish 担任任务数量节点！！

                    };
                    //分割任务目标的字符串！！

                    
                    
                    
                    vo.UpdateMissionPro(vo.MissionState);
                    GlobalData.MissionData.AddUserMissionVo(vo);
                    targetMissionVos.Add(vo);
                }
            }
        }

        //todo List后面要加上可领取奖励的该NPC任务！！
        var cangetRewardlist = GlobalData.MissionData.GetFinishTashList(NpcData.ID);
        if (cangetRewardlist!=null)
        {
            foreach (var v in cangetRewardlist)
            {
                targetMissionVos.Add(v); 
            }

        }
        
        
        
        SetData(targetMissionVos);
    }


    

    private void SetData(List<UserMissionVo> userMissionVos)
    {
        View.SetData(NpcData,userMissionVos);
    }

    public override void OnMessage(Message message)
    {
        string name = message.Name;
        object[] body = message.Params;
        switch (name)
        {
        }
    }

    public override void Destroy()
    {
        EventDispatcher.RemoveEventListener<int,int>(EventConst.ChooseTask,StartDialogView);
        //EventDispatcher.RemoveEventListener<int>(EventConst.GetTaskReward,GetTaskReward);
        EventDispatcher.RemoveEventListener(EventConst.RefreshTaskState,FindMissionData);
        base.Destroy();
    }
}