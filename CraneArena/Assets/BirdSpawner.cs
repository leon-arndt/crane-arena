using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class BirdSpawner : MonoBehaviour
{
    [SerializeField] private GameObject m_BirdToSpawn;

    
    private PolygonCollider2D m_SpawnArea = null;
    private float m_RadiusToCheckSpawnsIn = 3f;
    private void Start()
    {
        m_SpawnArea = GetComponent<PolygonCollider2D>();
    }
    [Button]
    /// <summary>
    /// Starts Spawning Enemies Continuously
    /// </summary>
    public void StartSpawning()
    {
        StartCoroutine(SpawnContinously());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnContinously()
    {
        while (true)
        {
            StartCoroutine(SpawnEnemies(1));

            float spawnDelay = Mathf.Max(1f, 10f);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public IEnumerator SpawnEnemies(int amount)
    {
        GameObject spawn = m_BirdToSpawn;

        var length = m_SpawnArea.points.Length;
        for (int i = 0; i < amount; i++)
        {
            var availableSpawn = m_SpawnArea.points[UnityEngine.Random.Range(0, length)] *transform.localScale ;

            Vector3 spawnPosition = new Vector3(availableSpawn.x, transform.position.y, availableSpawn.y);
            
            var enemy = Instantiate(spawn, spawnPosition, Quaternion.identity);

            //Parent the newly spawned parent to the spawn group
           // enemy.transform.SetParent(m_spawnGroup);// #TODO: create Spawngroup

            var offset =  Vector3.zero - enemy.transform.position;
            enemy.transform.rotation = Quaternion.LookRotation(
                          Vector3.forward,
                          offset
                      );

            //TODO: check for overlap
            //random delay
            float randomDelay = UnityEngine.Random.Range(0f, 1f);
            yield return new WaitForSeconds(randomDelay);
        }
    }
    
}
