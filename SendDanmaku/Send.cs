using System;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace SendDanmaku
{
    public partial class Send : Form
    {
        public string cookie;
        public string fontsize;
        public string color;
        public string roomid;
        public Send()
        {
            InitializeComponent();
            Setting st = new Setting();
            try
            {
                string[] rows = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\sendsetting.dat");
                this.fontsize = rows[0];
                this.color = rows[1];
                this.roomid = rows[2];
                this.cookie = rows[3];
            }
            catch
            {
                if (st.ShowDialog() == DialogResult.OK)
                {
                    this.fontsize = st.fontsize;
                    this.color = st.color;
                    this.roomid = st.roomid;
                    this.cookie = st.cookie;
                }
            }
            while (this.cookie == "")
            {
                MessageBox.Show("用户COOKIE不存在！您可以在Chrome内核浏览器中开启开发者模式，在console中输入‘document.cookie’+回车确认，然后复制双引号内内容即为本软件所需cookie", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (st.ShowDialog() == DialogResult.OK)
                {
                    this.fontsize = st.fontsize;
                    this.color = st.color;
                    this.roomid = st.roomid;
                    this.cookie = st.cookie;
                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void 退出QToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void 关于AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("作者： Sg4Dylan @ Github\n授權協議： GPLv2\n插件版本： 0.1 Beta\n感謝您的使用！", "关于", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void 配置SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setting st = new Setting();
            if (st.ShowDialog() == DialogResult.OK)
            {
                this.fontsize = st.fontsize;
                this.color = st.color;
                this.roomid = st.roomid;
                this.cookie = st.cookie;
            }
        }

        private void send_button_Click(object sender, EventArgs e)
        {
            if (this.cookie == "" || this.roomid == "")
            {
                MessageBox.Show("请正确配置cookie及房间号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://live.bilibili.com/msg/send");
            request.Method = "POST";
            string postData = "color=" + this.color + "&fontsize=" + this.fontsize + "&mode=1&msg=" + danmaku_inputbox.Text + "&rnd=145232544&roomid=" + this.roomid + "";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.SetCookies(new Uri("http://live.bilibili.com/"), cookie);
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            danmaku_inputbox.Text = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void 帮助HToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("关于cookie获取：您可以在Chrome内核浏览器中开启开发者模式，在console中输入‘document.cookie’+回车确认，然后复制双引号内内容即为本软件所需cookie", "帮助", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
