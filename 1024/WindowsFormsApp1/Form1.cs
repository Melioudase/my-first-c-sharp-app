using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        Button[,] buttons = new Button[4, 4]; //按钮的数组

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        void CreateButtons(int r,int c,int text)
        {
            int x0 = 100, y0 = 10, w = 45, d = 50;
            Button btn = new Button();
            btn.Top = y0 + r * d;
            btn.Left = x0 + c * d;
            btn.Width = w;
            btn.Height = w;
            btn.Text = text.ToString();
            btn.Tag = r * 4 + c;
            btn.Visible = true;
            buttons[r, c] = btn;
            this.Controls.Add(btn); 


        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
            int r, c;
            Random rnd = new Random();
            r = rnd.Next(4);
            c = rnd.Next(4);
            //向上合并
            if (e.KeyData == Keys.W)
            {
                Up();
                //判断是否输
                if (Lose())
                {
                    return;
                }
                if (Empty())//判断是否仍有空位
                {
                    while (buttons[r, c] != null)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            r = rnd.Next(4);
                            c = rnd.Next(4);
                        }
                    }
                    CreateButtons(r, c, 2);
                }
            }
            //向下合并
            if (e.KeyData == Keys.S)
            {
                Down();
                if (Lose())
                {
                    return;
                }
                if (Empty())
                {
                    while (buttons[r, c] != null)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            r = rnd.Next(4);
                            c = rnd.Next(4);
                        }
                    }
                    CreateButtons(r, c, 2);
                }
            }
            //向左合并
            if (e.KeyData == Keys.D)
            {
                NewLeft();
                if (Lose())
                {
                    return;
                }
                if (Empty())
                {
                    while (buttons[r, c] != null)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            r = rnd.Next(4);
                            c = rnd.Next(4);
                        }
                    }
                    CreateButtons(r, c, 2);
                }
            }
            //向右合并
            if (e.KeyData == Keys.A)
            {
                NewRight();
                if (Lose())
                {
                    return;
                }
                if (Empty())
                {
                    while (buttons[r, c] != null)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            r = rnd.Next(4);
                            c = rnd.Next(4);
                        }
                    }
                    CreateButtons(r, c, 2);
                }
            }
                //随机在4*4的矩阵中生成一个button并保证不重复
                //统计分数
                Score();
                //给方块染上颜色
                NewPaint();
        }


        bool Empty()
        {
            int x, y, z;
            z = 0;
            for (x=0;x<4;x++)
                for (y=0;y<4;y++)
                    if (buttons[x, y] == null)
                        z++;

            if (z == 0)
                return false;
            else
                return true;
        }

        bool Lose()
        {
            int x, y, z;
            z = 0;
            if (Empty())
                return false;
            else
            {
                for (x = 0; x < 3; x++)
                    for (y = 0; y < 3; y++)
                        if (buttons[x, y].Text == buttons[x + 1, y].Text || buttons[x, y].Text == buttons[x, y + 1].Text)
                            z++;
                if (z == 0)
                {
                    this.label3.Text = "YOU LOSE";
                    return true;
                }
                else
                    return false; 
            }
        }
       
        void NewPaint()
        {
            for (int i=0;i<4;i++)
                for (int j=0;j<4;j++)
                {
                    if (buttons[i, j] != null)
                    {
                        int temp;
                        temp = int.Parse(buttons[i, j].Text);
                        buttons[i, j].BackColor = Color.FromArgb(255-64/temp, 400/temp , 50);
                    }
                }
        }
        void DeleteButtons(int x,int y)
        {
            this.Controls.Remove(buttons[x, y]);
            buttons[x, y] = null;
        }
        void Score()
        {
            int x = 0;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    if (buttons[i,j]!=null)
                        x += int.Parse(buttons[i, j].Text);
                }
            this.label2.Text = "得分：" + x;
        }

        void NewRight()
        {
            for (int i = 0; i < 4; i++)
            {
                int temp;
                if (buttons[i, 0] != null && buttons[i, 1] != null)
                {
                    if (int.Parse(buttons[i, 0].Text) == int.Parse(buttons[i, 1].Text))
                    {
                        buttons[i, 0].Text = (int.Parse(buttons[i, 0].Text) + int.Parse(buttons[i, 1].Text)).ToString();
                        DeleteButtons(i, 1);
                        if (buttons[i, 2] != null && buttons[i, 3] != null)
                        {
                            if (int.Parse(buttons[i, 2].Text) == int.Parse(buttons[i, 3].Text))
                            {
                                temp = int.Parse(buttons[i, 3].Text) + int.Parse(buttons[i, 2].Text);
                                CreateButtons(i, 1, temp);
                                DeleteButtons(i, 2);
                                DeleteButtons(i, 3);
                            }
                            else
                            {
                                temp = int.Parse(buttons[i, 2].Text);
                                CreateButtons(i, 1, temp);
                                DeleteButtons(i, 2);
                                temp = int.Parse(buttons[i, 3].Text);
                                CreateButtons(i, 2, temp);
                                DeleteButtons(i, 3);
                            }
                        }
                        else if (buttons[i, 2] != null && buttons[i, 3] == null)
                        {
                            temp = int.Parse(buttons[i, 2].Text);
                            CreateButtons(i, 1, temp);
                            DeleteButtons(i, 2);
                        }
                        else if (buttons[i, 2] == null && buttons[i, 3] != null)
                        {
                            temp = int.Parse(buttons[i, 3].Text);
                            CreateButtons(i, 1, temp);
                            DeleteButtons(i, 3);
                        }
                        else
                        { }
                    }
                    else
                    {
                        if (buttons[i, 2] != null && buttons[i, 3] != null)
                        {
                            if (int.Parse(buttons[i, 2].Text) == int.Parse(buttons[i, 1].Text))
                            {
                                temp = int.Parse(buttons[i, 2].Text) + int.Parse(buttons[i, 1].Text);
                                buttons[i, 1].Text = temp.ToString();
                                buttons[i, 2].Text = int.Parse(buttons[i, 3].Text).ToString();
                                DeleteButtons(i, 3);
                            }
                            else
                            {
                                if (int.Parse(buttons[i, 2].Text) == int.Parse(buttons[i, 3].Text))
                                {
                                    buttons[i, 2].Text = (int.Parse(buttons[i, 2].Text) + int.Parse(buttons[i, 3].Text)).ToString();
                                    DeleteButtons(i, 3);
                                }
                            }
                        }
                        else if (buttons[i, 2] != null && buttons[i, 3] == null)
                        {
                            if (int.Parse(buttons[i, 2].Text) == int.Parse(buttons[i, 1].Text))
                            {
                                temp = int.Parse(buttons[i, 2].Text) + int.Parse(buttons[i, 1].Text);
                                buttons[i, 1].Text = temp.ToString();
                                DeleteButtons(i, 2);
                            }

                        }
                        else if (buttons[i, 2] == null && buttons[i, 3] != null)
                        {
                            if (int.Parse(buttons[i, 3].Text) == int.Parse(buttons[i, 1].Text))
                            {
                                buttons[i, 1].Text = (int.Parse(buttons[i, 1].Text) + int.Parse(buttons[i, 3].Text)).ToString();
                                DeleteButtons(i, 3);
                            }
                            else
                            {
                                temp = int.Parse(buttons[i, 3].Text);
                                CreateButtons(i, 2, temp);
                                DeleteButtons(i, 3);
                            }
                        }
                        else
                        { }
                    }
                }
                else if (buttons[i, 0] != null && buttons[i, 1] == null)
                {
                    if (buttons[i, 2] != null && buttons[i, 3] != null)
                    {
                        if (int.Parse(buttons[i, 2].Text) == int.Parse(buttons[i, 0].Text))
                        {
                            buttons[i, 0].Text = (int.Parse(buttons[i, 2].Text) + int.Parse(buttons[i, 2].Text)).ToString();
                            DeleteButtons(i, 2);
                            temp = int.Parse(buttons[i, 3].Text);
                            CreateButtons(i, 1, temp);
                            DeleteButtons(i, 3);
                        }
                        else
                        {
                            if (int.Parse(buttons[i, 2].Text) == int.Parse(buttons[i, 3].Text))
                            {
                                temp = int.Parse(buttons[i, 2].Text) + int.Parse(buttons[i, 2].Text);
                                CreateButtons(i, 1, temp);
                                DeleteButtons(i, 2);
                                DeleteButtons(i, 3);
                            }
                            else
                            {
                                temp = int.Parse(buttons[i, 2].Text);
                                CreateButtons(i, 1, temp);
                                buttons[i, 2].Text = int.Parse(buttons[i, 3].Text).ToString();
                                DeleteButtons(i, 3);
                            }
                        }
                    }
                    else if (buttons[i, 2] != null && buttons[i, 3] == null)
                    {
                        if (int.Parse(buttons[i, 2].Text) == int.Parse(buttons[i, 0].Text))
                        {
                            buttons[i, 0].Text = (int.Parse(buttons[i, 2].Text) + int.Parse(buttons[i, 0].Text)).ToString();
                            DeleteButtons(i, 2);
                        }
                        else
                        {
                            temp = int.Parse(buttons[i, 2].Text);
                            CreateButtons(i, 1, temp);
                            DeleteButtons(i, 2);
                        }
                    }
                    else if (buttons[i, 2] == null && buttons[i, 3] != null)
                    {
                        if (int.Parse(buttons[i, 0].Text) == int.Parse(buttons[i, 3].Text))
                        {
                            buttons[i, 0].Text = (int.Parse(buttons[i, 3].Text) + int.Parse(buttons[i, 0].Text)).ToString();
                            DeleteButtons(i, 3);
                        }
                        else
                        {
                            temp = int.Parse(buttons[i, 3].Text);
                            CreateButtons(i, 1, temp);
                            DeleteButtons(i, 3);
                        }
                    }
                    else
                    { }
                }
                else if (buttons[i, 0] == null && buttons[i, 1] != null)
                {
                    if (buttons[i, 2] != null && buttons[i, 3] != null)
                    {
                        if (int.Parse(buttons[i, 1].Text) == int.Parse(buttons[i, 2].Text))
                        {
                            temp = int.Parse(buttons[i, 1].Text) + int.Parse(buttons[i, 2].Text);
                            CreateButtons(i, 0, temp);
                            buttons[i, 1].Text = int.Parse(buttons[i, 3].Text).ToString();
                            DeleteButtons(i, 2);
                            DeleteButtons(i, 3);
                        }
                        else
                        {
                            if (int.Parse(buttons[i, 3].Text) == int.Parse(buttons[i, 2].Text))
                            {
                                temp = int.Parse(buttons[i, 1].Text);
                                CreateButtons(i, 0, temp);
                                buttons[i, 1].Text = (int.Parse(buttons[i, 3].Text) + int.Parse(buttons[i, 2].Text)).ToString();
                                DeleteButtons(i, 2);
                                DeleteButtons(i, 3);
                            }
                            else
                            {
                                temp = int.Parse(buttons[i, 1].Text);
                                CreateButtons(i, 0, temp);
                                buttons[i, 1].Text = int.Parse(buttons[i, 2].Text).ToString();
                                buttons[i, 2].Text = int.Parse(buttons[i, 3].Text).ToString();
                                DeleteButtons(i, 3);
                            }
                        }
                    }
                    else if (buttons[i, 2] != null && buttons[i, 3] == null)
                    {
                        if (int.Parse(buttons[i, 1].Text) == int.Parse(buttons[i, 2].Text))
                        {
                            temp = int.Parse(buttons[i, 1].Text) + int.Parse(buttons[i, 2].Text);
                            CreateButtons(i, 0, temp);
                            DeleteButtons(i, 1);
                            DeleteButtons(i, 2);
                        }
                        else
                        {
                            temp = int.Parse(buttons[i, 1].Text);
                            CreateButtons(i, 0, temp);
                            buttons[i, 1].Text = int.Parse(buttons[i, 2].Text).ToString();
                            DeleteButtons(i, 2);
                        }
                    }
                    else if (buttons[i, 2] == null && buttons[i, 3] != null)
                    {
                        if (int.Parse(buttons[i, 1].Text) == int.Parse(buttons[i, 3].Text))
                        {
                            temp = int.Parse(buttons[i, 3].Text) + int.Parse(buttons[i, 1].Text);
                            CreateButtons(i, 0, temp);
                            DeleteButtons(i, 1);
                            DeleteButtons(i, 3);
                        }
                        else
                        {
                            temp = int.Parse(buttons[i, 1].Text);
                            CreateButtons(i, 0, temp);
                            buttons[i, 1].Text = int.Parse(buttons[i, 3].Text).ToString();
                            DeleteButtons(i, 3);
                        }
                    }
                    else
                    {
                        temp = int.Parse(buttons[i, 1].Text);
                        CreateButtons(i, 0, temp);
                        DeleteButtons(i, 1);
                    }
                }
                else
                {
                    if (buttons[i, 2] != null && buttons[i, 3] != null)
                    {
                        if (int.Parse(buttons[i, 3].Text) == int.Parse(buttons[i, 2].Text))
                        {
                            temp = int.Parse(buttons[i, 3].Text) + int.Parse(buttons[i, 2].Text);
                            CreateButtons(i, 0, temp);
                            DeleteButtons(i, 2);
                            DeleteButtons(i, 3);
                        }
                        else
                        {
                            temp = int.Parse(buttons[i, 2].Text);
                            CreateButtons(i, 0, temp);
                            temp = int.Parse(buttons[i, 3].Text);
                            CreateButtons(i, 1, temp);
                            DeleteButtons(i, 2);
                            DeleteButtons(i, 3);
                        }
                    }
                    else if (buttons[i, 2] == null && buttons[i, 3] != null)
                    {
                        temp = int.Parse(buttons[i, 3].Text);
                        CreateButtons(i, 0, temp);
                        DeleteButtons(i, 3);
                    }
                    else if (buttons[i, 2] != null && buttons[i, 3] == null)
                    {
                        temp = int.Parse(buttons[i, 2].Text);
                        CreateButtons(i, 0, temp);
                        DeleteButtons(i, 2);
                    }
                    else
                    { }
                }

            }

        }
        void NewLeft()
        {
            for (int i = 0; i < 4; i++)
            {
                int temp;
                if (buttons[i, 3] != null && buttons[i, 2] != null)
                {
                    if (int.Parse(buttons[i, 3].Text) == int.Parse(buttons[i, 2].Text))
                    {
                        buttons[i, 3].Text = (int.Parse(buttons[i, 3].Text) + int.Parse(buttons[i, 2].Text)).ToString();
                        DeleteButtons(i, 2);
                        if (buttons[i, 1] != null && buttons[i, 0] != null)
                        {
                            if (int.Parse(buttons[i, 1].Text) == int.Parse(buttons[i, 0].Text))
                            {
                                temp = int.Parse(buttons[i, 0].Text) + int.Parse(buttons[i, 1].Text);
                                CreateButtons(i, 2, temp);
                                DeleteButtons(i, 1);
                                DeleteButtons(i, 0);
                            }
                            else
                            {
                                temp = int.Parse(buttons[i, 1].Text);
                                CreateButtons(i, 2, temp);
                                DeleteButtons(i, 1);
                                temp = int.Parse(buttons[i, 0].Text);
                                CreateButtons(i, 1, temp);
                                DeleteButtons(i, 0);
                            }
                        }
                        else if (buttons[i, 1] != null && buttons[i, 0] == null)
                        {
                            temp = int.Parse(buttons[i, 1].Text);
                            CreateButtons(i, 2, temp);
                            DeleteButtons(i, 1);
                        }
                        else if (buttons[i, 1] == null && buttons[i, 0] != null)
                        {
                            temp = int.Parse(buttons[i, 0].Text);
                            CreateButtons(i, 2, temp);
                            DeleteButtons(i, 0);
                        }
                        else
                        { }
                    }
                    else
                    {
                        if (buttons[i, 1] != null && buttons[i, 0] != null)
                        {
                            if (int.Parse(buttons[i, 1].Text) == int.Parse(buttons[i, 2].Text))
                            {
                                temp = int.Parse(buttons[i, 1].Text) + int.Parse(buttons[i, 2].Text);
                                buttons[i, 2].Text = temp.ToString();
                                buttons[i, 1].Text = int.Parse(buttons[i, 0].Text).ToString();
                                DeleteButtons(i, 0);
                            }
                            else
                            {
                                if (int.Parse(buttons[i, 1].Text) == int.Parse(buttons[i, 0].Text))
                                {
                                    buttons[i, 1].Text = (int.Parse(buttons[i, 1].Text) + int.Parse(buttons[i, 0].Text)).ToString();
                                    DeleteButtons(i, 0);
                                }
                            }
                        }
                        else if (buttons[i, 1] != null && buttons[i, 0] == null)
                        {
                            if (int.Parse(buttons[i, 1].Text) == int.Parse(buttons[i, 2].Text))
                            {
                                temp = int.Parse(buttons[i, 1].Text) + int.Parse(buttons[i, 2].Text);
                                buttons[i, 2].Text = temp.ToString();
                                DeleteButtons(i, 1);
                            }

                        }
                        else if (buttons[i, 1] == null && buttons[i, 0] != null)
                        {
                            if (int.Parse(buttons[i, 0].Text) == int.Parse(buttons[i, 2].Text))
                            {
                                buttons[i, 2].Text = (int.Parse(buttons[i, 2].Text) + int.Parse(buttons[i, 0].Text)).ToString();
                                DeleteButtons(i, 0);
                            }
                            else
                            {
                                temp = int.Parse(buttons[i, 0].Text);
                                CreateButtons(i, 1, temp);
                                DeleteButtons(i, 0);
                            }
                        }
                        else
                        { }
                    }
                }
                else if (buttons[i, 3] != null && buttons[i, 2] == null)
                {
                    if (buttons[i, 1] != null && buttons[i, 0] != null)
                    {
                        if (int.Parse(buttons[i, 1].Text) == int.Parse(buttons[i, 3].Text))
                        {
                            buttons[i, 3].Text = (int.Parse(buttons[i, 1].Text) + int.Parse(buttons[i, 1].Text)).ToString();
                            DeleteButtons(i, 1);
                            temp = int.Parse(buttons[i, 0].Text);
                            CreateButtons(i, 2, temp);
                            DeleteButtons(i, 0);
                        }
                        else
                        {
                            if (int.Parse(buttons[i, 1].Text) == int.Parse(buttons[i, 0].Text))
                            {
                                temp = int.Parse(buttons[i, 1].Text) + int.Parse(buttons[i, 1].Text);
                                CreateButtons(i, 2, temp);
                                DeleteButtons(i, 1);
                                DeleteButtons(i, 0);
                            }
                            else
                            {
                                temp = int.Parse(buttons[i, 1].Text);
                                CreateButtons(i, 2, temp);
                                buttons[i, 1].Text = int.Parse(buttons[i, 0].Text).ToString();
                                DeleteButtons(i, 0);
                            }
                        }
                    }
                    else if (buttons[i, 1] != null && buttons[i, 0] == null)
                    {
                        if (int.Parse(buttons[i, 1].Text) == int.Parse(buttons[i, 3].Text))
                        {
                            buttons[i, 3].Text = (int.Parse(buttons[i, 1].Text) + int.Parse(buttons[i, 3].Text)).ToString();
                            DeleteButtons(i, 1);
                        }
                        else
                        {
                            temp = int.Parse(buttons[i, 1].Text);
                            CreateButtons(i, 2, temp);
                            DeleteButtons(i, 1);
                        }
                    }
                    else if (buttons[i, 1] == null && buttons[i, 0] != null)
                    {
                        if (int.Parse(buttons[i, 3].Text) == int.Parse(buttons[i, 0].Text))
                        {
                            buttons[i, 3].Text = (int.Parse(buttons[i, 0].Text) + int.Parse(buttons[i, 3].Text)).ToString();
                            DeleteButtons(i, 0);
                        }
                        else
                        {
                            temp = int.Parse(buttons[i, 0].Text);
                            CreateButtons(i, 2, temp);
                            DeleteButtons(i, 0);
                        }
                    }
                    else
                    { }
                }
                else if (buttons[i, 3] == null && buttons[i, 2] != null)
                {
                    if (buttons[i, 1] != null && buttons[i, 0] != null)
                    {
                        if (int.Parse(buttons[i, 2].Text) == int.Parse(buttons[i, 1].Text))
                        {
                            temp = int.Parse(buttons[i, 2].Text) + int.Parse(buttons[i, 1].Text);
                            CreateButtons(i, 3, temp);
                            buttons[i, 2].Text = int.Parse(buttons[i, 0].Text).ToString();
                            DeleteButtons(i, 1);
                            DeleteButtons(i, 0);
                        }
                        else
                        {
                            if (int.Parse(buttons[i, 0].Text) == int.Parse(buttons[i, 1].Text))
                            {
                                temp = int.Parse(buttons[i, 2].Text);
                                CreateButtons(i, 3, temp);
                                buttons[i, 2].Text = (int.Parse(buttons[i, 0].Text) + int.Parse(buttons[i, 1].Text)).ToString();
                                DeleteButtons(i, 1);
                                DeleteButtons(i, 0);
                            }
                            else
                            {
                                temp = int.Parse(buttons[i, 2].Text);
                                CreateButtons(i, 3, temp);
                                buttons[i, 2].Text = int.Parse(buttons[i, 1].Text).ToString();
                                buttons[i, 1].Text = int.Parse(buttons[i, 0].Text).ToString();
                                DeleteButtons(i, 0);
                            }
                        }
                    }
                    else if (buttons[i, 1] != null && buttons[i, 0] == null)
                    {
                        if (int.Parse(buttons[i, 2].Text) == int.Parse(buttons[i, 1].Text))
                        {
                            temp = int.Parse(buttons[i, 2].Text) + int.Parse(buttons[i, 1].Text);
                            CreateButtons(i, 3, temp);
                            DeleteButtons(i, 2);
                            DeleteButtons(i, 1);
                        }
                        else
                        {
                            temp = int.Parse(buttons[i, 2].Text);
                            CreateButtons(i, 3, temp);
                            buttons[i, 2].Text = int.Parse(buttons[i, 1].Text).ToString();
                            DeleteButtons(i, 1);
                        }
                    }
                    else if (buttons[i, 1] == null && buttons[i, 0] != null)
                    {
                        if (int.Parse(buttons[i, 2].Text) == int.Parse(buttons[i, 0].Text))
                        {
                            temp = int.Parse(buttons[i, 0].Text) + int.Parse(buttons[i, 2].Text);
                            CreateButtons(i, 3, temp);
                            DeleteButtons(i, 2);
                            DeleteButtons(i, 0);
                        }
                        else
                        {
                            temp = int.Parse(buttons[i, 2].Text);
                            CreateButtons(i, 3, temp);
                            buttons[i, 2].Text = int.Parse(buttons[i, 0].Text).ToString();
                            DeleteButtons(i, 0);
                        }
                    }
                    else
                    {
                        temp = int.Parse(buttons[i, 2].Text);
                        CreateButtons(i, 3, temp);
                        DeleteButtons(i, 2);
                    }
                }
                else
                {
                    if (buttons[i, 1] != null && buttons[i, 0] != null)
                    {
                        if (int.Parse(buttons[i, 0].Text) == int.Parse(buttons[i, 1].Text))
                        {
                            temp = int.Parse(buttons[i, 0].Text) + int.Parse(buttons[i, 1].Text);
                            CreateButtons(i, 3, temp);
                            DeleteButtons(i, 1);
                            DeleteButtons(i, 0);
                        }
                        else
                        {
                            temp = int.Parse(buttons[i, 1].Text);
                            CreateButtons(i, 3, temp);
                            temp = int.Parse(buttons[i, 0].Text);
                            CreateButtons(i, 2, temp);
                            DeleteButtons(i, 1);
                            DeleteButtons(i, 0);
                        }
                    }
                    else if (buttons[i, 1] == null && buttons[i, 0] != null)
                    {
                        temp = int.Parse(buttons[i, 0].Text);
                        CreateButtons(i, 3, temp);
                        DeleteButtons(i, 0);
                    }
                    else if (buttons[i, 1] != null && buttons[i, 0] == null)
                    {
                        temp = int.Parse(buttons[i, 1].Text);
                        CreateButtons(i, 3, temp);
                        DeleteButtons(i, 1);
                    }
                    else
                    { }
                }

            }

        }
        void Down()
        {
            for (int i = 0; i < 4; i++)
            {
                int temp;
                if (buttons[3, i] != null && buttons[2, i] != null)
                {
                    if (int.Parse(buttons[3, i].Text) == int.Parse(buttons[2, i].Text))
                    {
                        buttons[3, i].Text = (int.Parse(buttons[3, i].Text) + int.Parse(buttons[2, i].Text)).ToString();
                        DeleteButtons(2, i);
                        if (buttons[1, i] != null && buttons[0, i] != null)
                        {
                            if (int.Parse(buttons[1, i].Text) == int.Parse(buttons[0, i].Text))
                            {
                                temp = int.Parse(buttons[0, i].Text) + int.Parse(buttons[1, i].Text);
                                CreateButtons(2, i, temp);
                                DeleteButtons(1, i);
                                DeleteButtons(0, i);
                            }
                            else
                            {
                                temp = int.Parse(buttons[1, i].Text);
                                CreateButtons(2, i, temp);
                                DeleteButtons(1, i);
                                temp = int.Parse(buttons[0, i].Text);
                                CreateButtons(1, i, temp);
                                DeleteButtons(0, i);
                            }
                        }
                        else if (buttons[1, i] != null && buttons[0, i] == null)
                        {
                            temp = int.Parse(buttons[1, i].Text);
                            CreateButtons(2, i, temp);
                            DeleteButtons(1, i);
                        }
                        else if (buttons[1, i] == null && buttons[0, i] != null)
                        {
                            temp = int.Parse(buttons[0, i].Text);
                            CreateButtons(2, i, temp);
                            DeleteButtons(0, i);
                        }
                        else
                        { }
                    }
                    else
                    {
                        if (buttons[1, i] != null && buttons[0, i] != null)
                        {
                            if (int.Parse(buttons[1, i].Text) == int.Parse(buttons[2, i].Text))
                            {
                                temp = int.Parse(buttons[1, i].Text) + int.Parse(buttons[2, i].Text);
                                buttons[2, i].Text = temp.ToString();
                                buttons[1, i].Text = int.Parse(buttons[0, i].Text).ToString();
                                DeleteButtons(0, i);
                            }
                            else
                            {
                                if (int.Parse(buttons[1, i].Text) == int.Parse(buttons[0, i].Text))
                                {
                                    buttons[1, i].Text = (int.Parse(buttons[1, i].Text) + int.Parse(buttons[0, i].Text)).ToString();
                                    DeleteButtons(0, i);
                                }
                            }
                        }
                        else if (buttons[1, i] != null && buttons[0, i] == null)
                        {
                            if (int.Parse(buttons[1, i].Text) == int.Parse(buttons[2, i].Text))
                            {
                                temp = int.Parse(buttons[1, i].Text) + int.Parse(buttons[2, i].Text);
                                buttons[2, i].Text = temp.ToString();
                                DeleteButtons(1, i);
                            }

                        }
                        else if (buttons[1, i] == null && buttons[0, i] != null)
                        {
                            if (int.Parse(buttons[0, i].Text) == int.Parse(buttons[2, i].Text))
                            {
                                buttons[2, i].Text = (int.Parse(buttons[2, i].Text) + int.Parse(buttons[0, i].Text)).ToString();
                                DeleteButtons(0, i);
                            }
                            else
                            {
                                temp = int.Parse(buttons[0, i].Text);
                                CreateButtons(1, i, temp);
                                DeleteButtons(0, i);
                            }
                        }
                        else
                        { }
                    }
                }
                else if (buttons[3, i] != null && buttons[2, i] == null)
                {
                    if (buttons[1, i] != null && buttons[0, i] != null)
                    {
                        if (int.Parse(buttons[1, i].Text) == int.Parse(buttons[3, i].Text))
                        {
                            buttons[3, i].Text = (int.Parse(buttons[1, i].Text) + int.Parse(buttons[1, i].Text)).ToString();
                            DeleteButtons(1, i);
                            temp = int.Parse(buttons[0, i].Text);
                            CreateButtons(2, i, temp);
                            DeleteButtons(0, i);
                        }
                        else
                        {
                            if (int.Parse(buttons[1, i].Text) == int.Parse(buttons[0, i].Text))
                            {
                                temp = int.Parse(buttons[1, i].Text) + int.Parse(buttons[1, i].Text);
                                CreateButtons(2, i, temp);
                                DeleteButtons(1, i);
                                DeleteButtons(0, i);
                            }
                            else
                            {
                                temp = int.Parse(buttons[1, i].Text);
                                CreateButtons(2, i, temp);
                                buttons[1, i].Text = int.Parse(buttons[0, i].Text).ToString();
                                DeleteButtons(0, i);
                            }
                        }
                    }
                    else if (buttons[1, i] != null && buttons[0, i] == null)
                    {
                        if (int.Parse(buttons[1, i].Text) == int.Parse(buttons[3, i].Text))
                        {
                            buttons[3, i].Text = (int.Parse(buttons[1, i].Text) + int.Parse(buttons[3, i].Text)).ToString();
                            DeleteButtons(1, i);
                        }
                        else
                        {
                            temp = int.Parse(buttons[1, i].Text);
                            CreateButtons(2, i, temp);
                            DeleteButtons(1, i);
                        }
                    }
                    else if (buttons[1, i] == null && buttons[0, i] != null)
                    {
                        if (int.Parse(buttons[3, i].Text) == int.Parse(buttons[0, i].Text))
                        {
                            buttons[3, i].Text = (int.Parse(buttons[0, i].Text) + int.Parse(buttons[3, i].Text)).ToString();
                            DeleteButtons(0, i);
                        }
                        else
                        {
                            temp = int.Parse(buttons[0, i].Text);
                            CreateButtons(2, i, temp);
                            DeleteButtons(0, i);
                        }
                    }
                    else
                    { }
                }
                else if (buttons[3, i] == null && buttons[2, i] != null)
                {
                    if (buttons[1, i] != null && buttons[0, i] != null)
                    {
                        if (int.Parse(buttons[2, i].Text) == int.Parse(buttons[1, i].Text))
                        {
                            temp = int.Parse(buttons[2, i].Text) + int.Parse(buttons[1, i].Text);
                            CreateButtons(3, i, temp);
                            buttons[2, i].Text = int.Parse(buttons[0, i].Text).ToString();
                            DeleteButtons(1, i);
                            DeleteButtons(0, i);
                        }
                        else
                        {
                            if (int.Parse(buttons[0, i].Text) == int.Parse(buttons[1, i].Text))
                            {
                                temp = int.Parse(buttons[2, i].Text);
                                CreateButtons(3, i, temp);
                                buttons[2, i].Text = (int.Parse(buttons[0, i].Text) + int.Parse(buttons[1, i].Text)).ToString();
                                DeleteButtons(1, i);
                                DeleteButtons(0, i);
                            }
                            else
                            {
                                temp = int.Parse(buttons[2, i].Text);
                                CreateButtons(3, i, temp);
                                buttons[2, i].Text = int.Parse(buttons[1, i].Text).ToString();
                                buttons[1, i].Text = int.Parse(buttons[0, i].Text).ToString();
                                DeleteButtons(0, i);
                            }
                        }
                    }
                    else if (buttons[1, i] != null && buttons[0, i] == null)
                    {
                        if (int.Parse(buttons[2, i].Text) == int.Parse(buttons[1, i].Text))
                        {
                            temp = int.Parse(buttons[2, i].Text) + int.Parse(buttons[1, i].Text);
                            CreateButtons(3, i, temp);
                            DeleteButtons(2, i);
                            DeleteButtons(1, i);
                        }
                        else
                        {
                            temp = int.Parse(buttons[2, i].Text);
                            CreateButtons(3, i, temp);
                            buttons[2, i].Text = int.Parse(buttons[1, i].Text).ToString();
                            DeleteButtons(1, i);
                        }
                    }
                    else if (buttons[1, i] == null && buttons[0, i] != null)
                    {
                        if (int.Parse(buttons[2, i].Text) == int.Parse(buttons[0, i].Text))
                        {
                            temp = int.Parse(buttons[0, i].Text) + int.Parse(buttons[2, i].Text);
                            CreateButtons(3, i, temp);
                            DeleteButtons(2, i);
                            DeleteButtons(0, i);
                        }
                        else
                        {
                            temp = int.Parse(buttons[2, i].Text);
                            CreateButtons(3, i, temp);
                            buttons[2, i].Text = int.Parse(buttons[0, i].Text).ToString();
                            DeleteButtons(0, i);
                        }
                    }
                    else
                    {
                        temp = int.Parse(buttons[2, i].Text);
                        CreateButtons(3, i, temp);
                        DeleteButtons(2, i);
                    }
                }
                else
                {
                    if (buttons[1, i] != null && buttons[0, i] != null)
                    {
                        if (int.Parse(buttons[0, i].Text) == int.Parse(buttons[1, i].Text))
                        {
                            temp = int.Parse(buttons[0, i].Text) + int.Parse(buttons[1, i].Text);
                            CreateButtons(3, i, temp);
                            DeleteButtons(1, i);
                            DeleteButtons(0, i);
                        }
                        else
                        {
                            temp = int.Parse(buttons[1, i].Text);
                            CreateButtons(3, i, temp);
                            temp = int.Parse(buttons[0, i].Text);
                            CreateButtons(2, i, temp);
                            DeleteButtons(1, i);
                            DeleteButtons(0, i);
                        }
                    }
                    else if (buttons[1, i] == null && buttons[0, i] != null)
                    {
                        temp = int.Parse(buttons[0, i].Text);
                        CreateButtons(3, i, temp);
                        DeleteButtons(0, i);
                    }
                    else if (buttons[1, i] != null && buttons[0, i] == null)
                    {
                        temp = int.Parse(buttons[1, i].Text);
                        CreateButtons(3, i, temp);
                        DeleteButtons(1, i);
                    }
                    else
                    { }
                }

            }

        }
        void Up()
        {
            for(int i=0;i<4;i++)
            {
                int temp;
                if (buttons[0, i] != null && buttons[1, i] != null)
                {
                    if (int.Parse(buttons[0, i].Text) == int.Parse(buttons[1, i].Text))
                    {
                        buttons[0, i].Text = (int.Parse(buttons[0, i].Text) + int.Parse(buttons[1, i].Text)).ToString();
                        DeleteButtons(1, i);
                        if (buttons[2, i] != null && buttons[3, i] != null)
                        {
                            if (int.Parse(buttons[2, i].Text) == int.Parse(buttons[3, i].Text))
                            {
                                temp = int.Parse(buttons[3, i].Text) + int.Parse(buttons[2, i].Text);
                                CreateButtons(1, i, temp);
                                DeleteButtons(2, i);
                                DeleteButtons(3, i);
                            }
                            else
                            {
                                temp = int.Parse(buttons[2, i].Text);
                                CreateButtons(1, i, temp);
                                DeleteButtons(2, i);
                                temp = int.Parse(buttons[3, i].Text);
                                CreateButtons(2, i, temp);
                                DeleteButtons(3, i);
                            }
                        }
                        else if (buttons[2, i] != null && buttons[3, i] == null)
                        {
                            temp = int.Parse(buttons[2, i].Text);
                            CreateButtons(1, i, temp);
                            DeleteButtons(2, i);
                        }
                        else if (buttons[2, i] == null && buttons[3, i] != null)
                        {
                            temp = int.Parse(buttons[3, i].Text);
                            CreateButtons(1, i, temp);
                            DeleteButtons(3, i);
                        }
                        else
                        { }
                    }
                    else
                    {
                        if (buttons[2, i] != null && buttons[3, i] != null)
                        {
                            if (int.Parse(buttons[2, i].Text) == int.Parse(buttons[1, i].Text))
                            {
                                temp = int.Parse(buttons[2, i].Text) + int.Parse(buttons[1, i].Text);
                                buttons[1, i].Text = temp.ToString();
                                buttons[2, i].Text = int.Parse(buttons[3, i].Text).ToString();
                                DeleteButtons(3, i);
                            }
                            else
                            {
                                if (int.Parse(buttons[2, i].Text) == int.Parse(buttons[3, i].Text))
                                {
                                    buttons[2, i].Text = (int.Parse(buttons[2, i].Text) + int.Parse(buttons[3, i].Text)).ToString();
                                    DeleteButtons(3, i);
                                }
                            }
                        }
                        else if (buttons[2, i] != null && buttons[3, i] == null)
                        {
                            if (int.Parse(buttons[2, i].Text) == int.Parse(buttons[1, i].Text))
                            {
                                temp = int.Parse(buttons[2, i].Text) + int.Parse(buttons[1, i].Text);
                                buttons[1, i].Text = temp.ToString();
                                DeleteButtons(2, i);
                            }

                        }
                        else if (buttons[2, i] == null && buttons[3, i] != null)
                        {
                            if (int.Parse(buttons[3, i].Text) == int.Parse(buttons[1, i].Text))
                            {
                                buttons[1, i].Text = (int.Parse(buttons[1, i].Text) + int.Parse(buttons[3, i].Text)).ToString();
                                DeleteButtons(3, i);
                            }
                            else
                            {
                                temp = int.Parse(buttons[3, i].Text);
                                CreateButtons(2, i, temp);
                                DeleteButtons(3, i);
                            }
                        }
                        else
                        { }
                    }
                }
                else if (buttons[0, i] != null && buttons[1, i] == null)
                {
                    if (buttons[2, i] != null && buttons[3, i] != null)
                    {
                        if (int.Parse(buttons[2, i].Text) == int.Parse(buttons[0, i].Text))
                        {
                            buttons[0, i].Text = (int.Parse(buttons[2, i].Text) + int.Parse(buttons[2, i].Text)).ToString();
                            DeleteButtons(2, i);
                            temp = int.Parse(buttons[3, i].Text);
                            CreateButtons(1, i, temp);
                            DeleteButtons(3, i);
                        }
                        else
                        {
                            if (int.Parse(buttons[2, i].Text) == int.Parse(buttons[3, i].Text))
                            {
                                temp = int.Parse(buttons[2, i].Text) + int.Parse(buttons[2, i].Text);
                                CreateButtons(1, i, temp);
                                DeleteButtons(2, i);
                                DeleteButtons(3, i);
                            }
                            else
                            {
                                temp = int.Parse(buttons[2, i].Text);
                                CreateButtons(1, i, temp);
                                buttons[2, i].Text = int.Parse(buttons[3, i].Text).ToString();
                                DeleteButtons(3, i);
                            }
                        }
                    }
                    else if (buttons[2, i] != null && buttons[3, i] == null)
                    {
                        if (int.Parse(buttons[2, i].Text) == int.Parse(buttons[0, i].Text))
                        {
                            buttons[0, i].Text = (int.Parse(buttons[2, i].Text) + int.Parse(buttons[0, i].Text)).ToString();
                            DeleteButtons(2, i);
                        }
                        else
                        {
                            temp = int.Parse(buttons[2, i].Text);
                            CreateButtons(1, i, temp);
                            DeleteButtons(2, i);
                        }
                    }
                    else if (buttons[2, i] == null && buttons[3, i] != null)
                    {
                        if (int.Parse(buttons[0, i].Text) == int.Parse(buttons[3, i].Text))
                        {
                            buttons[0, i].Text = (int.Parse(buttons[3, i].Text) + int.Parse(buttons[0, i].Text)).ToString();
                            DeleteButtons(3, i);
                        }
                        else
                        {
                            temp = int.Parse(buttons[3, i].Text);
                            CreateButtons(1, i, temp);
                            DeleteButtons(3, i);
                        }
                    }
                    else
                    { }
                }
                else if (buttons[0, i] == null && buttons[1, i] != null)
                {
                    if (buttons[2, i] != null && buttons[3, i] != null)
                    {
                        if (int.Parse(buttons[1, i].Text) == int.Parse(buttons[2, i].Text))
                        {
                            temp = int.Parse(buttons[1, i].Text) + int.Parse(buttons[2, i].Text);
                            CreateButtons(0, i, temp);
                            buttons[1, i].Text = int.Parse(buttons[3, i].Text).ToString();
                            DeleteButtons(2, i);
                            DeleteButtons(3, i);
                        }
                        else
                        {
                            if (int.Parse(buttons[3, i].Text) == int.Parse(buttons[2, i].Text))
                            {
                                temp = int.Parse(buttons[1, i].Text);
                                CreateButtons(0, i, temp);
                                buttons[1, i].Text = (int.Parse(buttons[3, i].Text) + int.Parse(buttons[2, i].Text)).ToString();
                                DeleteButtons(2, i);
                                DeleteButtons(3, i);
                            }
                            else
                            {
                                temp = int.Parse(buttons[1, i].Text);
                                CreateButtons(0, i, temp);
                                buttons[1, i].Text = int.Parse(buttons[2, i].Text).ToString();
                                buttons[2, i].Text = int.Parse(buttons[3, i].Text).ToString();
                                DeleteButtons(3, i);
                            }
                        }
                    }
                    else if (buttons[2, i] != null && buttons[3, i] == null)
                    {
                        if (int.Parse(buttons[1, i].Text) == int.Parse(buttons[2, i].Text))
                        {
                            temp = int.Parse(buttons[1, i].Text) + int.Parse(buttons[2, i].Text);
                            CreateButtons(0, i, temp);
                            DeleteButtons(1, i);
                            DeleteButtons(2, i);
                        }
                        else
                        {
                            temp = int.Parse(buttons[1, i].Text);
                            CreateButtons(0, i, temp);
                            buttons[1, i].Text = int.Parse(buttons[2, i].Text).ToString();
                            DeleteButtons(2, i);
                        }
                    }
                    else if (buttons[2, i] == null && buttons[3, i] != null)
                    {
                        if (int.Parse(buttons[1, i].Text) == int.Parse(buttons[3, i].Text))
                        {
                            temp = int.Parse(buttons[3, i].Text) + int.Parse(buttons[1, i].Text);
                            CreateButtons(0, i, temp);
                            DeleteButtons(1, i);
                            DeleteButtons(3, i);
                        }
                        else
                        {
                            temp = int.Parse(buttons[1, i].Text);
                            CreateButtons(0, i, temp);
                            buttons[1, i].Text = int.Parse(buttons[3, i].Text).ToString();
                            DeleteButtons(3, i);
                        }
                    }
                    else
                    {
                        temp = int.Parse(buttons[1, i].Text);
                        CreateButtons(0, i, temp);
                        DeleteButtons(1, i);
                    }
                }
                else
                {
                    if (buttons[2, i] != null && buttons[3, i] != null)
                    {
                        if (int.Parse(buttons[3, i].Text) == int.Parse(buttons[2, i].Text))
                        {
                            temp = int.Parse(buttons[3, i].Text) + int.Parse(buttons[2, i].Text);
                            CreateButtons(0, i, temp);
                            DeleteButtons(2, i);
                            DeleteButtons(3, i);
                        }
                        else
                        {
                            temp = int.Parse(buttons[2, i].Text);
                            CreateButtons(0, i, temp);
                            temp = int.Parse(buttons[3, i].Text);
                            CreateButtons(1, i, temp);
                            DeleteButtons(2, i);
                            DeleteButtons(3, i);
                        }
                    }
                    else if (buttons[2, i] == null && buttons[3, i] != null)
                    {
                        temp = int.Parse(buttons[3, i].Text);
                        CreateButtons(0, i, temp);
                        DeleteButtons(3, i);
                    }
                    else if (buttons[2, i] != null && buttons[3, i] == null)
                    {
                        temp = int.Parse(buttons[2, i].Text);
                        CreateButtons(0, i, temp);
                        DeleteButtons(2, i);
                    }
                    else
                    { }
                }

            }
        }

        private void Label2_Click(object sender, EventArgs e)
        {
           
        }
    }
}
