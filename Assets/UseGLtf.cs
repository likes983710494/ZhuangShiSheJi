using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Siccity.GLTFUtility;

public class UseGLtf : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		ImportGLTF("");

	}

    // Update is called once per frame
    void Update()
    {
        
    }

	void ImportGLTF(string filepath)
	{
		GameObject result = Importer.LoadFromFile(filepath);
	}
}
