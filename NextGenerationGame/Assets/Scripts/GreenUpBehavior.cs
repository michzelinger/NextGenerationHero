using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;

public class GreenUpBehavior : MonoBehaviour 
{
   private bool eggReady;

   public int heroHealth;
   public int maxHealth = 3;
   public Text mGameControlText = null;
   private Rigidbody2D rb;
   public Slider eggSlider;
   public float arrowDamage = 100f;
   public Text mEnemyCountText = null;
   public Text mHeroDeathText = null;
   
   public float mPlanesTouched = 0;
   public float speed = 20f;
   public    float currentSpeed;
   public float forwardAcceleration = 0.0000000000000005f;
   public float backwardAcceleration = -0.0000000000000005f;
  // private float accelerationPerUpdate;
   public float maxSpeed = 100f;
   public float mHeroRotateSpeed = 90f / 2f;

   public float eggRate = 0.2f;
   
   public float bulletRate = 1f;
   public float bullet2Rate = 0.5f;
   private float eggTimeStamp;
   private float currentEggCD = 0.0f;
   private float bulletTimeStamp;
   private float bullet2TimeStamp;
   private bool outOfScreen = false;

   private GameController mGameGameController = null;
   public bool mFollowMousePosition = true;

   public bool mHeroHealthControl = false;
   public Text mHeroHealthText = null;
   // Start is called before the first frame update
   void Start()
   {
      heroHealth = maxHealth;
      currentSpeed = speed;
      //accelerationPerUpdate = acceleration / 50000;
      rb = GetComponent<Rigidbody2D>();
      rb.freezeRotation = true;
      rb.velocity = transform.up * speed;
      mGameGameController = FindObjectOfType <GameController>();
   }

   
   // Update is called once per frame
   /*void FixedUpdate()
   {
      accelerationPerUpdate += acceleration / 50000;
      Debug.Log("accel" + accelerationPerUpdate);
   }*/
   void Update()
   {
     // currentSpeed += accelerationPerUpdate;
      if (Input.GetKeyDown(KeyCode.M))
      {
         mFollowMousePosition = !mFollowMousePosition;
         UpdateGameControl();
      }

      if(Input.GetKeyDown(KeyCode.G))
      {
         mHeroHealthControl = !mHeroHealthControl;
         if(mHeroHealthControl)
         { 
            mHeroDeathText.text = "Hero Death: Enabled";
            heroHealth = 3;
            mHeroHealthText.text = "Hero Health: " + heroHealth;
         }
         else 
         {
             mHeroDeathText.text = "Hero Death: Disabled";
             mHeroHealthText.text = "";
         }
      }

      Vector3 pos = transform.position;

      if (mFollowMousePosition)
      {
         pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         //Debug.Log("Position is " + pos);
         pos.z = 0f;
      }

      else
      {
         /*if (Input.GetKey(KeyCode.W))
         {
            /*backwardAcceleration = -0.0000000000000005f;
            if(currentSpeed < maxSpeed)
            {
               currentSpeed += forwardAcceleration / 50;
               forwardAcceleration += forwardAcceleration;
               rb.velocity = new Vector2(currentSpeed * transform.up.x, currentSpeed * transform.up.y);
            }
            else
            {
               currentSpeed = maxSpeed;
            }
            Debug.Log("velocity: " + rb.velocity);
            Debug.Log("speed " + currentSpeed);
            pos += ((currentSpeed * Time.smoothDeltaTime) * transform.up);
         }
         if (Input.GetKey(KeyCode.S))
         {
            forwardAcceleration = 0.0000000000000005f;
            if(currentSpeed > (maxSpeed * -1))
            {
               backwardAcceleration += backwardAcceleration / 50;
               currentSpeed -= backwardAcceleration;
               rb.velocity = new Vector2(currentSpeed * -transform.up.x, currentSpeed * -transform.up.y);
            }
            else
            {
               currentSpeed = -100;
            }
            
            
            pos -= ((currentSpeed * Time.smoothDeltaTime) * transform.up);
         }*/
         if(Input.GetKey(KeyCode.W))
         {
            currentSpeed += 0.5f;
         } 
         if(Input.GetKey(KeyCode.S))
         {
            currentSpeed -= 0.5f;
         }
         
        /* if (Input.GetKey(KeyCode.W))
         {
            if(speed < maxSpeed)
            {
               speed += Time.deltaTime * acceleration;
            }
            else if(speed > maxSpeed)
            {
               speed = 200f;
            }
            pos += ((speed * Time.smoothDeltaTime) * transform.up);
         }

         if (Input.GetKey(KeyCode.S))
         {
            pos -= ((speed * Time.smoothDeltaTime) * transform.up);
         }*/
         

         if (Input.GetKey(KeyCode.D))
         {
            //rb.velocity = new Vector2(currentSpeed * transform.up.x, currentSpeed * transform.up.y);
            Debug.Log("velocity: " + rb.velocity);
            transform.Rotate(transform.forward, -mHeroRotateSpeed * Time.smoothDeltaTime);
         }

         if (Input.GetKey(KeyCode.A))
         {
            //rb.velocity = new Vector2(currentSpeed * transform.up.x, currentSpeed * transform.up.y);
            transform.Rotate(transform.forward, mHeroRotateSpeed * Time.smoothDeltaTime);
         }
         rb.velocity = transform.up * currentSpeed;
      }
      transform.position = pos;
      if(currentEggCD >= eggRate)
      {
         eggReady = true;
      }
      else
      {
         eggReady = false;
         currentEggCD += Time.deltaTime;
         currentEggCD = Mathf.Clamp(currentEggCD, 0.0f, eggRate);
      }
      eggSlider.value = currentEggCD / eggRate;
      if(eggSlider.value >= 1.0f)
      {
         eggSlider.gameObject.SetActive(false);
      }
      else
      {
         eggSlider.gameObject.SetActive(true);
      }

      if (eggReady && outOfScreen == false && Input.GetKey(KeyCode.Space))
      {
          GameObject e = Instantiate(Resources.Load("Prefabs/Egg") as GameObject);
          e.transform.localPosition = transform.localPosition;
          e.transform.rotation = transform.rotation;
          Debug.Log("Spawn Eggs:" + e.transform.localPosition);
          mGameGameController.IncreaseNumEggs();
          currentEggCD = 0.0f;
          
      }

      


      if(outOfScreen == false && Time.time >= bulletTimeStamp && Input.GetKey(KeyCode.Mouse0))
      {
         GameObject e = Instantiate(Resources.Load("Prefabs/Bullet") as GameObject);
          e.transform.localPosition = transform.localPosition;
          e.transform.rotation = transform.rotation;
          Debug.Log("Spawn Bullets:" + e.transform.localPosition);
          bulletTimeStamp = Time.time + bulletRate;
      }

      if(outOfScreen == false && Time.time >= bullet2TimeStamp && Input.GetKey(KeyCode.LeftControl))
      {
          GameObject e = Instantiate(Resources.Load("Prefabs/Bullet2") as GameObject);
          e.transform.localPosition = transform.localPosition;
          e.transform.rotation = transform.rotation;
          Debug.Log("Spawn Bullets:" + e.transform.localPosition);
          bullet2TimeStamp = Time.time + bullet2Rate;
      }
      
   }
  /*private void OnTriggerEnter2D(Collider2D collision)
   {
      Debug.Log("Here x Plane: OnTriggerEnter2D");
      mPlanesTouched++;
      mEnemyCountText.text = "Planes touched " + mPlanesTouched;
      Destroy(collision.gameObject);
      mGameGameController.EnemyDestroyed();
   }*/

   private void OnCollisionEnter2D(Collision2D collision)
   {
      GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
      if(enemy.tag == "Enemy")
      {
         EnemyBehavior enemyKill = collision.gameObject.GetComponent<EnemyBehavior>();
         Debug.Log("Here x Plane: OnCollisionEnter2D");
         mPlanesTouched++;
         mEnemyCountText.text = "Planes touched " + mPlanesTouched;
         enemyKill.TakeDamage(arrowDamage);
         //mGameGameController.EnemyDestroyed();
         if(mHeroHealthControl == true)
         {
            Debug.Log("Hero Health" + heroHealth);
            if(heroHealth > 1)
            {
               heroHealth -= 1;
               mHeroHealthText.text = "Hero Health: " + heroHealth;
               Debug.Log("Hero Health" + heroHealth);
            }
            else
            {
               heroHealth -= 1;
               mHeroHealthText.text = "Hero Health: " + heroHealth;
               Debug.Log("Hero Health" + heroHealth);
               Destroy(gameObject);
            }
         }
      }
      
   }

   void OnBecameInvisible()
   {
      outOfScreen = true;
      Debug.Log("Out of screen " + outOfScreen);
     
   }

   void OnBecameVisible()
   {
      outOfScreen = false;
      Debug.Log("Out of screen " + outOfScreen);
     
   }
   private void OnCollisionStay2D(Collision2D collision)
   {
      Debug.Log("Plane On CollisionStay");
   }

   private void UpdateGameControl()
   {
      if(mFollowMousePosition == true)
      {
         mGameControlText.text = "Hero Control Mode: Follow Mouse Position";
      }
      else
      {
         mGameControlText.text = "Hero Control Mode: Keyboard Mode";
      }
   }
   
   

}


