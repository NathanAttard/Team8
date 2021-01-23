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

    private static int _assaultammo = 280;
    public static int AssaultAmmo
    {
        get { return _assaultammo; }
        set { _assaultammo = value; }
    }

    private static int _handammo = 80;
    public static int HandAmmo
    {
        get { return _handammo; }
        set { _handammo = value; }
    }

    private static int _assaultmag;
    public static int AssaultMag
    {
        get { return _assaultmag; }
        set { _assaultmag = value; }
    }

    private static int _handmag;
    public static int HandMag
    {
        get { return _handmag; }
        set { _handmag = value; }
    }

    private static bool _isassault;
    public static bool IsAssault
    {
        get { return _isassault; }
        set { _isassault = value; }
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

    private static int _playerhealth = 100;
    public static int PlayerHealth
    {
        get { return _playerhealth; }
        set { _playerhealth = value; }
    }

    private static Vector3 _playerposition;
    public static Vector3 PlayerPosition
    {
        get { return _playerposition; }
        set { _playerposition = value; }
    }

    private static GameObject _playerobject;
    public static GameObject PlayerObject
    {
        get { return _playerobject; }
        set { _playerobject = value; }
    }
}
