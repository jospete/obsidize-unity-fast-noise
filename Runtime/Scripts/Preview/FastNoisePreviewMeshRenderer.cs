using UnityEngine;

namespace Obsidize.FastNoise
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(MeshFilter))]
    public class FastNoisePreviewMeshRenderer : MonoBehaviour
    {

        [SerializeField] private FastNoiseModule _noiseModule;
        [SerializeField] private float _heightAmplifier = 3f;

        private MeshFilter _meshFilter;
        private PlaneMeshBuilder3D _meshBuilder;
        private FastNoiseContext _noiseContext;

		private void Update()
        {

            if (_noiseModule == null)
            {
                return;
            }

            if (_meshFilter == null)
			{
                _meshFilter = GetComponent<MeshFilter>();
            }

            if (_meshFilter == null)
			{
                return;
			}

            if (_meshBuilder == null)
			{
                _meshBuilder = new PlaneMeshBuilder3D();
            }

            if (_noiseContext == null)
			{
                _noiseContext = _noiseModule.CreateContext();
            }

            _noiseContext.SetSeed(_noiseModule.Preview.Seed);
            _meshBuilder.Resize(_noiseModule.Preview.TextureDimensions);

            _noiseModule.Preview.DrawPreviewHeightMap(_noiseContext, _meshBuilder, _heightAmplifier);
            _meshBuilder.ApplyTo(_meshFilter.sharedMesh);
            
            _meshFilter.sharedMesh.name = $"{_noiseModule.name} Gemerated Mesh";
        }
	}
}
