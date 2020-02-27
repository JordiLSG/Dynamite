using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    public float delay;
    public float radius;
    public float force;

    public GameObject explosionEffect;

    private float countdown;

    private bool exploded = false;

    private void Start()
    {
        countdown = delay;
    }

    private void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0 && exploded == false)
        {
            StartCoroutine(Explode());
        }
    }


    private IEnumerator Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        exploded = true;
        print("Exploded");
        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, radius);

        for (int i = 0; i < collidersToDestroy.Length; i++)
        {
            Destructible destructible = collidersToDestroy[i].gameObject.GetComponent<Destructible>();

            if (destructible != null)
            {
                destructible.Explode();
            }
        }

        yield return new WaitForEndOfFrame();

        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);

        for (int i = 0; i < collidersToMove.Length; i++)
        {
            Rigidbody rigidbody = collidersToMove[i].gameObject.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(force, transform.position, radius);
            }
        }

        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
