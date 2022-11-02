using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField]float accelerationPower = 15f;
    [SerializeField]float steeringPower = 2f;
    [SerializeField]bool isAccelerating, isBraking, isDrifting, countownStarted;
    public bool gameOver, hasDelivery;
    public float finalDeliveries;
    [SerializeField]float steeringAmount, speed, direction;
    [SerializeField]float deliveries, deliveryTime;
    [SerializeField]Text timerText, deliveryText, accelerationText, hasDeliveryText;
    [SerializeField]GameObject gameOverMenu;

    public int randomNumber;
    private int lastNumber;
    public List<GameObject> PickupPoints = new List<GameObject>();
    public List<GameObject> DropoffPoints = new List<GameObject>();
    private GameObject currentPPoint;
    private GameObject currentDpoint;
    AudioSource audioSource;
    public AudioClip baggrab;
    public AudioClip hurt;
    public AudioClip accell;
    public AudioClip dropoff;
    public AudioClip accellextra;
    public AudioClip drift;
    
    // Start is called before the first frame update
    void Start()
    {
        randomNumber = 0;
        rb = GetComponent<Rigidbody2D>();
        isAccelerating = false;
        isBraking = false;
        isDrifting = false;
        deliveries = 0;
        deliveryTime = 60f;
        timerText.text = ($"Time Remaining for next delivery: {deliveryTime} seconds");
        deliveryText.text = ($"Deliveries Completed: {deliveries}");
        hasDeliveryText.text = ("Pickup the food");
        gameOver = false;
        gameOverMenu.SetActive(false);
        Time.timeScale = 1;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        steeringAmount = -Input.GetAxis ("Horizontal");
        speed = Input.GetAxis("Vertical")*accelerationPower;
        direction = Mathf.Sign(Vector2.Dot (rb.velocity, rb.GetRelativeVector(Vector2.up)));
        rb.rotation += steeringAmount * steeringPower * rb.velocity.magnitude * direction;

        rb.AddRelativeForce (Vector2.up * speed);

        rb.AddRelativeForce (-Vector2.right * rb.velocity.magnitude *steeringAmount/2);
    }

    void Update()
    {
                if (randomNumber >= 0)
        {
                foreach (GameObject obj in PickupPoints)
            {
                obj.SetActive(false);
            }
                foreach (GameObject obj in DropoffPoints)
            {
                obj.SetActive(false);
            }
            PickupPoints[randomNumber].SetActive(true);
            DropoffPoints[randomNumber].SetActive(true);
        }



        accelerationText.text = ($"{Mathf.Abs(speed).ToString("F1")} mph");
        if (Input.GetButton("Accelerate") || Input.GetAxisRaw("Accelerate") > 0)
        {
            isAccelerating = true;
            if (Input.GetButton("Brake"))
            {
            }
            else
            {
             if (audioSource.isPlaying)
            {
             
            }
            else
            {
            if (accelerationPower > 30)
            {
            PlaySound(accellextra);       
            }
            else
            {
            PlaySound(accell); 
            }
            }
            }
        }
        
        
        else
        {
            isAccelerating = false;
        }
        AccelerationCheck();

        if (Input.GetButton("Brake") || Input.GetAxisRaw("Brake") > 0)
        {
            isBraking = true;
            BrakeCheck();
            if (isAccelerating)
            {
                isDrifting = true;
                isBraking = false;
                DriftCheck();
                 if (audioSource.isPlaying)
            {
             audioSource.Stop();
            }
            else
            {
                PlaySound(drift); 
            }
            }
        }
        else
        {
            isBraking = false;
            isDrifting = false;
            BrakeCheck();
            DriftCheck();
        }
        if (Input.GetButtonDown("Escape"))
        {
            Debug.Log ("Game closed");
            Application.Quit();
        }

        if (countownStarted)
        {
            deliveryTime -= Time.deltaTime;
            UpdateCountdown();
        }

        if (deliveryTime <= 0)
        {
            countownStarted = false;
            UpdateCountdown();
            gameOver = true;
            gameOverMenu.SetActive(true);
            speed = 0;
            accelerationPower = 0;
            finalDeliveries = deliveries;
            Time.timeScale = 0;
        }


    }

    void AccelerationCheck()
    {
        if (isAccelerating && direction > 0)
        {
            accelerationPower += Time.deltaTime*2f;
            if (accelerationPower >= 45)
            {
                accelerationPower = 45;
                return;
                
            }
        }
        else if (isAccelerating && direction < 0)
        {
            accelerationPower += Time.deltaTime*1.5f;
            if (accelerationPower >= 30)
            {
                accelerationPower = 30;
                return;
                
            }
        }
        else if (!isAccelerating && accelerationPower > 15)
        {
            accelerationPower -= Time.deltaTime*5f;
            audioSource.Pause ();
        }
        else
        {
            accelerationPower = 15;
        }
    }

    void BrakeCheck()
    {
        if(isBraking && accelerationPower > 15)
        {
            accelerationPower -= Time.deltaTime*7.5f;
        }
        else
        {
            return;
        }
    }

    void DriftCheck()
    {
        if(isDrifting)
        {
            steeringPower = 2f;
            accelerationPower -= Time.deltaTime*1.5f;
        }
        else
        {
            steeringPower = 1.5f;
            isDrifting = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Collider2D>().tag == "PickupPoint" && deliveries <= 0)
        {
            if (!hasDelivery)
            {
                PlaySound(baggrab);
            }
            countownStarted = true;
            hasDelivery = true;
            hasDeliveryText.text = "You have a delivery!";
        }
        else if (col.GetComponent<Collider2D>().tag == "PickupPoint" && deliveries > 0)
        {
            if (!hasDelivery)
            {
                PlaySound(baggrab);
                deliveryTime += 2.5f;
            }
            hasDelivery = true;
            hasDeliveryText.text = "You have a delivery!";
        }

        if (col.GetComponent<Collider2D>().tag == "DropoffPoint" && hasDelivery)
        {
            hasDelivery = false;
            hasDeliveryText.text = "Pick up the next delivery!";
            deliveries++;
            UpdateDeliveries();
            deliveryTime += 10;
            NewRandomNumber();
            PlaySound(dropoff);
        }
    }
     void OnCollisionEnter2D(Collision2D other)
    {
       
        PlaySound(hurt);
    }

    void UpdateCountdown()
    {
        timerText.text = ($"Time remaining: {deliveryTime.ToString("F1")}.");
    }

    void UpdateDeliveries()
    {
        deliveryText.text = ($"Deliveries Completed: {deliveries.ToString("F1")}.");
    }

    public virtual void NewRandomNumber()
{
    randomNumber = Random.Range(0, 5);
    if (randomNumber == lastNumber)
    {
        randomNumber = Random.Range(0, 5);
    }
    lastNumber = randomNumber;
}
public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
