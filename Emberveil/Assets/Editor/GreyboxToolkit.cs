#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

/// <summary>
/// Editor toolkit for building grey-box scenes in the HD-2D style.
/// Creates floors, walls, stairs, and room layouts using primitive meshes.
/// Run via menu: Emberveil > Greybox Toolkit
///
/// ProBuilder can be used alongside this for more complex shapes,
/// but this tool handles the common modular pieces quickly.
///
/// All pieces use MeshCollider or BoxCollider for physics.
/// </summary>
public class GreyboxToolkit : EditorWindow
{
    // Room settings
    private int roomWidth = 10;
    private int roomDepth = 10;
    private float wallHeight = 3f;
    private float tileSize = 1f;
    private bool addCeiling = false;

    // Door settings
    private bool addDoorNorth = false;
    private bool addDoorSouth = true;
    private bool addDoorEast = false;
    private bool addDoorWest = false;
    private float doorWidth = 2f;
    private float doorHeight = 2.5f;

    // Stair settings
    private int stairSteps = 8;
    private float stairWidth = 2f;

    // Material
    private Material greyboxMaterial;

    [MenuItem("Emberveil/Greybox Toolkit")]
    public static void ShowWindow()
    {
        GetWindow<GreyboxToolkit>("Greybox Toolkit");
    }

    private void OnGUI()
    {
        GUILayout.Label("Emberveil Greybox Toolkit", EditorStyles.boldLabel);
        GUILayout.Space(5);

        greyboxMaterial = (Material)EditorGUILayout.ObjectField("Greybox Material", greyboxMaterial, typeof(Material), false);
        GUILayout.Space(10);

        // --- Room Builder ---
        GUILayout.Label("Room Builder", EditorStyles.boldLabel);
        roomWidth = EditorGUILayout.IntSlider("Width (X)", roomWidth, 3, 50);
        roomDepth = EditorGUILayout.IntSlider("Depth (Z)", roomDepth, 3, 50);
        wallHeight = EditorGUILayout.Slider("Wall Height", wallHeight, 2f, 8f);
        addCeiling = EditorGUILayout.Toggle("Add Ceiling", addCeiling);

        GUILayout.Space(5);
        GUILayout.Label("Doors", EditorStyles.miniLabel);
        addDoorNorth = EditorGUILayout.Toggle("North Door (+Z)", addDoorNorth);
        addDoorSouth = EditorGUILayout.Toggle("South Door (-Z)", addDoorSouth);
        addDoorEast = EditorGUILayout.Toggle("East Door (+X)", addDoorEast);
        addDoorWest = EditorGUILayout.Toggle("West Door (-X)", addDoorWest);
        doorWidth = EditorGUILayout.Slider("Door Width", doorWidth, 1f, 4f);
        doorHeight = EditorGUILayout.Slider("Door Height", doorHeight, 2f, wallHeight);

        GUILayout.Space(5);
        if (GUILayout.Button("Build Room", GUILayout.Height(30)))
        {
            BuildRoom();
        }

        GUILayout.Space(15);

        // --- Individual Pieces ---
        GUILayout.Label("Individual Pieces", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Floor Tile"))
        {
            CreateFloorTile(Vector3.zero);
        }
        if (GUILayout.Button("Wall Segment"))
        {
            CreateWallSegment(Vector3.zero, wallHeight, Vector3.forward);
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Stair Block"))
        {
            CreateStairBlock(Vector3.zero);
        }
        if (GUILayout.Button("Pillar"))
        {
            CreatePillar(Vector3.zero, wallHeight);
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Ramp"))
        {
            CreateRamp(Vector3.zero, 4f, wallHeight);
        }
        if (GUILayout.Button("Platform"))
        {
            CreatePlatform(Vector3.zero, 3f, 3f, 0.3f);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(15);

        // --- Outdoor Terrain ---
        GUILayout.Label("Outdoor Pieces", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Ground Plane (20x20)"))
        {
            CreateGroundPlane(Vector3.zero, 20, 20);
        }
        if (GUILayout.Button("Ground Plane (50x50)"))
        {
            CreateGroundPlane(Vector3.zero, 50, 50);
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Rock (Small)"))
        {
            CreateRock(Vector3.zero, 0.5f);
        }
        if (GUILayout.Button("Rock (Large)"))
        {
            CreateRock(Vector3.zero, 1.5f);
        }
        if (GUILayout.Button("Tree Trunk"))
        {
            CreateTreeTrunk(Vector3.zero, 4f);
        }
        GUILayout.EndHorizontal();
    }

    /// <summary>
    /// Build a complete room with floor, walls, and optional doors/ceiling
    /// </summary>
    private void BuildRoom()
    {
        var parent = new GameObject("Room_Greybox");
        Undo.RegisterCreatedObjectUndo(parent, "Create Room");

        float w = roomWidth * tileSize;
        float d = roomDepth * tileSize;
        float h = wallHeight;

        // Floor
        var floor = CreateCube(new Vector3(w / 2, -0.05f, d / 2), new Vector3(w, 0.1f, d), "Floor", parent.transform);

        // Walls
        // North wall (+Z)
        if (addDoorNorth)
        {
            CreateWallWithDoor(new Vector3(w / 2, 0, d), new Vector3(w, h, 0.2f), doorWidth, doorHeight, "Wall_North", parent.transform);
        }
        else
        {
            CreateCube(new Vector3(w / 2, h / 2, d), new Vector3(w, h, 0.2f), "Wall_North", parent.transform);
        }

        // South wall (-Z)
        if (addDoorSouth)
        {
            CreateWallWithDoor(new Vector3(w / 2, 0, 0), new Vector3(w, h, 0.2f), doorWidth, doorHeight, "Wall_South", parent.transform);
        }
        else
        {
            CreateCube(new Vector3(w / 2, h / 2, 0), new Vector3(w, h, 0.2f), "Wall_South", parent.transform);
        }

        // East wall (+X)
        if (addDoorEast)
        {
            CreateWallWithDoor(new Vector3(w, 0, d / 2), new Vector3(0.2f, h, d), doorWidth, doorHeight, "Wall_East", parent.transform, true);
        }
        else
        {
            CreateCube(new Vector3(w, h / 2, d / 2), new Vector3(0.2f, h, d), "Wall_East", parent.transform);
        }

        // West wall (-X)
        if (addDoorWest)
        {
            CreateWallWithDoor(new Vector3(0, 0, d / 2), new Vector3(0.2f, h, d), doorWidth, doorHeight, "Wall_West", parent.transform, true);
        }
        else
        {
            CreateCube(new Vector3(0, h / 2, d / 2), new Vector3(0.2f, h, d), "Wall_West", parent.transform);
        }

        // Ceiling
        if (addCeiling)
        {
            CreateCube(new Vector3(w / 2, h, d / 2), new Vector3(w, 0.1f, d), "Ceiling", parent.transform);
        }

        Selection.activeGameObject = parent;
        Debug.Log($"[Greybox] Room created: {roomWidth}x{roomDepth}, height {wallHeight}");
    }

    /// <summary>
    /// Create a wall with a door opening in the center
    /// </summary>
    private void CreateWallWithDoor(Vector3 basePos, Vector3 fullSize, float dWidth, float dHeight, string name, Transform parent, bool doorAlongZ = false)
    {
        var doorParent = new GameObject(name);
        doorParent.transform.SetParent(parent);

        float wallLength = doorAlongZ ? fullSize.z : fullSize.x;
        float wallThickness = doorAlongZ ? fullSize.x : fullSize.z;
        float h = fullSize.y;

        float sideLength = (wallLength - dWidth) / 2f;

        if (doorAlongZ)
        {
            // Left segment
            if (sideLength > 0)
                CreateCube(basePos + new Vector3(0, h / 2, -wallLength / 2 + sideLength / 2), new Vector3(wallThickness, h, sideLength), name + "_Left", doorParent.transform);
            // Right segment
            if (sideLength > 0)
                CreateCube(basePos + new Vector3(0, h / 2, wallLength / 2 - sideLength / 2), new Vector3(wallThickness, h, sideLength), name + "_Right", doorParent.transform);
            // Top (above door)
            if (h - dHeight > 0)
                CreateCube(basePos + new Vector3(0, dHeight + (h - dHeight) / 2, 0), new Vector3(wallThickness, h - dHeight, dWidth), name + "_Top", doorParent.transform);
        }
        else
        {
            // Left segment
            if (sideLength > 0)
                CreateCube(basePos + new Vector3(-wallLength / 2 + sideLength / 2, h / 2, 0), new Vector3(sideLength, h, wallThickness), name + "_Left", doorParent.transform);
            // Right segment
            if (sideLength > 0)
                CreateCube(basePos + new Vector3(wallLength / 2 - sideLength / 2, h / 2, 0), new Vector3(sideLength, h, wallThickness), name + "_Right", doorParent.transform);
            // Top (above door)
            if (h - dHeight > 0)
                CreateCube(basePos + new Vector3(0, dHeight + (h - dHeight) / 2, 0), new Vector3(dWidth, h - dHeight, wallThickness), name + "_Top", doorParent.transform);
        }
    }

    // --- Individual Piece Creators ---

    private void CreateFloorTile(Vector3 pos)
    {
        var go = CreateCube(pos + new Vector3(0.5f, -0.05f, 0.5f), new Vector3(tileSize, 0.1f, tileSize), "FloorTile");
        Selection.activeGameObject = go;
    }

    private void CreateWallSegment(Vector3 pos, float height, Vector3 facing)
    {
        var go = CreateCube(pos + Vector3.up * height / 2, new Vector3(tileSize, height, 0.2f), "WallSegment");
        Selection.activeGameObject = go;
    }

    private void CreateStairBlock(Vector3 pos)
    {
        var parent = new GameObject("Staircase");
        Undo.RegisterCreatedObjectUndo(parent, "Create Staircase");
        parent.transform.position = pos;

        float stepHeight = wallHeight / stairSteps;
        float stepDepth = stairWidth / stairSteps;

        for (int i = 0; i < stairSteps; i++)
        {
            float y = stepHeight * (i + 0.5f);
            float z = stepDepth * i;
            CreateCube(
                pos + new Vector3(0, y, z + stepDepth / 2),
                new Vector3(stairWidth, stepHeight, stepDepth),
                $"Step_{i}",
                parent.transform
            );
        }

        Selection.activeGameObject = parent;
    }

    private void CreatePillar(Vector3 pos, float height)
    {
        var go = CreateCube(pos + Vector3.up * height / 2, new Vector3(0.4f, height, 0.4f), "Pillar");
        Selection.activeGameObject = go;
    }

    private void CreateRamp(Vector3 pos, float length, float height)
    {
        // Approximate ramp with a rotated cube
        var go = new GameObject("Ramp");
        Undo.RegisterCreatedObjectUndo(go, "Create Ramp");

        var mesh = go.AddComponent<MeshFilter>();
        var renderer = go.AddComponent<MeshRenderer>();
        go.AddComponent<MeshCollider>();

        // Create wedge mesh
        mesh.sharedMesh = CreateWedgeMesh(length, height, 2f);

        if (greyboxMaterial != null)
            renderer.sharedMaterial = greyboxMaterial;

        go.transform.position = pos;
        Selection.activeGameObject = go;
    }

    private void CreatePlatform(Vector3 pos, float width, float depth, float height)
    {
        var go = CreateCube(pos + Vector3.up * height / 2, new Vector3(width, height, depth), "Platform");
        Selection.activeGameObject = go;
    }

    private void CreateGroundPlane(Vector3 pos, float width, float depth)
    {
        var go = CreateCube(pos + new Vector3(width / 2, -0.05f, depth / 2), new Vector3(width, 0.1f, depth), "GroundPlane");
        // Color it slightly green for outdoor
        var renderer = go.GetComponent<MeshRenderer>();
        if (renderer != null && greyboxMaterial == null)
        {
            var mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            mat.SetColor("_BaseColor", new Color(0.45f, 0.55f, 0.35f));
            renderer.sharedMaterial = mat;
        }
        Selection.activeGameObject = go;
    }

    private void CreateRock(Vector3 pos, float radius)
    {
        var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.name = "Rock";
        go.transform.position = pos + Vector3.up * radius * 0.4f;
        go.transform.localScale = new Vector3(radius * 2, radius * 1.2f, radius * 1.8f);
        // Slight random rotation for variety
        go.transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
        Undo.RegisterCreatedObjectUndo(go, "Create Rock");

        if (greyboxMaterial != null)
            go.GetComponent<MeshRenderer>().sharedMaterial = greyboxMaterial;

        Selection.activeGameObject = go;
    }

    private void CreateTreeTrunk(Vector3 pos, float height)
    {
        var go = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        go.name = "TreeTrunk";
        go.transform.position = pos + Vector3.up * height / 2;
        go.transform.localScale = new Vector3(0.6f, height / 2, 0.6f);
        Undo.RegisterCreatedObjectUndo(go, "Create Tree Trunk");

        if (greyboxMaterial != null)
            go.GetComponent<MeshRenderer>().sharedMaterial = greyboxMaterial;

        // Add a sphere for foliage
        var foliage = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        foliage.name = "Foliage";
        foliage.transform.SetParent(go.transform);
        foliage.transform.localPosition = new Vector3(0, 0.6f, 0);
        foliage.transform.localScale = new Vector3(5f, 3f, 5f);

        var foliageMat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        foliageMat.SetColor("_BaseColor", new Color(0.3f, 0.5f, 0.25f));
        foliage.GetComponent<MeshRenderer>().sharedMaterial = foliageMat;

        Selection.activeGameObject = go;
    }

    // --- Utility ---

    /// <summary>
    /// Create a cube primitive with given center position and size
    /// </summary>
    private GameObject CreateCube(Vector3 center, Vector3 size, string name, Transform parent = null)
    {
        var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.name = name;
        go.transform.position = center;
        go.transform.localScale = size;

        if (parent != null)
            go.transform.SetParent(parent);

        if (greyboxMaterial != null)
            go.GetComponent<MeshRenderer>().sharedMaterial = greyboxMaterial;

        Undo.RegisterCreatedObjectUndo(go, "Create " + name);
        return go;
    }

    /// <summary>
    /// Create a simple wedge mesh for ramps
    /// </summary>
    private Mesh CreateWedgeMesh(float length, float height, float width)
    {
        var mesh = new Mesh();
        mesh.name = "Wedge";

        float hw = width / 2;

        Vector3[] vertices = new Vector3[]
        {
            // Bottom face
            new Vector3(-hw, 0, 0),
            new Vector3(hw, 0, 0),
            new Vector3(-hw, 0, length),
            new Vector3(hw, 0, length),
            // Top edge (at far end)
            new Vector3(-hw, height, length),
            new Vector3(hw, height, length),
        };

        int[] triangles = new int[]
        {
            // Bottom
            0, 2, 1, 1, 2, 3,
            // Slope
            0, 1, 4, 1, 5, 4,
            // Back
            2, 4, 3, 3, 4, 5,
            // Left side
            0, 4, 2,
            // Right side
            1, 3, 5,
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }
}
#endif
