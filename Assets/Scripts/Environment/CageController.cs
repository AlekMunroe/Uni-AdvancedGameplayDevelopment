using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageController : MonoBehaviour
{
    public enum CageType
    {
        Default,
        Trigger,
    }

    public enum TriggerAction
    {
        Open,
        Close,
        Activate,
        Deactivate,
    }
    
    private GameObject _player;
    public GameObject triggerObject;
    [SerializeField] private GameObject _cageModel;
    public CageType cageType;
    
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (cageType == CageType.Trigger)
        {
            
        }
    }
    void Update()
    {
        var playerTimeline = _player.GetComponent<TimelineIdentifier>().currentTimeline;
        switch (cageType)
        {
            case CageType.Default:
                _cageModel.SetActive(playerTimeline != 1);
                break;
            // Default cage activation logic. Current method is EXTREMELY JANK, needs to be reworked at some point.
            
            case CageType.Trigger:
                //TODO: Implement triggered cage for use in level 9
                break;
        }
    }
    
    public void TriggerCage(TriggerAction action)
    {
        switch (action)
        {
            case TriggerAction.Open:
                break;
            case TriggerAction.Close:
                break;
            case TriggerAction.Activate:
                break;
            case TriggerAction.Deactivate:
                break;
            //TODO: needs logic
        }
    }
}
