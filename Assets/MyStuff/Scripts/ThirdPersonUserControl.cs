using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    
    public class ThirdPersonUserControl : MonoBehaviour
    {
        public Animator gunAnimator;
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private bool m_Jump;                      

        private float forward;
        private float sideways;
        private float turn;
        private float turnUp;
        public Text deathText;

        public Transform spawnP;
        public List<Transform> temp;
        public Transform[] spawnPoints;

        private void Start()
        {
            numDicks = 1;
            PlayerPrefs.SetFloat("Health",1);
            deathText.enabled = false;
            m_Character = GetComponent<ThirdPersonCharacter>();
            //line = GetComponent<LineRenderer>();
            dick.name ="Dick";
            isDead = false;

            foreach (Transform child in spawnP)
                temp.Add(child);
            spawnPoints = temp.ToArray();

        }
        Vector2 move;
        public Button shootButton;

        private void FixedUpdate()
        {
            if (isDead)
            {
                death();
            } else
            {
                if (Application.platform == RuntimePlatform.Android)
                {
                    getAndroidInput();//sets toMove
                }
                else
                {
                    getKeyboardInput();//sets toMove
                }
                move.x = sideways;
                move.y = forward;
                m_Character.Move(move, turn, turnUp, m_Jump);

                m_Jump = false;


                SpawnNewEnimies();
                Aim();
                Shoot();
                healthBar();

                checkDeath();
            }
            
        }
        private void LateUpdate()
        {
            Canvas.ForceUpdateCanvases();
            
        }

        public Joystick moveJoy;
        public Joystick aimJoy;
        private void getAndroidInput()
        {
            sideways = moveJoy.Horizontal*2;
            forward = moveJoy.Vertical*2;

            turn = aimJoy.Horizontal;
            turnUp = aimJoy.Vertical;
        }
        public void ShootyShoot()
        {
            shooting = !shooting;
        }
        private void getKeyboardInput()
        {
            getAndroidInput();
            /*sideways = Input.GetAxis("Horizontal");
            forward = Input.GetAxis("Vertical");

            turn = Input.GetAxis("Horizontal2");
            turnUp = Input.GetAxis("Vertical2");

            shooting = Input.GetButton("Jump");
*/
        }
        public Rigidbody gun;
        public Transform tip;
        public Camera cam;
        public GameObject gunLookAt;
        public GameObject gunLookAtElse;
        public float timer = 0;
        public float shootDelay;
        public bool shooting = false;
        public RaycastHit hit;
        public RaycastHit shot;
        public LineRenderer line;
        public Transform gunnygun;
        void Aim()
        {
            gunnygun.transform.forward = this.transform.forward;
        }
        public DickControl dick;
        public float shotRad;
        public float shotForce;

        void Shoot()
        {
            
            if (shooting)
            {
                line.enabled = true;
                if(Physics.Raycast(tip.transform.position, tip.transform.forward, out shot))
                    {
                    line.SetPositions(new Vector3[] { tip.transform.position, shot.point });
                        //Debug.DrawRay(tip.transform.position, tip.transform.forward, Color.red);

                    if (shot.collider.name == "Dick")
                    {

                        spawning = true; // spawn more after first dick dies
                        spawnTimer = spawnInterval;
                        shot.collider.name = "dead"; // to kill the dicks that get shot
                        numDicks--;

                    }
                } else
                {
                    line.SetPositions(new Vector3[] { tip.transform.position, gunLookAt.transform.position });
                }
            }  else
            {
                line.enabled = false;
            }
        }
        public bool isDead;
        void checkDeath()
        {
            
            if (transform.position.y < -1)// falls
            {
                map.SetActive(false);
                if (transform.position.y < -10)
                {
                    isDead = true;
                    deathText.text = "So the world is flat? dang";
                }
            }

            if (health<=0)
            {
                isDead = true;
                deathText.text = "you died fool";
            }


        }
        public GameObject map;
        public float G;
        public float R;
        public float fadeTime;
        public Light lighting;
        void fade()
        {
            if (fadeTime>0)
            {
                cam.backgroundColor -= new Color(R, G, 0);
                fadeTime -= Time.deltaTime;
                lighting.intensity -= Time.deltaTime / fadeTime;

            } else
            {
                SceneManager.LoadScene("StartScene");
            }
            
        }
        void death()
        {
            GetComponent<Animator>().enabled = false;
            deathText.enabled = true;
            fade();
        }
        public RectTransform HBar;
        float health;
        void healthBar()
        {
            health = PlayerPrefs.GetFloat("Health");
            HBar.localScale = new Vector3(health, 1, 1);

        }
        public float spawnInterval;
         float spawnTimer;

        int dickNum = 1;
        public int numNewDicks;
        bool spawning = false;
        float multiplier = 0;
        public float multMax;
        public int numDicks;
        public int maxDicks;
        void SpawnNewEnimies()
        {
            if (spawning && numDicks <= maxDicks)
            {
                spawnTimer -= Time.deltaTime;
                if (spawnTimer < 0)
                {
                    spawnTimer = spawnInterval;
                    if (multiplier < multMax)
                    {
                        multiplier++;

                    }

                    for (int i = 0; i < numNewDicks*multiplier; i++)
                    {
                        dick = Instantiate(dick, spawnPoints[i%spawnPoints.Length].position, new Quaternion());

                        dick.isDead = false;
                        dick.name = "Dick";
                        dick.sighted = true;
                        dick.animator.SetBool("Patrolling", false);
                        dick.animator.applyRootMotion = true;
                        dick.deletable = true;
                        numDicks++;

                    }
                }               
            }
        }
    }
}