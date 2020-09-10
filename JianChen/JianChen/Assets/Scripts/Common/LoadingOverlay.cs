using UnityEngine;
using UnityEngine.UI;

public class LoadingOverlay : MonoBehaviour {

        private static LoadingOverlay _instance;
        private bool _isShow = false;

        public static LoadingOverlay Instance { get { return _instance; } }

        private float _startTime;

        public float Timeout = 60;
        public float DelayShow = 1.2f;

        private void Awake()
        {
            _instance = this;
            Hide();
        }

        public void Show()
        {
            _isShow = true;
            _startTime = Time.realtimeSinceStartup;
            gameObject.Show();
        }
        
        public void Hide()
        {
            _isShow = false;
            gameObject.Hide();
        }

        public void ShowMask(bool showMask)
        {
            gameObject.SetActive(showMask);
        }
        
        

        private void Update()
        {
            if (_isShow)
            {
                if (Time.realtimeSinceStartup - _startTime > DelayShow)
                {
                    
                    float speed = 6;
                    if (Time.realtimeSinceStartup - _startTime > Timeout)
                    {
                        Hide();
                    }
                }
            }
        }
}
