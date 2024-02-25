using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float blockSpeed = 5f;
    public float lifeTime = 5f;

    private Rigidbody2D block;

    void Start()
    {
        block = gameObject.GetComponent<Rigidbody2D>();
        block.constraints = RigidbodyConstraints2D.FreezePositionX;
        block.constraints = RigidbodyConstraints2D.FreezeRotation;
        Destroy(transform.gameObject, lifeTime);
    }

    void Update()
    {
        block.velocity = new Vector2(-blockSpeed, 0);
    }
}
