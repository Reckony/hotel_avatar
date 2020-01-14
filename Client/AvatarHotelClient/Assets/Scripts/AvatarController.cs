using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class AvatarController : MonoBehaviour
{
    public float animSpeed = 1.5f;             
    public float lookSmoother = 3.0f;           
    public bool useCurves = true;                                                         
    public float useCurvesHeight = 0.5f;       
    public float forwardSpeed = 7.0f;
    public float backwardSpeed = 2.0f;
    public float rotateSpeed = 2.0f;
    public float moveSpeed = 3.0f;
    public float jumpPower = 3.0f;
    public SpeechController speechController;
    public PanelContainer panelHandler;
    public PictureHandler galleryCanvasScript;
    private CapsuleCollider col;
    private Rigidbody rb;
    private Vector3 velocity;
    private float orgColHight;
    private Vector3 orgVectColCenter;
    private Animator anim;                         
    private AnimatorStateInfo currentBaseState;
    private bool jumpCommand;
    private bool restCommand;
    private bool greetCommand;
    public bool pointCommand;
    private bool moveAvatar;
    private bool resetCommand;

    static int idleState = Animator.StringToHash("Base Layer.Idle");
    static int locoState = Animator.StringToHash("Base Layer.Locomotion");
    static int jumpState = Animator.StringToHash("Base Layer.Jump");
    static int restState = Animator.StringToHash("Base Layer.Rest");
    static int greetState = Animator.StringToHash("Base Layer.Greet");

    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        orgColHight = col.height;
        orgVectColCenter = col.center;
        jumpCommand = false;
        restCommand = false;
        moveAvatar = false;
        resetCommand = false;
        InvokeRepeating("Rest", 60f, 60f);
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");          
        float v = Input.GetAxis("Vertical");          
        anim.SetFloat("Speed", v);                     
        anim.SetFloat("Direction", h);                 
        anim.speed = animSpeed;                       
        currentBaseState = anim.GetCurrentAnimatorStateInfo(0); 
        rb.useGravity = true;

        velocity = new Vector3(0, 0, v);                                              
        velocity = transform.TransformDirection(velocity);

        if (v > 0.1)
        {
            velocity *= forwardSpeed;    
        }
        else if (v < -0.1)
        {
            velocity *= backwardSpeed;  
        }

        if (jumpCommand == true)
        {
            if (!anim.IsInTransition(0))
            {
                anim.SetBool("Jump", true);
                jumpCommand = false;
            }
        }
        transform.localPosition += velocity * Time.fixedDeltaTime;

        transform.Rotate(0, h * rotateSpeed, 0);

        if (currentBaseState.nameHash == locoState)
        {
            if (useCurves)
            {
                resetCollider();
            }
        }

        else if (currentBaseState.nameHash == jumpState)
        {
                                                                   
            if (!anim.IsInTransition(0))
            {
                if (useCurves)
                {
                    float jumpHeight = anim.GetFloat("JumpHeight");
                    float gravityControl = anim.GetFloat("GravityControl");
                    if (gravityControl > 0)
                        rb.useGravity = false; 

                    Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
                    RaycastHit hitInfo = new RaycastHit();

                    if (Physics.Raycast(ray, out hitInfo))
                    {
                        if (hitInfo.distance > useCurvesHeight)
                        {
                            col.height = orgColHight - jumpHeight;         
                            float adjCenterY = orgVectColCenter.y + jumpHeight;
                            col.center = new Vector3(0, adjCenterY, 0); 
                        }
                        else
                        {					
                            resetCollider();
                        }
                    }
                }		
                anim.SetBool("Jump", false);
            }
        }

        else if (currentBaseState.nameHash == idleState)
        {
            if (useCurves)
            {
                resetCollider();
            }
            if (restCommand == true)
            {
                anim.SetBool("Rest", true);
                restCommand = false;
            }
            if(greetCommand == true)
            {
                anim.SetBool("Greet", true);
                greetCommand = false;
            }
            if(pointCommand == true)
            {
                anim.SetBool("Point", true);
            }
        }
        else if (currentBaseState.nameHash == restState)
        {
            if (!anim.IsInTransition(0))
            {
                anim.SetBool("Rest", false);
            }
        }
        else if (currentBaseState.nameHash == greetState)
        {
            if (!anim.IsInTransition(0))
            {
                anim.SetBool("Greet", false);
            }
        }
        if(moveAvatar)
        {
            var currentPosition = transform.position;
            transform.position = Vector3.MoveTowards(
                currentPosition,
                new Vector3(-1.5f, currentPosition.y, currentPosition.z),
                moveSpeed * Time.deltaTime);
            Quaternion newRotation = Quaternion.AngleAxis(30, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, moveSpeed * Time.deltaTime);
            if (currentPosition.x == -1.5f && transform.rotation == newRotation)
            {
                moveAvatar = false;
            }
        }
        if (resetCommand)
        {
            var currentPosition = transform.position;
            transform.position = Vector3.MoveTowards(
                currentPosition,
                new Vector3(0f, currentPosition.y, currentPosition.z),
                moveSpeed * Time.deltaTime);
            Quaternion newRotation = Quaternion.AngleAxis(0, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, moveSpeed * Time.deltaTime);
            if (transform.rotation == newRotation)
            {
                resetCommand = false;
                pointCommand = false;
                anim.SetBool("Point", false);
            }
        }
    }

    void resetCollider()
    {
        col.height = orgColHight;
        col.center = orgVectColCenter;
    }

    public void Jump()
    {
        jumpCommand = true;
    }

    public void Rest()
    {
        restCommand = true;
    }

    public void Greet(bool withAnswer)
    {
        greetCommand = true;
        if (withAnswer)
            speechController.greetWithAnswer();
        else
            speechController.greetWithoutAnswer();
    }

    public void Answer()
    {
        greetCommand = true;
        speechController.onlyAnswer();
    }

    public void offerHelp()
    {
        greetCommand = true;
        speechController.offerHelp();
    }

    public void showMap()
    {
        clearPanelsExceptWhiteBackground();
        panelHandler.whiteBackgroundTransition.makeVisible();
        foreach (var gameObject in panelHandler.locationTransitions)
        {
            gameObject.makeVisible();
        }
        moveAvatar = true;
        pointCommand = true;
    }

    public void reset()
    {
        clearPanels();
        resetCommand = true;
    }

    public void clearPanels()
    {
        if (panelHandler.whiteBackgroundTransition.gameObject.activeInHierarchy)
            panelHandler.whiteBackgroundTransition.makeInvisible();
        clearPanelsExceptWhiteBackground();
    }

    public void clearPanelsExceptWhiteBackground()
    {
        galleryCanvasScript.returnToOriginal();
        if (panelHandler.darkBackgroundTransition.gameObject.activeInHierarchy)
            panelHandler.darkBackgroundTransition.makeInvisible();
        foreach (var gameObject in panelHandler.reviewsTransitions)
        {
            if (gameObject.gameObject.activeInHierarchy)
                gameObject.makeInvisible();
        }
        foreach (var gameObject in panelHandler.locationTransitions)
        {
            if (gameObject.gameObject.activeInHierarchy)
                gameObject.makeInvisible();
        }
        foreach (var gameObject in panelHandler.offertFirstPageTransitions)
        {
            if (gameObject.gameObject.activeInHierarchy)
                gameObject.makeInvisible();
        }
        foreach (var gameObject in panelHandler.offertSecondPageTransitions)
        {
            if (gameObject.gameObject.activeInHierarchy)
                gameObject.makeInvisible();
        }
        foreach (var gameObject in panelHandler.reviewsTransitions)
        {
            if (gameObject.gameObject.activeInHierarchy)
                gameObject.makeInvisible();
        }
        foreach (var gameObject in panelHandler.menuFirstPageTransitions)
        {
            if (gameObject.gameObject.activeInHierarchy)
                gameObject.makeInvisible();
        }
        foreach (var gameObject in panelHandler.menuSecondPageTransitions)
        {
            if (gameObject.gameObject.activeInHierarchy)
                gameObject.makeInvisible();
        }
        foreach (var gameObject in panelHandler.roomsTransitions)
        {
            if (gameObject.gameObject.activeInHierarchy)
                gameObject.makeInvisible();
        }
    }

    public void showOffert()
    {
        clearPanelsExceptWhiteBackground();
        panelHandler.whiteBackgroundTransition.makeVisible();
        foreach (var gameObject in panelHandler.offertFirstPageTransitions)
        {
            gameObject.makeVisible();
        }
        moveAvatar = true;
        pointCommand = true;
    }

    public void showGallery()
    {
        clearPanels();
        moveAvatar = true;
        pointCommand = true;
        galleryCanvasScript.centerImage();
    }

    public void showRooms()
    {
        clearPanelsExceptWhiteBackground();
        panelHandler.whiteBackgroundTransition.makeVisible();
        foreach (var gameObject in panelHandler.roomsTransitions)
        {
            gameObject.makeVisible();
        }
        moveAvatar = true;
        pointCommand = true;
    }

    public void showReviews()
    {
        clearPanels();
        panelHandler.darkBackgroundTransition.makeVisible();
        foreach(var gameObject in panelHandler.reviewsTransitions)
        {
            gameObject.makeVisible();
        }
        moveAvatar = true;
        pointCommand = true;
    }

    public void showMenu()
    {
        clearPanelsExceptWhiteBackground();
        panelHandler.whiteBackgroundTransition.makeVisible();
        foreach (var gameObject in panelHandler.menuFirstPageTransitions)
        {
            gameObject.makeVisible();
        }
        moveAvatar = true;
        pointCommand = true;
    }
}
