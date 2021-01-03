using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyAfterSpawns());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestroyAfterSpawns()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
