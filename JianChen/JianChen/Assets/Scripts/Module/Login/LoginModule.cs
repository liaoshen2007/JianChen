using FrameWork.JianChen.Core;
using FrameWork.JianChen.Interfaces;

public class LoginModule : ModuleBase,IPopup
{
    private LoginPanel _loginPanel;
    
    public override void Init()
    {
        _loginPanel=new LoginPanel();
        _loginPanel.Init(this);
        _loginPanel.Show(0);
    }

    public void Close()
    {
        
    }

    public override void Remove(float delay)
    {
        base.Remove(0);
        _loginPanel.Destroy();
    }
    

    public void Destroy()
    {
        _loginPanel.Hide();
    }
}