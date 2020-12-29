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

    private static int _assaultheadshotsnum;
    public static int AssaultHeadshotsNum
    {
        get { return _assaultheadshotsnum; }
        set { _assaultheadshotsnum = value; }
    }

    private static int _handheadshotsnum;
    public static int HandHeadshotsNum
    {
        get { return _handheadshotsnum; }
        set { _handheadshotsnum = value; }
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

    private static int _assaultbulletdmg = 3;
    public static int AssaultBulletDMG
    {
        get { return _assaultbulletdmg; }
    }

    private static int _handbulletdmg = 2;
    public static int HandBulletDMG
    {
        get { return _handbulletdmg; }
    }

    private static int _headshotmulti = 3;
    public static int HeadShotMulti
    {
        get { return _headshotmulti; }
    }

    private static int _playerhealth;
    public static int PlayerHealth
    {
        get { return _playerhealth; }
        set { _playerhealth = value; }
    }
}
