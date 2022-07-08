using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
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
        if (active)
        {
            GameModeMachine.CurrentMode = ModeType.Setup;
        }
        else
        {
            GameModeMachine.CurrentMode = ModeType.Play;
        }
        
        Events.onGameModeChanged.Invoke();
    }
}
