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
                ("LikePage",  typeof(LikePage)),
                ("DislikePage",typeof(DislikePage))
            };

        public void NavigateTo(string pageName)
        {
            var item = _pages.First(p => p.Tag.Equals(pageName));

            if (_currentPage == item.Page)
                return;

            _frame.Navigate(item.Page);

            _currentPage = item.Page;
        }
    }
}
