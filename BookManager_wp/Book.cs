using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager_wp
{
    public class Book
    {
        public string ISBN { get; set; }
        public string 도서명 { get; set; }
        public string 분류 { get; set; }
        public string 저자 { get; set; }
        public string 설명 { get; set; }
        public string 출판사 { get; set; }
        public string 출판일 { get; set; }
        public int 보유권수 { get; set; }
        public DateTime 등록일 { get; set; }

        public string 빌린ID { get; set; }
        public string 빌린이름 { get; set; }
        public bool 대여상태 { get; set; }
        public DateTime 대여일 { get; set; }
    }
}
