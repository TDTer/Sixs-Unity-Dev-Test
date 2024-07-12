using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] public GameObject ballPrefab;
    [SerializeField] Transform minPos;
    [SerializeField] Transform maxPos;
    [SerializeField] Transform firstSoccerGoal;
    [SerializeField] Transform secondSoccerGoal;

    [SerializeField] private List<Ball> balls = new List<Ball>();

    void Awake()
    {
        SpawnBall(5);
    }

    void Update()
    {
        if (balls.Count == 0)
        {
            SpawnBall(5);
        }
    }

    private Vector3 RandomPosition()
    {
        float randomX = Random.Range(minPos.position.x, maxPos.position.x);
        float randomZ = Random.Range(minPos.position.z, maxPos.position.z);

        return new Vector3(randomX, minPos.position.y, randomZ);
    }

    private void SpawnBall(int ballNumber)
    {
        for (int i = 0; i < ballNumber; i++)
        {
            Ball ball = Instantiate(ballPrefab, RandomPosition(), Quaternion.identity).GetComponent<Ball>();
            ball.Scoregoal = GetNearestGoal(ball.transform);

            balls.Add(ball);
        }
    }

    // Check the soccer goal nearst to ball
    private Transform GetNearestGoal(Transform ballTransform)
    {
        float distanceToFirstGoal = Vector3.Distance(ballTransform.position, firstSoccerGoal.position);
        float distanceToSecondGoal = Vector3.Distance(ballTransform.position, secondSoccerGoal.position);

        return distanceToFirstGoal < distanceToSecondGoal ? firstSoccerGoal : secondSoccerGoal;
    }

    public void RemoveBall(Ball ballToRemove)
    {
        balls.Remove(ballToRemove);
    }


    public Ball farthestBall(Transform playerTransform)
    {
        Ball farthestBall = null;
        float maxDistance = 0f;

        foreach (Ball ball in balls)
        {
            float distance = Vector3.Distance(playerTransform.position, ball.transform.position);
            if (distance > maxDistance)
            {
                maxDistance = distance;
                farthestBall = ball;
            }
        }

        return farthestBall;
    }
}
