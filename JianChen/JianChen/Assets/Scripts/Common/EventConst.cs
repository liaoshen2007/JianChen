namespace Common
{
    /// <summary>
    /// 全局事件(用于模块之间，窗口和模块的交互)
    /// </summary>
    public class EventConst
    {
        

        //Common
        public const string MainMenuDisplayChange = "MainMenuDisplayChange";
        public const string UpdateUserInfo = "UpdateUserInfo";
        public const string UserLevelUp = "UserLevelUp";
        public const string UpdateMapName = "UpdateMapName";
        
        //道具更新
        public const string PropUpdated = "PropUpdated";
        
        
        //Login
        public const string Register = "Register";

        public const string LoadGame = "LoadGame";
        //public const string DoneRegister = "DoneRegister";
        
        //LoadModel
        public const string LoadModel = "LoadModel";
        public const string UnLoadModel = "UnLoadModel";
        
        //Player
        public const string JumpToOtherScene = "JumpToOtherScene";
        
        //NPC
        public const string ClickNpc = "ClickNpc";
        public const string ClickEnemy = "ClickEnemy";
        
        //Task
        public const string ChooseTask = "ChooseTask";
        public const string CanGetRewardFromNpc = "CanGetRewardFromNpc";
        public const string GetTaskReward = "GetTaskReward";
        public const string RefreshTaskState = "RefreshTaskState";
        
        //GameMain
        public const string GameMainBtnOnClick = "GameMainBtnOnClick";
        public const string HasPlayerEnter = "HasPlayerEnter";
        public const string GiveUpTargetAndBack = "GiveUpTargetAndBack";
        public const string AwakePoemState = "AwakePoemState";
        public const string SetPoemItemStr = "SetPoemItemStr";
        public const string LookAtNPC = "LookAtNPC";
        public const string SetCameState = "SetCameState";
        public const string ShowAwardWindow = "ShowAwardWindow";
        

        public const string HasBeenAttacked = "HasBeenAttacked";
        
        //Prop
        public const string UpdateGrid = "UpdateGrid";
        public const string SwapBagItem = "SwapBagItem";
        public const string ChooseBagItem = "ChooseBagItem";
        
        //Shop
        public const string BuyItem = "BuyItem";
        
        //Equipment
        public const string UpdateEquipmentView = "UpdateEquipmentView";
        
        //Skill
        public const string SetSkillKeyPos = "SetSkillKeyPos";
        
        //Battle
        public const string ShowBattleTipsView = "ShowBattleTipsView";
        public const string SetBattleCam = "SetBattleCam";



    }
}