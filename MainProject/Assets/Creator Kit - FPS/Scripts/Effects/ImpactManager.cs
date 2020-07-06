using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This handle impacts on object from the raycast of the weapon. It will create a pool of the prefabs for performance
/// optimisation.
/// </summary>
public class ImpactManager : MonoBehaviour
{
    [System.Serializable]
    public class ImpactSetting
    {
        public ParticleSystem ParticlePrefab;
        public AudioClip ImpactSound;
        public Material TargetMaterial;
    }

    static public ImpactManager Instance { get; protected set; }

    public ImpactSetting DefaultSettings;
    public ImpactSetting[] ImpactSettings;
    

    Dictionary<Material, ImpactSetting> m_SettingLookup = new Dictionary<Material,ImpactSetting>();

    Vector3 m_position;
    Vector3 m_normal; 
    Material m_material = null;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
       // PoolSystem.Instance.InitPool(DefaultSettings.ParticlePrefab, 3);
        foreach (var impactSettings in ImpactSettings)
        {
            PoolSystem.Instance.InitPool(impactSettings.ParticlePrefab, 1);
            m_SettingLookup.Add(impactSettings.TargetMaterial, impactSettings);
        }
    }
    public void ImpactData(Vector3 position, Vector3 normal, Material material = null) {
         m_position=position;
        print("ImpactData m_position" + m_position);
         m_normal=normal;
         m_material =material;
    }
    public Vector3 GetImpactPosition()
    {
        return m_position;
    }
    public Vector3 GetImpactNormal()
    {
        print("m_normal" + m_normal);
        return m_normal;
    }
    public void PlayImpact()
    {
        print("PlayImpact m_position" + m_position);
        ImpactSetting setting = null;
        if (m_material == null || !m_SettingLookup.TryGetValue(m_material, out setting))
        {
            setting = ImpactSettings[0];
        }
        
        var sys =  PoolSystem.Instance.GetInstance<ParticleSystem>(setting.ParticlePrefab);
        sys.gameObject.transform.position = m_position;
        sys.gameObject.transform.forward = m_normal;

        sys.gameObject.SetActive(true);
        sys.Play();

        var source = WorldAudioPool.GetWorldSFXSource();

        source.transform.position = m_position;
        source.pitch = Random.Range(0.8f, 1.1f);
        source.PlayOneShot(setting.ImpactSound);
    }
}
