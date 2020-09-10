using System.Collections;
using System.Collections.Generic;
using Common;
using DataModel;
using FrameWork.JianChen.Core.Event;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    private Text _playerLevel;
    private Text _playerName;
    private Text _saveTime;
    private Button _loadGameBtn;
    private PlayerVo _chooseVo;
    
    
    private void Awake()
    {
        _playerLevel = transform.GetText("LevelText");
        _playerName = transform.GetText("PlayerNameBtm/Text");
        _saveTime = transform.GetText("SaveTimeBtm/Text");
        _loadGameBtn = transform.GetButton("OkBtn");
        _loadGameBtn.onClick.AddListener(OnLoadGameClick);


    }

    private void OnLoadGameClick()
    {
        EventDispatcher.TriggerEvent(EventConst.LoadGame,_chooseVo);
        
        
    }

    public void SetData(PlayerVo vo)
    {
        _chooseVo = vo;
        _playerLevel.text = "Lv."+vo.Level ;
        _playerName.text = vo.UserName + "";
        _saveTime.text = vo.SaveTime + "";


    }
    
    
    
}
