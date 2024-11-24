using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private Score score;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject pistolAmmoPrefab;
    [SerializeField] private GameObject rifleAmmoPrefab;
    [Header("Player")]
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerSpawner;
    [Header("Level Sets")]
    [SerializeField] private List<GameObject> levelSets;
    [Header("Spawners")]
    [SerializeField] private List<Transform> mobSpawners;
    [SerializeField] private List<Transform> ammoSpawners;
    private int howManyEnemies;
    private int currentLevel = 1;
    private bool areSpawned = false;
    private bool ammoSpawned = false;
    void Start()
    {
        AudioManager.Instance.BackgroundMusicManager(SceneManager.GetActiveScene().name);
        score.ZeroScore();
        NewLevel();
    }

    void Update()
    {
        if(howManyEnemies == 0 && !areSpawned)
        {
            NewLevel();
        }
        if(areSpawned && !ammoSpawned)
        {
            StartCoroutine(SpawnAmmo());
        }
    }

    public void RemoveEnemy()
    {
        howManyEnemies -= 1;
        if(howManyEnemies == 0)
        {
            areSpawned = false;
        }
    }

    private void NewLevel()
    {
        areSpawned = true;
        MovePlayer();
        GenerateLevel();
        SpawnEnemies();
        score.AddLevel();
        FindAnyObjectByType<UIManager>().ShowLevelText();
    }

    private void MovePlayer()
    {
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = playerSpawner.position;
        player.GetComponent<CharacterController>().enabled = true;
    }

    private void SpawnEnemies()
    {
        string checkName = "EnemySpawner LS" + currentLevel.ToString();
        for(int i = 0; i < mobSpawners.Count; i++)
        {
            if(mobSpawners[i].gameObject.name == checkName)
            {
                Instantiate(enemy, mobSpawners[i].position, mobSpawners[i].rotation);
                howManyEnemies += 1;  
            }
        }
    }

    private void GenerateLevel()
    {
        levelSets[currentLevel].SetActive(false);
        currentLevel = UnityEngine.Random.Range(0, levelSets.Count);
        Debug.Log("RANDOM == " + currentLevel);
        levelSets[currentLevel].SetActive(true);
    }

    private IEnumerator SpawnAmmo()
    {
        ammoSpawned = true;
        yield return new WaitForSeconds(10f);
        if(!IsAmmo(ammoSpawners[0].position))
        {
            Instantiate(pistolAmmoPrefab, ammoSpawners[0].position, ammoSpawners[0].rotation);
        }
        if(!IsAmmo(ammoSpawners[1].position) && score.GetLevel() > 5)
        {
            Instantiate(rifleAmmoPrefab, ammoSpawners[1].position, ammoSpawners[1].rotation);
        }
        ammoSpawned = false;
    }

    private bool IsAmmo(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 1f);
        foreach(Collider collider in colliders)
        {
            if (collider.CompareTag("Ammo"))
            {
                return true;
            }
        }
        return false;
    }

    //GETTERY
    public int GetHowManyEnemies()
    {
        return howManyEnemies;
    }

}
