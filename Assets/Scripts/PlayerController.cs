using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public Material vermelho;
    public Material azul;

    public GameObject cuboPrefab;

    private static int playerIdAvailable;
    [SyncVar] private int playerId;

    private void Start()
    {
        //diferencia as cores
        Material material = null;

        if (isServer && isLocalPlayer)
        {
            material = vermelho;
        }
        else if (isServer && !isLocalPlayer)
        {
            material = azul;
        }
        else if (!isServer && isLocalPlayer)
        {
            material = azul;
        }
        else if (!isServer && !isLocalPlayer)
        {
            material = vermelho;
        }

        var meshRenderer = GetComponent<MeshRenderer>();

        meshRenderer.material = material;

        if (isServer)
        {
            if (isLocalPlayer)
            {
                playerIdAvailable = 0;
            }
            playerIdAvailable++;
            playerId = playerIdAvailable;
        }
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");

            transform.Translate(Vector3.left * (v * Time.deltaTime * 2));
            transform.Rotate(Vector3.up * (h * Time.deltaTime * 100));

            if (Input.GetKeyDown(KeyCode.Space))
            {
                CmdSpawnarCubo();
            }
        }
    }

    [Command]
    private void CmdSpawnarCubo()
    {
        var cubo = Instantiate(cuboPrefab);

        cubo.transform.position = transform.position + (transform.forward * 2);
        cubo.transform.rotation = transform.rotation;

        //cubo.GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().material;

        cubo.GetComponent<Cubo>().cuboId = playerId;

        NetworkServer.Spawn(cubo);
    }
}