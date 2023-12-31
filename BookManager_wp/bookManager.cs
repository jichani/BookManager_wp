﻿using System;
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
        // 콜백 함수
        private bool checkIsBorrowed(Book b)
        {
            return b.대여상태;
        }
        public BookManager()
        {
            InitializeComponent();
            label1.Text = DateTime.Now.ToString("yyyy년 MM월 dd일 HH시 mm분 ss초");
            label2.Text = "전체 도서 수 : " + DataManager.Books.Count;
            label3.Text = "전체 회원 수 : " + DataManager.Users.Count;

            // 람다를 이용(책)

            // 대출 중인 도서의 수 = 메소드
            // Where = Books 안에서 필터링 역할을 한다. checkIsBorrowed를 Books 안에 있는 Book들
            // 하나하나에 대해서 호출하고 그 결과가 true인 것만 남긴다. .Count는 true의 개수를 센다.
            label4.Text = "대출 중인 도서의 수 : " + DataManager.Books.Where(checkIsBorrowed).Count();
            // 연체 중인 도서의 수 = 무명 델리게이트 이용
            // 빌리고 나서 7일 이상이 경과되면 연체로 간주한다.
            label5.Text = "연체 중인 도서의 수 : " + DataManager.Books.Where(
                delegate (Book x)
                {
                    return x.대여상태 && x.대여일.AddDays(7) < DateTime.Now;
                }
                ).Count();

            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            if (DataManager.Books.Count > 0)
                dataGridView1.DataSource = DataManager.Books;
            if (DataManager.Users.Count > 0)
                dataGridView2.DataSource = DataManager.Users;

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
 