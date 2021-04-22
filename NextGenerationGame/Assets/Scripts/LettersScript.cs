using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;

public class LettersScript : MonoBehaviour
{
    public bool mHideLetters = false;
    private GameController mGameGameController = null;
    public float letterHealth = 100f;
    // Start is called before the first frame update
    void Start()
    {
        mGameGameController = FindObjectOfType <GameController>();
        //gameObject.tag = "Letter";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            mHideLetters = !mHideLetters;
            HideLetters();
        }
    }
    
    public void HideLetters()
    {
        if(mHideLetters == true)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
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
        Debug.Log("Letter: Color = " + c);

        if (c.a <= 0.0f)
        {
            Sprite t = Resources.Load<Sprite>("Textures/Egg");   // File name with respect to "Resources/" folder
            s.sprite = t;
            s.color = Color.white;
        }
    }
    
     public void TakeDamage(float amount)
     {
         
         letterHealth -= amount;
         Debug.Log("Letter Health: " + letterHealth);
         
         if (letterHealth <= 0)
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
         mGameGameController.LetterDestroyed(gameObject.tag);
         Destroy(gameObject);
     }
}
