using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCircle : MonoBehaviour
{
    public GameObject ship;
    [Space]
    public float moveDistance;
    public float sprintDistance;

    float distanceToMove;
    float cursorMoveSpeed;

    private void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        if (Input.GetButton("Sprint"))
        {
            distanceToMove = sprintDistance;
            cursorMoveSpeed = 0.01f;
        }
        else
        {
            distanceToMove = moveDistance;
            cursorMoveSpeed = 0.1f;
        }

        if (xInput == 0 && yInput == 0)
        {
            if(transform.localPosition != ship.transform.position)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, ship.transform.position, 0.1f);
            }
        }    
        else
        {
            //Debug.Log(Mathf.Abs(ship.transform.position.x - transform.localPosition.x));
            //Debug.Log(Mathf.Abs(ship.transform.position.z - transform.localPosition.z));
            Debug.Log((transform.localPosition - ship.transform.position).magnitude);

            //if (Mathf.Abs(ship.transform.position.x - transform.localPosition.x) <= distanceToMove && Mathf.Abs(ship.transform.position.z - transform.localPosition.z) <= distanceToMove)
            if((transform.localPosition - ship.transform.position).magnitude <= distanceToMove)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, transform.localPosition + new Vector3(xInput, 0,  yInput).normalized * distanceToMove, cursorMoveSpeed);
            }
        }

    }
}
