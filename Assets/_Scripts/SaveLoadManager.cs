using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using TMPro;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance;

    [SerializeField] GameObject errorScreen;
    [SerializeField] GameObject startScreen;
    [SerializeField] GameObject loadButton;
    [SerializeField] TextMeshProUGUI errorText;

    readonly string filePath = Application.dataPath + "/SaveInfo/OPCG_Data.csv";// Path to exel sheet
    private string rawExelSheetData;                                            // Raw data in a long string
    private string[,] exelSheet = new string[10, 10];                           // Data after initilization
    bool _saveExists;                                                           // If data in path was found

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);

        Instance = this;
    }

    private void Start()
    {
        TryInitDataFromFile();
    }

    //connected to UI button
    public void SaveData()
    {
        // Getting current cards in player hand
        List<string> currentData = Hand.instance.GetHandData();

        // Openinng streamWriter to write to file
        StreamWriter writer = new StreamWriter(filePath);

        // Going over Data and writing to file
        for (int i = 0; i < currentData.Count; i++)
            writer.Write(currentData[i] + ",");

        // Saving and closing our file
        writer.Close();
    }
    public void LoadData()
    {
        if (!_saveExists)
            return;

        List<string> nameList = new List<string>();

        for (int i = 0; i < exelSheet.Length; i++)
        {
            //first number is rows, second is collumn (use name row to load data into hand)
            if (!string.IsNullOrEmpty(exelSheet[0, i]))
                nameList.Add(exelSheet[0, i]);
            else
                break;
        }

        if (nameList.Count > 0)
            Hand.instance.LoadHand(nameList);

        startScreen.SetActive(false);
    }
    public void LoadScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    /// <summary>
    /// Take the CSV file, divide the columns and rows into string[,] just like excel
    /// </summary>
    private void TryInitDataFromFile()
    {
        // Check if file path exist
        if (!DoesDataFileExists(filePath)) { return; }

        // Reads all data from file
        rawExelSheetData = System.IO.File.ReadAllText(filePath);

        // Acts on read Data
        if (!String.IsNullOrWhiteSpace(rawExelSheetData))
            SaveHasData();// not decoupled just seperated for cleaner code
        else
            SaveDoesntHaveData();
    }
    private void SaveHasData()
    {
        // CSV uses Enter between their rows so we split the data
        string[] _rows = rawExelSheetData.Split("\n"[0]);//Enter as code
        for (int i = 0; i < _rows.Length; i++)
        {
            //CSV uses comma between their cells in each row so we split the data more
            string[] _colums = _rows[i].Split(',');

            for (int x = 0; x < _colums.Length; x++)
            {
                exelSheet[i, x] = _colums[x];
            }
        }

        // Mark as save was found and has Data
        _saveExists = true;
    }
    private void SaveDoesntHaveData()
    {
        loadButton.SetActive(false);
        Debug.Log("Save is Empty");
    }
    private bool DoesDataFileExists(string FilePath)
    {
        if (System.IO.File.Exists(FilePath))
        {
            try
            {
                rawExelSheetData = System.IO.File.ReadAllText(FilePath);
            }
            catch (Exception)
            {
                errorScreen.SetActive(true);
                errorText.text = $"Please close file at: {FilePath}";
                print($"Please close file at: {FilePath}");
                return false;
            }
        }
        else
        {
            errorScreen.SetActive(true);
            errorText.text = $"File Not Found! \nFile shoud be in: {FilePath}";
            print($"File Not Found! \nFile shoud be in: {FilePath}");
            return false;
        }

        return true;
    }

}