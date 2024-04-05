using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ValidateInput : MonoBehaviour
{
    [SerializeField] DropSpawner DropSpawner;
    [SerializeField] TMP_InputField inputField;

    private string inputText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Validateinput(string input)
    {
        DropSpawner.checkInput(input);
        inputField.Select();
    }
}
