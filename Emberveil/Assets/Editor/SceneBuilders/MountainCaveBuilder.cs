#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

/// <summary>
/// Editor script that generates the Mountain Cave scene (Chapter 1 opening).
/// Run via menu: Emberveil > Build Scenes > Mountain Cave
///
/// Creates a grey-box cave interior with:
/// - Cave chamber (floor, walls, rough ceiling)
/// - Light shaft from above
/// - Exit passage leading out
/// - Mouse spawning/awakening positions
/// - Directional light (cold blue-white)
/// - Scene transition trigger at exit
/// - Post-processing volume
/// - Camera waypoints for intro sweep
/// </summary>
public class MountainCaveBuilder
{
    [MenuItem("Emberveil/Build Scenes/Mountain Cave")]
    public static void BuildMountainCave()
    {
        // Create new scene
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        scene.name = "MountainCave";

        // --- Lighting ---
        // Cold directional light
        var dirLightGO = new GameObject("Directional Light");
        var dirLight = dirLightGO.AddComponent<Light>();
        dirLight.type = LightType.Directional;
        dirLight.color = new Color(0.6f, 0.65f, 0.8f); // Cold blue-white
        dirLight.intensity = 0.4f;
        dirLightGO.transform.rotation = Quaternion.Euler(60, -30, 0);

        // Light shaft from above (point light simulating crack in ceiling)
        var lightShaftGO = new GameObject("LightShaft");
        var lightShaft = lightShaftGO.AddComponent<Light>();
        lightShaft.type = LightType.Spot;
        lightShaft.color = new Color(0.7f, 0.75f, 0.9f);
        lightShaft.intensity = 3f;
        lightShaft.range = 15f;
        lightShaft.spotAngle = 30f;
        lightShaftGO.transform.position = new Vector3(0, 8, 0);
        lightShaftGO.transform.rotation = Quaternion.Euler(90, 0, 0);

        // --- Cave Geometry ---
        var caveParent = new GameObject("CaveGeometry");

        // Floor (slightly uneven — two overlapping planes at slight angles)
        CreateCube(new Vector3(0, -0.1f, 0), new Vector3(12, 0.2f, 10), "Floor_Main", caveParent.transform, new Color(0.35f, 0.32f, 0.3f));
        CreateCube(new Vector3(-1, -0.05f, 2), new Vector3(4, 0.15f, 3), "Floor_Raised", caveParent.transform, new Color(0.38f, 0.35f, 0.32f));

        // Back wall (irregular — multiple overlapping cubes)
        CreateCube(new Vector3(0, 2, -5.5f), new Vector3(14, 5, 1), "Wall_Back", caveParent.transform, new Color(0.3f, 0.28f, 0.26f));
        CreateCube(new Vector3(-3, 1.5f, -5f), new Vector3(2, 4, 1.5f), "Wall_BackBulge_L", caveParent.transform, new Color(0.28f, 0.26f, 0.25f));
        CreateCube(new Vector3(4, 2.5f, -5.2f), new Vector3(3, 3, 1.2f), "Wall_BackBulge_R", caveParent.transform, new Color(0.32f, 0.3f, 0.27f));

        // Left wall
        CreateCube(new Vector3(-6.5f, 2, 0), new Vector3(1, 5, 12), "Wall_Left", caveParent.transform, new Color(0.3f, 0.28f, 0.26f));
        CreateCube(new Vector3(-6f, 1, -2), new Vector3(1.5f, 3, 4), "Wall_LeftBulge", caveParent.transform, new Color(0.28f, 0.25f, 0.24f));

        // Right wall (with exit passage gap)
        CreateCube(new Vector3(6.5f, 2, -2), new Vector3(1, 5, 6), "Wall_Right_Back", caveParent.transform, new Color(0.3f, 0.28f, 0.26f));
        CreateCube(new Vector3(6.5f, 2, 4), new Vector3(1, 5, 2), "Wall_Right_Front", caveParent.transform, new Color(0.3f, 0.28f, 0.26f));
        // Gap between 3-5 Z is the exit passage

        // Ceiling (rough — multiple overlapping pieces)
        CreateCube(new Vector3(0, 5, 0), new Vector3(14, 0.5f, 12), "Ceiling_Main", caveParent.transform, new Color(0.25f, 0.23f, 0.22f));
        CreateCube(new Vector3(-2, 4.5f, -1), new Vector3(4, 0.8f, 3), "Ceiling_Stalactite1", caveParent.transform, new Color(0.22f, 0.2f, 0.19f));
        CreateCube(new Vector3(3, 4.2f, 2), new Vector3(2, 1, 2), "Ceiling_Stalactite2", caveParent.transform, new Color(0.22f, 0.2f, 0.19f));

        // Hole in ceiling above light shaft
        // (Represented by a gap — the ceiling main piece could be split, but for grey-box this works)

        // Exit passage — narrow corridor leading right
        var passageParent = new GameObject("ExitPassage");
        passageParent.transform.SetParent(caveParent.transform);
        CreateCube(new Vector3(9, -0.1f, 3.5f), new Vector3(6, 0.2f, 2), "Passage_Floor", passageParent.transform, new Color(0.35f, 0.32f, 0.3f));
        CreateCube(new Vector3(9, 2, 2.3f), new Vector3(6, 5, 0.5f), "Passage_Wall_South", passageParent.transform, new Color(0.3f, 0.28f, 0.26f));
        CreateCube(new Vector3(9, 2, 4.7f), new Vector3(6, 5, 0.5f), "Passage_Wall_North", passageParent.transform, new Color(0.3f, 0.28f, 0.26f));
        CreateCube(new Vector3(9, 4.5f, 3.5f), new Vector3(6, 1, 3), "Passage_Ceiling", passageParent.transform, new Color(0.25f, 0.23f, 0.22f));

        // --- Gameplay Objects ---
        var gameplayParent = new GameObject("Gameplay");

        // Awakening position (where Mouse starts — lying on the floor)
        var awakePos = new GameObject("AwakeningPosition");
        awakePos.transform.SetParent(gameplayParent.transform);
        awakePos.transform.position = new Vector3(-1, 0.1f, -1);
        awakePos.transform.rotation = Quaternion.Euler(0, 90, 0);

        // Standing position (where Mouse stands up to)
        var standPos = new GameObject("StandingPosition");
        standPos.transform.SetParent(gameplayParent.transform);
        standPos.transform.position = new Vector3(-1, 0.1f, -1);

        // Player spawn (same as standing position for after-intro loading)
        var spawnGO = new GameObject("SpawnPoint_Default");
        spawnGO.transform.SetParent(gameplayParent.transform);
        spawnGO.transform.position = new Vector3(-1, 0.1f, -1);
        var spawnPoint = spawnGO.AddComponent<SpawnPoint>();
        spawnPoint.spawnPointId = "CaveStart";
        spawnPoint.facingDirection = Vector3.right;

        // Exit transition trigger
        var exitTrigger = new GameObject("ExitTrigger");
        exitTrigger.transform.SetParent(gameplayParent.transform);
        exitTrigger.transform.position = new Vector3(12, 1.5f, 3.5f);
        var exitCol = exitTrigger.AddComponent<BoxCollider>();
        exitCol.isTrigger = true;
        exitCol.size = new Vector3(1, 3, 2);
        var exitTransition = exitTrigger.AddComponent<SceneTransition>();
        // Note: targetSceneName and spawnPointId need to be set in Inspector

        // --- Camera Waypoints (for intro sweep) ---
        var waypointsParent = new GameObject("IntroCameraWaypoints");

        // Waypoint 1: High above, looking at distant mountain peak
        var wp1 = new GameObject("Waypoint_Peak");
        wp1.transform.SetParent(waypointsParent.transform);
        wp1.transform.position = new Vector3(0, 50, -30);
        wp1.transform.rotation = Quaternion.Euler(30, 0, 0);

        // Waypoint 2: Sweeping down toward the cave
        var wp2 = new GameObject("Waypoint_MidSweep");
        wp2.transform.SetParent(waypointsParent.transform);
        wp2.transform.position = new Vector3(0, 20, -15);
        wp2.transform.rotation = Quaternion.Euler(35, 0, 0);

        // Waypoint 3: Just outside cave entrance
        var wp3 = new GameObject("Waypoint_CaveEntrance");
        wp3.transform.SetParent(waypointsParent.transform);
        wp3.transform.position = new Vector3(12, 5, 3.5f);
        wp3.transform.rotation = Quaternion.Euler(25, -150, 0);

        // Waypoint 4: Inside cave, looking at Mouse's position
        var wp4 = new GameObject("Waypoint_InsideCave");
        wp4.transform.SetParent(waypointsParent.transform);
        wp4.transform.position = new Vector3(3, 4, 2);
        wp4.transform.rotation = Quaternion.Euler(35, -120, 0);

        // --- Managers ---
        // Create GameManager if not present
        var gmGO = new GameObject("GameManager");
        gmGO.AddComponent<GameManager>();

        // Create CutsceneManager
        var cmGO = new GameObject("CutsceneManager");
        cmGO.AddComponent<CutsceneManager>();

        // Create IntroSequenceController
        var introGO = new GameObject("IntroSequenceController");
        introGO.AddComponent<IntroSequenceController>();
        // Note: Camera waypoints and positions need to be wired in Inspector

        // --- Mouse (Player) ---
        var mouseGO = new GameObject("Mouse");
        mouseGO.transform.position = new Vector3(-1, 0.1f, -1);

        var cc = mouseGO.AddComponent<CharacterController>();
        cc.height = 1f;
        cc.radius = 0.3f;
        cc.center = new Vector3(0, 0.5f, 0);

        mouseGO.AddComponent<PlayerController>();
        mouseGO.AddComponent<GloveController>();

        // Add a visible placeholder — small cube for now
        var mouseVisual = GameObject.CreatePrimitive(PrimitiveType.Cube);
        mouseVisual.name = "MouseVisual_Placeholder";
        mouseVisual.transform.SetParent(mouseGO.transform);
        mouseVisual.transform.localPosition = new Vector3(0, 0.5f, 0);
        mouseVisual.transform.localScale = new Vector3(0.5f, 0.8f, 0.5f);
        Object.DestroyImmediate(mouseVisual.GetComponent<BoxCollider>()); // CC handles collision

        // Placeholder Glove light
        var gloveLightGO = new GameObject("GloveLight");
        gloveLightGO.transform.SetParent(mouseGO.transform);
        gloveLightGO.transform.localPosition = new Vector3(0, 0.5f, 0.3f);
        var mouseGloveLight = gloveLightGO.AddComponent<Light>();
        mouseGloveLight.type = LightType.Point;
        mouseGloveLight.color = new Color(0.2f, 0.8f, 0.6f);
        mouseGloveLight.intensity = 0.8f;
        mouseGloveLight.range = 4f;
        mouseGloveLight.enabled = false;

        // --- Main Camera ---
        var camGO = new GameObject("Main Camera");
        camGO.tag = "MainCamera";
        var cam = camGO.AddComponent<Camera>();
        cam.orthographic = true;
        cam.orthographicSize = 6f;
        cam.nearClipPlane = 0.1f;
        cam.farClipPlane = 100f;
        cam.backgroundColor = new Color(0.05f, 0.05f, 0.08f); // Very dark blue
        cam.clearFlags = CameraClearFlags.SolidColor;
        camGO.AddComponent<HD2DCameraController>();

        // Position camera for HD-2D view
        float tiltAngle = 35f;
        float distance = 12f;
        float yOffset = Mathf.Sin(tiltAngle * Mathf.Deg2Rad) * distance;
        float zOffset = -Mathf.Cos(tiltAngle * Mathf.Deg2Rad) * distance;
        camGO.transform.position = mouseGO.transform.position + new Vector3(0, yOffset, zOffset);
        camGO.transform.rotation = Quaternion.Euler(tiltAngle, 0, 0);

        // --- Post-Processing Volume ---
        var volumeGO = new GameObject("PostProcessing Volume");
        var volume = volumeGO.AddComponent<Volume>();
        volume.isGlobal = true;
        // Note: Volume profile needs to be assigned in Inspector or via HD2DSetupWizard

        // Save the scene
        EnsureDirectory("Assets/Scenes");
        EditorSceneManager.SaveScene(scene, "Assets/Scenes/MountainCave.unity");

        Debug.Log("[SceneBuilder] Mountain Cave scene built and saved to Assets/Scenes/MountainCave.unity");
        Debug.Log("[SceneBuilder] TODO: Wire up IntroSequenceController references in Inspector");
        Debug.Log("[SceneBuilder] TODO: Set ExitTrigger's SceneTransition target to 'MountainDescent'");
    }

    // --- Utility ---

    private static GameObject CreateCube(Vector3 center, Vector3 size, string name, Transform parent, Color color)
    {
        var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.name = name;
        go.transform.position = center;
        go.transform.localScale = size;
        go.transform.SetParent(parent);
        go.isStatic = true;

        // Create material with color
        var mat = new Material(Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard"));
        mat.SetColor(mat.HasProperty("_BaseColor") ? "_BaseColor" : "_Color", color);
        mat.SetFloat("_Smoothness", 0.1f);
        go.GetComponent<MeshRenderer>().sharedMaterial = mat;

        return go;
    }

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
