﻿//
//  Square
//
//  Created by Krzysztof Maliński, Adam Kolinski, 
// Damian Klabuhn, Mikolaj Mikolajczak
//  
// Code inspired by Croovsko, project by Krzysztof Malinski
// 


using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent _event;
    public UnityEvent _response;

    private void OnEnable()
    {
        _event.RegisterListener(this);
    }

    private void OnDisable()
    {
        _event.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        _response.Invoke();
    }
}