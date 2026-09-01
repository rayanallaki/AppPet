namespace AppPet.Views;

public partial class FrmPrincipal : ContentPage
{
    public FrmPrincipal()
    {
        InitializeComponent();
    }

   //	public async void BtnLogin_Clicked(object sender, EventArgs e)
   //  {
   //    await Navigation.PushAsync(new FrmLogin());
   // }

    private void ButtonLogin(object sender, EventArgs e)
    {
        Navigation.PushAsync(new FrmAgendamento());
    }

    private void ButtonCadastro(object sender, EventArgs e)
    {
        Navigation.PushAsync(new FrmCadUsuario());
    }
}