using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WeekReportForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 各テキストボックスの値を取得
            string text1 = textBox1.Text;
            string text2 = textBox2.Text;
            string text3 = textBox3.Text;
            string text4 = textBox4.Text;
            string text5 = textBox5.Text;
            string next = textBox6.Text;

            string filepath = "";
            DateTime Today = dateTimePicker1.Value;
            string day = Today.Month.ToString() + "/" + Today.Day.ToString();

            FolderBrowserDialog fbDialog = new FolderBrowserDialog();

            // ダイアログの説明文を指定する
            fbDialog.Description = "週報保存先フォルダを選択";

            // デフォルトのフォルダを指定する
            fbDialog.SelectedPath = @"C:";

            // 「新しいフォルダーの作成する」ボタンを表示する
            fbDialog.ShowNewFolderButton = true;

            //フォルダを選択するダイアログを表示する
            if (fbDialog.ShowDialog() == DialogResult.OK)
            {
                filepath = fbDialog.SelectedPath + @"\";
            }
            else
            {
                return;
            }
            // ファイル名決定
            string filename = filepath + 
                Today.Year.ToString() + 
                CheckNum(Today.Month.ToString()) +
                CheckNum(Today.Day.ToString()) +
                "_週報.txt";

            List<int> dayList = WeekCheck(Today.DayOfWeek);

            if (dayList[0] == -1)
            {
                MessageBox.Show("休日に週報は作成できません。");
                return;
            }

            DateTime first = Today.AddDays(-dayList[0]);
            string firstday = first.Month.ToString() + "/" + first.Day.ToString();

            DateTime second = Today.AddDays(-dayList[1]);
            string secondday = second.Month.ToString() + "/" + second.Day.ToString();

            DateTime third = Today.AddDays(-dayList[2]);
            string thirdday = third.Month.ToString() + "/" + third.Day.ToString();

            DateTime fourth = Today.AddDays(-dayList[3]);
            string fourthday = fourth.Month.ToString() + "/" + fourth.Day.ToString();


            // タイトル作成
            StringBuilder sb = new StringBuilder();
            sb.Append("【週報】 " + firstday + "～" + day + "\n");

            // 今週の報告
            sb.Append("■今週の報告\n");
            sb.Append(firstday + " " + text1 + "\n\n");
            sb.Append(secondday + " " + text2 + "\n\n");
            sb.Append(thirdday + " " + text3 + "\n\n");
            sb.Append(fourthday + " " + text4 + "\n\n");
            sb.Append(day + " " + text5 + "\n\n");
            sb.Append("■来週の取り組み\n");
            sb.Append(next);

            File.AppendAllText(filename,sb.ToString());
            MessageBox.Show("週報が作成されました。");

        }
        private List<int> WeekCheck(DayOfWeek dow)
        {
            List<int> retList = new List<int>();
            switch (dow)
            {
                case DayOfWeek.Monday:
                    retList.Add(6);
                    retList.Add(5);
                    retList.Add(4);
                    retList.Add(3);
                    break;
                case DayOfWeek.Tuesday:
                    retList.Add(6);
                    retList.Add(5);
                    retList.Add(4);
                    retList.Add(1);
                    break;
                case DayOfWeek.Wednesday:
                    retList.Add(6);
                    retList.Add(5);
                    retList.Add(2);
                    retList.Add(1);
                    break;
                case DayOfWeek.Thursday:
                    retList.Add(6);
                    retList.Add(3);
                    retList.Add(2);
                    retList.Add(1);
                    break;
                case DayOfWeek.Friday:
                    retList.Add(4);
                    retList.Add(3);
                    retList.Add(2);
                    retList.Add(1);
                    break;
                default:
                    retList.Add(-1);
                    break;
            }
            return retList;
        }

        private string CheckNum(string i)
        {
            string str = "";

            if (i.Length == 1)
            {
                str = "0" + i;
            }
            else
            {
                str = i;
            }
            return str;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.ResetText();
            textBox2.ResetText();
            textBox3.ResetText();
            textBox4.ResetText();
            textBox5.ResetText();
            textBox6.ResetText();
        }
    }
}
