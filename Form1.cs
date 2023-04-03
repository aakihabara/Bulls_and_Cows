using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BullsAndCows
{
    public partial class Form1 : Form
    {
        int currBulls = 0, currCows = 0, answer, currentIndex = 0;

        Algorithm computer = new Algorithm();

        bool startPl = true, pvpMode = false;
        int whosTurn = 0;
        int n = 0;

        public Form1()
        {
            InitializeComponent();
            n = 2;
            dataGridView1.RowCount = n;
            dataGridView2.RowCount = n;
            dataGridView1.ColumnCount = 3;
            dataGridView2.ColumnCount = 3;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Height = 498 / dataGridView1.RowCount;
                dataGridView2.Rows[i].Height = 498 / dataGridView1.RowCount;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    dataGridView1.Columns[j].Width = 300 / dataGridView1.ColumnCount;
                    dataGridView2.Columns[j].Width = 300 / dataGridView1.ColumnCount;
                }
            }
            label1.Hide();
            label2.Hide();
            label3.Hide();
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            button2.Hide();
            button3.Hide();
            button4.Hide();
            radioButton1.Select();
            radioButton3.Select();
        }

        public void checkBorder()
        {
            if (currentIndex + 1 > n)
            {
                n++;
                //dataGridView1.RowCount++;
                //dataGridView2.RowCount++;
                dataGridView1.Rows.Add();
                dataGridView2.Rows.Add();
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Height = 498 / n;
                    dataGridView2.Rows[i].Height = 498 / n;
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        dataGridView1.Columns[j].Width = 300 / dataGridView1.ColumnCount;
                        dataGridView2.Columns[j].Width = 300 / dataGridView1.ColumnCount;
                    }
                }
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    dataGridView1.Rows[dataGridView1.RowCount - 2].Cells[j].Value = dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[j].Value;
                    dataGridView2.Rows[dataGridView1.RowCount - 2].Cells[j].Value = dataGridView2.Rows[dataGridView1.RowCount - 1].Cells[j].Value;
                    dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[j].Value = "";
                    dataGridView2.Rows[dataGridView1.RowCount - 1].Cells[j].Value = "";
                }
            }
        }

        #region Key Pressin

        public void CheckKeyPress(KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) || e.KeyChar == '\b')
            {
                return;
            }
            e.Handled = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckKeyPress(e);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckKeyPress(e);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckKeyPress(e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button1.Show();
            computer = new Algorithm();
            groupBox1.Show();
            radioButton1.Show();
            radioButton2.Show();
            radioButton3.Show();
            radioButton4.Show();
            radioButton1.Select();
            radioButton3.Select();
            button4.Hide();
            currBulls = 0;
            currCows = 0;
            currentIndex = 0;
            pvpMode = false;
            startPl = true;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    dataGridView1[j, i].Value = "";
                    dataGridView2[j, i].Value = "";
                }
            }
        }

        #endregion

        #region buttons

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                pvpMode = true;
            }
            else if(radioButton2.Checked)
            {
                pvpMode = false;
            }
            else
            {
                MessageBox.Show("Хоть что-то, да выбрать нужно");
                return;
            }

            if (radioButton3.Checked)
            {
                startPl = true;
                whosTurn = 1;
                label3.Show();
                textBox3.Show();
                button2.Show();
            }
            else if (radioButton4.Checked)
            {
                startPl= false;
                whosTurn = 2;
                if (pvpMode)
                {
                    label3.Show();
                    textBox3.Show();
                    button2.Show();
                }
                else
                {
                    int botGuess = computer.Guessing(currBulls, currCows);
                    dataGridView1[0, currentIndex].Value = botGuess.ToString();
                    button3.Show();
                    label1.Show();
                    label2.Show();
                    textBox1.Show();
                    textBox2.Show();
                }
            }
            else
            {
                MessageBox.Show("Хоть что-то, да выбрать нужно");
                return;
            }

            radioButton1.Hide();
            radioButton2.Hide();
            radioButton3.Hide();
            radioButton4.Hide();
            button1.Hide();
            groupBox1.Hide();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int.TryParse(textBox1.Text, out currBulls);
            int.TryParse(textBox2.Text, out currCows);

            if(((currBulls < 0) && (currBulls > 4)) || ((currCows < 0) && (currBulls > 4)) || (currBulls + currCows > 4))
            {
                MessageBox.Show("Необходимо ввести корректное число быков и коров");
                return;
            }

            textBox1.Text = "";
            textBox2.Text = "";
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Show();
            label1.Hide();
            label2.Hide();
            label3.Show();
            button2.Show();
            button3.Hide();

            if (!pvpMode)
            {
                dataGridView1[1, currentIndex].Value = currBulls.ToString();
                dataGridView1[2, currentIndex].Value = currCows.ToString();

                if (currBulls == 4)
                {
                    MessageBox.Show("Победил компьютер, число = " + dataGridView1[0, currentIndex].Value.ToString() + ", а загаданное число = " + computer.myNum.ToString());
                    HideAll();
                    button4.Show();
                }

                if (startPl)
                {
                    currentIndex++;
                }
            }
            else
            {
                switch (whosTurn)
                {
                    case 1:
                        dataGridView1[1, currentIndex].Value = currBulls.ToString();
                        dataGridView1[2, currentIndex].Value = currCows.ToString();
                        if (startPl)
                        {
                            currentIndex++;
                        }
                        break;
                    case 2:
                        dataGridView2[1, currentIndex].Value = currBulls.ToString();
                        dataGridView2[2, currentIndex].Value = currCows.ToString();
                        if (!startPl)
                        {
                            currentIndex++;
                        }
                        break;
                }

                checkBorder();

                if (currBulls == 4)
                {
                    switch (whosTurn)
                    {
                        case 1:
                            MessageBox.Show("Победил игрок 2, число = " + dataGridView1[0, currentIndex].Value.ToString()); //Пофиксить вывод числа
                            HideAll();
                            button4.Show();
                            break;
                        case 2:
                            MessageBox.Show("Победил игрок 1, число = " + dataGridView1[0, currentIndex].Value.ToString()); //Пофиксить вывод числа
                            HideAll();
                            button4.Show();
                            break;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length != 4)
            {
                MessageBox.Show("Нужно ввести ответ заново, т.к. в синтаксисе обнаружена ошибка");
                return;
            }
            string temp = textBox3.Text;
            bool good = true;

            for (int i = 0; i < temp.Length; i++)
            {
                for (int j = i; j < temp.Length; j++)
                {
                    if(i != j && temp[i] == temp[j])
                    {
                        good = false;
                    }
                }
            }

            if (!good)
            {
                MessageBox.Show("Нужно ввести ответ заново, т.к. в синтаксисе обнаружена ошибка");
                return;
            }
            
            int.TryParse(textBox3.Text, out answer);
            string textAnsw = textBox3.Text;


            if (!pvpMode)
            {
                checkBorder();
                var tempToWrite = computer.GuessIt(textAnsw);
                dataGridView2[0, currentIndex].Value = textAnsw;
                dataGridView2[1, currentIndex].Value = tempToWrite.Item1.ToString();
                dataGridView2[2, currentIndex].Value = tempToWrite.Item2.ToString();

                if (tempToWrite.Item1 == 4)
                {
                    MessageBox.Show("Победил игрок, заданное число = " + textAnsw);
                    HideAll();
                    button4.Show();
                    return;
                }

                if (!startPl)
                {
                    currentIndex++;
                }

                int botGuess = computer.Guessing(currBulls, currCows);
                if (botGuess == -1)
                {
                    MessageBox.Show("Обманывать бота была плохой идеей");
                    HideAll();
                    button4.Show();
                    return;
                }
                dataGridView1[0, currentIndex].Value = botGuess.ToString();
                
            }
            else
            {
                checkBorder();
                switch (whosTurn)
                {
                    case 1:
                        dataGridView2[0, currentIndex].Value = textAnsw;
                        whosTurn = 2;
                        break;
                    case 2:
                        dataGridView1[0, currentIndex].Value = textAnsw;
                        whosTurn = 1;
                        break;
                }
            }
            button2.Hide();
            button3.Show();
            textBox1.Show();
            textBox2.Show();
            textBox3.Hide();
            label1.Show();
            label2.Show();
            label3.Hide();
            textBox3.Text = "";
        }

        public void HideAll()
        {
            groupBox1.Hide();
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            label1.Hide();
            label2.Hide();
            label3.Hide();
            button1.Hide();
            button2.Hide();
            button3.Hide();
        }

        #endregion


    }
}
