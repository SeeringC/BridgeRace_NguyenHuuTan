using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public GameObject GroundBrick;
    public GameObject CurrentFloor;
    public GameObject ExitCharacter;
    public Character Player;
    public Character bot1;
    public Character bot2;
    public Character bot3;

    public List<GameObject> BrickList;


    private bool IsSpawned = false;
    private Character character;


    private void Update()
    {

        if (bot1.NeedToDestroy == true)

        {
            DestroyBrick1();
        }
        if (Player.NeedToDestroy == true)
        {
            DestroyBrick2();
        }
        if (bot2.NeedToDestroy == true)

        {
            DestroyBrick3();
        }
        if (bot3.NeedToDestroy == true)

        {
            DestroyBrick4();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!IsSpawned)
            {
                Debug.Log("im spawned");
                SpawnAllBrick();
                IsSpawned = true;
            }
            if (!other.gameObject.transform.GetComponent<Character>().MapBrickShown)
            {
                for (int i = 0; i < BrickList.Count; i++)
                {
                    if (BrickList[i] != null)
                    {
                        Debug.Log("brick color is: " + BrickList[i].transform.GetChild(0).GetComponent<Renderer>().sharedMaterial);
                        Debug.Log("player color is: " + other.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial);
                        if (BrickList[i].transform.GetChild(0).GetComponent<Renderer>().sharedMaterial ==
                        other.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial)
                        {
                            Debug.Log("same color");
                            BrickList[i].transform.GetChild(0).GetComponent<Renderer>().enabled = true;
                        }
                    }
                }
                other.gameObject.transform.GetComponent<Character>().MapBrickShown = true;  
            }
        }
    }
   
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ExitCharacter = other.gameObject;
        }
    }
    private void SpawnAllBrick()
    {
        int k = 0;
        for (int i = -5; i < 5; i++)
        {
            for (int j = -5; j < 5; j++)
            {
                Vector3 SpawnBrickPosition = CurrentFloor.transform.position + new Vector3(i * 2, 0, j * 2);

                GameObject Brick = Instantiate(GroundBrick, SpawnBrickPosition, Quaternion.identity, transform);
                Brick.transform.GetComponent<MapBrick>().ChangeColorBrick();
                BrickList.Add(Brick);
                Brick.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
                k++;
            }
        }
        
    }

    private void DestroyBrick1()
    {

        for (int i = 0; i < BrickList.Count; i++)
        {
            if (BrickList[i] != null)
            {
                if (BrickList[i].transform.GetChild(0).GetComponent<Renderer>().sharedMaterial ==
                bot1.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial)
                {

                    Destroy(BrickList[i].gameObject);
                }
            }
            bot1.NeedToDestroy = false;
        }
    }

    private void DestroyBrick2()
    {
        for (int i = 0; i < BrickList.Count; i++)
        {
            if (BrickList[i] != null)
            {
                if (BrickList[i].transform.GetChild(0).GetComponent<Renderer>().sharedMaterial ==
                Player.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial)
                {

                    Destroy(BrickList[i].gameObject);

                }
            }
            Player.NeedToDestroy = false;
        }
    }
    private void DestroyBrick3()
    {

        for (int i = 0; i < BrickList.Count; i++)
        {
            if (BrickList[i] != null)
            {
                if (BrickList[i].transform.GetChild(0).GetComponent<Renderer>().sharedMaterial ==
                bot2.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial)
                {

                    Destroy(BrickList[i].gameObject);
                }
            }
            bot2.NeedToDestroy = false;
        }
    }
    private void DestroyBrick4()
    {

        for (int i = 0; i < BrickList.Count; i++)
        {
            if (BrickList[i] != null)
            {
                if (BrickList[i].transform.GetChild(0).GetComponent<Renderer>().sharedMaterial ==
                bot3.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial)
                {
                    Destroy(BrickList[i].gameObject);
                }
            }
            bot3.NeedToDestroy = false;
        }
    }


  
}
