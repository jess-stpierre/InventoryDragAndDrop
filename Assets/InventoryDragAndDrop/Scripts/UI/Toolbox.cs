
///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Anything a child of this gameObject will not be destroyed between levels/ scenes
/// </summary>
public class Toolbox : MonoBehaviour
{
    public static Toolbox Instance { get; private set; }

    public GameObject canvas;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        if (canvas != null)
        {
            var go = canvas;
            go.transform.parent = this.gameObject.transform;
        }

        if (canvas == null)
        {
            canvas = GameObject.FindGameObjectWithTag("Canvas");
        }
    }
}
