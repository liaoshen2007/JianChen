using Common;
using DataModel;
using FrameWork.JianChen.Core;
using FrameWork.JianChen.Core.Event;
using game.main;
using Module.Login;

public class GameMainModule : ModuleBase
{
	private GameMainPanel _mainMenuPanel;

	public override void Init()
	{
		_mainMenuPanel = new GameMainPanel();
		_mainMenuPanel.Init(this);
        
//		RegisterModel<PlayerModel>();
//        
//		EventDispatcher.AddEventListener<MainMenuDisplayState>(EventConst.MainMenuDisplayChange, OnMainMenuDisplayChange);
//        
//		GuideManager.RegisterModule(this);
	}


	public override void OnShow(float delay)
	{      
		base.OnShow(delay);
		//SendMessage(new Message(MessageConst.CMD_MAIN_CHANGE_DISPLAY, MainUIDisplayState.ShowAll));
		Main.ChangeMenu(MainUIDisplayState.ShowAll);

		Main.EnableBackKey = true;
        
		AssetManager.Instance.UnloadSingleFileBundleLater();
		AssetManager.Instance.LogMessage();

//		AssetLoader.UnloadAllAudio();
//        
//		_mainMenuPanel.OnShow();
//        
//		GuideManager.OpenGuide(this);
	}

	private void OnMainMenuDisplayChange(MainUIDisplayState state)
	{
		SendMessage(new Message(MessageConst.CMD_MAIN_CHANGE_DISPLAY, state));
	}
}