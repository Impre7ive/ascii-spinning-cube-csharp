namespace ASCII_Spinning_Cube
{
	public class Cube
	{
		private int fieldWidth = 100;
		private int fieldHeight = 26;
		private int[] buffer;

		private int sideLength;
		private readonly int offset = 10;
		public Cube(int sideLength)
		{
			this.sideLength = sideLength;
			this.buffer = new int[fieldWidth * fieldHeight];
		}

		public void draw()
		{
			for (int i = 0; i < sideLength; i++)
			{
				for (int j = 0; j < 100; j++)
				{
					Console.Write('.');
				}

				Console.WriteLine();
			}			
		}
	}	
}