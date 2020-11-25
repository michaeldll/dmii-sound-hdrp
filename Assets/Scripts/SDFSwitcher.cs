using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDFSwitcher : MonoBehaviour
{
    public List<Mesh> meshes;
    public MeshToSDF meshToSDF;
    public NoteAnalyzer noteAnalyzer;

    public void switchTo(int index){
        meshToSDF.mesh = meshes[index];
    }

    void Update(){
        switchTo(noteAnalyzer.currentNodeIndex);
    }
}
