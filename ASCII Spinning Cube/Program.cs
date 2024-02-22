namespace ASCII_Spinning_Cube
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var cubeSideLength = 10;
			var cube = new Cube(cubeSideLength);

			while (true)
			{
				cube.draw();
				Thread.Sleep(1000/75);
			}
		}
	}
}