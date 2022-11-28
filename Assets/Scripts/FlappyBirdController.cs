using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBirdController : MonoBehaviour
{
    #region Singleton
    public static FlappyBirdController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }
    #endregion
    #region Player Components
    public Rigidbody rb;
    public BoxCollider col;
    public Animator animator;
    public float power;
    public float maxPower;
    public float jumpDelayTime = .5f;
    float invincibleTimer;
    public Vector3 collisionPoint;
    #endregion

    private void Start()
    {
        col = GetComponent<BoxCollider>();
        collisionPoint = transform.position;
    }

    void Update()
    {
        if (GameManager.instance.isGameRunning)
        {
            GoForward();
            LimitJumpPower();
        }
    }

    public void LimitJumpPower()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity -= new Vector3(0, GameManager.instance.gameplaySpeed/100, 0);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxPower+2);
        }
        else
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxPower);
    }

    public void GoForward()
    {
        transform.Translate(transform.forward * Time.deltaTime * GameManager.instance. gameplaySpeed);
    }

    bool canJump=true;
    public void GoUp()
    {
        if (GameManager.instance.isGameRunning &&canJump)
            StartCoroutine(Jump());
    }

    IEnumerator Jump()
    {
        canJump = false;
        float timer = 0;
        while (timer < jumpDelayTime)
        {
            rb.velocity += new Vector3(0, power, 0);
            timer += Time.deltaTime;
            yield return null;
        }
        canJump = true;
    }

    public IEnumerator BecomeInvisible(float timer)
    {
        transform.position = collisionPoint;
        col.enabled = false;
        invincibleTimer += timer;
        while (invincibleTimer > 0)
        {
            invincibleTimer -= Time.deltaTime;
            yield return null;
        }
        col.enabled = true;
    }

    void AskForRetry()
    {
        collisionPoint.z = transform.position.z;
        col.enabled=false;
        rb.isKinematic = true;
        GameManager.instance.isGameRunning = false;
        GameManager.instance.retryPanel.SetActive(true);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground"|| other.transform.tag == "Hurdle") 
        {
            AskForRetry();
            return;
        }
        if(other.tag == "LevelSpawner")
        {
            LevelManager.instance.SpawnLevel();
            return;
        }
    }   
    void OnCollisionEnter(Collision other)
    {
        if ((other.transform.tag == "Ground" || other.transform.tag=="Hurdle"))
        {
            AskForRetry();
        }
    }
}