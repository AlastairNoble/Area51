using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disapear : MonoBehaviour
{
    Collider m_Collider;
    public Transform girl;
    public Light light;
    public float insideLight;
    void Start()
    {
        m_Collider = GetComponent<Collider>();
    }
    void Update()
    {

        foreach (Transform child in transform)
            child.GetComponent<MeshRenderer>().enabled = !m_Collider.bounds.Contains(girl.position); //make walls disapear if girl is in building

        if (m_Collider.bounds.Contains(girl.position)){
            light.intensity = insideLight;
        }else
        {
            light.intensity = 1;
        }


    }
}
