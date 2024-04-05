using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomOperationGenerator : MonoBehaviour
{

    private List<string> operationsHistory = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.I))
        {
            PerformRandomOperation();
        }*/
    }

    //generate 2 positive rundom numbers and a random operation
    public void PerformRandomOperation()
    {
        int number1 = Random.Range(1, 10);
        int number2 = Random.Range(1, 10);

        string[] operations = { "+", "-", "×", "/" };
        string operationSymbol = operations[Random.Range(0, operations.Length)];

        //result must be positive and not 0
        while ((operationSymbol == "-" || operationSymbol == "/") && number2 > number1)
        {
            number1 = Random.Range(1, 10);
            number2 = Random.Range(1, 10);
        }

        int result = 0;
        switch (operationSymbol)
        {
            case "+":
                result = number1 + number2;
                break;
            case "-":
                result = number1 - number2;
                break;
            case "×":
                result = number1 * number2;
                break;
            case "/":
                result = number1 / number2;
                break;
        }

        //populate the list
        string operationString = $"{number1} {operationSymbol}{number2}={result}";
        operationsHistory.Add(operationString);
        //Debug.Log("Indice " + (operationsHistory.Count-1) + ": " + operationString);
    }

    //retrive the entire list
    public List<string> GetOperationsHistory()
    {
        return operationsHistory;
    }

    //get the last operation and the corrisponding index
    public Operation GetLastOperation()
    {
        Operation lastOperation = new Operation();
        if (operationsHistory.Count > 0)
        {
            lastOperation.operation = operationsHistory[operationsHistory.Count - 1];
            lastOperation.index = operationsHistory.Count - 1;
        }
        else
        {
            lastOperation.operation = null; //list is empty
            lastOperation.index = -1;
        }
        return lastOperation;
    }

    //remove from the list the operation at given index
    public void RemoveOperation(int index)
    {
        if (index >= 0 && index < operationsHistory.Count)
        {
            operationsHistory.RemoveAt(index);
        }
        else
        {
            Debug.LogWarning("Indice non valido");
        }
    }
}

//I've created a class so I can retrive in one call the operation
//and the corrisponding index in the list
public class Operation
{
    public string operation;
    public int index;
}
