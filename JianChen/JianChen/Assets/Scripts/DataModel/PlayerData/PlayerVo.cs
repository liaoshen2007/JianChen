using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataModel
{
    public class PlayerVo
    {
        public string RoleName;
        public int Level;
        public int Exp;
        public int Gold;
        public string UserName;
        public int ID;

        public int HP;
        public int MaxHP;
        
        
        public int NpcId;
        
        public int Occupation;
        public int Sexual;
        public int Equip;//本来最好应该是map

        public string MapName; //所在的地图的名字也需要的

        public string SaveTime;//存档时间，或者可以当成下线时间
        
        public List<UserMissionVo> UserMissionVos;
        public List<UserGrid> UserBagGrids; //用户背包格子
        public List<UserEquipData> UserEquipDatas;
        public List<UserSkillData> UserSkillDatas;

        
        /// <summary>
        /// 初始化字段
        /// </summary>
        /// <param name="userPlayervo"></param>
        public void InitData(PlayerVo userPlayervo)
        {
            //之后出现的字段要慢慢加！
            ID = userPlayervo.ID;
            UserName = userPlayervo.UserName;
            RoleName = userPlayervo.RoleName;
            Occupation = userPlayervo.Occupation;
            Sexual = userPlayervo.Sexual;
            Equip = userPlayervo.Equip;
            MapName = userPlayervo.MapName;
            HP = userPlayervo.HP;
            MaxHP = 1000;
            Level = userPlayervo.Level;
            Exp = userPlayervo.Exp;
            Gold = userPlayervo.Gold;
            NpcId = userPlayervo.NpcId;
            UserMissionVos = userPlayervo.UserMissionVos;
            UserBagGrids = userPlayervo.UserBagGrids;
            UserEquipDatas = userPlayervo.UserEquipDatas;
            UserSkillDatas = userPlayervo.UserSkillDatas;
            SaveTime = userPlayervo.SaveTime;
            Debug.Log("Currole"+RoleName);
            
            GlobalData.MissionData.InitUserMissionData(userPlayervo.UserMissionVos);
            GlobalData.PropModel.SetUserPropData(userPlayervo.UserBagGrids);

        }

        //直接覆盖比较安全吧，待测试！
        public void UpdateMission(List<UserMissionVo> lastmissionVos)
        {
            UserMissionVos = lastmissionVos;
        }

        public void UpdateHP(int value)
        {
            HP += value;
            if (HP <= 0)
            {
                HP = 0;
            }
        }

        public void UpdateGold(int value)
        {
            Gold += value;
            if (Gold<=0)
            {
                Gold = 0;
            }


        }


    }
    
    
    
}

/// <summary>
/// 初始化字段
/// </summary>
/// <param name="userPlayervo"></param>
//        public void InitData(PlayerBmobData userPlayervo)
//        {
//            //之后出现的字段要慢慢加！
//            UserName = userPlayervo.UserName;
//            RoleName = userPlayervo.PlayerName;
//            Occupation = userPlayervo.Occupation.Get();
//            Sexual = userPlayervo.Sexual.Get();
//            Equip = userPlayervo.Equip.Get();
//            Debug.Log("Currole"+RoleName);
//
//        }