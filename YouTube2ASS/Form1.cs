using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTube2ASS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);
        }

        void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        void Form1_DragDrop(object sender, DragEventArgs e)
        {
            listBox1.Items.Clear();
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                using (StreamReader stRead = new StreamReader(file))
                {
                    while (!stRead.EndOfStream)
                    {
                        listBox1.Items.Add(stRead.ReadLine());
                    }
                }
            }                
        }

        private int fn_stringToMs(string str_input)
        {
            str_input = str_input.Replace("<", "");
            str_input = str_input.Replace(">", "");
            int int_result = 0;
            string[] str_time = str_input.Split(':');
            string[] str_second = str_time[2].Split('.');
            this.Text = str_time[0];
            int_result = Convert.ToInt32(str_second[1]) + 1000 * (Convert.ToInt32(str_time[0]) * 3600 + Convert.ToInt32(str_time[1]) * 60 + Convert.ToInt32(str_second[0]));
            return int_result;
        }

        private string fn_msTOString(int int_input)
        {
            int int_hour = int_input / 1000 / 3600;
            int int_min = (int_input - int_hour * 1000 * 3600) / 1000 / 60;
            int int_sec = (int_input - int_hour * 1000 * 3600 - int_min * 1000 * 60) / 1000;
            int int_ms = int_input - int_hour * 1000 * 3600 - int_min * 1000 * 60 - int_sec * 1000;
            //0:00:17.01
            string str_result = int_hour.ToString() + ":" + int_min.ToString("D2") + ":" + int_sec.ToString("D2") + "." + int_ms.ToString("D3");
            return str_result;
        }

        private int[] fn_getStartAndEndTime(string str_input)
        {
            int[] int_result = new int[2];

            string str_currentline = str_input.ToString();
            string[] str_time_temp1 = str_currentline.Split(new string[] { " --> " }, StringSplitOptions.None);
            string str_timeStart = str_time_temp1[0];
            string[] str_time_temp2 = str_time_temp1[1].Split(' ');
            string str_timeEnd = str_time_temp2[0];
            
            int_result[0] = fn_stringToMs(str_timeStart);
            int_result[1] = fn_stringToMs(str_timeEnd);

            return int_result;
        }

        private void button_doit_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            for (int i=0;i<listBox1.Items.Count;i++)
            {
                int int_CurrentStart = 0;
                int int_CurrentEnd = 0;
                int int_NextStart = 0;

                
                if (listBox1.Items[i].ToString().Contains("align:start position:"))
                {
                    //Timeline process
                    int[] int_time = new int[2];
                    int[] int_timeNext = new int[2];

                    int_time = fn_getStartAndEndTime(listBox1.Items[i].ToString());
                    int_CurrentStart = int_time[0];
                    int_NextStart = int_time[1];

                    if (listBox1.Items.Count - (i + 3) >0)
                    {
                        int_timeNext = fn_getStartAndEndTime(listBox1.Items[i + 3].ToString());
                        int_CurrentEnd = int_timeNext[0] - 1;
                    }
                    else
                    {
                        int_CurrentEnd = int_time[1];
                    }
                    int int_Current1Start = int_CurrentStart;
                    int int_Current1End = int_CurrentEnd;
                    int int_Current2Start = int_Current1End;
                    int int_Current2End = int_NextStart - 1;
                    int int_Current3Start = int_Current2End;
                    int int_Current3End = int_Current3Start + 500;
                    //int int_PosX = 417;
                    //int int_PosY = 750;
                    //int int_LineHeight = 40;
                    int int_PosX = Convert.ToInt32(textBox_x.Text);
                    int int_PosY = Convert.ToInt32(textBox_y.Text);
                    int int_LineHeight = Convert.ToInt32(textBox_height.Text);
                    int int_Duration = 370;

                    
                    //Text process
                    string str_line = listBox1.Items[i + 1].ToString();
                    
                    string pattern = @"<c>";
                    string replacement = @"{\c&HE5E5E5&}";
                    string input = str_line;
                    str_line = Regex.Replace(input, pattern, replacement);

                    pattern = @"<.c>";
                    replacement = @"{\c}";                    
                    input = str_line;
                    str_line = Regex.Replace(input, pattern, replacement);

                    //RGB -> BGR
                    Regex rx = new Regex(@"<c\.color.{6}>");
                    foreach (Match match in rx.Matches(str_line))
                    {
                        string str_matchedString = match.Value.ToString();
                        string str_matchedStringReplace = @"{\c&H" + str_matchedString.Substring(12, 2) + str_matchedString.Substring(10, 2) + str_matchedString.Substring(8, 2) + "&}";
                        str_line = str_line.Replace(str_matchedString, str_matchedStringReplace);
                    }

                    //Karaoke process
                    string str_CurrentLine1 = "Dialogue: 0,"
                        + fn_msTOString(int_Current1Start) + ","
                        + fn_msTOString(int_Current1End) + ",Default,,0,0,0,,";
                    string str_CurrentLine2 = "Dialogue: 0,"
                        + fn_msTOString(int_Current2Start) + ","
                        + fn_msTOString(int_Current2End) + ",Default,,0,0,0,,";
                    string str_CurrentLine3 = "Dialogue: 0,"
                        + fn_msTOString(int_Current3Start) + ","
                        + fn_msTOString(int_Current3End) + ",Default,,0,0,0,,";

                    bool bool_IsFirstWord = true;
                    rx = new Regex(@"<\d{2}:\d{2}:\d{2}\.\d{3}>");

                    int int_lastTime = 0;
                    string str_lastMatch = "";
                    foreach (Match match in rx.Matches(str_line))
                    {
                        
                        if (bool_IsFirstWord)
                        {
                            int_lastTime = int_CurrentStart;
                            str_lastMatch = match.Value.ToString();
                            int int_nowTime = fn_stringToMs(str_lastMatch);
                            float float_time = (int_nowTime - int_lastTime) / 10;
                            string str_time = Math.Round(float_time, 1).ToString();
                            //str_CurrentLine1
                            str_CurrentLine1 += @"{\fs53.7\move("
                            + int_PosX.ToString() + "," + int_PosY.ToString() + ","
                            + int_PosX.ToString() + "," + (int_PosY - int_LineHeight).ToString() + ","
                            + "0," + int_Duration.ToString() + ")}"
                            + @"{\k" + str_time + "}" + str_line.Replace(str_lastMatch, "");

                            //str_CurrentLine2
                            str_CurrentLine2 += @"{\fs53.7\move("
                            + int_PosX.ToString() + "," + (int_PosY - int_LineHeight).ToString() + ","
                            + int_PosX.ToString() + "," + (int_PosY - int_LineHeight * 2).ToString() + ","
                            + "0," + int_Duration.ToString() + ")}"
                            + str_line.Replace(str_lastMatch, "");

                            //str_CurrentLine3
                            str_CurrentLine3 += @"{\fs53.7\move("
                            + int_PosX.ToString() + "," + (int_PosY - int_LineHeight * 2).ToString() + ","
                            + int_PosX.ToString() + "," + (int_PosY - int_LineHeight * 3).ToString() + ","
                            + "0," + int_Duration.ToString() + ")"
                            + @"\clip(0," + (int_PosY - int_LineHeight * 3 - 15).ToString() + ",1920,1080)}"
                            + str_line.Replace(str_lastMatch, "");

                            bool_IsFirstWord = false;
                            int_lastTime = int_nowTime;
                        }
                        else
                        {
                            str_lastMatch = match.Value.ToString();
                            int int_nowTime = fn_stringToMs(str_lastMatch);
                            float float_time = (int_nowTime - int_lastTime) / 10;
                            string str_time = Math.Round(float_time, 1).ToString();
                            str_CurrentLine1 = str_CurrentLine1.Replace(str_lastMatch, @"{\k" + str_time + "}");
                            str_CurrentLine2 = str_CurrentLine2.Replace(str_lastMatch, "");
                            str_CurrentLine3 = str_CurrentLine3.Replace(str_lastMatch, "");

                            int_lastTime = int_nowTime;
                        }
                    }
                    richTextBox1.Text += str_CurrentLine1 + "\r\n" + str_CurrentLine2 + "\r\n" + str_CurrentLine3 + "\r\n";
                }
            }            
        }
    }
}
