using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerate : MonoBehaviour
{
    [SerializeField] private GameController m_Controller;
    [SerializeField] private List<GameObject> m_PlatformPrefabSpring;
    [SerializeField] private List<GameObject> m_PlatformPrefabSummer;
    [SerializeField] private List<GameObject> m_PlatformPrefabFall;
    [SerializeField] private List<GameObject> m_PlatformPrefabWinter;
    [SerializeField] private Transform m_StartingPlatform;
    [SerializeField] private float m_DistanceSpawn = 2;

    private Transform m_PlatformEndPoint;
    Transform m_LastCreatedPlataform;


    private Transform m_PlayerTransform;

    // Start is called before the first frame update
    private void Start()
    {
        m_Controller = FindAnyObjectByType<GameController>();
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
        m_PlatformEndPoint = m_LastCreatedPlataform.Find(GameParameters.PlataformName.END);
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
        Transform transformPlataformEnd = transformPlataform.Find(GameParameters.PlataformName.END);
        return transformPlataformEnd.position.x;
    }

    private GameObject GetRandomPlataform()
    {
        
        switch (m_Controller.getCurrentSession())
        {
            case Session.SUMMER:
                int randomId = Random.Range(0, m_PlatformPrefabSummer.Count);
                return m_PlatformPrefabSummer[randomId];
            case Session.FALL:
                randomId = Random.Range(0, m_PlatformPrefabFall.Count);
                return m_PlatformPrefabFall[randomId];
            case Session.WINTER:
                randomId = Random.Range(0, m_PlatformPrefabWinter.Count);
                return m_PlatformPrefabWinter[randomId];
            case Session.SPRING:
            default:
                randomId = Random.Range(0, m_PlatformPrefabSpring.Count);
                return m_PlatformPrefabSpring[randomId];
        }
    }
}
