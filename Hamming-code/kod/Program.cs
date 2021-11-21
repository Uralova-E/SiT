using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kod
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите код (из 0 и 1)");
            string s = "";
            string sk = "";
            string primer = "";

            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Backspace:
                        if (s.Length > 0)
                        {
                            s = s.Remove(s.Length - 1, 1);
                            Console.Write(key.KeyChar + " " + key.KeyChar);
                        }
                        break;
                    default:
                        if ((key.KeyChar <= 49) && (key.KeyChar >= 48))
                        {
                            Console.Write(key.KeyChar);
                            s += key.KeyChar;
                        }
                        break;
                }
            }
            while (key.KeyChar != 13);
            Console.WriteLine("\n---------------------------");

            primer = s;
            primer = primer.Insert(0, "X").Insert(1, "X");

            sk = s; // для вывода
            s = s.Insert(0, "0").Insert(1, "0"); // вставка нулей в 0, 2^0
            int numBit = 2; //отсчет начинается с двух, так как первый два бита мы уже добавили            

            for (int i = 4, j = 3; i < s.Length; i = (int)Math.Pow(2, j), j++)
            {
                numBit++;
                s = s.Insert(i - 1, "0");
                primer = primer.Insert(i - 1, "X");
            }
            Console.WriteLine("Кол-во контрольных бит: {0} ", numBit);
            //Console.WriteLine(s);
            Console.WriteLine(primer);

            Console.WriteLine();

            string s1 = null, s2 = null, s3 = null, s4 = null, s5 = null;
            int x = s.Length;

            for (int i = 0; i < s.Length / 2; i++)
                s1 += "0";
            for (int i = 0; i < s.Length; i = i + 2)
                s1 = s1.Insert(i, "1");
            Console.WriteLine(s1);

            if (numBit >= 1)
            {
                for (int i = 0; i < s.Length / 2; i++)
                    s2 += "0";
                for (int i = 1; i < s.Length; i = i + 4)
                    s2 = s2.Insert(i, "11");
                if (s2.Length > s1.Length)
                {
                    s2 = s2.Remove(s2.Length - 1);
                }
                else if (s2.Length < s1.Length)
                {
                    s2 += "0";
                }
                Console.WriteLine(s2);
            }
            if (numBit >= 3)
            {
                s3 = s;
                s3 = s3.Replace('1', '0');

                for (int i = 3; i < s.Length; i = i + 8)
                {
                    s3 = s3.Insert(i, "1111");
                }
                if (s3.Length > s2.Length)
                {
                    int raznica = s3.Length - s2.Length;
                    s3 = s3.Remove(s3.Length - raznica);
                }
                Console.WriteLine(s3);
            }

            if (numBit >= 4)
            {
                s4 = s;
                s4 = s4.Replace('1', '0');

                for (int i = 7; i < s.Length; i = i + 16)
                {
                    s4 = s4.Insert(i, "11111111");
                }
                if (s4.Length > s3.Length)
                {
                    int raznica = s4.Length - s3.Length;
                    s4 = s4.Remove(s4.Length - raznica);
                }
                Console.WriteLine(s4);
            }

            if (numBit >= 5)
            {
                s5 = s;
                s5 = s5.Replace('1', '0');

                for (int i = 15; i < s.Length; i = i + 32)
                {
                    s5 = s5.Insert(i, "1111111111111111");
                }
                if (s5.Length > s4.Length)
                {
                    int raznica = s5.Length - s4.Length;
                    s5 = s5.Remove(s5.Length - raznica);
                }
                Console.WriteLine(s5);
            }

            int[] sBoss = s.Select(ch => int.Parse(ch.ToString())).ToArray(); //массив из целых чисел
            int[] sBoss1 = new int[0];
            int[] sBoss2 = new int[0];
            int[] sBoss3 = new int[0];
            int[] sBoss4 = new int[0];
            int[] sBoss5 = new int[0];

            if (numBit > 0)
            {
                sBoss1 = s1.Select(ch => int.Parse(ch.ToString())).ToArray();
            }
            if (numBit >= 1)
            {
                sBoss2 = s2.Select(ch => int.Parse(ch.ToString())).ToArray();
            }
            if (numBit >= 3)
            {
                sBoss3 = s3.Select(ch => int.Parse(ch.ToString())).ToArray();
            }
            if (numBit >= 4)
            {
                sBoss4 = s4.Select(ch => int.Parse(ch.ToString())).ToArray();
            }
            if (numBit >= 5)
            {
                sBoss5 = s5.Select(ch => int.Parse(ch.ToString())).ToArray();
            }

            string[] tem = new string[numBit]; //для хранения r(x)

            int temp = 0;
            for (int i = 0; i < s.Length; i++)
                temp += sBoss[i] * sBoss1[i];
            if (temp > 1)
                tem[0] = Convert.ToString(temp % 2);
            else
                tem[0] = Convert.ToString(temp);
            Console.WriteLine("r1 = {0} ", tem[0]);

            int temp2 = 0;
            if (numBit >= 1)
            {
                for (int i = 0; i < s.Length; i++)
                    temp2 += sBoss[i] * sBoss2[i];
                if (temp2 > 1)
                    tem[1] = Convert.ToString(temp2 % 2);
                else
                    tem[1] = Convert.ToString(temp2);
                Console.WriteLine("r2 = {0} ", tem[1]);
            }

            int temp3 = 0;
            if (numBit >= 3)
            {
                for (int i = 0; i < s.Length; i++)
                    temp3 += sBoss[i] * sBoss3[i];
                if (temp3 > 1)
                    tem[2] = Convert.ToString(temp3 % 2);
                else
                    tem[2] = Convert.ToString(temp3);
                Console.WriteLine("r3 = {0} ", tem[2]);
            }

            int temp4 = 0;
            if (numBit >= 4)
            {
                for (int i = 0; i < s.Length; i++)
                    temp4 += sBoss[i] * sBoss4[i];
                if (temp4 > 1)
                    tem[3] = Convert.ToString(temp4 % 2);
                else
                    tem[3] = Convert.ToString(temp4);
                Console.WriteLine("r4 = {0} ", tem[3]);
            }

            int temp5 = 0;
            if (numBit >= 5)
            {
                for (int i = 0; i < s.Length; i++)
                    temp5 += sBoss[i] * sBoss5[i];
                if (temp5 > 1)
                    tem[4] = Convert.ToString(temp5 % 2);
                else
                    tem[4] = Convert.ToString(temp5);
                Console.WriteLine("r5 = {0} ", tem[4]);
            }

            for (int i = 1, j = 1, k = 0; i < sk.Length; i = (int)Math.Pow(2, j), j++, k++)
            {
                sk = sk.Insert(i - 1, tem[k]);
            }
            Console.Write("Закодированное слово:{0}", sk);



            //декодирование:
            int[][] error1 = new int[7][];
            int[][] error2 = new int[21][];
            int[][] error3 = new int[35][];
            int[][] error4 = new int[35][];
            int[][] error5 = new int[21][];
            int[][] error6 = new int[7][];
            int[][] error7 = new int[1][];
            string errors = "";
            int n = 0; //порядок вектора ошибки
            int e1 = 0, e2 = 0, e3 = 0, e4 = 0, e5 = 0, e6 = 0, e7 = 0;
            

            for (int i = 0; i < 128; i++)
            {
                n = 0;
                errors = Convert.ToString(i, 2);
                for (int j = 0; j < errors.Length; j++) if (errors[j] == '1') n++;
                if (n == 1)
                {
                    error1[e1] = new int[7];
                    for (int t = 0; t < errors.Length; t++) error1[e1][7-errors.Length+t] = (int)Char.GetNumericValue(errors[t]);
                    e1++;
                }
                if (n == 2)
                {
                    error2[e2] = new int[7];
                    for (int t = 0; t < errors.Length; t++) error2[e2][7 - errors.Length + t] = (int)Char.GetNumericValue(errors[t]);
                    e2++;
                }
                if (n == 3)
                {
                    error3[e3] = new int[7];
                    for (int t = 0; t < errors.Length; t++) error3[e3][7 - errors.Length + t] = (int)Char.GetNumericValue(errors[t]);
                    e3++;
                }
                if (n == 4)
                {
                    error4[e4] = new int[7];
                    for (int t = 0; t < errors.Length; t++) error4[e4][7 - errors.Length + t] = (int)Char.GetNumericValue(errors[t]);
                    e4++;
                }
                if (n == 5)
                {
                    error5[e5] = new int[7];
                    for (int t = 0; t < errors.Length; t++) error5[e5][7 - errors.Length + t] = (int)Char.GetNumericValue(errors[t]);
                    e5++;
                }
                if (n == 6)
                {
                    error6[e6] = new int[7];
                    for (int t = 0; t < errors.Length; t++) error6[e6][7 - errors.Length + t] = (int)Char.GetNumericValue(errors[t]);
                    e6++;
                }
                if (n == 7)
                {
                    error7[e7] = new int[7];
                    for (int t = 0; t < errors.Length; t++) error7[e7][7 - errors.Length + t] = (int)Char.GetNumericValue(errors[t]);
                    e7++;
                }
            }

            Console.WriteLine("\n\n");

            
            string was = "";
            int plus = 0, errors_num = 0, errors_cor = 0;
            Console.WriteLine("_________________________________________________________________________________________________");
            Console.WriteLine("|  Кратность  | Обнаруживающая | Корректирующая | Обнаруженные | Скоректированные | Общее число |");
            Console.WriteLine("|   ошибки    |   способность  |   способность  |    ошибки    |      ошибки      |    ошибок   |");
            Console.WriteLine("_________________________________________________________________________________________________");
            for (int j = 0; j < error1.Length; j++)
            {
                s = sk;
                int[] sBossDecode = s.Select(ch => int.Parse(ch.ToString())).ToArray();
                for (int u = 0; u < 7; u++)
                {
                    if (error1[j][u] == 1) sBossDecode[u] = (sBossDecode[u] + 1) % 2;
                }

                errors = String.Join("", error1[j]);
                was = String.Join("", sBossDecode);
                s = String.Join("", sBossDecode);

                temp = 0;
                for (int i = 0; i < s.Length; i++)
                    temp += sBossDecode[i] * sBoss1[i];
                if (temp > 1)
                    tem[0] = Convert.ToString(temp % 2);
                else
                    tem[0] = Convert.ToString(temp);
                //Console.WriteLine("r1 = {0} ", tem[0]);

                if (numBit >= 1)
                {
                    temp2 = 0;
                    for (int i = 0; i < s.Length; i++)
                        temp2 += sBossDecode[i] * sBoss2[i];
                    if (temp2 > 1)
                        tem[1] = Convert.ToString(temp2 % 2);
                    else
                        tem[1] = Convert.ToString(temp2);
                    //Console.WriteLine("r2 = {0} ", tem[1]);
                }

                if (numBit >= 3)
                {
                    temp3 = 0;
                    for (int i = 0; i < s.Length; i++)
                        temp3 += sBossDecode[i] * sBoss3[i];
                    if (temp3 > 1)
                        tem[2] = Convert.ToString(temp3 % 2);
                    else
                        tem[2] = Convert.ToString(temp3);
                    //Console.WriteLine("r3 = {0} ", tem[2]);
                }

                if (numBit >= 4)
                {
                    temp4 = 0;
                    for (int i = 0; i < s.Length; i++)
                        temp4 += sBossDecode[i] * sBoss4[i];
                    if (temp4 > 1)
                        tem[3] = Convert.ToString(temp4 % 2);
                    else
                        tem[3] = Convert.ToString(temp4);
                    //Console.WriteLine("r4 = {0} ", tem[3]);
                }

                if (numBit >= 5)
                {
                    temp5 = 0;
                    for (int i = 0; i < s.Length; i++)
                        temp5 += sBossDecode[i] * sBoss5[i];
                    if (temp5 > 1)
                        tem[4] = Convert.ToString(temp5 % 2);
                    else
                        tem[4] = Convert.ToString(temp5);
                    //Console.WriteLine("r5 = {0} ", tem[4]);
                }

                plus = 0;

                if (tem[0] != "0")
                    plus += 1;
                if (numBit >= 1)
                {
                    if (tem[1] != "0")
                        plus += 2;
                }
                if (numBit >= 3)
                {
                    if (tem[2] != "0")
                        plus += 4;
                }
                if (numBit >= 4)
                {
                    if (tem[3] != "0")
                        plus += 8;
                }
                if (numBit >= 5)
                {
                    if (tem[4] != "0")
                        plus += 16;
                }

                if (s[plus - 1] == '1')
                {
                    s = s.Remove(plus - 1, 1).Insert(plus - 1, "0");
                }
                else
                {
                    s = s.Remove(plus - 1, 1).Insert(plus - 1, "1");
                }
                
            
                if (sk == s) errors_cor++;
                if (plus > 0) errors_num++;
            }
           
            Console.WriteLine("|     1       |       {0}%     |       {1}%     |      {2}       |        {3}         |      {4}      |", System.Math.Round((double)(errors_num * 100 / error1.Length)), System.Math.Round((double)(errors_cor * 100 / error1.Length)), errors_num, errors_cor, error1.Length);
            Console.WriteLine("_________________________________________________________________________________________________");

            errors_num = 0; errors_cor = 0;

            for (int j = 0; j < error2.Length; j++)
            {
                s = sk;
                int[] sBossDecode = s.Select(ch => int.Parse(ch.ToString())).ToArray();
                for (int u = 0; u < 7; u++)
                {
                    if (error2[j][u] == 1) sBossDecode[u] = (sBossDecode[u] + 1) % 2;
                }

                errors = String.Join("", error2[j]);
                was = String.Join("", sBossDecode);
                s = String.Join("", sBossDecode);

                temp = 0;
                for (int i = 0; i < s.Length; i++)
                    temp += sBossDecode[i] * sBoss1[i];
                if (temp > 1)
                    tem[0] = Convert.ToString(temp % 2);
                else
                    tem[0] = Convert.ToString(temp);
                //Console.WriteLine("r1 = {0} ", tem[0]);

                if (numBit >= 1)
                {
                    temp2 = 0;
                    for (int i = 0; i < s.Length; i++)
                        temp2 += sBossDecode[i] * sBoss2[i];
                    if (temp2 > 1)
                        tem[1] = Convert.ToString(temp2 % 2);
                    else
                        tem[1] = Convert.ToString(temp2);
                    //Console.WriteLine("r2 = {0} ", tem[1]);
                }

                if (numBit >= 3)
                {
                    temp3 = 0;
                    for (int i = 0; i < s.Length; i++)
                        temp3 += sBossDecode[i] * sBoss3[i];
                    if (temp3 > 1)
                        tem[2] = Convert.ToString(temp3 % 2);
                    else
                        tem[2] = Convert.ToString(temp3);
                    //Console.WriteLine("r3 = {0} ", tem[2]);
                }

                if (numBit >= 4)
                {
                    temp4 = 0;
                    for (int i = 0; i < s.Length; i++)
                        temp4 += sBossDecode[i] * sBoss4[i];
                    if (temp4 > 1)
                        tem[3] = Convert.ToString(temp4 % 2);
                    else
                        tem[3] = Convert.ToString(temp4);
                    //Console.WriteLine("r4 = {0} ", tem[3]);
                }

                if (numBit >= 5)
                {
                    temp5 = 0;
                    for (int i = 0; i < s.Length; i++)
                        temp5 += sBossDecode[i] * sBoss5[i];
                    if (temp5 > 1)
                        tem[4] = Convert.ToString(temp5 % 2);
                    else
                        tem[4] = Convert.ToString(temp5);
                    //Console.WriteLine("r5 = {0} ", tem[4]);
                }

                plus = 0;

                if (tem[0] != "0")
                    plus += 1;
                if (numBit >= 1)
                {
                    if (tem[1] != "0")
                        plus += 2;
                }
                if (numBit >= 3)
                {
                    if (tem[2] != "0")
                        plus += 4;
                }
                if (numBit >= 4)
                {
                    if (tem[3] != "0")
                        plus += 8;
                }
                if (numBit >= 5)
                {
                    if (tem[4] != "0")
                        plus += 16;
                }

                if (s[plus - 1] == '1')
                {
                    s = s.Remove(plus - 1, 1).Insert(plus - 1, "0");
                }
                else
                {
                    s = s.Remove(plus - 1, 1).Insert(plus - 1, "1");
                }

                if (sk == s) errors_cor++;
                if (plus > 0) errors_num++;
            }
            Console.WriteLine("|     2       |       {0}%     |        {1}%      |      {2}      |        {3}         |      {4}     |", System.Math.Round((double)(errors_num * 100 / error2.Length)), System.Math.Round((double)(errors_cor * 100 / error2.Length)), errors_num, errors_cor, error2.Length);
            Console.WriteLine("_________________________________________________________________________________________________");


            errors_num = 0; errors_cor = 0;

            for (int j = 0; j < error3.Length; j++)
            {
                s = sk;
                int[] sBossDecode = s.Select(ch => int.Parse(ch.ToString())).ToArray();
                for (int u = 0; u < 7; u++)
                {
                    if (error3[j][u] == 1) sBossDecode[u] = (sBossDecode[u] + 1) % 2;
                }

                errors = String.Join("", error3[j]);
                was = String.Join("", sBossDecode);
                s = String.Join("", sBossDecode);

                temp = 0;
                for (int i = 0; i < s.Length; i++)
                    temp += sBossDecode[i] * sBoss1[i];
                if (temp > 1)
                    tem[0] = Convert.ToString(temp % 2);
                else
                    tem[0] = Convert.ToString(temp);
                //Console.WriteLine("r1 = {0} ", tem[0]);

                if (numBit >= 1)
                {
                    temp2 = 0;
                    for (int i = 0; i < s.Length; i++)
                        temp2 += sBossDecode[i] * sBoss2[i];
                    if (temp2 > 1)
                        tem[1] = Convert.ToString(temp2 % 2);
                    else
                        tem[1] = Convert.ToString(temp2);
                    //Console.WriteLine("r2 = {0} ", tem[1]);
                }

                if (numBit >= 3)
                {
                    temp3 = 0;
                    for (int i = 0; i < s.Length; i++)
                        temp3 += sBossDecode[i] * sBoss3[i];
                    if (temp3 > 1)
                        tem[2] = Convert.ToString(temp3 % 2);
                    else
                        tem[2] = Convert.ToString(temp3);
                    //Console.WriteLine("r3 = {0} ", tem[2]);
                }

                if (numBit >= 4)
                {
                    temp4 = 0;
                    for (int i = 0; i < s.Length; i++)
                        temp4 += sBossDecode[i] * sBoss4[i];
                    if (temp4 > 1)
                        tem[3] = Convert.ToString(temp4 % 2);
                    else
                        tem[3] = Convert.ToString(temp4);
                    //Console.WriteLine("r4 = {0} ", tem[3]);
                }

                if (numBit >= 5)
                {
                    temp5 = 0;
                    for (int i = 0; i < s.Length; i++)
                        temp5 += sBossDecode[i] * sBoss5[i];
                    if (temp5 > 1)
                        tem[4] = Convert.ToString(temp5 % 2);
                    else
                        tem[4] = Convert.ToString(temp5);
                    //Console.WriteLine("r5 = {0} ", tem[4]);
                }

                plus = 0;

                if (tem[0] != "0")
                    plus += 1;
                if (numBit >= 1)
                {
                    if (tem[1] != "0")
                        plus += 2;
                }
                if (numBit >= 3)
                {
                    if (tem[2] != "0")
                        plus += 4;
                }
                if (numBit >= 4)
                {
                    if (tem[3] != "0")
                        plus += 8;
                }
                if (numBit >= 5)
                {
                    if (tem[4] != "0")
                        plus += 16;
                }

                if (plus > 0)
                {
                    if (s[plus - 1] == '1')
                    {
                        s = s.Remove(plus - 1, 1).Insert(plus - 1, "0");
                    }
                    else
                    {
                        s = s.Remove(plus - 1, 1).Insert(plus - 1, "1");
                    }
                }

                if (sk == s) errors_cor++;
                if (plus > 0) errors_num++;
            }
            Console.WriteLine("|     3       |       {0}%      |       {1}%       |      {2}      |        {3}         |      {4}     |", System.Math.Round((double)(errors_num * 100 / error3.Length)), System.Math.Round((double)(errors_cor * 100 / error3.Length)), errors_num, errors_cor, error3.Length);
            Console.WriteLine("_________________________________________________________________________________________________");


            errors_num = 0; errors_cor = 0;
            for (int j = 0; j < error4.Length; j++)
            {
                s = sk;
                int[] sBossDecode = s.Select(ch => int.Parse(ch.ToString())).ToArray();
                for (int u = 0; u < 7; u++)
                {
                    if (error4[j][u] == 1) sBossDecode[u] = (sBossDecode[u] + 1) % 2;
                }

                errors = String.Join("", error4[j]);
                was = String.Join("", sBossDecode);
                s = String.Join("", sBossDecode);

                temp = 0;
                for (int i = 0; i < s.Length; i++)
                    temp += sBossDecode[i] * sBoss1[i];
                if (temp > 1)
                    tem[0] = Convert.ToString(temp % 2);
                else
                    tem[0] = Convert.ToString(temp);
                //Console.WriteLine("r1 = {0} ", tem[0]);

                if (numBit >= 1)
                {
                    temp2 = 0;
                    for (int i = 0; i < s.Length; i++)
                        temp2 += sBossDecode[i] * sBoss2[i];
                    if (temp2 > 1)
                        tem[1] = Convert.ToString(temp2 % 2);
                    else
                        tem[1] = Convert.ToString(temp2);
                    //Console.WriteLine("r2 = {0} ", tem[1]);
                }

                if (numBit >= 3)
                {
                    temp3 = 0;
                    for (int i = 0; i < s.Length; i++)
                        temp3 += sBossDecode[i] * sBoss3[i];
                    if (temp3 > 1)
                        tem[2] = Convert.ToString(temp3 % 2);
                    else
                        tem[2] = Convert.ToString(temp3);
                    //Console.WriteLine("r3 = {0} ", tem[2]);
                }

                if (numBit >= 4)
                {
                    temp4 = 0;
                    for (int i = 0; i < s.Length; i++)
                        temp4 += sBossDecode[i] * sBoss4[i];
                    if (temp4 > 1)
                        tem[3] = Convert.ToString(temp4 % 2);
                    else
                        tem[3] = Convert.ToString(temp4);
                    //Console.WriteLine("r4 = {0} ", tem[3]);
                }

                if (numBit >= 5)
                {
                    temp5 = 0;
                    for (int i = 0; i < s.Length; i++)
                        temp5 += sBossDecode[i] * sBoss5[i];
                    if (temp5 > 1)
                        tem[4] = Convert.ToString(temp5 % 2);
                    else
                        tem[4] = Convert.ToString(temp5);
                    //Console.WriteLine("r5 = {0} ", tem[4]);
                }

                plus = 0;

                if (tem[0] != "0")
                    plus += 1;
                if (numBit >= 1)
                {
                    if (tem[1] != "0")
                        plus += 2;
                }
                if (numBit >= 3)
                {
                    if (tem[2] != "0")
                        plus += 4;
                }
                if (numBit >= 4)
                {
                    if (tem[3] != "0")
                        plus += 8;
                }
                if (numBit >= 5)
                {
                    if (tem[4] != "0")
                        plus += 16;
                }

                if (plus > 0)
                {
                    if (s[plus - 1] == '1')
                    {
                        s = s.Remove(plus - 1, 1).Insert(plus - 1, "0");
                    }
                    else
                    {
                        s = s.Remove(plus - 1, 1).Insert(plus - 1, "1");
                    }
                }

                if (sk == s) errors_cor++;
                if (plus > 0) errors_num++;
            }
            Console.WriteLine("|     4       |       {0}%      |       {1}%       |      {2}      |        {3}         |      {4}     |", System.Math.Round((double)(errors_num * 100 / error4.Length)), System.Math.Round((double)(errors_cor * 100 / error4.Length)), errors_num, errors_cor, error4.Length);
            Console.WriteLine("_________________________________________________________________________________________________");

            errors_num = 0; errors_cor = 0;

            for (int j = 0; j < error5.Length; j++)
            {
                s = sk;
                int[] sBossDecode = s.Select(ch => int.Parse(ch.ToString())).ToArray();
                for (int u = 0; u < 7; u++)
                {
                    if (error5[j][u] == 1) sBossDecode[u] = (sBossDecode[u] + 1) % 2;
                }

                errors = String.Join("", error5[j]);
                was = String.Join("", sBossDecode);
                s = String.Join("", sBossDecode);

                temp = 0;
                for (int i = 0; i < s.Length; i++)
                    temp += sBossDecode[i] * sBoss1[i];
                if (temp > 1)
                    tem[0] = Convert.ToString(temp % 2);
                else
                    tem[0] = Convert.ToString(temp);
                //Console.WriteLine("r1 = {0} ", tem[0]);

                if (numBit >= 1)
                {
                    temp2 = 0;
                    for (int i = 0; i < s.Length; i++)
                        temp2 += sBossDecode[i] * sBoss2[i];
                    if (temp2 > 1)
                        tem[1] = Convert.ToString(temp2 % 2);
                    else
                        tem[1] = Convert.ToString(temp2);
                    //Console.WriteLine("r2 = {0} ", tem[1]);
                }

                if (numBit >= 3)
                {
                    temp3 = 0;
                    for (int i = 0; i < s.Length; i++)
                        temp3 += sBossDecode[i] * sBoss3[i];
                    if (temp3 > 1)
                        tem[2] = Convert.ToString(temp3 % 2);
                    else
                        tem[2] = Convert.ToString(temp3);
                    //Console.WriteLine("r3 = {0} ", tem[2]);
                }

                if (numBit >= 4)
                {
                    temp4 = 0;
                    for (int i = 0; i < s.Length; i++)
                        temp4 += sBossDecode[i] * sBoss4[i];
                    if (temp4 > 1)
                        tem[3] = Convert.ToString(temp4 % 2);
                    else
                        tem[3] = Convert.ToString(temp4);
                    //Console.WriteLine("r4 = {0} ", tem[3]);
                }

                if (numBit >= 5)
                {
                    temp5 = 0;
                    for (int i = 0; i < s.Length; i++)
                        temp5 += sBossDecode[i] * sBoss5[i];
                    if (temp5 > 1)
                        tem[4] = Convert.ToString(temp5 % 2);
                    else
                        tem[4] = Convert.ToString(temp5);
                    //Console.WriteLine("r5 = {0} ", tem[4]);
                }

                plus = 0;

                if (tem[0] != "0")
                    plus += 1;
                if (numBit >= 1)
                {
                    if (tem[1] != "0")
                        plus += 2;
                }
                if (numBit >= 3)
                {
                    if (tem[2] != "0")
                        plus += 4;
                }
                if (numBit >= 4)
                {
                    if (tem[3] != "0")
                        plus += 8;
                }
                if (numBit >= 5)
                {
                    if (tem[4] != "0")
                        plus += 16;
                }

                if (plus > 0)
                {
                    if (s[plus - 1] == '1')
                    {
                        s = s.Remove(plus - 1, 1).Insert(plus - 1, "0");
                    }
                    else
                    {
                        s = s.Remove(plus - 1, 1).Insert(plus - 1, "1");
                    }
                }

                if (sk == s) errors_cor++;
                if (plus > 0) errors_num++;
            }
            Console.WriteLine("|     5       |       {0}%     |       {1}%       |      {2}      |        {3}         |      {4}     |", System.Math.Round((double)(errors_num * 100 / error5.Length)), System.Math.Round((double)(errors_cor * 100 / error5.Length)), errors_num, errors_cor, error5.Length);
            Console.WriteLine("_________________________________________________________________________________________________");


            errors_num = 0; errors_cor = 0;
            for (int j = 0; j < error6.Length; j++)
            {
                s = sk;
                int[] sBossDecode = s.Select(ch => int.Parse(ch.ToString())).ToArray();
                for (int u = 0; u < 7; u++)
                {
                    if (error6[j][u] == 1) sBossDecode[u] = (sBossDecode[u] + 1) % 2;
                }

                errors = String.Join("", error6[j]);
                was = String.Join("", sBossDecode);
                s = String.Join("", sBossDecode);

                temp = 0;
                for (int i = 0; i < s.Length; i++)
                    temp += sBossDecode[i] * sBoss1[i];
                if (temp > 1)
                    tem[0] = Convert.ToString(temp % 2);
                else
                    tem[0] = Convert.ToString(temp);
                //Console.WriteLine("r1 = {0} ", tem[0]);

                if (numBit >= 1)
                {
                    temp2 = 0;
                    for (int i = 0; i < s.Length; i++)
                        temp2 += sBossDecode[i] * sBoss2[i];
                    if (temp2 > 1)
                        tem[1] = Convert.ToString(temp2 % 2);
                    else
                        tem[1] = Convert.ToString(temp2);
                    //Console.WriteLine("r2 = {0} ", tem[1]);
                }

                if (numBit >= 3)
                {
                    temp3 = 0;
                    for (int i = 0; i < s.Length; i++)
                        temp3 += sBossDecode[i] * sBoss3[i];
                    if (temp3 > 1)
                        tem[2] = Convert.ToString(temp3 % 2);
                    else
                        tem[2] = Convert.ToString(temp3);
                    //Console.WriteLine("r3 = {0} ", tem[2]);
                }

                if (numBit >= 4)
                {
                    temp4 = 0;
                    for (int i = 0; i < s.Length; i++)
                        temp4 += sBossDecode[i] * sBoss4[i];
                    if (temp4 > 1)
                        tem[3] = Convert.ToString(temp4 % 2);
                    else
                        tem[3] = Convert.ToString(temp4);
                    //Console.WriteLine("r4 = {0} ", tem[3]);
                }

                if (numBit >= 5)
                {
                    temp5 = 0;
                    for (int i = 0; i < s.Length; i++)
                        temp5 += sBossDecode[i] * sBoss5[i];
                    if (temp5 > 1)
                        tem[4] = Convert.ToString(temp5 % 2);
                    else
                        tem[4] = Convert.ToString(temp5);
                    //Console.WriteLine("r5 = {0} ", tem[4]);
                }

                plus = 0;

                if (tem[0] != "0")
                    plus += 1;
                if (numBit >= 1)
                {
                    if (tem[1] != "0")
                        plus += 2;
                }
                if (numBit >= 3)
                {
                    if (tem[2] != "0")
                        plus += 4;
                }
                if (numBit >= 4)
                {
                    if (tem[3] != "0")
                        plus += 8;
                }
                if (numBit >= 5)
                {
                    if (tem[4] != "0")
                        plus += 16;
                }

                if (plus > 0)
                {
                    if (s[plus - 1] == '1')
                    {
                        s = s.Remove(plus - 1, 1).Insert(plus - 1, "0");
                    }
                    else
                    {
                        s = s.Remove(plus - 1, 1).Insert(plus - 1, "1");
                    }
                }

                if (sk == s) errors_cor++;
                if (plus > 0) errors_num++;
            }
            Console.WriteLine("|     6       |       {0}%     |       {1}%       |      {2}       |        {3}         |      {4}      |", System.Math.Round((double)(errors_num * 100 / error6.Length)), System.Math.Round((double)(errors_cor * 100 / error6.Length)), errors_num, errors_cor, error6.Length);
            Console.WriteLine("_________________________________________________________________________________________________");


            errors_num = 0; errors_cor = 0;
            for (int j = 0; j < error7.Length; j++)
            {
                s = sk;
                int[] sBossDecode = s.Select(ch => int.Parse(ch.ToString())).ToArray();
                for (int u = 0; u < 7; u++)
                {
                    if (error7[j][u] == 1) sBossDecode[u] = (sBossDecode[u] + 1) % 2;
                }

                errors = String.Join("", error7[j]);
                was = String.Join("", sBossDecode);
                s = String.Join("", sBossDecode);

                temp = 0;
                for (int i = 0; i < s.Length; i++)
                    temp += sBossDecode[i] * sBoss1[i];
                if (temp > 1)
                    tem[0] = Convert.ToString(temp % 2);
                else
                    tem[0] = Convert.ToString(temp);
                //Console.WriteLine("r1 = {0} ", tem[0]);

                if (numBit >= 1)
                {
                    temp2 = 0;
                    for (int i = 0; i < s.Length; i++)
                        temp2 += sBossDecode[i] * sBoss2[i];
                    if (temp2 > 1)
                        tem[1] = Convert.ToString(temp2 % 2);
                    else
                        tem[1] = Convert.ToString(temp2);
                    //Console.WriteLine("r2 = {0} ", tem[1]);
                }

                if (numBit >= 3)
                {
                    temp3 = 0;
                    for (int i = 0; i < s.Length; i++)
                        temp3 += sBossDecode[i] * sBoss3[i];
                    if (temp3 > 1)
                        tem[2] = Convert.ToString(temp3 % 2);
                    else
                        tem[2] = Convert.ToString(temp3);
                    //Console.WriteLine("r3 = {0} ", tem[2]);
                }

                if (numBit >= 4)
                {
                    temp4 = 0;
                    for (int i = 0; i < s.Length; i++)
                        temp4 += sBossDecode[i] * sBoss4[i];
                    if (temp4 > 1)
                        tem[3] = Convert.ToString(temp4 % 2);
                    else
                        tem[3] = Convert.ToString(temp4);
                    //Console.WriteLine("r4 = {0} ", tem[3]);
                }

                if (numBit >= 5)
                {
                    temp5 = 0;
                    for (int i = 0; i < s.Length; i++)
                        temp5 += sBossDecode[i] * sBoss5[i];
                    if (temp5 > 1)
                        tem[4] = Convert.ToString(temp5 % 2);
                    else
                        tem[4] = Convert.ToString(temp5);
                    //Console.WriteLine("r5 = {0} ", tem[4]);
                }

                plus = 0;

                if (tem[0] != "0")
                    plus += 1;
                if (numBit >= 1)
                {
                    if (tem[1] != "0")
                        plus += 2;
                }
                if (numBit >= 3)
                {
                    if (tem[2] != "0")
                        plus += 4;
                }
                if (numBit >= 4)
                {
                    if (tem[3] != "0")
                        plus += 8;
                }
                if (numBit >= 5)
                {
                    if (tem[4] != "0")
                        plus += 16;
                }

                if (plus > 0)
                {
                    if (s[plus - 1] == '1')
                    {
                        s = s.Remove(plus - 1, 1).Insert(plus - 1, "0");
                    }
                    else
                    {
                        s = s.Remove(plus - 1, 1).Insert(plus - 1, "1");
                    }
                }

                if (sk == s) errors_cor++;
                if (plus > 0) errors_num++;
            }
            Console.WriteLine("|     7       |       {0}%       |       {1}%       |      {2}       |        {3}         |      {4}      |", System.Math.Round((double)(errors_num * 100 / error7.Length)), System.Math.Round((double)(errors_cor * 100 / error7.Length)), errors_num, errors_cor, error7.Length);
            Console.WriteLine("_________________________________________________________________________________________________");


            Console.WriteLine("\n\nВектора ошибок кратности 1: ");
            for (int i = 0; i < error1.Length; i++)
            {
                errors = String.Join("", error1[i]);
                Console.Write(errors);
                Console.Write(", ");
            }
            Console.WriteLine("\n\nВектора ошибок кратности 2: ");
            for (int i = 0; i < error2.Length; i++)
            {
                errors = String.Join("", error2[i]);
                Console.Write(errors);
                Console.Write(", ");
                if (((i + 1) % 7 == 0) && (i > 0)) Console.Write("\n");
            }
            Console.WriteLine("\n\nВектора ошибок кратности 3: ");
            for (int i = 0; i < error3.Length; i++)
            {
                errors = String.Join("", error3[i]);
                Console.Write(errors);
                Console.Write(", ");
                if (((i + 1) % 7 == 0) && (i > 0)) Console.Write("\n");
            }
            Console.WriteLine("\n\nВектора ошибок кратности 4: ");
            for (int i = 0; i < error4.Length; i++)
            {
                errors = String.Join("", error4[i]);
                Console.Write(errors);
                Console.Write(", ");
                if (((i + 1) % 7 == 0) && (i > 0)) Console.Write("\n");
            }
            Console.WriteLine("\n\nВектора ошибок кратности 5: ");
            for (int i = 0; i < error5.Length; i++)
            {
                errors = String.Join("", error5[i]);
                Console.Write(errors);
                Console.Write(", ");
                if (((i + 1) % 7 == 0) && (i > 0)) Console.Write("\n");
            }
            Console.WriteLine("\n\nВектора ошибок кратности 6: ");
            for (int i = 0; i < error6.Length; i++)
            {
                errors = String.Join("", error6[i]);
                Console.Write(errors);
                Console.Write(", ");
                if (((i + 1) % 7 == 0) && (i > 0)) Console.Write("\n");
            }
            Console.WriteLine("\n\nВектора ошибок кратности 7: ");
            for (int i = 0; i < error7.Length; i++)
            {
                errors = String.Join("", error7[i]);
                Console.Write(errors);
            }

            Console.ReadKey();
        }
    }
}
