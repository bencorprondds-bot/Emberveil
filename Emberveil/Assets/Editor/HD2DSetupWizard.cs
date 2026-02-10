#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Editor wizard that sets up the HD-2D rendering pipeline for Emberveil.
/// Run via menu: Emberveil > Setup HD-2D Pipeline
///
/// Creates:
/// - URP 3D pipeline asset with HD-2D settings
/// - Post-processing profile (bloom, depth of field, vignette)
/// - Pixel-art material (point-filtered, unlit)
/// - Billboard sprite material (alpha cutout, point-filtered)
/// - Global post-processing volume prefab
/// </summary>
public class HD2DSetupWizard : EditorWindow
{
    [MenuItem("Emberveil/Setup HD-2D Pipeline")]
    public static void ShowWindow()
    {
        GetWindow<HD2DSetupWizard>("HD-2D Setup");
    }

    private void OnGUI()
    {
        GUILayout.Label("Emberveil HD-2D Setup Wizard", EditorStyles.boldLabel);
        GUILayout.Space(10);

        GUILayout.Label("This wizard creates the rendering pipeline assets\nneeded for the HD-2D visual style.", EditorStyles.wordWrappedLabel);
        GUILayout.Space(20);

        if (GUILayout.Button("1. Create URP Pipeline Asset", GUILayout.Height(30)))
        {
            CreateURPPipelineAsset();
        }

        GUILayout.Space(5);

        if (GUILayout.Button("2. Create Post-Processing Profile", GUILayout.Height(30)))
        {
            CreatePostProcessingProfile();
        }

        GUILayout.Space(5);

        if (GUILayout.Button("3. Create Materials", GUILayout.Height(30)))
        {
            CreateMaterials();
        }

        GUILayout.Space(5);

        if (GUILayout.Button("4. Create Post-Processing Volume", GUILayout.Height(30)))
        {
            CreatePostProcessingVolume();
        }

        GUILayout.Space(20);

        if (GUILayout.Button("Run All Steps", GUILayout.Height(40)))
        {
            RunAllSteps();
        }
    }

    /// <summary>
    /// Run all setup steps in order
    /// </summary>
    private void RunAllSteps()
    {
        CreateURPPipelineAsset();
        CreatePostProcessingProfile();
        CreateMaterials();
        CreatePostProcessingVolume();
        Debug.Log("[HD2D Setup] All steps complete! Assign the pipeline asset in Project Settings > Graphics.");
    }

    /// <summary>
    /// Create the URP pipeline asset with HD-2D appropriate settings
    /// </summary>
    private static void CreateURPPipelineAsset()
    {
        EnsureDirectory("Assets/Settings");

        // Create the pipeline asset
        var pipelineAsset = UniversalRenderPipelineAsset.Create();
        AssetDatabase.CreateAsset(pipelineAsset, "Assets/Settings/HD2D_URPAsset.asset");

        // Configure for HD-2D
        // Note: Some properties may need to be set via SerializedObject for full control
        var so = new SerializedObject(pipelineAsset);

        // Enable post-processing
        var postProcessProp = so.FindProperty("m_PostProcessingFeatureSet");
        if (postProcessProp != null)
        {
            postProcessProp.intValue = 1; // PostProcessingFeatureSet.Integrated
        }

        // Shadow settings — soft shadows for HD-2D depth
        var shadowDistProp = so.FindProperty("m_MainLightShadowmapResolution");
        if (shadowDistProp != null)
        {
            shadowDistProp.intValue = 2048;
        }

        so.ApplyModifiedProperties();

        AssetDatabase.SaveAssets();
        Debug.Log("[HD2D Setup] URP Pipeline Asset created at Assets/Settings/HD2D_URPAsset.asset");
        Debug.Log("[HD2D Setup] IMPORTANT: Go to Edit > Project Settings > Graphics and assign this as the Scriptable Render Pipeline Settings.");
    }

    /// <summary>
    /// Create a post-processing profile with HD-2D effects
    /// </summary>
    private static void CreatePostProcessingProfile()
    {
        EnsureDirectory("Assets/Settings");

        var profile = ScriptableObject.CreateInstance<VolumeProfile>();

        // Bloom — for Glove glow and Ember luminescence
        var bloom = profile.Add<Bloom>(true);
        bloom.threshold.value = 0.9f;
        bloom.threshold.overrideState = true;
        bloom.intensity.value = 0.8f;
        bloom.intensity.overrideState = true;
        bloom.scatter.value = 0.7f;
        bloom.scatter.overrideState = true;
        bloom.tint.value = new Color(0.85f, 1f, 0.95f); // Slight blue-green
        bloom.tint.overrideState = true;

        // Depth of Field — tilt-shift feel for HD-2D depth
        var dof = profile.Add<DepthOfField>(true);
        dof.mode.value = DepthOfFieldMode.Bokeh;
        dof.mode.overrideState = true;
        dof.focusDistance.value = 10f;
        dof.focusDistance.overrideState = true;
        dof.focalLength.value = 50f;
        dof.focalLength.overrideState = true;
        dof.aperture.value = 5.6f;
        dof.aperture.overrideState = true;

        // Vignette — subtle darkening at edges
        var vignette = profile.Add<Vignette>(true);
        vignette.intensity.value = 0.25f;
        vignette.intensity.overrideState = true;
        vignette.smoothness.value = 0.4f;
        vignette.smoothness.overrideState = true;

        // Color Adjustments — warm color grading
        var colorAdj = profile.Add<ColorAdjustments>(true);
        colorAdj.colorFilter.value = new Color(1f, 0.97f, 0.92f); // Warm tint
        colorAdj.colorFilter.overrideState = true;
        colorAdj.saturation.value = 10f;
        colorAdj.saturation.overrideState = true;

        AssetDatabase.CreateAsset(profile, "Assets/Settings/HD2D_PostProcessProfile.asset");
        AssetDatabase.SaveAssets();
        Debug.Log("[HD2D Setup] Post-processing profile created at Assets/Settings/HD2D_PostProcessProfile.asset");
    }

    /// <summary>
    /// Create the standard materials for HD-2D rendering
    /// </summary>
    private static void CreateMaterials()
    {
        EnsureDirectory("Assets/Materials");

        // 1. Pixel Art Material — for 3D environment textures
        CreatePixelArtMaterial();

        // 2. Billboard Sprite Material — for character sprites
        CreateBillboardMaterial();

        // 3. Greybox Material — for ProBuilder grey-boxing
        CreateGreyboxMaterial();

        AssetDatabase.SaveAssets();
        Debug.Log("[HD2D Setup] Materials created in Assets/Materials/");
    }

    private static void CreatePixelArtMaterial()
    {
        var shader = Shader.Find("Universal Render Pipeline/Lit");
        if (shader == null)
        {
            Debug.LogWarning("[HD2D Setup] URP Lit shader not found. Is URP installed?");
            return;
        }

        var mat = new Material(shader);
        mat.name = "PixelArt_Lit";

        // Point filtering is set on the texture import settings, not the material
        // But we can set the material to use the texture's native filtering
        mat.SetFloat("_Smoothness", 0f); // No specular — pixel art is matte
        mat.SetFloat("_SpecularHighlights", 0f);
        mat.SetFloat("_EnvironmentReflections", 0f);

        AssetDatabase.CreateAsset(mat, "Assets/Materials/PixelArt_Lit.mat");
    }

    private static void CreateBillboardMaterial()
    {
        // Use the custom billboard shader if available, otherwise URP Unlit with alpha cutout
        var shader = Shader.Find("Emberveil/BillboardSprite");
        if (shader == null)
        {
            shader = Shader.Find("Universal Render Pipeline/Unlit");
        }
        if (shader == null)
        {
            Debug.LogWarning("[HD2D Setup] No suitable shader found for billboard material.");
            return;
        }

        var mat = new Material(shader);
        mat.name = "BillboardSprite";

        // Alpha cutout for pixel-art sprites
        mat.SetFloat("_Cutoff", 0.5f);
        mat.SetFloat("_Surface", 0f); // Opaque with alpha test
        mat.EnableKeyword("_ALPHATEST_ON");
        mat.renderQueue = (int)RenderQueue.AlphaTest;

        AssetDatabase.CreateAsset(mat, "Assets/Materials/BillboardSprite.mat");
    }

    private static void CreateGreyboxMaterial()
    {
        var shader = Shader.Find("Universal Render Pipeline/Lit");
        if (shader == null) return;

        var mat = new Material(shader);
        mat.name = "Greybox";
        mat.SetColor("_BaseColor", new Color(0.7f, 0.7f, 0.7f, 1f));
        mat.SetFloat("_Smoothness", 0.2f);

        AssetDatabase.CreateAsset(mat, "Assets/Materials/Greybox.mat");
    }

    /// <summary>
    /// Create a global post-processing volume in the current scene
    /// </summary>
    private static void CreatePostProcessingVolume()
    {
        // Check if one already exists
        var existing = Object.FindObjectOfType<Volume>();
        if (existing != null)
        {
            Debug.Log("[HD2D Setup] Post-processing volume already exists in scene.");
            return;
        }

        var volumeGO = new GameObject("PostProcessing Volume");
        var volume = volumeGO.AddComponent<Volume>();
        volume.isGlobal = true;
        volume.priority = 1f;

        // Try to assign the profile
        var profilePath = "Assets/Settings/HD2D_PostProcessProfile.asset";
        var profile = AssetDatabase.LoadAssetAtPath<VolumeProfile>(profilePath);
        if (profile != null)
        {
            volume.profile = profile;
            Debug.Log("[HD2D Setup] Post-processing volume created and profile assigned.");
        }
        else
        {
            Debug.LogWarning("[HD2D Setup] Post-processing volume created but profile not found. Run step 2 first.");
        }
    }

    /// <summary>
    /// Ensure a directory exists in the Assets folder
    /// </summary>
    private static void EnsureDirectory(string path)
    {
        if (!AssetDatabase.IsValidFolder(path))
        {
            string[] parts = path.Split('/');
            string current = parts[0];
            for (int i = 1; i < parts.Length; i++)
            {
                string next = current + "/" + parts[i];
                if (!AssetDatabase.IsValidFolder(next))
                {
                    AssetDatabase.CreateFolder(current, parts[i]);
                }
                current = next;
            }
        }
    }
}
#endif
