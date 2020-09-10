using System.Collections;
using System.Collections.Generic;
using JianChen.Data;

namespace DataModel
{
    public class GlobalData
    {
        public static PlayerData PlayerData;
        public static SceneData SceneData;
        public static MissionData MissionData;
        public static PoemGameData PoemGameData;
        public static PropModel PropModel;
        public static ShopModel ShopModel;
        public static SkillModel SkillModel;
        
        public static void InitData()
        {
            PlayerData=new PlayerData();  
            SceneData=new SceneData();
            MissionData=new MissionData();
            PoemGameData=new PoemGameData();
            PropModel=new PropModel();
            ShopModel=new ShopModel();
            SkillModel=new SkillModel();
        }

    } 

}


