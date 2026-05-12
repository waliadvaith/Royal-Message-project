using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillTree : MonoBehaviour
{
    // don't forget to push all of the work you have done


    // prefabs for both enemies
    public PrefabAssetType Barbarian;
    public PrefabAssetType Musketeer;
    // components for both character movement and ai movement scripts
    public CharacterMovement CharacterMovementScript;
    public EnemyAI EnemyAIScript;
    public MoralityBar MoralityBarScript;
    public SwordScript SwordScriptScript;
    public CrossbowScript CrossbowScriptScript;
    public Health HealthScript;
    public float Damage;
    public float Health;

    // get the damage from the sword script and crossbow



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // get components when starting
        CharacterMovement characterMovement = GetComponent<CharacterMovement>();
        EnemyAI enemyAI = GetComponent<EnemyAI>();
        MoralityBar bar = GetComponent<MoralityBar>();
        SwordScript script = GetComponentInParent<SwordScript>();
        CrossbowScript crossbowScript = GetComponentInParent<CrossbowScript>();
        HealthPot healthPot = GetComponent<HealthPot>();

        // get health from the health script and get damage from the crossbow and sword script
        Health = HealthScript.currentHealth + Damage;
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        // increase players damage when destroy a enemny
        

        // increases players health based on morality but cannot go above 100 health
        if (MoralityBarScript)
        {
            MoralityBar script = GetComponentInParent<MoralityBar>();
        }

        // decreases players health based on morality but cannot go below 100 health

    }

}
