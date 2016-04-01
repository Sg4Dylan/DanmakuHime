using System;
using System.Windows.Forms;
using System.IO;

namespace SendDanmaku
{
    public partial class Setting : Form
    {
        public string cookie;
        public string fontsize;
        public string color;
        public string roomid;

        public Setting()
        {
            InitializeComponent();
            try
            {
                string[] rows = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\sendsetting.dat");
                numericUpDown1.Text = rows[0];
                textBox2.Text = rows[1];
                textBox3.Text = rows[2];
                textBox4.Text = rows[3];
            }
            catch
            {
                MessageBox.Show("无预设文件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.fontsize = numericUpDown1.Text;
            this.color = textBox2.Text;
            this.roomid = textBox3.Text;
            this.cookie = textBox4.Text;
            cookie = this.cookie.Replace(";", ",");
            string[] lines = { fontsize, color, roomid, cookie };
            File.WriteAllLines(Directory.GetCurrentDirectory()+"\\sendsetting.dat", lines);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void color_Buton_Click(object sender, EventArgs e)
        {
            ColorDialog color_Dialog = new ColorDialog();
            if (color_Dialog.ShowDialog() ==  DialogResult.OK)
            {
                //True hex color code
                //String font_color_unreal = (color_Dialog.Color.R).ToString("X") + (color_Dialog.Color.G).ToString("X") + (color_Dialog.Color.B).ToString("X");
                //MessageBox.Show(font_color_unreal);
                String font_color_real = (255-color_Dialog.Color.R).ToString("X") + (255 - color_Dialog.Color.G).ToString("X") + (255 - color_Dialog.Color.B).ToString("X");
                //MessageBox.Show(Convert.ToInt32(font_color_real, 16).ToString());
                textBox2.Text = Convert.ToInt32(font_color_real, 16).ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
