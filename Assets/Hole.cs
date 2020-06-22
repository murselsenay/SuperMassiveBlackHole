using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    //editörde görüntülemek istenilen private değişkenleri serialized tanımlıyoruz.
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] MeshCollider meshCol;

    [SerializeField] float radius;
    [SerializeField] Transform hole;

    [SerializeField] float speed;

    Mesh mesh;
    List<int> holeVertices = new List<int>();
    List<Vector3> offsets = new List<Vector3>();
    int holeVerticesCount;

    float x, y;
    Vector3 touchPoint;

    public static Vector3 positions;
    public GameObject cubePrefab;
    void Start()
    {
        mesh = meshFilter.mesh;
        findHoleVertices();
       
    }

    // Update is called once per frame
    void Update()
    {
        positions = hole.transform.position;
        hole.transform.position = new Vector3(Mathf.Clamp(hole.transform.position.x, -1f, 1f), 0, Mathf.Clamp(hole.transform.position.z, -2f, 2f));//limitler
        //ekrana tıklanma olayı
        if (Input.GetMouseButton(0))
        {
                MoveHole();
            
        }
        //deliğin ortasındaki gameobject'in hareketi bittiğinde deliğin köşelerini tekrar kontrol ediyoruz.
        UpdateVerticesPosition();


    }
    void MoveHole()//deliğin ortasına eklediğimiz gameobject kontrolleri
    {
        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");

        touchPoint = Vector3.Lerp(hole.position, hole.position + new Vector3(x, 0f, y), speed * Time.deltaTime);

        hole.position = touchPoint;
    }

    void UpdateVerticesPosition()
    {
        Vector3[] vertices = mesh.vertices;
        for (int i = 0; i < holeVerticesCount; i++)
        {
            vertices[holeVertices[i]] = hole.position + offsets[i];//offset ile her köşegenin kenara olan offset değerini deliğin konumu ile birlikte ayrı ayrı tutuyoruz.
        }

        mesh.vertices = vertices;
        meshFilter.mesh = mesh;
        meshCol.sharedMesh = mesh;
    }
    void findHoleVertices()
    {
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            float distance = Vector3.Distance(hole.position, mesh.vertices[i]);//deliğin ortasından mesh'in kenarlarına olan uzaklıkları değişkende tutuyoruz. 
                                                                               //bunu da updateverticesposition fonksiyonunda bulduğumuz vertices sayısı kadar yapıyoruz ki değişiken etrafındaki her köşeden kenarlara olan uzaklığı bulmak için.
            if (distance < radius)
            {
                holeVertices.Add(i);
                offsets.Add(mesh.vertices[i] - hole.position);//deliğin radiusunu kenara olan uzaklıkla karşılaştırıp, kenarın deliğin her kenarına olan uzaklığını yani ofset değerini offsets listesine atıyoruz. 
            }
        }
        holeVerticesCount = holeVertices.Count;
    }
    public void CreateCube()
    {
        Instantiate(cubePrefab, new Vector3(Random.Range(-1.5f, 2), 1, Random.Range(0, 2)), Quaternion.identity);
    }

}


