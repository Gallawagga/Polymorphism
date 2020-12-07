using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Steered Cohesion")]
public class SteeredCohesionBehavior : FilteredFlockBehavior //smoothly steers the flock in the direciton they want to go, rather than them jankily turning immediately. 
{
    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neigbours, return no adjustment
        if (context.Count == 0)
        {
            return Vector2.zero;
        }

        //add all points together and average
        Vector2 cohesionMove = Vector2.zero;

        //if(filter == null){filteredcontext = context} else {filteredContext = filter.Filter(agent, context)} - scheduled for next term, week 14.
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach(Transform item in context)
        {
            cohesionMove += (Vector2)item.position;
        }
        cohesionMove /= context.Count;

        //create offset from agent position
        cohesionMove -= (Vector2)agent.transform.position;
        cohesionMove = Vector2.SmoothDamp(agent.transform.up, cohesionMove, ref currentVelocity,agentSmoothTime);
        return cohesionMove;
    }
}
