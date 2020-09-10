//using System.Collections;
//using cn.bmob.exception;
//using cn.bmob.io;

using System;
using System.Collections.Generic;
using Common;
using DataModel;
using FrameWork.JianChen.Core;
using FrameWork.JianChen.Core.Event;
using game.main;
using UnityEngine;
using Module;

public class LoginController : Controller
{
    public LoginView _LoginView;

    private RegisterWindow _registerWindow;
    private string _account;
    private string _password;

    private string _newaccount;

    private string _newpassword;
    private LoadGameWindow _loadGameWindow;


    public override void Start()
    {
        EventDispatcher.AddEventListener<string, string, string>(EventConst.Register, DoRegister);
        EventDispatcher.AddEventListener<PlayerVo>(EventConst.LoadGame,LoadGame);
//        AVObject.RegisterSubclass<PlayerBmobData>(); 
        SendMessage(new Message(MessageConst.CMD_LOGIN_LOAD_DATA));
    }


    private void DoRegister(string arg1, string arg2, string email)
    {
        //在此创建账号！
        //Debug.LogError("account"+arg1);
        _newaccount = arg1;
        _newpassword = arg2;
        //注册用户可以用Bmob特殊的账号管理中心！而不需要我们自创的表！
//        BmobUser user=new BmobUser();
//        user.username = arg1;
//        user.password = arg2;
//        user.email = email;
//        LoadingOverlay.Instance.Show();
//        Main.BmobUnity.Signup(user,OnRegisterCallBack);
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
            case MessageConst.CMD_LOGIN_DO_LOGIN: //登陆
//                _account = (string) body[0];
//                _password = (string) body[1];
//                Debug.Log("Loginaccount" + _account);
//                LoadingOverlay.Instance.Show();
//                PlayerPrefs.SetString("username", _account);
//                PlayerPrefs.SetString("password", _password);
                //SendMessage(new Message(MessageConst.CMD_LOGIN_LOAD_DATA));
                LoginLocalGameScene();
                //Main.BmobUnity.Login<UserAccountTable>(_account,_password,OnLoginBomb);
                break;
            case MessageConst.CMD_REGISTER_OPEN:
                _registerWindow = PopupManager.ShowWindow<RegisterWindow>("Login/Prefabs/RegisterWindow");
                break;
            case MessageConst.CMD_NEWGAMECREATER:

                CreatNewPlayer((string)body[0]);
                //LoginLocalGameScene();
                break;
            
        }
    }


    private void CreatNewPlayer(string userName)
    {
        Debug.Log("CreatNewPlayer:"+userName);
//        UserMissionVo usermissionvo=new UserMissionVo()
//        {
//            UserId = 1001,
//            MissionId = 1001,
//            Finish = 1,
//            MissionPro = 0,
//            MissionType = MissionType.MainLine,
//            MissionState = MissionState.StatusUnStarted,
//            Progress = 0
//        };
        List<UserMissionVo> userMissionVos=new List<UserMissionVo>();
        //userMissionVos.Add(usermissionvo);
        
        List<UserGrid> userGrids=new List<UserGrid>();

        for (int i = 0; i < 30; i++)
        {
            userGrids.Add(new UserGrid(){GridId = i,GridPropId = 0,GripType = GripType.Equip,PropNum = 0});
        }
        
        for (int i = 0; i < 30; i++)
        {
            userGrids.Add(new UserGrid(){GridId = i,GridPropId = 0,GripType = GripType.Consume,PropNum = 0});
        }
        
        List<UserSkillData> userSkillDatas=new List<UserSkillData>();
        foreach (var v in GlobalData.SkillModel.SkillBaseDataDic)
        {
            UserSkillData userskill=new UserSkillData()
            {
                SkillId = v.Key,
                SkillKeyPos = "",
                Level = 0,
            };
            userSkillDatas.Add(userskill);
        }
        
        
        PlayerVo newPlayerVo=new PlayerVo()
        {
            ID = 0,
            RoleName = "27号楞",
            Level = 1,
            Exp = 0,
            Gold = 0,
            UserName = userName,
            HP = 1000,
            MaxHP = 1000,
            NpcId = 1001,
            Occupation = 1,
            Sexual = 1,
            Equip = 0,
            MapName = "FreshScene",
            UserMissionVos=userMissionVos,
            UserBagGrids = userGrids,
            UserSkillDatas = userSkillDatas,
            SaveTime = DateTime.Now.ToLongTimeString()
        };

        
        //todo ！！！！！！！你经常遗忘的一步，要在newPlayerVo那里去增加新的数据！！！！！！！
//        List<PlayerVo> playervos=new List<PlayerVo>();
//        playervos.Add(newPlayerVo);
        ConfigDataManager.LoadUserDataById<PlayerVo>("UserPlayerData", data => {
            data.Add(newPlayerVo);
            AssetLoader.SaveUserData(data,"UserPlayerData");
            LoadGame(newPlayerVo);
        });



    }

    private void LoginLocalGameScene()
    {
        LoadingOverlay.Instance.Hide();
        ConfigDataManager.LoadUserDataById<PlayerVo>("UserPlayerData", data => {
            if (_loadGameWindow == null)
            {
                _loadGameWindow = PopupManager.ShowWindow<LoadGameWindow>("Login/Prefabs/LoadGameWindow");
            }

            _loadGameWindow.SetData(data);
            
            
//            //之后会把所有PlayerVo都存在UserPlayerData，但是这个可以供玩家去选择哪一个playerVo去开始游戏！！
//            GlobalData.PlayerData.InitLocalUserData(data[0]);
//            EntityManager.Instance.SpawnLeadingRolePool(EntityConstant.EntityConst.MainRole, Vector3.zero,false,Vector3.zero);
//            EntityManager.Instance.SpawnLeadingRolePool(EntityConstant.EntityConst.NPCRole, Vector3.zero,false,Vector3.zero);
//            EntityManager.Instance.SpawnLeadingRolePool(EntityConstant.EntityConst.EnemyRole, Vector3.zero,false,Vector3.zero);
//            
//            
//            Loading.instance.LoadingScene(data[0].MapName, () =>
//            {
//                ModuleManager.Instance.Remove(ModuleConfig.MODULE_LOGIN);
//                ModuleManager.Instance.OpenCommonModule(ModuleConfig.MODULE_GAME_MAIN, Main.CommonContainer);
//                EventDispatcher.TriggerEvent(EventConst.UpdateUserInfo);
//                EventDispatcher.TriggerEvent(EventConst.UpdateMapName,GlobalData.SceneData.CurMapData.MapName);
//                //todo 要把出生点的安全位置传给MainRolePool
//
//                    
//            });
        });
        

    }
    
    private void LoadGame(PlayerVo vo)
    {
        if (_loadGameWindow != null) _loadGameWindow.Close();
        LoadingOverlay.Instance.Hide();
        GlobalData.PlayerData.InitLocalUserData(vo);
        EntityManager.Instance.SpawnLeadingRolePool(EntityConstant.EntityConst.MainRole, Vector3.zero,false,Vector3.zero);
        EntityManager.Instance.SpawnLeadingRolePool(EntityConstant.EntityConst.NPCRole, Vector3.zero,false,Vector3.zero);
        EntityManager.Instance.SpawnLeadingRolePool(EntityConstant.EntityConst.EnemyRole, Vector3.zero,false,Vector3.zero);
            
            
        Loading.instance.LoadingScene(vo.MapName, () =>
        {
            ModuleManager.Instance.Remove(ModuleConfig.MODULE_LOGIN);
            ModuleManager.Instance.OpenCommonModule(ModuleConfig.MODULE_GAME_MAIN, Main.CommonContainer);
            EventDispatcher.TriggerEvent(EventConst.UpdateUserInfo);
            EventDispatcher.TriggerEvent(EventConst.UpdateMapName,GlobalData.SceneData.CurMapData.MapName);
            //todo 要把出生点的安全位置传给MainRolePool

                    
        });
        
        
    }

    public override void Destroy()
    {
        base.Destroy();
        EventDispatcher.RemoveEvent(EventConst.Register);
    }

    #region  BmobTest

        //    private void OnRegisterCallBack(BmobUser res, BmobException exception)
//    {
//        LoadingOverlay.Instance.Hide();
//        if (exception!=null)
//        {
//            Debug.LogError(exception);
//
//        }
//        else
//        {
//            PlayerPrefs.SetString("username", _newaccount);
//            PlayerPrefs.SetString("password", _newpassword);
//            _LoginView.DefaultAccount();
//            _registerWindow.Close(); 
//        }
//
//    }

    
    //    private void LoginFail()
//    {
//        Debug.LogError("登录失败");
//    }


    
    //    private void LoginGameScene()
//    {
//        //跳转到下一个游戏场景！要记录DontDestroyOnLoad!      
//        BmobQuery query=new BmobQuery();
//        query.WhereEqualTo("userName", _account);
//        
//        Main.BmobUnity.Find<PlayerBmobData>("PlayerInfo",query, (res, exception) =>
//        {
//            LoadingOverlay.Instance.Hide();
//            if (exception!=null)
//            {
//                Debug.LogError(exception.Message);
//                return;
//            }
//            
//            foreach (var v in res.results)
//            {
//                GlobalData.PlayerData.InitData(v);
//            }            
//            
//            EntityManager.Instance.SpawnLeadingRolePool(EntityConstant.EntityConst.MainRole, Vector3.zero,false,Vector3.zero);
//            EntityManager.Instance.SpawnLeadingRolePool(EntityConstant.EntityConst.NPCRole, Vector3.zero,false,Vector3.zero);
//            EntityManager.Instance.SpawnLeadingRolePool(EntityConstant.EntityConst.EnemyRole, Vector3.zero,false,Vector3.zero);
//            Loading.instance.LoadingScene("FreshScene", () =>
//            {
//                ModuleManager.Instance.Remove(ModuleConfig.MODULE_LOGIN);
//                ModuleManager.Instance.OpenCommonModule(ModuleConfig.MODULE_GAME_MAIN, Main.CommonContainer);
//                //todo 要把出生点的安全位置传给MainRolePool
//
//                    
//            });
//        }); 
//        
//        
//        Debug.LogError("Jump");
//
//    }
    
    //    private void OnLoginBomb(UserAccountTable response, BmobException exception)
//    {
//        if (exception != null)
//        {
//            Debug.LogError("登录失败, 失败原因为： " + exception.Message);
//            LoginFail();
//            return;
//        }
//
//        //可以根据是否有角色来创建角色！
//        Debug.Log("登录成功, @" + response.username + "角色信息" + response.HasPlayer);
//        if (response.HasPlayer==null||response.HasPlayer.Get() == 0)
//        {
//            Debug.LogError("待创建角色");
//            LoadingOverlay.Instance.Hide();
//            ModuleManager.Instance.EnterModule(ModuleConfig.MODULE_CREATE_USER);
//        }
//        else
//        {
//            Debug.Log("登录成功, 当前用户对象Session： " + BmobUser.CurrentUser.sessionToken);
//            FlowText.ShowMessage("登录成功，正在加载游戏场景");
//            PlayerPrefs.SetString("username", _account);
//            PlayerPrefs.SetString("password", _password);
//            LoginGameScene();
//        }
//
//    } 

    #endregion
    
    #region LeanCloud之前的用户注册
    //以下为LeanCloud的注册方式！
//        var userLeanCloud = new AVUser();
//        userLeanCloud.Username = arg1;
//        userLeanCloud.Password = arg2;
//        userLeanCloud.Email = email;
//        userLeanCloud["RoleNum"] = 0;
//
//        //无法在子线程调用某些方法，之后要合并这些方法！
//        //并非所有都需要跟携程结合，基础数据存储还是可以用await的，只是用户注册比较特殊！
//        var task = userLeanCloud.SignUpAsync().ContinueWith(t =>
//        {
//            var uid = userLeanCloud.ObjectId;
//            Debug.Log(t.IsCompleted + "注册成功" + uid);
//        });
//        ClientTimer.Instance.GetTaskCallBack(task, CloseRegister, null);

    //            var role = new AVQuery<PlayerBmobData>().WhereEqualTo("userName",user.Username);//查找到该用户名的角色！
//            var rolequery =role.FindAsync();
//            rolequery.ContinueWith(t =>
//            {
//                if (t.IsFaulted || t.IsCanceled)
//                {
//                    // 登录失败，可以查看错误信息。
//                    Debug.LogError(t.Exception + " " + t.Id);
//                }
//                else
//                {
//                    //登录成功
//                    foreach (var v in t.Result)
//                    {
//                        Debug.LogError(v.PlayerName);
//                    }
//                    Debug.LogError("Success!" );
//
//                    //登录成功后直接请求创建角色的窗口！
//                }
//            });
    
    //                var task = AVUser.LogInAsync(_account, _password).ContinueWith(t =>
//                {
//                    if (t.IsFaulted || t.IsCanceled)
//                    {
//                        // 登录失败，可以查看错误信息。
//                        Debug.LogError(t.Exception.Message + " " + t.Id);
//                    }
//                    else
//                    {
//                        //登录成功
//                        Debug.Log("Success!" + t.Id);
//
//                        //登录成功后直接请求创建角色的窗口！
//                    }
//                });
//                PlayerPrefs.SetString("username", _account);
//                PlayerPrefs.SetString("password", _password);

//                ClientTimer.Instance.GetTaskCallBack(task, LoginGameScene, LoginFail);
//    ClientTimer.Instance.GetTaskCallBack(rolequery, taskResult =>
//    {
//
////                foreach (var v in taskResult)
////                {
////                    Debug.LogError(v.PlayerName);
////
////                    GlobalData.PlayerData.InitData(v);
////                }
//                
//        //要把加载数据的东西都放在loading中比较好
//
//
//    });
//        //先不做跳转，因为后端还没做好数据存储之类的操作，先测试创建角色的模块
//        //判断RoleNum大于1的时候，直接跳转！    
//        AVUser user = AVUser.CurrentUser;


    //    private void OnRegisterBomb(BmobUser response, BmobException exception)
//    {
//        if (exception != null)
//        {
//            Debug.LogError("注册失败, 失败原因为： " + exception.Message);
//            return;
//        }
//
//        Debug.LogError("注册成功");
//        PlayerPrefs.SetString("username", _newaccount);
//        PlayerPrefs.SetString("password", _newpassword);
//        _LoginView.DefaultAccount();
//        _registerWindow.Close();
//    }    
//    var num = (int) user["RoleNum"];
//        if (num < 1)
//    {
//        //如果该账号中心没有角色ID,则直接跳转到创建角色界面！
//        ModuleManager.Instance.EnterModule(ModuleConfig.MODULE_CREATE_USER);
//    }
//    else
//    {           
//
//
//    }


    #endregion



}