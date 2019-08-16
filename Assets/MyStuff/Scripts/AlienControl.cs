using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AlienControl : MonoBehaviour
{
    Animator animator;
    Rigidbody rigidbody;
    bool following;
    float forward;
    float turn;
    public Transform girl;
    float radius;
    public float captureRadius;
    public float lostRadius;
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        following = false;
        messageText.text = "Storm Area 51!";

    }
    float toLook;
    public Text deathText;
    public Text messageText;
    public float speed;
    public float runSpeed;
    void Update()
    {
        messageText.enabled = !deathText.enabled;
        radius = (girl.transform.position - transform.position).magnitude;
        if (radius<captureRadius)
        {
            following = true;
        }
        if (following)
        {
            if (radius > lostRadius)
            {
                animator.SetFloat("Radius", 0);
                messageText.text = "Mr Grey is Lost!";


            }
            else
            {
                if (radius > 3)
                {
                    rigidbody.velocity = -speed*(transform.position - girl.position).normalized;
                    animator.SetFloat("Radius", runSpeed);
                    messageText.text = "Find Mr Grey's UFO!";


                }
                else
                {
                    rigidbody.velocity = new Vector3();
                    animator.SetFloat("Radius", 0);

                }
                transform.LookAt(girl);

            }

        }
    }
}
