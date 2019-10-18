using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{



    void Update()
    {
        if (Input.GetMouseButton(0)) //walk
        {
            ClickToWalk(false);
        }
        if (Input.GetMouseButton(1)) //run
        {
            ClickToWalk(true);
        }
        if (Input.GetKeyDown(KeyCode.Space)) //stop
        {
            agent.ResetPath();
        }
    }

    private void ClickToWalk(bool run)
    {
        RaycastHit groundHit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out groundHit, Mathf.Infinity))
        {
            agent.SetDestination(groundHit.point);
            agent.speed = run ? speedValues.runSpeed : speedValues.walkSpeed;
        }
    }
}