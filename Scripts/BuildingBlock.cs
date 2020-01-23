using System;
using UnityEngine;

namespace ModularBuildings
{
	[Serializable]
	public struct BlockDimension
	{
		public float Width { get; set; }
		public float Height { get; set; }
		public float Depth { get; set; }
		public Vector3 TopCenter { get; set; }
	}

	/// <summary>
	///     Custom Editor <see cref="BuildingBlockEditor" />
	/// </summary>
	public class BuildingBlock : MonoBehaviour
	{
		public bool _hasOverridingDimensionValue;
		public Vector3 _overridenBlockDimension;


		private const float DEFAULT_HEIGHT = 4;
		private const float DEFAULT_WIDTH = 4;
		private const float DEFAULT_DEPTH = 4;
		private readonly Vector3 DEFAULT_TOPCENTER = new Vector3(0, DEFAULT_HEIGHT, 0);
		public BlockDimension Dimension { get; private set; }
		public bool VerticalMirror = false;
		public bool HorizontalMirror = false;
		public bool IsTop = false;

		public string BlockType { get; set; }


		public string BlockName
		{
			get => name;
			set => name = value + $":{name}";
		}

		public void CalculateDimension()
		{
			Dimension = new BlockDimension() // TODO: Temporary Hack- Risul
			{
				Width = 4,
				Height = 3.5f,
				Depth = 4,
				TopCenter = new Vector3(0, 3.5f, 0)
			};
			return;
			/*
			if (_hasOverridingDimensionValue)
			{
				Dimension = new BlockDimension()
				{
					Width = _overridenBlockDimension.x,
					Height =  _overridenBlockDimension.y,
					Depth = _overridenBlockDimension.z,
					TopCenter = new Vector3(0,_overridenBlockDimension.y,0)
				};
				return;
			}

			if (GetComponent<MeshFilter>() == null)
				BlockType = BlockSymbol.Gap;
			// Default width and height for Gap type
			if (BlockType == BlockSymbol.Gap)
			{
				Dimension = new BlockDimension()
				{
					Width = DEFAULT_WIDTH,
					Height =  DEFAULT_HEIGHT,
					Depth = DEFAULT_DEPTH
				};
			}
			else
			{
				var mesh = GetComponent<MeshFilter>().sharedMesh;
				// Calculate height from Bounding area // TODO: This might give wrong value based on the mesh, Need a better way to solve - Risul
				var height = mesh.bounds.max.y - mesh.bounds.min.y;
				// Calculate Depth from bounding area's depth and pivot distance
				var depth = transform.position.z - mesh.bounds.min.z;
				// Top Get top center from the height
				var topCenter = new Vector3(0, height, 0);
				// Calculate Width- Scan all vertices left to right along the pivot

				var vertices = mesh.vertices;
				var maxX = Mathf.NegativeInfinity;
				var minX = Mathf.Infinity;
				foreach (var vertex in vertices)
				{
					var pos = transform.TransformPoint(vertex);
					if (!Mathf.Approximately(pos.y, transform.position.y))
						continue;
					if (pos.x > maxX)
						maxX = pos.x;
					if (pos.x < minX)
						minX = pos.x;
				}
				var width = maxX-minX;
				
				Dimension = new BlockDimension()
				{
					Width = width,
					Height = height,
					Depth = depth,
					TopCenter = topCenter
				};
			}*/
		}

		public void DestroyBlock()
		{
			DestroyImmediate(gameObject);
		}
	}
}
