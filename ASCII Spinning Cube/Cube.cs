namespace ASCII_Spinning_Cube
{
	public class Cube
	{
		private int fieldWidth = 100;
		private int fieldHeight = 26;
		private int[] buffer;
		private ConsoleKey rotationDirection;
		private ManualResetEvent stopSignal;

		private int sideLength;
		private readonly int offset = 10;
		public Cube(int sideLength, ManualResetEvent stopSignal)
		{
			this.sideLength = sideLength;
			this.buffer = new int[fieldWidth * fieldHeight];
			this.stopSignal = stopSignal;
		}

		public void draw()
		{
			for (int i = 0; i < sideLength; i++)
			{
				for (int j = 0; j < 100; j++)
				{
					Console.Write('.');
				}
			}

			if (!stopSignal.WaitOne(0))
			{
				Thread.Sleep(1000 / 75);
				draw();
			}
		}

		public void changeDirection(ConsoleKey direction)
		{
			rotationDirection = direction;
		}
	}	
}