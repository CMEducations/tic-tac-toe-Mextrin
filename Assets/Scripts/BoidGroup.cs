using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidGroup : MonoBehaviour
{
    GameObject[] waypoints;
    int currentWaypointIndex;

    public Transform CurrentWaypoint => waypoints[currentWaypointIndex].transform;
    [SerializeField, Range(2, 50)] int maxGroupSize = 10;
    [SerializeField] GameObject NPCPrefab;

    [HideInInspector] public List<NPC> npcGroup = new List<NPC>();
    
    public Vector3 GroupCenter
    {
        get
        {
            Vector3 center = Vector3.zero;
            for (int i = 0; i < npcGroup.Count; i++)
            {
                center += npcGroup[i].transform.position;
            }
            
            return center / npcGroup.Count;
        }
    }

    public float GroupZoneRange => Mathf.Sqrt(npcGroup.Count * 1.0f);

    private void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        currentWaypointIndex = Random.Range(0, waypoints.Length);

        int spawnSize = Random.Range(2, maxGroupSize);
        for (int i = 0; i < spawnSize; i++)
        {
            GameObject newNPC = Instantiate(NPCPrefab, transform.position, Quaternion.identity, transform);
            npcGroup.Add(newNPC.GetComponent<NPC>());
        }
    }

    private void Update()
    {
        if (Vector3.Distance(CurrentWaypoint.position, GroupCenter) < GroupZoneRange)
        {
            TargetReached();
        }

        Debug.DrawLine(GroupCenter, CurrentWaypoint.position, Color.green, Time.deltaTime);
    }

    public void TargetReached()
    {
        currentWaypointIndex = Random.Range(0, waypoints.Length);
    }
}
