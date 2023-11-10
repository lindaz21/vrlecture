using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour
{
    public List<GameObject> environmentPrefabs = new List<GameObject>();

    private List<GameObject> instances = new List<GameObject>();

    public List<Collider> restrictedBounds = new List<Collider>();

    public int numObjects = 30;

    public Vector3 generatorBoundsMin = new Vector3(-30, 0, -30);

    public Vector3 generatorBoundsMax = new Vector3(30, 0, 30);

    public bool reset = false;

    // Start is called before the first frame update
    void Start()
    {
        // Your code for Exercise 1.1 part 1.) here
        GenerateEnvironment();
    }

    // Update is called once per frame
    void Update()
    {
        // Your code for Exercise 1.1 part 3.) here
        if (reset == true)
        {
            for (int i = 0; i < numObjects; i++)
                Destroy(instances[i]);
            reset = false;
            instances.Clear();
            GenerateEnvironment();
        }
    }

    void ClearEnvironment()
    {
        // Your code for Exercise 1.1 part 3.) here
    }

    void GenerateEnvironment()
    {
        // Your code for Exercise 1.1 part 1.) here
        StartCoroutine(ResolveCollisions());

        for (int i = 0; i < numObjects; i++) 
        {
            GameObject randomPrefab = environmentPrefabs[Random.Range (0, environmentPrefabs.Count)];
            float randomX = Random.Range (generatorBoundsMin.x, generatorBoundsMax.x);
            float randomZ =  Random.Range (generatorBoundsMin.z, generatorBoundsMax.z);
            int randomRotY = Random.Range (0, 359);
            GameObject newInstance = Instantiate(randomPrefab, new Vector3(randomX, 0, randomZ), Quaternion.Euler(0, randomRotY, 0));
            newInstance.transform.parent = this.gameObject.transform;
            instances.Add(newInstance);

        }
        
    }

    IEnumerator ResolveCollisions()
    {
        yield return new WaitForSeconds(2);
        bool resolveAgain = false;
        var house_collider = new Collider();
        var gameobj_collider = new Collider();
        // Your code for Exercise 1.1 part 2.) here

        if (restrictedBounds[0] != null)
        {
            house_collider = restrictedBounds[0];
        }
        for (int i = 0; i < numObjects; i++)
        {
            if (instances[i] != null)
            {
                gameobj_collider = instances[i].GetComponent<Collider>();
            }
            
            if (house_collider.bounds.Intersects(gameobj_collider.bounds))
            {
                float randomX = Random.Range(generatorBoundsMin.x, generatorBoundsMax.x);
                float randomZ = Random.Range(generatorBoundsMin.z, generatorBoundsMax.z);
                int randomRotY = Random.Range(0, 359);
                // instances[i] = Instantiate(instances[i], new Vector3(randomX, 0, randomZ), Quaternion.Euler(0, randomRotY, 0));
                instances[i].transform.position = new Vector3(randomX, 0, randomZ);
                resolveAgain = true;
            }
        }

        if (resolveAgain)
            StartCoroutine(ResolveCollisions());
    }
}
