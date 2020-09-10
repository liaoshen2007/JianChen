using System.Collections.Generic;
using Common;
using DataModel;
using FrameWork.JianChen.Core;
using FrameWork.JianChen.Core.Event;
using game.main;
using Module;
using UnityEngine;

public class GameMainController : Controller
{


	public GameMainView view;
	private StatusWindow _statusWindow;
	private SettingWindow _settingWindow;
	private AwardWindow _awardWindow;
	
	public override void Start()
    {
	    //！！！！！！！！！！！！注意！！！！！！！
	    //此框架最大的坑就在于使用了EventListener之后，忘了移除！！
	    //！！！！！！！！！！！！注意！！！！！！！
	    EventDispatcher.AddEventListener<MainUIDisplayState>(EventConst.MainMenuDisplayChange, OnMainUIDisplayChange);
	    EventDispatcher.AddEventListener(EventConst.UpdateUserInfo,UpdateRoleInfo);
	    EventDispatcher.AddEventListener<string>(EventConst.UpdateMapName,UpdateMapName);
	    EventDispatcher.AddEventListener<bool>(EventConst.AwakePoemState,AwakePoemState);
	    EventDispatcher.AddEventListener<string>(EventConst.SetPoemItemStr,SetPoemData);
	    EventDispatcher.AddEventListener<int,string>(EventConst.SetSkillKeyPos,SetSkillKeyBtn);
	    EventDispatcher.AddEventListener<bool>(EventConst.ShowBattleTipsView,SetBattleTipsView);
	    EventDispatcher.AddEventListener<List<AwardData>>(EventConst.ShowAwardWindow,OpenAwardWindow);
	    EventDispatcher.AddEventListener(EventConst.RefreshTaskState,RefreshTaskTips);
	    InitSkillKeyPos();
	    InitMissionGameTips();
    }



	//！！！！！！！！！！！！注意！！！！！！！
	//此框架最大的坑就在于使用了EventListener之后，忘了移除！！
	//！！！！！！！！！！！！注意！！！！！！！
	
	
	private void InitMissionGameTips()
	{
		//todo userMissionvo需要添加一个字段：Needtips!!被选中需要提示的任务才会显示在列表上！！接收任务的时候默认会出现在列表中。
		//还有一些可以开始的主线任务，这个可能连MissionRule的字段都需要改啊！
		List<UserMissionVo> userMissionVos=new List<UserMissionVo>();
		foreach (var v in GlobalData.MissionData.UserMissionVos)
		{
			//todo 最多显示5个任务！
			if (v.MissionState==MissionState.StatusUnsUnfinished||v.MissionState==MissionState.StatusUnclaimed)
			{
				userMissionVos.Add(v);
			}
			
		}
		
		view.SetUserMissionData(userMissionVos);
		
		
	}
	
	private void RefreshTaskTips()
	{
		List<UserMissionVo> userMissionVos=new List<UserMissionVo>();
		foreach (var v in GlobalData.MissionData.UserMissionVos)
		{
			//todo 最多显示5个任务！
			if (v.MissionState==MissionState.StatusUnsUnfinished||v.MissionState==MissionState.StatusUnclaimed)
			{
				userMissionVos.Add(v);
			}
			
		}
		
		Debug.Log("刷新任务列表！！:"+userMissionVos.Count);
		view.SetUserMissionData(userMissionVos);
	}
	
	

	private void OpenAwardWindow(List<AwardData> awardDatas)
	{
		if (_awardWindow==null)
		{
			_awardWindow=PopupManager.ShowWindow<AwardWindow>("GameMain/Prefabs/AwardWindow");
			_awardWindow.SetData(awardDatas);
			
		}
		
		
	}

	private void InitSkillKeyPos()
	{
		view.SetSkillIcon(GlobalData.PlayerData.PlayerVo.UserSkillDatas);
	}
	
	private void SetSkillKeyBtn(int skillIdx, string skillKeyMap)
	{
		GlobalData.SkillModel.SetSkillKeyMap(skillIdx,skillKeyMap);
		view.SetSkillIcon(GlobalData.PlayerData.PlayerVo.UserSkillDatas);
	}

	private void SetBattleTipsView(bool isShow)
	{
		view.SetBattleTipsShow(isShow);
		
	}
	
	private void AwakePoemState(bool enable)
	{
		view.SetPoemListAwake(enable);
	}

	private void SetPoemData(string poem)
	{
		view.SetPoemListData(poem);
	}

	//todo 这里还需要刷新金钱数量和主界面任务状态等等
	private void UpdateRoleInfo()
	{
		view.SetData(GlobalData.PlayerData.PlayerVo);
	}

	private void UpdateMapName(string mapName)
	{
		//Debug.Log("Current Map："+mapName);
		view.UpdateMapName(mapName);
		
	}
	
	
	private void OnMainUIDisplayChange(MainUIDisplayState state)
	{
		switch (state)
		{
			case MainUIDisplayState.ShowAll:
				view.ShowAll();
				break;
			case MainUIDisplayState.ShowUserInfo:
				view.ShowUserInfo();
				break;
			case MainUIDisplayState.ShowUserInfoAndTopBar:
				view.ShowTopBarAndUserInfo();
				break;
			case MainUIDisplayState.ShowTopBar:
				view.ShowTopBar();
				break;
			case MainUIDisplayState.HideAll:
				view.ShowAll(false);
				break;
			default:
				Debug.LogError("No this type:"+state);
				break;
		}        
	}

	/// <summary>
    /// 处理View消息
    /// </summary>
    /// <param name="message"></param>
    public override void OnMessage(Message message)
    {
        string name = message.Name;
        object[] body = message.Params;
        switch (name)
        {
			case MessageConst.CMD_MAIN_OPENBAGVIEW:
				ModuleManager.Instance.EnterModule(ModuleConfig.MODULE_BAGVIEW,false);
				break;
			case MessageConst.CMD_MAIN_OPENEQUIPMENT:
				ModuleManager.Instance.EnterModule(ModuleConfig.MODULE_EQUIPMENT, false);
				break;
			case MessageConst.CMD_MAIN_OPENSTATUSVIEW:
				if (_statusWindow == null)
				{
					_statusWindow = PopupManager.ShowWindow<StatusWindow>("GameMain/Prefabs/StatusWindow");
				}

				break;
			case MessageConst.CMD_MAIN_OPENSHOPVIEW:
				if (_settingWindow == null)
				{
					_settingWindow=PopupManager.ShowWindow<SettingWindow>("GameMain/Prefabs/SettingWindow");
				}
				
				
				break;
			case MessageConst.CMD_MAIN_OPENSKILLVIEW:
				ModuleManager.Instance.EnterModule(ModuleConfig.MODULE_SKILL, false);
				break;
	        case MessageConst.CMD_MAIN_OPENTASKVIEW:
		        ModuleManager.Instance.EnterModule(ModuleConfig.MODULE_MISSION, false);
		        break;
	        case MessageConst.CMD_MAIN_OPENBATTLETIPSVIEWE:
		        bool isAcceptBattle = (bool)body[0];
		        if (isAcceptBattle)
		        {
			        Debug.LogError("isAcceptBattle");
					EventDispatcher.TriggerEvent(EventConst.SetBattleCam,Vector3.zero);
		        }
		        else
		        {
			        Debug.LogError("cancle!");
		        }
		        
		        //不管是什么状态，都会关闭BattleTipsUI
		        view.SetBattleTipsShow(false);
		        
		        
		        break;
			
        }
    }





	public override void Destroy()
	{

		EventDispatcher.RemoveEventListener<MainUIDisplayState>(EventConst.MainMenuDisplayChange, OnMainUIDisplayChange);
		EventDispatcher.RemoveEventListener(EventConst.UpdateUserInfo,UpdateRoleInfo);
		EventDispatcher.RemoveEventListener<string>(EventConst.UpdateMapName,UpdateMapName);
		EventDispatcher.RemoveEventListener<bool>(EventConst.AwakePoemState,AwakePoemState);
		EventDispatcher.RemoveEventListener<string>(EventConst.SetPoemItemStr,SetPoemData);
		EventDispatcher.RemoveEventListener<int,string>(EventConst.SetSkillKeyPos,SetSkillKeyBtn);
		EventDispatcher.RemoveEventListener<List<AwardData>>(EventConst.ShowAwardWindow,OpenAwardWindow);
		EventDispatcher.RemoveEventListener<bool>(EventConst.ShowBattleTipsView,SetBattleTipsView);
		EventDispatcher.RemoveEventListener(EventConst.RefreshTaskState,RefreshTaskTips);
		base.Destroy();
	}
}
