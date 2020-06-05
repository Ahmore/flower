using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewPage1 : ContentPage
    {
        public ObservableCollection<Flower> Items { get; set; }
        public Flower SelectedFlower { get; set; }
        public FlowersDatabase Database { get; }

        public ListViewPage1()
        {
            InitializeComponent();
            Database = new FlowersDatabase();
            var result = Database.GetItemsAsync().Result;
            Items = new ObservableCollection<Flower>(result);


            MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            SelectedFlower = (Flower)e.Item;
        }
        async private void onEdit1(object sender, EventArgs e)
        {

            if (SelectedFlower == null)
                return;

            var flower = SelectedFlower;

            string result = await DisplayPromptAsync("Changing name", "Fill new name");
            int index = Items.IndexOf(flower);
            var newFlower = new Flower
            {
                ID = flower.ID,
                Name = result,
                ImageUrl = flower.ImageUrl
            };

            Items.Remove(flower);
            Items.Add(newFlower);
            int index2 = Items.IndexOf(newFlower);
            Items.Move(index2, index);

            await Database.SaveItemAsync(newFlower);
        }
        private void onEdit2(object sender, EventArgs e)
        {
            if (SelectedFlower == null)
                return;

            Navigation.PushAsync(new Page1(this));
        }

        async private void onAdd(object sender, EventArgs e)
        {
            var num = new Random().Next(1000000);
            var flower = new Flower
            {
                ID = 0,
                Name = "Flower " + num,
                ImageUrl = "https://haft-wzory.pl/wp-content/uploads/2018/10/kwiatek.jpg"
            };
            await Database.SaveItemAsync(flower);
            var result = Database.GetItemsAsync().Result;
            Items = new ObservableCollection<Flower>(result);
            MyListView.ItemsSource = null;
            MyListView.ItemsSource = Items;
            Console.WriteLine(Items.Count);
            //OnPropertyChanged(nameof(Items));
        }
    }
}
