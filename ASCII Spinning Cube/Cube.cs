using System.Text;

namespace ASCII_Spinning_Cube
{
	public class Cube
	{
		private int fieldWidth = 100;
		private int fieldHeight = 26;
		private char[] buffer;
		private float[] zBuffer;
		private ConsoleKey rotationDirection;
		private ManualResetEvent stopSignal;

		private int angleX = 0;
		private int angleY = 0;
		private int angleZ = 0;

		private int sideLength;
		private readonly int offset = 10;
		public Cube(int sideLength, ManualResetEvent stopSignal)
		{
			this.sideLength = sideLength;
			this.buffer = new char[fieldWidth * fieldHeight];
			this.zBuffer = new float[fieldWidth * fieldHeight];
			this.stopSignal = stopSignal;
		}

		public void Draw()
		{
			ClearBuffers();
			ResetCursorPosition();
			//AddSurfaces();
			AddVerticles();
			OutputCube();
			IncrementAngle();

			if (!stopSignal.WaitOne(0))
			{
				Thread.Sleep(1000 / 75);
				Draw();
			}
		}

		private void IncrementAngle()
		{
			angleX += 1;
		}

		private void ResetCursorPosition()
		{
			Console.SetCursorPosition(0, 0);
		}

		private void AddVerticles()
		{
			buffer[100 + angleX] = '@';
			buffer[200] = '@';
			buffer[300] = '@';
			buffer[400] = '@';
		}

		private void OutputCube()
		{
			StringBuilder result = new StringBuilder();

			for (int i = 1; i <= fieldWidth * fieldHeight; i++)
			{
				result.Append(buffer[i-1]);

				if (i % fieldWidth == 0) {
					result.Append(Environment.NewLine);
				}
			}

			Console.WriteLine(result.ToString());
		}

		private void ClearBuffers()
		{
			for (int i = 0; i < buffer.Length; i++)
			{
				buffer[i] = '.';
				zBuffer[i] = 0;
			}
		}

		public void changeDirection(ConsoleKey direction)
		{
			rotationDirection = direction;
		}
	}	
}