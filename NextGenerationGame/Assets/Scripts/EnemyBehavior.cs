using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{


    public GameObject[] waypoints;
    int current = 0;
    public float speed;
    private float WPradius = 0.2f;
    private GameController mGameGameController = null;
    public float enemyHealth = 100f;
    // Start is called before the first frame update
    void Start()
    {

        waypoints = new GameObject[6];
        mGameGameController = FindObjectOfType <GameController>();
        gameObject.tag = "Enemy";
    }

    // Update is called once per frame
    void Update()
    {
         waypoints[0] = GameObject.FindGameObjectWithTag("Letter");
         waypoints[1] = GameObject.FindGameObjectWithTag("LetterB");
         waypoints[2] = GameObject.FindGameObjectWithTag("LetterC");
         waypoints[3] = GameObject.FindGameObjectWithTag("LetterD");
         waypoints[4] = GameObject.FindGameObjectWithTag("LetterE");
         waypoints[5] = GameObject.FindGameObjectWithTag("LetterF");
        if (mGameGameController.switchWaypointMode == false && Vector2.Distance(waypoints[current].transform.position, transform.position) < WPradius)
        {
            
            current++;
            if (current >= waypoints.Length)
            {
                current = 0;
            }

        }
        else if(mGameGameController.switchWaypointMode == true && Vector2.Distance(waypoints[current].transform.position, transform.position) < WPradius)
        {
            int r = Random.Range(0,6);
            Debug.Log("number random" + r);
            while(current == r)
            {
                r = Random.Range(0,6);
            }
            Debug.Log("current " + current);
            current = r;
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
        RotateTowardsTarget();
    }
    private void RotateTowardsTarget()
{
    float rotationSpeed = 10f; 
    float offset = 270f;    
    Vector3 direction = waypoints[current].transform.position - transform.position;
    direction.Normalize();
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    Quaternion rotation = Quaternion.AngleAxis(angle + offset, Vector3.forward);
    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
}
    private void UpdateColor(float amount)
    {
        float decreaseAmount = amount / 100f;
        SpriteRenderer s = GetComponent<SpriteRenderer>();
        
        Color c = s.color;
        float delta = decreaseAmount;
        c.r -= delta;
        c.a -= delta;
        s.color = c;
        Debug.Log("Plane: Color = " + c);

        if (c.a <= 0.0f)
        {
            Sprite t = Resources.Load<Sprite>("Textures/Egg");   // File name with respect to "Resources/" folder
            s.sprite = t;
            s.color = Color.white;
        }
    }
    
     public void TakeDamage(float amount)
     {
         
         enemyHealth -= amount;
         Debug.Log("Enemy Health: " + enemyHealth);
         
         if (enemyHealth <= 0)
         {
             Die();
         }
         else
         {
            UpdateColor(amount);
         }
     }
     void Die()
     {
         Destroy(gameObject);
         mGameGameController.EnemyDestroyed();
     }
     
}
