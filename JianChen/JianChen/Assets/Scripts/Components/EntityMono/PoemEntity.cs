using Assets.Scripts.Framework.JianChen.Service;
using FrameWork.JianChen.Core;
using UnityEngine;

public class PoemEntity : EntityView
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        //Debug.LogError("enter"+other.gameObject.layer+other.gameObject.name);
        //算法，如果是触发到了传送门立刻出现Loading图，并且进入下一个场景
        switch (other.gameObject.layer)
        {
            case 9:
                Debug.Log("enterScene");
                //joystick.enabled = false;
                
//                EventSystem.current.enabled = true;

                //触发跳转场景的事件
                break;
            case 11:
                //Debug.LogError("mainPlayer get ,need to recover!");
                PoolManager.Instance.RecoverEntity("3DModel/PropModel/Prefabs/Poem",this);
                //碰撞到其他玩家
                break;
            case 12:
                Debug.Log("NPC");
                //碰撞到NPC，将来可以用来触发气泡对话或者欢迎动作之类的。
                break;
            case 14:
                Debug.Log("Area");
                break;
            default:
                Debug.Log("Tochething:"+other.gameObject.name);
                break;
            
        }
        
        
        
    }
}
