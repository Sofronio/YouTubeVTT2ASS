using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Xml;

namespace YouTube2ASS
{
    public partial class Form1 : Form
    {
        string str_programPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\";
        string str_filePath = "";
        string str_fileName = "";
        string str_fileExt = "";
        string[] filesWithPath;
        List<string> strlist_import = new List<string>();
        List<subtitle> mySubtitle = new List<subtitle>();
        class subtitle
        {
            public string Content
            {
                get
                {
                    return content;
                }

                set
                {
                    content = value;
                }
            }

            public string WordStart
            {
                get
                {
                    return timeStart;
                }

                set
                {
                    timeStart = value;
                }
            }

            public string WordEnd
            {
                get
                {
                    return timeEnd;
                }

                set
                {
                    timeEnd = value;
                }
            }

            public int Position
            {
                get
                {
                    return position;
                }

                set
                {
                    position = value;
                }
            }

            public bool Selected
            {
                get
                {
                    return selected;
                }

                set
                {
                    selected = value;
                }
            }

            public string Color
            {
                get
                {
                    return color;
                }

                set
                {
                    color = value;
                }
            }

            public int LineNumber
            {
                get
                {
                    return lineNumber;
                }

                set
                {
                    lineNumber = value;
                }
            }

            public string NextStart
            {
                get
                {
                    return nextStart;
                }

                set
                {
                    nextStart = value;
                }
            }

            public string NextEnd
            {
                get
                {
                    return nextEnd;
                }

                set
                {
                    nextEnd = value;
                }
            }

            public string ColorNext
            {
                get
                {
                    return colorNext;
                }

                set
                {
                    colorNext = value;
                }
            }

            private string content;
            private string timeStart;
            private string timeEnd;
            private string nextStart;
            private string nextEnd;
            private int position;
            private bool selected;
            private string color;
            private string colorNext;
            private int lineNumber;
        }
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
                var strlist_json = File.ReadAllLines(str_programPath + "setting.json");
                List<string> list_strlist_json = new List<string>(strlist_json);

                //cut off [] in json file
                string str_json = "";
                foreach (var strlist_json_item in strlist_json)
                {
                    str_json += strlist_json_item;
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                json_setting _json_setting = js.Deserialize<json_setting>(str_json.Substring(1, str_json.Length - 2));

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
            JavaScriptSerializer js = new JavaScriptSerializer();
            string json = js.Serialize(_json_setting.ToArray());
            System.IO.File.WriteAllText(str_programPath + "setting.json", json);
        }

        void fn_writeScript()
        {
            if (str_fileName != "")
            {
                List<string> list_avs = new List<string>();
                try
                {
                    //usually video name is three chars less than vtt name, because auto vtt usually have ".en" to show it's a English subitle.
                    string str_videoName = Regex.Match(str_fileName, "(?<VideoNameWithOutExt>.*).[A-Za-z]{2}").Groups["VideoNameWithOutExt"].Value;


                    var strlist_header = File.ReadAllLines(str_programPath + "header_avs.txt", Encoding.GetEncoding(936));
                    for (int i = 0; i < strlist_header.Count(); i++)
                    {
                        strlist_header[i] = strlist_header[i].Replace("[video_filename]", str_videoName + ".mp4").Replace("[subtitle_filename]", str_fileName + ".youtube.ass");
                    }
                    File.WriteAllLines(str_filePath + str_fileName + ".youtube.avs", strlist_header, Encoding.GetEncoding(936));

                    string str_nvencc = "NVEncC.exe --avs -i \"" + str_fileName + ".youtube.avs\" --vbr 3500 -o \"" + str_fileName + ".264\"";
                    File.WriteAllText(str_filePath + str_fileName + ".doit.cmd", str_nvencc);
                }
                //catch (FileNotFoundException)
                //{
                    
                //}
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
            JavaScriptSerializer js = new JavaScriptSerializer();
            dynamic json_setting = js.DeserializeObject("{'int_x':196,'int_y':1024,'int_height':54,'str_style':'Style: YouTubeAuto,Arial,20,&H00FFFFFF,&HFF0000FF,&H46000000,&H00000000,0,0,0,0,100,100,0,0,3,0.07,0,1,10,10,10,1'}");
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
            filesWithPath = (string[])e.Data.GetData(DataFormats.FileDrop);

            using (StreamReader stRead = new StreamReader(filesWithPath[0]))
            {
                while (!stRead.EndOfStream)
                {
                    listBox1.Items.Add(stRead.ReadLine());
                }
            }

            str_fileName = Path.GetFileNameWithoutExtension(filesWithPath[0]);
            str_fileExt = Path.GetExtension(filesWithPath[0]);
            str_filePath = Path.GetDirectoryName(filesWithPath[0]) + @"\";
            button_doit.Text = "YouTubeTT loaded!";
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
            string str_result = int_hour.ToString() + ":" + int_min.ToString("D2") + ":" + int_sec.ToString("D2") + "." + (int_ms / 10).ToString("D2");
            return str_result;
        }

        private string msToTime(string ms)
        {
            TimeSpan t = TimeSpan.FromMilliseconds(Convert.ToInt64(ms));
            string result = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}",
                                    t.Hours,
                                    t.Minutes,
                                    t.Seconds,
                                    t.Milliseconds);
            return result;
        }

        private void processXML()
        {
            if (filesWithPath[0] != "")
            {
                mySubtitle.Clear();
                XmlDocument myXMLDoc = new XmlDocument();
                myXMLDoc.Load(filesWithPath[0]);

                var ps = myXMLDoc.GetElementsByTagName("p");
                //each p is a TimedLine
                //p with content, contains p.LineStart, p.LineEnd is p.Next.LineStart.
                //p without content, contains only p.LineStart, providing p.Pre.LineEnd.
                //p with content could be the last line.
                //p without content could not be the last line. 2018-05-02
                int psFlag = 0;
                int sPosition = 0;
                string sContent;
                string sWordStart;
                string sWordEnd;
                int sLineNumber = 0;
                string sColor = "";
                string sLastColor = "";
                string sLineColor = "";
                foreach (XmlNode p in ps)
                {
                    if (p.ChildNodes.Count != 0)
                    {
                        //Debug.WriteLine(p.InnerXml);
                        sLineNumber++;
                        if (p.InnerXml.Contains("<"))
                        {
                            if (p.Attributes["t"] != null)
                            {
                                //each s is a content, which may contain "t" as s.WordStart
                                //the first s in p doesn't contain "t", so ss[0].WordStart is LineStart
                                //other s contains "t", as WordStart
                                //s could be the last in p, s.WordEnd is p.LineEnd, which is p.Next.LineStart

                                if (p.Attributes["p"] != null)
                                {
                                    //<pen id="1" fc="#E5E5E5"/>
                                    //<pen id="2" fc="#CCCCCC"/>
                                    //if not said, it'll be id="1" fc="#E5E5E5"
                                    switch (p.Attributes["p"].Value.ToString())
                                    {
                                        case "1":
                                            sLineColor = "E5E5E5";
                                            break;
                                        case "2":
                                            sLineColor = "CCCCCC";
                                            break;
                                        default:
                                            sLineColor = "E5E5E5";
                                            break;
                                    }
                                }
                                else
                                    sLineColor = "E5E5E5";

                                var ss = p.ChildNodes;
                                int ssFlag = 0;
                                foreach (XmlNode s in ss)
                                {
                                    sContent = s.InnerText.Trim();
                                    if (s.Attributes["t"] == null)
                                    {
                                        if (s.Attributes["p"] != null)
                                        {
                                            //<pen id="1" fc="#E5E5E5"/>
                                            //<pen id="2" fc="#CCCCCC"/>
                                            //if not said, it'll be id="1" fc="#E5E5E5"
                                            switch (s.Attributes["p"].Value.ToString())
                                            {
                                                case "1":
                                                    sColor = "E5E5E5";
                                                    break;
                                                case "2":
                                                    sColor = "CCCCCC";
                                                    break;
                                                default:
                                                    sColor = "E5E5E5";
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            if (ssFlag == 0)
                                                sColor = sLineColor;
                                            else
                                                sColor = sLastColor;
                                        }
                                        sLastColor = sColor;
                                        //s is the first word, WordStart should be LineStart.
                                        //s maybe the only word.
                                        //s maybe the final word.
                                        sWordStart = p.Attributes["t"].Value;
                                        if (s.NextSibling == null)
                                        {
                                            if (psFlag == ps.Count - 1)
                                            {
                                                //s is the final word, which means there is no more lines, WordEnd should be LineStart + Duration, which is a bit longer than normal ones.
                                                sWordEnd = (Convert.ToInt32(p.Attributes["t"].Value) + Convert.ToInt32(p.Attributes["d"].Value)).ToString();
                                            }
                                            else
                                            {
                                                //s is the only word, but not the final word, WordEnd should be next LineStart.
                                                sWordEnd = ps[psFlag + 1].Attributes["t"].Value;
                                            }
                                        }
                                        else
                                        {
                                            //s is not the only word, WordEnd should be LineStart + next WordStart.
                                            sWordEnd = (Convert.ToInt32(p.Attributes["t"].Value) + Convert.ToInt32(ss[ssFlag + 1].Attributes["t"].Value)).ToString();
                                        }
                                    }
                                    else
                                    {
                                        //s is not the first word, WordStart should be LineStart + TimeShift.
                                        //s cannot be the only word.
                                        //s maybe the final word.
                                        sWordStart = (Convert.ToInt32(p.Attributes["t"].Value) + Convert.ToInt32(s.Attributes["t"].Value)).ToString();
                                        if (s.NextSibling == null)
                                        {
                                            //s is the last word.
                                            //s maybe the final word.
                                            if (psFlag == ps.Count - 1)
                                            {
                                                //s is the final word, which means there is no more lines, WordEnd should be LineStart + Duration, which is a bit longer than normal ones.
                                                sWordEnd = (Convert.ToInt32(p.Attributes["t"].Value) + Convert.ToInt32(p.Attributes["d"].Value)).ToString();
                                            }
                                            else
                                            {
                                                //s is the last word, but not the final word, WordEnd should be next LineStart.
                                                sWordEnd = ps[psFlag + 1].Attributes["t"].Value;
                                            }
                                        }
                                        else
                                        {
                                            //s is not the last word, WordEnd should be LineStart + next WordStart.
                                            sWordEnd = (Convert.ToInt32(p.Attributes["t"].Value) + Convert.ToInt32(ss[ssFlag + 1].Attributes["t"].Value)).ToString();
                                        }
                                    }
                                    ssFlag++;
                                    //Debug.WriteLine(Content + "\t" + WordStart + "\t" + WordEnd);
                                    Debug.WriteLine(sContent + "\t" + msToTime(sWordStart) + "\t" + msToTime(sWordEnd) + "\t" + sPosition);
                                    mySubtitle.Add(new subtitle { Content = sContent, WordStart = msToTime(sWordStart), WordEnd = msToTime(sWordEnd), Position = sPosition, Selected = false, Color = sColor, LineNumber = sLineNumber });
                                    sPosition = sPosition + sContent.Length + 1;
                                }
                                Debug.WriteLine("");
                            }
                        }
                    }
                    else
                    {
                        if (p.Attributes["t"] != null)
                        {
                            //s could maybe [Music] or [Applause]
                        }
                    }
                    psFlag++;
                }
            }
        }

        private void processVTT()
        {

            //when it's a timed line
            //00:00:24.510 --> 00:00:27.170 align:start position:0%
            //(\d{2}:\d{2}:\d{2}.\d{3})                 $1              00:00:24.510
            //\s-->\s                                   wasted          " --> "
            //(\d{2}:\d{2}:\d{2}.\d{3})                 $2              00:00:27.170
            //" position:0%"                            wasted
            string patternTime = @"(?<TimeStart>\d{2}:\d{2}:\d{2}.\d{3})\s-->\s(?<TimeEnd>\d{2}:\d{2}:\d{2}.\d{3}).*(?<NextStart>\d{2}:\d{2}:\d{2}.\d{3})\s-->\s(?<NextEnd>\d{2}:\d{2}:\d{2}.\d{3})";

            //when it's a content line
            //another<00:21:54.539><c> one</c><00:21:54.809><c> on</c><00:21:54.960><c> top</c><00:21:55.639><c> by</c><00:21:56.639><c> the</c></c><c.colorCCCCCC><00:21:56.700><c> way</c></c><c.colorE5E5E5><00:21:56.940><c> double</c><00:21:57.840><c> it</c></c>
            //<c.colorE5E5E5>close<00:20:24.640><c> it</c></c><c.colorCCCCCC><00:20:24.910><c> close</c><00:20:25.060><c> it</c></c><c.colorE5E5E5><00:20:25.180><c> close</c><00:20:25.570><c> it</c><00:20:25.690><c> and</c><00:20:26.110><c> so</c><00:20:26.740><c> and</c></c><c.colorCCCCCC><00:20:27.010><c> so</c></c>            
            //(?<Content>(\w+')?(\w+))                  get Content
            //(<\/?c.*?>)*                              drop            </c></c.colorFFFFFF>
            //<(?<WordEnd>\d{2}:\d{2}:\d{2}.\d{3})?     get WordEnd     have 0 or one time "00:21:54.539", if it's the end, there would be no time;
            string patternContent = @"(<c.color(?<Color>.*?)>)?(?<Content>(\w+')?(\w+))(<\/c>){0,2}(<c.color(?<NextColor>.*?)>)?<(?<WordEnd>\d{2}:\d{2}:\d{2}.\d{3})?";

            string LineStart = "";
            string LineEnd = "";
            string LineNextStart = "";
            string LineNextEnd = "";
            string sContent = "";
            string sWordStart = "";
            string sWordEnd = "";
            string sColorNext = "";
            string sColorLast = "";
            string sColor = "";
            int sPosition = 0;
            int sLineNumber = 0;

            for (int i = 10; i < listBox1.Items.Count; i = i + 8)
            {
                string str_time = listBox1.Items[i].ToString();
                string str_nextTime = "";
                string str_content = listBox1.Items[i + 2].ToString();
                if (i + 8 < listBox1.Items.Count)
                    str_nextTime = listBox1.Items[i + 8].ToString();
                else
                    str_nextTime = str_time;
                strlist_import.Add(str_time + " " + str_nextTime);
                strlist_import.Add(str_content);
            }

            foreach (var text in strlist_import)
            {
                //match TimeLine                
                if (Regex.IsMatch(text, patternTime))
                {
                    LineStart = Regex.Match(text, patternTime).Groups["TimeStart"].Value;
                    LineEnd = Regex.Match(text, patternTime).Groups["TimeEnd"].Value;
                    LineNextStart = Regex.Match(text, patternTime).Groups["NextStart"].Value;
                    LineNextEnd = Regex.Match(text, patternTime).Groups["NextEnd"].Value;
                    sWordStart = LineStart;
                }
                else
                {
                    if (Regex.IsMatch(text, patternContent))
                    {
                        int wordCount = 0;
                        sLineNumber++;
                        foreach (Match m in Regex.Matches(text, patternContent))
                        {
                            sContent = m.Groups["Content"].Value;

                            //youtube default color
                            //::cue(c.colorCCCCCC) { color: rgb(204, 204, 204); } <- Secondary
                            //::cue(c.colorE5E5E5) { color: rgb(229, 229, 229); } <- Default
                            if (m.Groups["Color"].Value == "")
                            {
                                if (sColorNext == "")
                                    sColor = "FFFFFF";
                                else
                                    sColor = sColorLast;
                            }
                            else
                            {
                                sColor = m.Groups["Color"].Value;
                            }

                            //get next word color
                            if (m.Groups["NextColor"].Value == "")
                            {
                                sColorNext = "FFFFFF"; ;
                            }
                            else
                            {
                                sColorNext = m.Groups["NextColor"].Value;
                            }

                            //get next word appear time, or I call it WordEnd, in fact, youtube vtt don't support intervals between words.
                            if (m.Groups["WordEnd"].Value == "")
                            {
                                sWordEnd = LineEnd;
                            }
                            else
                            {
                                sWordEnd = m.Groups["WordEnd"].Value;
                            }

                            mySubtitle.Add(new subtitle
                            {
                                Content = sContent,
                                WordStart = sWordStart,
                                WordEnd = sWordEnd,
                                NextStart = LineNextStart,
                                NextEnd = LineNextEnd,
                                Position = sPosition,
                                Selected = false,
                                Color = sColor,
                                ColorNext = sColorNext,
                                LineNumber = sLineNumber
                            });
                            sPosition = sPosition + m.Groups["Content"].Value.Length + 1;
                            sWordStart = sWordEnd;
                            sColorLast = sColorNext;
                            wordCount++;
                        }
                    }
                }
            }
        }

        private int timeToMs(string input)
        {
            string pattern = @"(?<Hour>\d{2}):(?<Minute>\d{2}):(?<Second>\d{2}).(?<MS>\d{3})";
            int h = 0;
            int m = 0;
            int s = 0;
            int ms = 0;
            h = Convert.ToInt16(Regex.Match(input, pattern).Groups["Hour"].Value);
            m = Convert.ToInt16(Regex.Match(input, pattern).Groups["Minute"].Value);
            s = Convert.ToInt16(Regex.Match(input, pattern).Groups["Second"].Value);
            ms = Convert.ToInt16(Regex.Match(input, pattern).Groups["MS"].Value);
            int result = ((h * 60 + m) * 60 + s) * 1000 + ms;
            return result;
        }

        private string RGBToBGR(string input)
        {
            return input.Substring(4, 2) + input.Substring(2, 2) + input.Substring(0, 2);
        }

        private async void generateASS()
        {
            mySubtitle.Clear();
            if (filesWithPath[0] != "")
            {
                switch (str_fileExt)
                {
                    case ".xml":
                        processXML();
                        break;
                    case ".XML":
                        processXML();
                        break;
                    case ".vtt":
                        processVTT();
                        break;
                    case ".VTT":
                        processVTT();
                        break;
                    default:
                        break;
                }
            }
            List<string> list_str_result = new List<string>();

            try
            {
                var strlist_header = File.ReadAllLines(str_programPath + "header_ass.txt");
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

            int mFlag = 0;
            int sLineNumber = 0;
            string str_Line1 = "";
            string str_Line2 = "";
            string str_Line3 = "";
            string str_LineStart = "";
            string str_LineEnd = "";
            //string str_LineNextStart = "";
            foreach (var m in mySubtitle)
            {
                //Color process
                string sContent = m.Content;
                string sColor = @"{\c&H" + RGBToBGR(m.Color) + "&}";

                if (m.LineNumber > sLineNumber)
                {
                    sLineNumber++;
                    //it's a new line
                    str_LineStart = m.WordStart;
                    str_LineEnd = m.NextStart;
                    if (sLineNumber != 1)
                    {
                        //it's not the first line
                        list_str_result.Add(str_Line1);
                        list_str_result.Add(str_Line2);
                        list_str_result.Add(str_Line3);
                    }

                    str_Line1 = "";
                    str_Line2 = "";
                    str_Line3 = "";

                    //this is a new line
                    int int_CurrentStart = timeToMs(str_LineStart);
                    int int_CurrentEnd = timeToMs(str_LineEnd);                                       
                    while (mySubtitle[mFlag].LineNumber != m.LineNumber)
                    {
                        mFlag++;
                    }
                    int int_NextEnd = timeToMs(mySubtitle[mFlag].NextEnd);

                    //1 show the current line until it moves up
                    int int_Current1Start = int_CurrentStart;
                    int int_Current1End = int_CurrentEnd;
                    //2 moves up and show until the next line moves up
                    int int_Current2Start = int_Current1End;
                    int int_Current2End = int_NextEnd;
                    //3 moves up to disappear using 500ms
                    int int_Current3Start = int_NextEnd;
                    int int_Current3End = int_Current3Start + 500;
                    int int_PosX = Convert.ToInt32(textBox_x.Text);
                    int int_PosY = Convert.ToInt32(textBox_y.Text);
                    int int_LineHeight = Convert.ToInt32(textBox_height.Text);
                    int int_LineDuration = 370;

                    //Karaoke process
                    string str_LineHeader1 = "Dialogue: 0,"
                        + fn_msTOString(int_Current1Start) + ","
                        + fn_msTOString(int_Current1End) + ",YouTubeAuto,,0,0,0,,";
                    string str_LineHeader2 = "Dialogue: 0,"
                        + fn_msTOString(int_Current2Start) + ","
                        + fn_msTOString(int_Current2End) + ",YouTubeAuto,,0,0,0,,";
                    string str_LineHeader3 = "Dialogue: 0,"
                        + fn_msTOString(int_Current3Start) + ","
                        + fn_msTOString(int_Current3End) + ",YouTubeAuto,,0,0,0,,";

                    string str_LineMove1 = @"{\fs53.7\move("
                        + int_PosX.ToString() + "," + int_PosY.ToString() + ","
                        + int_PosX.ToString() + "," + (int_PosY - int_LineHeight).ToString() + ","
                        + "0," + int_LineDuration.ToString() + ")}";
                    string str_LineMove2 = @"{\fs53.7\move("
                        + int_PosX.ToString() + "," + (int_PosY - int_LineHeight).ToString() + ","
                        + int_PosX.ToString() + "," + (int_PosY - int_LineHeight * 2).ToString() + ","
                        + "0," + int_LineDuration.ToString() + ")}";
                    //plus 10 to make no intervals between lines without flashing
                    string str_LineMove3 = @"{\fs53.7\move("
                        + int_PosX.ToString() + "," + (int_PosY - int_LineHeight * 2).ToString() + ","
                        + int_PosX.ToString() + "," + (int_PosY - int_LineHeight * 3).ToString() + ","
                        + "10," + (int_LineDuration+10).ToString() + ")"
                        + @"\clip(0," + (int_PosY - int_LineHeight * 3 - 15).ToString() + ",1920,1080)}";

                    float float_time = (timeToMs(m.WordEnd) - timeToMs(m.WordStart)) / 10;
                    string sWordDuration = Math.Round(float_time, 1).ToString();

                    string str_ContentKaraoke = @"{\k" + sWordDuration + "}" + sColor + sContent + " " + @"{\c}";
                    string str_ContentStatic = sColor + sContent + " " + @"{\c}";

                    str_Line1 = str_LineHeader1 + str_LineMove1 + str_ContentKaraoke;
                    str_Line2 = str_LineHeader2 + str_LineMove2 + str_ContentStatic;
                    str_Line3 = str_LineHeader3 + str_LineMove3 + str_ContentStatic;
                }
                else
                {
                    float float_time = (timeToMs(m.WordEnd) - timeToMs(m.WordStart)) / 10;
                    string sWordDuration = Math.Round(float_time, 1).ToString();
                    string str_ContentKaraoke = @"{\k" + sWordDuration + "}" + sColor + sContent + " " + @"{\c}";
                    string str_ContentStatic = sColor + sContent + " " + @"{\c}";

                    str_Line1 += str_ContentKaraoke;
                    str_Line2 += str_ContentStatic;
                    str_Line3 += str_ContentStatic;
                    str_LineEnd = m.WordEnd;
                }
            }

            if (str_fileName != "")
            {
                string str_fileOutput = str_filePath + str_fileName + ".youtube.ass";
                File.WriteAllLines(str_fileOutput, list_str_result);
            }

            //write script
            if (checkBox_writeScript.Checked == true)
                fn_writeScript();

            await Animation();
        }

        public async Task Animation()
        {
            button_doit.Text = "Done!";
            await Task.Delay(2000);
            button_doit.Text = "And another one?";

        }

        private void button_saveSetting_Click(object sender, EventArgs e)
        {
            fn_saveSetting(Convert.ToInt32(textBox_x.Text), Convert.ToInt32(textBox_y.Text), Convert.ToInt32(textBox_height.Text), textBox_style.Text, checkBox_writeScript.Checked);
        }

        private void button_doit_Click(object sender, EventArgs e)
        {
            generateASS();
        }
    }
}
