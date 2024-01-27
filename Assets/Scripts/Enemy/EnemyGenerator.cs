using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] enemies;
    private int numEnemies;
    public Unity.AI.Navigation.NavMeshSurface naveMeshLayer;
    void Start()
    {
        StartCoroutine(EnemySpawner());
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    IEnumerator EnemySpawner()
    {
        float posX = 0;
        float posZ = 0;
        while (numEnemies < 15)
        {
            posX = Random.Range(gameObject.transform.position.x - 10f, gameObject.transform.position.x + 10f);
            posZ = Random.Range(gameObject.transform.position.z - 10f, gameObject.transform.position.z + 10f);
           // if (CheckInNaveMesh(new Vector3(posX, enemies[enemyRandomizer()].transform.position.y, posZ)) == true)
            {
                Instantiate(enemies[enemyRandomizer()], new Vector3(posX, enemies[enemyRandomizer()].transform.position.y, posZ), Quaternion.identity);
                yield return new WaitForSeconds(3f);
                numEnemies += 1;
            }

        }
    }

    int enemyRandomizer()
    {
        int randomEnemy;
        randomEnemy = Random.Range(0, 3);
        return randomEnemy;
    }

   // bool CheckInNaveMesh(Vector3 randPosition)
   // {
   //     NavMeshHit hit;
   //     if (NavMesh.SamplePosition(randPosition, out hit, Mathf.Infinity, NavMesh.GetAreaFromName("Walkable")))
   //     {
   //         return true;
   //     }
   //     return false;
   // }

}
