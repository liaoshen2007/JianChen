using UnityEngine;
using UnityEngine.UI;
using System;
using FrameWork.JianChen.Core;

public class MissionView : View
{
    private Button m_CloseBtn = null;
    private Transform m_MissionList = null;
    private Transform m_ToggleList = null;
    private Text m_Text = null;

    void Awake()
    {
        m_CloseBtn = transform.FindChild("MissionPanel/CloseBtn").GetComponent<Button>();
        m_MissionList = transform.FindChild("MissionPanel/MissionList").GetComponent<Transform>();
        m_ToggleList = transform.FindChild("MissionPanel/ToggleList").GetComponent<Transform>();
        m_Text = transform.FindChild("MissionPanel/Panel/Text").GetComponent<Text>();
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
