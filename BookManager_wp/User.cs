using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager_wp
{
    public class User
    {
        // 아이디랑 이름으로 유저 정보를 구분하기 위함.
        public string ID { get; set; }
        public string 이름 { get; set; }
        public string 연락처 { get; set; }
        public DateTime 등록일 { get; set; }
    }
}
