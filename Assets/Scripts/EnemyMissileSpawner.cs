using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private float Ypadding = 0.5f;
    private float minX, maxX;

    public int missilesCountToSpawn = 20;
    public float missilesDelay = 0.5f;
    float yValue;
    void Start()
    {
        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 1 ,0)).x;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 1 ,0)).x;
        float randomX = Random.Range(minX, maxX);
        yValue = Camera.main.ViewportToWorldPoint(new Vector3(0, 1 ,0)).y;

        Instantiate(missilePrefab, new Vector3(randomX, yValue + Ypadding, 0), Quaternion.identity);
    }

    void Update()
    {
        
    }
    public void StartRound(){
        StartCoroutine(SpawnMissiles());
    }
    public IEnumerator SpawnMissiles()
    {
        while (missilesCountToSpawn > 0)
        {
        float randomX = Random.Range(minX, maxX);
        Instantiate(missilePrefab, new Vector3(randomX, yValue + Ypadding, 0), Quaternion.identity);

        missilesCountToSpawn --;
        yield return new WaitForSeconds(missilesDelay);
        }
    }
    

}
