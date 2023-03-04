using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float acceleration = 10;
    public GameObject bulletprefab;
    private Transform gunleft, gunright;

    private Rigidbody rb;
    private Vector2 controlls;
    private bool FireButtonDown = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gunleft = transform.Find("gunleft");
        gunright = transform.Find("gunright");

    }

    // Update is called once per frame
    void Update()
    {
        float v, h;
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
        controlls = new Vector2(h, v);
        if(Mathf.Abs(transform.position.x) > 19)
        {
            Vector3 newPosition = new Vector3(transform.position.x * -1, 0, transform.position.z);
            transform.position = newPosition;
        }
        if (Mathf.Abs(transform.position.z) > 9)
        {
            Vector3 newPosition = new Vector3(transform.position.x, 0, transform.position.z * -1);
            transform.position = newPosition;
        }
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            FireButtonDown = true;
        }




    }

    void FixedUpdate()
    {
        rb.AddForce(transform.forward * controlls.y * acceleration, ForceMode.Acceleration);
        rb.AddTorque(transform.up * controlls.x * acceleration, ForceMode.Acceleration);
        if (FireButtonDown)
        {
            GameObject bullet1 = Instantiate(bulletprefab, gunleft.position, Quaternion.identity);
            bullet1.transform.parent = null;
            bullet1.GetComponent<Rigidbody>().AddForce(transform.forward * 10,
                                                        ForceMode.VelocityChange);
            Destroy(bullet1, 5);
            GameObject bullet2 = Instantiate(bulletprefab, gunright.position, Quaternion.identity);
            bullet2.transform.parent = null;
            bullet2.GetComponent<Rigidbody>().AddForce(transform.forward * 10,
                                                        ForceMode.VelocityChange);
            Destroy(bullet2, 5);
            FireButtonDown = false;

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject target = collision.gameObject;
        if (target.CompareTag("Enemy"))
        {
            //game over
            Time.timeScale = 0;
            GameObject gameOverScreen = GameObject.Find("Canvas").transform.Find("GameOverScreen").gameObject;
            gameOverScreen.SetActive(true);

            Destroy(target);
            Destroy(this.gameObject);
        }

    }
}
