using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Cubo : NetworkBehaviour
{
    public Material vermelho, azul;

    [SyncVar] public int cuboId;

    private void Start()
    {
        Material material = cuboId == 1 ? vermelho : azul;

        var meshRenderer = GetComponent<MeshRenderer>();

        meshRenderer.material = material;
    }

    private void Update()
    {
    }
}