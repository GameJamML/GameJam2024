using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] enemiesType;

    public Unity.AI.Navigation.NavMeshSurface naveMeshLayer;

    [HideInInspector] public List<GameObject> pullEnemies = new List<GameObject>();
    [HideInInspector] public bool stop;
    List<GameObject> pullEnemiesDone = new List<GameObject>();

    private float lastGenerated = 0;

    [SerializeField] private float generationrate;
    [SerializeField] private float SpawnRangeX;
    [SerializeField] private float SpawnRangeZ;
    [SerializeField] private int numberOfEachEnemy;

    private bool _sleep = false;

    void Start()
    {
        CreatePool();
        stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_sleep)
            return;

        lastGenerated += Time.deltaTime;

        if (lastGenerated >= generationrate && pullEnemies.Count != 0)
        {
            Spawner();
            lastGenerated = 0f;
        }
    }

    void Spawner()
    {
        float posX = 0f;
        float posZ = 0f;
        int enemySelected = EnemyRandomizer(pullEnemies.Count);
        Vector3 randomPosition = setRandomPosition(posX, posZ);
        if (CheckInNaveMesh(randomPosition) == true && pullEnemies[enemySelected].activeSelf == false && stop == false)
        {
            pullEnemies[enemySelected].transform.position = randomPosition;
            pullEnemies[enemySelected].SetActive(true);
            SwapListCreatedEnemy(enemySelected);
        }
    }

    private void SwapListCreatedEnemy(int enemySelected)
    {
        pullEnemiesDone.Add(pullEnemies[enemySelected]);
        pullEnemies.Remove(pullEnemies[enemySelected]);
    }

    private void SwapListKilledEnemy(int enemySelected)
    {
        pullEnemies.Add(pullEnemies[enemySelected]);
        pullEnemiesDone.Remove(pullEnemies[enemySelected]);
    }

    private int EnemyRandomizer(int maxrange)
    {
        int randomEnemy;
        randomEnemy = Random.Range(0, maxrange);
        return randomEnemy;
    }

    private bool CheckInNaveMesh(Vector3 randPosition)
    {
         NavMeshHit hit;
         if (NavMesh.SamplePosition(randPosition, out hit, 0.6f, NavMesh.GetAreaFromName("NavMesh_Terrain")))
         {
             return true;
         }
        return false;
    }

    private Vector3 setRandomPosition(float posX, float posZ)
    {
        posX = Random.Range(gameObject.transform.position.x - SpawnRangeX, gameObject.transform.position.x + SpawnRangeX);
        posZ = Random.Range(gameObject.transform.position.z - SpawnRangeZ, gameObject.transform.position.z + SpawnRangeZ);
        Vector3 randomPos = new Vector3(posX, 0f, posZ);
        return randomPos;
    }

    private void CreatePool()
    {
        for (int i = 0; i < enemiesType.Length; i++)
        {
            for (int j = 0; j < numberOfEachEnemy; j++)
            {
                GameObject newEnemy = Instantiate(enemiesType[i], Vector3.zero, Quaternion.identity, transform);
                newEnemy.SetActive(false);
                pullEnemies.Add(newEnemy);
            }
        }
    }

    public void destroyEnemy(bool isDead, int enemy)
    {
        pullEnemiesDone[enemy].transform.position = Vector3.zero;
        pullEnemiesDone[enemy].SetActive(false);
        SwapListKilledEnemy(enemy);
    }

    public void SleepAllEnemies()
    {
        gameObject.BroadcastMessage("SleepEnemy");
        _sleep = true;
    }

    public void AwakeAllEnemies()
    {
        gameObject.BroadcastMessage("AwakeEnemy");
        _sleep = false;
    }
}
