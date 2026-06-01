using Microsoft.Maui.Controls;
using App_Viatura_SCV.ViewModels;

namespace App_Viatura_SCV.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // Avisa para a tela que as regras e comandos estão no ViewModel
            BindingContext = new PainelViewModel();
        }
    }
}