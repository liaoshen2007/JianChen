using Common;
using UnityEngine;
using UnityEngine.UI;

namespace Components
{
	public class BackBtnComponent:MonoBehaviour
	{
		public delegate void BackClickDelegate();

		public BackClickDelegate OnBackClick;

		public void Start()
		{
			// this.transform.localPosition=new Vector3(30,-24);
			gameObject.AddComponent<ButtonSound>().SoundName = "03.Hit";
			this.GetComponent<Button>().onClick.AddListener(delegate()
			{
				OnBackClick.Invoke();
			});
		}

	}
}