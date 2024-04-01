using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System;

public class Leaderboard : MonoBehaviour
{
    const string connectionUri = "mongodb+srv://admin:admin@shotzzlecluster.kubxqxp.mongodb.net/?retryWrites=true&w=majority&appName=ShotzzleCluster";
    const string databaseName = "Leaderboard";
    const string collectionName = "Level1";

    void Start()
    {
        ConnectToMongoDB();
        Debug.Log("start");
    }

    void ConnectToMongoDB()
    {
        var settings = MongoClientSettings.FromConnectionString(connectionUri);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        var client = new MongoClient(settings);
        var database = client.GetDatabase(databaseName);
        var collection = database.GetCollection<BsonDocument>(collectionName);

        try
        {
            var filter = new BsonDocument();
            var documents = collection.Find(filter).ToList();
            Debug.Log(documents.Count);
            
            foreach (var document in documents)
            {
                Debug.Log(document);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }
}