using UnityEngine;

public class Spawn : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnObject
    {
        public GameObject prefab;
        [Range(0f, 1f)] public float spawnchance;

    }

    private int spawncount;

    public SpawnObject[] objects;

    public float minspawn = 0.5f;
    public float maxspawn = 0.75f;

    private void Spawner()
    {
        if (spawncount % 5 == 0 && spawncount != 0)
        {
            GameObject heart = Instantiate(objects[3].prefab);
            heart.transform.position += transform.position;
            spawncount++;
        }
        else
        {
            int randomIndex = Random.Range(0, 3);
            GameObject obstacle = Instantiate(objects[randomIndex].prefab);
            obstacle.transform.position += transform.position;
            spawncount++;
        }
        //float spawnchance = Random.value;
        //float totalSpawnchance = 0.0f;

        //for (int i = 0; i < objects.Length; i++)
        //{
        //    totalSpawnchance += objects[i].spawnchance;
        //    if (spawnchance < totalSpawnchance)
        //    {
        //        GameObject obstacle = Instantiate(objects[i].prefab);
        //        obstacle.transform.position += transform.position;
        //        break;
        //    }
        //}

        Invoke(nameof(Spawner), Random.Range(minspawn, maxspawn));
    }

    
    private void OnEnable()
    {
        spawncount = 0;
        Invoke(nameof(Spawner), Random.Range(minspawn, maxspawn));
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    
}
