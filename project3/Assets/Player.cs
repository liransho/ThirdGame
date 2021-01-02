using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Vector3 forward = new Vector3(0, 0, 1);
    Vector3 rotationRight = new Vector3(0, 30, 0);
    Vector3 rotationLeft = new Vector3(0, -30, 0);

    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    Transform cube;
    [SerializeField]
    float speed = 30f;
    [SerializeField]
    Text score;

    bool gameEnded = false;
    float restartDelay = 0.3f;
    float roadHeight = 0f;
    float maxSpeed = 40f;
    float second = 0;
    int x;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("w"))
        {
            cube.Translate(forward * speed * Time.deltaTime);
            speed += 0.01f;
            if (speed >= maxSpeed)
            {
                speed = maxSpeed;
            }


        }

        if (Input.GetKey("d"))
        {
            Quaternion deltaRotationRight = Quaternion.Euler(rotationRight * Time.deltaTime);
            rb.MoveRotation(rb.rotation * deltaRotationRight);
        }

        if (Input.GetKey("a"))
        {
            Quaternion deltaRotationLeft = Quaternion.Euler(rotationLeft * Time.deltaTime);
            rb.MoveRotation(rb.rotation * deltaRotationLeft);
        }

        if (cube.position.y < roadHeight)
        {
            GameOver();
        }

        second += Time.deltaTime;
        x = Mathf.FloorToInt(second);
        score.text = x.ToString();



    }

    public void GameOver()
    {
        if (gameEnded == false)
        {
            gameEnded = true;
            Invoke("Restart", restartDelay);
        }
    }

    void Restart()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Finish")
        {
            Debug.Log("CONGRATULATIONS YOU HAVE FINISH THE RACE!!!!!");
            GameOver();

        }
        

    }


}
