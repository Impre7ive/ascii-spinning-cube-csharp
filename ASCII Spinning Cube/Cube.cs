using Microsoft.VisualBasic;
using System.Text;
using System.Threading;

namespace ASCII_Spinning_Cube
{
	public class Cube
	{
		private int fieldWidth = 100;
		private int fieldHeight = 28;
		private char[] buffer;
		private float[] zBuffer;

		private float angleX = 0;
		private float angleY = 0;
		private float angleZ = 0;
		private int scaleCoefficient = 20;
		private int sideLength = 20;
		private int cameraDistance = 90;

		private Dictionary<ConsoleKey, Action> rotateCube;
		private ConsoleKey rotationDirection = ConsoleKey.DownArrow;
		private ManualResetEvent stopSignal;
		private ASCIIRender renderer;
		private PointManipulator point;
		private Point[] vertices;

		public Cube(int sideLength, ManualResetEvent stopSignal)
		{
			this.sideLength = sideLength;
			this.buffer = new char[fieldWidth * fieldHeight];
			this.zBuffer = new float[fieldWidth * fieldHeight];
			this.stopSignal = stopSignal;
			this.renderer = new ASCIIRender();
			this.point = new PointManipulator();

			rotateCube = new Dictionary<ConsoleKey, Action> {
				{ConsoleKey.UpArrow, () => {this.angleY +=  0.05f; } },
				{ConsoleKey.DownArrow, () => {this.angleY -=  0.10f; } },
				{ConsoleKey.RightArrow, () => {this.angleX +=  0.05f; } },
				{ConsoleKey.LeftArrow, () => {this.angleX -=  0.05f; } },
			};

			vertices = new Point[] {
				new Point { X = -1, Y = -1, Z = -1},
				new Point { X = 1, Y = -1, Z = -1},
				new Point { X = -1, Y = 1, Z = -1},
				new Point { X = 1, Y = 1, Z = -1},
				new Point { X = -1, Y = -1, Z = 1},
				new Point { X = 1, Y = -1, Z = 1},
				new Point { X = -1, Y = 1, Z = 1},
				new Point { X = 1, Y = 1, Z = 1},
			};

			vertices = vertices.Select(p => { 
				p.X = p.X * sideLength;
				p.Y = p.Y * sideLength; 
				p.Z = p.Z * sideLength; 
				return p; 
				})
				.ToArray();
		}

		public void Draw()
		{
			ClearBuffers();
			renderer.ResetCursorPosition();

			for (int cubeX = -sideLength; cubeX < sideLength; cubeX++)
			{
				for (int cubeY = -sideLength; cubeY < sideLength; cubeY++)
				{
					AddSurfaces(new Point { X = cubeX, Y = cubeY, Z = -sideLength }, '=');
					AddSurfaces(new Point { X = sideLength, Y = cubeY, Z = cubeX }, '\\');
					AddSurfaces(new Point { X = -sideLength, Y = cubeY, Z = -cubeX }, '#');
					AddSurfaces(new Point { X = -cubeX, Y = cubeY, Z = sideLength }, '%');
					AddSurfaces(new Point { X = cubeX, Y = -sideLength, Z = -cubeY }, '&');
					AddSurfaces(new Point { X = cubeX, Y = sideLength, Z = cubeY }, '~');
				}
			}

			AddVerticles('+');
			renderer.OutputCube(fieldWidth, fieldHeight, buffer);
			IncrementAngle();

			if (!stopSignal.WaitOne(0))
			{
				Thread.Sleep(100);
				Draw();
			}
		}

		private void AddSurfaces(Point p, char ch)
		{
			var x = point.RotateX(p, angleX, angleY, angleZ);
			var y = point.RotateY(p, angleX, angleY, angleZ);
			var z = point.RotateZ(p, angleX, angleY, angleZ) + cameraDistance;

			var ooz = 1 / (z + cameraDistance);
			var screenX = point.ConvertToScreenX(x, z, fieldWidth, scaleCoefficient);
			var screenY = point.ConvertToScreenY(y, z, fieldHeight, scaleCoefficient);
			var index = screenY * fieldWidth + screenX;

			if (index >= 0 && index < buffer.Length)
			{
				if (ooz > zBuffer[index])
				{
					zBuffer[index] = (float)ooz;
					buffer[index] = ch;
				}
			}
		}

		private void AddVerticles(char ch)
		{
			for (int i = 0; i < vertices.Length; i++)
			{
				var x = point.RotateX(vertices[i], angleX, angleY, angleZ);
				var y = point.RotateY(vertices[i], angleX, angleY, angleZ);
				var z = point.RotateZ(vertices[i], angleX, angleY, angleZ) + cameraDistance;

				var ooz = 1 / (z + cameraDistance); //perspective factor
				var screenX = point.ConvertToScreenX(x, z, fieldWidth, scaleCoefficient);
				var screenY = point.ConvertToScreenY(y, z, fieldHeight, scaleCoefficient);
				int index = screenY * fieldWidth + screenX;

				if (index >= 0 && index < buffer.Length)
				{
					buffer[index] = '+';
				}
			}
		}
	
		private void IncrementAngle()
		{
			rotateCube[rotationDirection].Invoke();
		}

		private void ClearBuffers()
		{
			for (int i = 0; i < buffer.Length; i++)
			{
				buffer[i] = ' ';
				zBuffer[i] = 0;
			}
		}

		public void changeDirection(ConsoleKey direction)
		{
			rotationDirection = direction;
		}
	}	
}