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

            dataGridView1.CellClick += dCellClick;
            DataGridViewCellEventHandler dCellClick2 = dgClick;
            dataGridView2.CellClick += dCellClick2;

        }

        private void dgClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView thisGridView = sender as DataGridView;
            User u = thisGridView.CurrentRow.DataBoundItem as User;
            textBox3.Text = u.ID;
        }

        private void dCellClick(object sender, DataGridViewCellEventArgs e)
        {
            Book book = dataGridView1.CurrentRow.DataBoundItem as Book;
            textBox1.Text = book.ISBN;
            textBox2.Text = book.도서명;
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
            dataGridView1.DataSource = null;
            if (DataManager.Books.Count > 0)
            {
                dataGridView1.DataSource = DataManager.Books;
            }
            label2.Text = "전체 도서 수 : " + DataManager.Books.Count;
            label4.Text = "대출 중인 도서의 수 : " + DataManager.Books.Where(checkIsBorrowed).Count();
            label5.Text = "연체 중인 도서의 수 : " + DataManager.Books.Where(
                delegate (Book x)
                {
                    return x.대여상태 && x.대여일.AddDays(7) < DateTime.Now;
                }
                ).Count();
        }

        private void 사용자관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new userManage().ShowDialog();
            label3.Text = "전체 회원 수 : " + DataManager.Users.Count;
            dataGridView2.DataSource = null;
            if (DataManager.Users.Count > 0)
                dataGridView2.DataSource = DataManager.Users;
        }

        // 대여
        private void button1_Click(object sender, EventArgs e)
        {
            // 양쪽 공백 다 지웠을 때 아무것도 없다면
            if (textBox1.Text.Trim() == "")
                MessageBox.Show("ISBN이 없음");
            else if (textBox3.Text.Trim() == "")
                MessageBox.Show("사용자 ID가 없음");
            else
            {
                try
                {
                    Book b = DataManager.Books.Single(x => x.ISBN == textBox1.Text);
                    if (b.대여상태)
                        MessageBox.Show("이미 대여된 책입니다.");
                    else
                    {
                        User u = DataManager.Users.Single(x => x.ID == textBox3.Text);
                        b.빌린ID = u.ID;
                        b.빌린이름 = u.이름;
                        b.대여상태 = true;
                        b.대여일 = DateTime.Now;

                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = DataManager.Books;

                        label4.Text = "대출 중인 도서의 수 : " + DataManager.Books.Where(checkIsBorrowed).Count();

                        DataManager.Save();
                        MessageBox.Show($"{b.도서명} 책이 {u.이름}님에게 대여됨 ");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("isbn이 없습니다.");
                }
            }
        }

        //반납
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
                MessageBox.Show("ISBN 입력하세요");
            else
            {
                try
                {
                    Book b = DataManager.Books.Single(x => x.ISBN == textBox1.Text);
                    if (b.대여상태)
                    {
                        // 빌린 사람 없다는 뜻
                        b.빌린ID = "";
                        b.빌린이름 = "";
                        b.대여상태 = false;

                        DateTime oldDay = b.대여일;
                        // 날짜 초기화
                        b.대여일 = new DateTime();

                        TimeSpan timeDiff = DateTime.Now - oldDay;

                        DataManager.Save();
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = DataManager.Books;

                        if (timeDiff.Days > 7)
                            MessageBox.Show(b.도서명 + " 연체 반납");
                        else
                            MessageBox.Show(b.도서명 + " 정상 반납");
                        label4.Text = "대출 중인 도서의 수 : " + DataManager.Books.Where(checkIsBorrowed).Count();

                        label5.Text = "연체 중인 도서의 수 : " + DataManager.Books.Where(
                            delegate (Book x)
                            {
                                return x.대여상태 && x.대여일.AddDays(7) < DateTime.Now;
                            }
                            ).Count();
                    }
                    else
                    {
                        MessageBox.Show("아직 안 빌린 책입니다");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("존재하지 않는 책입니다.");
                }
            }
        }
    }
}
 