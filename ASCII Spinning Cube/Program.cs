namespace ASCII_Spinning_Cube
{
	public class Program
	{
		private static ManualResetEvent stopSignal = new ManualResetEvent(false);
		static void Main(string[] args)
		{
			Console.CursorVisible = false;

			var cube = new Cube(stopSignal);
			var cubeThread = new Thread(new ThreadStart(cube.Draw));
			cubeThread.Start();

			ConsoleKey key;

			do
			{
				key = Console.ReadKey(true).Key;

				switch (key)
				{				
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