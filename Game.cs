using System;
using OpenTK.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System.Runtime.InteropServices;
using System.Drawing;
using static CursWork.Form1;
using System.Drawing.Imaging;
using System.Diagnostics;



namespace CursWork
{

        public class Game : GameWindow
        {
            private int starTextureId;

            private Vector3 cameraPosition;
            private Vector3 cameraTarget;
            private Vector3 cameraUp;
            private float cameraYaw;
            private float cameraPitch;
            private float cameraMoveSpeed;
            private float cameraTurnSpeed;
            private bool isMouseCaptured;

            private bool isMouseScrolling;
            private float zoomSpeed = 0.1f;

            private int[] backgroundTextureIds = new int[6];
            private float textureScale = 20.0f; 

            private float distance = 35.0f;
            string imagePath = "Textures\\star2.png"; // Путь к файлу текстуры

            private Star[] stars;

        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
                : base(gameWindowSettings, nativeWindowSettings)
            {
                Console.WriteLine("Привет");
                VSync = VSyncMode.On;

            }

            private string getFullPath(string filePath)
        {
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
            return fullPath;
        }

        protected override void OnLoad()
            {

            base.OnLoad();
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);


            // Загрузка текстур фона
            backgroundTextureIds[0] = LoadTexture(getFullPath("Textures\\GalaxyTex_NegativeX.jpg"));
                backgroundTextureIds[1] = LoadTexture(getFullPath("Textures\\GalaxyTex_NegativeX.jpg"));
                backgroundTextureIds[2] = LoadTexture(getFullPath("Textures\\GalaxyTex_NegativeZ.jpg"));
                backgroundTextureIds[3] = LoadTexture(getFullPath("Textures\\GalaxyTex_PositiveX.jpg"));
                backgroundTextureIds[4] = LoadTexture(getFullPath("Textures\\GalaxyTex_PositiveY.jpg"));
                backgroundTextureIds[5] = LoadTexture(getFullPath("Textures\\GalaxyTex_PositiveZ.jpg"));

            

                cameraPosition = new Vector3(0.0f, 0.0f, 35.0f);
                cameraTarget = Vector3.Zero;
                cameraUp = Vector3.UnitY;
                cameraYaw = 0.0f;
                cameraPitch = 0.0f;
                cameraMoveSpeed = 0.1f;
                cameraTurnSpeed = 0.01f;
                isMouseCaptured = false;


                GL.Enable(EnableCap.DepthTest); 
                GL.DepthFunc(DepthFunction.Lequal); 
                                                    
            int starTextureId;


           
            starTextureId = GL.GenTexture();

            
            GL.BindTexture(TextureTarget.Texture2D, starTextureId);

            
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

           
            Bitmap starTextureImage = new Bitmap(getFullPath("Textures\\star2.png")); // Загрузка изображения из файла
                                                             
                                                             
            BitmapData data = starTextureImage.LockBits(new Rectangle(0, 0, starTextureImage.Width, starTextureImage.Height),
                                                        ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                          OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            starTextureImage.UnlockBits(data);
            this.starTextureId = starTextureId;

        }

        private int LoadTexture(string path)
        {
            int textureId = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, textureId);

            Bitmap textureBitmap = new Bitmap(path);
            BitmapData data = textureBitmap.LockBits(new Rectangle(0, 0, textureBitmap.Width, textureBitmap.Height),
                                                    ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                          OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            textureBitmap.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            return textureId;
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
            {
                base.OnMouseWheel(e);

                if (e.OffsetY > 0)
                {
                    distance -= 0.4f;
                }
                else if (e.OffsetY < 0)
                {
                    distance += 0.4f;
                }

                UpdateCamera();
            }
            private OpenTK.Mathematics.Vector2 mouseStartPosition;

            protected override void OnMouseDown(MouseButtonEventArgs e)
            {
                base.OnMouseDown(e);

                if (e.Button == MouseButton.Left)
                {
                    isMouseCaptured = true;
                    mouseStartPosition = new OpenTK.Mathematics.Vector2(MouseState.Position.X, MouseState.Position.Y);
                    //MouseCursorVisible = false;
                }
            }
           
            protected override void OnMouseUp(MouseButtonEventArgs e)
            {
                base.OnMouseUp(e);

                if (e.Button == MouseButton.Left)
                {
                    isMouseCaptured = false;
                }
            }

            
            protected override void OnMouseMove(MouseMoveEventArgs e)
            {
                base.OnMouseMove(e);

                if (isMouseCaptured)
                {
                    float deltaX = e.Position.X - mouseStartPosition.X;
                    float deltaY = e.Position.Y - mouseStartPosition.Y;

                   
                    cameraYaw += deltaX * cameraTurnSpeed;
                    cameraPitch -= deltaY * cameraTurnSpeed;

                    
                    if (cameraPitch > MathHelper.TwoPi)
                        cameraPitch -= MathHelper.TwoPi;
                    if (cameraPitch < 0)
                        cameraPitch += MathHelper.TwoPi;

                   
                    UpdateCamera();

                    mouseStartPosition = e.Position;
                }
            }

            protected override void OnResize(ResizeEventArgs e)
            {
                base.OnResize(e);
                GL.Viewport(0, 0, Size.X, Size.Y);
            }

            protected override void OnUpdateFrame(FrameEventArgs args)
            {
                ProcessInput();

                base.OnUpdateFrame(args);
            }

            private static float[] GenerateCircleVertices(int segments, float radius)
            {
                float[] vertices = new float[segments * 3];

                for (int i = 0; i < segments; i++)
                {
                    float angle = i * 2 * (float)Math.PI / segments;
                    float x = radius * (float)Math.Cos(angle);
                    float y = radius * (float)Math.Sin(angle);

                    vertices[i * 3] = x;
                    vertices[i * 3 + 1] = y;
                    vertices[i * 3 + 2] = 0;
                }

                return vertices;
            }
            protected override void OnRenderFrame(FrameEventArgs args)
            {
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                float aspectRatio = (float)Size.X / (float)Size.Y;
                Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), aspectRatio, 0.1f, 100.0f);
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadMatrix(ref perspective);

                Matrix4 scale = Matrix4.CreateScale(2.0f); 
                Matrix4 lookat = Matrix4.LookAt(cameraPosition, cameraTarget, cameraUp);
                Matrix4 modelview = scale * lookat;
                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadMatrix(ref modelview);

                GL.Viewport(0, 0, Size.X, Size.Y);
                
                GL.BindTexture(TextureTarget.Texture2D, starTextureId);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                GL.Enable(EnableCap.Texture2D);
             
                GL.Begin(PrimitiveType.Quads);



                if (stars != null)
                {
                    for (int j = 0; j < stars.Length; j++)
                    {
                        Star star = stars[j];
                        if(star == null)
                    {
                        continue;
                    }
                        double scaled = 3e20;//20
                        double x = star.x / scaled;

                        double y = star.y / scaled;
                        double starSize = 0.02;
                        GL.TexCoord2(0, 0); GL.Vertex3(-starSize + x , -starSize + y, 0);
                        GL.TexCoord2(1, 0); GL.Vertex3(starSize + x, -starSize + y, 0);
                        GL.TexCoord2(1, 1); GL.Vertex3(starSize + x, starSize + y, 0);
                        GL.TexCoord2(0, 1); GL.Vertex3(-starSize + x, starSize + y, 0);
                    }
                }


                    
                    // Нарисуйте текстуры фона
                    GL.BindTexture(TextureTarget.Texture2D, backgroundTextureIds[1]);
                    DrawQuad(-1, -1, -1, 0, MathHelper.PiOver2, 1); // Задняя грань

                    GL.BindTexture(TextureTarget.Texture2D, backgroundTextureIds[1]);
                    DrawQuad(-1, 1, 0, 1, MathHelper.PiOver2, 0); // Нижняя грань

                    GL.BindTexture(TextureTarget.Texture2D, backgroundTextureIds[2]);
                    DrawQuad(-1, 1, 1, 0, MathHelper.Pi, 1); // Передняя грань

                    GL.BindTexture(TextureTarget.Texture2D, backgroundTextureIds[3]);
                    DrawQuad(1, -1, 0, 0, MathHelper.PiOver2, 3); // Левая грань

                    GL.BindTexture(TextureTarget.Texture2D, backgroundTextureIds[4]);
                    DrawQuad(1, 1, 0, 0, 0, 4); // Правая грань

                    GL.BindTexture(TextureTarget.Texture2D, backgroundTextureIds[5]);
                    DrawQuad(-1, -1, 0, 0, MathHelper.PiOver2, 5); // Верхняя грань
                    GL.End();

                SwapBuffers();
                base.OnRenderFrame(args);
            }
        private void DrawQuad(float x, float y, float z, float angle, float rotation, int side)
        {

            GL.PushMatrix();
            GL.Translate(x, y, z);
            GL.Rotate(rotation, 0, 0, 1);
           
            GL.Begin(PrimitiveType.Quads);
            switch (side)
            {
                case 0: // Задняя сторона
                    GL.TexCoord2(0, 0); GL.Vertex3(-textureScale, -textureScale, -textureScale);
                    GL.TexCoord2(1, 0); GL.Vertex3(textureScale, -textureScale, -textureScale);
                    GL.TexCoord2(1, 1); GL.Vertex3(textureScale, textureScale, -textureScale);
                    GL.TexCoord2(0, 1); GL.Vertex3(-textureScale, textureScale, -textureScale);
                    break;
                case 1: // Нижняя сторона
                    GL.TexCoord2(0, 0); GL.Vertex3(-textureScale, -textureScale, textureScale);
                    GL.TexCoord2(1, 0); GL.Vertex3(textureScale, -textureScale, textureScale);
                    GL.TexCoord2(1, 1); GL.Vertex3(textureScale, -textureScale, -textureScale);
                    GL.TexCoord2(0, 1); GL.Vertex3(-textureScale, -textureScale, -textureScale);
                    break;
                case 2: // Передняя сторона
                    GL.TexCoord2(1, 0); GL.Vertex3(-textureScale, textureScale, textureScale);
                    GL.TexCoord2(0, 0); GL.Vertex3(textureScale, textureScale, textureScale);
                    GL.TexCoord2(0, 1); GL.Vertex3(textureScale, -textureScale, textureScale);
                    GL.TexCoord2(1, 1); GL.Vertex3(-textureScale, -textureScale, textureScale);
                    break;
                   
                case 3: // Левая сторона
                    GL.TexCoord2(1, 0); GL.Vertex3(-textureScale, -textureScale, textureScale);
                    GL.TexCoord2(1, 1); GL.Vertex3(-textureScale, -textureScale, -textureScale);
                    GL.TexCoord2(0, 1); GL.Vertex3(-textureScale, textureScale, -textureScale);
                    GL.TexCoord2(0, 0); GL.Vertex3(-textureScale, textureScale, textureScale);
                    break;
                case 4: // Правая сторона
                    GL.TexCoord2(0, 0); GL.Vertex3(textureScale, -textureScale, -textureScale);
                    GL.TexCoord2(1, 0); GL.Vertex3(textureScale, -textureScale, textureScale);
                    GL.TexCoord2(1, 1); GL.Vertex3(textureScale, textureScale, textureScale);
                    GL.TexCoord2(0, 1); GL.Vertex3(textureScale, textureScale, -textureScale);
                    break;
                case 5: // Верхняя сторона
                    GL.TexCoord2(0, 0); GL.Vertex3(-textureScale, textureScale, textureScale);
                    GL.TexCoord2(1, 0); GL.Vertex3(textureScale, textureScale, textureScale);
                    GL.TexCoord2(1, 1); GL.Vertex3(textureScale, textureScale, -textureScale);
                    GL.TexCoord2(0, 1); GL.Vertex3(-textureScale, textureScale, -textureScale);
                    break;
            }
            GL.End();

            GL.PopMatrix();
        }
      

        private void UpdateCamera()
            {
                float latitude = cameraPitch + MathHelper.PiOver2; // Угол между положительным направлением Y и вектором камеры
                float longitude = cameraYaw; // Угол между положительным направлением X и вектором камеры

                // Преобразование сферических координат в декартовы
                float y = distance * (float)Math.Sin(latitude);
                float xz = distance * (float)Math.Cos(latitude);
                float x = xz * (float)Math.Sin(longitude);
                float z = xz * (float)Math.Cos(longitude);

                // Обновление позиции камеры
                cameraPosition = new Vector3(x, y, z);
                cameraTarget = Vector3.Zero;
                cameraUp = Vector3.UnitY;
            }
            private void ProcessInput()
            {
                var keyboardState = KeyboardState;
                var mouseState = MouseState;

                // Управление поворотом камеры с помощью мыши
                if (isMouseCaptured)
                {
                    float deltaX = mouseState.Delta.X;
                    float deltaY = mouseState.Delta.Y;

                    cameraYaw += deltaX * cameraTurnSpeed;
                    cameraPitch -= deltaY * cameraTurnSpeed;

                    // Ограничение угла поворота камеры
                    // Ограничение угла поворота камеры
                    if (cameraPitch > MathHelper.TwoPi)
                        cameraPitch -= MathHelper.TwoPi;
                    if (cameraPitch < 0)
                        cameraPitch += MathHelper.TwoPi;

                    // Поворот камеры
                    UpdateCamera();
                }
            }

            protected override void OnUnload()
            {
                base.OnUnload();
                GL.DeleteTexture(starTextureId);
            }
        [DllImport("user32.dll")]
            public static extern bool ShowCursor(bool bShow);

        internal void setStars(Star[] stars)
        {
            this.stars = stars;
        }

    }
   
}
    

