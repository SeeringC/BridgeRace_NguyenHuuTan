using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject GroundBrick;
    public GameObject Floor;
    public GameObject Player;
    private void Start()
    {
        Debug.Log("spawned");
        SpawnAllBrick();
    }

    private void SpawnAllBrick()
    {
        for (int i = -5; i < 5; i++)
        {
            for (int j = -5; j < 5; j++)
            {
                Vector3 RandomSpawnPosition = Floor.transform.position + new Vector3(i * 2, 0, j * 2);

                Instantiate(GroundBrick, RandomSpawnPosition, Quaternion.identity);
            }

        }
    }


}