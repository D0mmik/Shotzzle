using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Leaderboard : MonoBehaviour
{
    const string connectionUri = "mongodb+srv://admin:admin@shotzzlecluster.kubxqxp.mongodb.net/?retryWrites=true&w=majority&appName=ShotzzleCluster";
    const string databaseName = "Leaderboard";
    [SerializeField] TMP_Text[] timeTexts;
    const string fieldNameTime = "time";
    const string fieldNameName = "name";
    IMongoDatabase database;
    List<BsonDocument>[] topTimesArrays = new List<BsonDocument>[3];
    [SerializeField] GameObject leaderboardParent;
    [SerializeField] GameObject timePrefab;
    [SerializeField] TMP_Text heading;
    const string playerID = "PlayerID";
    
    public static Leaderboard Instance { get; private set; }

    private void Awake() 
    { 
    
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    void Start()
    {
        var settings = MongoClientSettings.FromConnectionString(connectionUri);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        var client = new MongoClient(settings);
        database = client.GetDatabase(databaseName);
        
        GetBestTimes();
    }

    public void SetTime()
    {
        if (!PlayerPrefs.HasKey(playerID)) PlayerPrefs.SetInt(playerID, Random.Range(1, 1000000));
        
        BsonDocument document = new BsonDocument
        {
            { fieldNameTime, Timer.Instance.time },
            { fieldNameName, PlayerPrefs.GetString("PlayerName") ?? "errorNamePP"},
            { playerID, PlayerPrefs.GetInt(playerID) }
        };
        
        var collection = database.GetCollection<BsonDocument>("Time" + SceneManager.GetActiveScene().name);
        collection.InsertOne(document);
    }
    
    void GetBestTimes()
    {
        if (!PlayerPrefs.HasKey(playerID)) return;

        try
        {
            foreach (var text in timeTexts)
            {
                var collection = database.GetCollection<BsonDocument>(text.name);
                var filter = Builders<BsonDocument>.Filter.Eq(playerID, PlayerPrefs.GetInt(playerID));
                var sort = Builders<BsonDocument>.Sort.Ascending(fieldNameTime);
                var cursor = collection.Find(filter).Sort(sort).Limit(1);
                var documents = cursor.ToList();

                text.text = documents.Count > 0 ? DisplayTime(Convert.ToDouble(documents[0][fieldNameTime])) : null;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }

    public void GetLeaderboard()
    {
        try
        {
            int index = 0;
            foreach (var text in timeTexts)
            {
                var collection = database.GetCollection<BsonDocument>(text.name);
                var filter = new BsonDocument();
                var sort = Builders<BsonDocument>.Sort.Ascending(fieldNameTime);
                var cursor = collection.Find(filter).Sort(sort).Limit(10);
                var documents = cursor.ToList();

                topTimesArrays[index] = documents;
                index++; 
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }

        foreach (var times in topTimesArrays)
        {
            foreach (var time in times)
            {
                Debug.Log(time);
            }
        }
    }

    public void RenderTimes(int page)
    {
        try
        {
            heading.text = "LEADERBOARD LEVEL " + (page + 1);
            foreach (Transform child in leaderboardParent.transform)
                Destroy(child.gameObject);
        
            foreach (var time in topTimesArrays[page])
            {
                GameObject timeClone = Instantiate(timePrefab, leaderboardParent.transform);
            
                if (time.Contains(fieldNameName)) timeClone.GetComponent<TimePrefab>().playerName.text = time[fieldNameName].ToString();
                if (time.Contains(fieldNameTime)) timeClone.GetComponent<TimePrefab>().time.text = DisplayTime(Convert.ToDouble(time[fieldNameTime]));
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public void ButtonPage1()
    {
        RenderTimes(0);
    }
    
    public void ButtonPage2()
    {
        RenderTimes(1);
    }
    
    public void ButtonPage3()
    {
        RenderTimes(2);
    }
    
    public string DisplayTime(Double timeToDisplay)
    {
        var t0 = (int) timeToDisplay;
        var m = t0/60;
        var s = t0 - m*60;
        var ms = (int)( (timeToDisplay - t0)*100);

        return $"{m:00}:{s:00}:{ms:00}";
    }
}