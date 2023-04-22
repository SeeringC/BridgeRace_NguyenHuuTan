using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BotBaseState
{
    public abstract void EnterState(Bot bot);

    public abstract void UpdateState(Bot bot);
    


}
