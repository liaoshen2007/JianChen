using System.Collections.Generic;
using System.IO;
using DataModel;
using FrameWork.JianChen.Core;
using game.main;
using LitJson;
using UnityEngine;

public class LoadDataController : Controller
{
    public override void OnMessage(Message message)
    {
        string name = message.Name;
        object[] body = message.Params;
        switch (name)
        {
            case MessageConst.CMD_LOGIN_LOAD_DATA:
                StartLoadData();
                break;

        }
    }

    private void StartLoadData()
    {
        ConfigDataManager.LoadPlayRuleDataById<MainRoleData>("MainRoleData", data =>
        {
            //在add数据的时候，一定要先解析json中的数组之类的数据！
            GlobalData.PlayerData.InitRule(data);
        });

        ConfigDataManager.LoadPlayRuleDataById<MissionRule>("MissionRuleData", data =>
        {
            GlobalData.MissionData.InitMissionRule(data);
        });
        

        ConfigDataManager.LoadPlayRuleDataById<PoemData>("PoemData", data =>
        {
            GlobalData.PoemGameData.InitPoemGameData(data);
        });

        ConfigDataManager.LoadPlayRuleDataById<EquipBaseData>("EquipBaseRule", data =>
        {
            GlobalData.PropModel.InitEquipBaseData(data);
            
            
        });

        ConfigDataManager.LoadPlayRuleDataById<ShopBaseData>("ShopBaseData", data =>
        {
            GlobalData.ShopModel.SetShopMallDic(data);
            
        });

        ConfigDataManager.LoadPlayRuleDataById<SkillBaseData>("SkillBaseData", data =>
        {
            GlobalData.SkillModel.InitSkillBaseData(data);
        });



    }
}


//        ConfigDataManager.LoadUserDataById<UserMissionVo>("UserMissionData", data =>
//        {
//            if (data!=null)
//            {
//                Debug.Log("missioncount:"+data.Count);
//                GlobalData.MissionData.InitUserMissionData(data);
//            }
//            else
//            {
//                Debug.LogError("NewMission");
//                UserMissionVo uservo=new UserMissionVo()
//                {
//                    UserId = 1001,
//                    MissionId = 1001,
//                    Finish = 1,
//                    MissionPro = 0,
//                    MissionType = MissionType.MainLine,
//                    MissionState = MissionState.StatusUnStarted,
//                    Progress = 0
//                };
//                List<UserMissionVo> userMissionVos=new List<UserMissionVo>();
//                userMissionVos.Add(uservo);
//                string jsondata = JsonMapper.ToJson(userMissionVos);
//                string path = AssetLoader.GetUserDataPath("UserMissionData");
//                StreamWriter sw = new StreamWriter(path);
//                sw.Write(jsondata);
//                sw.Close();
//                sw.Dispose();
//                
//                
//            }
//            
//            
//        });

//        ConfigDataManager.LoadPlayDataById<NPCData>("NPCData", data =>
//        {
//            Debug.LogError(data.Count);
//            foreach (var v in data)
//            {
//                //这是正确的代码！
//                Debug.LogError(v.DefaultDialog);
//                
//                //可以解析成string[]或者int[]需要包裹一层！
//                var dia = JsonMapper.ToObject<string[]>(v.DefaultDialog);
//                Debug.LogError(dia.Length+" "+dia[0]);
//
//            }
//
//        });
