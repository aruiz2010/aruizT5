using aruizT5.Models;

namespace aruizT5.Views;

public partial class vPersona : ContentPage
{
	public vPersona()
	{
		InitializeComponent();
	}
    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        App.personRepo.AddNewPerson(txtPersona.Text);
        lblStatus.Text = App.personRepo.statusMessage;
    }

    private void btnObtener_Clicked(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        List<Persona> people = App.personRepo.GetAllPeople();
        Listapersonas.ItemsSource = people;
    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        if (int.TryParse(txtPersona.Text, out int personId))
        {
            App.personRepo.DeletePerson(personId);
            lblStatus.Text = App.personRepo.statusMessage;
        }
        else
        {
            lblStatus.Text = "Ingrese un ID válido para eliminar la persona";
        }
    }

    private void btnActualizar_Clicked(object sender, EventArgs e)
    {
        if (int.TryParse(txtIdPersonaUpdate.Text, out int personId) && !string.IsNullOrEmpty(txtNuevoNombre.Text))
        {
            App.personRepo.UpdatePersonName(personId, txtNuevoNombre.Text);
            lblStatusUpdate.Text = App.personRepo.statusMessage;
        }
        else
        {
            lblStatusUpdate.Text = "Ingrese un ID válido y un nuevo nombre para actualizar la persona";
        }
    }
}
