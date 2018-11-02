
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] GameObject level;
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] levelObjects;
    [SerializeField] Image[] levelObjectsImages;
    [SerializeField] float levelRadiusOffset = 3f;
    [SerializeField] float minDistanceBetweenObjects = 6f;

    private float xPlaneRadius;
    private float zPlaneRadius;
    public static GameObject objectToActivate;
    public static int objectToActivateNumber;
    public static GameManagerScript instance;

    void Awake()
    {
        CreateSingletonPattern();
        RandomizeArray(levelObjects);
        AssignSprites(levelObjectsImages, levelObjects);
    }

    void Start()
    {
        objectToActivateNumber = 0;
        objectToActivate = levelObjects[objectToActivateNumber];
        TeleportPlayer();
        TeleportLevelObjects(levelObjects);
    }

    private void TeleportPlayer()
    {
        Renderer rend = level.GetComponent<Renderer>();
        xPlaneRadius = (rend.bounds.size.x / 2) - levelRadiusOffset;
        zPlaneRadius = (rend.bounds.size.z / 2) - levelRadiusOffset;

        player.transform.position = GenerateNewPos(player);
    }

    private void TeleportLevelObjects(GameObject[] objects)
    {
        foreach (var obj in objects)
        {
            Vector3 newPos = GenerateNewPos(obj);
            Collider[] collidersAtNewPos = Physics.OverlapSphere(newPos, minDistanceBetweenObjects);
            if (collidersAtNewPos.Length > 1)
            {
                TeleportLevelObjects(levelObjects);
                return;
            }
            else if (collidersAtNewPos.Length == 1)
            {
                obj.transform.position = new Vector3(newPos.x, newPos.y, newPos.z);
            }
            else
            {
                print("You messed up");
            }
        }
    }

    private Vector3 GenerateNewPos(GameObject obj)
    {
        return new Vector3(Random.Range(-xPlaneRadius, xPlaneRadius),
            obj.transform.position.y,
            Random.Range(-zPlaneRadius, zPlaneRadius));
    }

    private void AssignSprites(Image[] images, GameObject[] objects)
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].sprite = objects[i].GetComponent<Image>().sprite;
        }
    }

    public void NextObjectToActivate()
    {
        objectToActivateNumber++;
        print(objectToActivateNumber);
        if (objectToActivateNumber > levelObjects.Length - 1)
        {
            ResetScene();
        }
        else
        {
            objectToActivate = levelObjects[objectToActivateNumber];
        }
    }

    public void ResetObjectToActivate()
    {
        objectToActivateNumber = 0;
        objectToActivate = levelObjects[objectToActivateNumber];
        foreach (var obj in levelObjects)
        {
            obj.GetComponent<ILevelObject>().Lose();
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void CreateSingletonPattern()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
    }

    static void RandomizeArray(GameObject[] array)
    {
        for (var i = array.Length - 1; i > 0; i--)
        {
            var r = Random.Range(0, i);
            var tmp = array[i];
            array[i] = array[r];
            array[r] = tmp;
        }
    }
}
