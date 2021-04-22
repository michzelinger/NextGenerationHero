using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float bulletDamage = 50f;
    public float kBulletSpeed = 50f;
    
    // Start is called before the first frame update
    void OnBecameInvisible()
    {
        Destroy(transform.gameObject);
    }
    void Start()
    {   
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * (kBulletSpeed * Time.smoothDeltaTime);
    }
    
     private void OnTriggerEnter2D(Collider2D collision)
   {
      
      if(collision.gameObject.tag == "Enemy")
      {
            EnemyBehavior enemy = collision.gameObject.GetComponent<EnemyBehavior>();
            Debug.Log("Here x Plane: OnTriggerEnter2D");
            enemy.TakeDamage(bulletDamage);
       //  mPlanesTouched++;
     //    mEnemyCountText.text = "Planes touched " + mPlanesTouched;
           // enemyKill.TakeDamage(eggDamage);
            Destroy(gameObject);
      }
      if(collision.gameObject.tag == "Letter" || collision.gameObject.tag == "LetterB" || collision.gameObject.tag == "LetterC" ||
        collision.gameObject.tag == "LetterD" || collision.gameObject.tag == "LetterE" || collision.gameObject.tag == "LetterF")
      {
          LettersScript letter = collision.gameObject.GetComponent<LettersScript>();
          letter.TakeDamage(bulletDamage);

          Destroy(gameObject);
      }
   }
}
