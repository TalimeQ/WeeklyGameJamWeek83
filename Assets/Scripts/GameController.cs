using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;
    public GameObject PlayerPrefab{get {return playerPrefab;} set { playerPrefab = value; } }
    [SerializeField]
    private Vector3 playerSpawnCoord =  new Vector3(0, 0, 0);
    [SerializeField]
    private ButtonController telephoneButtonController;
    [SerializeField]
    private Vector3[] gridArray;
    [SerializeField]
    private float meteorSpawnInterval;
    [SerializeField]
    private GameObject meteorToSpawn;
    [SerializeField]
    private GameObject meteorUi;
    private bool bIsGameActive = false;
    public bool BIsGameActive { get { return bIsGameActive; } set { bIsGameActive = value; } }
    [SerializeField]
    private Scoreboard scoreboardRef;
    private int score;

    void Start()
    {
        
    }
    public void Finish()
    {
        StopAllCoroutines();
        meteorUi.SetActive(true);
        bIsGameActive = false;
    }
    public void UpdateScore()
    {
        score++;
        scoreboardRef.UpdateScore(score);

    }
    public void RestartScore()
    {
        score = 0;
        scoreboardRef.UpdateScore(score);
    }

    public void Initialized()
    {
        RestartScore();
        GameObject playerObj =   Instantiate(playerPrefab, playerSpawnCoord, this.transform.rotation);
        PlayerController playerCont = playerObj.GetComponent<PlayerController>();
        telephoneButtonController.PlayerControllerRef = playerCont;
        playerCont.MovementGrid = gridArray;
        meteorSpawnInterval = 5.0f;
        StartCoroutine(waveSpawner());
        // just protection
        bIsGameActive = true;
       
    }
    IEnumerator waveSpawner()
    {
        while(true)
        {
            // Pool them later.
            Vector3 spawnPos = gridArray[Random.Range(0, 15)];
            int whereToSpawn = Random.Range(0, 2);
            Vector2 realSpawnPos = new Vector2(0,0);
            switch (whereToSpawn)
            {
                case 0:
                    realSpawnPos = new Vector2(spawnPos.x, -29.25f);
                    whereToSpawn = 0;
                    break;
                case 1:
                    realSpawnPos = new Vector2(0,spawnPos.y);
                    whereToSpawn = Random.Range(0, 2);
                    if(whereToSpawn != 0)
                    {
                        realSpawnPos.x = -88.0f;
                        whereToSpawn = 1;
                    }
                    else
                    {
                        realSpawnPos.x = -73.0f;
                        whereToSpawn = 2;
                    }
                    break;
                default:
                    print("GameController :: Unknown range!");
                    break;
                
            }
            GameObject spawnedMeteor = Instantiate(meteorToSpawn, realSpawnPos ,this.transform.rotation);
            meteor meteorCompReference = spawnedMeteor.GetComponent<meteor>();
            meteorCompReference.GameContRef = this;
            switch (whereToSpawn)
            {
                case 0:
                    meteorCompReference.YMov = -1.0f;
                    meteorCompReference.XMov = 0.0f;
                    break;
                case 1:
                    meteorCompReference.XMov = 1.0f;
                    meteorCompReference.YMov = 0.0f;
                    break;
                case 2:
                    meteorCompReference.XMov = -1.0f;
                    meteorCompReference.YMov = 0.0f;
                    break;
                default:
                    break;
            }

            
            meteorSpawnInterval -= 0.1f;
            if(meteorSpawnInterval < 1f)
            {
                meteorSpawnInterval = 1f;
            }
            UpdateScore();
            yield return new WaitForSeconds(meteorSpawnInterval);
           
        }
        
    }
}
        