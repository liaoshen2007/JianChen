using System;
using DataModel;
using FrameWork.JianChen.Core;
using game.main;

public class SkillController : Controller
{

    public SkillView View;

    public override void Start()
    {
        var targetSkillList = GlobalData.SkillModel.GetTargetSkillListItem(Occupation.All);
        View.SetData(targetSkillList);
    }

    public override void OnMessage(Message message)
    {
        string name = message.Name;
        object[] body = message.Params;
        switch (name)
        {

        }
    }

    public override void Destroy()
    {
        base.Destroy();
    }
}
