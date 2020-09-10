using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffectVo : MonoBehaviour
{
    public int EffectValue=0;
    private bool HasEffect = true;
    private string _creator = "";
    
    //这个不仅要设置伤害值，还要设置伤害的目标！
    public void SetEffectValue(string creator,int effectvalue)
    {
        _creator = creator;
        HasEffect = false;
//        Debug.LogError("effectvalue"+effectvalue);
        EffectValue = effectvalue;
    }
    
    //这个就是碰撞后给EnemySet值！！

//    private void OnCollisionEnter(Collision other)
//    {
//        Debug.LogError(other.collider.name);
//    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.LogError(other.name);
        //伤害类的技能特效是不可以攻击自身的！

        bool hitSuccess = EffectValue>0&&other.gameObject.name!=_creator;
       // Debug.Log("hitSuccess:"+hitSuccess);


        switch (other.gameObject.layer)
        {
            case 9:
                Debug.Log("enterScene");
                //joystick.enabled = false;
                
                //todo 以下代码需要优化啊！！
//                EventSystem.current.enabled = true;

                //触发跳转场景的事件
                break;
            case 11:
                Debug.Log("mainPlayer");
                if (hitSuccess)
                {
                    Debug.Log("CanSendEvent and call enemy damage!");
                    var mainroleEntity = other.gameObject.GetComponent<MainRoleSingleEntity>();
                    mainroleEntity.HasBeenAttack(EffectValue);

                }
                //碰撞到其他玩家
                break;
            case 12:
                Debug.Log("NPC");
                //碰撞到NPC，将来可以用来触发气泡对话或者欢迎动作之类的。
                
                
                break;
            case 13:
                Debug.Log("Enemy");
                if (hitSuccess)
                {
                    Debug.Log("CanSendEvent and call enemy damage!");
                    var enemyEntity = other.gameObject.GetComponent<EnemyRoleSingleEntity>();
                    enemyEntity.HasBeenAttack(EffectValue);

                }


                break;
            default:
                Debug.Log("Tochething:"+other.gameObject.name);
                break;
            
        }
        
        
    }

//    private void OnCollisionStay(Collision other)
//    {
//        Debug.LogError(other.collider.name);
//    }
}
