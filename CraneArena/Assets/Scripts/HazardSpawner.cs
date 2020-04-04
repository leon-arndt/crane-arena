using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawn rigidbody hazards over time
/// </summary>
public class HazardSpawner : MonoBehaviourSingleton<HazardSpawner>
{
    [SerializeField]
    private GameObject[] hazardPrefabs;

    private int index;

    public void SpawnHazard()
    {
        int index = Random.Range(0, hazardPrefabs.Length);
        GameObject hazard = Instantiate(hazardPrefabs[index]);

        //update hazard position
        hazard.transform.position = new Vector3(0, 20, 0);

        //update hazard rotation
        float randomY = Random.Range(0f, 360f);
        hazard.transform.rotation = Quaternion.Euler(new Vector3(0f, randomY, 0f));
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnHazardRoutine());
    }

    /// <summary>
    /// Looping coroutine to spawn new hazards
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnHazardRoutine()
    {
        if (GameManager.Instance.timeLeft > 10f)
        {
            SpawnHazard();
        }

        yield return new WaitForSeconds(10f);

        StartCoroutine(SpawnHazardRoutine());
    }
}
