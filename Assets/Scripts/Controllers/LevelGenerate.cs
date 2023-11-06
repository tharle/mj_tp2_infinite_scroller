using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerate : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_PlatformPrefabSpring;
    [SerializeField] private Transform m_StartingPlatform;
    [SerializeField] private float m_DistanceSpawn = 2;

    private Transform m_PlatformEndPoint;
    Transform m_LastCreatedPlataform;


    private Transform m_PlayerTransform;
    private GameController m_GameController;

    // Start is called before the first frame update
    private void Start()
    {
        m_GameController = FindAnyObjectByType<GameController>();
        m_PlayerTransform = FindAnyObjectByType<PlayerController>().transform;
        m_LastCreatedPlataform = m_StartingPlatform;
    }

    private void Update()
    {
        if (Vector3.Distance(m_PlayerTransform.position, m_LastCreatedPlataform.position) >= m_DistanceSpawn)
        {
            CreatePlataforms();
        }
    }

    private void CreatePlataforms()
    {
        m_PlatformEndPoint = m_LastCreatedPlataform.Find("End");
        m_LastCreatedPlataform = SpawPlataform(m_PlatformEndPoint.position);
    }

    private Transform SpawPlataform(Vector3 spawnPosition)
    {
        GameObject plataformBase = m_PlatformPrefabSpring[0];
        GameObject newPlataform = Instantiate(plataformBase, spawnPosition, Quaternion.identity);
        return newPlataform.transform;
    }
}
