using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawn : MonoBehaviour
{
    public EnemySpawn enemySpawn;
    public GameObject key;
  
    void Start()
    {
       
        
    }

    void Update()
    {
            if (enemySpawn.totalenemies == 12)
            {
                Instantiate(key, transform.position, Quaternion.identity);
                ShooterGameManager.Instance.Boss_State();
                gameObject.SetActive(false);
            }
        
        
    }
}
