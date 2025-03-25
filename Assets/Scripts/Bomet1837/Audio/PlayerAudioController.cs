using System.Collections;
using System.Collections.Generic;
using Mono.CompilerServices.SymbolWriter;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    private PlayerController playerController;

    private AudioSource [] 
    audioSourcesAll,
    footstepsIndoor,
    footstepsOutdoor,
    footstepsLab,
    footstepsGravel,

    jumps,
    landings,
    interactions;

    [Header("Material Settings")]
    public Material []
    matIndoor,
    matOutdoor,
    matLab,
    matGravel;

    private AudioSource [][] footsteps;
    private Material [][] materials;
    private AudioSource jumpMain;

    public float footstepCooldown = 0.5f;
    private float footstepTimer = 0f;




    void Awake()
    {
        playerController = GetComponent<PlayerController>();

        audioSourcesAll = FindObjectsOfType<AudioSource>();
        List<AudioSource> footstepsOutdoorList = new List<AudioSource>();
        List<AudioSource> footstepsIndoorList = new List<AudioSource>();
        List<AudioSource> footstepsLabList = new List<AudioSource>();
        List<AudioSource> footstepsGravelList = new List<AudioSource>();
        List<AudioSource> jumpList = new List<AudioSource>();
        List<AudioSource> landingList = new List<AudioSource>();
        List<AudioSource> interactionList = new List<AudioSource>();


        foreach (var asrc in audioSourcesAll)
        {
            switch (asrc)
            {
                case var _ when asrc.name.StartsWith("ip"):
                footstepsIndoorList.Add(asrc);
                break;
                
                case var _ when asrc.name.StartsWith("op"):
                footstepsOutdoorList.Add(asrc);
                break;

                case var _ when asrc.name.StartsWith("lab"):
                footstepsLabList.Add(asrc);
                break;

                case var _ when asrc.name.StartsWith("g"):
                footstepsGravelList.Add(asrc);
                break;

                case var _ when asrc.name.StartsWith("jump"):
                jumpList.Add(asrc);
                break;

                case var _ when asrc.name.StartsWith("land"):
                landingList.Add(asrc);
                break;

                case var _ when asrc.name.StartsWith("int"):
                interactionList.Add(asrc);
                break;
            }

            if (asrc.name.Equals("jumpwhoosh"))
            {
                jumpMain = asrc;
            }
        }

        footstepsOutdoor = footstepsOutdoorList.ToArray();
        footstepsIndoor = footstepsIndoorList.ToArray();
        footstepsLab = footstepsLabList.ToArray();
        footstepsGravel = footstepsGravelList.ToArray();
        jumps = jumpList.ToArray();
        landings = landingList.ToArray();
        interactions = interactionList.ToArray();

        footsteps = new AudioSource[][] {footstepsOutdoor, footstepsIndoor, footstepsLab, footstepsGravel};
        materials = new Material[][] {matOutdoor, matIndoor, matLab, matGravel};
    }

    public void Start()
    {
        Debug.Log("Player Audio Controller started with " + footsteps.Length + " materials and " + footsteps[0].Length + " footstep sounds.");   
    }

    public void Update()
    {  
        footstepTimer += Time.deltaTime;
        PlayFootstep();   
        foreach (var audio in audioSourcesAll)
        {
            if (audio.isPlaying)
            {
              //  Debug.Log("Played" + audio.name);
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("PuzzleBox"))
        {
            PlayLand();
        }
    } 

    public void PlayFootstep()
    {
        if (playerController._input.magnitude >= 0.1f && footstepTimer >= footstepCooldown)
        {
           // Debug.Log("Player is grounded and moving.");
            string matCurrent = GetMatName(playerController.currentMat.name);
            for (var i = 0; i < materials.Length; i++)
            {
                for (var j = 0; j < materials[i].Length; j++)
                {
                    string matName = GetMatName(materials[i][j].name);
                    if (matCurrent == matName)
                    {
                    //    Debug.Log("Playing footstep sound for material: " + materials[i][j].name);
                        footsteps[i][Random.Range(0, footsteps[i].Length)].Play();
                        footstepTimer = 0f;
                        return;
                    }
                }
            }
            Debug.LogWarning("No matching material found for currentMat: " + playerController.currentMat?.name);


        }
        else
        {
 //           Debug.Log("Player is not grounded or not moving.");
        }
    }

    public void PlayJump()
    {
        jumpMain.Play();
        jumps[Random.Range(0, jumps.Length)].Play();
    }

    public void PlayLand()
    {
        landings[Random.Range(0, landings.Length)].Play();
    }

    private string GetMatName(string matName)
    {
        const string suffix = " (Instance)";
        if (matName.EndsWith(suffix))
        {
            return matName.Substring(0, matName.Length - suffix.Length);
        }
        return matName;
    }



}
