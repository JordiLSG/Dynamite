using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject destructiblePrefab;

    //private void OnMouseDown()
    //{
    //    Explode();
    //}

    public void Explode()
    {
        Instantiate(destructiblePrefab, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
