using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoemItem : MonoBehaviour
{

    private Text _poemItem;
    private Button _poemBtn;
    private int _curIdx;
    
    void Awake()
    {
        _poemItem = transform.GetText();
        _poemBtn = transform.GetButton();
        _poemBtn.onClick.AddListener(OnPoemClick);

    }

    private void OnPoemClick()
    {
        Debug.Log("Click this poem:"+_curIdx);
    }

    public void SetData(int idx,string poemItem)
    {
        _poemItem.text = poemItem;
        _curIdx = idx;
    }
    
    
}
