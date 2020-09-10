

using FrameWork.JianChen.Core;
using FrameWork.JianChen.Interfaces;

namespace Module.Login
{
	public class GameMainPanel : Panel
	{
		private GameMainController _mainControl;
//		private UserInfoController _userInfoController;

		public override void Init(IModule module)
		{
			base.Init(module);

			var viewScript = (GameMainView)InstantiateView<GameMainView>("GameMain/Prefabs/GameMainView");
          
			_mainControl = new GameMainController();
			_mainControl.view = viewScript;
            
//			_userInfoController = new UserInfoController();
//			_userInfoController.View = viewScript.UserInfoView;

			RegisterController(_mainControl);
//			RegisterController(_userInfoController);
            
			_mainControl.Start();

		}

		public override void Hide()
		{
			ClientTimer.Instance.DelayCall(Destroy, 0.4f);
		}

		public override void Show(float delay = 0.1f)
		{
            
            
		}

	}
}