using System.Collections;
using System.Collections.Generic;
using Common;
using DataModel;
using FrameWork.JianChen.Core;
using Module;
using UnityEngine;
using UnityEngine.UI;

namespace game.main
{
    public class SettingWindow : Window
    {
        private Button _closeBtn;
        private Slider _bgmSlider;
        private Slider _voiceSlider;
        private Button _quitGameBtn;
        private Button _saveGameBtn;
        private Button _returnTown;
        private Button _sellBtn;

        private void Awake()
        {
            _closeBtn = transform.GetButton("SettingPanel/CloseBtn");
            _bgmSlider = transform.Find("SettingPanel/InfoPanel/BGMSlider").GetComponent<Slider>();
            _voiceSlider = transform.Find("SettingPanel/InfoPanel/VoiceSlider").GetComponent<Slider>();
            _quitGameBtn = transform.GetButton("SettingPanel/QuitGame");
            _saveGameBtn = transform.GetButton("SettingPanel/SaveGame");
            _returnTown = transform.GetButton("SettingPanel/ReturnToTown");
            _sellBtn = transform.GetButton("SettingPanel/Shop");
            _closeBtn.onClick.AddListener(Close);

            _bgmSlider.value = AudioManager.Instance.BgMusicVolum;
            _voiceSlider.value = AudioManager.Instance.EffectVolume;
            
            _bgmSlider.onValueChanged.AddListener(OnBgmSliderEvent);
            _voiceSlider.onValueChanged.AddListener(OnVoiceSliderEvent);
            
            _quitGameBtn.onClick.AddListener(OnQuitGameClick);
            _saveGameBtn.onClick.AddListener(OnSaveGameClick);
            _returnTown.onClick.AddListener(OnReturnTownClick);
            _sellBtn.onClick.AddListener(OnShopClick);

        }

        private void OnShopClick()
        {
            ModuleManager.Instance.EnterModule(ModuleConfig.MODULE_SHOP, false);
        }

        private void OnReturnTownClick()
        {
            
        }

        private void OnSaveGameClick()
        {
            PopupManager.ShowAlertWindow("是否要保存游戏？").WindowActionCallback = evt =>
            {
                if (evt == WindowEvent.Ok)
                {
                    GlobalData.PlayerData.SavePlayerData();
                }
                
            };
            

        }

        private void OnQuitGameClick()
        {
            PopupManager.ShowConfirmWindow("是否要退出游戏？").WindowActionCallback = evt =>
            {
                if (evt == WindowEvent.Ok)
                {
                    Application.Quit();
                }
                
            };
            

        }

        private void OnVoiceSliderEvent(float value)
        {
            AudioManager.Instance.SetAudioSize(AudioManager.AudioTypes.Effect, value);
            PlayerPrefs.SetFloat("EffectVolume", value);
        }

        private void OnBgmSliderEvent(float value)
        {
            AudioManager.Instance.SetAudioSize(AudioManager.AudioTypes.Bgm, value);
            PlayerPrefs.SetFloat("BgMusicVolum", value);
        }

        protected override void OpenAnimation()
        {
        }


        protected override void AddBgMask()
        {
        }
        
        
        
    }
    

}

