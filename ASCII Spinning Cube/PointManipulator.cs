using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Spinning_Cube
{
	public class PointManipulator
	{
		public double RotateX(Point p, float angleX, float angleY, float angleZ)
		{
			return p.X * Math.Cos(angleY) * Math.Cos(angleZ) +
				   p.Y * (Math.Cos(angleX) * Math.Sin(angleZ) - Math.Sin(angleX) * Math.Sin(angleY) * Math.Cos(angleZ)) +
				   p.Z * (Math.Sin(angleX) * Math.Sin(angleZ) + Math.Cos(angleX) * Math.Sin(angleY) * Math.Cos(angleZ));
		}

		public double RotateY(Point p, float angleX, float angleY, float angleZ)
		{
			return p.X * (Math.Sin(angleX) * Math.Sin(angleY) * Math.Cos(angleZ) + Math.Cos(angleX) * Math.Sin(angleZ)) +
				   p.Y * (Math.Cos(angleX) * Math.Cos(angleZ) - Math.Sin(angleX) * Math.Sin(angleY) * Math.Sin(angleZ)) +
				   p.Z * Math.Cos(angleY) * Math.Sin(angleZ);
		}

		public double RotateZ(Point p, float angleX, float angleY, float angleZ)
		{
			return p.X * (-Math.Sin(angleY)) +
				   p.Y * (Math.Sin(angleX) * Math.Cos(angleY)) +
				   p.Z * (Math.Cos(angleX) * Math.Cos(angleY));
		}
		public int ConvertToScreenX(double x, double z, int fieldWidth, int scaleCoefficient)
		{
			return (int)(fieldWidth / 2 + x * 1 / z * scaleCoefficient * 2);
		}

		public int ConvertToScreenY(double y, double z, int fieldHeight, int scaleCoefficient)
		{
			return (int)(fieldHeight / 2 + y * 1 / z * scaleCoefficient);
		}
	}
}
