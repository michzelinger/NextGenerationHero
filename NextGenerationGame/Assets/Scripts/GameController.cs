using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
public class GameController : MonoBehaviour
{
    public bool switchWaypointMode = false;
    private int maxPlanes = 10;
    private int maxLetter = 1;
    public int numA = 0;
    public int numB = 0;
    public int numC = 0;
    public int numD = 0;
    public int numE = 0;
    public int numF = 0;
    public int numberOfPlanes = 0;
    private int numberOfEnemiesKilled = 0;

    public Text mEggsCountText = null;
    public Text mEnemiesInTheWorld = null;
    public Text mWaypointMode = null;
    public Text mEnemiesDestroyed = null;
    public int numEggsOnScreen = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            switchWaypointMode = !switchWaypointMode;
            updateWaypointMode();
        }
        
        if(Input.GetKey(KeyCode.Q)) {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        if(numberOfPlanes < maxPlanes)
        {

            CameraSupport s = Camera.main.GetComponent<CameraSupport>();
            
            GameObject e = Instantiate(Resources.Load("Prefabs/Enemy") as GameObject);
            Vector3 pos;
            pos.x = s.GetWorldBound().min.x + Random.value * s.GetWorldBound().size.x;
            pos.y = s.GetWorldBound().min.y + Random.value * s.GetWorldBound().size.y;
            pos.z = 0;
            e.transform.localPosition = pos;
            numberOfPlanes++;
            mEnemiesInTheWorld.text = "Number of Enemies " + numberOfPlanes;
        }
        if(numA < maxLetter)
        {
            CameraSupport s = Camera.main.GetComponent<CameraSupport>();

            GameObject e = Instantiate(Resources.Load("Prefabs/A_Walk") as GameObject);
            var position = new Vector3 (Random.Range(-60.0f, -75.0f), Random.Range(50.0f, 65.0f), 1);
            e.transform.localPosition = position;
            numA++;
        }
        if(numB < maxLetter)
        {
            CameraSupport s = Camera.main.GetComponent<CameraSupport>();

            GameObject e = Instantiate(Resources.Load("Prefabs/B_Walk") as GameObject);
            var position = new Vector3 (Random.Range(80.0f, 95.0f), Random.Range(-70.0f, -85.0f), 1);
            e.transform.localPosition = position;
            numB++;
        }
        if(numC < maxLetter)
        {
            CameraSupport s = Camera.main.GetComponent<CameraSupport>();

            GameObject e = Instantiate(Resources.Load("Prefabs/C_Walk") as GameObject);
            var position = new Vector3 (Random.Range(135.0f, 150.0f), Random.Range(-5.0f, -20.0f), 1);
            e.transform.localPosition = position;
            numC++;
        }
        if(numD < maxLetter)
        {
            CameraSupport s = Camera.main.GetComponent<CameraSupport>();

            GameObject e = Instantiate(Resources.Load("Prefabs/D_Walk") as GameObject);
            var position = new Vector3 (Random.Range(-50.0f, -65.0f), Random.Range(-45.0f, -60.0f), 1);
            e.transform.localPosition = position;
            numD++;
        }
        if(numE < maxLetter)
        {
            CameraSupport s = Camera.main.GetComponent<CameraSupport>();

            GameObject e = Instantiate(Resources.Load("Prefabs/E_Walk") as GameObject);
            var position = new Vector3 (Random.Range(90.0f, 105.0f), Random.Range(60.0f, 75.0f), 1);
            e.transform.localPosition = position;
            numE++;
        }
        if(numF < maxLetter)
        {
            CameraSupport s = Camera.main.GetComponent<CameraSupport>();

            GameObject e = Instantiate(Resources.Load("Prefabs/F_Walk") as GameObject);
            var position = new Vector3 (Random.Range(30.0f, 45.0f), Random.Range(1.0f, -16.0f), 1);
            e.transform.localPosition = position;
            numF++;
        }
    }
    
    public void updateWaypointMode()
    {
        if(switchWaypointMode == true)
        {
            mWaypointMode.text = "Waypoint Mode: Random";
        }
        else
        {
            mWaypointMode.text = "Waypoint Mode: Sequential";
        }
    }
    public void IncreaseNumEggs()
    {
        numEggsOnScreen++;
        mEggsCountText.text = "Eggs in world " + numEggsOnScreen;
    }

    public void EggDestroyed()
    {
        numEggsOnScreen--;
        mEggsCountText.text = "Eggs in world " + numEggsOnScreen;
    }
    public void EnemyDestroyed()
    {
        numberOfPlanes--;
        mEnemiesInTheWorld.text = "Number of Enemies " + numberOfPlanes;
        numberOfEnemiesKilled++;
        mEnemiesDestroyed.text = "Number of Enemies Killed " + numberOfEnemiesKilled;
    }

    public void LetterDestroyed(string tag)
    {
        Debug.Log(tag);
        if(tag == "Letter")
            numA--;
        if(tag == "LetterB")
            numB--;
        if(tag == "LetterC")
            numC--;
        if(tag == "LetterD")
            numD--;
        if(tag == "LetterE")
            numE--;
        if(tag == "LetterF")
            numF--;
    }


}
