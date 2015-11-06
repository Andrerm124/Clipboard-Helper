using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Clipboard_Helper
{
    public partial class MasterForm : Form
    {
        public static MasterForm thisForm;
        public static string startupPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string resourcePath = startupPath + "\\ClipboardHelper";
        public static string historyFile = resourcePath + "\\ClipboardHistory.txt";
        public static string settingsFile = resourcePath + "\\Settings.txt";

        public Boolean beingDragged = false;
        public Point clickOffset = new Point(0, 0);
        public Settings settings;
        public static List<StoredData> clipboardData = new List<StoredData>();

        public MasterForm()
        {
            InitializeComponent();
            
            lstClips.eventHandler += new CustomListBox.MyEventHandler(ClipClicked);

            foreach (Control panels in this.Controls)
            {
                if (panels is CustomPanel)
                {
                    pnlBase.Initialized();

                    foreach (Control c in panels.Controls)
                    {
                        if (c is CustomListBox)
                            ((CustomListBox)c).Initialized();
                    }
                }
            }

            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Properties.Settings.Default.ResourcePath = "NoneSet";
            Properties.Settings.Default.Save();

            string defaultPath = (String)Properties.Settings.Default.ResourcePath;

            if (defaultPath.Equals("NoneSet"))
            {
                Console.WriteLine("Changing");
                UpdateResourcePath(resourcePath);

                Properties.Settings.Default.ResourcePath = resourcePath;
                Properties.Settings.Default.Save();
            } else
                UpdateResourcePath(defaultPath);

            if (!Directory.Exists(resourcePath))
                Directory.CreateDirectory(resourcePath);

            LoadSettings();

            LoadClipboard();

            thisForm = this;
        }

        public static void UpdateResourcePath(string newPath)
        {
            resourcePath = newPath;
            historyFile = resourcePath + "\\ClipboardHistory.txt";
            settingsFile = resourcePath + "\\Settings.txt";
        }

        public static void UpdateResourcePathAndUpdate(string newPath)
        {
            resourcePath = newPath;
            historyFile = resourcePath + "\\ClipboardHistory.txt";
            settingsFile = resourcePath + "\\Settings.txt";

            Properties.Settings.Default["ResourcePath"] = resourcePath;
            Properties.Settings.Default.Save();

            thisForm.LoadSettings();
            thisForm.LoadClipboard();
        }

        private void ClipClicked(object sender, ListBoxEvent e)
        {
            lstClips.RemoveItem(e.indexClicked);
            Clipboard.SetDataObject(e.itemClicked, true, 10, 100);
        }

        private void SaveClipboard()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<StoredData>));

            using (StreamWriter stream = new StreamWriter(historyFile))
            {
                serializer.Serialize(stream, clipboardData);
            }
        }

        private void LoadClipboard()
        {
            if (!File.Exists(historyFile))
                return;

            using (StreamReader stream = new StreamReader(historyFile))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<StoredData>));
                List<StoredData> loadedData = (List<StoredData>)serializer.Deserialize(stream);
                loadedData.Reverse();

                foreach (StoredData storedData in loadedData)
                {
                    lstClips.AddItem(storedData);
                }
            }
        }

        private int FindKeyInData(string findKey)
        {
            int index = -1;
            int foundIndex = -1;

            foreach (StoredData storedData in clipboardData)
            {
                index++;

                //Console.WriteLine("Index: " + index);

                if (findKey.Replace("\r", "").Equals(storedData.storedClip.Replace("\r", "")))
                {
                    foundIndex = index;
                    break;
                }
            }

            return foundIndex;
        }

        private void LoadSettings()
        {
            if (!File.Exists(settingsFile))
            {
                CreateDefaultSettings();
                return;
            }

            using (StreamReader stream = new StreamReader(settingsFile))
            {
                XmlSerializer serialiser = new XmlSerializer(typeof(Settings));

                try
                {
                    settings = (Settings)serialiser.Deserialize(stream);
                }
                catch (InvalidOperationException)
                {
                    CreateDefaultSettings();
                    return;
                }
            }

            this.Location = settings.position;
        }

        private void SaveSettings()
        {
            if (settings == null)
                return;

            settings.position = this.Location;

            using (StreamWriter stream = new StreamWriter(settingsFile))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                serializer.Serialize(stream, settings);
            }
        }

        private void CreateDefaultSettings()
        {
            settings = new Settings();

            int xPos = Screen.PrimaryScreen.Bounds.Width - 285;
            int yPos = Screen.PrimaryScreen.WorkingArea.Height - 262;
            this.Left = xPos;
            this.Top = yPos;
            settings.position = new Point(xPos, yPos);

            using (StreamWriter stream = new StreamWriter(settingsFile))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                serializer.Serialize(stream, settings);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            beingDragged = true;
            clickOffset.X = e.X;
            clickOffset.Y = e.Y;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            beingDragged = false;

            SaveSettings();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {            
            if (beingDragged)
            {
                Point mousePos = this.PointToScreen(e.Location);

                int screenWidth = Screen.PrimaryScreen.Bounds.Width;
                int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;

                int newX = mousePos.X - clickOffset.X;
                int newY = mousePos.Y - clickOffset.Y;

                if (newX < 1)
                    newX = 1;
                else if (newX + this.Width > screenWidth)
                    newX = screenWidth - this.Width - 1;

                if (newY < 1)
                    newY = 1;
                else if (newY + this.Height > screenHeight)
                    newY = screenHeight - this.Height - 1;

                this.Left = newX;
                this.Top = newY;
            }
        }

        private void clipboardMonitor1_ClipboardChanged(object sender, ClipboardChangedEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                string clip = (string) e.DataObject.GetData(DataFormats.Text);

                int foundIndex = FindKeyInData(clip);

                if (foundIndex != 0)
                {
                    if(foundIndex != -1)
                        lstClips.RemoveItem(foundIndex);

                    StoredData sd = new StoredData();
                    sd.storedClip = clip;
                    sd.storedTime = DateTime.Now;
                    lstClips.AddItem(sd);
                    SaveClipboard();

                    lstClips.indexClicked = 0;
                }
            }
        }

        private void MasterForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                trayIcon.Visible = true;
                trayIcon.ShowBalloonTip(0);
                this.Hide();
            }
        }

        private void customDeleteButton1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void trayIcon_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void trayIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void MasterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            trayIcon.Dispose();
        }

        private void MasterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            trayIcon.Dispose();
        }

        private void trayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
            else if (e.Button == MouseButtons.Right)
            {
                trayIconMenu.Show(Control.MousePosition);
                trayIconMenu.BringToFront();
            }
        }

        private void trayIconMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string itemClicked = e.ClickedItem.Text;

            if (itemClicked.Equals("Exit"))
                this.Dispose();
            else if (itemClicked.Equals("Settings"))
            {
                SettingsForm form = new SettingsForm();
                form.Visible = true;
            }
        }

        private void trayIconMenu_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}


        // Old Settings Code \\
        /*private void SetSavedPosition()
        {
            if(!File.Exists(settingsFile))
            {
                this.Left = Screen.PrimaryScreen.Bounds.Width - this.Width - 1;
                this.Top = Screen.PrimaryScreen.WorkingArea.Height - this.Height - 1;

                SaveWindowPosition();
                return;
            }

            using (StreamReader reader = File.OpenText(settingsFile))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    Console.WriteLine("Line: " + line);

                    if (line.StartsWith("Position"))
                    {
                        Console.WriteLine("FoundPos");
                        try
                        {
                            string locSplit1 = line.Split(':')[1];
                            string[] locSplit2 = locSplit1.Split(',');
                            string xSplit = locSplit2[0];
                            string ySplit = locSplit2[1];

                            int xInt;
                            int yInt;

                            if (int.TryParse(xSplit, out xInt))
                            {
                                Console.WriteLine("xInt: " + xInt);
                            }
                            else
                                return;

                            if (int.TryParse(ySplit, out yInt))
                            {
                                Console.WriteLine("yInt: " + yInt);
                            }
                            else
                                return;

                            Console.WriteLine("X: " + xInt + " Y: " + yInt);

                            this.Left = xInt;
                            this.Top = yInt;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
            }
        }

        private void SaveWindowPosition()
        {
            using (StreamWriter writer = File.CreateText(settingsFile))
            {
                writer.WriteLine("Position:" + this.Location.X + "," + this.Location.Y);
            }
        }*/

        // Old Clipboard History \\
        /*private void LoadOldClips()
        {
            if (File.Exists(historyFile))
            {
                StreamReader reader = new StreamReader(historyFile);
                StringBuilder clip = new StringBuilder();
                Boolean building = false;

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    if (line.Length > 2 && line.Substring(0, 3).Equals("BS;"))
                    {
                        line = line.ToString().Remove(0, 3);
                        building = true;
                    }

                    if (building)
                    {
                        Boolean endClip = false;

                        if (line.Length > 2 && line.EndsWith("ES;"))
                        {
                            int lth = line.Length;
                            line = line.Remove(lth - 3, 3);
                            clip.Append(line);

                            endClip = true;
                        } else
                            clip.AppendLine(line);

                        if (endClip)
                        {
                            if (!clipboardData.Contains(clip.ToString()))
                                this.lstClips.AddItem(clip.ToString());

                            clip.Clear();
                            building = false;
                        }
                    }
                }

                reader.Close();
            }
            else
                File.Create(historyFile);
        }

        private void AddAndReSortClipHistory(string clip)
        {
            File.OpenWrite(historyFile);
                
            StreamWriter writer = new StreamWriter(historyFile);

            foreach (ListBoxItem item in lstClips.contents)
            {
                writer.WriteLine("BS;" + item.title + "ES;");
            }

            writer.Flush();
            writer.Close();
        }

        private void AppendClipToHistory(string clip)
        {
            if (!File.Exists(historyFile))
                File.Create(historyFile);

            using (StreamWriter writer = File.AppendText(historyFile))
            {
                writer.WriteLine("BS;" + clip + "ES;");
            }
        }*/
