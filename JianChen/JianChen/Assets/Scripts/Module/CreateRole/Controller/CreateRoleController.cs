//using cn.bmob.exception;
//using cn.bmob.io;
//using cn.bmob.response;
using Common;
using DataModel;
using FrameWork.JianChen.Core;
using game.main;

public class CreateRoleController : Controller
{


	public CreateRoleView _CreateRoleView;
	private CreatRoleVo _creatRoleVo;
//	private PlayerBmobData newPlayer;
	
	public override void Start()
    {
//       AVObject.RegisterSubclass<PlayerBmobData>(); 
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
          case  MessageConst.CMD_CREATEROLE:
	          var vo = (CreatRoleVo)body[0];//不知道为什么不可以用messagebody!
	          _creatRoleVo = vo;
	          ChangeAVUserRoleNum();
	          break;
        }
    }

	private void ChangeAVUserRoleNum()
	{
//		AVUser user = AVUser.CurrentUser;
//		_creatRoleVo.UserName = user.Username;		
//		var num = (int) user["RoleNum"];
//		if (num < 1)
//		{
//			user["RoleNum"] = 1;
//		}
//		else
//		{
//			user["RoleNum"] = num + 1;
//		}
//		var task = user.SaveAsync().ContinueWith(t => { _creatRoleVo.ServerId = (int)user["RoleNum"]; });
//		ClientTimer.Instance.GetTaskCallBack(task,CreatSuccss,null);
		
//		BmobUser buser=BmobUser.CurrentUser;
//		UserAccountTable gameUser=buser as UserAccountTable;
//		gameUser.HasPlayer = 1;
//		Main.BmobUnity.UpdateUser(gameUser.objectId,gameUser,gameUser.sessionToken,CreatSuccss);

	}

	/// <summary>
	/// 此处正在创建一个角色并且添加到数据库中！
	/// </summary>
//	private void CreatSuccss(UpdateCallbackData res,BmobException exception)
//	{
//		if (exception!=null)
//		{
//			Debug.LogError(exception.Message);
//			return;
//		}
//		else
//		{
//			Debug.Log("Good!CreatSuccss!");
//			newPlayer=new PlayerBmobData();
//			newPlayer.UserName = BmobUser.CurrentUser.username;
//			newPlayer.ServerId = _creatRoleVo.ServerId;//服务器Id这个是错误的，不过之后分服务器之后再做吧！
//			newPlayer.PlayerName = _creatRoleVo.RoleName;
//			newPlayer.Equip = _creatRoleVo.Equip;
//			newPlayer.Occupation = _creatRoleVo.Occupation;
//			newPlayer.Sexual = _creatRoleVo.Sexual;
//			
//			Main.BmobUnity.Create("PlayerInfo",newPlayer,CreatNewPlayer);
//			
////			var task = newPlayer.SaveAsync();
////			ClientTimer.Instance.GetTaskCallBack(task, () =>
////			{
////				//直接自己添加！
////				GlobalData.PlayerData.InitData(newPlayer);	
////				Loading.instance.LoadingScene("Scenes/FreshScene");
////				ModuleManager.Instance.Remove(ModuleConfig.MODULE_CREATE_USER);
////				ModuleManager.Instance.Remove(ModuleConfig.MODULE_LOGIN);
////				ModuleManager.Instance.OpenCommonModule(ModuleConfig.MODULE_GAME_MAIN, Main.CommonContainer);
////			
////			},null);
//		}
//		
//	}
//
//	private void CreatNewPlayer(CreateCallbackData response, BmobException exception)
//	{
//		if (exception!=null)
//		{
//			Debug.LogError(exception.Message);
//			return;
//		}
//		else
//		{
//			SendMessage(new Message(MessageConst.CMD_LOGIN_LOAD_DATA));
//			GlobalData.PlayerData.InitData(newPlayer);
//			EntityManager.Instance.SpawnLeadingRolePool(EntityConstant.EntityConst.MainRole, Vector3.zero,false,Vector3.zero);
//			EntityManager.Instance.SpawnLeadingRolePool(EntityConstant.EntityConst.NPCRole, Vector3.zero,false,Vector3.zero);
//			Loading.instance.LoadingScene("FreshScene", () =>
//			{
//				ModuleManager.Instance.Remove(ModuleConfig.MODULE_CREATE_USER);
//				ModuleManager.Instance.Remove(ModuleConfig.MODULE_LOGIN);
//				ModuleManager.Instance.OpenCommonModule(ModuleConfig.MODULE_GAME_MAIN, Main.CommonContainer);
////				EntityManager.Instance.SpawnLeadingRolePool(EntityConstant.EntityConst.MainRole, Vector3.zero,false,Vector3.zero);
//			});
//		}
//		
//	}

	public override void Destroy()
	{
		base.Destroy();
	}
}
