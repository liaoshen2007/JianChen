
/// <summary>
/// notice:
/// 
/// 
/// MVC框架间传递事件用到的事件字段
/// model,module,controller的事件字段必须满足以下格式
/// MVC模块名_业务层模块名_具体派发内容(例如: MODULE_GAME_PAUSE , CMD_COMMERCIAL_GAIN_REWARD , MODEL_COMMERCIAL_ADD_CITY_DATA)
/// 
/// 
/// </summary>

public class MessageConst
{
    


    /// <summary>
    /// Module层消息通知
    /// </summary>
    /// 
     #region
   
    //创建用户
    public const string MODULE_CREATE_USER_CONFIRM = "MODULE_CREATE_USER_CONFIRM";
    
   
    #endregion


    /// <summary>
    /// Control层消息通知
    /// </summary>
    /// 
    #region
    //登录
    public const string CMD_LOGIN_DO_LOGIN = "MODULE_LOGIN_DO_LOGIN";

    public const string CMD_REGISTER_OPEN = "CMD_REGISTER_OPEN";
    public const string CMD_CREATEROLE = "CMD_CREATEROLE";

    public const string CMD_NEWGAMECREATER = "CMD_NEWGAMECREATER";

    public const string CMD_LOGIN_LOAD_DATA = "CMD_LOGIN_LOAD_DATA";//加载公有数据

    //主场景
    public const string CMD_MAIN_CHANGE_DISPLAY = "MODULE_MAIN_CHANGE_DISPLAY";//切换主场景菜单显示
    public const string CMD_MAIN_OPENBAGVIEW = "CMD_MAIN_OPENBAGVIEW";//打开背包
    public const string CMD_MAIN_OPENEQUIPMENT = "CMD_MAIN_OPENEQUIPMENT";//打开装备栏
    public const string CMD_MAIN_OPENSTATUSVIEW = "CMD_MAIN_OPENSTATUSVIEW";//打开状态栏
    public const string CMD_MAIN_OPENSHOPVIEW = "CMD_MAIN_OPENSHOPVIEW";//打开商城界面
    public const string CMD_MAIN_OPENSKILLVIEW = "CMD_MAIN_OPENSKILLVIEW";//打开技能栏界面
    public const string CMD_MAIN_OPENTASKVIEW = "CMD_MAIN_OPENTASKVIEW";//打开任务界面
    public const string CMD_MAIN_OPENBATTLETIPSVIEWE = "CMD_MAIN_OPENBATTLETIPSVIEWE";



    //加载
    public const string CMD_MAINROLEENTITY_CREATE = "CMD_MainRoleENTITY_CREATE";
    public const string CMD_NPCENTITY_CREATENPC = "CMD_NPCENTITY_CREATENPC";
    public const string CMD_ENEMYENTITY_CREATEENEMY = "CMD_ENEMYENTITY_CREATEENEMY";
    
    //对话
    public const string CMD_DIALOG_RECEIVETASK = "CMD_DIALOG_RECEIVETASK";
    public const string CMD_DIALOG_GETREWARD = "CMD_DIALOG_GETREWARD";
    public const string CMD_DIALOG_UNFINISHTASK = "CMD_DIALOG_UNFINISHTASK";
    
    //Player跳转到其他场景
    public const string CMD_MAINROLE_JUMPTOOTHERSCENE = "CMD_MAINROLE_JUMPTOOTHERSCENE";
    public const string CMD_MAINROLE_RESETROLEPOSITION = "CMD_MAINROLE_RESETROLEPOSITION";
    
    //背包
    public const string CMD_BAGIVEW_CLOSE = "CMD_BAGIVEW_CLOSE";
    public const string CMD_BAGVIEW_APPLYPROP = "CMD_BAGVIEW_APPLYPROP";
    
    //装备
    public const string CMD_EQUIMENT_CLOSE = "CMD_EQUIMENT_CLOSE";
    
    //商店
    public const string CMD_SHOP_CLOSE = "CMD_SHOP_CLOSE";
    
    //技能
    public const string CMD_SKILL_CLOSE = "CMD_SKILL_CLOSE";
    
    //任务
    public const string CMD_MISSION_CLOSE = "CMD_MISSION_CLOSE";
    public const string CMD_MISSION_CHOSETASKTYPE = "CMD_MISSION_CHOSETASKTYPE";

    #endregion




}
