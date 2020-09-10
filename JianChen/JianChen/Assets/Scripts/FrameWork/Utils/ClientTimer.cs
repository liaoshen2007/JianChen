using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TimerHandler
{

    public string Name { get; private set; }
    public Action<int> StepFunction { get; private set; }
    public Action FinishFunction { get; private set; }
    public float Interval { get; private set; }
    public long Finish { get; private set; }
    public float IntervalTime { get; private set; }

    public TimerHandler(string name, long finish, float interval, Action<int> stepFunction, Action finishFunction)
    {
        Name = name;
        StepFunction = stepFunction;
        FinishFunction = finishFunction;
        Interval = interval;
        Finish = finish;
        IntervalTime = 0;
    }

    public bool TryExecute(float delta)
    {
        IntervalTime += delta;
        long currentTimeStamp = ClientTimer.Instance.GetCurrentTimeStamp();
        if (IntervalTime >= Interval)
        {
            int millSecond = (int)(Finish - currentTimeStamp);
            if (StepFunction != null)
                StepFunction(millSecond);
            IntervalTime = IntervalTime - Interval;
        }
        if (currentTimeStamp >= Finish)
        {
            if (FinishFunction != null)
                FinishFunction();
            return true;
        }
        return false;
    }
}

public class ClientTimer : MonoBehaviour
{
    private long _serverTime;
    /// <summary>
    /// 设置服务器时间的本地时刻
    /// </summary>
    private float _serverTimeStart;

    List<TimerHandler> timers = new List<TimerHandler>();

    private static ClientTimer instance;
    public static ClientTimer Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        instance = this;
        Application.targetFrameRate = 30;
    }

    List<TimerHandler> removeHandler = new List<TimerHandler>();//修改主要是解决bug 调用RemoveCountDown当用foreach遍历Collection时，如果对Collection有Add或者Remove操作时，会发生以下运行时错误：
//"Collection was modified; enumeration operation may not execute."：

    void Update()
    {
        if (timers == null || timers.Count == 0) return;
        for(int i= timers.Count-1; i>=0;i--)
        {
            if (timers[i].TryExecute(Time.deltaTime))
            {
                timers.Remove(timers[i]);
            }
        }
    }

    private TimerHandler GetTimer(string name)
    {
        List<TimerHandler> timers = instance.timers;
        foreach (var timer in timers)
        {
            if (timer.Name == name)
                return timer;
        }
        return null;
    }

    public void InitTimeStamp(long serverTime)
    {
        _serverTime = serverTime;
        _serverTimeStart = Time.realtimeSinceStartup;
    }

    /// <summary>
    /// 当前服务器时间戳
    /// </summary>
    /// <returns></returns>
    public long GetCurrentTimeStamp()
    {
        long time = _serverTime + (long) (Time.realtimeSinceStartup - _serverTimeStart) * 1000;
        return time;
    }

    public TimerHandler AddCountDown(string name, long finish, float interval, Action<int> stepCallback, Action finishCallback)
    {
        if (finish < GetCurrentTimeStamp())
        {
            return null;
        }

        var action = new TimerHandler(name, finish, interval, stepCallback, finishCallback);

        TimerHandler timer = GetTimer(name);
        if (timer != null)
        {
            timers.Remove(timer);
        }

        timers.Add(action);
        
        return action;
    }

    public string TransformTime(long lefTime)
    {
        long s = (lefTime/1000) % 60;
        long m = (lefTime / (60 * 1000)) % 60;
        long h = lefTime / (60 * 60*1000);
        return string.Format("{0:D2}时{1:D2}分{2:D2}秒", h, m, s);
    }

    public long GetCurHour(long lefttime)
    {
        //Debug.LogError("获得小时！"+lefttime / (60 * 60 * 1000));
        return lefttime / (60 * 60 * 1000);
    }

    public void RemoveCountDown(TimerHandler handler)
    {
        var timer = GetTimer(handler.Name);
        if (timer != null)
        {
            timers.Remove(timer);
        }
    }

    public void RemoveCountDown(string name)
    {
        var timer = GetTimer(name);
        if (timer != null)
        {
            timers.Remove(timer);
        }
    }

    public void Cleanup()
    {
        timers = new List<TimerHandler>();
        CancelInvoke("socketInvoker");
        CancelInvoke("socketInvoker2");
    }

    //added by eric.yin

    private Action _invokeCall;
    private Action _invokeCall2;
    
    
    

    /// <summary>
    /// 
    /// </summary>
    /// <param name="invokeCall"></param>
    /// <param name="time"></param>
    public void AddSocketInvokeRepeating(Action invokeCall, int time)
    {
        _invokeCall = invokeCall;
        CancelInvoke("socketInvoker");
        InvokeRepeating("socketInvoker", time, time);
    }

    public void AddSocketInvokeRepeating2(Action invokeCall, int time)
    {
        _invokeCall2 = invokeCall;
        CancelInvoke("socketInvoker2");
        InvokeRepeating("socketInvoker2", time, time);
    }

    public void CancelInvokeRepeationg()
    {
        CancelInvoke("socketInvoker");
    }
    public void CancelInvokeRepeationg2()
    {
        CancelInvoke("socketInvoker2");
    }

    private void socketInvoker()
    {
        if (_invokeCall != null)
        {
            _invokeCall.Invoke();
        }
    }
    private void socketInvoker2()
    {
        if (_invokeCall2 != null)
        {
            _invokeCall2.Invoke();
        }
    }
    public Coroutine DelayCall<T>(Action<T> callback, float time, T data)
    {
        return StartCoroutine(DoDelayCall(time, callback, data));
    }
    public Coroutine DelayCall(Action callback, float time)
    {
         return StartCoroutine(DoDelayCall(time, callback));
    }

    private IEnumerator GetTaskCall(Task task,Action success,Action err)
    {
        var time = 10f;
        while(task.IsCompleted==false&&time>0)
        {
            time -= Time.deltaTime;
            yield return null;
        }

        if (time>0&&task.IsCompleted)
        {
            LoadingOverlay.Instance.Hide();
            success();
        }
        else
        {
            Debug.LogError("Delay too long!"+time);
            if (time<0)
            {
                err?.Invoke(); 
            }

        }
    }
    
    private IEnumerator GetTaskCall<T>(Task<T> task,Action<T> success,Action err)
    {
        var time = 10f;
        while(task.IsCompleted==false&&time>0)
        {
            time -= Time.deltaTime;
            yield return null;
        }

        if (time>0&&task.IsCompleted)
        { 
            LoadingOverlay.Instance.Hide();
            success(task.Result);
        }
        else
        {
            Debug.LogError("Delay too long!"+time);
            if (time<0)
            {
                err?.Invoke(); 
            }

        }
    }

    public void GetTaskCallBack(Task task,Action success,Action err=null)
    {
        if (task!=null)
        {            
            StartCoroutine(GetTaskCall(task,success,err)); 
        } 
    }

    public void GetTaskCallBack<T>(Task<T> task,Action<T> success,Action error=null)
    {
        if (task!=null)
        {
            StartCoroutine(GetTaskCall(task,success,error));  
        }


    }
    
//    public Coroutine NewDelayCall<T>(Action<T> callback, float time, T data)
//    {
//        return StartCoroutine(DoDelayCall(time, callback, data));
//    }
//    //xys定时器存在bug  添加新方法救急
//    public Coroutine NewDelayCall(Action callback, float time)
//    {
//         return StartCoroutine(DoDelayCall(time, callback));
//    }
//    //xys定时器存在bug   添加新方法救急
//    public void NewCancelDelayCall(Coroutine coroutine)
//    {
//        Debug.Log("NewCancelDelayCall1");
//        // if (instance && instance.transform.parent != null)
//        if (instance)
//        {  
//            Debug.Log("NewCancelDelayCall2");
//            StopCoroutine(coroutine);
//        }
//    }

    public void GetCall(IEnumerator tor)
    {

        if (tor!=null)
        {
            StartCoroutine(tor);
        }
    }
    
    public void CancelDelayCall(Coroutine coroutine)
    {
        if(coroutine != null)
            StopCoroutine(coroutine);
    }

    IEnumerator DoDelayCall<T>(float delay, Action<T> callback, T data)
    {
        yield return new WaitForSeconds(delay);
        if (this != null && callback != null)
            callback(data);
    }
    IEnumerator DoDelayCall(float delay, Action callback)
    {
        yield return new WaitForSeconds(delay);
        if (this != null && callback != null)
        {
            try
            {
                callback();
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex + " callback:" + callback.ToString());
            }
        }

    }

    public void StartOverlayCall(bool needresult,Action callback,float delayTime)
    {
        StartCoroutine(OverlayTimeCall(needresult, callback, delayTime));
    }
    
    /// <summary>
    /// 超时等待一般都是放在函数的最后一行
    /// </summary>
    /// <param name="needresult"></param>
    /// <param name="callback"></param>
    /// <param name="delayTime"></param>
    /// <returns></returns>
    public IEnumerator OverlayTimeCall(bool needresult,Action callback,float delayTime)
    {
        float timer = 0f;
        while (!needresult&&timer<delayTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        if (timer>delayTime)
        {
            Debug.LogError("OverTime!");
            yield break;
        }
        callback?.Invoke();
    }
    
}
