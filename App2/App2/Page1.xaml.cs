using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        private ListViewPage1 vm;
        public Page1(ListViewPage1 _vm)
        {
            InitializeComponent();

            BindingContext = vm = _vm;

            
        }

        async public void OnNameChange(object sender, EventArgs e)
        {
            var newName = ((Entry)sender).Text;
            var items = vm.Items;

            var flower = vm.SelectedFlower;

            int index = vm.Items.IndexOf(flower);
            var newFlower = new Flower
            {
                ID = flower.ID,
                Name = newName,
                ImageUrl = flower.ImageUrl
            };

            vm.Items.Remove(flower);
            vm.Items.Add(newFlower);
            int index2 = vm.Items.IndexOf(newFlower);
            vm.Items.Move(index2, index);

            await vm.Database.SaveItemAsync(newFlower);

            Navigation.PopAsync();
        }
    }
}