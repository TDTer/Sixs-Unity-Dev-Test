using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

#if UNITY_EDITOR 
using UnityEditor;
#endif

public class PlayerController : MonoBehaviour
{
    private const float RADIUS_RANGE = 1.5f;
    public const int SCORE_PER_BALL = 5;

    [SerializeField] private Animator anim;

    private float speed = 5.0f;
    private float velocity = 0.0f;
    private float acceleration = 1.5f;
    private float deceleration = 4.0f;
    private int blendHash;
    private float rotationSpeed = 5.0f;
    private Ball targetBall;


    public bool IsCanKick => targetBall;
    public int Score { get; set; }



    void Start()
    {
        blendHash = Animator.StringToHash(Constant.BLEND_STRING);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        targetBall = CheckBallInRange();
    }


    private void Move()
    {

        Vector3 inputDirection = new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal"));

        if (inputDirection != Vector3.zero)
        {
            if (velocity < 1)
            {
                velocity += Time.deltaTime * acceleration;
            }

            // Rotate towards the input direction
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(inputDirection),
                Time.deltaTime * rotationSpeed
            );

            transform.position += inputDirection * speed * Time.deltaTime * velocity;
            anim.SetFloat(blendHash, velocity);
        }
        else
        {
            if (velocity > 0)
            {
                velocity -= Time.deltaTime * deceleration;
            }

            anim.SetFloat(blendHash, velocity);
        }
    }


    public void Kick()
    {
        if (targetBall != null)
        {
            targetBall.onKick();
        }
    }

    Ball CheckBallInRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, RADIUS_RANGE);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(Constant.BALL_TAG))
            {
                return hitCollider.GetComponent<Ball>();
            }

        }
        return null;
    }


    //Check Platform
    //     private RuntimePlatform platform
    //     {
    //         get
    //         {
    // #if UNITY_ANDROID
    //             return RuntimePlatform.Android;
    // #elif UNITY_IOS 
    //             return RuntimePlatform.IPhonePlayer;
    // #elif UNITY_STANDALONE_OSX
    //              return RuntimePlatform.OSXPlayer;
    // #elif UNITY_STANDALONE_WIN
    //             return RuntimePlatform.WindowsPlayer;
    // #endif
    //         }
    //     }

    //     public bool isTouchInterface
    //     {
    //         get
    //         {
    // #if UNITY_EDITOR
    //             // Game is being played in the editor and the selected BuildTarget is either Android or iOS
    //             if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android ||
    //         EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS)
    //             {
    //                 return true;
    //             }
    // #endif
    //             // Game is being played on an Android or iOS device
    //             if (platform == RuntimePlatform.Android ||
    //             platform == RuntimePlatform.IPhonePlayer)
    //             {
    //                 return true;
    //             }
    //             // Game is being played on something other then an Android or iOS device
    //             else
    //             {
    //                 return false;
    //             }
    //         }
    //     }
}
