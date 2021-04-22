using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EggBehavior : MonoBehaviour
{
    public float eggDamage = 25f;
    public float kEggSpeed = 40f;    

    private GameController mGameGameController = null;
    // Start is called before the first frame update
    void OnBecameInvisible()
    {
        Destroy(gameObject);
        mGameGameController.EggDestroyed();
    }
    void Start()
    {   
        mGameGameController = FindObjectOfType <GameController>();
        //mLifeCount = kLifeTime;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * (kEggSpeed * Time.smoothDeltaTime);
    }
    
     private void OnTriggerEnter2D(Collider2D collision)
   {
      
      if(collision.gameObject.tag == "Enemy")
      {
            EnemyBehavior enemy = collision.gameObject.GetComponent<EnemyBehavior>();
            Debug.Log("Here x Plane: OnTriggerEnter2D");
            enemy.TakeDamage(eggDamage);
       //  mPlanesTouched++;
     //    mEnemyCountText.text = "Planes touched " + mPlanesTouched;
           // enemyKill.TakeDamage(eggDamage);
            Destroy(gameObject);
      }
      if(collision.gameObject.tag == "Letter" || collision.gameObject.tag == "LetterB" || collision.gameObject.tag == "LetterC" ||
        collision.gameObject.tag == "LetterD" || collision.gameObject.tag == "LetterE" || collision.gameObject.tag == "LetterF")
      {
          LettersScript letter = collision.gameObject.GetComponent<LettersScript>();
          letter.TakeDamage(eggDamage);

          Destroy(gameObject);
      }
   }
    

}
