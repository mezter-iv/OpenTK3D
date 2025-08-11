using OpenTK.Compute.OpenCL;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK3D.Graphics;
using OpenTK3D.World;
using StbImageSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace OpenTK3D
{
    internal class Game : GameWindow
    {
        int width, height;

        Camera camera;

        float yRot = 0f;

        Chunk chunk;
        ShaderProgram program;

        public Game(int w, int h) : base(GameWindowSettings.Default, NativeWindowSettings.Default) {
            width = w;
            height = h;
            this.CenterWindow(new Vector2i(width, height));
        }
        protected override void OnLoad()
        {
            base.OnLoad();

            chunk = new Chunk(new Vector3(0, 0, 0));

            program = new ShaderProgram("Default.vert", "Default.frag");
           


            GL.Enable(EnableCap.DepthTest);

            camera = new Camera(width, height, Vector3.Zero);
            CursorState = CursorState.Grabbed;
        }
        protected override void OnUnload()
        {
            base.OnUnload();



        }
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
            width = e.Width;
            height = e.Height;
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            MouseState mouse = MouseState;
            KeyboardState keyboard = KeyboardState;

            base.OnUpdateFrame(args);
            camera.Update(keyboard, mouse, args);
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.ClearColor(0.6f, 0.3f, 1f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);


            Matrix4 model = Matrix4.Identity;
            Matrix4 view = camera.getViewMatrix();
            Matrix4 projection = camera.getProjectionMatrix();


            int modelLocation = GL.GetUniformLocation(program.ID, "model");
            int viewLocation = GL.GetUniformLocation(program.ID, "view");
            int projectionLocation = GL.GetUniformLocation(program.ID, "projection");

            GL.UniformMatrix4(modelLocation, true, ref model);
            GL.UniformMatrix4(viewLocation, true, ref view);
            GL.UniformMatrix4(projectionLocation, true, ref projection);

            chunk.Render(program);

            SwapBuffers();
            base.OnRenderFrame(args);
        }
    }
}
