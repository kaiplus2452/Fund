using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class FundValue
    {

        public string decreaseDay = "16";       //扣款日
        public double decreaseMoney = 5000;     //扣款值
        public double rate = 1;                 //匯率
        public string cur = "TWD";              //幣別
        public double count = 0;                //庫存單位
        public double allMoney = 0;             //投資總額
        public double netValue = 0;             //參考淨值(每點價值)
        public double percent = 0;              //報酬率
        public double profit = 0;               //損益(獲利)
        public string strUrl = "";

    }
}
