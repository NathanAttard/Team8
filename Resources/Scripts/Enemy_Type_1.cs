using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Type_1 : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        health = 10;
        damage = 5;
        coins = 5;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
