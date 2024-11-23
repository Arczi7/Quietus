using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private NavMeshSurface surface;
    [SerializeField] private Score score;
    [SerializeField] private GameObject enemy;
    [SerializeField] private List<Transform> mobSpawners;
    [SerializeField] private GameObject pistolAmmoPrefab;
    [SerializeField] private GameObject riffleAmmoPrefab;
    [SerializeField] private List<Transform> ammoSpawners;
    [Header("Player")]
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerSpawner;
    private int howManyEnemies;
    private bool areSpawned = false;
    void Start()
    {
        score.ZeroScore();
        NewLevel();
    }

    void Update()
    {
        if(howManyEnemies == 0 && !areSpawned)
        {
            NewLevel();
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
        //GenerateNavMesh();
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
        for(int i = 0; i < mobSpawners.Count; i++)
        {
            Instantiate(enemy, mobSpawners[i].position, mobSpawners[i].rotation);
            howManyEnemies += 1;
        }
    }

    private void GenerateLevel()
    {
        
    }

    private IEnumerator SpawnAmmo()
    {
        yield return new WaitForSeconds(10f);
        for(int i = 0; i < ammoSpawners.Count; i++)
        {
            if(!IsAmmo(ammoSpawners[i].position))
            {
                Instantiate(enemy, mobSpawners[i].position, mobSpawners[i].rotation);
            }
        }
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

    /**
    [!] MAŁA ADNOTACJA
    FUNKCJA PONIŻEJ MOŻE POWODOWAĆ "DŁUGIE" ROZPOCZYNANIE SIĘ GRY
    PONIEWAŻ GENERUJE SIATKĘ PO KTÓREJ BĘDĄ PORUSZAĆ SIĘ PRZECIWNICY
    ZA KAŻDYM RAZEM, GDY GENEROWANY JEST NOWY UKŁAD POZIOMU
    **/
    private void GenerateNavMesh()
    {
        surface.BuildNavMesh();
    }

}
