using System;
using FrameWork.JianChen.Core;

public class SkillModule : ModuleBase
{
    SkillPanel _skillpanel;

    public override void Init()
    {
        _skillpanel = new SkillPanel();
        _skillpanel.Init(this);
        _skillpanel.Show(0);
    }

    public override void OnShow(float delay)
    {
        base.OnShow(delay);
        _skillpanel.Show(0);
    }

    public override void OnMessage(Message message)
    {
        string name = message.Name;
        object[] body = message.Params;
        switch (name)
        {

        }
    }
}
