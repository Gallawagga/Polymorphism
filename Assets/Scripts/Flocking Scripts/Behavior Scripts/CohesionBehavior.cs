using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class CohesionBehavior : FilteredFlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if(context.Count == 0)
        {
            return Vector2.zero;
        }

        //add all points together and get the average
        Vector2 cohesionMove = Vector2.zero;
        //code taken from SteeredCohesionBehavior
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in context)
        {
            cohesionMove += (Vector2)item.position;
        }
        cohesionMove /= context.Count;

        //create offset from agent position

        //direction from a to b = b - a
        cohesionMove -= (Vector2)agent.transform.position;
        return cohesionMove;

    }
        
}
