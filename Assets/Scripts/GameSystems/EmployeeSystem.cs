using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeSystem : MonoBehaviour
{
    // Holds information about all employees,
    // creates a new CoinsSpentArea to buy a employee, when LevelInfo tells
    // Load employes from SaveFile
    public static EmployeeSystem Instance { get; private set; }

    [SerializeField] private Transform hostPlaceOfWork;
    [SerializeField] private Transform hostPlaceOfWorkPosition;
    [SerializeField] private Transform magicCleanerPosition;
    [SerializeField] private Transform coinSpentArea;
    [SerializeField] private List<Transform> employeesTransform;

    private List<bool> employees = new List<bool>() { false, false };
    private List<Transform> employeePlacesOfWork = new List<Transform>();
    private float hostPositionAdjuster = 2.5f;

    private void Awake()
    {
        Instance = this;
        InstantiateHostPlaceOfWork();
    }

    private void InstantiateHostPlaceOfWork()
    {
        // Creates a CounterDesk
        Transform placeOfWork = Instantiate(hostPlaceOfWork);
        placeOfWork.position = hostPlaceOfWorkPosition.position;
        employeePlacesOfWork.Add(placeOfWork);
    }

    public void InstantiateHost()
    {
        // Load Host from SaveFile
        if (!employees[0])
        {
            Transform host = Instantiate(employeesTransform[0]);
            host.position = new Vector3(hostPlaceOfWorkPosition.position.x, hostPlaceOfWorkPosition.position.y, hostPlaceOfWorkPosition.position.z + hostPositionAdjuster / 2);
        }
    }

    public void InstatiateMagicCleaner()
    {
        // Load MagicCleaner from SaveFile
        if (!employees[1])
        {
            Transform magicCleaner = Instantiate(employeesTransform[1]);
            magicCleaner.position = magicCleanerPosition.position;
        }
    }

    public void SetEmployeeNumber(int employeeNumber, int price)
    {
        // Initiate CoinsSpentArea according to LevelInfo
        if (employeeNumber == 1 && !employees[0])
        {
            // Initiate area to buy a host
            Transform area = Instantiate(coinSpentArea);
            area.position = new Vector3(hostPlaceOfWorkPosition.position.x + hostPositionAdjuster, hostPlaceOfWorkPosition.position.y, hostPlaceOfWorkPosition.position.z);
            Vector3 employeePosition = new Vector3(hostPlaceOfWorkPosition.position.x, hostPlaceOfWorkPosition.position.y, hostPlaceOfWorkPosition.position.z + hostPositionAdjuster / 2);
            area.gameObject.GetComponent<CoinSpentArea>().SetParameters(price, employeesTransform[0], employeePosition);

        }
        if (employeeNumber == 2 && !employees[1])
        {
            // Initiate area to buy a magic cleaner
            Transform area = Instantiate(coinSpentArea);
            area.position = magicCleanerPosition.position;
            area.gameObject.GetComponent<CoinSpentArea>().SetParameters(price, employeesTransform[1], magicCleanerPosition.position);
        }
    }

    public void SetEmployee(int employeeNumber)
    {
        // Get Information that Player baught an emplyee
        employees[employeeNumber] = true;

    }

    public Transform GetHostPlaceOfWork()
    {
        return employeePlacesOfWork[0];
    }

    public List<bool> GetEmployeesList()
    {
        // Return employee list to save
        return employees;
    }

}
