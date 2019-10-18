using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    #region Internal classes
    public class SpeedValues
    {
        public float walkSpeed = 3.0f;
        public float runSpeed = 8.0f;
    }
    #endregion

    #region Variables
    public NavMeshAgent agent;
    public SpeedValues speedValues = new SpeedValues();

    private Transform cachedTransform;
    #endregion

    #region Properties
    public Transform CachedTransform { get { return cachedTransform; } }
    #endregion

    void Awake()
    {
        cachedTransform = transform;        
    }
}
