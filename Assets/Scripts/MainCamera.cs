using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Vector3 offsetFromPlayer = new Vector3(0.0f, 5.0f, -20.0f);

    private Transform cachedTransform;
    private Player player;

    private Player Player
    {
        get
        {
            if (player == null) { player = FindObjectOfType<Player>(); }
            return player;
        }
    }

    void Awake()
    {
        cachedTransform = transform;
    }

    
    void Update()
    {
        cachedTransform.position = Player.CachedTransform.position + offsetFromPlayer;
    }
}
