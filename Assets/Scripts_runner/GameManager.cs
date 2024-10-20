using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float gameSpeed { get; private set; }
    public float initspeed = 5f;
    public float speedinc = 0.1f;
    private int grounded = 1;
    private Player p;
    private Spawn s;
    private Alien a;
    private bool Alienseq = false;
    public  int sig = 0;
    private float lastCallTime = 0f;
    public TextMeshProUGUI scoretext;
    public TextMeshProUGUI controls;
    public TextMeshProUGUI endscore;
    public TextMeshProUGUI highscoretxt;
    public TextMeshProUGUI indicatorinfo;
    public Button start;
    public Button mainmenu;
    private float score = 0;
    private float highscore;
    public Button retry;
    private bool notpaused = true;
    public float currentTime;
    public bool allowinput;
    public GameObject newbackground;
    public bool poweredup =  false;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        notpaused = false;
        p = FindObjectOfType<Player>();
        s = FindObjectOfType<Spawn>();
        a = FindObjectOfType<Alien>();
        allowinput = false;
        gameSpeed = initspeed;
        endscore.gameObject.SetActive(false);
        highscoretxt.gameObject.SetActive(false);
        s.gameObject.SetActive(false);
        a.gameObject.SetActive(false);
        retry.gameObject.SetActive(false);
        mainmenu.gameObject.SetActive(false);
        controls.gameObject.SetActive(true);
        indicatorinfo.gameObject.SetActive(true);
        start.gameObject.SetActive(true);
    }


    public void NewGame()
    {

        controls.gameObject.SetActive(false);
        indicatorinfo.gameObject.SetActive(false);
        start.gameObject.SetActive(false);
        endscore.gameObject.SetActive(false);
        highscoretxt.gameObject.SetActive(false);
        allowinput = true;
        score = 0;

        p.resethp();
        currentTime = 0;
        lastCallTime = 0;
        
        gameSpeed = initspeed;
        enabled = true;
        p.gameObject.SetActive(true);
        s.gameObject.SetActive(true);
        a.gameObject.SetActive(false);
        Alienseq = false;
        p.forcefieldCooldown = 3f;
        retry.gameObject.SetActive(false);
        mainmenu.gameObject.SetActive(false);
        currentTime = 0;
        lastCallTime = Time.time;
        notpaused = true;
    }

    public void gotomenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


    public void GameOver()
    {
        notpaused = false;
        Obstacle[] obs = FindObjectsOfType<Obstacle>();
        Meteor[] met = FindObjectsOfType<Meteor>();
        Lava[] lavas = FindObjectsOfType<Lava>();

        foreach (var obstacle in obs)
        {
            Destroy(obstacle.gameObject);
        }
        foreach (var meteor in met)
        {
            Destroy(meteor.gameObject);
        }
        foreach (var lav in lavas)
        {
            Destroy(lav.gameObject);
        }
        gameSpeed = 0f;
        enabled = false;
        p.gameObject.SetActive(false);
        s.gameObject.SetActive(false);
        a.gameObject.SetActive(false);
        if (highscore == 0f || score > highscore)
        {
            highscore = score;
        }
        endscore.text = "Score: " + Mathf.FloorToInt(score).ToString();
        highscoretxt.text = "HighScore: " + Mathf.FloorToInt(highscore).ToString();
        endscore.gameObject.SetActive(true);
        highscoretxt.gameObject.SetActive(true);

        retry.gameObject.SetActive(true);
        mainmenu.gameObject.SetActive(true);


    }

    private void Update()
    {
        if (notpaused)
        {
            gameSpeed += speedinc * Time.deltaTime;
            score += gameSpeed * Time.deltaTime;
            scoretext.text = Mathf.FloorToInt(score).ToString("D5");
            currentTime = Time.time;
            if (currentTime - lastCallTime >= 15f)
            {
                startstopseq();
                lastCallTime = currentTime;
            }
        }
        
    }

    private void ground()
    {
        grounded = 1;
    }

    public void unground()
    {
        grounded = 0;
        Invoke(nameof(ground), 0.75f);
    }

    public bool getground()
    {
        if(grounded == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //public bool getseq()
    //{
    //    if(Alienseq == false)
    //    {
    //        return false;
    //    }
    //    else
    //    {
    //        return true;
    //    }
    //}
    public void truebegin()
    {
        Vector3 pos = newbackground.transform.position;
        pos.z -= 2f;
        newbackground.transform.position = pos;
        NewGame();
        //Invoke(nameof(NewGame), 1f);
    }

    private void startstopseq()
    {
        if (Alienseq ==  true)
        {
            Alienseq = false;
            s.gameObject.SetActive(true);
            a.gameObject.SetActive(false);
            p.forcefieldCooldown = 3f;
            sig = 1;
        }
        else
        {
            Alienseq = true;
            s.gameObject.SetActive(false);
            a.gameObject.SetActive(true);
            p.forcefieldCooldown = 0.85f;
            sig = 1;
        }
    }

}
