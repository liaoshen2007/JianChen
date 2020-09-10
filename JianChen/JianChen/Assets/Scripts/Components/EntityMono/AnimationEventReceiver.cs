using System;
using UnityEngine;

public class AnimationEventReceiver : MonoBehaviour
{

    public Action AniEvent;
    
    
    private void NormalAniEvent()
    {
        Debug.Log("NormalAniEvent");
        AniEvent?.Invoke();
    }
    
    
    
}
