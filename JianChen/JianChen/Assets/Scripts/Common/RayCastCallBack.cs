using System.Collections;
using System.Collections.Generic;
using Common;
using FrameWork.JianChen.Core.Event;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RayCastCallBack : MonoBehaviour
{

    private Camera mainCamera;
    private RaycastHit _raycastHit;
    private Ray _ray;

//    private void Awake()
//    {
//        mainCamera=GameObject.Find("ModelCamera").GetComponent<Camera>();
//    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (mainCamera==null)
            {
                mainCamera=GameObject.Find("ModelCamera")?.GetComponent<Camera>();
                //return;
            }

            if (mainCamera!=null)
            {
                _ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                Debug.DrawLine(_ray.origin,_raycastHit.point,Color.red,2);
                if (Physics.Raycast(_ray,out _raycastHit,100)&&!IsPointerOverUIObject())
                {
                    GameObject gameObj = _raycastHit.collider.gameObject;
                    Debug.Log("Hit ObjName:"+gameObj.name+"Hit layer:"+gameObj.layer);
                    switch (gameObj.layer)
                    {
                        case 12://"NPC":
                            //Debug.Log("NPC"+gameObj.GetComponent<NPCRoleSingleEntity>());
                            
                            //todo 之后要做到靠近npc点击才能生效！
                            var npcEntitydata = gameObj.GetComponent<NPCRoleSingleEntity>();
                            if (npcEntitydata!=null)
                            {
                                AudioManager.Instance.PlayEffect(AudioManager.DefaultBackButtonEffectName);
                                Debug.Log("Clickevent:"+npcEntitydata.npcData.Name);
                                EventDispatcher.TriggerEvent(EventConst.ClickNpc,npcEntitydata.npcData.ID);
                                EventDispatcher.TriggerEvent(EventConst.LookAtNPC,npcEntitydata.targetCamPos);
                            }
                            
                            break;
                        case 11://"MainPlayer":
                            Debug.Log("MainPlayer"+gameObj.name);
                            break;
                        case 8://"Env":
                            //Debug.LogError("env"+gameObj.transform.position);
                            break;
                        default:
                        Debug.LogError(gameObj.name+" "+gameObj.tag);
                        break;
                    }
                    
                    
                }

            }

        }
        
        
    }
    
    /// <summary>
    /// 点击UI是返回false!
    /// </summary>
    /// <returns></returns>
    private bool IsPointerOverUIObject(){
        PointerEventData eventDataCurrentPosition=new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position=new Vector2(Input.mousePosition.x,Input.mousePosition.y);
        List<RaycastResult> results=new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition,results);
//        Debug.Log(results.Count);
        bool hasClickUI = results.Count > 0;
        if (hasClickUI)
        {
            GameObject obj = EventSystem.current.currentSelectedGameObject;
            if (obj != null)
            {
                Button btn = obj.GetComponent<Button>();
                if (btn != null)
                {
                    ButtonSound bs = obj.GetComponent<ButtonSound>();
                    if (bs != null)
                    {
                        //如果SoundName没有设置就不播放按钮音频
                        if(bs.SoundName != null)
                            btn.PlayButtonEffect(bs.SoundName);
                    }
                    else
                    {
                        //播放默认音频
                        btn.PlayButtonEffect();
                    }
                }
            }
        }
        

        return hasClickUI;
    }
    
    
}
