
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ShooterGameManager : MonoBehaviour
{

    public static ShooterGameManager Instance { get; private set; }
    public bool bossState;
    public bool bossFight;
    public bool allowinput;
    public EnemySpawn enemySpawn;
    public KeySpawn keySpawn;
    private AudioSource audio;
    private Player_shooter player;
    public TextMeshProUGUI objective, wintext;
    public Button tryagain, mainmenu;
    public Boss boss;
    private bool gamestarted;



    private void Awake()
    {
        if (Instance == null)
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

    IEnumerator startgame()
    {
        yield return new WaitForSeconds(8f);
        gamestarted = true;
        enemySpawn.gameObject.SetActive(true);
        keySpawn.gameObject.SetActive(true);
        audio.enabled = true;
    }
   

    void Start()
    {
        audio = GetComponent<AudioSource>();
        tryagain.gameObject.SetActive(false);
        wintext.gameObject.SetActive(false);
        mainmenu.gameObject.SetActive(false);
        player = FindObjectOfType<Player_shooter>();
        StartCoroutine(startgame());
    }

    private void FixedUpdate()
    {
        if (!gamestarted)
        {
            objective.text = "\n\n\nA,D to move\nSPACE to jump\nE to deflect\nLEFT CLICK to shoot";
            objective.fontSize = 30;
        }
        else if (enemySpawn.totalenemies < 12)
        {
            objective.fontSize = 15;
            objective.text = "Aliens killed " + enemySpawn.totalenemies + "/12";
        }
        else if (!player.haskey && !bossState)
        {
            objective.text = "Collect key";
        }
        else if (player.haskey && !bossState)
        {
            objective.text = "Open door";
        }
        else if (!bossFight)
        {
            audio.volume = 0.015f;
            objective.text = "Rise";
        }
        else
        {
            objective.text = "Defeat Boss\nBoss HP: " + boss.HP;
            audio.volume = 0.35f;
            objective.fontSize = 22;
            objective.color = Color.red;
        }
    }

    public void Gameover()
    {
        enemySpawn.enabled = false;
        player.enabled = false;
        player.GetComponent<SpriteRenderer>().enabled = false;
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Laser[] lasers = FindObjectsOfType<Laser>();
        Homingprojectile[] homingprojectiles = FindObjectsOfType<Homingprojectile>();
        foreach(var enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
        foreach(var laser in lasers)
        {
            Destroy(laser.gameObject);
        }
        foreach (var homings in homingprojectiles)
        {
            Destroy(homings.gameObject);
        }
        mainmenu.gameObject.SetActive(true);
        tryagain.gameObject.SetActive(true);


    }

    public void NewGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void win()
    {
        player.enabled = false;
        wintext.gameObject.SetActive(true);
        mainmenu.gameObject.SetActive(true);

    }

    public void gotomenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Boss_State()
    {
        
        enemySpawn.enabled = false;
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Laser[] lasers = FindObjectsOfType<Laser>();
        Homingprojectile[] homingprojectiles = FindObjectsOfType<Homingprojectile>();
        foreach (var enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
        foreach (var laser in lasers)
        {
            Destroy(laser.gameObject);
        }
        foreach (var homings in homingprojectiles)
        {
            Destroy(homings.gameObject);
        }


    }

    
   
}
