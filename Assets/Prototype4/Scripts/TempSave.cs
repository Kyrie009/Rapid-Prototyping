using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSave : GameBehaviour<TempSave>
{
    //Stats
    public int maxHealth = 1000;
    public int health = 1000;
    public int atk = 100;
    public int playerLevel = 1;
    public int maxExp = 100;
    public int exp = 0;

    public void SavePlayerStatus(int _health, int _exp, int _playerLevel) //should be better to parse in the stats rather than having the script be dependent on looking it up.
    {
        health = _health;
        exp = _exp;
        playerLevel = _playerLevel;

    }
}
