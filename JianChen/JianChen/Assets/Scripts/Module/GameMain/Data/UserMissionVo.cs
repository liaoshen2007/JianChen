using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataModel
{
	public enum MissionType {
		MainLine=0,
		Daily=1,
		Activity=2
	}

	public enum MissionState
	{
		StatusUnStarted=0, //未开始
		StatusUnsUnfinished=1,//未完成
		StatusUnclaimed=2,        //未领取
		StatusBeRewardedWith=3,           //已领取

	}
	
	public class UserMissionVo:IComparable<UserMissionVo>
	{
		public int UserId;//换句话说，这个UserId的作用待定。
		public int MissionId;
		public MissionType MissionType;
		public MissionState MissionState;
		public List<TaskDetail> ProgressList;
		public List<TaskDetail> FinishList;
		
		public int Progress;
		public int Finish;
		public int MissionPro;
		
		//todo 需要添加一个字段：Needtips!!

		public void UpdateMissionPro(MissionState Status)
		{
			switch (Status)
			{
				case MissionState.StatusUnclaimed:
					MissionPro = 0;
					break;
				case MissionState.StatusUnsUnfinished:
					MissionPro = 1;
					break;
				case MissionState.StatusBeRewardedWith:
					MissionPro = 2;
					break;
			}
			
		}


		public int CompareTo(UserMissionVo other)
		{
			//根据MissionStatusPB来排序！
			int result = 0;
			
			if (other.MissionPro.CompareTo(MissionPro) != 0)
			{
				result = -other.MissionPro.CompareTo(MissionPro);
			}
			else if (other.MissionId.CompareTo(MissionId)!=0)
			{
				result = other.MissionId.CompareTo(MissionId);
			}


			return result;
		}
	}
    
    
    
}