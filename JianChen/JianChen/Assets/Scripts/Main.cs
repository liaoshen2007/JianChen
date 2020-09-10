using Assets.Scripts.Common;
using Common;
using DataModel;
using FrameWork.JianChen.Core;
using FrameWork.JianChen.Core.Event;
using game.main;
using Module;
using UnityEngine;

//using LeanCloud;

public class Main : MonoBehaviour
{
    // Use this for initialization
    private int storyId;
    private string welcomeTips = "你好欢迎来到剑晨世界";

    public static Camera UiCamera;
    public static GameObject UiContainer;
    public static GameObject CommonContainer;
    public static FixedJoystick MainJoystick;
    public static FollowMainRole FollowCam;
    public static Transform TargetRole;

    public static int StageHeight = 1080;
    public static int StageWidth = 1920;
    private static bool _enableBackKey = true;

//    public static BmobUnity BmobUnity;

    /// <summary>
    /// 整体缩放率
    /// </summary>
    public static float ScaleFactor = 1;

    /// <summary>
    /// Canvas缩放率
    /// </summary>
    public static float CanvasScaleFactor;

    public static float ScaleX;
    public static float ScaleY;

    /// <summary>
    /// 刘海屏
    /// </summary>
    public bool IsSpecialScreen;


    public static void ChangeMenu(MainUIDisplayState state)
    {
        EventDispatcher.TriggerEvent(EventConst.MainMenuDisplayChange, state);
    }

    public static void SetFollowTran(Transform mainrole)
    {
//        FollowCam.SetFollow(mainrole);  
        TargetRole = mainrole;
    }

    public static bool EnableBackKey
    {
        get { return _enableBackKey; }
        set { _enableBackKey = value; }
    }



    void Start()
    {
        DontDestroyOnLoad(this);
        AssetManager.Initialize();
        EntityManager.Initialize();
        AudioManager.Initialize();
//        UiCamera = GetComponent<Camera>();
        FollowCam = GameObject.Find("ModelCamera").GetComponent<FollowMainRole>();
        UiContainer = gameObject.transform.Find("Canvas").gameObject;

//        BmobUnity = this.GetComponent<BmobUnity>();
//        BmobDebug.Register(print);
        GlobalData.InitData();
        CommonContainer = gameObject.transform.Find("CommonCanvas").gameObject;
        Canvas canvas = transform.Find("Canvas").GetComponent<Canvas>();

        ScaleX = StageWidth / (float) Screen.width;
        ScaleY = StageHeight / (float) Screen.height;
        ScaleFactor = Mathf.Min(ScaleX, ScaleY);
        ScaleFactor *= canvas.scaleFactor;
        CanvasScaleFactor = canvas.scaleFactor;

        int offY = SetOffsetOnPhone();
        ModuleManager.Instance.SetOffY(offY);
        ModuleManager.Instance.SetContainer(UiContainer);
        ReturnablePanel.SetBackBtn(GameObject.Find("BackBtn"));
        ModuleManager.Instance.OpenModule(ModuleConfig.MODULE_LOGIN);
        EventDispatcher.AddEventListener<FollowMainRole.CamState>(EventConst.SetCameState,SetModelCamState);
        EventDispatcher.AddEventListener<Transform>(EventConst.LookAtNPC,SetNPCCamPos);
        EventDispatcher.AddEventListener<Vector3>(EventConst.SetBattleCam,SetBattleCamPos);

    }
    
    public void SetNPCCamPos(Transform npctran)
    {
        
        FollowCam.SetTargetNPC(npctran);
        FollowCam.SetModelCamState(FollowMainRole.CamState.LOOK_AT_NPC);
    }

    public void SetBattleCamPos(Vector3 focusPoint)
    {
        
        
        FollowCam.SetModelCamState(FollowMainRole.CamState.Battle);
        
    }

    public void SetModelCamState(FollowMainRole.CamState state)
    {
        FollowCam.SetModelCamState(state);
    }

    private void OnDestroy()
    {
        EventDispatcher.RemoveEventListener<FollowMainRole.CamState>(EventConst.SetCameState,SetModelCamState);
        EventDispatcher.RemoveEventListener<Transform>(EventConst.LookAtNPC,SetNPCCamPos);
        EventDispatcher.RemoveEventListener<Vector3>(EventConst.SetBattleCam,SetBattleCamPos);
    }

    /// <summary>
    /// 适配检查是否为刘海屏
    /// </summary>
    /// <returns></returns>
    private int SetOffsetOnPhone()
    {
        IsSpecialScreen = false;
//		var info = UnityNativeExtension.Instance.GetDeviceInfo();
//		Debug.Log("Device Info :" + info);
//#if UNITY_IOS
//        if(info.Equals("iPhone10,3") || info.Equals("iPhone10,6"))
//        {
//            IsSpecialScreen = true;
//            return 90;
//        }
//#elif UNITY_ANDROID
//        if (info.Equals("true"))
//        {
//            IsSpecialScreen = true;
//            return 80;
//        }
//#endif
        Debug.Log("=======IsSpecialScreen=======" + IsSpecialScreen);
        return 0;
    }
}

public enum MainUIDisplayState
{
    ShowAll,
    ShowUserInfo,
    ShowUserInfoAndTopBar,
    ShowTopBar,
    HideAll
}

public enum GlobalGameState
{
    Login=0,
    Loading=1,
    GamePlay=2,
}
