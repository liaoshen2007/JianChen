using System.Collections;
using System.Collections.Generic;
using FrameWork.JianChen.Core;
using UnityEngine;
using UnityEngine.UI;

public class NPCRoleSingleEntity : EntityView
{

    public NPCData npcData;
    public Transform targetCamPos;
    public Text _nameTxt;
    
    private void Awake()
    {
        targetCamPos = transform.Find("FacePos");
        _nameTxt = transform.Find("Hub/Canvas/NameTex").GetText();
    }


    public void SetNpcData(NPCData npcdata)
    {
        Debug.Log(npcdata.Name);
        npcData = npcdata;
        _nameTxt.text = npcdata.Name;
    }


}
