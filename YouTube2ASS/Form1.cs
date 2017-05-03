using Newtonsoft.Json;
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
using System.Web.Script.Serialization;
using System.Windows.Forms;


namespace YouTube2ASS
{
    public partial class Form1 : Form
    {
        string str_path = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\";
        string str_fileInput = "";
        public class json_setting
        {
            public int int_x { get; set; }
            public int int_y { get; set; }
            public int int_height { get; set; }
            public string str_style { get; set; }
            public bool bool_script { get; set; }
        }

        void fn_loadSetting()
        {
            try
            {
                var strlist_json = File.ReadAllLines(str_path + "setting.json");
                List<string> list_strlist_json = new List<string>(strlist_json);

                //cut off [] in json file
                string str_json = "";
                foreach (var strlist_json_item in strlist_json)
                {
                    str_json += strlist_json_item;
                }
                json_setting _json_setting = JsonConvert.DeserializeObject<json_setting>(str_json.Substring(1, str_json.Length - 2));

                textBox_height.Text = _json_setting.int_height.ToString();
                textBox_x.Text = _json_setting.int_x.ToString();
                textBox_y.Text = _json_setting.int_y.ToString();
                textBox_style.Text = _json_setting.str_style.ToString();
                checkBox_writeScript.Checked = Convert.ToBoolean(_json_setting.bool_script);
            }
            catch
            {

            }
        }

        void fn_saveSetting(int input_x, int input_y, int input_height, string input_style, bool input_script)
        {
            List<json_setting> _json_setting = new List<json_setting>();
            _json_setting.Add(new json_setting()
            {
                int_x = input_x,
                int_y = input_y,
                int_height = input_height,
                str_style = input_style,
                bool_script = input_script
            });
            string json = JsonConvert.SerializeObject(_json_setting.ToArray(), Formatting.Indented);
            System.IO.File.WriteAllText(str_path + "setting.json", json);
        }

        void fn_writeScript()
        {
            if (str_fileInput != "")
            {
                List<string> list_avs = new List<string>();
                try
                {
                    string input = str_fileInput;
                    string pattern = @"(.*)(\d{4})(\d{2})(\d{2})(.*)(-[-0-9A-Za-z_]{11})(\.[A-Za-z]*)(\.vtt)";
                    //                   |     |     |       |    |  |        |         |   |        |
                    //                  path  num*4 num*2 num*2  any - youtube_vid_ID11 dot lang   .vtt
                    //                    1    2     3      4     5   (       6        )        7    8
                    //keep path, and delete yotuube_vid_id and extname
                    string replacement = @"$2-$3-$4$5.youtube";
                    //add dash between yyyy mm dd, and add ".youtube" extnamef
                    string str_fileOutput = Regex.Replace(input, pattern, replacement);
                    //20xx-xx-xx_xxxxx.youtube

                    replacement = @"$2$3$4$5$6.mp4";
                    string str_video = Regex.Replace(input, pattern, replacement);
                    //original YouTube video

                    replacement = @"$1";
                    string str_fileOutputPath = Regex.Replace(input, pattern, replacement);
                    //path

                    var strlist_header = File.ReadAllLines(str_path + "header_avs.txt",Encoding.GetEncoding(936));
                    List<string> list_str_header = new List<string>(strlist_header);
                    foreach (var str_header in list_str_header)
                    {
                        list_avs.Add(str_header.ToString().Replace("[video_filename]", str_video).Replace("[subtitle_filename]", str_fileOutput + ".ass"));
                    }
                    File.WriteAllLines(str_fileOutputPath + str_fileOutput + ".avs", list_avs, Encoding.GetEncoding(936));

                    string str_nvencc = "NVEncC.exe --avs -i \"" + str_fileOutput + ".avs\" --vbr 3500 -o \"" + str_fileOutput + ".264\"";
                    File.WriteAllText(str_fileOutputPath + "doit.cmd", str_nvencc);
                }
                catch (Exception e)
                {
                    this.Text = e.ToString();
                }
            }
        }


        public Form1()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);
            dynamic json_setting = JsonConvert.DeserializeObject("{'int_x':196,'int_y':1024,'int_height':54,'str_style':'Style: YouTubeAuto,Arial,20,&H00FFFFFF,&HFF0000FF,&HC0000000,&H00000000,0,0,0,0,100,100,0,0,3,0.07,0,1,10,10,10,1'}");
            fn_loadSetting();
            try
            {
                fn_loadSetting();
            }
            catch
            {

            }
        }

        void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
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
            str_fileInput = files[0];
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
            string str_result = int_hour.ToString() + ":" + int_min.ToString("D2") + ":" + int_sec.ToString("D2") + "." + (int_ms/10).ToString("D2");
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
            List<string> list_str_result = new List<string>();

            try
            {
                var strlist_header = File.ReadAllLines(str_path + "header_ass.txt");
                List<string> list_str_header = new List<string>(strlist_header);
                foreach (var str_header in list_str_header)
                {
                    list_str_result.Add(str_header.ToString());
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                list_str_result.Add(textBox_style.Text);
                list_str_result.Add("");
                list_str_result.Add("[Events]");
                list_str_result.Add("Format: Layer, Start, End, Style, Name, MarginL, MarginR, MarginV, Effect, Text");
            }

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
                        + fn_msTOString(int_Current1End) + ",YouTubeAuto,,0,0,0,,";
                    string str_CurrentLine2 = "Dialogue: 0,"
                        + fn_msTOString(int_Current2Start) + ","
                        + fn_msTOString(int_Current2End) + ",YouTubeAuto,,0,0,0,,";
                    string str_CurrentLine3 = "Dialogue: 0,"
                        + fn_msTOString(int_Current3Start) + ","
                        + fn_msTOString(int_Current3End) + ",YouTubeAuto,,0,0,0,,";

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
                    list_str_result.Add(str_CurrentLine1);
                    list_str_result.Add(str_CurrentLine2);
                    list_str_result.Add(str_CurrentLine3);
                }
            }
            if (str_fileInput != "")
            {
                string input = str_fileInput;
                string pattern = @"^(.*)(\d{4})(\d{2})(\d{2})(.*)(-[-0-9A-Za-z_]{11}\.[A-Za-z]*\.vtt)";
                //                 |  |     |     |       |    |  |        |         |   |        |
                //             start path  num*4 num*2 num*2  any - youtube_vid_ID11 dot lang   .vtt
                //                     1    2     3      4     5   (              6                ) 
                //keep path, and delete yotuube_vid_id and extname
                string replacement = @"$1$2-$3-$4$5.youtube.ass";
                //add dash between yyyy mm dd, and add ".youtube.ass" extnamef
                string str_fileOutput = Regex.Replace(input, pattern, replacement);

                File.WriteAllLines(str_fileOutput, list_str_result);
            }

            //write script
            if (checkBox_writeScript.Checked == true)
                fn_writeScript();

        }

        private void button_saveSetting_Click(object sender, EventArgs e)
        {
            fn_saveSetting(Convert.ToInt32(textBox_x.Text), Convert.ToInt32(textBox_y.Text), Convert.ToInt32(textBox_height.Text), textBox_style.Text, checkBox_writeScript.Checked);
        }
    }
}
