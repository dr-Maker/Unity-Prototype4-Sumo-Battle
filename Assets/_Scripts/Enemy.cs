using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Rigidbody _rigidbody;
    public float moveForce;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {

        _rigidbody = GetComponent<Rigidbody>();

        player = GameObject.Find("Player");


    }

    // Update is called once per frame
    void Update()
    {

        //Vector Destino - Origen
        Vector3 LookDirection = (player.transform.position - this.transform.position).normalized;
        _rigidbody.AddForce(moveForce * LookDirection, ForceMode.Force);

        if (this.transform.position.y < -10)
        {
            Destroy(gameObject);
        }

    }
}
