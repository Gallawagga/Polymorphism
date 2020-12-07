using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Physics Layer")]
public class PhysicsLayerFilter : ContextFilter
{
    public LayerMask mask;

    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();
        foreach (Transform item in original)
        {

            //BITWISE OPERATOR '<<'
            //shifts digit in binary code to left X times, or essentially multiplies the number by 2 X times. \

            //BITWISE OPERATOR '|'
            //The | operator compares each binary digit across two integers and gives back a 1 if either of them are 1. 

            if (mask == (mask | (1 << item.gameObject.layer))) //does the layer in the original List<Transform> exist in this mask? Either or? It allows multiple layers to be selected, adding all their binary values together. 
            {
                filtered.Add(item);
            }

        }
        return filtered;
    }

}

