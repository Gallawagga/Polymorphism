using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehavior : FlockBehavior
{
    [System.Serializable]
    public struct BehaviorGroup
    {
        public FlockBehavior behaviors;
        public float weights;
    }

    public BehaviorGroup[] behaviors;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector2 move = Vector2.zero;

        //loop through each behavior attached
        for(int i = 0; i < behaviors.Length ;i++)
        {
            //gets the calculate move method of each behavior attached
            Vector2 partialMove = behaviors[i].behaviors.CalculateMove(agent, context, flock) * behaviors[i].weights;

            if(partialMove != Vector2.zero)
            {
                //make sure the number we get for moving the agent isnt larger than the weight we gave it
                if(partialMove.sqrMagnitude > behaviors[i].weights * behaviors[i].weights)
                {
                    partialMove.Normalize();
                    partialMove *= behaviors[i].weights;
                }


                //bring all the behaviors together as one
                move += partialMove;
            }
        }

        return move;
    }
}


