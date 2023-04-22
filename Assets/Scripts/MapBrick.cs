using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBrick : MonoBehaviour
{
    public ColorData color;
    public MeshRenderer renderers;
    public GameObject GroundBrick;
   
    public Vector3 SpawnBrickPosition;
    public bool NeedToSpawn;
    //void Start()
    //{
    //    ChangeColorBrick();
    //}
    public void ChangeColorBrick()
    {
        renderers.sharedMaterial = color.GetColorBrick();

    }

    private void Update()
    {

    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (renderers.sharedMaterial == other.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial)
            {
                SpawnBrickPosition = transform.position;
                NeedToSpawn = true;
                StartCoroutine(CheckToRespawnBrick());
            }
        }
    }

    private IEnumerator CheckToRespawnBrick()
    {       
            if (NeedToSpawn == true)
            {

                yield return new WaitForSeconds(8);
                transform.GetComponent<BoxCollider>().enabled = true;
                transform.GetChild(0).GetComponent<Renderer>().enabled = true;

                NeedToSpawn = false;

            }
    }

    


}
