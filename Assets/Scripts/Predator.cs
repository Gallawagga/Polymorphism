using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : Life
{
    [Header("Predator Variables")]
    public CompositeBehavior searchBehavior;
    public CompositeBehavior pursueBehavior;
    [SerializeField] List<FlockAgent> hungryAgents = new List<FlockAgent>();

    public enum PredatorState { search, pursue }
    [SerializeField] private PredatorState currentState;
    public PredatorState GetCurrentState() //oldschool getter
    { return currentState; }
    public void SetCurrentState(PredatorState newState) //oldschool setter
    { currentState = newState; }

    protected override void Update()
    {

        switch (GetCurrentState())
        {
            case PredatorState.search:
                base.Update();
                foreach (FlockAgent agent in agents) //check if any agent is near something yummy...
                {
                    agent.agentSprite.color = agentColor; //change colour back to white.
                    Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius); //send out an overlap circle from each agent which tracks colliders.

                    foreach (Collider2D item in contextColliders)
                    {
                        FlockAgent itemAgent = item.GetComponent<FlockAgent>(); //check each found collider against having a flockagent script.
                        if (itemAgent != null && itemAgent.AgentFlock is Prey) //looking for an agent with a DIFFERENT flock script attached!
                        {
                            SetCurrentState(PredatorState.pursue);
                            GetCurrentState();
                        }
                    }
                }
                break;

            case PredatorState.pursue:
                base.Update();
                foreach (FlockAgent agent in agents)
                {
                    agent.agentSprite.color = Color.red;  //red is the colour of BLOOOOOOOOOOOOOD
                    Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, (neighborRadius * 1.3f));//send out collider-search
                    List<FlockAgent> preyWatchList = new List<FlockAgent>();

                    foreach (Collider2D item in contextColliders)
                    {
                        FlockAgent itemAgent = item.GetComponent<FlockAgent>(); //check each found collider against having a flockagent script.
                        if (itemAgent != null && itemAgent.AgentFlock is Prey && !preyWatchList.Contains(itemAgent))
                        {
                            preyWatchList.Add(itemAgent);       //storing any seen prey every frame
                            if (!hungryAgents.Contains(agent))  
                            {
                                hungryAgents.Add(agent);        //deciding if predator is hungry
                            }
                        }
                        if(preyWatchList.Count == 0)            //if there is no prey nearby...
                        {
                            if (hungryAgents.Contains(agent))
                            {
                                hungryAgents.Remove(agent);     //predator is no longer hungry.
                            }
                        }
                    }   //the above code states that if there are any prey nearby, then the agent is HUNGRY

                    if (hungryAgents .Count == 0)
                    {
                        hungryAgents.Clear();
                        SetCurrentState(PredatorState.search);
                        GetCurrentState();
                    }
                    //if there are any prey nearby, add to the hungry group
                    //if the hungry group numbers less than 25% of the flock's total size, 
                }
                break;
        }
    }

    protected override void Start()
    {
        base.Start();
        behavior = searchBehavior;
        SetCurrentState(PredatorState.search);
        GetCurrentState();
    }

}
