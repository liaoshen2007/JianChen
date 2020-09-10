using System;
using System.Collections.Generic;
using FrameWork.JianChen.Core;

public class DialogModule : ModuleBase
{
    DialogPanel _dialogpanel;
    private int _missionId=0;
    private NPCData _npcData;
    private int _dialogState = 0;

    public override void Init()
    {
        _dialogpanel = new DialogPanel();
        _dialogpanel.MissionId = _missionId;
        _dialogpanel.NpcData = _npcData;
        _dialogpanel.DialogState = _dialogState;
        _dialogpanel.Init(this);
        _dialogpanel.Show(0);
    }

    public override void OnShow(float delay)
    {
        base.OnShow(delay);
        _dialogpanel.Show(0);
    }

    public override void OnMessage(Message message)
    {
        string name = message.Name;
        object[] body = message.Params;
        switch (name)
        {

        }
    }

    public override void SetData(params object[] paramsObjects)
    {
        if (paramsObjects.Length>0)
        {
            _missionId = (int)paramsObjects[0];
            _npcData=(NPCData)paramsObjects[1];
            _dialogState=(int)paramsObjects[2];

        }
        
        
    }
}
