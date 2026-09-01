namespace AppPet.Views;

public partial class FrmCadUsuario : ContentPage
{
    public FrmCadUsuario()
    {
        InitializeComponent();
    }

    //	private async void BtnCadastrar(object? sender, EventArgs e)
    //	{ 
    //	DisplayAlert("Cadastro", "Usuário cadastrado com sucesso!", "OK");
//		Navigation.PopAsync();
  //  }

    private void ButtonCadastrar(object sender, EventArgs e)
    {
        DisplayAlert("Cadastro", "Usuário cadastrado com sucesso!", "OK");
        Navigation.PopAsync();
    }
}