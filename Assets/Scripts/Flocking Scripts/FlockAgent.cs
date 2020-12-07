using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//we have to be attached to a gameobject that has a Collider2D Also attached
[RequireComponent(typeof(Collider2D))] 
public class FlockAgent : MonoBehaviour
{
    Flock agentFlock;
    public SpriteRenderer agentSprite;
    public Flock AgentFlock { get { return agentFlock; } }

    private Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }

    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    public void Initialize(Flock flock)
    {
        agentFlock = flock;
    }

    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;

        if(velocity.x >= 0)
        { agentSprite.flipY = true; }
        else
        { agentSprite.flipY = false; }
    }
}
