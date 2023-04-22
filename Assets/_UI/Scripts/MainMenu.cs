using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{

    public void PlayButton()
    {
        Time.timeScale = 1f;

        UIManager.Ins.OpenUI<GamePlay>(UIManager.UIID.GamePlay).SetupOnOpen(GameManager.Ins.Player);
        Close(0);
    }
}
