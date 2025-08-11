using System;
using OpenTK.Mathematics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK3D.Graphics;

namespace OpenTK3D.World
{
    internal class Chunk
    {
        private List<Vector3> chunkvertices;
        private List<Vector2> chunkuv;
        private List<uint> chunkid;
        const int sizew = 16;
        const int sizeh = 32;
        public Vector3 position;

        private uint indexcount;

        VAO ChunkVAO;
        VBO ChunkVBO;
        VBO CHunkUVVBO;
        IBO ChunkIBO;

        Texture texture;
        public Chunk(Vector3 position) { 
            this.position = position;
            chunkvertices = new List<Vector3>();
            chunkuv = new List<Vector2>();
            chunkid = new List<uint>();

            GenBlocks();
            BuildChunk();
        }
        public void GenChunk() { }
        public void GenBlocks() {
            for (int i = 0; i < 3; i++) {
                Block block = new Block(new Vector3(i, 0, 0));

                int facecount = 0;

                var frontFaceData = block.GetFace(Faces.FRONT);
                chunkvertices.AddRange(frontFaceData.vertices);
                chunkuv.AddRange(frontFaceData.uv);
                facecount++;

                if (i == 2) {
                    var rightFaceData = block.GetFace(Faces.RIGHT);
                    chunkvertices.AddRange(rightFaceData.vertices);
                    chunkuv.AddRange(rightFaceData.uv);
                }
                if (i == 0)
                {
                    var leftFaceData = block.GetFace(Faces.LEFT);
                    chunkvertices.AddRange(leftFaceData.vertices);
                    chunkuv.AddRange(leftFaceData.uv);
                    facecount++;
                }

                var backFaceData = block.GetFace(Faces.BACK);
                chunkvertices.AddRange(backFaceData.vertices);
                chunkuv.AddRange(backFaceData.uv);

                var topFaceData = block.GetFace(Faces.TOP);
                chunkvertices.AddRange(topFaceData.vertices);
                chunkuv.AddRange(topFaceData.uv);

                var bottomFaceData = block.GetFace(Faces.BOTTOM);
                chunkvertices.AddRange(bottomFaceData.vertices);
                chunkuv.AddRange(bottomFaceData.uv);

                facecount += 4;

                addindicves(facecount);
            }
        }
        public void BuildChunk() {
            ChunkVAO = new VAO();
            ChunkVAO.Bind();

            ChunkVBO = new VBO(chunkvertices);
            ChunkVBO.Bind();
            ChunkVAO.LinkToVAO(0, 3, ChunkVBO);

            CHunkUVVBO = new VBO(chunkuv);
            CHunkUVVBO.Bind();
            ChunkVAO.LinkToVAO(1, 2, CHunkUVVBO);

            ChunkIBO = new IBO(chunkid);

            texture = new Texture("DirtText.png");
        }
        public void Render(ShaderProgram program) { 
            program.Bind();
            ChunkVAO.Bind();
            ChunkIBO.Bind();
            texture.Bind();
            GL.DrawElements(PrimitiveType.Triangles, chunkid.Count, DrawElementsType.UnsignedInt, 0);
        }
        public void addindicves(int amount ) {
            for (int i = 0; i < amount; i++) {
                chunkid.Add(0 + indexcount);
                chunkid.Add(1 + indexcount);
                chunkid.Add(2 + indexcount);
                chunkid.Add(2 + indexcount);
                chunkid.Add(3 + indexcount);
                chunkid.Add(0 + indexcount);
                indexcount += 4;
            }
        }
        public void Delete() { 
            ChunkVAO.Delete();
            ChunkVBO.Delete();
            CHunkUVVBO.Delete();
            ChunkIBO.Delete();
        }
    }
}
