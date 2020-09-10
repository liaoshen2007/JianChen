using UnityEngine;
using UnityEngine.UI;
using System;
using FrameWork.JianChen.Core;

public class EquipmentView : View
{
    private Button m_CloseBtn = null;
    private Transform m_EquipItemList = null;

    void Awake()
    {
        m_CloseBtn = transform.FindChild("EquipPanel/CloseBtn").GetComponent<Button>();
        m_EquipItemList = transform.FindChild("EquipPanel/EquipItemList").GetComponent<Transform>();
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
