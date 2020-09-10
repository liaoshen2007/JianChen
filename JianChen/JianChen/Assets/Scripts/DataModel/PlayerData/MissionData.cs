using System;
using System.Collections.Generic;
using Common;
using DataModel;
using FrameWork.JianChen.Core;
using FrameWork.JianChen.Core.Event;
using game.main;
using UnityEngine;

namespace JianChen.Data
{
	public class MissionData : Model
	{
		//这里可以写玩家任务数据相关的方法！
		public List<UserMissionVo> UserMissionVos;
		public Dictionary<int, MissionRule> MissionRuleDic;
		public TaskTrigger TaskStateTrigger;

		public MissionData()
		{
			UserMissionVos =new List<UserMissionVo>();
			MissionRuleDic=new Dictionary<int, MissionRule>();
			TaskStateTrigger=new TaskTrigger();
		}

		
		//不去动它的结构，但是赋值入口来自PlayerVo
		public void InitUserMissionData(List<UserMissionVo> data)
		{
			UserMissionVos.Clear();
			foreach (var vo in data)
			{
				//Debug.Log(vo.MissionId);
				UserMissionVos.Add(vo);
			}
			
			//初始化的时候记得就要调用TaskTrigger
			
			List<UserMissionVo> needtriggerlist=new List<UserMissionVo>();
			
			foreach (var v in UserMissionVos)
			{
				if (v.MissionState==MissionState.StatusUnsUnfinished)
				{
					needtriggerlist.Add(v);
				}
			}
			
			TaskStateTrigger.InitTaskTrigger(needtriggerlist);
			TaskStateTrigger.FinfishTask = FinisheMissionData;
			TaskStateTrigger.UpdateUserMission = UpdateUserMissionData;





		}


		public void InitMissionRule(List<MissionRule> data)
		{
			foreach (var v in data)
			{
				MissionRuleDic.Add(v.MissionId,v);
				//Debug.Log(v.MissionName);
			}
			
		}

		public void AddUserMissionVo(UserMissionVo data)
		{
			if (UserMissionVos != null)
			{
				if (!UserMissionVos.Contains(data))
				{
					UserMissionVos.Add(data);
					GlobalData.PlayerData.PlayerVo.UpdateMission(UserMissionVos);
				}
				//通知PlayerVo层需要改变
			}
			else
			{
				Debug.LogError("UserMissionVos Error");
			}

			
			
			
		}

		/// <summary>
		/// 获取目标NPC的任务完成Item
		/// </summary>
		/// <param name="npcid"></param>
		/// <returns></returns>
		public List<UserMissionVo> GetFinishTashList(int npcid)
		{
			List<UserMissionVo> finishTaskVos=new List<UserMissionVo>();
			foreach (var vo in UserMissionVos)
			{
				if (vo.MissionState==MissionState.StatusUnclaimed)
				{
					var missionrule = MissionRuleDic[vo.MissionId];
					if (missionrule.GoalNPC==npcid)
					{
						finishTaskVos.Add(vo);
						
					}
				}
				
			}

			return finishTaskVos.Count > 0 ? finishTaskVos : null;



		}
		

		

		public void ReceiveMissionData(int curmissionId)
		{
			foreach (var v in UserMissionVos)
			{
				if (v.MissionId == curmissionId)
				{
					//未领取状态。
					v.MissionState = MissionState.StatusUnsUnfinished;
					TaskStateTrigger.UpdateTaskTrigger(v);
					Debug.Log("完成任务："+MissionRuleDic[v.MissionId].MissionName);
					FlowText.ShowMessage("完成任务："+MissionRuleDic[v.MissionId].MissionName);
				}
				
				
			}
			

		}

		//这个应该由任务触发器来调用！
		public void FinisheMissionData(UserMissionVo data)
		{
			//改变data的任务状态就可以了。
			foreach (var vo in UserMissionVos)
			{
				//完成任务后，状态变为未领取。
				if (vo.MissionId==data.MissionId)
				{
					vo.MissionState = MissionState.StatusUnclaimed;
					
					//todo 测试检查是否其他数据也需要变动
					//todo 需要通知NPC改变头顶灯泡状态,还有通知可领奖状态的NPC。


					RefreshTaskInfo();
				}


			}
			

		}

		//获取奖励的又是另外的事情。
		public void GetTaskReward(int curmissionid)
		{

			foreach (var vo in UserMissionVos)
			{
				if (vo.MissionId==curmissionid)
				{
					vo.MissionState = MissionState.StatusBeRewardedWith;
					//todo 需要给GameMain发送一个消息，弹出奖励弹窗。

					var missionrule = MissionRuleDic[vo.MissionId];
					var awardarr = ParseData(missionrule.Award);
					List<AwardData> curawardlist=new List<AwardData>();
					foreach (var v in awardarr)
					{
						string[] awarddataarr = v.Split(',');
						int resourceid = Convert.ToInt32(awarddataarr[0]);
						ResourceType resourcetype = (ResourceType) (Convert.ToInt32(awarddataarr[1]));
						int resourcenum = Convert.ToInt32(awarddataarr[2]);
						AwardData awardData = new AwardData()
						{
							ResourceId = resourceid,
							ResourceType = resourcetype,
							Num = resourcenum
							
						};
						curawardlist.Add(awardData);

					}
					
					EventDispatcher.TriggerEvent(EventConst.ShowAwardWindow,curawardlist);
					
					
					RefreshTaskInfo();
				}

			}
			

		}
		
		public string[] ParseData(string text)
		{
			string[] arr = text.Split(';');
			if (arr.Length >= 1)
			{
				Debug.Log(arr[0]);
			}
			else
			{
				Debug.Log(text+" "+arr.Length );
			}

			return arr;



		}
		
		
		
		//接受任务，完成任务等要调度的地方！
		public void UpdateUserMissionData(UserMissionVo data)
		{
			foreach (var vo in UserMissionVos)
			{
				//Debug.Log(vo.MissionId);
				if (vo.MissionId == data.MissionId)
				{
					//vo = data;
					vo.MissionType = data.MissionType;
					vo.MissionPro = data.MissionPro;
					vo.Progress = data.Progress;
					vo.Finish = data.Finish;
					vo.MissionState = data.MissionState;
					vo.UserId = data.UserId;
					vo.ProgressList = data.ProgressList;
					vo.FinishList = data.FinishList;
				}
				//UserMissionVos.Add(vo);
			}		
			
			
			RefreshTaskInfo();
		}

		//todo 任务数据只要有变更，就要通过此函数通知所有相关UI或者数据等等！
		private void RefreshTaskInfo()
		{
			GlobalData.PlayerData.PlayerVo.UpdateMission(UserMissionVos);
			
			//bug 任务数据改变要立刻通知相关UI的改变！
			EventDispatcher.TriggerEvent(EventConst.RefreshTaskState);
			
			
		}
		
		

	}

}


