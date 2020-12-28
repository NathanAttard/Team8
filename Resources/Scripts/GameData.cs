using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameData : MonoBehaviour
{
    //Data to be saved in Flask Server
    private static string _username;
    public static string Username
    {
        get { return _username; }
        set { _username = value; }
    }

    private static int _headshotsnum;
    public static int HeadshotsNum
    {
        get { return _headshotsnum; }
        set { _headshotsnum = value; }
    }


    //-------------------------------------------------------

    private static int _coins;
    public static int Coins
    {
        get { return _coins; }
        set { _coins = value; }
    }

    private static int _ammo;
    public static int Ammo
    {
        get { return _ammo; }
        set { _ammo = value; }
    }

    private static int _bulletdmg = 2;
    public static int BulletDMG
    {
        get { return _bulletdmg; }
    }

    private static int _playerhealth;
    public static int PlayerHealth
    {
        get { return _playerhealth; }
        set { _playerhealth = value; }
    }
}
