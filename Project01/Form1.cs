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
namespace Project01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //open dialog and select a file name
            Stream MyListPlay = null;  //สร้าง ลิสเพล ขึ้นมาเพื่อใช้งาน
            openFileDialog1 = new OpenFileDialog();//สร้างไฟล์ไดอาล็อค
            openFileDialog1.Filter = "All FILES (*.*)|*.*";//ฟิลเตอร์เพื่อจับนามสกุลไฟล์
            openFileDialog1.Multiselect = true;//ทำให้สามารถเพิ่มไฟล์ได้พร้อมกันหลายๆไฟล์
            if (openFileDialog1.ShowDialog() == System.Windows.Forms. DialogResult.OK)
            {


                if ((MyListPlay = openFileDialog1.OpenFile()) != null)//สร้างเงื่อนไขเพื่อเพิ่มไฟล์
                {
                    using (MyListPlay)
                    {
                        string[] filePath = openFileDialog1.FileNames;
                        string[] filename = openFileDialog1.SafeFileNames;

                        for (int i = 0; i < openFileDialog1.SafeFileNames.Count(); i++)//จัดเก็บไฟล์ จะใช้ลูปเพื่อให้เพิ่มไฟล์ได้หลายๆรอบ
                        {
                            string[] saLvwItem = new string[2];
                            saLvwItem[0] = filename[i];
                            saLvwItem[1] = filePath[i];
                            ListViewItem lvi = new ListViewItem(saLvwItem);//นำมาจัดเก็บไว้ใน ลิสวิว
                            listView1.Items.Add(lvi);
                        }
                    }
                }
            }
        }
                   
        private void btnPlay_Click(object sender, EventArgs e)
        {   
            // play all media on playlist
            WMPLib.IWMPPlaylist playlist = axWindowsMediaPlayer1.playlistCollection.newPlaylist("myplaylist");//สั่งให้มีเดียเพลเยอร์เล่นไฟล์
            WMPLib.IWMPMedia media;

            for (int i = 0; i < listView1.Items.Count; i++)//ลูปเพื่อให้เล่นไฟล์ทั้งหมดแบบต่อเนื่อง
            {

                int j = 1;
                media = axWindowsMediaPlayer1.newMedia(listView1.Items[i].SubItems[j].Text);
                playlist.appendItem(media);
                j++;
                axWindowsMediaPlayer1.currentPlaylist = playlist;
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //double click on a song for play
            {
                string selectedFile = listView1.FocusedItem.SubItems[1].Text;//สร้างขึ้นเพื่อให้สามารถดับเบิ้ลคลิกที่ไฟล์เพื่อเล่นได้
                axWindowsMediaPlayer1.URL = @selectedFile;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //clear playlist of data
            listView1.Items.Clear();
        }

        private void btnFull_Click(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)//สามารถทำให้ตัวมีเดียเพลเยอร์สามารถเข้า/ออกโหมดฟูลสกินได้ ในกรณีที่เล่นไฟอยู่
            { axWindowsMediaPlayer1.fullScreen = true; }
        }
      }
    }


