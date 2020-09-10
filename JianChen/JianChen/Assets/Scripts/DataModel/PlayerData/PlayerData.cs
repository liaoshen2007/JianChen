using System;
using System.Collections.Generic;
using DataModel;
using FrameWork.JianChen.Core;
using game.main;

namespace JianChen.Data
{
    public class PlayerData : Model
    {
        public PlayerVo PlayerVo;
        public List<MainRoleData> _MainRoleDatas;
        public bool HasPlayer = false;

        public PlayerData()
        {
            HasPlayer = false;
            PlayerVo=new PlayerVo();  
        }

//        public void InitData(PlayerBmobData playerAvoData)
//        {
//            PlayerVo.InitData(playerAvoData);           
//        }

        public void InitLocalUserData(PlayerVo vo)
        {
            PlayerVo.InitData(vo);          
        }

        public void InitRule(List<MainRoleData> _datas)
        {
//            foreach (var v in _datas)
//            {
//                 Debug.Log(v.Name);
//            }
            
            _MainRoleDatas = _datas;
        }
        
        //这里可以拓展玩家数据相关的方法！

        public void UpdatePlayerMoney(int gold)
        {
            PlayerVo.UpdateGold(gold);
        }

        
        //最多可以存5个玩家的存档,封装好数据存储对象
        public void SavePlayerData()
        {
            List<PlayerVo> playervos=new List<PlayerVo>();
            PlayerVo.SaveTime = DateTime.Now.ToLongTimeString();//ToShortDateString();
            playervos.Add(PlayerVo);
            //now i know !!
            AssetLoader.SaveUserData(playervos,"UserPlayerData");
        }

    }

}


