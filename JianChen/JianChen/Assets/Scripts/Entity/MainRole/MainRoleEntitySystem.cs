using DataModel;
using FrameWork.JianChen.Core;
using UnityEngine;

public class MainRoleEntitySystem : EntityBase
{
    private MainRoleGameEntity _mainRoleGameEntity;
    private Vector3 _spawnPos;


    public override void Init()
    {
        _mainRoleGameEntity = new MainRoleGameEntity();
        _mainRoleGameEntity.Init(this);
        _mainRoleGameEntity.Show(0);
    }

    public override void OnMessage(Message message)
    {
        string name = message.Name;
        object[] body = message.Params;
        switch (name)
        {
            case MessageConst.CMD_MAINROLEENTITY_CREATE:
                _mainRoleGameEntity.CreateMainRole();
                break;
            case MessageConst.CMD_MAINROLE_RESETROLEPOSITION:
                _mainRoleGameEntity.ResetMainRolePos();
                break;
        }
    }

    public override void SetData(params object[] paramsObjects)
    {
        if (paramsObjects.Length > 0)
        {
//			Debug.LogError("setpos");
            _spawnPos = (Vector3) paramsObjects[0];
        }
    }

//	public void Destroy()
//	{
//		_mainRoleGameEntity.Destroy();
//	}
}