using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBuildBridgeState : BotBaseState
{
    public bool BrickCountSet = false;
    int BrickCount = 0;
    public override void EnterState(Bot bot)
    {
        BrickCount = Random.Range(1, 3);
    }

    public override void UpdateState(Bot bot)
    {

        if (bot.PlayerBrickList == null)
        {
            bot.SwitchState(bot.SeekBrickState);
        }
        if (bot.NextFloor)
        {           
            bot.SwitchState(bot.SeekBrickState);
        }

        if (bot.PlayerBrickList.Count >= BrickCount)
        {

            GameObject Win = GameObject.FindGameObjectWithTag("Win");
            bot.Agent.SetDestination(Win.transform.position);
        }
        
        else
        {
            bot.SwitchState(bot.SeekBrickState);
        }
    }
}
