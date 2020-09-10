using System;
using System.Collections.Generic;
using DataModel;
using FrameWork.JianChen.Core;
using UnityEngine;

namespace JianChen.Data
{
    public class TaskTrigger : Model
    {

        //初始化，把UserMissionvo里未完成的正在进行中的任务传参到此类。
        //根据任务细节类型进行分类，但是最好把数据放到一个字典里去。
        //做一个通用的任务细节触发器，根据TaskDetail里面的TaskType来进行分类。
        //在可能触发任务的地方加上触发器的类型调用。//比如怪物死亡，或者金币增加或者收集物品等等，任务触发器的测试是非常复杂的，得多测试。

        private List<UserMissionVo> _triggerTaskList;
        public Action<UserMissionVo> UpdateUserMission;
        public Action<UserMissionVo> FinfishTask;
        
        public TaskTrigger()
        {
            _triggerTaskList=new List<UserMissionVo>();
        }
        

        public void InitTaskTrigger(List<UserMissionVo> data)
        {
            _triggerTaskList = data;
            Debug.Log("cur task count:"+_triggerTaskList.Count);
        }

        //接受了新的任务或者完成了新的任务
        public void UpdateTaskTrigger(UserMissionVo newTask)
        {
            if (!_triggerTaskList.Contains(newTask))
            {
                _triggerTaskList.Add(newTask);
                Debug.Log("newtask:"+newTask.MissionId);
            }
 
            //todo 这里就要根据newtask的任务细节来判断是否为对话要直接trytasktrigger
            
        }

        //todo 完成任务之后要记得清除任务。
        public void RemoveFinishTask(UserMissionVo finishtask)
        {

            _triggerTaskList.Remove(finishtask);
            FinfishTask(finishtask);
        }
        

        //在可能会调用任务触发器的地方调用此函数。比如对话类型要传NPCID，打猎要传怪物ID等等。
        public void TryTaskTrigger(TaskDetailType taskDetailType,int detailId,int num)
        {
            if (_triggerTaskList.Count>0)
            {
                //我感觉应该要优化成字典！
                List<UserMissionVo> finishTask=new List<UserMissionVo>();
                
                foreach (var v in _triggerTaskList)
                {
                    if (v.FinishList.Count>0)
                    {
                        
                        //先判断是否可以增加目标任务的进度
                        //todo 这里的算法应该可以优化
                        for (int i = 0; i < v.FinishList.Count; i++)
                        {
                            if (v.FinishList[i].TaskDetailtype==(int)taskDetailType&&v.FinishList[i].DetailId==detailId)
                            {
                                v.ProgressList[i].TargetNum += num;//增加进度值                           
                            }   
                        }

                        //如果已经满足目标进度了，那么就直接设置为任务完成。
                        int finishsingletaskCount = 0;
                        for (int i = 0; i < v.ProgressList.Count; i++)
                        {
                            if (v.ProgressList[i].TaskDetailtype==v.FinishList[i].TaskDetailtype&&v.ProgressList[i].TargetNum>=v.FinishList[i].TargetNum)
                            {
                                finishsingletaskCount += 1;
                                //todo 这个将来可以触发单条任务变蓝色。
                            }

                        }

                        UpdateUserMission(v);
                        //代表所有任务完成了。
                        if (finishsingletaskCount==v.FinishList.Count)
                        {
                            //触发MissionData的任务完成！
                            finishTask.Add(v);
                        }

                    }
                    
                    //记得要同步到MissionData和PlayerData.



                }

                if (finishTask.Count>0)
                {
                    foreach (var v in finishTask)
                    {
                        RemoveFinishTask(v);
                    }
                    
                    
                    
                }
                
                
                
            }
            else
            {
                Debug.Log("cur task count:"+_triggerTaskList.Count);
            }
            
            
            
            
        }
        
        
    }

}

public enum TaskDetailType
{
    Dialog=0,//对话
    Hunting=1,//打猎
    Collect=2,//收集
    Arrive=3,//到达目的地
	
}
