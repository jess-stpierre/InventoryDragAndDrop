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
            var go3 = canvas;
            go3.transform.parent = this.gameObject.transform;
        }

        if (canvas == null)
        {
            canvas = GameObject.FindGameObjectWithTag("Canvas");
        }
    }
}
