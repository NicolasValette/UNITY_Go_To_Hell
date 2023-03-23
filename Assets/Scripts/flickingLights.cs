using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//script pour faire clignotter les lumieres au depart du scenario
public class flickingLights : MonoBehaviour
{
    public GameObject[] flickeringLights;
    public float minFlickTime = 0.1f;
    public float maxFlickTime = 0.4f;

    Light lights;
    // Start is called before the first frame update
    void Start()
    {
        flickeringLights = GameObject.FindGameObjectsWithTag("FlickeringLight");
        for ( int i = 0; i < flickeringLights.Length; i++ )
        {
            lights = flickeringLights[i].GetComponent<Light>();
            StartCoroutine(Flicker(lights));
        }
    }

    // Update is called once per frame
    IEnumerator Flicker(Light lights)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minFlickTime, maxFlickTime));
            lights.enabled = !lights.enabled;
        }
    }
}
