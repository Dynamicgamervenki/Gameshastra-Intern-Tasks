using Unity.VisualScripting;
using UnityEngine;

public class FloorSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject FloorPrefab;
    [SerializeField]
    private GameObject CurrentFloor;
    [SerializeField]
    public GameObject[] MoveablePlatforms;

    int totalfloors = 1;

    private void SpawnFloor()
    {
        if (FloorPrefab == null)
            return;

        float floorLength = CurrentFloor.GetComponent<Renderer>().bounds.size.z;
        Vector3 spawnPos = CurrentFloor.transform.position + new Vector3(0, 0, floorLength);

        GameObject SpawnedFloor = Instantiate(FloorPrefab);
        CurrentFloor = SpawnedFloor;
        SpawnedFloor.transform.position = spawnPos;
        totalfloors++;

        SpawnBlocks();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
            SpawnFloor();
    }

    private void SpawnBlocks()
    {
        float floorLength = CurrentFloor.GetComponent<Renderer>().bounds.size.z;
        float floorheight = CurrentFloor.GetComponent<Renderer>().bounds.size.y;
        float floorwidth = CurrentFloor.GetComponent<Renderer>().bounds.size.x;   

        int random = Random.Range(0, 21);
        for(int i = 0;i < 20;i++)
        {
            GameObject SpawnBlock = Instantiate(MoveablePlatforms[Random.Range(0, MoveablePlatforms.Length)]);
            //SpawnBlock.transform.position = CurrentFloor.transform.position + new Vector3(0, floorheight, i * Random.Range(-15,30));
            //SpawnBlock.transform.position += new Vector3(Random.Range(-5,floorwidth),0,0); 
            SpawnBlock.transform.position = new Vector3(Random.Range(-5, floorwidth-10), floorheight,Random.Range(100*(totalfloors-1),(totalfloors*floorLength) - 50));
            MovablePlatform platform = SpawnBlock.GetComponentInChildren<MovablePlatform>();
            platform.Init(Random.value > 0.5f, Random.value > 0.5f, (Axis)Random.Range(0, 3),Random.Range(-1,10),Random.Range(0.5f,1.5f));
        }
    }

}
