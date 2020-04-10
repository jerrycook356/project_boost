using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlDisplay : MonoBehaviour
{

    public string controlString = "A = Left, D = Right, Space = Thrust, Escape = Quit ";
    GameObject controlText;

    // Start is called before the first frame update
    void Start()
    {
        print("entering display start");
        StartCoroutine(DisplayControls(controlString, 3f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DisplayControls(string controls, float delay)
    {
        print("entering display controls");
        controlText.GetComponent<Text>().text = controls;
        controlText.SetActive(true);
        yield return new WaitForSeconds(delay);
        controlText.SetActive(false);
        print("exit display controls");
    }
}
