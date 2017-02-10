using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WydatkiDomowe
{
    /// <summary>
    /// Interaction logic for YearlyRaportWindow.xaml
    /// </summary>
    public partial class YearlyRaportWindow : Window
    {
        private CollectionToView<YearlyRaport> collectionYearlyRaport;
        private BillsBaseDataContext dateBase;

        public YearlyRaportWindow(BillsBaseDataContext db)
        {
            dateBase = db;
            InitializeComponent();
            collectionYearlyRaport = new CollectionToView<YearlyRaport>(db);
            loadDateToWindow();
        }

        private void loadDateToWindow()
        {
            collectionYearlyRaport.LoadCollection();
        }
    }
}
