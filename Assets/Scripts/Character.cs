using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour
{
    public GameObject PlayerBrick;
    public Rigidbody rb;
    public float Speed = 10.0f;
    public GameObject RayCastPos;
    public List<GameObject> PlayerBrickList;

    public Transform GroundCheck;
    public GameObject SpawnBrick;
    public Transform ConstSpawnBrickPos;
    public GameObject PlayerBrickPos;
    public ColorData color;
    public MeshRenderer renderers;
    public GameObject Canvas;
    public RaycastHit hit;
    GameObject RemovePlayerBrick;

    public bool NextFloor = false;
    public bool NeedToDestroy = false;
    public bool MapBrickShown = false;
    
    public virtual void Start()
    {
        ChangeColor();

    }

    
    public virtual void Update()
    {

        DeactiveStair();

        Debug.DrawRay(RayCastOrigin(), Vector3.down, Color.red);
        OnStair();
        CreateAndDestroyBrick();

        color.ax = 0;
    }
    
    

    public void ChangeColor()
    {
        renderers.sharedMaterial = color.GetColor2();
        color.ax++;
    }
      
    public void CreateAndDestroyBrick()
    {

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f, ~(1 << 8)))
        {

            if (hit.transform.CompareTag("MapBrick") && hit.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial == renderers.sharedMaterial)
            {

                    PlayerBrickPos.transform.position = PlayerBrickPos.transform.position + new Vector3(0f, 0.6f, 0f);

                    PlayerBrick.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial = renderers.sharedMaterial;
                    GameObject BrickToAdd = Instantiate(PlayerBrick, ConstSpawnBrickPos.position, transform.rotation, PlayerBrickPos.transform);
                   
                    PlayerBrickList.Add(BrickToAdd);
                    hit.collider.GetComponent<BoxCollider>().enabled = false;
                    hit.collider.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
            }
            if (hit.transform.CompareTag("StairBrick"))
            {
                if (IsMovingUp())
                {
                    if (hit.collider.gameObject.GetComponent<MeshRenderer>().enabled == false ||
                        hit.collider.gameObject.GetComponent<MeshRenderer>().sharedMaterial != renderers.sharedMaterial)
                    {
                        hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                        hit.collider.GetComponent<MeshRenderer>().sharedMaterial = renderers.sharedMaterial;

                        if (PlayerBrickList != null)
                        {
                            RemovePlayerBrick = PlayerBrickList[0];
                        }

                        Destroy(RemovePlayerBrick);

                        PlayerBrickPos.transform.position = PlayerBrickPos.transform.position;
                        PlayerBrickList.RemoveAt(0);

                    }
                }
            }
        } 
    }

 
    public void DeactiveStair()
    {
        if (Physics.Raycast(RayCastOrigin(), Vector3.down, out hit, 8f, ~(1 << 8)))
        {
            if (hit.transform.CompareTag("StairBrick"))
            {

                if (PlayerBrickList.Count != 0)
                {
                    hit.collider.isTrigger = true;
                }
            }
        }

        if (Physics.Raycast(transform.position, Vector3.forward, out hit, 1f, ~(1 << 8)))
        {

            if (hit.transform.CompareTag("StairLock"))
            {
                if (PlayerBrickList.Count != 0)
                {
                    hit.collider.transform.gameObject.SetActive(false);
                }
            }
            if (hit.transform.CompareTag("DoorClosed"))
            {
                hit.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                hit.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
                hit.collider.isTrigger = true;
            }

            if (hit.transform.CompareTag("DoorOpen"))
            {
                hit.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
                hit.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DoorClosed"))
        {
            other.isTrigger = false;
            
        }
        if (other.CompareTag("DoorOpen"))
        {
            MapBrickShown = false;
            NextFloor = true;
            NeedToDestroy = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            NextFloor = false;
        }
    }
    
    public bool IsMovingUp()
    {
        RaycastHit HitFront;
        RaycastHit HitBody;
        if (Physics.Raycast(transform.position, Vector3.down, out HitBody, 2f, ~(1 << 8)))
        {
            if (Physics.Raycast(RayCastOrigin(), Vector3.down, out HitFront, 2f, ~(1 << 8)))
            {
                if (HitFront.transform.position.y < HitBody.transform.position.y)
                {
                    return false;
                }
            }
        }
        return true;
}
    
    
    public bool OnStair()
    {
        Debug.DrawRay(RayCastOrigin(), Vector3.down, Color.blue);

        if (Physics.Raycast(RayCastOrigin(), Vector3.down, out hit, 0.1f))
        {

            if (hit.normal != Vector3.up)
            {
                return true;
            }
        }
        return false;
    }



    public Vector3 RayCastOrigin()
    {
        return (RayCastPos.transform.position);
    }
    

    

   
}
    
 