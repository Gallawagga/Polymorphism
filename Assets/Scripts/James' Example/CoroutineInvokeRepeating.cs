using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineInvokeRepeating : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InvokeHelloRepeating(0, 1f));   
    }

    private IEnumerator InvokeHelloRepeating (float _delay, float _time)
    {
        yield return new WaitForSeconds(_delay);

        do
        {
            LogHello();

            yield return new WaitForSeconds(_time);
        }
        while (true);
    }

    private void LogHello ()
    {
        Debug.Log("Hello");

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
