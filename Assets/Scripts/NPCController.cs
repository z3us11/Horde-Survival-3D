using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] float maxMoveSpeed;
    [SerializeField] float moveForce;

    Rigidbody rb;
    CharacterController characterController;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(characterController == null)
        {
            characterController = FindAnyObjectByType<CharacterController>();   
        }

        var direction = characterController.transform.position - transform.position;
        if(rb.velocity.magnitude < maxMoveSpeed)
            rb.AddForce(direction * moveForce * Time.deltaTime);
        // Remove vertical (Y-axis) difference if you only want horizontal rotation
        direction.y = 0f;
        // Check if the direction vector is valid
        if (direction.sqrMagnitude > 0.001f)
        {
            // Calculate the rotation needed to face the target
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Apply the rotation to this object
            transform.rotation = targetRotation;
        }
    }
}
