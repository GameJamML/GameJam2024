using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] enemiesType;

    public Unity.AI.Navigation.NavMeshSurface naveMeshLayer;

    [HideInInspector] public List<GameObject> pullEnemies = new();
    [HideInInspector] public bool stop;
    List<GameObject> pullEnemiesDone = new();

    private float lastGenerated = 0;

    [SerializeField] private float generationrate;
    [SerializeField] private float SpawnRangeX;
    [SerializeField] private float SpawnRangeZ;
    [SerializeField] private int numberOfEachEnemy;

    private bool _sleep = false;

    [HideInInspector] public Action awakeEvent;
    [HideInInspector] public Action sleepEvent;
    int rateBytime;
    void Start()
    {
        CreatePool();
        stop = false;
    }

    private void OnEnable()
    {
        Timer.MinutePassed += SpawnerRate;
    }

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
        int enemySelected = EnemyRandomizer(pullEnemies.Count);
        Vector3 randomPosition = SetRandomPosition();
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

    private int EnemyRandomizer(int maxrange)
    {
        int randomEnemy;
        randomEnemy = UnityEngine.Random.Range(0, maxrange);
        return randomEnemy;
    }

    private bool CheckInNaveMesh(Vector3 randPosition)
    {
        return NavMesh.SamplePosition(randPosition, out _, 0.6f, NavMesh.GetAreaFromName("NavMesh_Terrain"));
    }

    private Vector3 SetRandomPosition()
    {
        float posX = UnityEngine.Random.Range(gameObject.transform.position.x - SpawnRangeX, gameObject.transform.position.x + SpawnRangeX);
        float posZ = UnityEngine.Random.Range(gameObject.transform.position.z - SpawnRangeZ, gameObject.transform.position.z + SpawnRangeZ);
        Vector3 randomPos = new (posX, 0f, posZ);
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

    public void SleepAllEnemies(bool fromAnotherGenrator = false)
    {
        sleepEvent?.Invoke();

        if (!fromAnotherGenrator)
            transform.parent.gameObject.BroadcastMessage("SleepAllEnemies", true);

        _sleep = true;
    }

    public void AwakeAllEnemies(bool fromAnotherGenrator = false)
    {
        awakeEvent?.Invoke();

        if (!fromAnotherGenrator)
            transform.parent.gameObject.BroadcastMessage("AwakeAllEnemies", true);

        _sleep = false;
    }

   public void SpawnerRate()
    {
        rateBytime++;
        switch (rateBytime)
        {
            case 1:
                generationrate -= 0.5f;
                break;
            case 2:
                generationrate -= 1f;
                break;
            case 3:
                generationrate -= 1f;
                break;
            case 4:
                generationrate -= 1.5f;
                break;
            default:
                break;
        }
        Debug.Log(generationrate);
    }

}
