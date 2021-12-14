using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{

    [SerializeField] private GameObject explosionPrefab;
    private GameController myGameController;
    [SerializeField] private float speed = 0.3f;
    GameObject[] houses;
    Vector3 target;
    void Start()
    {
        myGameController = GameObject.FindObjectOfType<GameController>();
        houses = GameObject.FindGameObjectsWithTag("Houses");
        target = houses[Random.Range(0, houses.Length)].transform.position;    
        speed = myGameController.enemyMissileSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position == target)
        {
            myGameController.EnemyMissileDestroyed();
            MissileExplode();
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Houses")
        {
            myGameController.EnemyMissileDestroyed();
            MissileExplode();
            if(other.GetComponent<MissileLauncher>() != null)
            {
                myGameController.PlayerHit();
                return;
            }
            myGameController.houseNumber--;
            Destroy(other.gameObject);
        }
        else if (other.tag == "Explosions")
        {
            myGameController.getScore();
            MissileExplode();
        }
    }

    private void MissileExplode(){
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
    }
}
