using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Same Flock")]
public class SameFlockFilter : ContextFilter
{
   
    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();
        foreach(Transform item in original)
        {
            FlockAgent itemAgent = item.GetComponent<FlockAgent>();
            if (itemAgent != null) // i.e if itemAgent exists! Could separate both necessities into different if statements but I prefer it this way.
            {
                if (itemAgent.AgentFlock == agent.AgentFlock)
                {
                    //"we're just trying to filter for it being in the same flock, that's all" - Andrew
                    filtered.Add(item);
                }
            }
        }
        return filtered;
    }

}
