using System.Collections;
using System.Collections.Generic;
using DataModel;
using game.main;
using UnityEngine;
using UnityEngine.UI;

public class LoadGameWindow : Window
{
    private Button _closeBtn;
    private LoopVerticalScrollRect _playerList;
    private List<PlayerVo> _playerVos;
    
    
    private void Awake()
    {
        _closeBtn = transform.GetButton("LoadGamePanel/CloseBtn");
        _closeBtn.onClick.AddListener(Close);
        
        _playerList = transform.Find("LoadGamePanel/PlayerInfoList/PlayerContent").GetComponent<LoopVerticalScrollRect>();
        _playerList.poolSize = 6;
        _playerList.prefabName = "Login/Prefabs/PlayerItem";
        _playerList.UpdateCallback = PlayerListUpdateCallBack;

    }

    public void SetData(List<PlayerVo> vos)
    {
        _playerVos = vos;
        _playerList.RefillCells();
        _playerList.totalCount = vos.Count;
        _playerList.RefreshCells();

    }
    
    private void PlayerListUpdateCallBack(GameObject gameobj, int index)
    {
        gameobj.GetComponent<PlayerItem>().SetData(_playerVos[index]);
    }
}
