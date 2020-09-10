using System;
using FrameWork.JianChen.Core;

public class MissionModule : ModuleBase
{
    MissionPanel _missionpanel;

    public override void Init()
    {
        _missionpanel = new MissionPanel();
        _missionpanel.Init(this);
        _missionpanel.Show(0);
    }

    public override void OnShow(float delay)
    {
        base.OnShow(delay);
        _missionpanel.Show(0);
    }

    public override void OnMessage(Message message)
    {
        string name = message.Name;
        object[] body = message.Params;
        switch (name)
        {
            case MessageConst.CMD_MISSION_CLOSE:
                ModuleManager.Instance.GoBack();
                break;
        }
    }
}
