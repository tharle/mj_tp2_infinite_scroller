using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerate : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_PlatformPrefabSpring;
    [SerializeField] private List<GameObject> m_PlatformPrefabSummer;
    [SerializeField] private List<GameObject> m_PlatformPrefabFall;
    [SerializeField] private List<GameObject> m_PlatformPrefabWinter;
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
        m_PlatformEndPoint = m_LastCreatedPlataform.Find(GameParameters.PlataformNames.END);
        m_LastCreatedPlataform = SpawPlataform(m_PlatformEndPoint.position);
    }

    private Transform SpawPlataform(Vector3 spawnPosition)
    {
        GameObject plataformBase = GetRandomPlataform();
        spawnPosition.x += GetOffsetWidthPlataform(plataformBase.transform);
        GameObject newPlataform = Instantiate(plataformBase, spawnPosition, Quaternion.identity);
        return newPlataform.transform;
    }

    private float GetOffsetWidthPlataform(Transform transformPlataform)
    {
        Transform transformPlataformEnd = transformPlataform.Find(GameParameters.PlataformNames.END);
        return transformPlataformEnd.position.x;
    }

    private GameObject GetRandomPlataform()
    {
        // int randomId = Random.RandomRange(0, 0);
        int randomId = 0;
        switch (m_GameController.getCurrentSession())
        {
            case Session.SUMMER:
                return m_PlatformPrefabSummer[randomId];
            case Session.FALL:
                return m_PlatformPrefabFall[randomId];
            case Session.WINTER:
                return m_PlatformPrefabWinter[randomId];
            case Session.SPRING:
            default:
                return m_PlatformPrefabSpring[randomId];
        }
    }
}
