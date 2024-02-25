using System.Collections.Generic;
using UnityEngine;

public class SpawnGameObject : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_GameObjects;
    [SerializeField] private Vector2 m_DelaySpawnRange = new Vector2(1, 3);
    private float m_ElapseTime = 0;
    private float m_DelaySpawn = 1;

    private GameController m_Controller;

    private void Start()
    {
        m_Controller = FindAnyObjectByType<GameController>();
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
        float delayMin = m_DelaySpawnRange.x / m_Controller.GetLevel();
        float delayMax = m_DelaySpawnRange.y / m_Controller.GetLevel();
        m_DelaySpawn = Random.Range(delayMin, delayMax);
    }


    private void DoSpawn()
    {
        GameObject plataformBase = GetRandomEnemy();
       Instantiate(plataformBase, transform.position, Quaternion.identity);
    }

    private GameObject GetRandomEnemy()
    {
        int randomId = Random.Range(0, m_GameObjects.Count);

        return m_GameObjects[randomId];

    }
}
