using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] GameObject missilePrefab;
    [SerializeField] GameObject missileLauncherPrefab;
    [SerializeField] private Texture2D cursorTexture;
    private Vector2 cursorHotspot;
    
    private GameController myGameController;
    private bool flag = true;

    void Start()
    {
        myGameController = GameObject.FindObjectOfType<GameController>();

        cursorHotspot = new Vector2(cursorTexture.width / 2f, cursorTexture.height);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }

 
    void Update()
    {
            if (Input.GetMouseButton(0) && myGameController.currentMissilesLoadedInLauncher > 0 && !myGameController.isGameOver)
            {
                if (flag)
                {
                    flag = false;
                    StartCoroutine(Wait());
                    
                }
            }
    }

    IEnumerator Wait(){
        FindObjectOfType<AudioManager>().PlaySound("shoot");
        Instantiate(missilePrefab, missileLauncherPrefab.transform.position, Quaternion.identity);
        myGameController.PlayerFiredMissle();
        yield return new WaitForSeconds(0.2f);
        flag = true;
    }
}
