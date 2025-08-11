using OpenTK.Graphics.ES11;
using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Mathematics;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace OpenTK3D.World
{
    internal class Block
    {
        public Vector3 position;
        private Dictionary<Faces, FaceData> faces;

        List<Vector2> dirtuv = new List<Vector2> {
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
        };
        public Block(Vector3 position) {
            this.position = position;
            faces = new Dictionary<Faces, FaceData> 
            {
                { Faces.FRONT, new FaceData {
                    vertices = AddTransf(FaceDataRaw.rawVertexData[Faces.FRONT]),
                    uv = dirtuv
                } },
                { Faces.BACK, new FaceData {
                    vertices = AddTransf(FaceDataRaw.rawVertexData[Faces.BACK]),
                    uv = dirtuv
                } },
                { Faces.LEFT, new FaceData {
                    vertices = AddTransf(FaceDataRaw.rawVertexData[Faces.LEFT]),
                    uv = dirtuv
                } },
                { Faces.RIGHT, new FaceData {
                    vertices = AddTransf(FaceDataRaw.rawVertexData[Faces.RIGHT]),
                    uv = dirtuv
                } },
                { Faces.TOP, new FaceData {
                    vertices = AddTransf(FaceDataRaw.rawVertexData[Faces.TOP]),
                    uv = dirtuv
                } },
                { Faces.BOTTOM, new FaceData {
                    vertices = AddTransf(FaceDataRaw.rawVertexData[Faces.BOTTOM]),
                    uv = dirtuv
                } },
            };
        }
        public List<Vector3> AddTransf(List<Vector3> vertices) { 
            List<Vector3> transfvert = new List<Vector3>();
            foreach (var v in vertices) { 
                transfvert.Add(v + position);
            }
            return transfvert;
        }
        public FaceData GetFace(Faces face) {
            return faces[face];
        }
    }
}
