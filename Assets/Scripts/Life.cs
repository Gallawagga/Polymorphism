using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : Flock
{
    [Header("Life Variables"), Tooltip("Variables belonging to the Life class")]
    [SerializeField] private float damage;
    public float maxHealth;
    public float currentHealth;
    public float speedmultiplier;
    protected Color agentColor;    //this is randomised every Start function. Taste the rainbow.

    private bool startedCo;
    private bool waited;

    protected void Awake()
    {
        agentColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)); //sets the agent color. 
        agentPrefab.agentSprite.color = agentColor;
    }

    IEnumerator waitforFiveSeconds(int secs)
    {
        startedCo = true;
        yield return new WaitForSeconds(secs);
        waited = true;
        startedCo = false;
        yield return null;
    }
}
