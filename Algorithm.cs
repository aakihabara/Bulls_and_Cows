using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BullsAndCows
{
    class Algorithm
    {

        Random rnd = new Random();
        public int myNum;
        int startGuess = 1234;
        int bulls = 0, cows = 0;
        List<int> allNums = new List<int>();
        bool firstStep = true;

        public int StartGuess { get => startGuess; }

        public Algorithm()
        {

            for (int i = 1; i <= 9; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    if (j != i)
                    {
                        for (int k = 0; k <= 9; k++)
                        {
                            if ((k != j) && (k != i))
                            {
                                for (int h = 0; h <= 9; h++)
                                {
                                    if ((h != k) && (h != i) && (h != j))
                                    {
                                        allNums.Add(i * 1000 + j * 100 + k * 10 + h);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            myNum = allNums[rnd.Next(0, allNums.Count)];
        }

        public void FindEquals(List<int> x, int bulls, int cows)
        {
            for(int i = 0; i < x.Count;)
            {
                string temp = startGuess.ToString();
                string temp1 = x[i].ToString();
                int countinB = 0;
                int countinC = 0;
                for (int j = 0; j < 4; j++)
                {
                    if (temp[j] == temp1[j])
                    {
                        countinB++;
                    }
                    else
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            if(j != k)
                            {
                                if (temp[j] == temp1[k])
                                {
                                    countinC++;
                                }
                            }
                        }
                    }
                }
                if(countinB != bulls || countinC != cows)
                {
                    x.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }

        public int Guessing(int b, int c)
        {
            if (firstStep)
            {
                firstStep = false;
                return startGuess;
            }

            bulls = b;
            cows = c;

            FindEquals(allNums, bulls, cows);

            if(allNums.Count == 0)
            {
                MessageBox.Show("Может объяснишь, зачем ты пытаешь меня обмануть?");
                return -1;
            }

            int res = allNums[rnd.Next(0, allNums.Count)];

            startGuess = res;
            return res;
        }

        public (int, int) GuessIt(string num)
        {
            int bulls = 0, cows = 0;
            string tempNumPlayer = num;
            string tempNum = myNum.ToString();

            int[] correctAnsw = new int[4] { 0, 0, 0, 0 };

            for (int i = 0; i < tempNum.Length; i++)
            {
                if (tempNum[i] == tempNumPlayer[i])
                {
                    bulls++;
                    correctAnsw[i]++;
                }
            }

            for (int i = 0; i < tempNum.Length; i++)
            {
                for (int j = 0; j < tempNum.Length; j++)
                {
                    if ((i != j) && (correctAnsw[j] != 1) && (tempNumPlayer[i] == tempNum[j]))
                    {
                        cows++;
                    }
                }
            }

            return (bulls, cows);
        }

    }
}
