using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : Life
{
    [Header("Prey Variables")]
    public CompositeBehavior wanderBehavior;
    public CompositeBehavior fleeBehavior;
    [SerializeField] List<FlockAgent> calmAgents = new List<FlockAgent>();               
    public enum PreyState { wander, flee }
    [SerializeField] private PreyState currentState;

    public PreyState GetCurrentState() //oldschool getter
    { return currentState; }
    public void SetCurrentState(PreyState newState) //oldschool setter
    { currentState = newState; }

    protected override void Update()
    {

        switch (GetCurrentState())
        {
            case PreyState.wander:
                base.Update(); //the base move function for the flock!
                behavior = wanderBehavior;
                foreach (FlockAgent agent in agents) //check if any agent is near a predator...
                {
                    agent.agentSprite.color = agentColor; //change colour back to white.
                    Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius); //send out an overlap circle from each agent which tracks colliders.
                    foreach (Collider2D item in contextColliders)
                    {
                        FlockAgent itemAgent = item.GetComponent<FlockAgent>(); //check each found collider against having a flockagent script.
                        if (itemAgent != null && itemAgent.AgentFlock is Predator) //looking for an agent with a DIFFERENT flock script attached!
                        {
                            SetCurrentState(PreyState.flee);        //set the current state to flee
                            GetCurrentState();                      //getter for flee
                        }
                    }
                }
                break;

            case PreyState.flee:
                base.Update();
                behavior = fleeBehavior;

                foreach (FlockAgent agent in agents)//counts all the agents not being chased by predators, when it gets to 75% of the flock size, return to wander. 
                {
                    agent.agentSprite.color = Color.yellow;  //yellow is the universal colour of cowardice.
                    Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius); //send out collider-search
                    List<FlockAgent> predatorWatchList = new List<FlockAgent>();        //declaring a temp list looking for predators nearby
                   
                    foreach (Collider2D item in contextColliders)
                    {
                        FlockAgent itemAgent = item.GetComponent<FlockAgent>(); //check each found collider against having a flockagent script.
                        if (itemAgent != null && itemAgent.AgentFlock is Predator && !predatorWatchList.Contains(itemAgent)) //looking for an agent with a DIFFERENT flock script attached!
                        {
                            predatorWatchList.Add(itemAgent);
                        }
                    }
                    if (predatorWatchList.Count == 0)    //IF there are no predator colliders found
                    {
                        if (!calmAgents.Contains(agent))    //IF agent isn't already in calmAgents
                        {
                            calmAgents.Add(agent);          //add agent to calmAgents
                        }
                        if (calmAgents.Count > (agents.Count * 0.75)) //if 3/4 of the agents don't have predators nearby.
                        {
                            predatorWatchList.Clear();              
                            calmAgents.Clear();                       //clear both temp lists.
                            SetCurrentState(PreyState.wander);        //set the current state to flee
                            GetCurrentState();                        //getter
                        }
                    }
                    else  //ELSE (there are predator colliders found)
                    {
                        if (calmAgents.Contains(agent)) //IF agent is in calmAgents
                        {
                            calmAgents.Remove(agent); //remove from calmAgents
                        }
                    }
                }
                break;
        }
    }


    protected override void Start()
    {
        base.Start();
        SetCurrentState(PreyState.wander);
        GetCurrentState();
    }


    IEnumerator waitforSeconds(float secs)
    {
        yield return new WaitForSeconds(secs);

    }


}
