using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Wander")]
public class WanderBehavior : FilteredFlockBehavior
{
    Vector2 waypointDirection = Vector2.zero;
    Path path = null;
    int currentWaypoint = 0;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (path == null) //if there's no path to be followed
        {
            FindPath(agent, context); //find a path!
        }

        return FollowPath(agent);
    }

    public bool InRadius(FlockAgent agent)
    {
        waypointDirection = (Vector2)path.waypoints[currentWaypoint].position - (Vector2)agent.transform.position;

        //waypointDirection.magnitude would give us the distance of the vector. 
        if (waypointDirection.magnitude < path.radius) 
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private Vector2 FollowPath(FlockAgent agent)
    {
        if (path == null)
        {
            return Vector2.zero;
        }

        if(InRadius(agent)) //if inside the radius of a path, then go to the next one. 
        {
            currentWaypoint++;
            if(currentWaypoint >= path.waypoints.Count)
            {
                currentWaypoint = 0;
            }
            return Vector2.zero;
        }
        return waypointDirection;
    }

    private void FindPath(FlockAgent agent, List<Transform> context) //use a filter and find a path
    {
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        if (filteredContext.Count == 0)
        {
            return;
        }

        int randomPath = Random.Range(0, filteredContext.Count);
        path = filteredContext[randomPath].GetComponentInParent<Path>();

    }

}
