using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character
{
    BoidGroup group;
    [SerializeField] float personalSpace = 2.5f;
    [SerializeField] float reactionDistance = 5.0f;

    [SerializeField] float minUpdateTime = 0.5f;
    [SerializeField] float maxUpdateTime = 2.0f;
    float nextUpdate;

    private void Start()
    {
        group = GetComponentInParent<BoidGroup>();
    }

    private void Update()
    {
        if (group)
        {
            if (nextUpdate < Time.time)
            {
                nextUpdate += Random.Range(minUpdateTime, maxUpdateTime);

                {
                    //Personal Space
                    Vector3 avgPersonalPosition = Vector3.zero;
                    int npcsInPersonalSpace = 0;
                    for (int i = 0; i < group.npcGroup.Count; i++)
                    {
                        if (Vector3.Distance(group.npcGroup[i].transform.position, transform.position) < personalSpace && group.npcGroup[i] != this)
                        {
                            avgPersonalPosition += group.npcGroup[i].transform.position;
                            npcsInPersonalSpace++;
                        }
                    }

                    if (npcsInPersonalSpace != 0)
                        avgPersonalPosition /= npcsInPersonalSpace;

                    //Return to 
                    Vector3 returnDirection = Vector3.zero;
                    float distanceToCenter = Vector3.Distance(transform.position, group.GroupCenter);
                    if (distanceToCenter > group.GroupZoneRange)
                        returnDirection = (group.GroupCenter - transform.position).normalized * Mathf.Max(0, distanceToCenter - group.GroupZoneRange);

                    //Final Calculations
                    Vector3 destination = Vector3.zero;
                    destination += (group.CurrentWaypoint.position - transform.position).normalized * 2;
                    if (npcsInPersonalSpace != 0) 
                        destination += (transform.position - avgPersonalPosition);
                    destination += returnDirection * 1.5f;

                    destination.Normalize();
                    destination *= 10;
                    destination += transform.position;
                    agent.SetDestination(destination);
                }
            }

            //Show Center
            if (Vector3.Distance(transform.position, group.GroupCenter) < group.GroupZoneRange)
            {
            //    Debug.DrawLine(transform.position, group.GroupCenter, Color.white, Time.deltaTime);

            }
            else
            {
                Vector3 target = (transform.position - group.GroupCenter).normalized * group.GroupZoneRange;
                target += group.GroupCenter;
                Debug.DrawLine(group.GroupCenter, target, Color.white, Time.deltaTime);
                Debug.DrawLine(target, transform.position, Color.red, Time.deltaTime);
            }

            //Debug.DrawLine(transform.position, agent.destination, Color.blue, Time.deltaTime);
        }
    }
}