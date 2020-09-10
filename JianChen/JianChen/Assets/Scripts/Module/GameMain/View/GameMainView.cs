
using System.Collections.Generic;
using Assets.Scripts.Framework.JianChen.Service;
using Common;
using DataModel;
using DG.Tweening;
using FrameWork.JianChen.Core;
using FrameWork.JianChen.Core.Event;
using game.main;
using UnityEngine;
using UnityEngine.UI;

public class GameMainView : View
{

    private FixedJoystick _joystick;
    private Transform buttonPanel;
    private Transform topPanel;
    private Transform bottomAreaPanel;
    private Transform _battleAcceptPanel;
    
    private Button _NormalBtn;

    private ProgressBar _hpBar;
    private ProgressBar _mpBar;
    private Text _hpTxt;
    private Text _mpTxt;

    private Text _roleInfoTxt;

    private Button _mapIn;
    private Button _mapOut;

    private Text _mapName;

    private ProgressBar _expBar;

    private Transform _menuTran;
    private Button _switchBtn;
    private bool _showMenu;

    private LoopVerticalScrollRect _poemLoopVerticalScrollRect;
    private LoopVerticalScrollRect _taskLoopVerticalScrollRect;
    private Transform _poemListTran;

    private List<string> _poemDataList;
    private List<UserMissionVo> _userMissionVos;

    private Button _bagBtn;
    private Button _statusBtn;
    private Button _skillBtn;
    private Button _equipBtn;
    private Button _settingBtn;
    private Button _taskBtn;

    private Button _acceptBattle;
    private Button _cancleBattle;

    private Button _skillKey1;
    private Button _skillKey2;
    private Button _skillKey3;

    private int _skillid1=1;
    private int _skillid2=1;
    private int _skillid3=1;

    private RawImage _skill1Icon;
    private RawImage _skill2Icon;
    private RawImage _skill3Icon;
    
    

    private void Awake()
    {
        _joystick = transform.Find("JoyStick").GetComponent<FixedJoystick>();
        Main.MainJoystick = _joystick;
        buttonPanel = transform.Find("BottonArea");
        topPanel = transform.Find("TopPanel");
        bottomAreaPanel = transform.Find("BottomPanel");
        _battleAcceptPanel = transform.Find("TopPanel/AcceptBattle");

        _acceptBattle = _battleAcceptPanel.Find("Accept").GetButton();
        _cancleBattle = _battleAcceptPanel.Find("Cancle").GetButton();
        
        _acceptBattle.onClick.AddListener(AcceptBattleOnClick);
        _cancleBattle.onClick.AddListener(CancleBattleOnClick);
        
        _NormalBtn = transform.Find("BottonArea/Normal").GetComponent<Button>();
        _NormalBtn.onClick.AddListener(OnNormalClick);

        _hpBar = transform.Find("TopPanel/RoleInfo/HPBar").GetComponent<ProgressBar>();
        _mpBar = transform.Find("TopPanel/RoleInfo/MPBar").GetComponent<ProgressBar>();
        _hpTxt = transform.Find("TopPanel/RoleInfo/HPBarTxt").GetText();
        _mpTxt = transform.Find("TopPanel/RoleInfo/MPBarTxt").GetText();
        _roleInfoTxt = transform.Find("TopPanel/RoleInfo/NameTag/InfoTxt").GetText();
        
        _mapIn = transform.Find("TopPanel/MapInfo/Minimap/InBtn").GetButton();
        _mapOut = transform.Find("TopPanel/MapInfo/Minimap/OutBtn").GetButton();
        _mapIn.onClick.AddListener(OnMapBigger);
        _mapOut.onClick.AddListener(OnMapSmaller);
        
        _mapName = transform.Find("TopPanel/MapInfo/Minimap/NameTag/MapName").GetText();
        
        _expBar = transform.Find("BottomPanel/ExpBar").GetComponent<ProgressBar>();

        _menuTran = transform.Find("BottomPanel/MainIcon");
        _showMenu = false;
        _switchBtn = transform.Find("BottomPanel/SwitchBtn").GetButton();
        _switchBtn.onClick.AddListener(SwitchMenuIconState);


        //添加玩家诗句碎片熟练度的字段
        _poemLoopVerticalScrollRect =
            transform.Find("TopPanel/PoemList/PoemContent").GetComponent<LoopVerticalScrollRect>();
        _poemLoopVerticalScrollRect.poolSize = 6;
        _poemLoopVerticalScrollRect.prefabName = "GameMain/Prefabs/PoemItem";
        _poemLoopVerticalScrollRect.UpdateCallback = OnPoemListCallBack;
        _poemListTran = transform.Find("TopPanel/PoemList");
        _poemListTran.gameObject.SetActive(false);
        
        _poemDataList=new List<string>();
        _userMissionVos=new List<UserMissionVo>();

        _taskLoopVerticalScrollRect = transform.Find("TopPanel/TaskList/TaskContent").GetComponent<LoopVerticalScrollRect>();
        _taskLoopVerticalScrollRect.poolSize = 6;
        _taskLoopVerticalScrollRect.prefabName = "GameMain/Prefabs/GameTaskItem";
        _taskLoopVerticalScrollRect.UpdateCallback = OnTaskListCallBack;
        
        

        _bagBtn = transform.GetButton("BottomPanel/MainIcon/Bag");
        _statusBtn = transform.GetButton("BottomPanel/MainIcon/Status");
        _skillBtn = transform.GetButton("BottomPanel/MainIcon/Skill");
        _equipBtn = transform.GetButton("BottomPanel/MainIcon/Equip");
        _settingBtn = transform.GetButton("BottomPanel/MainIcon/Setting");
        _taskBtn = transform.GetButton("TopPanel/RoleInfo/Task");
        
        _bagBtn.onClick.AddListener(BagOnClick);
        _statusBtn.onClick.AddListener(StatusOnClick);
        _skillBtn.onClick.AddListener(SkillViewOnClick);
        _equipBtn.onClick.AddListener(EquipOnClick);
        _settingBtn.onClick.AddListener(SettingOnClick);
        _taskBtn.onClick.AddListener(TaskViewOnClick);

        _skillKey1 = transform.GetButton("BottonArea/SkillBtn1");
        _skillKey2 = transform.GetButton("BottonArea/SkillBtn2");
        _skillKey3 = transform.GetButton("BottonArea/SkillBtn3");
        
        _skillKey1.onClick.AddListener(OnSkillKey1Click);
        _skillKey2.onClick.AddListener(OnSkillKey2Click);
        _skillKey3.onClick.AddListener(OnSkillKey3Click);

        _skill1Icon = _skillKey1.transform.Find("SkillKey1").GetRawImage();
        _skill2Icon = _skillKey2.transform.Find("SkillKey2").GetRawImage();
        _skill3Icon = _skillKey3.transform.Find("SkillKey3").GetRawImage();




    }

    /// <summary>
    /// 点击后接受战斗
    /// </summary>
    private void AcceptBattleOnClick()
    {
        SendMessage(new Message(MessageConst.CMD_MAIN_OPENBATTLETIPSVIEWE,Message.MessageReciverType.CONTROLLER,true));
    }
    
    /// <summary>
    /// 取消战斗
    /// </summary>
    private void CancleBattleOnClick()
    {
        SendMessage(new Message(MessageConst.CMD_MAIN_OPENBATTLETIPSVIEWE,Message.MessageReciverType.CONTROLLER,false));
    }


    private void TaskViewOnClick()
    {
        Debug.Log("Task");
        SendMessage(new Message(MessageConst.CMD_MAIN_OPENTASKVIEW,Message.MessageReciverType.CONTROLLER));
    }
    
    private void SettingOnClick()
    {
        Debug.Log("Setting");
        SendMessage(new Message(MessageConst.CMD_MAIN_OPENSHOPVIEW,Message.MessageReciverType.CONTROLLER));
    }

    private void EquipOnClick()
    {
        Debug.Log("EquipOnClick");
        SendMessage(new Message(MessageConst.CMD_MAIN_OPENEQUIPMENT,Message.MessageReciverType.CONTROLLER));
        
    }

    private void SkillViewOnClick()
    {
        Debug.Log("SkillViewOnClick");
        SendMessage(new Message(MessageConst.CMD_MAIN_OPENSKILLVIEW,Message.MessageReciverType.CONTROLLER));
    }

    private void StatusOnClick()
    {
        Debug.Log("StatusOnClick");
        SendMessage(new Message(MessageConst.CMD_MAIN_OPENSTATUSVIEW,Message.MessageReciverType.CONTROLLER));
    }

    private void BagOnClick()
    {
        Debug.Log("BagOnClick");
        SendMessage(new Message(MessageConst.CMD_MAIN_OPENBAGVIEW,Message.MessageReciverType.CONTROLLER));
    }

    public void SetBattleTipsShow(bool isShow)
    {
        _battleAcceptPanel.gameObject.SetActive(isShow);
        
        
    }
    
    public void SetPoemListAwake(bool enable)
    {
        _poemListTran.gameObject.SetActive(enable);
        _poemDataList.Clear();
        _poemLoopVerticalScrollRect.totalCount = _poemDataList.Count;
        _poemLoopVerticalScrollRect.RefreshCells();
    }

    public void SetPoemListData(string poemItem)
    {
        _poemDataList.Add(poemItem);
        _poemLoopVerticalScrollRect.totalCount = _poemDataList.Count;
        _poemLoopVerticalScrollRect.RefreshCells();
    }

    public void SetUserMissionData(List<UserMissionVo> vos)
    {
        _userMissionVos = vos;
        //_taskLoopVerticalScrollRect.RefillCells();
        _taskLoopVerticalScrollRect.totalCount = _userMissionVos.Count;
        Debug.Log(_taskLoopVerticalScrollRect.totalCount);
        _taskLoopVerticalScrollRect.RefreshCells();
        
        //这样加就好了？！！！！不科学！！！这个列表问题太大了！
        _taskLoopVerticalScrollRect.RefillCells();

    }
    
    private void OnPoemListCallBack(GameObject gameobj, int index)
    {
        gameobj.GetComponent<PoemItem>().SetData(index,_poemDataList[index]);
    }
    
    private void OnTaskListCallBack(GameObject gameobj, int index)
    {
        gameobj.GetComponent<GameTaskItem>().SetData(_userMissionVos[index]);
    }


    
    /// <summary>
    /// 控制Menu的状态
    /// </summary>
    private void SwitchMenuIconState()
    {
        if (_showMenu)
        {
            //先旋转按钮然后缩小_menuTran,最后隐藏。
            
            Tweener tween1 = _switchBtn.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.3f);
            Tweener tween2 = _menuTran.transform.DOScaleX(0.05f, 0.3f);
            DOTween.Sequence().Append(tween1).Join(tween2).OnComplete(() =>
            {
                _showMenu = false;
                _menuTran.gameObject.SetActive(false);
            });
        }
        else
        {
            //先显示tran,然后显示_menu,然后放大
            _menuTran.gameObject.SetActive(true);
            _menuTran.transform.localScale=new Vector3(0.1f,1);
            Tweener tween1 = _switchBtn.transform.DOLocalRotate(new Vector3(0, 0, 180), 0.3f);
            Tweener tween2 = _menuTran.transform.DOScaleX(1, 0.3f);
            DOTween.Sequence().Append(tween1).Join(tween2).OnComplete(() =>
            {
                _showMenu = true;
            });
        }
        
        
        
    }


    public void SetData(PlayerVo vo)
    {
        _hpBar.Progress = (int)((float)vo.HP/(vo.MaxHP)*100);
        _mpBar.Progress = (int)((float)vo.HP/(vo.MaxHP)*100);
        _hpTxt.text = vo.HP + "/" + vo.MaxHP;
        _mpTxt.text = vo.HP + "/" + vo.MaxHP;//暂时没有mp！！！
        _roleInfoTxt.text = "Lv."+vo.Level+" "+vo.RoleName;
        //_mapName.text = "FreshScene";
        _expBar.Progress = (int)((float)vo.Exp/(400)*100);//exp还需要配表！

    }

    public void UpdateMapName(string mapName)
    {
        _mapName.text = mapName;
    }
    
    private void OnMapSmaller()
    {
        Debug.Log("OnMapSmaller");
    }

    private void OnMapBigger()
    {
        Debug.Log("OnMapBigger");
    }
    
    private void OnNormalClick()
    {
        //发送普通按键请求。
        EventDispatcher.TriggerEvent(EventConst.GameMainBtnOnClick,0,1);
        
    }

    public void SetSkillIcon(List<UserSkillData> userSkillList)
    {
        //先初始化
        _skill1Icon.texture=ResourceManager.Load<Texture>("SkillIcon/skill0");
        _skill2Icon.texture=ResourceManager.Load<Texture>("SkillIcon/skill0");
        _skill3Icon.texture=ResourceManager.Load<Texture>("SkillIcon/skill0");
        
        //遍历一遍SkillDataList,Switch case keycode，然后赋值——Skillid1！
        for (int i = 0; i < userSkillList.Count; i++)
        {
            var userSkilldata = userSkillList[i];
            switch (userSkilldata.SkillKeyPos)
            {
                case "SkillKey1":
                    var skillbaseData = GlobalData.SkillModel.SkillBaseDataDic[userSkilldata.SkillId];
                    _skillid1 = userSkilldata.SkillId;
                    _skill1Icon.texture=ResourceManager.Load<Texture>("SkillIcon/"+skillbaseData.SkillIcon);
                    break;
                case "SkillKey2":
                    var skillbaseData2 = GlobalData.SkillModel.SkillBaseDataDic[userSkilldata.SkillId];
                    _skillid2 = userSkilldata.SkillId;
                    _skill2Icon.texture=ResourceManager.Load<Texture>("SkillIcon/"+skillbaseData2.SkillIcon);
                    break;
                case "SkillKey3":
                    var skillbaseData3 = GlobalData.SkillModel.SkillBaseDataDic[userSkilldata.SkillId];
                    _skillid3 = userSkilldata.SkillId;
                    _skill3Icon.texture=ResourceManager.Load<Texture>("SkillIcon/"+skillbaseData3.SkillIcon);
                    break;
                default:
//                    Debug.Log("No skillkeycode");
                    break;
            }


        }
        
        
        
        
        
        
    }
    
    private void OnSkillKey1Click()
    {
        
        EventDispatcher.TriggerEvent(EventConst.GameMainBtnOnClick,1,_skillid1);
        
    }

    private void OnSkillKey2Click()
    {
        EventDispatcher.TriggerEvent(EventConst.GameMainBtnOnClick,2,_skillid2);
    }

    private void OnSkillKey3Click()
    {
        EventDispatcher.TriggerEvent(EventConst.GameMainBtnOnClick,3,_skillid3);
        
        
    }
    


    public void ShowAll(bool isShow=true)
    {
        _joystick.gameObject.SetActive(isShow);
        buttonPanel.gameObject.SetActive(isShow);
        topPanel.gameObject.SetActive(isShow);
        bottomAreaPanel.gameObject.SetActive(isShow);
    }

    public void ShowUserInfo()
    {

    }

    public void ShowTopBarAndUserInfo()
    {

    }

    public void ShowTopBar()
    {
        _joystick.gameObject.SetActive(false); 
        buttonPanel.gameObject.SetActive(false);
        topPanel.gameObject.SetActive(false);
        bottomAreaPanel.gameObject.SetActive(false);
    }
}
