using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DigMineToggler : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    
    void Awake()
    {
        _toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    private void OnToggleChanged(bool active)
    {
        var msg = active ? "toggle 1" : "toggle 2";
        Debug.Log(msg);
    }
}
