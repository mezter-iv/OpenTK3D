using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace OpenTK3D.Graphics
{
    internal class ShaderProgram
    {
        public int ID;
        public ShaderProgram(string vertexShaderfilePath, string fragmentShaderfilePath) {
            ID = GL.CreateProgram();

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, LoadShaderSourse(vertexShaderfilePath));
            GL.CompileShader(vertexShader);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, LoadShaderSourse(fragmentShaderfilePath));
            GL.CompileShader(fragmentShader);

            GL.AttachShader(ID, vertexShader);
            GL.AttachShader(ID, fragmentShader);

            GL.LinkProgram(ID);

            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }
        public void Bind() {
            GL.UseProgram(ID);
        }
        public void UnBind() {
            GL.UseProgram(0);
        }
        public void Delete()
        {
            GL.DeleteShader(ID);
        }
        public static string LoadShaderSourse(string fp)
        {
            string shaderSourse = "";

            try
            {
                using (var r = new StreamReader("../../../Shaders/" + fp))
                {
                    shaderSourse = r.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            return shaderSourse;
        }
    }
}
