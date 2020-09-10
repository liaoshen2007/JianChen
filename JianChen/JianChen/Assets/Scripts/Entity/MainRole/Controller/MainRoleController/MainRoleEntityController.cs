using Common;
using DataModel;
using FrameWork.JianChen.Core;
using FrameWork.JianChen.Core.Event;
using UnityEngine;

public class MainRoleEntityController : Controller
{


	public MainRoleSingleEntity EntityObj;
	public GlobalGameState _curGameState;

	
	public override void Start()
    {
		//发送消息给GameMainUI，可以操作控制！  //初始化人物可以执行!
	    EventDispatcher.AddEventListener(EventConst.LoadModel,LoadMainRoleModel);
	    EventDispatcher.AddEventListener<int,int>(EventConst.GameMainBtnOnClick,GameMainBtnClick);

	    
    }

	private void GameMainBtnClick(int btnIndex,int skillIndex)
	{
		EntityObj.SetGameMainClickCMD(btnIndex,skillIndex);
		
		
	}

	private void LoadMainRoleModel()
	{
		if (!GlobalData.PlayerData.HasPlayer)
		{
			SendMessage(new Message(MessageConst.CMD_MAINROLEENTITY_CREATE));
		}
		else
		{
//			Debug.LogError("resetMonsterPos：");
			SendMessage(new Message(MessageConst.CMD_MAINROLE_RESETROLEPOSITION));
		}

		_curGameState = GlobalGameState.GamePlay;
		SetViewGameState();
	}

	private void SetViewGameState()
	{
		EntityObj.SetGameState(_curGameState);
		
	}

	public void InitPlayer()
	{
		if (GlobalData.PlayerData.HasPlayer)
		{
			EntityObj.SetData(GlobalData.PlayerData.PlayerVo);
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
			case MessageConst.CMD_MAINROLE_JUMPTOOTHERSCENE:
				string sceneName = (string) body[0];
				Debug.Log("JumpToScene"+sceneName);
				Loading.instance.LoadingScene(sceneName, () =>
				{
					_curGameState = GlobalGameState.Loading;
					SetViewGameState();
				});
				
				break;
	        
	        
        }
    }

	

	public override void Destroy()
	{
		EventDispatcher.RemoveEventListener(EventConst.LoadModel,LoadMainRoleModel);
		EventDispatcher.RemoveEventListener<int,int>(EventConst.GameMainBtnOnClick,GameMainBtnClick);
	}
}
