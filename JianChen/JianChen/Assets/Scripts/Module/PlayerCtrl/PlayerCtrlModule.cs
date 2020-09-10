using FrameWork.JianChen.Core;

public class PlayerCtrlModule : ModuleBase {
	
	 public override void Init()
    {
        
    }

    public override void OnMessage(Message message)
    {
        string name = message.Name;
        object[] body = message.Params;
        switch (name)
        {
            
        }
    }
	
    public void Destroy()
    {
		
    }
}
