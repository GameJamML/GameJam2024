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
    [SerializeField] private int numberOfEachEnemy;

    private Vector3 startEnemies = new Vector3(0, 100, 0);
    void Start()
    {
        CreatePool();
        stop = false;
    }

    // Update is called once per frame
    void Update()
    {
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
        int enemySelected = enemyRandomizer(pullEnemies.Count);
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
    private int enemyRandomizer(int maxrange)
    {
        int randomEnemy;
        randomEnemy = Random.Range(0, maxrange);
        return randomEnemy;
    }

    private bool CheckInNaveMesh(Vector3 randPosition)
    {
         NavMeshHit hit;
         if (NavMesh.SamplePosition(randPosition, out hit, 4.0f, NavMesh.GetAreaFromName("NavMesh_Terrain")))
         {
             return true;
         }
        return false;
    }

    private Vector3 setRandomPosition(float posX, float posZ)
    {
        posX = Random.Range(gameObject.transform.position.x - 10f, gameObject.transform.position.x + 10f);
        posZ = Random.Range(gameObject.transform.position.z - 10f, gameObject.transform.position.z + 10f);
        Vector3 randomPos = new Vector3(posX, 3.79f, posZ);
        return randomPos;
    }

    private void CreatePool()
    {
        for (int i = 0; i < enemiesType.Length; i++)
        {
            for (int j = 0; j < numberOfEachEnemy; j++)
            {
                GameObject newEnemy = Instantiate(enemiesType[i], startEnemies, Quaternion.identity, transform);
                newEnemy.SetActive(false);
                pullEnemies.Add(newEnemy);
            }

        }
      
    }

    public void destroyEnemy(bool isDead, int enemy)
    {
        pullEnemiesDone[enemy].transform.position = startEnemies;
        pullEnemiesDone[enemy].SetActive(false);
        SwapListKilledEnemy(enemy);
    }
}
