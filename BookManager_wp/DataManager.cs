using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BookManager_wp
{
    public class DataManager
    {
        // 클래스 변수. 인스턴스 변수가 아니므로 이 list는 공유된다.
        public static List<Book> Books = new List<Book>();
        public static List<User> Users = new List<User>();

        // XML 방식을 통해서 데이터 관리할 것

        // 정적 생성자. 접근 제한자(public, private) 사용 못 한다.
        // 맨 처음 딱 한 번만 호출된다. (아래 둘 중 하나일 경우 생성됨)
        // 1. 해당 클래스에 있는 인스턴스에 접근할 때
        // 2, 해당 클래스로 접근할 때 (ex. DataManager.Books)
        // 책 6단원 중에 있다.

        static DataManager()
        {
            Load();
        }

        public static void Load()
        {
            try
            {
                // ReadAllText : 파일에 있는 글자들을 전부 읽어들인 것
                string booksOutput = File.ReadAllText("./Books.xml");
                XElement bx = XElement.Parse(booksOutput);
                Books.Clear();
                // foreach 루프 방식
                foreach (var item in bx.Descendants("book"))
                {
                    Book book = new Book();
                    book.ISBN = item.Element("ISBN").Value;
                    book.도서명 = item.Element("도서명").Value;
                    book.분류 = item.Element("분류").Value;
                    book.저자 = item.Element("저자").Value;
                    book.설명 = item.Element("설명").Value;
                    book.출판사 = item.Element("출판사").Value;
                    book.출판일 = item.Element("출판일").Value;
                    book.보유권수 = int.Parse(item.Element("보유권수").Value);
                    book.등록일 = DateTime.Parse(item.Element("등록일").Value);
                    book.대여상태 = item.Element("대여상태").Value != "0" ? true : false;
                    book.대여일 = DateTime.Parse(item.Element("대여일").Value);
                    book.빌린ID = item.Element("빌린ID").Value;
                    book.빌린이름 = item.Element("빌린이름").Value;
                    Books.Add(book);
                }

                string usersOutput = File.ReadAllText("./Users.xml");
                XElement ux = XElement.Parse(usersOutput);
                // LINQ 방식
                Users = (from item in ux.Descendants("user")
                    select new User()
                    {
                    ID = item.Element("ID").Value,
                    이름 = item.Element("이름").Value,
                    연락처 = item.Element("연락처").Value,
                    등록일 = DateTime.Parse(item.Element("등록일").Value)
                    }).ToList<User>();
            }

            catch (Exception)
            {
                // Save가 제대로 안 되서 Load가 실패하면 무한호출되어 StackOverFlow가 날 수 있다.
                Save();
                Load();
            }
        }
        public static void Save()
        {

        }
    }
}
