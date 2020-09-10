using System.Collections;
using System.Collections.Generic;
using game.main;
using UnityEngine;
using UnityEngine.UI;

public class StatusWindow : Window
{
    private Button _closeBtn;
    private Text _mainProperties;
    private Text _detailProperties;

    private void Awake()
    {
        _closeBtn = transform.Find("StatusPanel/CloseBtn").GetButton();
        _mainProperties = transform.Find("StatusPanel/InfoPanel/MainProperities").GetText();
        _detailProperties = transform.Find("StatusPanel/InfoPanel/DetailProperities").GetText();
        _closeBtn.onClick.AddListener(Close);
    }

    public void SetData()
    {
        
        
        
    }
    
    protected override void OpenAnimation()
    {
    }


    protected override void AddBgMask()
    {
    }

    
    
}
