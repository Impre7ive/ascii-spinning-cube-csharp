namespace ASCII_Spinning_Cube
{
	public class Program
	{
		private static ManualResetEvent stopSignal = new ManualResetEvent(false);
		private const int CubeSideLength = 10;
		static void Main(string[] args)
		{
			var cube = new Cube(CubeSideLength, stopSignal);
			var cubeThread = new Thread(new ThreadStart(cube.draw));
			cubeThread.Start();

			ConsoleKey key;

			do
			{
				key = Console.ReadKey(true).Key;

				switch (key)
				{
					case ConsoleKey.UpArrow:
						cube.changeDirection(ConsoleKey.UpArrow); 
						break;
					case ConsoleKey.DownArrow:
						cube.changeDirection(ConsoleKey.DownArrow);
						break;					
					case ConsoleKey.LeftArrow:
						cube.changeDirection(ConsoleKey.LeftArrow);
						break;					
					case ConsoleKey.RightArrow:
						cube.changeDirection(ConsoleKey.RightArrow);
						break;
				}
			} 
			while (key != ConsoleKey.Escape);

			stopSignal.Set();
			cubeThread.Join();
		}
	}
}