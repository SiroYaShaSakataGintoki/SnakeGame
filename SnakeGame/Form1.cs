using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    /// <summary>
    /// timer1:用于调用贪吃蛇进行移动功能函数的定时器(负责移动的定时器间隔必须大于等于其他定时器功能，否则会出现判定延迟或无效)
    /// timer2:监测贪吃蛇生命状态的定时器，若死亡则进行结算功能
    /// timer3:若食物被吃则进行食物生成的定时器
    /// timer4:判断食物是否被吃用的定时器
    /// </summary>
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            //设置关卡为第一关
            comboBox1.Text = "第一关";
            Start();
        }      
        //创建名称为snake的链表表示贪吃蛇对象
         List<Label> Snake = new List<Label>();
        //定义贪吃蛇的初始长度
         private int SnakeLength =5;
        //定义初始移动方向
        string Direction = "D";
        //定义玩家成绩变量
        public int Grade=0;
        //定义弹出窗口返回值
        DialogResult dr;
        //贪吃蛇生命状态
        int life=0;
        //定义食物控件
        Label food = new Label();
        //定义食物坐标
        int food_x;
        int food_y;
        //定义食物数量
        int food_count = 0;
        //在picturebox实现贪吃蛇并定义贪吃蛇在picturebox中的初始位置
        private List<Label> Start()
        {
            //循环创建label组成贪吃蛇的身体
            for (int i = 0; i < SnakeLength; i++)
            {
                Label body = new Label();
                body.BackColor = Color.Red;
                body.AutoSize = false;
                body.Size = new Size(5, 5);
                body.Text = "";
                Snake.Add(body);
            }
            //贪吃蛇头部颜色改变区分
            Snake[0].BackColor = Color.Yellow;
            //贪吃蛇身体计数器
            int BodyCount = 0;
            //贪吃蛇头部初始坐标
            int x = 200;
            int y = 200;
            //foreach设置贪吃蛇在picturebox中的位置
            foreach (Label bodys in Snake)
            {
                Snake[BodyCount].Location = new Point(x, y);
                pictureBox1.Controls.Add(Snake[BodyCount]);
                x = x - 5;
                BodyCount++;
            }
            return Snake;
        }
        //定时器来重复调用贪吃蛇的移动函数使其保持特定频率移动
        private void timer1_Tick(object sender, EventArgs e)
        {
            SnakeMove();

        }
        //使贪吃蛇具有永久移动功能
        private void SnakeMove()
        {
            int BodyCount = Snake.ToArray().Length;
            switch (Direction)
            {
                case "W":
                    {
                        for (int i = BodyCount - 1; i > 0; i--)
                        {
                            Snake[i].Left = Snake[i - 1].Left;
                            Snake[i].Top = Snake[i - 1].Top;
                            Snake[i - 1].Top -= 5;
                        }

                    }
                    break;
                case "A":
                    {

                        for (int i = BodyCount - 1; i > 0; i--)
                        {

                            Snake[i].Left = Snake[i - 1].Left;
                            Snake[i].Top = Snake[i - 1].Top;
                            Snake[i - 1].Left -= 5;

                        }

                    }
                    break;
                case "S":
                    {
                        for (int i = BodyCount - 1; i > 0; i--)
                        {
                            Snake[i].Left = Snake[i - 1].Left;
                            Snake[i].Top = Snake[i - 1].Top;
                            Snake[i - 1].Top += 5;
                        }

                    }
                    break;
                case "D":
                    {

                        for (int i =BodyCount-1;i>0;i--)
                        {

                            Snake[i].Left = Snake[i - 1].Left;
                            Snake[i].Top = Snake[i - 1].Top;
                            Snake[i - 1].Left +=5;

                        }

                    }
                    break;

            }
        }
        //启动贪吃蛇移动功能与判定死亡功能<移动与判定死亡功能同步>
        private void button1_Click(object sender, EventArgs e)
        {
            //贪吃蛇移动开始
            timer1.Enabled =true;
            //贪吃蛇死亡判定监视开始
            timer2.Enabled = true;
            //食物生成开始
            timer3.Enabled = true;
            //食物状态监测器开始监测
            timer4.Enabled = true;
            //游戏难度调整监测器开始监测
            timer5.Enabled = true;
        }
        //停止贪吃蛇移动功能与判定死亡功能<移动与判定死亡功能同步>
        private void button2_Click(object sender, EventArgs e)
        {
            //贪吃蛇移动停止
            timer1.Enabled = false;
            //贪吃蛇死亡判定停止
            timer2.Enabled = false;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
            //获取用户按键并转化为字母
            //string key = Convert.ToString(e.KeyCode);
            switch (e.KeyCode.ToString())
            {
                case "W":
                    if (Direction != "S")
                        Direction = "W";
                    
                    break;
                case "A":
                    if (Direction != "D")
                        Direction = "A";
                  
                    break;
                case "S":
                    if (Direction != "W")
                        Direction = "S";
                   
                    break;
                case "D":
                    if (Direction != "A")
                        Direction = "D";
                    
                    break;
                 //若用户按下其他键则无效
                default:
                    break;

            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            SnakeDie();
        }
        //判定贪吃蛇是否死亡，若死亡则退出游戏并告知玩家游戏分数
        private void SnakeDie()
        {
            //贪吃蛇身体长度
            int BodyCount = Snake.ToArray().Length;
            //for循环判定贪吃蛇头部是否与其他身体坐标相同，若相同则为自残死亡
            for (int i = 1; i < BodyCount; i++)
            {

                if (Snake[i].Location == Snake[0].Location || Snake[0].Top ==(-5) || Snake[0].Left ==(-5) || Snake[0].Left==365|| Snake[0].Top == 390)
                {
                    //游戏结束贪吃蛇停止运动
                    timer1.Enabled = false;
                    timer2.Enabled = false;
                    life =1;
                    //跳出提示框通知玩家游戏结束
                    dr=MessageBox.Show("本次游戏得分:"+Grade, "游戏结束");
                    //游戏结束跳出循环
                    break;                    
                }
            }
            //判断是否死亡，若死亡则执行结束游戏功能，否则不进行任何操作
            if(life==1)
            {
                GameOver();
            }

        }
        //用户游戏结束后点击确认按钮出发事件
        public void GameOver()
        {
            int BodyCount = Snake.ToArray().Length;
            if (dr==DialogResult.OK)
            {
                //循环清空list里组成贪吃蛇的全部控件，余留下其位置删其内容
                for (int i = BodyCount - 1; i >= 0; i--)
                {
                    Snake[i].Dispose();
                }
                //移除整个list使其余留空位也被清除
                Snake.Clear();
                //删除完毕返回开始界面
                Snake = Start();
                //重新开始则生命为0表示重生
                life = 0;
                //重置移动方向为默认移动方向
                Direction = "D";
                //重置分数为0
                Grade = 0;

            }
        }
        //食物随机出现函数
        public void Food()
        {
            //设置食物的坐标为随机数同时不能出现在贪吃蛇的身体上以及游戏区外
            Random ran = new Random();
            L1://必须为5的倍数因为蛇头为5*5若不满足5的倍数则会永远无法坐标相同
             food_x= (ran.Next(0, 360)/5)*5;
             food_y = (ran.Next(0,385)/5)*5;
            //判断该坐标是否与贪吃蛇身体坐标相同
            int BodyCount = Snake.ToArray().Length;
            for(int i=0;i<BodyCount;i++)
            {
                if (Snake[i].Location == new Point(food_x, food_y))
                {
                    //若食物坐标与贪吃蛇坐标某部分身体坐标相同则跳转到给坐标赋值一步重新赋值
                    goto L1;
                }
                
            }
            //获得可行坐标picturebox添加食物
            food.AutoSize = false;
            food.Size = new Size(5, 5);
            food.Text = "";
            food.BackColor = Color.Orange;
            food.BackColor = Color.Orange;
            food.BackColor = Color.Orange;
            food.Location = new Point(food_x, food_y);
            pictureBox1.Controls.Add(food);
            //食物数量+1
            food_count++;
        }
        //贪吃蛇吃食物事件
        public void SnakeEat()
        {
            if (food.Location.ToString() == Snake[0].Location.ToString())
            {
                //食物与蛇头坐标相同则使食物数置为一食物生成监测食物数为一则将自动转移原食物位置
                food_count--;
                //分数+10
                Grade = Grade + 10;
                //贪吃蛇体长+1
                int SnakeLength = Snake.ToArray().Length;
                Label body = new Label();
                body.BackColor = Color.Red;
                body.AutoSize = false;
                body.Size = new Size(5, 5);
                body.Text = "";
                Snake.Add(body);
                body.Location = Snake[SnakeLength - 1].Location;
                pictureBox1.Controls.Add(Snake[SnakeLength]);
            }
        }
        //食物是否被吃检测装置
        private void timer4_Tick(object sender, EventArgs e)
        {
            SnakeEat();
        }
        //食物生成监测器
        private void timer3_Tick(object sender, EventArgs e)
        {
            if (food_count == 0)
            {
                Food();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer2.Enabled = false;
            life = 1;
            //跳出提示框通知玩家游戏结束
            dr = MessageBox.Show("本次游戏得分:" + Grade, "游戏结束");
            GameOver();
        }
        //用于调整游戏难度的函数：贪吃蛇的游戏关卡数越大，贪吃蛇移动速度越快，游戏难度越高
        private void GameDifficulty()
        {
            //获取游戏关卡数
            string str = comboBox1.Text;
            //根据游戏关卡数进行对应的游戏难度调整
            switch (str)
            {
                case "第一关":
                    timer1.Interval = 100;                 
                    break;
                case "第二关":
                    timer1.Interval = 10;                   
                    break;
                default:
                    break;
            }
        }
        //监测调整游戏难度
        private void timer5_Tick(object sender, EventArgs e)
        {
            GameDifficulty();
        }
        //退出游戏button
        private void button4_Click(object sender, EventArgs e)
        {
            //游戏中弹跳出窗口则先暂停游戏
            timer1.Enabled = false;
            if (MessageBox.Show("真的要退出游戏吗？", "退出游戏", MessageBoxButtons.OKCancel) == DialogResult.OK)

            {

                Dispose();

                Application.Exit();

            }
            else
            {
                //用户选择不退出游戏则继续游戏
                timer1.Enabled = true;
            }
        }
    }
}
