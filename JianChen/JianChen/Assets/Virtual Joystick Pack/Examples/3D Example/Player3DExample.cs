using Common;
using UnityEngine;

public class Player3DExample : MonoBehaviour {

    public float moveSpeed = 8f;
    public Joystick joystick;
	private Animator _animator;
	private AnimationEventReceiver _animationEventReceiver;
	public string EffectName = "1";
	public Transform entitytran;
	public int[] damageArray = new[] {100};


	private void Awake()
	{
		_animator =transform.Find("Model").GetComponent<Animator>();
		//后续加载的时候要查找joyStick组件！

		_animationEventReceiver = transform.Find("Model").GetComponent<AnimationEventReceiver>();
		_animationEventReceiver.AniEvent = NormalAniEvent;
	}

	void Update () 
	{
        Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);

        if (moveVector != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveVector);
	        _animator?.SetFloat("Speed",moveSpeed);
	        //_animator?.SetFloat("Direction",joystick.Horizontal);
            transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.World);
        }
        else
        {
	        _animator?.SetFloat("Speed",0f);
	        //_animator?.SetFloat("Direction",0f);
        }

		if (Input.GetKeyDown(KeyCode.K))
		{
			_animator.SetTrigger("Slider");
			
		}
		
	}

	private void NormalAniEvent()
	{
		Debug.Log("NormalAniEventPlayer3DExample");
		var effect=EntityManager.Instance.SpawnSkillEffect(EffectName);
		effect.transform.SetParent(transform,false);
		effect.transform.SetParent(entitytran);
		SkillPlayer skill = effect.GetComponent<SkillPlayer>();
		skill.PlayerEffect(transform.name,damageArray);

	}
	
	
}