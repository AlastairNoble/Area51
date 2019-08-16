using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class ufo : MonoBehaviour
{
    Rigidbody rbody;
    public bool endgame;
    public float vert;
    public float delay;
    public Text deathText;
    public float endingTimer;
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        endgame = false;
    }
    void Update()
    {
        if (endgame)
        {
            delay -= Time.deltaTime;
            if (delay < 0)
            {
                rbody.AddForce(new Vector3(0, vert, 0));
                deathText.enabled = true;
                deathText.text = "Mr Grey is Saved!";
                endingTimer -= Time.deltaTime;
                if (endingTimer < 0)
                {
                    SceneManager.LoadScene("StartScene");
                }
            }

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Mr Grey")
        {
            endgame = true;
        }
    }
}
