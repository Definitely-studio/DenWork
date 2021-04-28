using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshParticlesSystem : MonoBehaviour
{
	private int max_quad_amount = 15000;
    private Mesh mesh;

    private Vector3[] vertices;
    private Vector2[] uv;
    private int[] triangles;

    private int quadIndex;

    private void Awake() {
    	mesh = new Mesh();

    	vertices = new Vector3[4 * max_quad_amount];
    	uv = new Vector2[4 * max_quad_amount];
    	triangles = new int[6 * max_quad_amount];	
		
		AddQuad(new Vector3(-3,-3));
    	
    	mesh.vertices = vertices;
    	mesh.uv = uv;
    	mesh.triangles = triangles;

    	GetComponent<MeshFilter>().mesh = mesh;
	    	

    }

    public void SpawnShell(Vector3 position) {
        AddQuad(position);

    	mesh.vertices = vertices;
    	mesh.uv = uv;
    	mesh.triangles = triangles;

    	GetComponent<MeshFilter>().mesh = mesh;

    }


    public void AddQuad(Vector3 position) {
    	Debug.Log("ADDQUAD"+position);

    	//Relocate vertices
    	int vIndex = quadIndex * 4;
    	int vIndex0 = vIndex;
    	int vIndex1 = vIndex + 1;
    	int vIndex2 = vIndex + 2;
    	int vIndex3 = vIndex + 3;    

    	Vector3 quadSize = new Vector3(0.01f, 0.05f);
    	float rotation = 0f;
    	vertices[vIndex0] = position + Quaternion.Euler(0, 0, rotation - 180) * quadSize;	    	
    	vertices[vIndex1] = position + Quaternion.Euler(0, 0, rotation - 270) * quadSize;	    	
     	vertices[vIndex2] = position + Quaternion.Euler(0, 0, rotation - 0) * quadSize;	    	
     	vertices[vIndex3] = position + Quaternion.Euler(0, 0, rotation - 90) * quadSize;	
     	// UV

     	uv[vIndex0] = new Vector2(0, 0);
     	uv[vIndex1] = new Vector2(0, 1);
     	uv[vIndex2] = new Vector2(1, 1);
     	uv[vIndex3] = new Vector2(1, 0);     	



     	// Create triangles
     	int tIndex = quadIndex * 6;

     	triangles[tIndex + 0] = vIndex0;    	
     	triangles[tIndex + 1] = vIndex1;    	
     	triangles[tIndex + 2] = vIndex2;    	

     	triangles[tIndex + 3] = vIndex0;    	
     	triangles[tIndex + 4] = vIndex2;    	
     	triangles[tIndex + 5] = vIndex3;    

     	quadIndex++;	
     }




}
