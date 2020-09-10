using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Framework.JianChen.Service;
using UnityEngine;
using UnityEngine.UI;

public class SkillItem : MonoBehaviour
{
    private RawImage _skillIcon;
    private Text _skillName;
    private Text _skillType;
    private Text _skillDesc;
    private Text _costMP;
    private SkillBaseData _skillBaseData;
    private SkillIconMono _skillIconMono;
    
    
    private void Awake()
    {
        _skillIcon = transform.GetRawImage("Frame/SkillItem");
        _skillName = transform.GetText("SkillNameBtm/SkillName");
        _skillType = transform.GetText("SkillNameBtm/SkillType");
        _skillDesc = transform.GetText("SkillInfoBtm/Desc");
        _costMP = transform.GetText("SkillInfoBtm/Cost");
        _skillIconMono = _skillIcon.GetComponent<SkillIconMono>();

    }

    public void SetData(SkillBaseData data)
    {
        _skillBaseData = data;
        _skillIcon.texture=ResourceManager.Load<Texture>("SkillIcon/"+data.SkillIcon);
        _skillName.text = data.SkillName;
        _skillType.text = data.SkillType + "";
        _skillDesc.text = data.SkillDesc;
        _costMP.text = "MP 0";
        _skillIconMono.SetData(transform.parent.transform.parent.transform.parent,data.SkillId);



    }
    
    
}
