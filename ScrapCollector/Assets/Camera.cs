using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public static Camera Instance { get; private set; }

    [Header("Configuration")]
    public Vector3 distancePosition;
    public Vector3 lerp;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Opcional: si quieres que persista entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
