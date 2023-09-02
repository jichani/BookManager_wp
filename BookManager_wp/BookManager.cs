using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookManager_wp
{
    public partial class BookManager : Form
    {
        public BookManager()
        {
            InitializeComponent();
            label1.Text = DateTime.Now.ToString("yyyy년 MM월 dd일 HH시 mm분 ss초");
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("yyyy년 MM월 dd일 HH시 mm분 ss초");
        }

        private void 책관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // ShowDialog : 모달
            // 뒤에꺼 클릭 안 되고 코드가 여기서 멈춰있다.
            new bookManage().ShowDialog();
        }

        private void 사용자관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new userManage().ShowDialog();
        }
    }
}
 