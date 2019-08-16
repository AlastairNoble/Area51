using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DickControl : MonoBehaviour
{
    public bool isDead;
    public Animator animator;
    Collider collider;
    public GameObject girl;
    public float shootRad;
    public Transform tip;
    RaycastHit hit;
    public LineRenderer line;
    public Transform patrolP;
    public List<Vector3> temp;
    void Start()
    {
        isDead = false;
        animator = GetComponent<Animator>();
        line.enabled = false;
        timer = shotInterval;
        timer1 = shotDuration;
        collider = GetComponent<Collider>();

        foreach (Transform child in patrolP)
        {
            temp.Add(child.position);
        }
        patrolPoints = temp.ToArray();

    }
    RaycastHit sight;
    public bool sighted = false;
    public float runRad;
    public float deleteTimer;
    public bool deletable;
    void Update()
    {
        if (this.name == "dead")
        {
            isDead = true;
            animator.enabled = false;
            deleteTimer -= Time.deltaTime;
            collider.enabled = false;

            if (deleteTimer < 0 && deletable)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            Physics.Raycast(transform.position, girl.transform.position - transform.position, out sight);
            if ((girl.transform.position - transform.position).magnitude < shootRad)// && sight.rigidbody.name == girl.name)
            {
                sighted = true;
                animator.SetBool("Patrolling", false);
            }

            if (sighted)
            {
                aim();
                if ((girl.transform.position - transform.position).magnitude < runRad){

                    fire();
                    animator.SetBool("Shooting", true);
                }
                else //chase her
                {
                    animator.SetBool("Shooting", false);
                }

            } else {
                patrol();
                animator.SetBool("Patrolling", true);
            }
        }

    }
    float timer;
    float timer1;
    public float shotDuration;
    public float shotInterval;
    bool doOnce;
    void fire()
    {
        timer -= Time.deltaTime;
        if (timer<0)
        {
            line.enabled = true;
            timer1 -= Time.deltaTime;
            if (doOnce)
            {
                if (Physics.Raycast(tip.position, tip.forward, out hit))
                {
                    if (hit.collider.name == "girl")
                    {
                        PlayerPrefs.SetFloat("Health",(float)(PlayerPrefs.GetFloat("Health")-.1));
                        doOnce = false;
                    }
                }
            }

            if (timer1 < 0)
            {
                line.enabled = false;
                timer = shotInterval;
                timer1 = shotDuration;
                doOnce = true;
            }
        }
        
    }
    void aim()
    {
        transform.LookAt(girl.transform.position);
    }
    public Vector3[] patrolPoints;
    int toMove = 0;
    void patrol()
    {
        if ((transform.position - patrolPoints[toMove]).magnitude < 1)
        {
            toMove++;
            toMove %= patrolPoints.Length;
        } else
        {
            transform.LookAt(patrolPoints[toMove]);

        }
    }
    
}
