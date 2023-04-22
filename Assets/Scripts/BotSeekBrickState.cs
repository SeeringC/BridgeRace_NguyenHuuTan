using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BotSeekBrickState : BotBaseState
{
    private bool TargetSetted = false;
    int BrickCount = 0;
    int BrickCountToBuild = 0;
    public override void EnterState(Bot bot)
    {
        BrickCount = Random.Range(10, 18);
        BrickCountToBuild = Random.Range(5, 15);
    }

    public override void UpdateState(Bot bot)
    {

        
        if ((bot.PlayerBrickList.Count >= BrickCountToBuild) && !TargetSetted)
        {
            bot.SwitchState(bot.BuildBridgeState);
        }

        if (bot.NextFloor == true)
        {
            TargetSetted = false;
            bot.NextFloor = false;
        }
        if (bot.PlayerBrickList.Count < BrickCount)
        {
            MapBrick[] AllBrick = GameObject.FindObjectsOfType<MapBrick>();
            if (!TargetSetted)
            {
                GameObject TargetBrick = AllBrick[Random.Range(0, AllBrick.Count() - 1)].gameObject;
                if (TargetBrick.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial == bot.renderers.sharedMaterial)
                {
                    bot.Target = TargetBrick.gameObject.transform.position;
                    TargetSetted = true;
                }
            }

            if (TargetSetted)
            {
                bot.Agent.SetDestination(bot.Target);

                if (Vector3.Distance(bot.transform.position, bot.Target) < 1.1f)
                {
                    TargetSetted = false;
                }
            }
            
        }
    }
}
