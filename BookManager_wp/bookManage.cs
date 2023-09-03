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
    public partial class bookManage : Form
    {
        public bookManage()
        {
            InitializeComponent();
            dataGridView1.DataSource = null;
            if (DataManager.Books.Count > 0)
                dataGridView1.DataSource = DataManager.Books;
        }
        // 책 추가
        private void button1_Click(object sender, EventArgs e)
        {
            bool existBook = false;
            foreach (var item in DataManager.Books)
            {
                // C#은 equals 대신 ==으로 string 비교 가능
                // java는 버전에 따라, 상황에 따라 다르다.
                if (item.ISBN == textBox1.Text)
                {
                    existBook = true;
                    break;
                }
            }
            if (existBook)
            {
                MessageBox.Show("이미 있음");
            }
            else
            {
                Book book = new Book();
                book.ISBN = textBox1.Text;
                book.도서명 = textBox2.Text;
                book.분류 = textBox3.Text;
                book.저자 = textBox4.Text;
                book.설명 = textBox5.Text;
                book.출판사 = textBox6.Text;
                book.출판일 = textBox7.Text;
                int.TryParse(textBox8.Text, out int 보유권수);
                book.보유권수 = 보유권수;
                book.등록일 = DateTime.Now;
                if (보유권수 <= 0)
                {
                    MessageBox.Show("보유권수가 이상해요");
                    return;
                }
                DataManager.Books.Add(book);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = DataManager.Books;
                DataManager.Save();
                //book.page = textBox3.Text
            }
        }
        // 책 수정
        private void button2_Click(object sender, EventArgs e)
        {
            Book b = null;
            for (int i = 0; i < DataManager.Books.Count; i++)
            {
                if (DataManager.Books[i].ISBN == textBox1.Text)
                {
                    b = DataManager.Books[i];
                    b.도서명 = textBox2.Text;
                    b.분류 = textBox3.Text;
                    b.저자 = textBox4.Text;
                    b.설명 = textBox5.Text;
                    b.출판사 = textBox6.Text;
                    b.출판일 = textBox7.Text;
                    int.TryParse(textBox8.Text, out int 보유권수);
                    b.보유권수 = 보유권수;
                    if (보유권수 <= 0)
                    {
                        MessageBox.Show("보유권수 값이 잘못되었습니다.");
                        return;
                    }
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = DataManager.Books;
                    DataManager.Save();
                }
            }
            if (b == null)
            {
                MessageBox.Show("없는 책입니다.");
            }
        }
        // 책 삭제
        private void button3_Click(object sender, EventArgs e)
        {
            bool existBook = false;
            for (int i = 0; i < DataManager.Books.Count; i++)
            {
                if (DataManager.Books[i].ISBN == textBox1.Text)
                {
                    // DataManager.Books.Remove(DataManager.Books[i]);
                    DataManager.Books.RemoveAt(i);
                    existBook = true;
                }
            }
            if (existBook)
            {
                // 초기화를 안해주면 에러가 뜨면서 튕긴다.
                dataGridView1.DataSource = null;
                if (DataManager.Books.Count > 0)
                {
                    dataGridView1.DataSource = DataManager.Books;
                }
                DataManager.Save();
            }
            else
            {
                MessageBox.Show("없는 책 입니다.");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Book b = dataGridView1.CurrentRow.DataBoundItem as Book;
            textBox1.Text = b.ISBN;
            textBox2.Text = b.도서명;
            textBox3.Text = b.분류;
            textBox4.Text = b.저자;
            textBox5.Text = b.설명;
            textBox6.Text = b.출판사;
            textBox7.Text = b.출판일;
            textBox8.Text = b.보유권수.ToString();
        }
    }
}
