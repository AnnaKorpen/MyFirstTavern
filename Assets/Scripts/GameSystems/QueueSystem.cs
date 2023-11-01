using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QueueSystem : MonoBehaviour
{
    // Creates a queue of customers
    public static QueueSystem Instance { get; private set; }

    [SerializeField] private Transform queueStartPosition;
    [SerializeField] private Transform customerPrefab;
    [SerializeField] private Transform customerIstantiatePosition;


    private List<Vector3> waitingQueuePositionList;
    private List<Customer> customerList;
    private int queueLenght = 5;
    private float customerAppearTimer = 3f;
    private float customerAppearTimeMin = 2f;
    private float customerAppearTimeMax = 7f;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        InstantiateQueue();
    }

    private void InstantiateQueue()
    {
        // Starts a new queue
        waitingQueuePositionList = new List<Vector3>();
        Vector3 firstPosition = queueStartPosition.position;
        float positionSize = 1.5f;
        for (int i = 0; i < queueLenght; i++)
        {
            waitingQueuePositionList.Add(firstPosition + new Vector3(0, 0, -1) * i * positionSize);
        }

        customerList = new List<Customer>();
    }

    public bool CanAddCustomer()
    {
        return customerList.Count < waitingQueuePositionList.Count;
    }

    public void AddCustomer()
    {
        // Initiate a new customer
        Transform customerTransform = Instantiate(customerPrefab);
        customerTransform.position = customerIstantiatePosition.position;
        Customer customer = customerTransform.gameObject.GetComponent<Customer>();
        customerList.Add(customer);
        customer.MoveTo(waitingQueuePositionList[customerList.IndexOf(customer)]);
    }

    public Vector3 GetCustomerIstantiatePosition()
    {
        return customerIstantiatePosition.position;
    }

    public Customer GetFirstCustomer()
    {
        //Check if there is a customer in queue
        if(customerList.Count > 0 && customerList[0].IsArrived())
        {
            Customer firstCustomer = customerList[0];
            customerList.RemoveAt(0);
            RelocateAllcustomers();
            return firstCustomer;
        }
        else
        {
            return null;
        }
    }

    private void RelocateAllcustomers()
    {
        // Changes all customers position after one is left the queue
        for (int i = 0; i < customerList.Count; i++)
        {
            customerList[i].MoveTo(waitingQueuePositionList[i]);
        }
    }

    private void Update()
    {
        if (CanAddCustomer())
        {
            customerAppearTimer -= Time.deltaTime;
            if (customerAppearTimer < 0)
            {
                AddCustomer();
                customerAppearTimer = UnityEngine.Random.Range(customerAppearTimeMin, customerAppearTimeMax);
            }
        }
    }
}
