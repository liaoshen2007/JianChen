using System;
using FrameWork.JianChen.Core;

public class NPCPreviewModule : ModuleBase
{
    NPCPreviewPanel _npcpreviewpanel;
    private NPCData _targetNpcData;

    public override void Init()
    {
        _npcpreviewpanel = new NPCPreviewPanel();
        _npcpreviewpanel.SetData(_targetNpcData);
        _npcpreviewpanel.Init(this);
        _npcpreviewpanel.Show(0);
    }

    public override void OnShow(float delay)
    {
        base.OnShow(delay);
        _npcpreviewpanel.Show(0);
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
            _targetNpcData = (NPCData) paramsObjects[0];

        }
        
        
    }
}
