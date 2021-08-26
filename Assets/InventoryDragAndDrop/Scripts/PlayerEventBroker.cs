﻿
///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Do not attach this script to a gameObject, leave it in your project folders, it'll do it's thing
/// It is a Broker that connects any triggered functions (that are coded below), to script that'll recieve those functions
/// </summary>
public class PlayerEventBroker
{
    public delegate void GameObjectEvent(GameObject obj);
    public static event GameObjectEvent OnAttemptPickup;

    public static void TriggerOnAttemptPickup(GameObject obj)
    {
        OnAttemptPickup?.Invoke(obj);
    }
}
