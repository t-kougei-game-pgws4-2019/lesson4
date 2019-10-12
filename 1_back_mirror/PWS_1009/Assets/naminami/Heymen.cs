using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heymen : MonoBehaviour
{
	private Mesh mesh;
	public MeshRenderer meshRenderer;
	public Material material;

	// Start is called before the first frame update
	void Start()
    {
		gameObject.AddComponent<MeshFilter>();
		meshRenderer = gameObject.AddComponent<MeshRenderer>();
		meshRenderer.material = material;
		mesh = GetComponent<MeshFilter>().mesh;

		GetComponent<MeshFilter>().mesh = generateFineMesh(100, 100, 10f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	Mesh generateFineMesh(int nX, int nZ, float sizeX, float sizeZ)
	{
		int num_verticies = (nX + 1) * (nZ + 1);
		Vector3[] a_vertex = new Vector3[num_verticies];
		Vector3[] a_normal = new Vector3[num_verticies];
		Vector2[] a_uv = new Vector2[num_verticies];

		float dX = sizeX / (float)nX;
		float dZ = sizeX / (float)nZ;
		int idx = 0;
		float fz = -0.5f * sizeZ;
		for(int z = 0; z <= nZ; z++)
		{
			float v = (float)z / (float)nZ;
			float fx = -0.5f * sizeX;
			for(int x = 0; x <= nX; x++)
			{
				float u = (float)x / (float)nX;
				a_vertex[idx] = new Vector3(fx, 0f, fz);
				a_normal[idx] = new Vector3(0f, 1f, 0f);
				a_uv[idx] = new Vector2(u, v);
				idx++;
				fx += dX;
			}
			fz += dZ;
		}

		idx = 0;
		int[] indecies = new int[nX * nZ * 6];
		for(int z = 0; z < nZ; z++)
		{
			int index = z * (nX + 1);
			for(int x = 0; x < nX; x++)
			{
				indecies[idx + 0] = index;
				indecies[idx + 1] = index + nX + 1;
				indecies[idx + 2] = index + 1;
				indecies[idx + 3] = index + 1;
				indecies[idx + 4] = index + nX + 1;
				indecies[idx + 5] = index + nX+ 2;
				index++;
				idx += 6;
			}
		}

		Mesh mesh = new Mesh
		{
			vertices = a_vertex,
			normals = a_normal,
			uv = a_uv
		};
		mesh.SetIndices(indecies, MeshTopology.Triangles, 0);

		return mesh;
	}
}
