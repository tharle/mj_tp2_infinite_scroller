using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGameObject : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_GameObjects;
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
            DoSpawn();
            RandomiseDelaySpawn();
        }
    }

    private void RandomiseDelaySpawn() {
        m_DelaySpawn = Random.Range(m_DelaySpawnRange.x, m_DelaySpawnRange.y);
    }


    private void DoSpawn()
    {
        GameObject plataformBase = GetRandomEnemy();
       Instantiate(plataformBase, transform.position, Quaternion.identity);
    }

    private GameObject GetRandomEnemy()
    {
        // int randomId = Random.RandomRange(0, 0);
        int randomId = 0;

        return m_GameObjects[randomId];

    }
}
