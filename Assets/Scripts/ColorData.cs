using System.Runtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class ColorData : ScriptableObject
{
    
    enum colorType
    {
        red = 0,
        green = 1,
        blue = 2,
        yeallow = 3,
    }
    public Material[] mats;
    public int ax = 0;


    public bool ColorIsBeingSet = false;

    private void OnEnable()
    {
       ax = Random.Range(0, 4);
    }

    public Material GetColor2()
    {
        while (!ColorIsBeingSet)
        {
            ColorIsBeingSet = true;
            if (ax == 4)
            {
                ax = 0;
            }
        }
        ColorIsBeingSet = false;
        return mats[ax];
    }
  
    public Material GetColorBrick()
    {
        int color = Random.Range(0, 4);

        return mats[color];
    }



   
   
}
