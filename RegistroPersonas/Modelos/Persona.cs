using System;
using System.Globalization;

namespace RegistroPersonas.Modelos
{
    public class Persona
    {
        public int dpi;
        public string nombre;
        public DateTime fechaNacimiento;

        public Persona()
        {
        }

        public void SetLine(string[] value)
        {
            this.dpi = int.Parse(value[0]);
            this.nombre = value[1];
            this.fechaNacimiento = Convert.ToDateTime(value[2]);
        }

        public void setDateOfBirth(string fecha) {

            fechaNacimiento = DateTime.ParseExact(fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        }

        public int Edad()
        {
            DateTime today = DateTime.Today;

            int age = today.Year - fechaNacimiento.Year;

            return age;
        }

        public string GetLine()
        {
            return dpi + "," + nombre + "," + fechaNacimiento.ToShortDateString();
        }
    }
}
