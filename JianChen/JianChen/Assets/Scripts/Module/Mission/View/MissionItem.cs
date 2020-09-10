using System.Collections;
using System.Collections.Generic;
using DataModel;
using UnityEngine;
using UnityEngine.UI;

public class MissionItem : MonoBehaviour
{

    private Text _missionText;
    private Button _missionBtn;
    private UserMissionVo _userMissionVo;
    
    
    private void Awake()
    {
        _missionText=transform.GetComponent<Text>();
        _missionBtn = transform.GetComponent<Button>();
        _missionBtn.onClick.AddListener(OnMissionClick);
        
        
    }

    private void OnMissionClick()
    {
        //通知TaskInfo出现信息。
        
        
    }


    public void SetData(UserMissionVo vo)
    {
        _userMissionVo = vo;
        var rule = GlobalData.MissionData.MissionRuleDic[vo.MissionId];
        
        //todo 之后可能会根据任务状态的不同来出现不同的Item图标
        switch (vo.MissionState)
        {
            case MissionState.StatusUnStarted:
                _missionText.text = rule.MissionName;
                break;
            case MissionState.StatusUnsUnfinished:
                _missionText.text = rule.MissionName+"(未完成)";
                break;
            case MissionState.StatusUnclaimed:
                _missionText.text = rule.MissionName+"(可领取)";
                break;
            case MissionState.StatusBeRewardedWith:
                _missionText.text = rule.MissionName;
                break;
            default:
                Debug.Log("New label:"+vo.MissionState);
                break;
        }



    }

    // Update is called once per frame

}
