using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public List<GameObject> buttons; // массив кнопок
    public float distanceFromCamera = 2f; // Дистанция взаимодействия

    public FirstPersonMovement movementScript; // Скрипт движение

    public FirstPersonLook lookScript; // Скрипт камеры

    public GameObject person; // Объект игрока

    const int SIZE = 5;
    const int START = 0;
    const int END = 24;

    private System.Random rand = new System.Random();

    private List<int> path;

    private int indexPath;

    private int buttonNumber;

    private Color blue = new Color(0.678f, 0.847f, 0.902f);

    private Color green = new Color(0f, 1f, 0f);
    private Color initialColor;

    private Rigidbody rb;

    void OnEnable()
    {
        DisableChildrenMeshRenderers();
        GetComponent<MeshRenderer>().enabled = false;

        rb = person.GetComponent<Rigidbody>();

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.isKinematic = true;
        rb.useGravity = false;

        movementScript.enabled = false;
        lookScript.enabled = false;
        initialColor = buttons[0].GetComponent<Renderer>().material.color;
        buttons[0].GetComponent<Renderer>().material.color = green;
        buttons[buttons.Count - 1].GetComponent<Renderer>().material.color = Color.red;
        buttonNumber = 0;

        path = FindRandomPath();
        indexPath = 1;
        buttons[path[indexPath]].GetComponent<Renderer>().material.color = blue;


        MoveObjectInFrontOfCamera();
    }

    void OnDisable()
    {
        restoreColors();
        DisableChildrenMeshRenderers();
        GetComponent<MeshRenderer>().enabled = false;

        rb.isKinematic = false;
        rb.useGravity = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        movementScript.enabled = true;
        lookScript.enabled = true;
    }

    void Update()
    {

        int newOne = -1;

        if (Input.GetKeyDown(KeyCode.W))
        {
            newOne = buttonNumber - 5;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            newOne = buttonNumber + 5;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            newOne = buttonNumber - 1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            newOne = buttonNumber + 1;
        }

        if (newOne == path[indexPath])
        {
            buttons[path[indexPath]].GetComponent<Renderer>().material.color = green;

            if (indexPath == path.Count - 1)
            {
                enabled = false;
                return;
            }

            indexPath++;

            if (indexPath != path.Count - 1)
            {
                buttons[path[indexPath]].GetComponent<Renderer>().material.color = blue;
            }

            buttonNumber = newOne;
        }
    }

    void restoreColors()
    {
        foreach (GameObject b in buttons)
        {
            b.GetComponent<Renderer>().material.color = initialColor;
        }
    }
    
    // Выключить MeshRenderer у всех дочерних объектов
    void DisableChildrenMeshRenderers()
    {
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer renderer in renderers)
        {
            renderer.enabled = false;
        }
    }

    // Включить MeshRenderer у всех дочерних объектов
    void EnableChildrenMeshRenderers()
    {
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer renderer in renderers)
        {
            renderer.enabled = true;
        }
    }

    List<int> GetNeighbors(int cell, HashSet<int> visited)
    {
        int x = cell % SIZE;
        int y = cell / SIZE;
        var neighbors = new List<int>();

        var directions = new (int dx, int dy)[]
        {
            (-1, 0), (1, 0), (0, -1), (0, 1)
        };

        foreach (var (dx, dy) in directions)
        {
            int nx = x + dx;
            int ny = y + dy;
            if (nx >= 0 && nx < SIZE && ny >= 0 && ny < SIZE)
            {
                int neighbor = ny * SIZE + nx;
                if (!visited.Contains(neighbor))
                    neighbors.Add(neighbor);
            }
        }

        // Перемешиваем соседей для случайности
        for (int i = neighbors.Count - 1; i > 0; i--)
        {
            int j = rand.Next(i + 1);
            (neighbors[i], neighbors[j]) = (neighbors[j], neighbors[i]);
        }

        return neighbors;
    }
    
    List<int> FindRandomPath()
    {
        var stack = new Stack<int>();
        var visited = new HashSet<int>();
        var path = new List<int>();

        stack.Push(START);
        visited.Add(START);
        path.Add(START);

        while (stack.Count > 0)
        {
            int current = stack.Peek();
            if (current == END)
                return new List<int>(path);

            var neighbors = GetNeighbors(current, visited);
            if (neighbors.Count > 0)
            {
                int nextCell = neighbors[0];
                stack.Push(nextCell);
                visited.Add(nextCell);
                path.Add(nextCell);
            }
            else
            {
                stack.Pop();
                path.RemoveAt(path.Count - 1);
            }
        }

        return new List<int>();
    }


    void MoveObjectInFrontOfCamera()
    {
        // Получаем центр экрана
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, distanceFromCamera);

        // Переводим экранные координаты в мировые
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenCenter);

        // Перемещаем объект
        transform.position = worldPosition;
        transform.rotation = Camera.main.transform.rotation;
        transform.rotation *= Quaternion.Euler(0, 180, 0);

        GetComponent<MeshRenderer>().enabled = true;
        EnableChildrenMeshRenderers();
    }


}
