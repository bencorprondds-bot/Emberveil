#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

/// <summary>
/// Editor script that generates the initial Overworld scene around the Old Oak.
/// Run via menu: Emberveil > Build Scenes > Overworld
///
/// Creates a grey-box outdoor area with:
/// - Large ground plane with slight color variation
/// - The Old Oak (large hollow tree structure)
/// - River (flat blue plane)
/// - Forest edge with placeholder trees
/// - Ivy patches at blocked paths
/// - Hawk NPC near the Old Oak
/// - Transitions: to Burrow (door), from Mountain (spawn)
/// </summary>
public class OverworldBuilder
{
    [MenuItem("Emberveil/Build Scenes/Overworld")]
    public static void BuildOverworld()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        scene.name = "Overworld";

        // --- Lighting ---
        var dirLightGO = new GameObject("Directional Light");
        var dirLight = dirLightGO.AddComponent<Light>();
        dirLight.type = LightType.Directional;
        dirLight.color = new Color(1f, 0.95f, 0.85f); // Warm daylight
        dirLight.intensity = 1.2f;
        dirLightGO.transform.rotation = Quaternion.Euler(45, 30, 0);

        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientLight = new Color(0.45f, 0.5f, 0.45f);

        // --- Ground ---
        var terrainParent = new GameObject("Terrain");

        // Main ground plane
        var ground = CreateCube(new Vector3(0, -0.05f, 0), new Vector3(60, 0.1f, 60), "Ground_Main", terrainParent.transform, new Color(0.4f, 0.52f, 0.32f));

        // Slight path from mountain (dirt color)
        CreateCube(new Vector3(-20, -0.02f, 0), new Vector3(15, 0.1f, 3), "Path_FromMountain", terrainParent.transform, new Color(0.5f, 0.45f, 0.35f));
        CreateCube(new Vector3(-10, -0.02f, 3), new Vector3(3, 0.1f, 10), "Path_ToOak", terrainParent.transform, new Color(0.5f, 0.45f, 0.35f));

        // --- The Old Oak ---
        var oakParent = new GameObject("TheOldOak");

        // Trunk (large cylinder)
        var trunk = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        trunk.name = "OakTrunk";
        trunk.transform.position = new Vector3(0, 4, 8);
        trunk.transform.localScale = new Vector3(5, 4, 5);
        trunk.transform.SetParent(oakParent.transform);
        trunk.isStatic = true;
        SetMaterialColor(trunk, new Color(0.4f, 0.3f, 0.2f));

        // Canopy (large flattened sphere)
        var canopy = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        canopy.name = "OakCanopy";
        canopy.transform.position = new Vector3(0, 10, 8);
        canopy.transform.localScale = new Vector3(14, 8, 14);
        canopy.transform.SetParent(oakParent.transform);
        canopy.isStatic = true;
        SetMaterialColor(canopy, new Color(0.25f, 0.42f, 0.2f));

        // Door opening (represented by a dark cube recessed into trunk)
        var door = CreateCube(new Vector3(0, 1.2f, 5.4f), new Vector3(1.5f, 2.4f, 0.5f), "OakDoor", oakParent.transform, new Color(0.15f, 0.1f, 0.08f));

        // Warm glow from inside (point light at door)
        var oakGlow = new GameObject("OakInteriorGlow");
        oakGlow.transform.SetParent(oakParent.transform);
        oakGlow.transform.position = new Vector3(0, 1.5f, 6);
        var oakLight = oakGlow.AddComponent<Light>();
        oakLight.type = LightType.Point;
        oakLight.color = new Color(1f, 0.85f, 0.6f); // Warm amber
        oakLight.intensity = 2f;
        oakLight.range = 6f;

        // --- River ---
        var riverParent = new GameObject("River");
        CreateCube(new Vector3(15, -0.3f, 0), new Vector3(5, 0.1f, 60), "RiverSurface", riverParent.transform, new Color(0.25f, 0.4f, 0.55f));
        // River banks (slight raised edges)
        CreateCube(new Vector3(12.2f, -0.1f, 0), new Vector3(0.5f, 0.3f, 60), "RiverBank_West", riverParent.transform, new Color(0.42f, 0.5f, 0.33f));
        CreateCube(new Vector3(17.8f, -0.1f, 0), new Vector3(0.5f, 0.3f, 60), "RiverBank_East", riverParent.transform, new Color(0.42f, 0.5f, 0.33f));

        // Redwood bridge over river
        var rwBridge = new GameObject("RedwoodBridge");
        rwBridge.transform.SetParent(riverParent.transform);
        CreateCube(new Vector3(15, 0.1f, 8), new Vector3(6, 0.15f, 3), "Bridge_Deck", rwBridge.transform, new Color(0.5f, 0.35f, 0.2f));
        CreateCube(new Vector3(12.2f, 0.5f, 8), new Vector3(0.15f, 0.8f, 3), "Bridge_Rail_W", rwBridge.transform, new Color(0.45f, 0.3f, 0.18f));
        CreateCube(new Vector3(17.8f, 0.5f, 8), new Vector3(0.15f, 0.8f, 3), "Bridge_Rail_E", rwBridge.transform, new Color(0.45f, 0.3f, 0.18f));

        // --- Forest Edge (with trees) ---
        var forestParent = new GameObject("ForestEdge");
        float[] treePositionsX = { -25, -22, -20, -18, -24, 25, 22, 20, 28 };
        float[] treePositionsZ = { -5, 5, 15, 25, -15, -5, 10, 20, 0 };
        for (int i = 0; i < treePositionsX.Length; i++)
        {
            CreateTree(new Vector3(treePositionsX[i], 0, treePositionsZ[i]), forestParent.transform, $"Tree_{i}");
        }
        // Back edge
        for (int i = 0; i < 8; i++)
        {
            CreateTree(new Vector3(-20 + i * 6, 0, 28), forestParent.transform, $"TreeBack_{i}");
        }

        // --- Ivy Patches (Area Gates) ---
        var ivyParent = new GameObject("IvyPatches");

        // Ivy blocking path east (past the river)
        CreateIvyPatch(new Vector3(22, 0, 8), new Vector3(4, 3, 4), "IvyBlock_East", ivyParent.transform);

        // Ivy blocking path north (into deep forest)
        CreateIvyPatch(new Vector3(-5, 0, 27), new Vector3(8, 3, 2), "IvyBlock_North", ivyParent.transform);

        // Ivy blocking path southwest
        CreateIvyPatch(new Vector3(-22, 0, -8), new Vector3(3, 3, 5), "IvyBlock_Southwest", ivyParent.transform);

        // --- NPCs ---
        var npcParent = new GameObject("NPCs");

        // Hawk — near the Old Oak, injured
        var hawkGO = new GameObject("Hawk");
        hawkGO.transform.SetParent(npcParent.transform);
        hawkGO.transform.position = new Vector3(-2, 0.1f, 6);

        // Hawk visual placeholder
        var hawkVisual = GameObject.CreatePrimitive(PrimitiveType.Cube);
        hawkVisual.name = "HawkVisual_Placeholder";
        hawkVisual.transform.SetParent(hawkGO.transform);
        hawkVisual.transform.localPosition = new Vector3(0, 0.4f, 0);
        hawkVisual.transform.localScale = new Vector3(0.4f, 0.7f, 0.4f);
        SetMaterialColor(hawkVisual, new Color(0.55f, 0.45f, 0.35f)); // Brown hawk
        Object.DestroyImmediate(hawkVisual.GetComponent<BoxCollider>());

        // Hawk needs a collider for interaction
        var hawkCol = hawkGO.AddComponent<BoxCollider>();
        hawkCol.center = new Vector3(0, 0.4f, 0);
        hawkCol.size = new Vector3(0.6f, 0.9f, 0.6f);

        hawkGO.AddComponent<BillboardSprite>();
        var hawkNPC = hawkGO.AddComponent<TalkableNPC>();

        // --- Gameplay ---
        var gameplayParent = new GameObject("Gameplay");

        // Spawn from mountain descent
        var spawnMountain = new GameObject("SpawnPoint_FromMountain");
        spawnMountain.transform.SetParent(gameplayParent.transform);
        spawnMountain.transform.position = new Vector3(-25, 0.1f, 0);
        var sp1 = spawnMountain.AddComponent<SpawnPoint>();
        sp1.spawnPointId = "FromMountain";
        sp1.facingDirection = Vector3.right;

        // Spawn from Burrow (exiting Old Oak door)
        var spawnBurrow = new GameObject("SpawnPoint_FromBurrow");
        spawnBurrow.transform.SetParent(gameplayParent.transform);
        spawnBurrow.transform.position = new Vector3(0, 0.1f, 4.5f);
        var sp2 = spawnBurrow.AddComponent<SpawnPoint>();
        sp2.spawnPointId = "FromBurrow";
        sp2.facingDirection = Vector3.back;

        // Transition into Burrow (at the Oak door)
        var burrowEntry = new GameObject("EnterBurrow");
        burrowEntry.transform.SetParent(gameplayParent.transform);
        burrowEntry.transform.position = new Vector3(0, 1.2f, 5.5f);
        var burrowCol = burrowEntry.AddComponent<BoxCollider>();
        burrowCol.isTrigger = true;
        burrowCol.size = new Vector3(1.5f, 2.4f, 0.5f);
        burrowEntry.AddComponent<SceneTransition>();
        // Note: Set target to "Burrow", spawnId="FromOverworld" in Inspector

        // --- Player ---
        var mouseGO = new GameObject("Mouse");
        mouseGO.transform.position = new Vector3(-25, 0.1f, 0);

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
        SetMaterialColor(mouseVisual, new Color(0.6f, 0.55f, 0.5f));

        mouseGO.AddComponent<BillboardSprite>();

        // --- Managers ---
        var gmGO = new GameObject("GameManager");
        gmGO.AddComponent<GameManager>();

        // --- Camera ---
        var camGO = new GameObject("Main Camera");
        camGO.tag = "MainCamera";
        var cam = camGO.AddComponent<Camera>();
        cam.orthographic = true;
        cam.orthographicSize = 8f;
        cam.nearClipPlane = 0.1f;
        cam.farClipPlane = 100f;
        cam.backgroundColor = new Color(0.55f, 0.7f, 0.85f); // Sky blue
        cam.clearFlags = CameraClearFlags.SolidColor;
        camGO.AddComponent<HD2DCameraController>();

        float tiltAngle = 35f;
        float dist = 12f;
        camGO.transform.position = mouseGO.transform.position + new Vector3(0, Mathf.Sin(tiltAngle * Mathf.Deg2Rad) * dist, -Mathf.Cos(tiltAngle * Mathf.Deg2Rad) * dist);
        camGO.transform.rotation = Quaternion.Euler(tiltAngle, 0, 0);

        // --- Post-Processing ---
        var volumeGO = new GameObject("PostProcessing Volume");
        volumeGO.AddComponent<UnityEngine.Rendering.Volume>().isGlobal = true;

        // Save
        EnsureDirectory("Assets/Scenes");
        EditorSceneManager.SaveScene(scene, "Assets/Scenes/Overworld.unity");

        Debug.Log("[SceneBuilder] Overworld scene built and saved to Assets/Scenes/Overworld.unity");
        Debug.Log("[SceneBuilder] TODO: Wire up Hawk's TalkableNPC character name and dialogue");
        Debug.Log("[SceneBuilder] TODO: Set EnterBurrow SceneTransition target to 'Burrow'");
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
        SetMaterialColor(go, color);
        return go;
    }

    private static void SetMaterialColor(GameObject go, Color color)
    {
        var renderer = go.GetComponent<MeshRenderer>();
        if (renderer == null) return;
        var mat = new Material(Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard"));
        mat.SetColor(mat.HasProperty("_BaseColor") ? "_BaseColor" : "_Color", color);
        mat.SetFloat("_Smoothness", 0.1f);
        renderer.sharedMaterial = mat;
    }

    private static void CreateTree(Vector3 basePos, Transform parent, string name)
    {
        var treeParent = new GameObject(name);
        treeParent.transform.SetParent(parent);

        float height = Random.Range(3f, 6f);

        var trunk = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        trunk.name = name + "_Trunk";
        trunk.transform.position = basePos + Vector3.up * height / 2;
        trunk.transform.localScale = new Vector3(0.4f, height / 2, 0.4f);
        trunk.transform.SetParent(treeParent.transform);
        trunk.isStatic = true;
        SetMaterialColor(trunk, new Color(0.4f, 0.3f, 0.2f));

        var foliage = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        foliage.name = name + "_Foliage";
        foliage.transform.position = basePos + Vector3.up * (height + 1);
        float foliageSize = Random.Range(2.5f, 4f);
        foliage.transform.localScale = new Vector3(foliageSize, foliageSize * 0.7f, foliageSize);
        foliage.transform.SetParent(treeParent.transform);
        foliage.isStatic = true;
        SetMaterialColor(foliage, new Color(
            Random.Range(0.2f, 0.35f),
            Random.Range(0.38f, 0.52f),
            Random.Range(0.15f, 0.25f)
        ));
    }

    private static void CreateIvyPatch(Vector3 center, Vector3 size, string name, Transform parent)
    {
        var ivy = CreateCube(center + Vector3.up * size.y / 2, size, name, parent, new Color(0.15f, 0.3f, 0.12f));

        // Add a collider that blocks the player
        // The cube primitive already has a BoxCollider — it blocks by default

        // Add a slight emissive glow to make ivy patches visually distinct
        var renderer = ivy.GetComponent<MeshRenderer>();
        if (renderer != null && renderer.sharedMaterial != null)
        {
            renderer.sharedMaterial.EnableKeyword("_EMISSION");
            renderer.sharedMaterial.SetColor("_EmissionColor", new Color(0.05f, 0.1f, 0.03f));
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
