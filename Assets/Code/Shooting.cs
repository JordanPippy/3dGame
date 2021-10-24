using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject bullet;
    private GameObject player;
    void Start()
    {
        bullet = Resources.Load<GameObject>("Bullet");
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
            shoot();
    }

    private void shoot()
    {
        Instantiate(bullet, player.transform.position + player.transform.forward, Quaternion.identity);
    }
}
