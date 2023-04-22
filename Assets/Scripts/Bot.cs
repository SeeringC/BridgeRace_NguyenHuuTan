using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{

    BotBaseState currentState;
    public BotIdleState IdleState = new BotIdleState();
    public BotSeekBrickState SeekBrickState = new BotSeekBrickState();
    public BotBuildBridgeState BuildBridgeState = new BotBuildBridgeState();

    public NavMeshAgent Agent;

    public int ID = 1;
    public Vector3 Target;
    Vector3 Destination;

    public override void Start()
    {
        base.Start();
        currentState = SeekBrickState;
        currentState.EnterState(this);
    }
    public override void Update()
    {
        base.Update();
        currentState.UpdateState(this);

    }

    public void SwitchState(BotBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void DestroyALlMapBrick()
    {
        GameObject MapBrickToDestroy = GameObject.FindGameObjectWithTag("MapBrick");
        if (MapBrickToDestroy.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial == renderers.sharedMaterial)
        {
            Destroy(MapBrickToDestroy.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Win"))
        {
            UIManager.Ins.OpenUI<GamePlay>(UIManager.UIID.Lose);

        }
    }
}
