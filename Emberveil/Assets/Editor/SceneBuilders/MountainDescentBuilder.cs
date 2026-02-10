#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

/// <summary>
/// Editor script that generates the Mountain Descent scene (Chapter 1 continued).
/// Run via menu: Emberveil > Build Scenes > Mountain Descent
///
/// Creates a grey-box outdoor mountain path with:
/// - Winding path downhill on rocky terrain
/// - Bridge crossing (narrative landmark)
/// - Fox silhouette position (on a ridge)
/// - Broken trail markers (environmental storytelling)
/// - Transition at bottom → Overworld
/// - Spawn point from MountainCave exit
/// </summary>
public class MountainDescentBuilder
{
    [MenuItem("Emberveil/Build Scenes/Mountain Descent")]
    public static void BuildMountainDescent()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        scene.name = "MountainDescent";

        // --- Lighting ---
        // Early dawn light — cold turning warm
        var dirLightGO = new GameObject("Directional Light");
        var dirLight = dirLightGO.AddComponent<Light>();
        dirLight.type = LightType.Directional;
        dirLight.color = new Color(0.85f, 0.8f, 0.7f); // Dawn gold
        dirLight.intensity = 0.8f;
        dirLightGO.transform.rotation = Quaternion.Euler(25, 45, 0);

        // Ambient — slight blue fog of early morning
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientLight = new Color(0.35f, 0.4f, 0.5f);
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogColor = new Color(0.6f, 0.65f, 0.75f);
        RenderSettings.fogStartDistance = 30f;
        RenderSettings.fogEndDistance = 80f;

        // --- Terrain Geometry ---
        var terrainParent = new GameObject("TerrainGeometry");

        // Mountain slope — series of descending platforms
        float pathY = 0;
        float pathZ = 0;

        // Section 1: Cave exit area (high, flat)
        CreatePlatform(new Vector3(0, 8, 0), 8, 6, "Section_CaveExit", terrainParent.transform, new Color(0.4f, 0.38f, 0.35f));

        // Section 2: First descent (angled path)
        CreatePlatform(new Vector3(3, 6.5f, 8), 6, 8, "Section_Descent1", terrainParent.transform, new Color(0.42f, 0.4f, 0.36f));
        CreateRamp(new Vector3(1, 7, 4), 4, 1.5f, 6, "Ramp_1", terrainParent.transform, new Color(0.4f, 0.38f, 0.35f));

        // Section 3: Lookout point (flat platform with view of valley)
        CreatePlatform(new Vector3(-2, 5, 16), 8, 6, "Section_Lookout", terrainParent.transform, new Color(0.43f, 0.42f, 0.38f));

        // Section 4: Path to bridge
        CreatePlatform(new Vector3(2, 3.5f, 24), 5, 10, "Section_PreBridge", terrainParent.transform, new Color(0.44f, 0.43f, 0.4f));
        CreateRamp(new Vector3(-1, 4, 20), 4, 1.5f, 5, "Ramp_2", terrainParent.transform, new Color(0.42f, 0.4f, 0.37f));

        // Section 5: THE BRIDGE
        var bridgeParent = new GameObject("Bridge");
        bridgeParent.transform.SetParent(terrainParent.transform);
        // Bridge deck
        CreateCube(new Vector3(2, 3, 34), new Vector3(3, 0.15f, 8), "Bridge_Deck", bridgeParent.transform, new Color(0.45f, 0.35f, 0.2f)); // Wooden brown
        // Bridge railings
        CreateCube(new Vector3(0.3f, 3.5f, 34), new Vector3(0.15f, 1f, 8), "Bridge_Rail_L", bridgeParent.transform, new Color(0.4f, 0.3f, 0.18f));
        CreateCube(new Vector3(3.7f, 3.5f, 34), new Vector3(0.15f, 1f, 8), "Bridge_Rail_R", bridgeParent.transform, new Color(0.4f, 0.3f, 0.18f));
        // Bridge posts
        for (int i = 0; i < 3; i++)
        {
            float z = 30 + i * 4;
            CreateCube(new Vector3(0.3f, 3.5f, z), new Vector3(0.2f, 1.5f, 0.2f), $"Bridge_Post_L{i}", bridgeParent.transform, new Color(0.4f, 0.3f, 0.18f));
            CreateCube(new Vector3(3.7f, 3.5f, z), new Vector3(0.2f, 1.5f, 0.2f), $"Bridge_Post_R{i}", bridgeParent.transform, new Color(0.4f, 0.3f, 0.18f));
        }

        // Section 6: After bridge — descent to valley
        CreatePlatform(new Vector3(2, 2, 42), 8, 8, "Section_PostBridge", terrainParent.transform, new Color(0.45f, 0.45f, 0.4f));
        CreateRamp(new Vector3(2, 2.5f, 38), 4, 0.5f, 8, "Ramp_3", terrainParent.transform, new Color(0.44f, 0.43f, 0.4f));

        // Section 7: Valley floor (transition area)
        CreatePlatform(new Vector3(0, 0, 52), 15, 10, "Section_ValleyEdge", terrainParent.transform, new Color(0.4f, 0.5f, 0.35f)); // Greener — approaching valley

        // Surrounding rock walls (canyon effect)
        CreateCube(new Vector3(-8, 5, 20), new Vector3(2, 12, 40), "RockWall_Left", terrainParent.transform, new Color(0.35f, 0.33f, 0.3f));
        CreateCube(new Vector3(12, 5, 20), new Vector3(2, 12, 40), "RockWall_Right", terrainParent.transform, new Color(0.35f, 0.33f, 0.3f));

        // --- Environmental Storytelling ---
        var storyParent = new GameObject("EnvironmentalStory");

        // Broken trail marker 1 (tilted cube)
        var marker1 = CreateCube(new Vector3(5, 7, 6), new Vector3(0.2f, 1.2f, 0.1f), "BrokenMarker_1", storyParent.transform, new Color(0.5f, 0.45f, 0.35f));
        marker1.transform.rotation = Quaternion.Euler(0, 15, 25); // Tilted — broken

        // Broken trail marker 2
        var marker2 = CreateCube(new Vector3(-1, 4.5f, 19), new Vector3(0.2f, 0.8f, 0.1f), "BrokenMarker_2", storyParent.transform, new Color(0.5f, 0.45f, 0.35f));
        marker2.transform.rotation = Quaternion.Euler(0, -30, 40); // More broken

        // Overgrown path stones
        for (int i = 0; i < 5; i++)
        {
            float x = Random.Range(-1f, 3f);
            float z = 8 + i * 3;
            var stone = CreateCube(new Vector3(x, 6.55f + (i * -0.3f), z), new Vector3(0.8f, 0.1f, 0.6f), $"PathStone_{i}", storyParent.transform, new Color(0.5f, 0.48f, 0.45f));
            stone.transform.rotation = Quaternion.Euler(0, Random.Range(0, 45), 0);
        }

        // Scattered rocks (debris)
        for (int i = 0; i < 8; i++)
        {
            float x = Random.Range(-5f, 9f);
            float z = Random.Range(5f, 45f);
            float y = Mathf.Lerp(8, 2, z / 45f) + 0.15f;
            var rock = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            rock.name = $"ScatteredRock_{i}";
            float size = Random.Range(0.2f, 0.6f);
            rock.transform.position = new Vector3(x, y, z);
            rock.transform.localScale = new Vector3(size, size * 0.6f, size);
            rock.transform.SetParent(storyParent.transform);
            rock.isStatic = true;
            var mat = new Material(Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard"));
            mat.SetColor(mat.HasProperty("_BaseColor") ? "_BaseColor" : "_Color", new Color(0.4f, 0.38f, 0.35f));
            rock.GetComponent<MeshRenderer>().sharedMaterial = mat;
        }

        // --- Fox Silhouette Position ---
        var foxParent = new GameObject("FoxEncounter");

        // Fox stands on a ridge above the lookout
        var foxRidge = CreateCube(new Vector3(-6, 8, 14), new Vector3(3, 2, 2), "FoxRidge", foxParent.transform, new Color(0.35f, 0.33f, 0.3f));

        // Fox silhouette placeholder (small dark figure)
        var foxSilhouette = GameObject.CreatePrimitive(PrimitiveType.Cube);
        foxSilhouette.name = "FoxSilhouette_Placeholder";
        foxSilhouette.transform.position = new Vector3(-6, 9.5f, 14);
        foxSilhouette.transform.localScale = new Vector3(0.4f, 0.6f, 0.3f);
        foxSilhouette.transform.SetParent(foxParent.transform);
        var foxMat = new Material(Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard"));
        foxMat.SetColor(foxMat.HasProperty("_BaseColor") ? "_BaseColor" : "_Color", new Color(0.1f, 0.1f, 0.12f)); // Dark silhouette
        foxSilhouette.GetComponent<MeshRenderer>().sharedMaterial = foxMat;

        // --- Gameplay Objects ---
        var gameplayParent = new GameObject("Gameplay");

        // Spawn point (from cave exit)
        var spawnGO = new GameObject("SpawnPoint_FromCave");
        spawnGO.transform.SetParent(gameplayParent.transform);
        spawnGO.transform.position = new Vector3(0, 8.1f, 1);
        var spawnPoint = spawnGO.AddComponent<SpawnPoint>();
        spawnPoint.spawnPointId = "FromCave";
        spawnPoint.facingDirection = Vector3.forward;

        // Transition to Overworld (at valley floor)
        var exitTrigger = new GameObject("ExitToOverworld");
        exitTrigger.transform.SetParent(gameplayParent.transform);
        exitTrigger.transform.position = new Vector3(0, 1, 57);
        var exitCol = exitTrigger.AddComponent<BoxCollider>();
        exitCol.isTrigger = true;
        exitCol.size = new Vector3(15, 3, 2);
        exitTrigger.AddComponent<SceneTransition>();
        // Note: targetSceneName="Overworld", spawnPointId="FromMountain" — set in Inspector

        // --- Player ---
        var mouseGO = new GameObject("Mouse");
        mouseGO.transform.position = new Vector3(0, 8.1f, 1);

        var cc = mouseGO.AddComponent<CharacterController>();
        cc.height = 1f;
        cc.radius = 0.3f;
        cc.center = new Vector3(0, 0.5f, 0);

        mouseGO.AddComponent<PlayerController>();
        mouseGO.AddComponent<GloveController>();

        var mouseVisual = GameObject.CreatePrimitive(PrimitiveType.Cube);
        mouseVisual.name = "MouseVisual_Placeholder";
        mouseVisual.transform.SetParent(mouseGO.transform);
        mouseVisual.transform.localPosition = new Vector3(0, 0.5f, 0);
        mouseVisual.transform.localScale = new Vector3(0.5f, 0.8f, 0.5f);
        Object.DestroyImmediate(mouseVisual.GetComponent<BoxCollider>());

        // --- Managers ---
        var gmGO = new GameObject("GameManager");
        gmGO.AddComponent<GameManager>();

        // --- Camera ---
        var camGO = new GameObject("Main Camera");
        camGO.tag = "MainCamera";
        var cam = camGO.AddComponent<Camera>();
        cam.orthographic = true;
        cam.orthographicSize = 8f; // Wider view for outdoor
        cam.nearClipPlane = 0.1f;
        cam.farClipPlane = 100f;
        cam.backgroundColor = new Color(0.5f, 0.6f, 0.7f);
        cam.clearFlags = CameraClearFlags.SolidColor;
        camGO.AddComponent<HD2DCameraController>();

        float tiltAngle = 35f;
        float distance = 12f;
        float yOff = Mathf.Sin(tiltAngle * Mathf.Deg2Rad) * distance;
        float zOff = -Mathf.Cos(tiltAngle * Mathf.Deg2Rad) * distance;
        camGO.transform.position = mouseGO.transform.position + new Vector3(0, yOff, zOff);
        camGO.transform.rotation = Quaternion.Euler(tiltAngle, 0, 0);

        // --- Post-Processing Volume ---
        var volumeGO = new GameObject("PostProcessing Volume");
        volumeGO.AddComponent<UnityEngine.Rendering.Volume>().isGlobal = true;

        // Save
        EnsureDirectory("Assets/Scenes");
        EditorSceneManager.SaveScene(scene, "Assets/Scenes/MountainDescent.unity");

        Debug.Log("[SceneBuilder] Mountain Descent scene built and saved to Assets/Scenes/MountainDescent.unity");
        Debug.Log("[SceneBuilder] TODO: Set ExitToOverworld SceneTransition target to 'Overworld'");
        Debug.Log("[SceneBuilder] TODO: Wire up Fox encounter trigger/animation");
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

        var mat = new Material(Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard"));
        mat.SetColor(mat.HasProperty("_BaseColor") ? "_BaseColor" : "_Color", color);
        mat.SetFloat("_Smoothness", 0.1f);
        go.GetComponent<MeshRenderer>().sharedMaterial = mat;

        return go;
    }

    private static void CreatePlatform(Vector3 center, float width, float depth, string name, Transform parent, Color color)
    {
        CreateCube(new Vector3(center.x, center.y - 0.25f, center.z), new Vector3(width, 0.5f, depth), name, parent, color);
    }

    private static void CreateRamp(Vector3 start, float length, float heightDiff, float width, string name, Transform parent, Color color)
    {
        // Approximate ramp with several stepped cubes
        int steps = 6;
        float stepLen = length / steps;
        float stepH = heightDiff / steps;

        var rampParent = new GameObject(name);
        rampParent.transform.SetParent(parent);

        for (int i = 0; i < steps; i++)
        {
            float z = start.z + stepLen * i + stepLen / 2;
            float y = start.y - stepH * i - stepH / 2;
            CreateCube(new Vector3(start.x, y, z), new Vector3(width, stepH, stepLen), $"{name}_Step{i}", rampParent.transform, color);
        }
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
