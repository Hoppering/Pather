using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Text;

namespace Pather
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            loadData();


        }

        public static string pathFile = "";

        private void loadData()
        {
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            listView1.Columns.Add("Наименование", 200);
            listView1.Columns.Add("Тип", 100);
            listView1.Columns.Add("Путь", 400);

            DriveInfo[] drivers = DriveInfo.GetDrives();
            List<string> driversAll = new List<string>();
            foreach (DriveInfo item in drivers)
            {
                if (item.DriveType.ToString() != "CDRom")
                {
                    driversAll.Add(item.Name);
                }
            }

            comboBox1.Items.AddRange(driversAll.ToArray());
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            goUp(path);

        }

        private void goUp(string path)
        {
            List<string> link = new List<string>(path.Split(@"\".ToCharArray()));

            link.RemoveAt(link.Count - 1);
            string linkRes = string.Join(@"\", link);
            if ((linkRes.Split(@"\".ToCharArray()).Length == 1))
            {
                linkRes += @"\";
            }

            textBox1.Text = linkRes;

            getAll(linkRes);

        }

        private void getAll(string path)
        {
            DriveInfo[] drivers = DriveInfo.GetDrives();
            for (int i = 0; i < drivers.Length; i++)
            {
                if (path.Split(@"\".ToCharArray())[0] == drivers[i].Name.Split(@"\".ToCharArray())[0])
                {
                    comboBox1.SelectedIndex = i;
                }
            }

            try
            {
                List<string> files = Directory.GetDirectories(path).ToList();
                List<string> all = Directory.GetFiles(path).ToList();
                foreach (string items in files)
                {
                    all.Add(items);
                }

                string[] arr1 = new string[4];

                ListViewItem item;
                listView1.Items.Clear();

                foreach (string i in all)
                {
                    arr1[0] = i.Split(@"\".ToCharArray())[i.Split(@"\".ToCharArray()).Length - 1];

                    if (Path.GetExtension(i) == "")
                    {
                        arr1[1] = "Папка";
                    }
                    else
                    {
                        arr1[1] = i.Split(@".".ToCharArray())[i.Split(@".".ToCharArray()).Length - 1];
                    }




                    arr1[2] = i;
                    item = new ListViewItem(arr1);
                    listView1.Items.Add(item);
                }
            }
            catch
            {
                MessageBox.Show(
                    "Путь не найден",
                    "Ошибка!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );

                List<string> link = new List<string>(path.Split(@"\".ToCharArray()));

                link.RemoveAt(link.Count - 1);
                string linkRes = string.Join(@"\", link);
                if ((linkRes.Split(@"\".ToCharArray()).Length == 1))
                {
                    linkRes += @"\";
                }

                textBox1.Text = linkRes;

            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            string nextPath = path + @"\" + listView1.SelectedItems[0].Text;
            string ext = Path.GetExtension(nextPath);
            if (ext == "")
            {
                textBox1.Text = nextPath;

                getAll(nextPath);

            }
            else
            {
                System.Diagnostics.Process.Start(nextPath);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedState = comboBox1.SelectedItem.ToString();
            textBox1.Text = selectedState;
            getAll(selectedState);
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                getAll(textBox1.Text);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string sourceFolder = textBox1.Text + @"\" + listView1.SelectedItems[0].Text;
            string compressFolder = textBox1.Text + @"\" + listView1.SelectedItems[0].Text + @".zip";
            ZipFile.CreateFromDirectory(sourceFolder, compressFolder, CompressionLevel.Optimal, false);
            getAll(textBox1.Text);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            pathFile = textBox1.Text;
            Form2 createFile = new Form2();
            createFile.ShowDialog();
            getAll(textBox1.Text);
        }
    }
}
