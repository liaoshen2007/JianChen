using System;
using FrameWork.JianChen.Core;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace game.main
{
	public enum WindowEvent
	{
		Null,
		Ok,
		Cancel,
		Yes,
		No,
		ClickOutsideToClose
	}
	
    public abstract class Window : View
    {
	    
	    public string AssetName;
	    public Action<WindowEvent> WindowActionCallback;

	    protected WindowEvent WindowEvent = WindowEvent.Null;
	    
	    protected GameObject BgMask;

	    private readonly float SamllScale = 0.3f;

	    public virtual void OnOpen()
	    {
	        OnInit();
		    
	        OpenAnimation();
	    }

	    protected virtual void OnInit()
	    {
	        AddBgMask();
	    }

	    protected virtual void AddBgMask()
	    {
	        BgMask = new GameObject("windowBg");

		    RawImage image = BgMask.AddComponent<RawImage>();
            image.color = new Color(1,1,1,0.0f);
            BgMask.transform.SetParent(transform.parent, false);
	        UIEventListener.Get(BgMask).onClick = OnClickOutside;

		    image.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
		    image.rectTransform.localPosition = new Vector3(0,0,0);
	        BgMask.transform.SetAsFirstSibling();
        }

	    public float MaskAlpha
	    {
		    set
		    {
			    if(BgMask == null)
				    return;
			    RawImage image = BgMask.GetComponent<RawImage>();
			    image.color = new Color(1,1,1,value);
		    }
		    get
		    {
			    if(BgMask == null)
				    return -1.0f;
			    return BgMask.GetComponent<RawImage>().color.a;
		    }
	    }

	    /// <summary>
	    /// 重载这个方法去掉点击空白区域关闭
	    /// </summary>
	    /// <param name="go"></param>
	    protected virtual void OnClickOutside(GameObject go)
	    {
		    WindowEvent = WindowEvent.ClickOutsideToClose;
            RemoveMask();
            Close();
        }

        private void RemoveMask()
        {
            if (BgMask != null)
            {
                UIEventListener.Get(BgMask).onClick = null;
                Destroy(BgMask);
                BgMask = null;
            }
        }

        public virtual void Close()
	    {
		    if(WindowEvent == WindowEvent.Null)
			    WindowEvent = WindowEvent.Cancel;
            CloseAnimation();
	    }

	    protected virtual void OpenAnimation()
	    {
	        CanvasGroup cg = gameObject.AddScriptComponent<CanvasGroup>();
	        cg.alpha = 0;
	        DOTween.To(() => cg.alpha, x => cg.alpha = x, 1.0f, 0.15f);
	        gameObject.transform.localScale = new Vector3(SamllScale, SamllScale, 1.0f);
	        gameObject.transform.DOScale(Vector3.one, 0.2f).SetEase(DG.Tweening.Ease.OutExpo);
	    }
        protected virtual void CloseAnimation()
	    {
	        CanvasGroup cg = gameObject.AddScriptComponent<CanvasGroup>();
	        cg.alpha = 1;
	        DOTween.To(() => cg.alpha, x => cg.alpha = x, 0.0f, 0.15f);
            gameObject.transform.DOScale(new Vector3(SamllScale, SamllScale, 1), 0.2f).SetEase(DG.Tweening.Ease.OutExpo).onComplete = DoClose;
	    }

	    protected virtual void DoClose()
	    {
	        RemoveMask();
	        PopupManager.CloseWindow(this);
		    WindowActionCallback?.Invoke(WindowEvent);
        }

    }
}
