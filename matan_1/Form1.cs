using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace matan_1
{
    public partial class Form1 : Form
    {
        double[,] editTable = new double[4, 5];
        double[,] startTable = new double[4, 5];

        public Form1()
        {
            InitializeComponent();
        }

        private void writeValuesButton_Click(object sender, EventArgs e)
        {
            editTable[0, 0] = double.Parse(x1Box.Text);
            editTable[0, 1] = double.Parse(y1Box.Text);
            editTable[0, 2] = double.Parse(z1Box.Text);
            editTable[0, 3] = double.Parse(p1Box.Text);
            editTable[0, 4] = double.Parse(answ1Box.Text);

            editTable[1, 0] = double.Parse(x2Box.Text);
            editTable[1, 1] = double.Parse(y2Box.Text);
            editTable[1, 2] = double.Parse(z2Box.Text);
            editTable[1, 3] = double.Parse(p2Box.Text);
            editTable[1, 4] = double.Parse(answ2Box.Text);

            editTable[2, 0] = double.Parse(x3Box.Text);
            editTable[2, 1] = double.Parse(y3Box.Text);
            editTable[2, 2] = double.Parse(z3Box.Text);
            editTable[2, 3] = double.Parse(p3Box.Text);
            editTable[2, 4] = double.Parse(answ3Box.Text);

            editTable[3, 0] = double.Parse(x4Box.Text);
            editTable[3, 1] = double.Parse(y4Box.Text);
            editTable[3, 2] = double.Parse(z4Box.Text);
            editTable[3, 3] = double.Parse(p4Box.Text);
            editTable[3, 4] = double.Parse(answ4Box.Text);

            for (int i = 0; i < editTable.GetLength(0); i++)
            {
                for(int j = 0; j < editTable.GetLength(1); j++)
                {
                    startTable[i, j] = editTable[i, j];
                }
            }

            successLabel.ForeColor = Color.Green;
            successLabel.Text = "Success!";
            finalTableBox.Text = "";
        }

        private void calcButton_Click(object sender, EventArgs e)
        {
            successLabel.ForeColor = Color.Black;
            successLabel.Text = "...";

            int coeffAmount = 3;
            int strNum = 0;
            int colNum = 0;

            for (int k = 0; k < editTable.GetLength(0); k++) {
                int colNumInStr = 0;
                int coeffNum = 0;
                double[] coeffTable = new double[coeffAmount];
                int length = 0;

                if (editTable[strNum, colNum] == 0)
                {
                    finalTableBox.Text += $"editTable[{strNum}, {colNum}] == {editTable[strNum, colNum]}";
                    for (int i = strNum; i < editTable.GetLength(0); i++)
                    {
                        if(i == editTable.GetLength(0) - 1)
                        {
                            break;
                        }
                        for (int j = colNum; j < editTable.GetLength(1); j++)
                        {
                                editTable[strNum, j] = editTable[i + 1, j];
                                editTable[i + 1, j] = startTable[strNum, j];
                        }
                        for (int c = 0; c < editTable.GetLength(0); c++)
                        {
                            for (int j = 0; j < editTable.GetLength(1); j++)
                            {
                                startTable[c, j] = editTable[c, j];
                            }
                        }
                        if (editTable[strNum, colNum] != 0)
                        {
                            break;
                        }
                    }
                }
                for (int j = k + 1; j < editTable.GetLength(0); j++)
                {
                    for (int c = length; c < coeffTable.Length;)
                    {
                        coeffTable[c] = editTable[j, k] / editTable[strNum, colNum];
                        length++;
                        break;
                    }
                }
                for (int i = 0; i < editTable.GetLength(1); i++)
                {
                    for (int j = k + 1; j < editTable.GetLength(0); j++)
                    {
                        editTable[j, i] = editTable[j, i] - coeffTable[coeffNum] * editTable[strNum, colNumInStr];
                        coeffNum++;
                    }
                    coeffNum = 0;
                    colNumInStr++;
                }
                colNum++;
                strNum++;
                coeffAmount--;
            }

            for (int i = 0; i < editTable.GetLength(0); i++)
            {
                for(int j = 0; j < editTable.GetLength(1); j++)
                {
                    finalTableBox.Text += $"{editTable[i,j]}         ";
                }
                finalTableBox.Text += "\n";
            }

            double P = editTable[3, 4] / editTable[3, 3];
            pAnswLabel.Text = (P).ToString();
            double Z = (editTable[2, 4] - editTable[2, 3] * P) / editTable[2, 2];
            zAnswLabel.Text = Z.ToString();
            double Y = (editTable[1, 4] - editTable[1, 3] * P - editTable[1, 2] * Z) / editTable[1, 1];
            yAnswLabel.Text = Y.ToString();
            double X = (editTable[0, 4] - editTable[0, 3] * P - editTable[0, 2] * Z - editTable[0, 1] * Y) / editTable[0, 0];
            xAnswLabel.Text = X.ToString();
        }
    }
}
