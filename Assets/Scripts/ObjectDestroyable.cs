using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyable : MonoBehaviour
{
    public float health;
    public List<GameObject> parts = new List<GameObject>();
    private BoxCollider boxCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        for(int i = 0; i< parts.Count; i++)
        {
            parts[i].AddComponent<Rigidbody>();
            Rigidbody rb = parts[i].GetComponent<Rigidbody>();
            float x = Random.Range(-100, 100);
            float y = Random.Range(0, 100);
            float z = Random.Range(-100, 100);
            rb.AddForce(new Vector3(x, y, z));
        }
        boxCollider.isTrigger = true;
        Destroy(this);
    }
}
