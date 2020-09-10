using System;
using Assets.Scripts.Framework.JianChen.Service;
using Common;
using DataModel;
using FrameWork.JianChen.Core;
using FrameWork.JianChen.Core.Event;
using game.main;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainRoleSingleEntity : EntityView
{

    private Animator _animator;
    public float moveSpeed = 5f;
    public Joystick joystick;
//    private Canvas _canvas;
    private ProgressBar _blood;
    private Text _bloodTxt;
    private Text _playerRoleName;
    
    private CharacterController _playCharacterController;
    private AnimationEventReceiver _animationEventReceiver;
    public string EffectName = "2";
    public Transform entitytran;
    public int[] damageArray = new[] {10};
    private PlayerVo _curvo;

    private void Awake()
    {
        _animator = transform.Find("Model").GetComponent<Animator>();
        _animationEventReceiver = transform.Find("Model").GetComponent<AnimationEventReceiver>();
//        _canvas = transform.Find("Hub/Canvas").GetComponent<Canvas>();
//        _canvas.worldCamera=Camera.main;
        Main.SetFollowTran(transform);
        _blood = transform.Find("Hub/Canvas/BloodBar").GetComponent<ProgressBar>();
        _bloodTxt = transform.Find("Hub/Canvas/BloodTex").GetComponent<Text>();
        _playerRoleName = transform.Find("Hub/Canvas/NameTex").GetComponent<Text>();
        
        _playCharacterController = transform.GetComponent<CharacterController>();
        _animationEventReceiver.AniEvent = NormalAniEvent;
    }

    public void SetData(PlayerVo vo)
    {
//        Debug.LogError(vo.Exp);
        _curvo = vo;
        _playerRoleName.text = vo.RoleName;
        _bloodTxt.text = vo.HP + "/" + vo.MaxHP;
        _blood.Progress = (int)((float)vo.HP/(vo.MaxHP)*100);

    }
    
    public void HasBeenAttack(int damage)
    {
        GlobalData.PlayerData.PlayerVo.UpdateHP(-damage);

        var curhp = GlobalData.PlayerData.PlayerVo.HP; 
        var maxhp=GlobalData.PlayerData.PlayerVo.MaxHP; 
        
        //玩家也要加状态判断，在死亡和僵直状态是不能移动的！
        if (curhp <= 0)
        {
            //_curHp = 0;
            curhp = 0;
            _animator.SetTrigger("Death");  
            //curState = EnemyState.Dead;
        }
//        else
//        {
//            _animator.SetTrigger("Damage");   
//        }

        _bloodTxt.text = curhp+"/"+maxhp;
        _blood.Progress = (int)((float)curhp/(maxhp)*100);
        
        EventDispatcher.TriggerEvent(EventConst.UpdateUserInfo);
        
        
    }

    public void SetGameState(GlobalGameState state)
    {
        //Debug.LogError(state);
        switch (state)
        {
            case GlobalGameState.Login:
                break;
            case GlobalGameState.Loading:
                if (joystick != null)
                {
                    joystick.OnPointerUp(null);
                }
                break;
            case GlobalGameState.GamePlay:
//                if (joystick != null)
//                {
//                    joystick.enabled = true;
//                }

                break;
            default:
                break;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //Debug.LogError("enter"+other.gameObject.layer+other.gameObject.name);
        //算法，如果是触发到了传送门立刻出现Loading图，并且进入下一个场景
        switch (other.gameObject.layer)
        {
            case 9:
                Debug.Log("enterScene");
                //joystick.enabled = false;
                
                //todo 以下代码需要优化啊！！
                var eventgo=EventSystem.current.gameObject;
                eventgo.gameObject.SetActive(false);
                SendMessage(new Message(MessageConst.CMD_MAINROLE_JUMPTOOTHERSCENE
                    ,Message.MessageReciverType.CONTROLLER,other.gameObject.name));
                eventgo.gameObject.SetActive(true);
//                EventSystem.current.enabled = true;

                //触发跳转场景的事件
                break;
            case 11:
                Debug.Log("mainPlayer");
                //碰撞到其他玩家
                break;
            case 12:
                Debug.Log("NPC");
                //碰撞到NPC，将来可以用来触发气泡对话或者欢迎动作之类的。
                break;
            case 14:
                Debug.Log("Area");
                //todo 要发送AreaId和transform!!
                var area = other.gameObject.GetComponent<Area>();
                EventDispatcher.TriggerEvent(EventConst.HasPlayerEnter,transform,area);
                break;
            default:
                Debug.Log("Tochething:"+other.gameObject.name);
                break;
            
        }
        
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exit:"+other.gameObject.layer+other.gameObject.name);
        //todo 之后玩家离开Area也要通知Enemy
        switch (other.gameObject.layer)
        {
            case 14:
                Debug.Log("Area");
                //todo 要发送AreaId和transform!!
                var area = other.gameObject.GetComponent<Area>();
                EventDispatcher.TriggerEvent(EventConst.GiveUpTargetAndBack, transform, area);
                break;
            default:
                Debug.Log("Tochething:" + other.gameObject.name);
                break; 
        }
        
    }

//    private void OnControllerColliderHit(ControllerColliderHit hit)
//    {
//        Debug.Log("exit:"+hit.gameObject.layer+hit.gameObject.name);
//    }

    public void SetGameMainClickCMD(int btnIndex,int skillIndex)
    {
        switch (btnIndex)
        {
            case 0:
                Debug.Log("_curvo.Equip"+_curvo.Equip);
                
                //可以考虑数值的东西了！
                damageArray=new[] {1000+_curvo.Equip};
                EffectName = "1";
                //todo skillBaseData需要添加Animation名称的字段！
                _animator.SetTrigger("Attack1");//Slider
                break;
            case 1:
                damageArray=new[] {1000+_curvo.Equip};
                EffectName = skillIndex.ToString();
                _animator.SetTrigger("Attack1");//Slider
                break;
            case 2:
                damageArray=new[] {1000+_curvo.Equip};
                EffectName = skillIndex.ToString();
                _animator.SetTrigger("Attack2");//Slider
                break;
            case 3:
                damageArray=new[] {1000+_curvo.Equip};
                EffectName = skillIndex.ToString();
                _animator.SetTrigger("AttackCritical");//Slider
                break;
            
            default:
                break;
            
        }
        
        Debug.Log("GameMainCmd"+btnIndex);
        
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
        //skilleffect.transform.SetParent(transform,false);
        var transform1 = skilleffect.transform;
        var transform2 = transform;
        transform1.position = transform2.position;
        transform1.rotation = transform2.rotation;
        
        
        skilleffect.transform.SetParent(EntityManager.Instance.transform);//挂到一个统一的特效节点下面去!
        skilleffect.PlayerEffect(transform.name,damageArray,2);
    }
    
    

    void Update () 
    {
        if (joystick==null)
        {
            //这个遥控杆是静态的真的很无奈！
            joystick = Main.MainJoystick;
            return;
        }
        
        
        Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);

        if (moveVector != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveVector);
            if (_animator != null) _animator.SetFloat("Speed", moveSpeed);
            //_animator?.SetFloat("Direction",joystick.Horizontal);
            //transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.World);
            //AudioManager.Instance.MoveClipEffect();
            //一定要用move方法来实现碰撞检测！
            _playCharacterController.Move(moveVector * moveSpeed * Time.deltaTime);
        }
        else
        {
            if (_animator != null) _animator.SetFloat("Speed", 0f);
            //AudioManager.Instance.StopEffect();  
            //_animator?.SetFloat("Direction",0f);
        }
        

        
        
    }
    
    
}
