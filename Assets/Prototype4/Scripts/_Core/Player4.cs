using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player4 : GameBehaviour
{
    //Stats
    public string playerName;
    public int maxHealth;
    public int health;
    public int atk;
    public int playerLevel;
    public int maxExp;
    public int exp;

    [SerializeField]
    private GameObject destructionFXPrefab;

    void Start()
    {
        //GetDefaults();
        GetSave();
    }

    public void GetSave()
    {
        maxHealth = _TS.maxHealth;
        health = _TS.health;
        atk = _TS.atk;
        playerLevel = _TS.playerLevel;
        exp = _TS.exp;
        maxExp = _TS.maxExp;
        _UI4.UpdatePlayerStatus();
    }

    public void GetDefaults()
    {
        maxHealth = 1000;
        health = maxHealth;
        atk = 100;
        playerLevel = 1;
        exp = 0;
        maxExp = 100;
    }

    //Takes Hit
    public void Hit(int _dmg)
    {
        if (!IsDead())
        {
            health -= _dmg;
            _UI4.UpdatePlayerStatus();
            if (IsDead())
            {
                Instantiate(destructionFXPrefab, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
                _UI4.GameOver();
            }
        }
    }

    public bool IsDead() //Check if enemy dead
    {
        return health <= 0;
    }

    public void RewardExp(int _exp)
    {
        exp += _exp;
        if (exp >= maxExp)
        {
            playerLevel++;
            exp = 0;
        }
        _UI4.UpdatePlayerStatus();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death"))
        {
            _UI4.GameOver();
        }
    }
}
