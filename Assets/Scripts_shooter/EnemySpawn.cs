
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] spawnpoints;
    public int NumberEnemies;
    public int totalenemies;
    public GameObject enemy;
    private float lastspawntime;
    private float spawnrate = 2f;
    private float currenttime;
    private int lastindex;
    // Start is called before the first frame update
    void Start()
    {
        lastspawntime = 0f;
    }

    private void OnEnable()
    {
        lastspawntime = Time.time;
        totalenemies = 0;
        NumberEnemies = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currenttime = Time.time;
        int randomIndex = Random.Range(0, 4);
        if (NumberEnemies < 5)
        {
            if (currenttime - lastspawntime > spawnrate && randomIndex != lastindex)
            {
                lastindex = randomIndex;
                Vector3 pos = spawnpoints[randomIndex].transform.position;
                Instantiate(enemy, pos, Quaternion.identity);
                NumberEnemies++;
                lastspawntime = Time.time;
            }
        }

    }

   
}
