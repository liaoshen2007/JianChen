using Common;
using DataModel;
using FrameWork.JianChen.Core.Event;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PropIconMono : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerEnterHandler
{
    private Transform originalSlot;
    private GameObject parent;
    private GameObject item;
    private float x_item;
    private float y_item;
    private Vector2 itemSize;
    private bool isDragging = false;  //默认设为false,否则OnPointerEnter每帧都会调用，会有bug
    private RectTransform itemRect;
    public UserGrid GridData;
    
    
    /// <summary>
    /// 添加CanvasGroup组件，在物品拖动时blocksRaycasts设置为false;
    /// 让鼠标的Pointer射线穿过Item物体检测到UI下层的物体信息
    /// </summary>
    private CanvasGroup itemCanvasGroup;
    
    
    private void Awake()
    {
        itemCanvasGroup = this.GetComponent<CanvasGroup>();
        item = this.transform.gameObject;

        x_item = item.GetComponent<RawImage>().GetPixelAdjustedRect().width;    //Image的初始长宽
        y_item = item.GetComponent<RawImage>().GetPixelAdjustedRect().height;

        //parent = transform.parent.gameObject;
        itemRect = transform.GetRectTransform();
    }

    public void SetData(UserGrid grid,Transform targetParent)
    {
        GridData = grid;
//        Debug.LogError("_thisGrid:"+grid.GridId);
        parent = targetParent.gameObject;
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
        if(eventData.pointerCurrentRaycast.depth==0 && isDragging==true)
        {
            SetOriginalPos(this.gameObject);
            return;
        }

        var raycastGrid = eventData.pointerCurrentRaycast.gameObject.GetComponent<PropIconMono>();
        if (raycastGrid != null && isDragging)
        {
            //可以实现GridData来实现格子的交换之类的数据！
            //Debug.Log("GridData："+raycastGrid.GridData.GridId);
            if (raycastGrid.GridData.GridPropId != 0)
            {
                SetOriginalPos(this.gameObject);
                Debug.Log("Can SwapItem"+raycastGrid.GridData.GridId);
                EventDispatcher.TriggerEvent<int,int>(EventConst.SwapBagItem,GridData.GridId,raycastGrid.GridData.GridId);
            }
            else
            {
                Debug.Log("SetCurrentSlot(eventData)");
            }
            
            
        }
        else
        {
            var raycastItem=eventData.pointerCurrentRaycast.gameObject.GetComponent<PropItem>();
            if (raycastItem != null && isDragging)
            {
                if (raycastItem.PropGrid.GridPropId == 0)
                {
                    SetOriginalPos(this.gameObject);
                    Debug.Log("SetCurrentSlot(eventData)"+raycastItem.PropGrid.GridId);
                    EventDispatcher.TriggerEvent<int,int>(EventConst.SwapBagItem,GridData.GridId,raycastItem.PropGrid.GridId);
                }
                else
                {
                    Debug.LogError("Error logic!");
                }
                
                
            }
            else if(raycastItem==null&&eventData.pointerCurrentRaycast.gameObject.name!="Prop")
            {
                Debug.LogError("Origin Slot!");
                SetOriginalPos(this.gameObject);
            }
            
//            else
//            {
//                SetOriginalPos(this.gameObject);
//            }
            

        }
        


        //Debug.Log(eventData.pointerCurrentRaycast.depth);
//        objectTag = eventData.pointerCurrentRaycast.gameObject.tag;
//        Debug.Log("Raycast = "+objectTag);
//
//       
//        if(objectTag!=null && isDragging==true)
//        {
//            
//            if (objectTag == Tags.InventorySlot)   //如果是空格子，则放置Item
//            {
//                SetCurrentSlot(eventData);
//            }
//            else if (objectTag == Tags.InventoryItem)      //交换物品
//            {
//                SwapItem(eventData);
//            }
//            else   //如果都不是则返回原位
//            {
//                SetOriginalPos(this.gameObject);
//            }
//        }
    }
    
    //把Item回归到原来位置
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
    
    //交换两个物体
    //由于拖放中，正被拖放的物体没有Block RayCast
    //具体思路：
    //1.记录当前射线照射到的物体（Item2）
    //2.获取Item2的parent的位置信息，并把item1放过去
    //3.把Item2放到Item1所在的位置
    
    //这版交换采用Texture赋值，修改texture就可以了！然后还要传递数据！
    public void SwapItem(PointerEventData eventData)
    {
        GameObject targetItem = eventData.pointerCurrentRaycast.gameObject;

        //下面这两个方法不可颠倒，否则执行顺序不一样会出bug
        //BUG：先把Item2放到了Item1的位置，此时Item1得到的位置信息是传递后的Item2的（原本Item1的位置）
        //因此会把Item1也放到Item2下，变成都在原本Item1的Slot内
        SetCurrentSlot(eventData);      
        SetOriginalPos(targetItem);
    }
    
    //设置Item到当前鼠标所在的Slot
    public void SetCurrentSlot(PointerEventData eventData)
    {
        //如果Slot为空
//        if (eventData.pointerCurrentRaycast.gameObject.tag==Tags.InventorySlot)
//        {
//            Transform currentSlot= eventData.pointerCurrentRaycast.gameObject.transform;
//            this.transform.SetParent(currentSlot);
//            //如果只是transform position，图片会默认在左上角顶点处的Anchor
//            //因此这里用anchoredPosition让Item图片填充满Slot
//            this.GetComponent<RectTransform>().anchoredPosition = currentSlot.GetComponent<RectTransform>().anchoredPosition;
//        }
//        else if(eventData.pointerCurrentRaycast.gameObject.tag == Tags.InventoryItem)
//        {
//            Transform currentSlot = eventData.pointerCurrentRaycast.gameObject.transform.parent;
//            this.transform.SetParent(currentSlot);
//            this.GetComponent<RectTransform>().anchoredPosition = currentSlot.GetComponent<RectTransform>().anchoredPosition;
//        }
    }

    
}
