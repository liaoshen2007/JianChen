using Assets.Scripts.Framework.JianChen.Service;
using DataModel;
using Module;
using UnityEngine;
using UnityEngine.UI;

namespace game.main
{
    public class AwardItem : MonoBehaviour
    {

        private RawImage _propimage;
        private Text _propname;
        private Text _propNum;

        private void Awake()
        {
            _propimage = transform.Find("IconBg/Prop").GetRawImage();
            _propname = transform.Find("Text").GetText();
            _propNum = transform.Find("IconBg/Num").GetText();
        }

        public void SetData(AwardData awardPb)
        {
            RewardVo vo=new RewardVo(awardPb,true);
            _propimage.texture=ResourceManager.Load<Texture>(vo.IconPath,ModuleConfig.MODULE_SHOP,true);
            _propname.text = vo.Name;
            _propNum.text = vo.Num.ToString();

        }
    
    }  

}


