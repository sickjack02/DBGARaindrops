using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class DropSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] DropPrefabs;
    [SerializeField] private Wave[] waves;

    public Dictionary<int, GameObject> RaindropsMap = new Dictionary<int, GameObject>();

    public GameManager GameManager;

    //limit of the screen for the random spawn
    private int xBoundMin = 120;
    private int xBoundMax = 1700;

    //references to the instaced operation
    RandomOperationGenerator operationList;
    private string operation;
    private int operationIndex;
    private List<string> operationResults = new List<string>();

    public List<string> GetOperationResults { get { return operationResults; } }

    private bool hasSpowned = true;

    public bool GetHasSpawnwd { get { return hasSpowned; } }

    int n = 0;

    float currentTime;

    float elapsedTime;
    int minutes;
    int lastMIn = 0;
    int waveIndex = 0;

    public List<GameObject> RaindropInScene = new List<GameObject>();

    public delegate void ScoreMetods(int num);
    public static event ScoreMetods UpdateScore;


    private void Start()
    {
        operationList = new RandomOperationGenerator();
        transform.position = new Vector3(960, 1170, 0);
        GameManager = FindObjectOfType<GameManager>();

        currentTime = 0;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        minutes = Mathf.FloorToInt(elapsedTime / 60);


        int wavesLenght = waves.Length;

        if (lastMIn != minutes)
        {
            lastMIn = minutes;
            if (waveIndex < wavesLenght - 1) waveIndex++;
        }

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            Spawnraindrop();
        }
    }

    //everytime I want to spawn a new raidrop
    void Spawnraindrop()
    {
        //call the methods for the new operation
        operationList.PerformRandomOperation();
        operation = operationList.GetLastOperation().operation;
        operationIndex = operationList.GetLastOperation().index;
        string[] splitString = operation.Split(new char[] { ' ', '=' });
        operationResults.Add(splitString[2]);

        //instantiate a new GO ant gave him a parent (in this way is shown in the canvas)
        GameObject raindrop = Instantiate(DropPrefabs[0], new Vector3(Random.Range(xBoundMin, xBoundMax), transform.position.y, 0), Quaternion.identity) as GameObject;
        raindrop.transform.SetParent(FindObjectOfType<DropSpawner>().transform, true);
        raindrop.name = "raindrop" + n;
        n++;

        RaindropInScene.Add(raindrop);

        //costruisco un Dictionary che ha come chiave il risultato e
        //come argomento l'istanza dell'oggetto creato 
        //in modo che posso distruggere tutte le goccie con quel risultato
        RaindropsMap.Add(operationIndex, raindrop);

        //take all of the new GO text fields and write inside of them
        TMP_Text[] Raindrop_Texts;
        Raindrop_Texts = raindrop.GetComponentsInChildren<TMP_Text>();
        Raindrop_Texts[0].text = splitString[0];
        Raindrop_Texts[1].text = splitString[1];

        Debug.Log("risultato indice " + operationIndex + ": " + splitString[2]);

        currentTime = waves[waveIndex].delayTime;
    }

    public void checkInput(string answer)
    {
        int indexToRemove = 0;

        //check if in the list of results there is the input answer 
        if (operationResults.Find(result => result == answer) == answer)
        {
            //int foundIdx = operationResults.IndexOf(answer);

            //check if the answer is present in any of the GO in the scene
            for (int x = 0; x < RaindropInScene.Count; x++)
            {
                //check if the gameobject is present
                if (RaindropInScene[x] != null)
                {
                    //take the index of the operation based on the GO name (the name is build like: raindrop[index of the corrispondin operation])
                    indexToRemove = int.Parse(RaindropInScene[x].name.Substring(8));
                }

                if (operationResults[indexToRemove] != null && operationResults[indexToRemove] == answer)
                {
                    //if there is destroy the GO and remove his reference on the dictionary
                    Destroy(RaindropsMap[indexToRemove]);
                    RaindropsMap.Remove(indexToRemove);
                    if (UpdateScore != null) UpdateScore(100);
                }
            }

            //put to null the result that match the answer
            for (int i = 0; i < operationResults.Count; i++)
            {
                if (operationResults[i] == answer)
                {
                    operationResults[i] = null;
                }
            }
        }
        else
        {

            if (UpdateScore != null) UpdateScore(-50);
            Debug.Log("risposta sbagliata");
        }
    }
}

//in case of different waves
[System.Serializable]
public class Wave
{
    //time for the drops to spawn
    public float delayTime;
}
