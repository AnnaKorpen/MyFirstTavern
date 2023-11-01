using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ProducerSystem : MonoBehaviour
{
    // Class regulates the number of producers, initiates new producers according to LevelInfo or SaveFile
    public static ProducerSystem Instance { get; private set; }

    [SerializeField] private List<Transform> producerTransformList;
    [SerializeField] private Transform producerStartPosition;
    [SerializeField] private Transform coinSpentArea;


    private float producerSpacing = 5f;
    private int previousProducerNumber = 0;
    private int producersInAction = 0;

    private void Awake()
    {
        Instance = this;

    }

    public void SetProducerNumber(int number, int price)
    {
        // Check if a new level has a new producer
        if (number > previousProducerNumber)
        {
            // if has a new producer, initiates a new coinSpentArea to buy it
            InstantiateNewProducer(previousProducerNumber, price);
            previousProducerNumber = number;
        }
    }
    public void LoadProducersFromSaveFile(int producerNumber)
    {
        // Load the correct number of producers acoording to SaveFile
        for (int i = 0; i < producerNumber; i++)
        {
            Vector3 producerPosition = new Vector3(producerStartPosition.position.x + producerSpacing * i, producerStartPosition.position.y, producerStartPosition.position.z);
            Transform producer = Instantiate(producerTransformList[i]);
            producer.position = producerPosition;
        }
        previousProducerNumber = producerNumber;
    }

    private void InstantiateNewProducer(int futureProducerIndex, int price)
    {
        // Initiates a new coinSpentArea to buy a producer
        Vector3 futureProducerPosition = new Vector3(producerStartPosition.position.x + producerSpacing* futureProducerIndex, producerStartPosition.position.y, producerStartPosition.position.z);
        Transform area = Instantiate(coinSpentArea);
        area.position = futureProducerPosition;
        area.gameObject.GetComponent<CoinSpentArea>().SetParameters(price, producerTransformList[futureProducerIndex], futureProducerPosition);

    }

    public int GetProducersInAction()
    {
        return producersInAction;
    }

    public void SetProducer()
    {
        // Make a correct number of current producers to save this information in SaveFile
        producersInAction = previousProducerNumber;
        
        
    }
}
