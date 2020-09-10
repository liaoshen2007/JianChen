using DataModel;
using FrameWork.JianChen.Core;
using FrameWork.JianChen.Interfaces;
using UnityEngine;

public class MainRoleGameEntity : Entity
{

	private MainRoleEntityController _mainRoleEntityController;	
    
	public override void Init(IEntitySystem entity)
	{
		SetComplexEntity();
		base.Init(entity);
		
		//InstantiateView可以拓展为传入位置！先读取json然后给生成的npc传入位置！

		_mainRoleEntityController=new MainRoleEntityController();
//		RegisterView(mainroleEntityobj);
		RegisterController(_mainRoleEntityController);
		_mainRoleEntityController.Start();
	}

	public void CreateMainRole()
	{
		//设位置！
		var mainroleEntityobj = (MainRoleSingleEntity)InstantiateView<MainRoleSingleEntity>("MainRole/LiaoChen/Prefab/DanBoardLiao");
		
		//todo 这个属于暂时先写死，到时候要根据玩家数据来读取不同的模型！
		mainroleEntityobj.transform.name = "DanBoardLiao"; 
		_mainRoleEntityController.EntityObj = mainroleEntityobj;
		_mainRoleEntityController.EntityObj.transform.localPosition = GlobalData.SceneData.SpawnPos(0);
		GlobalData.PlayerData.HasPlayer = true;
		_mainRoleEntityController.InitPlayer();
	}

	public void ResetMainRolePos()
	{
		_mainRoleEntityController.EntityObj.transform.localPosition = GlobalData.SceneData.SpawnPos(0);
	}	
	public override void Hide()
	{
	}

	public override void Show(float delay)
	{
		Debug.Log("Success spawn mainrole!");
	}
}