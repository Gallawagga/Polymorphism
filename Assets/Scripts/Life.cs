using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;

    [Range(10, 500)]
    public int startingCount = 250;
    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;
    [Range(0f, 1f)]
    public float smallRadiusMultiplier = 0.2f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    float squareSmallRadius;

    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    public float SquareSmallRadius { get { return squareSmallRadius; } }

    private void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
        squareSmallRadius = squareNeighborRadius * smallRadiusMultiplier * smallRadiusMultiplier;

        //loops for startingCount times. creates StratingCount amount of agents on game start.
        for (int i = 0; i < startingCount; i++)
        {
            //create a new agent (the agent is the AI)
            FlockAgent newAgent = Instantiate( //instantiate creates a clone of a gameobject or prefab
                agentPrefab, //this is the prefab being cloned
                Random.insideUnitCircle * startingCount * AgentDensity, // give in a random position within a circle
                Quaternion.Euler(Vector3.forward * Random.Range(0, 360f)), //give it a random rotation
                transform  //this transform is the parent of the new AI agent
                );
            newAgent.name = "Agent " + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }
    }


}
