using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Represents a utility class for creating and managing bounding shapes (e.g., cube, cylinder)
/// around a set of objects. Provides functionality for initializing, creating grids,
/// and calculating bounds to enclose the target data.
/// </summary>
public class BoundingShape : MonoBehaviour
{
    private GameObject boundingCube;
    private GameObject dataParent;

    /// <summary>
    /// Initializes the bounding shape with the specified data parent and shape type.
    /// </summary>
    /// <param name="dataParentObject">The parent object containing the data.</param>
    /// <param name="shape">The type of shape to create (Cube, Cylinder, Sphere).</param>
    public void Initialize(GameObject dataParentObject, Shape shape)
    {
        dataParent = dataParentObject;
        CreateBoundingShape(shape);
    }

    /// <summary>
    /// Creates a 3D grid using planes on the bottom, back, and right sides of the bounding cube.
    /// </summary>
    public void Create3DGrid()
    {
        CreateBottomPlane();
        CreateRightPlane();
        CreateBackPlane();
    }

    public enum Shape
    {
        Cube,
        Cylinder,
        Sphere,
    }

    /// <summary>
    /// Creates the bounding shape enclosing the data.
    /// </summary>
    /// <param name="shape">The type of bounding shape to create.</param>
    private void CreateBoundingShape(Shape shape)
    {
        if (boundingCube != null) Destroy(boundingCube);

        boundingCube = shape switch
        {
            Shape.Cube => GameObject.CreatePrimitive(PrimitiveType.Cube),
            Shape.Cylinder => GameObject.CreatePrimitive(PrimitiveType.Cylinder),
            _ => null
        };
        boundingCube.name = $"Bounding {shape}";

        var transparentMaterial = new Material(Shader.Find("Standard"))
        {
            color = new Color(0.0f, 0.0f, 0.0f, 0.0f),
            renderQueue = 3000
        };
        transparentMaterial.SetFloat("_Mode", 3);
        transparentMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        transparentMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        transparentMaterial.SetInt("_ZWrite", 0);
        transparentMaterial.DisableKeyword("_ALPHATEST_ON");
        transparentMaterial.EnableKeyword("_ALPHABLEND_ON");
        transparentMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");

        boundingCube.GetComponent<Renderer>().material = transparentMaterial;
        boundingCube.layer = LayerMask.NameToLayer("Grabbable Box");

        switch (shape)
        {
            case Shape.Cube:
                boundingCube.AddComponent<BoxCollider>();
                break;
            case Shape.Cylinder:
                boundingCube.AddComponent<CapsuleCollider>();
                break;
        }

        boundingCube.AddComponent<XRGrabInteractable>();
        var rigidBody = boundingCube.GetComponent<Rigidbody>();
        rigidBody.useGravity = false;
        rigidBody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

        Bounds dataBounds = CalculateDataBounds();
        boundingCube.transform.position = dataBounds.center;
        boundingCube.transform.localScale = shape switch
        {
            Shape.Cube => dataBounds.size + Vector3.one * 0.01f,
            Shape.Cylinder => new Vector3(
                Mathf.Max(dataBounds.size.x, dataBounds.size.z),
                dataBounds.size.y / 2.0f,
                Mathf.Max(dataBounds.size.x, dataBounds.size.z)),
            _ => boundingCube.transform.localScale
        };

        dataParent.transform.SetParent(boundingCube.transform, true);
    }

    /// <summary>
    /// Calculates the combined bounds of all renderers within the data parent.
    /// </summary>
    private Bounds CalculateDataBounds()
    {
        Bounds bounds = new(dataParent.transform.position, Vector3.zero);
        foreach (Transform barParent in dataParent.transform)
        {
            foreach (Renderer childRenderer in barParent.GetComponentsInChildren<Renderer>())
            {
                bounds.Encapsulate(childRenderer.bounds);
            }
        }
        return bounds;
    }

    /// <summary>
    /// Creates the bottom plane of the bounding cube.
    /// </summary>
    public void CreateBottomPlane()
    {
        GameObject planeParent = new("Bottom Plane");
        planeParent.transform.SetParent(boundingCube.transform);

        Vector3 cubeCenter = boundingCube.transform.position;
        Vector3 halfScale = boundingCube.transform.localScale / 2.0f;

        Vector3 zStart = cubeCenter + new Vector3(-halfScale.x, -halfScale.y, -halfScale.z);
        Vector3 zEnd = cubeCenter + new Vector3(-halfScale.x, -halfScale.y, halfScale.z);
        Vector3 xStart = cubeCenter + new Vector3(-halfScale.x, -halfScale.y, -halfScale.z);
        Vector3 xEnd = cubeCenter + new Vector3(halfScale.x, -halfScale.y, -halfScale.z);

        Vector3 position = new(
            (xStart.x + xEnd.x) / 2.0f,
            cubeCenter.y - halfScale.y,
            (zStart.z + zEnd.z) / 2.0f
        );

        CreateSolidPlane(planeParent.transform, xStart, xEnd, zStart, zEnd, Quaternion.identity, position);
    }

    /// <summary>
    /// Creates the back plane of the bounding cube.
    /// </summary>
    public void CreateBackPlane()
    {
        GameObject planeParent = new("Back Plane");
        planeParent.transform.SetParent(boundingCube.transform);

        Vector3 cubeCenter = boundingCube.transform.position;
        Vector3 halfScale = boundingCube.transform.localScale / 2.0f;

        Vector3 xStart = cubeCenter + new Vector3(-halfScale.x, -halfScale.y, halfScale.z);
        Vector3 xEnd = cubeCenter + new Vector3(halfScale.x, -halfScale.y, halfScale.z);
        Vector3 yStart = cubeCenter + new Vector3(-halfScale.x, -halfScale.y, halfScale.z);
        Vector3 yEnd = cubeCenter + new Vector3(-halfScale.x, halfScale.y, halfScale.z);

        Vector3 position = new(
            (xStart.x + xEnd.x) / 2.0f,
            (yStart.y + yEnd.y) / 2.0f,
            cubeCenter.z + halfScale.z
        );

        CreateSolidPlane(planeParent.transform, yStart, yEnd, xStart, xEnd, Quaternion.Euler(0, 90, 90), position);
    }

    /// <summary>
    /// Creates the right plane of the bounding cube.
    /// </summary>
    public void CreateRightPlane()
    {
        GameObject planeParent = new("Right Plane");
        planeParent.transform.SetParent(boundingCube.transform);

        Vector3 cubeCenter = boundingCube.transform.position;
        Vector3 halfScale = boundingCube.transform.localScale / 2.0f;

        Vector3 zStart = cubeCenter + new Vector3(halfScale.x, -halfScale.y, -halfScale.z);
        Vector3 zEnd = cubeCenter + new Vector3(halfScale.x, -halfScale.y, halfScale.z);
        Vector3 yStart = cubeCenter + new Vector3(halfScale.x, -halfScale.y, halfScale.z);
        Vector3 yEnd = cubeCenter + new Vector3(halfScale.x, halfScale.y, halfScale.z);

        Vector3 position = new(
            cubeCenter.x + halfScale.x,
            (yStart.y + yEnd.y) / 2.0f,
            (zStart.z + zEnd.z) / 2.0f
        );

        CreateSolidPlane(planeParent.transform, zStart, zEnd, yStart, yEnd, Quaternion.Euler(90, 90, 0), position);
    }

    /// <summary>
    /// Creates a solid plane with the specified dimensions and orientation.
    /// </summary>
    private void CreateSolidPlane(Transform parent, Vector3 start, Vector3 end, Vector3 zStart, Vector3 zEnd, Quaternion rotation, Vector3 position)
    {
        float width = Vector3.Distance(start, end);
        float height = Vector3.Distance(zStart, zEnd);

        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Cube);
        plane.transform.SetParent(parent);
        plane.transform.position = position;
        plane.transform.localScale = new Vector3(width, 0.01f, height);
        plane.transform.rotation = rotation;

        plane.GetComponent<Renderer>().material.mainTexture = Resources.Load<Texture2D>("Textures/GrayGrid");
    }

    /// <summary>
    /// Gets the transform of the bounding shape.
    /// </summary>
    public Transform Transform => boundingCube != null ? boundingCube.transform : null;
}
