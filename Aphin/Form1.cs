using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Aphin
{
	public partial class Form1 : Form
	{

		int xb, yb,
			xa, ya,
			xo, yo;

		public class Matrix
		{
			private double[,] matr;

			public int m;
			public int n;

			public Matrix(int m, int n)
			{
				this.m = m;
				this.n = n;
				this.matr = new double[m, n];
			}

			public Matrix(int m, int n, double x1, double y1, double h)
			{
				this.m = m;
				this.n = n;
				this.matr = new double[m, n];
				matr[0, 0] = x1;
				matr[0, 1] = y1;
				matr[0, 2] = h;
			}

			public double this[int x, int y]
			{
				get
				{
					return this.matr[x, y];
				}
				set
				{
					this.matr[x, y] = value;
				}
			}

			public static Matrix operator *(Matrix matrix, int value)
			{
				var result = new Matrix(matrix.m, matrix.n);
				for (int i = 0; i < matrix.m; i++)
				{
					for (int j = 0; j < matrix.n; i++)
						result[i, j] = matrix[i, j] * value;
				}
				return result;
			}
			public static Matrix operator *(Matrix matrix, Matrix matrix2)
			{
				if (matrix.n != matrix2.m)
				{
					throw new ArgumentException("It's not be multiplied");
				}
				var result = new Matrix(matrix.m, matrix2.n);
				for (var i = 0; i < matrix.m; i++)
				{
					for (var j = 0; j < matrix2.n; j++)
					{
						result[i, j] = 0;

						for (var k = 0; k < matrix.n; k++)
						{
							result[i, j] += matrix[i, k] * matrix2[k, j];
						}
					}
				}

				return result;
			}
		}
		public Form1()
		{
			InitializeComponent();
			g.Clear(Color.White);
		}

		int X0 = 0;
		int Y0 = 0;

		int X1 = 0;
		int Y1 = 0;

		int X2 = 0;
		int Y2 = 0;

		int X3 = 0;
		int Y3 = 0;

		int XPOINT = 0;
		int YPOINT = 0;

		int X0_SECOND = 0;
		int Y0_SECOND = 0;

		int X1_SECOND = 0;
		int Y1_SECOND = 0;


		static Bitmap bmp = new Bitmap(927, 706);

		bool mouse_Down = false;

		public Graphics g = Graphics.FromImage(bmp);

		Pen myPen = new Pen(Color.Black, 3f);

		Pen pen_fill = new Pen(Color.Black);
		private void pictureBox1_Click(object sender, EventArgs e)
		{

		}

		private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
		{
			mouse_Down = true;
			bmp.SetPixel(e.X, e.Y, Color.Black);

			if (!checkBox2.Checked)
			{
				if ((radioButton1.Checked || radioButton2.Checked) && !checkBox1.Checked)
				{
					this.X0 = e.X;
					this.Y0 = e.Y;
				}
				if (checkBox1.Checked)
				{
					XPOINT = e.X;
					YPOINT = e.Y;
				}
			}
			else
			{
				this.X0_SECOND = e.X;
				this.Y0_SECOND = e.Y;
			}
			if (radioButton1.Checked)
			{
				xo = e.X;
				yo = e.Y;
			}
			if (checkBox1.Checked)
			{
				XPOINT = e.X;
				YPOINT = e.Y;
				//MessageBox.Show(xb + " " + yb);
			}
			pictureBox1.Image = bmp;
		}

		private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
		{
			if (!checkBox2.Checked)
			{
				if (mouse_Down)
				{
					if (!checkBox1.Checked)
						g.Clear(Color.White);
					this.X1 = Math.Min(pictureBox1.Width - 1, Math.Max(e.X, 1));
					this.Y1 = Math.Min(pictureBox1.Height - 1, Math.Max(e.Y, 1));

					if (radioButton1.Checked && !checkBox1.Checked)
					{
						Line_Bres(X0, Y0, X1, Y1);
						xa = X1 - xo;
						ya = Y1 - yo;
					}

					else
						if (radioButton2.Checked && !checkBox1.Checked)
						Draw_square(ref X0, ref Y0, ref X1, ref Y1);
				}
			}
			else
			{
				this.X1_SECOND = e.X;
				this.Y1_SECOND = e.Y;
			}
		}

		private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
		{
			if (!checkBox2.Checked)
			{
				if (radioButton1.Checked && !checkBox1.Checked)
				{
					Line_Bres(X0, Y0, X1, Y1);
				}

				mouse_Down = false;
				if (mouse_Down && radioButton1.Checked && !checkBox1.Checked)
				{
					Line_Bres(X0, Y0, X1, Y1);

				}
				else if (mouse_Down && radioButton2.Checked && !checkBox1.Checked)
					Draw_square(ref X0, ref Y0, ref X1, ref Y1);
			}
			else
			{
				Line_Bres(X0_SECOND, Y0_SECOND, X1_SECOND, Y1_SECOND);
			}
		}

		static void Swap(ref int x, ref int y)
		{
			int t = x;
			x = y;
			y = t;
		}
		void Line_Bres(int x0, int y0, int x1, int y1)
		{
			int xtemp0 = x0;
			int xtemp1 = x1;
			int ytemp0 = y0;
			int ytemp1 = y1;
			if (Math.Abs(x1 - x0) < Math.Abs(y1 - y0))
			{
				Swap(ref x0, ref y0);
				Swap(ref x1, ref y1);
			}
			if (x0 > x1)
			{
				Swap(ref x0, ref x1);
				Swap(ref y0, ref y1);
			}
			int deltax = Math.Abs(x1 - x0);
			int deltay = Math.Abs(y1 - y0);
			int error = 0;
			int deltaerr = (deltay + 1);
			int y = y0;
			int diry = y1 - y0;
			if (diry > 0)
				diry = 1;
			if (diry < 0)
				diry = -1;
			for (int x = x0; x <= x1; x++)
			{
				bmp.SetPixel(Math.Abs(ytemp1 - ytemp0) > Math.Abs(xtemp1 - xtemp0) ? y : x, Math.Abs(ytemp1 - ytemp0) > Math.Abs(xtemp1 - xtemp0) ? x : y, Color.Black);
				error = error + deltaerr;
				if (error >= deltax + 1)
				{
					y += diry;
					error = error - (deltax + 1);
				}
			}
			pictureBox1.Image = bmp;
		}

		void Draw_square(ref int x0, ref int y0, ref int x1, ref int y1)
		{
			int Divx = x1 > x0 ? x1 - x0 : x0 - x1;
			int Divy = y1 > y0 ? y1 - y0 : y0 - y1;
			int Div_all = Divx > Divy ? Divy : Divx;
			x1 = x1 > x0 ? x0 + Div_all : x0 - Div_all;
			y1 = y1 > y0 ? y0 + Div_all : y0 - Div_all;
			int xtemp0 = x0;
			int xtemp1 = x1;
			int ytemp0 = y0;
			int ytemp1 = y1;

			if (xtemp1 + xtemp0 > ytemp1 + ytemp0)
			{
				//xtemp1 = xtemp0 + Math.Abs(ytemp1 - ytemp0);
			}
			else
			if (xtemp1 + xtemp0 < ytemp1 + ytemp0)
			{
				// ytemp1 = ytemp0 + Math.Abs(xtemp1 - xtemp0);
			}

			//if(xtemp1>xtemp0 && ytemp1>xtemp0)

			Line_Bres(xtemp0, ytemp0, xtemp0, ytemp1);
			Line_Bres(xtemp0, ytemp1, xtemp1, ytemp1);
			Line_Bres(xtemp0, ytemp0, xtemp1, ytemp0);
			Line_Bres(xtemp1, ytemp0, xtemp1, ytemp1);
			X2 = xtemp0;
			Y2 = ytemp1;
			X3 = xtemp1;
			Y3 = ytemp0;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			g.Clear(Color.White);
			pictureBox1.Image = bmp;

		}

		private void button2_Click(object sender, EventArgs e)
		{
			g.Clear(Color.White);
			int dx = int.Parse(textBox1.Text);
			int dy = int.Parse(textBox2.Text);

			Matrix matr = new Matrix(1, 3, X0, Y0, 1);
			Matrix matr2 = new Matrix(1, 3, X1, Y1, 1);
			Matrix matr3 = new Matrix(1, 3, X2, Y2, 1);
			Matrix matr4 = new Matrix(1, 3, X3, Y3, 1);

			Matrix offset = new Matrix(3, 3);
			offset[0, 0] = 1; offset[0, 1] = 0; offset[0, 2] = 0; offset[1, 0] = 0; offset[1, 1] = 1; offset[1, 2] = 0; offset[2, 0] = dx; offset[2, 1] = dy; offset[2, 2] = 1;

			var res1 = matr * offset;
			var res2 = matr2 * offset;
			var res3 = matr3 * offset;
			var res4 = matr4 * offset;

			if (!(res1[0, 0] > pictureBox1.Width - 1 || res1[0, 0] < 1 || res2[0, 0] > pictureBox1.Width - 1 || res2[0, 0] < 1 || res1[0, 1] > pictureBox1.Height - 1 || res1[0, 1] < 1 || res2[0, 1] > pictureBox1.Height - 1 || res2[0, 1] < 1))
			{
				X0 = (int)res1[0, 0];
				X1 = (int)res2[0, 0];
				Y0 = (int)res1[0, 1];
				Y1 = (int)res2[0, 1];
				X2 = (int)res3[0, 0];
				Y2 = (int)res3[0, 1];
				X3 = (int)res4[0, 0];
				Y3 = (int)res4[0, 1];

				X1 = Math.Min(pictureBox1.Width - 1, Math.Max(X1, 1));
				Y1 = Math.Min(pictureBox1.Height - 1, Math.Max(Y1, 1));
				X0 = Math.Min(pictureBox1.Width - 1, Math.Max(X0, 1));
				Y0 = Math.Min(pictureBox1.Height - 1, Math.Max(Y0, 1));
				X2 = Math.Min(pictureBox1.Width - 1, Math.Max(X2, 1));
				Y2 = Math.Min(pictureBox1.Height - 1, Math.Max(Y2, 1));
				X3 = Math.Min(pictureBox1.Width - 1, Math.Max(X3, 1));
				Y3 = Math.Min(pictureBox1.Height - 1, Math.Max(Y3, 1));
			}

			if (radioButton1.Checked)
				Line_Bres(X0, Y0, X1, Y1);
			else
			if (radioButton2.Checked)
			{
				Line_Bres(X0, Y0, X2, Y2);
				Line_Bres(X2, Y2, X1, Y1);
				Line_Bres(X1, Y1, X3, Y3);
				Line_Bres(X3, Y3, X0, Y0);
			}
		}

		private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
			char number = e.KeyChar;
			if (!(Char.IsDigit(number) || number == '-' || char.IsControl(number)))
				e.Handled = true;
		}
		private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
		{
			char number = e.KeyChar;
			if (!(Char.IsDigit(number) || number == '-' || char.IsControl(number)))
				e.Handled = true;
		}

		private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
		{
			char number = e.KeyChar;
			if (!(Char.IsDigit(number) || number == '-' || char.IsControl(number)))
				e.Handled = true;
		}

		private void button3_Click(object sender, EventArgs e)
		{
			g.Clear(Color.White);
			int grad = int.Parse(textBox3.Text);
			double rad = grad * Math.PI / 180.0;

			Matrix matr1 = new Matrix(1, 3, X0, Y0, 1);
			Matrix matr2 = new Matrix(1, 3, X1, Y1, 1);
			Matrix matr3 = new Matrix(1, 3, X2, Y2, 1);
			Matrix matr4 = new Matrix(1, 3, X3, Y3, 1);

			int x_centr = 0;
			int y_centr = 0;

			if (!checkBox1.Checked)
			{
				x_centr = X1 - (X1 - X0) / 2;
				y_centr = Y1 - (Y1 - Y0) / 2;
			}
			else
			{
				x_centr = XPOINT;
				y_centr = YPOINT;
			}
			//MessageBox.Show(x_centr.ToString());
			//MessageBox.Show(y_centr.ToString());
			Matrix offset = new Matrix(3, 3);
			offset[0, 0] = Math.Cos(rad);
			offset[0, 1] = Math.Sin(rad);
			offset[0, 2] = 0;
			offset[1, 0] = -Math.Sin(rad);
			offset[1, 1] = Math.Cos(rad);
			offset[1, 2] = 0;
			offset[2, 0] = -x_centr * Math.Cos(rad) + y_centr * Math.Sin(rad) + x_centr;
			offset[2, 1] = -x_centr * Math.Sin(rad) - y_centr * Math.Cos(rad) + y_centr;
			offset[2, 2] = 1;

			var res1 = matr1 * offset;
			var res2 = matr2 * offset;
			var res3 = matr3 * offset;
			var res4 = matr4 * offset;
			if (!(res1[0, 0] > pictureBox1.Width - 1 || res1[0, 0] < 1 || res2[0, 0] > pictureBox1.Width - 1 || res2[0, 0] < 1 || res1[0, 1] > pictureBox1.Height - 1 || res1[0, 1] < 1 || res2[0, 1] > pictureBox1.Height - 1 || res2[0, 1] < 1))
			{
				//X0 = res1[0, 0] < (int)res1[0, 0] ?

				X0 = (int)Math.Floor(res1[0, 0]);
				X1 = (int)Math.Floor(res2[0, 0]);
				Y0 = (int)Math.Floor(res1[0, 1]);
				Y1 = (int)Math.Floor(res2[0, 1]);
				X2 = (int)Math.Floor(res3[0, 0]);
				Y2 = (int)Math.Floor(res3[0, 1]);
				X3 = (int)Math.Floor(res4[0, 0]);
				Y3 = (int)Math.Floor(res4[0, 1]);

				X1 = Math.Min(pictureBox1.Width - 1, Math.Max(X1, 1));
				Y1 = Math.Min(pictureBox1.Height - 1, Math.Max(Y1, 1));
				X0 = Math.Min(pictureBox1.Width - 1, Math.Max(X0, 1));
				Y0 = Math.Min(pictureBox1.Height - 1, Math.Max(Y0, 1));
				X2 = Math.Min(pictureBox1.Width - 1, Math.Max(X2, 1));
				Y2 = Math.Min(pictureBox1.Height - 1, Math.Max(Y2, 1));
				X3 = Math.Min(pictureBox1.Width - 1, Math.Max(X3, 1));
				Y3 = Math.Min(pictureBox1.Height - 1, Math.Max(Y3, 1));
			}

			if (radioButton1.Checked)
				Line_Bres(X0, Y0, X1, Y1);
			else
			if (radioButton2.Checked)
			{
				Line_Bres(X0, Y0, X2, Y2);
				Line_Bres(X2, Y2, X1, Y1);
				Line_Bres(X1, Y1, X3, Y3);
				Line_Bres(X3, Y3, X0, Y0);
			}
		}

		private void button6_Click(object sender, EventArgs e)
		{

		}

		private void label1_Click(object sender, EventArgs e)
		{
		}

		private void Button7_Click(object sender, EventArgs e)
		{
			if (Is_Cross2(X0, Y0, X1, Y1, X0_SECOND, Y0_SECOND, X1_SECOND, Y1_SECOND))
				label2.Text = "Cross";
			else
				label2.Text = "Not cross";
		}

		private void button5_Click(object sender, EventArgs e)
		{
            //if ((yb * xa - xb * ya) > 0)
            //{
            //	label1.Text = "Справа";
            //}
            //if ((yb * xa - xb * ya) < 0)
            //{
            //	label1.Text = "Слева";
            //}
            //if((XPOINT - X1) * (Y2-Y1)-(YPOINT-Y1)*(X2 - X1) >0)
            if ((X1 - X0) * (YPOINT - Y0) - (Y1 - Y0) * (XPOINT - X0) > 0)
                label1.Text = "Справа";
            else
                label1.Text = "Слева";
        }

		private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
		{
			char number = e.KeyChar;
			if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && (e.KeyChar <= 39 || e.KeyChar >= 46) && number != 47 && number != 61) //калькулятор
				e.Handled = true;
		}

		private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
		{
			char number = e.KeyChar;
			if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && (e.KeyChar <= 39 || e.KeyChar >= 46) && number != 47 && number != 61) //калькулятор
				e.Handled = true;
		}

		private void button4_Click(object sender, EventArgs e)
		{
			g.Clear(Color.White);
			double alpha = double.Parse(textBox4.Text);
			double beta = double.Parse(textBox5.Text);

			int x_centr = 0;
			int y_centr = 0;

			if (!checkBox1.Checked)
			{
				x_centr = X1 - (X1 - X0) / 2;
				y_centr = Y1 - (Y1 - Y0) / 2;
			}
			else
			{
				x_centr = XPOINT;
				y_centr = YPOINT;
			}

			Matrix matr = new Matrix(1, 3, X0, Y0, 1);
			Matrix matr2 = new Matrix(1, 3, X1, Y1, 1);
			Matrix matr3 = new Matrix(1, 3, X2, Y2, 1);
			Matrix matr4 = new Matrix(1, 3, X3, Y3, 1);

			Matrix offset = new Matrix(3, 3);
			offset[0, 0] = alpha; offset[0, 1] = 0; offset[0, 2] = 0; offset[1, 0] = 0; offset[1, 1] = beta; offset[1, 2] = 0; offset[2, 0] = (1 - alpha) * x_centr; offset[2, 1] = (1 - beta) * y_centr; offset[2, 2] = 1;

			var res1 = matr * offset;
			var res2 = matr2 * offset;
			var res3 = matr3 * offset;
			var res4 = matr4 * offset;
			if (!(res1[0, 0] > pictureBox1.Width - 1 || res1[0, 0] < 1 || res2[0, 0] > pictureBox1.Width - 1 || res2[0, 0] < 1 || res1[0, 1] > pictureBox1.Height - 1 || res1[0, 1] < 1 || res2[0, 1] > pictureBox1.Height - 1 || res2[0, 1] < 1))
			{
				X0 = (int)Math.Floor(res1[0, 0]);
				X1 = (int)Math.Floor(res2[0, 0]);
				Y0 = (int)Math.Floor(res1[0, 1]);
				Y1 = (int)Math.Floor(res2[0, 1]);
				X2 = (int)Math.Floor(res3[0, 0]);
				Y2 = (int)Math.Floor(res3[0, 1]);
				X3 = (int)Math.Floor(res4[0, 0]);
				Y3 = (int)Math.Floor(res4[0, 1]);

				X1 = Math.Min(pictureBox1.Width - 1, Math.Max(X1, 1));
				Y1 = Math.Min(pictureBox1.Height - 1, Math.Max(Y1, 1));
				X0 = Math.Min(pictureBox1.Width - 1, Math.Max(X0, 1));
				Y0 = Math.Min(pictureBox1.Height - 1, Math.Max(Y0, 1));
				X2 = Math.Min(pictureBox1.Width - 1, Math.Max(X2, 1));
				Y2 = Math.Min(pictureBox1.Height - 1, Math.Max(Y2, 1));
				X3 = Math.Min(pictureBox1.Width - 1, Math.Max(X3, 1));
				Y3 = Math.Min(pictureBox1.Height - 1, Math.Max(Y3, 1));
			}

			if (radioButton1.Checked)
				Line_Bres(X0, Y0, X1, Y1);
			else
			if (radioButton2.Checked)
			{
				Line_Bres(X0, Y0, X2, Y2);
				Line_Bres(X2, Y2, X1, Y1);
				Line_Bres(X1, Y1, X3, Y3);
				Line_Bres(X3, Y3, X0, Y0);
			}
		}

		bool Is_Cross2(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
		{
			int det = (y4 - y3) * (x1 - x2) - (x4 - x3) * (y1 - y2);
			if (det != 0)
			{
				double T1 = ((x4 - x2) * (y4 - y3) - (x4 - x3) * (y4 - y2)) / det;
				double T2 = ((x1 - x2) * (y4 - y2) - (x4 - x2) * (y1 - y2)) / det;
				if (T1 >= 0 && T1 <= 1 && T2 >= 0 && T2 <= 1)
					return true;
			}
			return false;
		}

		bool Is_Cross(int x0, int y0, int x1, int y1, int x0_second, int y0_second, int x1_second, int y1_second)
		{
			var dr1x = x1 - x0;
			var dr1y = y1 - y0;
			var dr2x = x1_second - x0_second;
			var dr2y = y1_second - y0_second;

			double a1 = -dr1y;
			double b1 = +dr1x;
			double d1 = -(a1 * x0 + b1 * y0);

			double a2 = -dr2y;
			double b2 = +dr2x;
			double d2 = -(a2 * x0_second + b2 * y0_second);

			double seg1_line2_start = a2 * x0 + b2 * y0 + d2;
			double seg1_line2_end = a2 * x1 + b2 * y1 + d2;
			double seg2_line1_start = a1 * x0_second + b1 * y0_second + d1;
			double seg2_line1_end = a1 * x1_second + b1 * y1_second + d1;

			if (seg1_line2_end * seg1_line2_start >= 0 || seg2_line1_start * seg2_line1_end >= 0)
				if (x1_second < x0_second || y1_second < x0_second) return false;

			//double u = seg1_line2_start / (seg1_line2_start - seg1_line2_end);
			return true;
		}
	}
}
