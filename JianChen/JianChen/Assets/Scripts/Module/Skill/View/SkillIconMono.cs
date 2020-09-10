using Common;
using FrameWork.JianChen.Core.Event;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillIconMono : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerEnterHandler
{
    private Transform originalSlot;
    private GameObject parent;
    private GameObject item;
    private float x_item;
    private float y_item;
    private Vector2 itemSize;
    private bool isDragging = false;  //默认设为false,否则OnPointerEnter每帧都会调用，会有bug
    private RectTransform itemRect;
    
    private CanvasGroup itemCanvasGroup;
    private int _skillId=1;

    private void Awake()
    {
        itemCanvasGroup = this.GetComponent<CanvasGroup>();
        item = this.transform.gameObject;

        x_item = item.GetComponent<RawImage>().GetPixelAdjustedRect().width;    //Image的初始长宽
        y_item = item.GetComponent<RawImage>().GetPixelAdjustedRect().height;

        //parent = transform.parent.gameObject;
        itemRect = transform.GetRectTransform();
    }

    public void SetData(Transform targetParent,int skillId)
    {

        parent = targetParent.gameObject;
        _skillId = skillId;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalSlot = this.GetComponent<Transform>().parent;       //每次拖拽开始前记录初始位置
        isDragging = true;
        itemCanvasGroup.blocksRaycasts = true;
        item.transform.SetParent(parent.transform, false);
       
        // 将item设置到当前UI层级的最下面（最表面，防止被同一层级的UI覆盖）
        item.transform.SetAsLastSibling();      
        
        itemRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x_item);
        itemRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, y_item);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        itemCanvasGroup.blocksRaycasts = false;
        DragPos(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnPointerEnter(eventData);
        itemCanvasGroup.blocksRaycasts = true;
        isDragging = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //当鼠标在最外层时（移出背包，Canvas外）
        //让物品回到原位
        if (eventData.pointerCurrentRaycast.depth == 0 && isDragging == true)
        {
            SetOriginalPos(this.gameObject);
            return;
        }
        
        //修改的想法：跟GameMainView的按钮碰撞的时候，获得碰撞到Mono是否为GameMain里的“A”“B””C”来添加修改UserSkillData!!！

        //应该要满足两个条件！！
        if (eventData.pointerCurrentRaycast.gameObject.name.Contains("SkillKey")&& isDragging)
        {
            Debug.LogError(eventData.pointerCurrentRaycast.gameObject.name);
            SetOriginalPos(this.gameObject);
            EventDispatcher.TriggerEvent<int,string>(EventConst.SetSkillKeyPos,_skillId,eventData.pointerCurrentRaycast.gameObject.name);
            
        }
        else
        {
            if (isDragging)
            {
                SetOriginalPos(this.gameObject);
            }
            

        }

        
        
    }
    
    public void SetOriginalPos(GameObject gameobject)
    {
        
        gameobject.transform.SetParent(originalSlot);
        itemRect.anchoredPosition = Vector2.zero;
        itemCanvasGroup.blocksRaycasts = true;
    }
    
    //获取鼠标当前位置，并赋给item
    private void DragPos(PointerEventData eventData)
    {
        //RectTransform RectItem = item.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(item.transform as RectTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            itemRect.position = globalMousePos;
        }
    }




}
