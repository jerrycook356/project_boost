using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class Oscilator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector = new Vector3(10f,10f,10f); //set initial movement so can tell it is on game object
    [SerializeField] float period = 2f; //the amount of time to complete one cycle of movement


   
    float movementFactor;  //0 for not moved, 1 for fully moved

    private Vector3 startingPosition;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //set movement factor automatically
        if(period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; //grows continually from 0

        const float tau = Mathf.PI * 2;   //about 6.28 (2pi) complete rotation around circle
        float rawSinWav = Mathf.Sin(cycles * tau);

        //divide cycle to get between -0.5 and 0.5, add 0.5 to get between 0 and 1
        movementFactor = (rawSinWav / 2f) + 0.5f; 

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
