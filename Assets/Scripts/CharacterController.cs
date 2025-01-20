using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    [SerializeField] float maxMoveVelocity;
    [SerializeField] float minMoveForce;
    [SerializeField] float maxMoveForce;
    [SerializeField] float moveForceChangeRate;
    [SerializeField] float maxBoostVelocity;
    [SerializeField] float boostMaxForce;
    [SerializeField] float boostChangeRate;
    [SerializeField] float brakeForce;
    Vector3 currentForceApplied;
    float currentVelocity;
    float currentMoveForce;
    float currentBoostForce;
    bool boostBtnReleased;
    float boostReleasedTimer;
    bool isBoosting;
    bool isBraking;
    [Space]
    [SerializeField] float rotateSpeed;
    [Space]
    [SerializeField] float tiltSpeed;
    [SerializeField] float maxTilt;
    float shipCurrentYRotation;
    [Space]
    [SerializeField] Transform shipRb;
    [SerializeField] Transform shipModel;
    [SerializeField] bool type1;
    [Header("UI")]
    [SerializeField] Image boostFillImg;
    [SerializeField] TMP_Text currentVelocityTxt;
    [SerializeField] Slider boostSlider;

    Rigidbody rb;

    //Coroutine isBoosting;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        boostBtnReleased = true;
        shipCurrentYRotation = shipRb.rotation.y;

        boostSlider.minValue = minMoveForce;
        boostSlider.maxValue = maxMoveForce;
        currentMoveForce = boostSlider.value;

    }

    private void FixedUpdate()
    {
        currentVelocity = rb.velocity.magnitude;
        currentVelocityTxt.text = currentVelocity.ToString("0");

        MoveCharacter();
        RotateCharacter();
    }

    private void Update()
    {
        Boost();
        Brake();
        //Boosting();

    }

    void Brake()
    {
        if (Input.GetButton("Brake"))
        {
            //if (boostSlider.value > 0)
            //    boostSlider.value -= moveForceChangeRate;
            //return;

            if (rb.velocity.magnitude > 0.2f)
            {
                isBraking = true;
                Vector3 decelerationForce = -rb.velocity.normalized * rb.mass * brakeForce;

                // Apply force opposite to the current velocity
                rb.AddForce(decelerationForce, ForceMode.Force);
            }
            // Optional: Stop the velocity completely when it's close to zero
            else
            {
                isBraking = false;
                //rb.velocity = Vector3.zero; // Snap velocity to zero
            }

            ////Adding force will decelerate
            //if (Vector3.Dot(rb.velocity, shipRb.forward * -brakeForce) < 0)
            //{
            //    isBraking = true;
            //    currentForceApplied = shipRb.forward * -minMoveForce;
            //    rb.AddForce(shipRb.forward * -minMoveForce);
            //}
            //else
            //{
            //    currentForceApplied = Vector3.zero;
            //    isBraking = false;
            //}
        }
        else
        {
            isBraking = false;
        }
    }

    void Boost()
    {
        if(Input.GetButton("Sprint"))
        {
            isBoosting = true;
            boostSlider.value += moveForceChangeRate;
        }
        else
        {
            isBoosting = false;
            if (boostSlider.value > 0)
                boostSlider.value -= moveForceChangeRate;
        }
        return;

        if (Input.GetButton("Sprint"))
        {
            //isBoosting = StartCoroutine(Boosting());
            isBoosting = true;
            currentBoostForce = boostMaxForce;
        }
        else
        {
            isBoosting = false;
            currentBoostForce = 0;
        }

        boostFillImg.fillAmount = (float)rb.velocity.magnitude / (float)maxBoostVelocity;


    }

    public void BoostSlider(Slider _boostSlider)
    {
        currentMoveForce = _boostSlider.value;
    }

    //void Boosting()
    //{
    //    if (!isBoosting)
    //        return;

    //    if (rb.velocity.magnitude < maxBoostVelocity)
    //        currentBoostForce += boostChangeRate * Time.deltaTime;
    //}



    void MoveCharacter()
    {
        float xVal = Input.GetAxisRaw("Horizontal");
        float yVal = Input.GetAxisRaw("Vertical");

        if (xVal != 0 || yVal != 0 && !isBraking)
        {
            //if (!isBoosting)
            //{
            //    if (rb.velocity.magnitude > maxMoveVelocity)
            //    {
            //        //Debug.Log("Max Move Speed");
            //        return;
            //    }
            //}
            //else
            //{
            //    if (rb.velocity.magnitude > maxBoostVelocity)
            //    {
            //        //Debug.Log("Max Boost Speed");
            //        return;
            //    }
            //}

            Vector3 moveDir;
            //moveDir = new Vector3(xVal, 0, yVal).normalized;
            moveDir = shipRb.forward;
            currentForceApplied = moveDir * currentMoveForce;
            rb.AddForce(moveDir * currentMoveForce);
            //rb.AddForce(moveDir * (minMoveForce + currentBoostForce));
        }
        else
        {
            currentForceApplied = Vector3.zero;
        }

    }

    void Tilt()
    {
        var direction = shipRb.rotation.y - shipCurrentYRotation;
        Debug.Log("Direction " + direction);
        if (direction > 0)
            shipRb.DORotate(new Vector3(shipRb.rotation.x, shipRb.rotation.y, -maxTilt), 1);
        //ship.rotation = new Quaternion(ship.rotation.x, ship.rotation.y, Mathf.Lerp(ship.rotation.z, -maxTilt, Time.deltaTime * tiltSpeed), ship.rotation.w);
        else if(direction < 0)
            shipRb.DORotate(new Vector3(shipRb.rotation.x, shipRb.rotation.y, maxTilt), 1);
        //ship.rotation = new Quaternion(ship.rotation.x, ship.rotation.y, Mathf.Lerp(ship.rotation.z, maxTilt, Time.deltaTime * tiltSpeed), ship.rotation.w);
        shipCurrentYRotation = shipRb.rotation.y;
    }

    void RotateCharacter()
    {
        //Tilt();
        
        float xVal = Input.GetAxisRaw("Horizontal");
        float zVal = Input.GetAxisRaw("Vertical");


        //if (xVal != 0 || zVal != 0)
        //{
        //    ship.transform.rotation = Quaternion.LookRotation(rb.velocity);
        //}
        //return;

        //Vector2 inputDirection = new Vector2(rb.velocity.x, rb.velocity.y);

        //if (inputDirection.sqrMagnitude > 0.01f)
        //{
        //    // Calculate the angle in degrees based on the input direction
        //    float angle = Mathf.Atan2(inputDirection.x, inputDirection.y) * Mathf.Rad2Deg;
        //    ship.DORotateQuaternion(Quaternion.Euler(0, angle, 0), 0.5f);
        //}
        //return;

        Vector2 inputDirection = new Vector2(xVal, zVal);

        if (inputDirection.sqrMagnitude > 0.01f)
        {
            // Calculate the angle in degrees based on the input direction
            float angle = Mathf.Atan2(inputDirection.x, inputDirection.y) * Mathf.Rad2Deg;
            shipRb.DORotateQuaternion(Quaternion.Euler(0, angle, 0), rotateSpeed);
            if(type1)
                shipModel.DORotateQuaternion(Quaternion.Euler(0, angle, 0), rotateSpeed);
        }
        if (type1)
            return;

        // Get the velocity vector
        Vector3 velocity = rb.velocity;
        Vector3 force = currentForceApplied;

        Vector3 averageDirection = (velocity.normalized + force.normalized).normalized;
        averageDirection = velocity;

        // Ignore Y-axis to rotate only in the horizontal plane
        Vector3 horizontalVelocity = new Vector3(averageDirection.x, 0, averageDirection.z);

        // Check if the velocity vector has a magnitude (to prevent jittering when velocity is zero)
        if (horizontalVelocity.magnitude > 0.1f)
        {
            // Calculate the rotation towards the velocity direction
            Quaternion targetRotation = Quaternion.LookRotation(horizontalVelocity);

            // Apply rotation to the Y-axis only
            shipModel.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
        }

    }

}
