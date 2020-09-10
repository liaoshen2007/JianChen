﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class UIEventListener : UnityEngine.EventSystems.EventTrigger
{
    public delegate void VoidDelegate(GameObject go);
    public delegate void VoidDelegate2(PointerEventData eventData);
    public VoidDelegate onClick;
    public VoidDelegate2 onDown;
    public VoidDelegate2 onUp;
    public VoidDelegate onEnter;
    public VoidDelegate onExit;
    public VoidDelegate onSelect;
    public VoidDelegate onUpdateSelect;
    public VoidDelegate onMove;

    public object Parameter;  
    
    static public UIEventListener Get(GameObject go)
    {
        UIEventListener listener = go.GetComponent<UIEventListener>();
        if (listener == null) listener = go.AddComponent<UIEventListener>();
        return listener;
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null) onClick(gameObject);
        base.OnPointerClick(eventData);
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null) onDown( eventData);
        
        base.OnPointerDown(eventData);
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null) onEnter(gameObject);
        base.OnPointerEnter(eventData);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (onExit != null) onExit(gameObject);
        base.OnPointerExit(eventData);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null) onUp( eventData);
        base.OnPointerUp(eventData);
    }
    public override void OnSelect(BaseEventData eventData)
    {
        if (onSelect != null) onSelect(gameObject);
        base.OnSelect(eventData);
    }
    public override void OnUpdateSelected(BaseEventData eventData)
    {
        if (onUpdateSelect != null) onUpdateSelect(gameObject);
    }

    public override void OnMove(AxisEventData eventData)
    {
        if (onMove != null) onMove(gameObject);
        base.OnMove(eventData);
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
    }
}