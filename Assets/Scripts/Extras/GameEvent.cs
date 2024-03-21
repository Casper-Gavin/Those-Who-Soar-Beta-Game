using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public static Action<EventType> OnEventFired;

    public enum EventType { // only BossFight is used - can put other events in (make sure to change in UIManager too)
        BossFightIntro,
        BossFightStart,
        LevelClearOne,
        LevelClearTwo,
        LevelClearThree,
        LevelClearFour
    }

    [SerializeField] private EventType eventType;
    [SerializeField] private LayerMask eventLayer;

    private bool eventFired;


    private void OnTriggerEnter2D(Collider2D other) {
        if (MyLibrary.CheckLayer(other.gameObject.layer, eventLayer)) {
            if (!eventFired) {
                // if it's not null the event gets invoked
                OnEventFired?.Invoke(eventType);
                eventFired = true;
            }
        }
    }
}
