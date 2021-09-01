using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCotroller : MonoBehaviour
{

    private Rigidbody _rigidbody;
    public float moveForce;


    public GameObject focalPoint;

    public bool hasPowerUp;
    public float powerForce;
    public float powerUpTime;
    public GameObject[] powerUpIndicators;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        //focalPoint = GameObject.Find("Focal Point");


    }

    // Update is called once per frame
    void Update()
    {

        float forwardInput = Input.GetAxis("Vertical");
        _rigidbody.AddForce(focalPoint.transform.forward * moveForce * forwardInput, ForceMode.Force);


        foreach (GameObject indicator in powerUpIndicators)
        {
            indicator.transform.position = this.transform.position + 0.5f * Vector3.down;
        }

        if (this.transform.position.y < -10)
        {
            SceneManager.LoadScene("prototype 4");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountDown());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            Vector3 awayFromPlayer = collision.gameObject.transform.position - this.transform.position;

            enemyRigidbody.AddForce(awayFromPlayer * powerForce, ForceMode.Impulse);
        }
    }

    IEnumerator PowerUpCountDown()
    {

        foreach (GameObject indicator in powerUpIndicators)
        {
            indicator.gameObject.SetActive(true);
            yield return new WaitForSeconds(powerUpTime / powerUpIndicators.Length);
            indicator.gameObject.SetActive(false);
        }
        
        hasPowerUp = false;

    }
}
