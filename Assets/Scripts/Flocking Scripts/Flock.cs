using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    protected List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;

    [Header("Flock Variables"), Tooltip("Variables belonging to the Flock class")]
    [Range(5,50)]
    public int startingCount = 25;
    [Range (1f, 100f)]
    public float driveFactor = 10f;
    [Range (1f,100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;
    [Range(0f, 1f)]
    public float smallRadiusMultiplier = 0.2f;

    const float AgentDensity = 0.08f;
    protected float squareMaxSpeed;
    protected float squareNeighborRadius;
    protected float squareAvoidanceRadius;
    protected float squareSmallRadius;

    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    public float SquareSmallRadius { get { return squareSmallRadius; } }

    protected virtual void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
        squareSmallRadius = squareNeighborRadius * smallRadiusMultiplier * smallRadiusMultiplier;

        //loops for startingCount times. creates StratingCount amount of agents on game start.
        for (int i = 0; i < startingCount; i++)
        {
            var xz = Random.insideUnitCircle * startingCount * AgentDensity;
            var newSpawn = new Vector3(xz.x, xz.y) + transform.position;

            //create a new agent (the agent is the AI)
            FlockAgent newAgent = Instantiate( //instantiate creates a clone of a gameobject or prefab
                agentPrefab, //this is the prefab being cloned
                newSpawn, // give in a random position within a circle
                Quaternion.Euler(Vector3.forward * Random.Range(0 , 360f)), //give it a random rotation
                transform  //this transform is the parent of the new AI agent
                );
            newAgent.name = "Agent " + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }
    }

    protected virtual void Update()
    {
        foreach(FlockAgent agent in agents) //go through every agent in the flock
        {
            List<Transform> context = GetNearbyObjects(agent);

            //FOR TESTING
            //agent.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f);
            
            Vector2 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if(move.sqrMagnitude > squareMaxSpeed) //we square them now so we save resources later. 
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }
    }

    protected List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach(Collider2D c in contextColliders) // detecting  collider components around the agent to the radius of neighbourRadius, then adding them to the context list.
        {
            if(c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }

        return context; // like getting and setting, it is important to return a value if that's what the function is setting.
    }

}
