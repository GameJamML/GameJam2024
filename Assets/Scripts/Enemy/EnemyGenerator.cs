using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] enemiesType;
    public Unity.AI.Navigation.NavMeshSurface naveMeshLayer;
    List<GameObject> pullEnemies = new List<GameObject>();
    List<GameObject> pullEnemiesDone = new List<GameObject>();
    private float lastGenerated = 0;
    [SerializeField] private float generationrate;
    [SerializeField] private int numberOfEachEnemy;
    void Start()
    {
        CreatePool();
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
        if (CheckInNaveMesh(randomPosition) == true && pullEnemies[enemySelected].activeSelf == false)
        {
            pullEnemies[enemySelected].transform.position = randomPosition;
            pullEnemies[enemySelected].SetActive(true);
            SwapListDone(enemySelected);

        }
    }

    void SwapListDone(int enemySelected)
    {
        pullEnemiesDone.Add(pullEnemies[enemySelected]);
        pullEnemies.Remove(pullEnemies[enemySelected]);
    }
    int enemyRandomizer(int maxrange)
    {
        int randomEnemy;
        randomEnemy = Random.Range(0, maxrange);
        return randomEnemy;
    }

   bool CheckInNaveMesh(Vector3 randPosition)
   {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randPosition, out hit, 1.0f, NavMesh.GetAreaFromName("NavMesh_Terrain")))
        {
            return true;
        }
       return false;
   }

    Vector3 setRandomPosition(float posX, float posZ)
    {
        posX = Random.Range(gameObject.transform.position.x - 10f, gameObject.transform.position.x + 10f);
        posZ = Random.Range(gameObject.transform.position.z - 10f, gameObject.transform.position.z + 10f);
        Vector3 randomPos = new Vector3(posX, 0.6f, posZ);
        return randomPos;
    }

    void CreatePool()
    {
        for (int i = 0; i < numberOfEachEnemy; i++)
        {
            GameObject newEnemy = Instantiate(enemiesType[0], new Vector3(0, 100, 0), Quaternion.identity, transform);
            newEnemy.SetActive(false);
            pullEnemies.Add(newEnemy);
        }
        for (int i = 0; i < numberOfEachEnemy; i++)
        {
            GameObject newEnemy = Instantiate(enemiesType[1], new Vector3(0, 100, 0), Quaternion.identity, transform);
            newEnemy.SetActive(false);
            pullEnemies.Add(newEnemy);
        }
        for (int i = 0; i < numberOfEachEnemy; i++)
        {
            GameObject newEnemy = Instantiate(enemiesType[2], new Vector3(0, 100, 0), Quaternion.identity, transform);
            newEnemy.SetActive(false);
            pullEnemies.Add(newEnemy);
        }
    }


}
