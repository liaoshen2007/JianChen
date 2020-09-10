using System.Collections;
using System.Collections.Generic;
using DataModel;
using UnityEngine;
using UnityEngine.UI;

public class GameTaskItem : MonoBehaviour
{
    
    private Text _taskItemtext;
    private Button _taskBtn;
    private UserMissionVo _curvo;
    
    
    private void Awake()
    {
        _taskItemtext = transform.GetText("BG/Info");
        _taskBtn = transform.GetButton();
        _taskBtn.onClick.AddListener(OnTaskClick);
    }

    private void OnTaskClick()
    {
        Debug.Log("Focus on this task:"+_curvo.MissionId);
    }

    public void SetData(UserMissionVo vo)
    {
        _curvo = vo;
        var rule = GlobalData.MissionData.MissionRuleDic[vo.MissionId];
        _taskItemtext.text = rule.MissionName;
        



    }
    
    
}
