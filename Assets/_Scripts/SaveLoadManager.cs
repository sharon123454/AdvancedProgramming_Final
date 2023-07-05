using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance;

    [SerializeField] GameObject errorScreen;
    [SerializeField] TextMeshProUGUI errorText;

    readonly string filePath = Application.dataPath + "/SaveInfo/OPCG_Data.csv";// Path to exel sheet
    private string rawExelSheetData;                                            // Raw data in a long string
    private string[,] exelSheet = new string[100, 100];                         // Data after initilization
    System.Random random = new System.Random();
    bool _saveExists;                                                           // If data in path was found

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);

        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        TryInitDataFromFile();

        foreach (var idk in exelSheet)
            if (!String.IsNullOrWhiteSpace(idk))
                print(idk);
    }

    /// <summary>
    /// Take the CSV file, divide the columns and rows into string[,] just like excel
    /// </summary>
    private void TryInitDataFromFile()
    {
        //Check if file path exist
        if (!DoesDataFileExists(filePath)) { return; }

        //Reads all data from file
        rawExelSheetData = System.IO.File.ReadAllText(filePath);
        if (!String.IsNullOrWhiteSpace(rawExelSheetData))
        {
            //CSV uses enter between their rows
            string[] _rows = rawExelSheetData.Split("\n"[0]);
            for (int i = 0; i < _rows.Length; i++)
            {
                //CSV uses comma between their cells in rows
                string[] _colums = _rows[i].Split(',');
                for (int x = 0; x < _colums.Length; x++)
                {
                    exelSheet[i, x] = _colums[x];
                    exelSheet[i, x] = exelSheet[i, x].Replace('^', ',');
                }
            }

            _saveExists = true;
        }
        else
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