using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Common;
using DataModel;
using FrameWork.JianChen.Core.Event;
using game.main;
using LitJson;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{

	public static Loading instance;
	public AsyncOperation _curprogress;
	private int mCurProgress = 0;

	private Text _progress;

	private ProgressBar _progressBar;
	// Use this for initialization
	void Start ()
	{
		instance = this;
		this.gameObject.SetActive(false);
		_progress = transform.Find("Text").GetComponent<Text>();
		_progressBar = transform.Find("ProgressBar").GetComponent<ProgressBar>();
		DontDestroyOnLoad(this.transform.parent.gameObject);
	}


	//下周回来继续优化！
	public void LoadingScene(string sceneName,Action action=null)
	{
		this.gameObject.SetActive(true);
		EventDispatcher.TriggerEvent(EventConst.UnLoadModel);//要先回收之前的NPC模型！
		_progress.text = "0%";
		_progressBar.Progress = 0;
//		_curprogress = SceneManager.LoadSceneAsync(sceneName);
		
		GlobalData.SceneData.LoadData(sceneName, () =>
		{
			StartCoroutine(LoadScene(sceneName,action));
		});


	}

	public IEnumerator LoadScene(string sceneName,Action action=null)
	{
		//this.gameObject.SetActive(true);
		_curprogress = SceneManager.LoadSceneAsync("Scenes/"+sceneName);
		// 不允许加载完毕自动切换场景，因为有时候加载太快了就看不到加载进度条UI效果了
		_curprogress.allowSceneActivation = false;
		// mAsyncOperation.progress测试只有0和0.9(其实只有固定的0.89...)
		// 所以大概大于0.8就当是加载完成了
		while (!_curprogress.isDone && _curprogress.progress < 0.8f)
		{
			yield return _curprogress;
		}
		//SceneManager.LoadSceneAsync(sceneName).completed += OnLoadScene;
		EventDispatcher.TriggerEvent(EventConst.UpdateMapName,sceneName);
		_curprogress.completed += OnLoadScene;
		action?.Invoke();
	}

	private void OnLoadScene(AsyncOperation obj)
	{
		//Debug.LogError(obj.progress.ToString(CultureInfo.InvariantCulture));
		//_progress.text = obj.progress.ToString(CultureInfo.InvariantCulture);
		if (obj.progress>=0.9f)
		{
			//要先确认摄像头系统是有人！才可以隐藏loading界面
			_progress.text = "%"+100;
			_progressBar.Progress = 1;
			
			AudioManager.Instance.PlayBGMbyname("tri_lnplay");
			EventDispatcher.TriggerEvent(EventConst.LoadModel);
		}
	}

	/// <summary>
	/// 加载完NPC的时候记得要调用隐藏Loading!
	/// </summary>
	public void OnHideLoadingView()
	{
//		Debug.LogError("HideLoadingView");
		this.gameObject.SetActive(false);
	}

	private void Update()
	{
		int progressBar = 0;
		if (_curprogress.progress < 0.8) 
			progressBar = (int)(_curprogress.progress * 100);
		else 
			progressBar = 100;


//		if (_curprogress!=null&&_curprogress.progress>0f)
//		{
//			 Debug.LogError(_curprogress.progress);
//			_progress.text = _curprogress.progress.ToString(CultureInfo.InvariantCulture);
//		}
		if (mCurProgress <= progressBar)
		{
			_progress.text = "%"+mCurProgress;
			_progressBar.Progress=(int)((float)mCurProgress/100*100);
			mCurProgress++;	
			// 进度条ui显示（本文不讨论） 
			//((Win_Loading)UIWindowCtrl.GetInstance().GetCurrentWindow()).loadingView.SetLoadSceneInfo(mCurProgress * 0.01f);
		}
		else
		{
			// 必须等进度条跑到100%才允许切换到下一场景
			if (progressBar == 100) _curprogress.allowSceneActivation = true;
		}

		
		
	}
}
