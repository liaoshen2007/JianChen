using UnityEngine;
using UnityEngine.UI;
using System;
using FrameWork.JianChen.Core;

public class BagViewView : View
{
    private Text m_Text = null;
    private Button m_CloseBtn = null;

    void Awake()
    {
        m_Text = transform.FindChild("BagPanel/GoldBtm/Text").GetComponent<Text>();
        m_CloseBtn = transform.FindChild("BagPanel/CloseBtn").GetComponent<Button>();
    }

    void Start()
    {
        InitUIEvent();
    }

    private void InitUIEvent()
    {
        m_CloseBtn.onClick.AddListener(OnCloseBtnClick);
    }

    private void OnCloseBtnClick()
    {
        throw new NotImplementedException();
    }
}
