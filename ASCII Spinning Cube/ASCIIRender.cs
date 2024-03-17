using System.Text;

namespace ASCII_Spinning_Cube
{
	public class ASCIIRender
	{
		public void ResetCursorPosition()
		{
			Console.SetCursorPosition(0, 0);
		}

		public void OutputCube(int fieldWidth, int fieldHeight, char[] buffer)
		{
			StringBuilder result = new StringBuilder();

			for (int i = 1; i <= fieldWidth * fieldHeight; i++)
			{
				result.Append(buffer[i - 1]);

				if (i % fieldWidth == 0)
				{
					result.Append(Environment.NewLine);
				}
			}

			Console.WriteLine(result.ToString());
		}
	}
}
