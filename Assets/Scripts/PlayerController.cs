using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public Animator animator;

    private void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(xInput, 0, yInput) * movementSpeed * Time.deltaTime;

        if(animator != null)
        {
            if (xInput != 0 || yInput != 0)
            {
                animator.SetFloat("speed", 1);
            }
            else
            {
                animator.SetFloat("speed", -1);
            }
        }
    }
}
