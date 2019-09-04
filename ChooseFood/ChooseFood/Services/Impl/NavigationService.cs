using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using ChooseFood.Views;
using FoodLibrary.Services;

namespace ChooseFood.Services.Impl
{
    public class NavigationService:INavigationService
    {
        private Frame _frame;

        /// <summary>
        /// 当前页。
        /// </summary>
        private Type _currentPage = typeof(MainPage);

        private string _foodName;

        public string FoodName() {
            return _foodName;
        }

        public void SetFrame(Frame frame)
        {
            _frame = frame;
        }

        private readonly List<(string Tag, Type Page)> _pages =
            new List<(string Tag, Type Page)>
            {
                ("MainPage",  typeof(MainPage)),
                ("今日推荐", typeof(MenuPage1)),
                ("历史记录", typeof(MenuPage2)),
                ("预览菜单", typeof(MenuPage3)),
                ("设置", typeof(MenuPage4)),
                ("数据同步",typeof(MenuPage5)),
                ("LikePage",  typeof(LikePage)),
                ("FirstPage",typeof(FirstPage)),
                ("ImagePage",typeof(ImagePage)),
                ("DislikePage",typeof(DislikePage)),
                ("LoginPromptPage",typeof(LoginPromptPage))
            };

        public void NavigateTo(string pageName,string foodName)
        {
            var item = _pages.First(p => p.Tag.Equals(pageName));

            if (_currentPage == item.Page)
                return;

            _frame.Navigate(item.Page);

            _currentPage = item.Page;

            _foodName = foodName;
        }
    }
}
