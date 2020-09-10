using System.Collections.Generic;
using Common;
using DataModel;
using FrameWork.JianChen.Core;
using FrameWork.JianChen.Core.Event;
using UnityEngine;

public class DialogController : Controller
{

    public DialogView View;
    public int CurMissionId;
    public NPCData CurNpcData;
    public int CurDialogstate;

    public override void Start()
    {
        GetTargetDialog();
    }

    private void GetTargetDialog()
    {
        ConfigDataManager.LoadDialogDataById<DialogData>(CurMissionId.ToString(), data =>
        {
            switch (CurDialogstate)
            {
                case 0:
                    SetDialogData(data);
                    break;
                case 3:
                    //完成任务的状态
                    foreach (var v in data)
                    {
                        if (v.DialogSetp==3)
                        {
                            View.SetChooseDialog(v);   
                        }
                    }               
                    break;
                case 4:
                    //任务未完成的时候
                    //bug 没有step==4的时候，应该要有容错处理。这个置后去做，!!!!原则上每个任务都有继续任务的对话，包括对话任务。出现这个BUG属于配表失误。
                    foreach (var v in data)
                    {
                        if (v.DialogSetp==4)
                        {
                            View.SetChooseDialog(v);   
                        }
                    }
                    
                    
                    break;
                default:
                    SetDialogData(data);
                    break;
                
            }
            

        });
    }
    
    private void SetDialogData(List<DialogData> dialogDatas)
    {
        View.SetData(dialogDatas);
    }

    public override void OnMessage(Message message)
    {
        string name = message.Name;
        object[] body = message.Params;
        switch (name)
        {
              case MessageConst.CMD_DIALOG_RECEIVETASK:
                  //接受任务后，通知任务数据层保存好当前数据任务，并且对任务数据有所改动。这是重中之重！
                  
                  //todo 如果是对话任务，则在MissionData的类里直接设置为已经完成！
                  //并且接收完任务对话，按道理就是要触发另外一个对话或者任务，所以，这必须要有Trigger.
                  
                  Debug.LogError("Reveice task:"+CurMissionId);
                  GlobalData.MissionData.ReceiveMissionData(CurMissionId);
                  //todo 在此处尝试调用TryTaskTrigger,缺一个参数，当前与之对话的NPC！
                  
                  GlobalData.MissionData.TaskStateTrigger.TryTaskTrigger(TaskDetailType.Dialog,CurNpcData.ID,1);
                  
                  
                  ModuleManager.Instance.GoBack();
                  break;
              case MessageConst.CMD_DIALOG_UNFINISHTASK:
                  
                  //todo 未完成任务的时候，需要另外的对话！
                  //bug 优化未完成任务时候的提示。
                  GlobalData.MissionData.TaskStateTrigger.TryTaskTrigger(TaskDetailType.Dialog,CurNpcData.ID,1);              
                  ModuleManager.Instance.GoBack();
                  break;
              
              case MessageConst.CMD_DIALOG_GETREWARD:
                  //之后要弹出NPC恭喜自己完成任务的对话，但是现在先测试数据闭环和任务完成后领奖的状态。
                  //领奖成功后，就要后退。
                  GlobalData.MissionData.GetTaskReward(CurMissionId);
                  ModuleManager.Instance.GoBack();
                  break;
        }
    }

    public override void Destroy()
    {
        EventDispatcher.TriggerEvent(EventConst.SetCameState,FollowMainRole.CamState.FollowMainRole);
        base.Destroy();
    }
}
