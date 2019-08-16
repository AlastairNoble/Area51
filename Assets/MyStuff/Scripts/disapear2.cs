using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disapear2 : MonoBehaviour
{
    public Transform girl;
    MeshRenderer mesh;
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        mesh.enabled = girl.position.z < -91;
    }
}
