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
        public class Matrix
        {
            private int[,] matr;

            public int m;
            public int n;

            public Matrix(int m, int n)
            {
                this.m = m;
                this.n = n;
                this.matr = new int[m, n];
            }

            public Matrix(int m, int n, int x1, int y1, int h)
            {
                this.m = m;
                this.n = n;
                this.matr = new int[m, n];
                matr[0, 0] = x1;
                matr[0, 1] = y1;
                matr[0, 2] = h;
            }

            public int this[int x, int y]
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
            this.X0 = e.X;
            this.Y0 = e.Y;
            pictureBox1.Image = bmp;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (mouse_Down)
            {
                if(!radioButton3.Checked)
                g.Clear(Color.White);
                this.X1 = Math.Min(pictureBox1.Width - 1, Math.Max(e.X, 1));
                this.Y1 = Math.Min(pictureBox1.Height - 1, Math.Max(e.Y, 1));

                if (radioButton1.Checked)
                    Line_Bres(X0, Y0, X1, Y1);
                else
                    if (radioButton2.Checked)
                    Draw_square(X0, Y0, X1, Y1);
            }

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            mouse_Down = false;
            if (mouse_Down && radioButton1.Checked)
                Line_Bres(X0, Y0, X1, Y1);
            else
                if (mouse_Down && radioButton2.Checked)
                Draw_square(X0, Y0, X1, Y1);
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

        void Draw_square(int x0, int y0, int x1, int y1)
        {
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

            Matrix offset = new Matrix(3, 3);
            offset[0, 0] = 1; offset[0, 1] = 0; offset[0, 2] = 0; offset[1, 0] = 0; offset[1, 1] = 1; offset[1, 2] = 0; offset[2, 0] = dx; offset[2, 1] = dy; offset[2, 2] = 1;

            var res1 = matr * offset;
            var res2 = matr2 * offset;
            if (!(res1[0, 0] > pictureBox1.Width - 1 || res1[0, 0] < 1 || res2[0, 0] > pictureBox1.Width - 1 || res2[0, 0] < 1 || res1[0, 1] > pictureBox1.Height - 1 || res1[0, 1] < 1 || res2[0, 1] > pictureBox1.Height - 1 || res2[0, 1] < 1))
            {
                X0 = res1[0, 0];
                X1 = res2[0, 0];
                Y0 = res1[0, 1];
                Y1 = res2[0, 1];

                X1 = Math.Min(pictureBox1.Width - 1, Math.Max(X1, 1));
                Y1 = Math.Min(pictureBox1.Height - 1, Math.Max(Y1, 1));
                X0 = Math.Min(pictureBox1.Width - 1, Math.Max(X0, 1));
                Y0 = Math.Min(pictureBox1.Height - 1, Math.Max(Y0, 1));
            }

            if (radioButton1.Checked)
                Line_Bres(X0, Y0, X1, Y1);
            else
               if (radioButton2.Checked)
                Draw_square(X0, Y0, X1, Y1);
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

        }
    }
}