using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cotroler : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    [Header("Movement")]
    public float speed = 6;
    public float rotationSpeed = 1.2f;
    public float gravity = 3;
    public float jumpForce = 0.5f;

    [Header("Camera")]
    public Transform headBone;
    public Transform cameraRig;
    public float rotationSlerp = 3;

    [Header("Player Rig")]
    public GameObject playerRig;

    //--------------

    Transform thisTransform, playerTransform;
    Animator playerAnimator;
    CharacterController thisCharacter;
    float jumpValue, mouseY, mouseX, speedchange, timeDelay, overallSpeed, crouchCh, injuredCh, tiredCh, tiredValue, tiredTime, axisHorizontal, axisVertical, angleProtationL, angleProtationR;
    bool ground, rotationWork, crouchOn, injured, tired;
    int curentLayer;


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void Awake()
    {
        //--------------

        thisTransform = transform;
        playerTransform = playerRig.transform;
        playerAnimator = playerRig.GetComponent<Animator>();
        thisCharacter = GetComponent<CharacterController>();
        GlVAR.playerPosition = thisTransform.position;

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void Update()
    {
        //--------------

        Axis();
        MovementPlayer();
        Tired();
        RotationPlayer();
        CameraRig();
        AnimationWalk();
        AnimationTired();
        AnimationCrouch();
        AnimationInjured();

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void Axis()
    {
        //--------------

        if (axisHorizontal != Input.GetAxis("Horizontal")) axisHorizontal = Input.GetAxis("Horizontal");
        if (axisVertical != Input.GetAxis("Vertical")) axisVertical = Input.GetAxis("Vertical");

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void MovementPlayer()
    {
        //--------------

        ground = thisCharacter.isGrounded;

        if (ground == true && curentLayer == 31)
        {
            if (Input.GetButtonDown("Jump") && injured == false) jumpValue = jumpForce;
        }
        else if (jumpValue > -gravity) jumpValue -= gravity * Time.deltaTime;

        if (Input.GetButton("Fire3") && axisVertical > 0 && injured == false && tired == false) speedchange = 2;
        else if (crouchOn == true) speedchange = 0.75f;
        else speedchange = 1;

        Vector3 curentDirection = thisTransform.TransformDirection(new Vector3(axisHorizontal * 0.5f, jumpValue, axisVertical)) * speed * speedchange;
        thisCharacter.Move(curentDirection * Time.deltaTime);

        overallSpeed = thisCharacter.velocity.magnitude;

        playerTransform.position = thisTransform.position;
        GlVAR.playerPosition = thisTransform.position;

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //--------------

        curentLayer = hit.gameObject.layer;

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void Tired()
    {
        //--------------

        if (speedchange > 1)
        {
            if (tiredValue < 20) tiredValue += Time.deltaTime;
            else tired = true;
        }
        else
        {
            if (tiredValue > 0) tiredValue -= Time.deltaTime;
            else tired = false;
        }

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void RotationPlayer()
    {
        //--------------

        mouseX += rotationSpeed * Input.GetAxis("Mouse X");

        float angle = Vector3.Angle(cameraRig.right, transform.right);

        if (overallSpeed < 0.5f)
        {
            if (angle > 65) rotationWork = true;
            else if (angle < 1) rotationWork = false;
        }
        else rotationWork = true;

        if (rotationWork == true)
        {
            if (angle < 120) thisTransform.rotation = Quaternion.Slerp(thisTransform.rotation, Quaternion.Euler(0, mouseX, 0), rotationSlerp * Time.deltaTime);
            else thisTransform.rotation = Quaternion.Slerp(thisTransform.rotation, Quaternion.Euler(0, mouseX, 0), rotationSlerp * Time.deltaTime * 3);
        }

        //--------------

        if (axisVertical > 0 && axisHorizontal > 0 || axisVertical < 0 && axisHorizontal < 0)
        {
            if (angleProtationR < 20) angleProtationR += Time.deltaTime * 50;
        }
        else if (angleProtationR > 0) angleProtationR -= Time.deltaTime * 50;

        if (axisVertical < 0 && axisHorizontal > 0 || axisVertical > 0 && axisHorizontal < 0)
        {
            if (angleProtationL < 20) angleProtationL += Time.deltaTime * 50;
        }
        else if (angleProtationL > 0) angleProtationL -= Time.deltaTime * 50;

        playerTransform.rotation = thisTransform.rotation * Quaternion.Euler(0, angleProtationR - angleProtationL, 0);

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void CameraRig()
    {
        //--------------

        mouseY -= rotationSpeed * Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, -60, 96);
        cameraRig.rotation = Quaternion.Euler(new Vector3(mouseY, mouseX, 0));
        cameraRig.position = headBone.position;

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void AnimationWalk()
    {
        //------------ Walk

        if (overallSpeed > 0.5f)
        {
            if (axisVertical > 0) playerAnimator.SetInteger("StateWalk", 1);
            else if (axisVertical < 0) playerAnimator.SetInteger("StateWalk", 2);
            else if (axisHorizontal > 0) playerAnimator.SetInteger("StateWalk", 3);
            else if (axisHorizontal < 0) playerAnimator.SetInteger("StateWalk", 4);
            else playerAnimator.SetInteger("StateWalk", 0);
        }
        else playerAnimator.SetInteger("StateWalk", 0);

        //------------ Rotate

        if (rotationWork == true && ground == true)
        {
            if (Input.GetAxis("Mouse X") > 0.25f && playerAnimator.GetInteger("StateRotate") != 1)
            {
                playerAnimator.SetInteger("StateRotate", 1);
                timeDelay = Time.time + 1;
            }

            if (Input.GetAxis("Mouse X") < -0.25f && playerAnimator.GetInteger("StateRotate") != 2)
            {
                playerAnimator.SetInteger("StateRotate", 2);
                timeDelay = Time.time + 1;
            }
        }

        if (timeDelay < Time.time) playerAnimator.SetInteger("StateRotate", 0);

        //------------ Run

        if (speedchange > 1 && overallSpeed > 0.5f) playerAnimator.SetBool("Run", true);
        else if (playerAnimator.GetBool("Run")) playerAnimator.SetBool("Run", false);

        //------------ Jump

        if (ground == false) playerAnimator.SetBool("Jump", true);
        else if (playerAnimator.GetBool("Jump")) playerAnimator.SetBool("Jump", false);

        //-------------- 
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void AnimationTired()
    {
        //--------------

        if (tired == true)
        {
            if (tiredCh < 1) tiredCh += Time.deltaTime * (2 - (tiredCh * 1.9f));
            playerAnimator.SetLayerWeight(1, tiredCh);
        }
        else
        {
            if (tiredCh > 0) tiredCh -= Time.deltaTime * (0.03f + tiredCh);
            playerAnimator.SetLayerWeight(1, tiredCh);
        }

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void AnimationCrouch()
    {
        //------------

        if (ground == true && speedchange < 2)
        {
            if (Input.GetButtonDown("Crouch")) crouchOn = !crouchOn;

            if (crouchOn == true)
            {
                if (crouchCh < 1) crouchCh += Time.deltaTime * (3 - (crouchCh * 2.9f));
                playerAnimator.SetLayerWeight(2, crouchCh);
            }
            else
            {
                if (crouchCh > 0) crouchCh -= Time.deltaTime * (0.03f + (crouchCh * 2.5f));
                playerAnimator.SetLayerWeight(2, crouchCh);
            }
        }
        else
        {
            if (crouchCh > 0) crouchCh -= Time.deltaTime * (0.03f + (crouchCh * 2.0f));
            playerAnimator.SetLayerWeight(2, crouchCh);
            if (crouchOn == true && speedchange > 1) crouchOn = false;
        }

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void AnimationInjured()
    {
        //--------------

        if (Input.GetKeyDown("q")) injured = !injured;

        //--------------
        if (injured == true)
        {
            if (axisHorizontal != 0 || axisVertical != 0) crouchOn = false;
        }

        if (injured == true && crouchOn == false)
        {
            if (injuredCh < 1) injuredCh += Time.deltaTime * (3 - (injuredCh * 2.9f));
            playerAnimator.SetLayerWeight(3, injuredCh);
            tired = false;
        }
        else
        {
            if (injuredCh > 0) injuredCh -= Time.deltaTime * (0.03f + injuredCh);
            playerAnimator.SetLayerWeight(3, injuredCh);
        }

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
