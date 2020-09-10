using UnityEngine;
using UnityEngine.UI;

namespace game.main
{
	public class ConfirmWindow : AlertWindow
	{
		[SerializeField] private Button _cancelBtn;
        
		public string CancelText
		{
			get
			{
				return _cancelBtn.GetComponentInChildren<Text>().text;
			}
			set
			{
				_cancelBtn.GetComponentInChildren<Text>().text = value;
			}
		}

		protected override void OnInit()
		{
			base.OnInit();
			_cancelBtn.onClick.AddListener(OnCancelBtn);
		}

		protected void OnCancelBtn()
		{
			WindowEvent = WindowEvent.Cancel;
			Close();
		}
	}
}
