using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        const int MaxN = 4; // максимально допустимая размерность матрицы и вектора
        int n = 3; // текущая размерность матрицы
        TextBox[,] MatrText = null; // матрица элементов типа TextBox
        double[,] Matr1 = new double[MaxN, MaxN]; // матрица 1 чисел с плавающей точкой
        double[,] Matr2 = new double[MaxN, MaxN]; // матрица 2 чисел с плавающей точкой
        double[,] Matr3 = new double[MaxN, MaxN]; // матрица результатов
        bool f1; // флажок, который указывает о вводе данных в матрицу Matr1
        bool f2; // флажок, который указывает о вводе данных в матрицу Matr2
        int dx = 40, dy = 20; // ширина и высота ячейки в MatrText[,]
        Form2 form2 = null;   // экземпляр (объект) класса формы Form2

        TextBox[] VectorText = null;

        int[] Vect1 = new int[MaxN];
        int[] Vect2 = new int[MaxN];
        int[] Vect3 = new int[MaxN];
        bool f3; // флажок, который указывает о вводе данных в матрицу Vect1
        bool f4; // флажок, который указывает о вводе данных в матрицу Vect2
        int bufConst;
        private void Clear_MatrText()
        {
            // Обнуление ячеек MatrText
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    MatrText[i, j].Text = "0";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // І. Инициализация элементов управления и внутренних переменных
            textBox1.Text = "";
            f1 = f2 = f3 = f4 = false; // матрицы и вектора еще не заполнены
            label2.Text = "false";
            label3.Text = "false";
            label4.Text = "false";
            label5.Text = "false";

            // ІІ. Выделение памяти и настройка MatrText
            int i, j;

            // 1. Выделение памяти для формы Form2
            form2 = new Form2();

            // 2. Выделение памяти под самую матрицу
            MatrText = new TextBox[MaxN, MaxN];
            VectorText = new TextBox[MaxN];

            // 3. Выделение памяти для каждой ячейки матрицы и ее настройка
            for (i = 0; i < MaxN; i++)
                for (j = 0; j < MaxN; j++)
                {
                    // 3.1. Выделить память
                    MatrText[i, j] = new TextBox();

                    // 3.2. Обнулить эту ячейку
                    MatrText[i, j].Text = "0";

                    // 3.3. Установить позицию ячейки в форме Form2
                    MatrText[i, j].Location = new System.Drawing.Point(10 + i * dx, 10 + j * dy);

                    // 3.4. Установить размер ячейки
                    MatrText[i, j].Size = new System.Drawing.Size(dx, dy);

                    // 3.5. Пока что спрятать ячейку
                    MatrText[i, j].Visible = false;

                    // 3.6. Добавить MatrText[i,j] в форму form2
                    form2.Controls.Add(MatrText[i, j]);
                }


                for (j = 0; j < MaxN; j++)
                {
                // 3.1. Выделить память
                VectorText[j] = new TextBox();

                // 3.2. Обнулить эту ячейку
                VectorText[j].Text = "0";

                // 3.3. Установить позицию ячейки в форме Form2
                VectorText[j].Location = new System.Drawing.Point(10 + dx, 10 + j * dy);

                // 3.4. Установить размер ячейки
                VectorText[j].Size = new System.Drawing.Size(dx, dy);

                // 3.5. Пока что спрятать ячейку
                VectorText[j].Visible = false;

                    // 3.6. Добавить MatrText[i,j] в форму form2
                    form2.Controls.Add(VectorText[j]);
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 1. Чтение размерности матрицы
            if (textBox1.Text == "") return;
            n = int.Parse(textBox1.Text);

            // 2. Обнуление ячейки MatrText
            Clear_MatrText();

            // 3. Настройка свойств ячеек матрицы MatrText
            //    с привязкой к значению n и форме Form2
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    // 3.1. Порядок табуляции
                    MatrText[i, j].TabIndex = i * n + j + 1;

                    // 3.2. Сделать ячейку видимой
                    MatrText[i, j].Visible = true;
                }

            // 4. Корректировка размеров формы
            form2.Width = 10 + n * dx + 20;
            form2.Height = 10 + n * dy + form2.button1.Height + 50;

            // 5. Корректировка позиции и размеров кнопки на форме Form2
            form2.button1.Left = 10;
            form2.button1.Top = 10 + n * dy + 10;
            form2.button1.Width = form2.Width - 30;

            // 6. Вызов формы Form2
            if (form2.ShowDialog() == DialogResult.OK)
            {
                // 7. Перенос строк из формы Form2 в матрицу Matr1
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        if (MatrText[i, j].Text != "")
                            Matr1[i, j] = Double.Parse(MatrText[i, j].Text);
                        else
                            Matr1[i, j] = 0;
                // 8. Данные в матрицу Matr1 внесены
                f1 = true;
                label2.Text = "true";
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            // 1. Чтение размерности матрицы
            if (textBox1.Text == "") return;
            n = int.Parse(textBox1.Text);

            // 2. Обнулить ячейки MatrText
            Clear_MatrText();

            // 3. Настройка свойств ячеек матрицы MatrText
            //    с привязкой к значению n и форме Form2
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    // 3.1. Порядок табуляции
                    MatrText[i, j].TabIndex = i * n + j + 1;

                    // 3.2. Сделать ячейку видимой
                    MatrText[i, j].Visible = true;
                }

            // 4. Корректировка размеров формы
            form2.Width = 10 + n * dx + 20;
            form2.Height = 10 + n * dy + form2.button1.Height + 50;

            // 5. Корректировка позиции и размеров кнопки на форме Form2
            form2.button1.Left = 10;
            form2.button1.Top = 10 + n * dy + 10;
            form2.button1.Width = form2.Width - 30;

            // 6. Вызов формы Form2
            if (form2.ShowDialog() == DialogResult.OK)
            {
                // 7. Перенос строк из формы Form2 в матрицу Matr2
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        Matr2[i, j] = Double.Parse(MatrText[i, j].Text);

                // 8. Матрица Matr2 сформирована
                f2 = true;
                label3.Text = "true";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FileStream fw = null;
            string msg;
            byte[] msgByte = null; // байтовый массив

            // 1. Открыть файл для записи
            fw = new FileStream("Res_Matr.txt", FileMode.Create);

            // 2. Запись матрицы результата в файл
            // 2.1. Сначала записать число элементов матрицы Matr3
            msg = n.ToString() + "\r\n";
            // перевод строки msg в байтовый массив msgByte
            msgByte = Encoding.Default.GetBytes(msg);

            // запись массива msgByte в файл
            fw.Write(msgByte, 0, msgByte.Length);

            // 2.2. Теперь записать саму матрицу
            msg = "";
            for (int i = 0; i < n; i++)
            {
                // формируем строку msg из элементов матрицы
                for (int j = 0; j < n; j++)
                    msg = msg + Matr3[i, j].ToString() + "  ";
                msg = msg + "\r\n"; // добавить перевод строки
            }

            // 3. Перевод строки msg в байтовый массив msgByte
            msgByte = Encoding.Default.GetBytes(msg);

            // 4. запись строк матрицы в файл
            fw.Write(msgByte, 0, msgByte.Length);

            // 5. Закрыть файл
            if (fw != null) fw.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 1. Проверка, введены ли данные в обеих матрицах
            if (!((f1 == true) && (f2 == true))) return;

            // 2. Вычисление произведения матриц. Результат в Matr3
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    Matr3[j, i] = 0;
                    for (int k = 0; k < n; k++)
                        Matr3[j, i] = Matr3[j, i] + Matr1[k, i] * Matr2[j, k];
                }

            // 3. Внесение данных в MatrText
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    // 3.1. Порядок табуляции
                    MatrText[i, j].TabIndex = i * n + j + 1;

                    // 3.2. Перевести число в строку
                    MatrText[i, j].Text = Matr3[i, j].ToString();
                }

            // 4. Вывод формы
            form2.ShowDialog();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            int nn;
            nn = Int16.Parse(textBox1.Text);
            if (nn != n)
            {
                f1 = f2 = false;
                label2.Text = "false";
                label3.Text = "false";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // 1. Проверка, введены ли данные в обеих матрицах
            if (!((f1 == true) && (f2 == true))) return;

            // 2. Вычисление произведения матриц. Результат в Matr3
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    Matr3[j, i] = Matr1[j, i] + Matr2[j, i];
                }

            // 3. Внесение данных в MatrText
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    // 3.1. Порядок табуляции
                    MatrText[i, j].TabIndex = i * n + j + 1;

                    // 3.2. Перевести число в строку
                    MatrText[i, j].Text = Matr3[i, j].ToString();
                }

            // 4. Вывод формы
            form2.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // 1. Проверка, введены ли данные в обеих матрицах
            if (!((f1 == true) && (f2 == true))) return;

            // 2. Вычисление произведения матриц. Результат в Matr3
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    Matr3[j, i] = Matr1[j, i] - Matr2[j, i];
                }

            // 3. Внесение данных в MatrText
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    // 3.1. Порядок табуляции
                    MatrText[i, j].TabIndex = i * n + j + 1;

                    // 3.2. Перевести число в строку
                    MatrText[i, j].Text = Matr3[i, j].ToString();
                }

            // 4. Вывод формы
            form2.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // 1. Чтение размерности матрицы
            if (textBox1.Text == "") return;
            n = int.Parse(textBox1.Text);

            // 2. Обнуление ячейки MatrText
            for (int i = 0; i < n; i++)
                VectorText[i].Text = "0";

            // 3. Настройка свойств ячеек матрицы MatrText
            //    с привязкой к значению n и форме Form2
            for (int i = 0; i < n; i++)
            {
                // 3.1. Порядок табуляции
                VectorText[i].TabIndex = i * n + 1;

                // 3.2. Сделать ячейку видимой
                VectorText[i].Visible = true;
            }

            // 4. Корректировка размеров формы
            form2.Width = 10 + n * dx + 20;
            form2.Height = 10 + n * dy + form2.button1.Height + 50;

            // 5. Корректировка позиции и размеров кнопки на форме Form2
            form2.button1.Left = 10;
            form2.button1.Top = 10 + n * dy + 10;
            form2.button1.Width = form2.Width - 30;

            // 6. Вызов формы Form2
            if (form2.ShowDialog() == DialogResult.OK)
            {
                // 7. Перенос строк из формы Form2 в матрицу Matr1
                for (int i = 0; i < n; i++)
                        if (VectorText[i].Text != "")
                        Vect1[i] = int.Parse(VectorText[i].Text);
                        else
                        Vect1[i] = 0;
                // 8. Данные в матрицу Matr1 внесены
                f3 = true;
                label4.Text = "true";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // 1. Чтение размерности матрицы
            if (textBox1.Text == "") return;
            n = int.Parse(textBox1.Text);

            // 2. Обнуление ячейки MatrText
            for (int i = 0; i < n; i++)
                VectorText[i].Text = "0";

            
            //    с привязкой к значению n и форме Form2
            for (int i = 0; i < n; i++)
            {
                // 3.1. Порядок табуляции
                VectorText[i].TabIndex = i * n + 1;

                // 3.2. Сделать ячейку видимой
                VectorText[i].Visible = true;
            }

            // 4. Корректировка размеров формы
            form2.Width = 10 + n * dx + 20;
            form2.Height = 10 + n * dy + form2.button1.Height + 50;

            // 5. Корректировка позиции и размеров кнопки на форме Form2
            form2.button1.Left = 10;
            form2.button1.Top = 10 + n * dy + 10;
            form2.button1.Width = form2.Width - 30;

            // 6. Вызов формы Form2
            if (form2.ShowDialog() == DialogResult.OK)
            {
                // 7. Перенос строк из формы Form2 в матрицу Matr1
                for (int i = 0; i < n; i++)
                    if (VectorText[i].Text != "")
                        Vect2[i] = int.Parse(VectorText[i].Text);
                    else
                        Vect2[i] = 0;
                // 8. Данные в матрицу Matr1 внесены
                f4 = true;
                label5.Text = "true";
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int result = 0;
            // 1. Проверка, введены ли данные в обеих матрицах
            if (!((f3 == true) && (f4 == true))) return;

            // 2. Вычисление произведения матриц. Результат в Matr3
            for (int i = 0; i < n; i++)
            {
                result += (Vect1[i] * Vect2[i]);
            }

            // 3. Внесение данных в MatrText
            for (int i = 0; i < 1; i++)
                {
                    // 3.1. Порядок табуляции
                    VectorText[i].TabIndex = i * n + 1;

                // 3.2. Перевести число в строку
                VectorText[i].Text = result.ToString();
                }
            for (int i = 1; i < n; i++)
                VectorText[i].Text = "0";

            // 4. Вывод формы
            form2.ShowDialog();

            for (int i = 1; i < n; i++)
                VectorText[i].Text = Vect3.ToString();
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            bufConst = Int16.Parse(textBox2.Text);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // 1. Проверка, введены ли данные в обеих матрицах
            if (!(f3 == true)) return;

            // 2. Вычисление произведения матриц. Результат в Matr3
            for (int i = 0; i < n; i++)
            {
                Vect3[i] = Vect1[i] * bufConst;
            }

            // 3. Внесение данных в MatrText
            for (int i = 0; i < n; i++)
            {
                // 3.1. Порядок табуляции
                VectorText[i].TabIndex = i * n + 1;

                // 3.2. Перевести число в строку
                VectorText[i].Text = Vect3[i].ToString();
            }

            // 4. Вывод формы
            form2.ShowDialog();
        }

        public Form1()
        {
            InitializeComponent();
        }
    }
}
