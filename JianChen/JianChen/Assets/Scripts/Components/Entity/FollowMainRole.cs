using Common;
using DG.Tweening;
using FrameWork.JianChen.Core.Event;
using UnityEngine;

public class FollowMainRole : MonoBehaviour {

    public float distanceAway = 10;          // 摄像机距离跟随物体背后的距离
    public float distanceUp = 2;            // 距离物体的最小距离
    public float smooth = 3;                // 摄像机移动平滑指数
    
    public Vector3 Distance=new Vector3(0f,-1f,3f);
    //public Transform follow;             //通过赋值取得物体（1-1）
    private Vector3 targetPosition;     // the position the camera is trying to be in
// 

    Transform follow;
//    public Vector3 offset=new Vector3(10,10,10);

    public CamState CurCamState=CamState.FollowMainRole;

    private Transform _targetNPCTran;
    
    

    private void Start()
    {
        DontDestroyOnLoad(this);

    }
    
    public void SetTargetNPC(Transform tran)
    {
        _targetNPCTran = tran;
    }
    
    public void SetModelCamState(CamState state)
    {

        switch (state)
        {
            case CamState.FollowMainRole:
                var followPos=follow.position - Distance;
                var followRotate=new Vector3(30,0,0);
                Tweener move0 = transform.DOMove(followPos, 0.5f).SetEase(Ease.OutSine);
                Tweener rotate0=transform.DORotate(followRotate,0.5f).SetEase(Ease.OutSine);

                DOTween.Sequence().Append(move0).Join(rotate0).OnComplete(() =>
                {
                    CurCamState = state;
                    Debug.Log("回到正常状态");
                });
                
                break;
            
            //这个需要优化，就是要等玩家跑到NPC面前的时候，才开始摄像头转到NPC！
            case CamState.LOOK_AT_NPC:
                CurCamState = state;
                var targetpos = _targetNPCTran.position;
                var targetrotate = _targetNPCTran.eulerAngles;

                Tweener move1 = transform.DOMove(targetpos, 0.5f).SetEase(Ease.OutSine);
                Tweener rotate1=transform.DORotate(targetrotate,0.5f).SetEase(Ease.OutSine);

                DOTween.Sequence().Append(move1).Join(rotate1).OnComplete(() =>
                {
                    Debug.Log("可以播放NPC自带语音了！");
                });
                
                
                break;
            case CamState.Story:
                break;
            case CamState.Battle:
                
                //算法，求多个点的质心！然后摄像头Focus那个质心。
                
                Debug.LogError("切换至战斗镜头");
                
                break;
            
            
            
        }
        
        
    }
    

    void LateUpdate()
    {
        // 设置追踪目标的坐标作为调整摄像机的偏移量

        switch (CurCamState)
        {
            case CamState.FollowMainRole:
                if (follow==null)
                {
                    follow = Main.TargetRole;
                    //Distance = follow.transform.position - transform.position;
                    return;
                }

                transform.position = follow.position - Distance;
                break;
            case CamState.LOOK_AT_NPC:
                break;
            case CamState.Story:
                break;
        }
        
    }



    public enum CamState
    {
        FollowMainRole,
        LOOK_AT_NPC,
        Story,
        Battle,
    }
    
    
    #region 方向跟随时代码
//    targetPosition = follow.position + Vector3.up * distanceUp - follow.forward * distanceAway;
//
//    // 在摄像机和被追踪物体之间制造一个顺滑的变化
//    transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
//
//    //设置视野中心是目标物体
//    transform.LookAt(follow);
////        transform.LookAt(follow);
////        transform.position = Vector3.Lerp(transform.position, follow.position - offset,Time.deltaTime*5);
    

    #endregion

    
    
}
