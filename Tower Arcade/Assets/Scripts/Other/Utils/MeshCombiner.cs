using UnityEngine;

[RequireComponent (typeof(MeshFilter))]
[RequireComponent (typeof(MeshRenderer))]
public class MeshCombiner : MonoBehaviour
{
    private void Awake()
    {
        CombineInstance[] elements = new CombineInstance[transform.childCount];

        int index = 0;
        foreach(Transform child in gameObject.transform)
        {
            MeshFilter filter = child.GetComponent<MeshFilter>();
            elements[index].mesh = filter.sharedMesh;
            elements[index].transform = child.localToWorldMatrix;
            child.gameObject.SetActive(false);
            index++;
        }

        Mesh mesh = new Mesh();
        mesh.CombineMeshes(elements);
        transform.GetComponent<MeshFilter>().sharedMesh = mesh;
        transform.gameObject.SetActive(true);
    }
}
