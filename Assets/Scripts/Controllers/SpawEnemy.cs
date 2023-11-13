using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawEnemy : MonoBehaviour
{
    [SerializeField] private List<EnemyController> m_Enemys;
    [SerializeField] private Vector2 m_DelaySpawnRange = new Vector2(1, 3);
    private float m_ElapseTime = 0;
    private float m_DelaySpawn = 1;

    private void Start()
    {
        RandomiseDelaySpawn();
    }

    void Update()
    {
        m_ElapseTime += Time.deltaTime;

        if (m_ElapseTime >= m_DelaySpawn)
        {
            m_ElapseTime -= m_DelaySpawn;
            SpawnEnemy();
            RandomiseDelaySpawn();
        }
    }

    private void RandomiseDelaySpawn() {
        m_DelaySpawn = Random.Range(m_DelaySpawnRange.x, m_DelaySpawnRange.y);
    }


    private void SpawnEnemy()
    {
       EnemyController plataformBase = GetRandomEnemy();
       Instantiate(plataformBase, transform.position, Quaternion.identity);
    }

    private EnemyController GetRandomEnemy()
    {
        // int randomId = Random.RandomRange(0, 0);
        int randomId = 0;

        return m_Enemys[randomId];

    }
}
