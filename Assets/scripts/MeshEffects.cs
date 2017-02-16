﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MeshEffects : MonoBehaviour {

	public string animMode;
	public List<string> args;
	GenerateMesh meshGenerator;

	// Use this for initialization
	void Start () {
		meshGenerator = this.GetComponent<GenerateMesh>();
	}

	//wave vert groups by the first vert's normal for the sake of simplicity
	void animateWave(float speed, float numWaves, float amplitude) {
		int count = 0; //only increase count after each vert group, rather than each individual vert
		foreach (Dictionary<Quaternion, VertexData> dict in meshGenerator.vertDict.verts.Values) {
			VertexData[] curVerts = dict.Values.ToArray();
			for (int i = 0; i < curVerts.Length; ++i) {
				meshGenerator.vertices[curVerts[i].verticesIndex] = meshGenerator.vertices[curVerts[i].verticesIndex] + meshGenerator.normals[curVerts[0].verticesIndex].normalized *
				(Mathf.Sin(speed * Time.time + (numWaves * 6.28f / (float)meshGenerator.vertDict.verts.Values.Count) * count) / (2000 / amplitude));
			}
			++count;
		}	
}
	
	// Update is called once per frame
	void Update () {
		if (animMode == "wave") {
			animateWave(float.Parse(args[0]), float.Parse(args[1]), float.Parse(args[2]));
		}
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		mesh.vertices = meshGenerator.vertices.ToArray();
		this.GetComponent<MeshCollider>().sharedMesh = mesh;

	}
}
