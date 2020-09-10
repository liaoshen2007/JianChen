//using DG.Tweening;

using System.Collections.Generic;
using Assets.Scripts.Framework.JianChen.Service;
using Common;
using DataModel;
using DG.Tweening;
using FrameWork.JianChen.Core;
using FrameWork.JianChen.Core.Event;
using game.main;
using UnityEngine;
using UnityEngine.UI;

public class EnemyRoleSingleEntity : EntityView
{

    public EnemyData Enemydata;
    private TimerHandler _idleState;
    private Animator _animator;
    public float moveSpeed = 1f;
    private Vector3 oriPos;
    private Vector3 patrolPos;
    private EnemyState curState;
    
    //之后,target要变成List!
    private Transform target;
    //private Sequence _sequence;
    private CharacterController _characterController;
    private float distance=1.5f;

    private AnimationEventReceiver _animationEventReceiver;
    
    private ProgressBar _hpBar;
    private Text _hptex;
    private Text _enemyName;
    public int[] damageArray = new[] {10};
    public string EffectName = "1";

    private int _curHp;

    private Queue<string> _poemItem;
    //private Queue<string> _outpoemItems;
    

    private void Awake()
    {
        _idleState=ClientTimer.Instance.AddCountDown("IdleState", long.MaxValue, 4f, IdleState, null);
        _animator = transform.Find("Model").GetComponent<Animator>();
        _characterController = transform.GetComponent<CharacterController>();
        _animationEventReceiver = transform.Find("Model").GetComponent<AnimationEventReceiver>();
        _hpBar = transform.Find("Hub/Canvas/BloodBar").GetComponent<ProgressBar>();
        _hptex = transform.Find("Hub/Canvas/BloodTex").GetComponent<Text>();
        _enemyName = transform.Find("Hub/Canvas/NameTex").GetComponent<Text>();
        _animationEventReceiver.AniEvent = NormalAniEvent;
        //oriPos = transform.position;

    }

    private void IdleState(int timeStep)
    {
        if (curState!=EnemyState.idle)
        {
            return;
        }
        
        //Debug.Log("Call enemy to run");
        float ranx = Random.Range(-100, 100)*0.03f;
        float ranz = Random.Range(-100, 100)*0.03f;
        
        patrolPos=new Vector3(oriPos.x+ranx,oriPos.y,oriPos.z+ranz);

//        transform.LookAt(patrolPos);
//        if (_animator != null) _animator.SetFloat("Speed", moveSpeed);
//        
//        Tweener enemyMove = transform.DOLocalMove(patrolPos, 2f).SetEase(DG.Tweening.Ease.OutSine);
//        _sequence=DOTween.Sequence().Append(enemyMove).OnComplete(() =>
//        {
//            _animator.SetFloat("Speed", 0f);
//        });

    }
    
    private void NormalAniEvent()
    {
        //Debug.Log("NormalAniEventPlayer3DExample");
//        var effect=EntityManager.Instance.SpawnSkillEffect(EffectName);
//        effect.transform.SetParent(transform,false);
//        effect.transform.SetParent(entitytran);
//        SkillPlayer skill = effect.GetComponent<SkillPlayer>();
        
        var skilleffect = (SkillPlayer)PoolManager.Instance.TryGetEntity(string.Format("3DModel/SkillEffect/Skill{0}",EffectName));
        if (skilleffect == null)
        {
            skilleffect=EntityManager.Instance.SpawnSkillEffect(EffectName).GetComponent<SkillPlayer>(); 
            
        }
        skilleffect.transform.SetParent(transform,false);
        //skilleffect.transform.SetParent(EntityManager.Instance.transform);//挂到一个统一的特效节点下面去!
        skilleffect.PlayerEffect(transform.name,damageArray,2);
    }

    public void HasBeenAttack(int damage)
    {
        //Debug.Log("need to reduceblood:"+damage);
        

        _curHp = _curHp-damage;
        if (_curHp <= 0)
        {
            _curHp = 0;
            _animator.SetTrigger("Death");  
            curState = EnemyState.Dead;
            
            //死后要隐藏掉！
            //死掉的时候有概率生成诗句
            SpawnPoemClip();
        }
        else
        {
            _animator.SetTrigger("Damage");
            ShowDamageHud(damage);

        }
        
        CreatPoemParts();

        _hptex.text = _curHp+"/"+Enemydata.HP*2;
        _hpBar.Progress = (int)((float)_curHp/(Enemydata.HP*2)*100);;
        
        
    }

    private void CreatPoemParts()
    {
        if (_poemItem.Count > 0)
        {
            string creatPoem = _poemItem.Dequeue();
            EventDispatcher.TriggerEvent(EventConst.SetPoemItemStr,creatPoem);   
        }
    }


    private void ShowDamageHud(int damage)
    {
        var damagehud = (DamageHud)PoolManager.Instance.TryGetEntity("3DModel/SkillEffect/DamageHudEffect");
        if (damagehud == null)
        {
            damagehud=EntityManager.Instance.SpawnDamageHud().GetComponent<DamageHud>(); 
            
        }
        damagehud.transform.SetParent(transform,false);
        damagehud.SetTextTween(damage);
        damagehud.transform.SetParent(EntityManager.Instance.transform);//挂到一个统一的特效节点下面去!

    }

    private void SpawnPoemClip()
    {
        var poemprop = (PoemEntity)PoolManager.Instance.TryGetEntity("3DModel/PropModel/Prefabs/Poem");
        if (poemprop == null)
        {
            poemprop=EntityManager.Instance.SpawnPoemClip().GetComponent<PoemEntity>(); 
            
        }
        //poemprop.transform.SetParent(transform,false);
        var poempos=poemprop.transform;
        poempos.position = transform.position;
        poemprop.GetComponent<ParabolaMotion>().SetRandomData(poempos.position);

        poemprop.transform.SetParent(EntityManager.Instance.transform);//挂到一个统一的特效节点下面去!
        




        //TestDotween(poemprop);
    }

    private void TestDotween(PoemEntity poemprop)
    {
        var targetMoveZ = poemprop.transform.localPosition.z + Random.Range(-5, 5);
        var lasttargetMoveY = poemprop.transform.localPosition.y+0.01f;
        var jumpHight = poemprop.transform.localPosition.y + 3;
        
        Tweener move1=poemprop.transform.DOLocalMoveZ(targetMoveZ, 1f).SetEase(DG.Tweening.Ease.OutSine);
        Tweener move3=poemprop.transform.DOLocalMoveY(lasttargetMoveY, 0.5f).SetEase(DG.Tweening.Ease.OutSine);
        Tweener move2=poemprop.transform.DOLocalMoveY(jumpHight, 0.5f).SetEase(DG.Tweening.Ease.OutSine);


        //要做一个抛物线运动！
        DOTween.Sequence().Append(move2).Append(move3).OnComplete(() =>
        {
            Debug.LogError("tween over1!");
        });
        
        DOTween.Sequence().Append(move1).OnComplete(() =>
        {
            Debug.LogError("tween over2!");
        });
    }
    

    public void SetTarget(Transform tran)
    {
        Debug.Log("enemy find player:"+tran.name);

        if (curState != EnemyState.Dead)
        {
            curState = EnemyState.runtoTarget;
            target = tran;
            EventDispatcher.TriggerEvent<bool>(EventConst.AwakePoemState,true);
        }

        //_sequence.Kill();
    }

    public void CallBacKtoOri(Transform tran)
    {
        if (curState == EnemyState.Dead)
            return;
        
        
        //逃跑的时候要激活玩家消失了诗句片段！
        EventDispatcher.TriggerEvent<bool>(EventConst.AwakePoemState,false);
        
        //这个可能会因为指针的问题失效！
        //_outpoemItems = _poemItem;
        _poemItem=GlobalData.PoemGameData.GetRandomPoemParts(Enemydata.Poem);
        
        curState = EnemyState.GoBack;
        if (tran==target)
        {
            target = null;
        }
        
    }

    public void RunBackToOri()
    {
        //离开区域的时候会跑回区域点！
        MoveToTarget(oriPos,EnemyState.idle);
    }

    

    public void SetEnemyData(EnemyData enemyData)
    {
        Debug.Log(enemyData.Name);
        Enemydata = enemyData;
        
        //这个数据要重设！
        _poemItem = GlobalData.PoemGameData.GetRandomPoemParts(enemyData.Poem);
        //_outpoemItems = _poemItem;
        oriPos = new Vector3((float)Enemydata.SpawnPos.PosX,(float)Enemydata.SpawnPos.PosY,(float)Enemydata.SpawnPos.PosZ);
        _enemyName.text = enemyData.Name;
        _curHp = enemyData.HP * 2;
        _hptex.text = enemyData.HP*2+"/"+enemyData.HP*2;
        _hpBar.Progress = (int)((float)_curHp/(Enemydata.HP*2)*100);
        patrolPos = oriPos;
        curState = EnemyState.idle;
        //Debug.LogError(oriPos);
    }

    private void MoveToTarget(Vector3 targetPos,EnemyState laState)
    {
        var offset = (targetPos - transform.position);
        if (offset.sqrMagnitude > 0.01f&&Vector3.Distance(targetPos,transform.position)>distance)
        {
            transform.LookAt(targetPos);
            _animator.SetFloat("Speed", moveSpeed);
            offset = offset.normalized * moveSpeed;
            _characterController.Move(offset*Time.deltaTime);
        }
        else
        {
            //靠近目标的时候，可以开始攻击了！
            if (curState == EnemyState.Dead)
                return;
            
            
            curState=laState;
            _animator.SetFloat("Speed", 0f);
        }
        

    }

    private void HitToTarget(Vector3 targetPos)
    {
        //在攻击范围内，播放攻击动画！
        
        if (curState == EnemyState.Dead)
            return;

        if (Vector3.Distance(targetPos,transform.position)<=distance)
        {
            _animator.SetTrigger("EasyHit");
            ClientTimer.Instance.DelayCall(() =>
            {
                if (target!=null)
                {
                    HitToTarget(target.position);
                }

            }, 3f);//攻击频率是3！
            curState = EnemyState.hitting;
        }
        else
        {
            curState = EnemyState.runtoTarget;
        }
    }
    
    
    //todo 要重构curState的赋值状态
    private void Update()
    {
        
        //这里写状态机
        switch (curState)
        {
            case EnemyState.idle:
                MoveToTarget(patrolPos, EnemyState.idle);
                break;
            case EnemyState.runtoTarget:
                //todo laststate应该是attack的！
                if (target!=null)
                {
                    MoveToTarget(target.position, EnemyState.hit); 
                }
                
                break;
            case EnemyState.hit:
                HitToTarget(target.position);
                break;
            case EnemyState.Damage:
            
                break;
            case EnemyState.GoBack:
                //离开区域了，赶快返回到初始位置！
                RunBackToOri();
                break;
            case EnemyState.Dead:
                
                break;
            default:
                break;
        }
        
    }

    private void OnDestroy()
    {
        ClientTimer.Instance.RemoveCountDown(_idleState);
    }
}

public enum EnemyState
{
    idle,
    runtoTarget,
    hit,
    GoBack,
    hitting,
    Damage,
    Dead
}
