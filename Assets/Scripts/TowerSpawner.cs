using System.Data.SqlTypes;
using UnityEngine;
using System.Collections;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private TowerTemplate towerTemplate;
    //[SerializeField]
    //private GameObject towerPrefab;
    //[SerializeField]
    //private int towerBuildGold = 50;
    [SerializeField]
    private EnemySpawner enemySpawner;
    [SerializeField]
    private PlayerGold playerGold;
    [SerializeField]
    private SystemTextViewer SystemTextViewer;
    private bool isOnTowerButton = false;
    private GameObject followTowerClone = null;

    public void ReadyToSpawnTower()
    {
        if (isOnTowerButton == true)
        {
            return;
        }

        if (towerTemplate.weapon[0].cost > playerGold.CurrentGold)
        {
            SystemTextViewer.PrintText(SystemType.Money);
            return;
        }

        isOnTowerButton = true;

        followTowerClone = Instantiate(towerTemplate.followTowerPrefab);

        StartCoroutine("OnTowerCancelSystem");
    }
    public void SpawnTower(Transform tileTransform)
    {
        if (isOnTowerButton == false)
        {
            return;
        }
        //if (towerBuildGold > playerGold.CurrentGold)
        //if (towerTemplate.weapon[0].cost > playerGold.CurrentGold)
        //{
        //    SystemTextViewer.PrintText(SystemType.Money);
        //    return;
        //}

        Tile tile = tileTransform.GetComponent<Tile>();

        if (tile.IsBuildTower == true)
        {
            SystemTextViewer.PrintText(SystemType.Build);
            return;
        }

        isOnTowerButton = false;
        tile.IsBuildTower = true;
        //playerGold.CurrentGold -= towerBuildGold;
        playerGold.CurrentGold -= towerTemplate.weapon[0].cost;

        Vector3 position = tileTransform.position + Vector3.back;
        //GameObject clone = Instantiate(towerPrefab, position, Quaternion.identity);
        GameObject clone = Instantiate(towerTemplate.towerPrefab, position, Quaternion.identity);
        clone.GetComponent<TowerWeapon>().Setup(enemySpawner, playerGold, tile);

        Destroy(followTowerClone);

        StopCoroutine("OnTowerCancelSystem");
    }

    private IEnumerator OnTowerCancelSystem()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                isOnTowerButton = false;

                Destroy(followTowerClone);
                break;
            }

            yield return null;
        }
    }
}
