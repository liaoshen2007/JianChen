using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Framework.JianChen.Service;
using DG.Tweening;
using FrameWork.JianChen.Core;
using UnityEngine;
using UnityEngine.UI;

public class DamageHud : EntityView
{

    private Text damageNum;

    private void Awake()
    {
        damageNum=transform.Find("Hub/Canvas/numTex").GetComponent<Text>();
    }

    public void SetTextTween(int damage,float duration = 0.5f)
    {
        damageNum.color = new Color(damageNum.color.r, damageNum.color.g, damageNum.color.b, 1);
        damageNum.text = damage.ToString();
        transform.localPosition=Vector3.zero;
        
        Tweener move1 = transform.DOLocalMoveY(transform.localPosition.y+1, 0.5f).SetEase(DG.Tweening.Ease.OutSine);
        Tweener move2 = transform.DOLocalMoveY(transform.localPosition.y+3, 0.5f).SetEase(DG.Tweening.Ease.OutSine);
        
        Tweener alpha2 = damageNum.DOColor(new Color(damageNum.color.r, damageNum.color.g, damageNum.color.b, 0), 0.3f);
        
        //动画完成的回调
        DOTween.Sequence()
            .Append(move1)
            .AppendInterval(duration)
            .Append(move2)
            .Join(alpha2).OnComplete(() =>
            {
                //回收！
                PoolManager.Instance.RecoverEntity("3DModel/SkillEffect/DamageHudEffect",this);
                
            });

        
        
    }
    
    
}
