using UnityEngine;
using UnityEngine.UI;
using System;
using FrameWork.JianChen.Core;

public class SkillView : View
{
    private Button m_CloseBtn = null;

    void Awake()
    {
        m_CloseBtn = transform.FindChild("SkillPanel/CloseBtn").GetComponent<Button>();
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
