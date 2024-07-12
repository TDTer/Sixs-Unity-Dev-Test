using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public const float TIME_ON_DELAY = 2.0f;

    [SerializeField] Transform scoregoal;
    [SerializeField] Rigidbody ballRg;
    [SerializeField] ParticleSystem goalParticle;

    private float speed = 2000.0f;

    //property
    public Transform Scoregoal
    {
        get => scoregoal;
        set
        {
            scoregoal = value;
        }
    }

    void Start()
    {
        ballRg = GetComponent<Rigidbody>();
    }

    public void onKick()
    {
        CameraFollower.Ins.ChangeTarget(transform);
        ballRg.AddForce((scoregoal.position - transform.position).normalized * speed * Time.deltaTime, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.SCORE_GOAL_TAG))
        {
            Invoke(nameof(ChangeCameraAfterDelay), TIME_ON_DELAY);
            UI.Ins.UpdateScore(PlayerController.SCORE_PER_BALL);

            this.gameObject.SetActive(false);
            Instantiate(goalParticle, transform.position, Quaternion.identity);
        }
    }

    private void ChangeCameraAfterDelay()
    {
        CameraFollower.Ins.ChangeCameraToPlayer();
        OnDespawn();
    }

    private void OnDespawn()
    {
        FindObjectOfType<SpawnManager>().RemoveBall(this);
        Destroy(gameObject);
    }


}
