using aruizT5.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aruizT5
{
    public class PersonRepository
    {
        string _dbPath;
        private SQLiteConnection conn;
        public string statusMessage { get; set; }

        public void Init()
        {
            if (conn is not null)
                return;
            conn = new(_dbPath);
            conn.CreateTable<Persona>();
        }

        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public void AddNewPerson(string name)
        {
            int result = 0;
            try
            {
                Init();
                if (string.IsNullOrEmpty(name))
                    throw new Exception("El nombre es requerido");
                Persona person = new() { Name = name };
                result = conn.Insert(person);
                statusMessage = string.Format("Se inserto una persona", result, name);

            }
            catch (Exception ex)
            {
                statusMessage = string.Format("Error, no se inserto ", name, ex.Message);
            }
        }

        public List<Persona> GetAllPeople()
        {
            try
            {
                Init();
                return conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {

                statusMessage = string.Format("Error al mostrar los datos ", ex.Message);
            }
            return new List<Persona>();
        }

        public void DeletePerson(int personId)
        {
            try
            {
                Init();
                if (personId <= 0)
                    throw new Exception("El ID de la persona es inválido");

                // Aquí obtienes la persona a eliminar
                Persona personToDelete = conn.Table<Persona>().FirstOrDefault(p => p.Id == personId);

                if (personToDelete != null)
                {
                    // Elimina la persona de la base de datos
                    int rowsAffected = conn.Delete(personToDelete);

                    if (rowsAffected > 0)
                        statusMessage = string.Format("Se eliminó correctamente la persona");
                    else
                        statusMessage = string.Format("No se encontró la persona  para eliminar");
                }
                else
                {
                    statusMessage = string.Format("No se encontró la persona para eliminar");
                }
            }
            catch (Exception ex)
            {
                statusMessage = string.Format("Error al intentar eliminar la persona ", ex.Message);
            }
        }

        public void UpdatePersonName(int personId, string newName)
        {
            try
            {
                Init();
                if (personId <= 0)
                    throw new Exception("El ID de la persona es inválido");

                Persona personToUpdate = conn.Table<Persona>().FirstOrDefault(p => p.Id == personId);

                if (personToUpdate != null)
                {
                    personToUpdate.Name = newName;
                    int rowsAffected = conn.Update(personToUpdate);

                    if (rowsAffected > 0)
                        statusMessage = string.Format("Se actualizó correctamente el nombre de la persona" );
                    else
                        statusMessage = string.Format("No se pudo actualizar el nombre de la persona");
                }
                else
                {
                    statusMessage = string.Format("No se encontró la persona");
                }
            }
            catch (Exception ex)
            {
                statusMessage = string.Format("Error al intentar actualizar el nombre de la persona" );
            }
        }
    }


}