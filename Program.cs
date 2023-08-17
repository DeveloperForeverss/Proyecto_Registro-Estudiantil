using System;
using System.Collections.Generic;
using System.IO;

namespace SistemaRegistroEstudiantil
{
    class Estudiante
    {
        public string Matricula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public char Sexo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Curso { get; set; }
        public List<string> Materias { get; set; } = new List<string>();
        public List<double> Notas { get; set; } = new List<double>();
    }

    class Program
    {
        static List<Estudiante> estudiantes = new List<Estudiante>();

        static void Main(string[] args)
        {
            MenuPrincipal();
        }

        static void MenuPrincipal()
        {
            while (true)
            {
                Console.WriteLine("===== Menú Principal =====");
                Console.WriteLine("1. Crear Estudiante");
                Console.WriteLine("2. Ver Estudiantes");
                Console.WriteLine("3. Eliminar Estudiante");
                Console.WriteLine("4. Digitar Notas");
                Console.WriteLine("5. Ver Reportes");
                Console.WriteLine("6. Salir");

                Console.Write("Seleccione una opción: ");
                int opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        CrearEstudiante();
                        break;
                    case 2:
                        VerEstudiantes();
                        break;
                    case 3:
                        EliminarEstudiante();
                        break;
                    case 4:
                        DigitarNotas();
                        break;
                    case 5:
                        VerReportes();
                        break;
                    case 6:
                        Console.WriteLine("Saliendo del sistema...");
                        return;
                    default:
                        Console.WriteLine("Opción inválida. Intente nuevamente.");
                        break;
                }
            }
        }

        static void CrearEstudiante()
        {
            Estudiante nuevoEstudiante = new Estudiante();

            Console.WriteLine("===== Crear Estudiante =====");
            Console.Write("Matrícula: ");
            nuevoEstudiante.Matricula = Console.ReadLine();

            Console.Write("Nombre: ");
            nuevoEstudiante.Nombre = Console.ReadLine();

            Console.Write("Apellido: ");
            nuevoEstudiante.Apellido = Console.ReadLine();

            Console.Write("Edad: ");
            nuevoEstudiante.Edad = Convert.ToInt32(Console.ReadLine());

            Console.Write("Sexo (M/F): ");
            nuevoEstudiante.Sexo = Convert.ToChar(Console.ReadLine());

            Console.Write("Fecha de Nacimiento (dd/mm/aaaa): ");
            nuevoEstudiante.FechaNacimiento = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

            Console.Write("Curso: ");
            nuevoEstudiante.Curso = Console.ReadLine();

            Console.Write("Cantidad de Materias: ");
            int cantidadMaterias = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < cantidadMaterias; i++)
            {
                Console.Write($"Nombre de Materia {i + 1}: ");
                string materia = Console.ReadLine();
                nuevoEstudiante.Materias.Add(materia);
            }

            estudiantes.Add(nuevoEstudiante);

            Console.WriteLine("Estudiante creado exitosamente.");
        }

        static void VerEstudiantes()
        {
            Console.WriteLine("===== Ver Estudiantes =====");

            foreach (Estudiante estudiante in estudiantes)
            {
                Console.WriteLine($"Matrícula: {estudiante.Matricula}");
                Console.WriteLine($"Nombre: {estudiante.Nombre} {estudiante.Apellido}");
                Console.WriteLine($"Edad: {estudiante.Edad}");
                Console.WriteLine($"Sexo: {estudiante.Sexo}");
                Console.WriteLine($"Fecha de Nacimiento: {estudiante.FechaNacimiento.ToString("dd/MM/yyyy")}");
                Console.WriteLine($"Curso: {estudiante.Curso}");
                Console.WriteLine("Materias:");
                foreach (string materia in estudiante.Materias)
                {
                    Console.WriteLine($"- {materia}");
                }
                Console.WriteLine("===================================");
            }
        }

        static void EliminarEstudiante()
        {
            Console.WriteLine("===== Eliminar Estudiante =====");
            Console.Write("Ingrese la Matrícula del estudiante a eliminar: ");
            string matricula = Console.ReadLine();

            Estudiante estudianteAEliminar = estudiantes.Find(e => e.Matricula == matricula);

            if (estudianteAEliminar != null)
            {
                estudiantes.Remove(estudianteAEliminar);
                Console.WriteLine("Estudiante eliminado correctamente.");
            }
            else
            {
                Console.WriteLine("Estudiante no encontrado.");
            }
        }

        static void DigitarNotas()
        {
            Console.WriteLine("===== Digitar Notas =====");
            Console.Write("Ingrese la Matrícula del estudiante: ");
            string matricula = Console.ReadLine();

            Estudiante estudiante = estudiantes.Find(e => e.Matricula == matricula);

            if (estudiante != null)
            {
                Console.WriteLine($"Ingrese las notas para {estudiante.Nombre} {estudiante.Apellido}:");
                foreach (string materia in estudiante.Materias)
                {
                    Console.Write($"Nota para {materia}: ");
                    double nota = Convert.ToDouble(Console.ReadLine());
                    estudiante.Notas.Add(nota);
                }
                Console.WriteLine("Notas ingresadas correctamente.");
            }
            else
            {
                Console.WriteLine("Estudiante no encontrado.");
            }
        }

        static void VerReportes()
        {
            Console.WriteLine("===== Ver Reportes =====");
            Console.WriteLine("1. Estudiantes Mujeres");
            Console.WriteLine("2. Estudiantes Aprobados");
            Console.WriteLine("3. Estudiantes Reprobados");
            Console.Write("Seleccione una opción: ");
            int opcionReporte = Convert.ToInt32(Console.ReadLine());

            switch (opcionReporte)
            {
                case 1:
                    Console.WriteLine("===== Estudiantes Mujeres =====");
                    foreach (Estudiante estudiante in estudiantes)
                    {
                        if (estudiante.Sexo == 'F')
                        {
                            Console.WriteLine($"Nombre: {estudiante.Nombre} {estudiante.Apellido}");
                        }
                    }
                    break;
                case 2:
                    Console.WriteLine("===== Estudiantes Aprobados =====");
                    foreach (Estudiante estudiante in estudiantes)
                    {
                        double promedio = CalcularPromedio(estudiante.Notas);
                        if (promedio >= 6.0)
                        {
                            Console.WriteLine($"Nombre: {estudiante.Nombre} {estudiante.Apellido} - Promedio: {promedio}");
                        }
                    }
                    break;
                case 3:
                    Console.WriteLine("===== Estudiantes Reprobados =====");
                    foreach (Estudiante estudiante in estudiantes)
                    {
                        double promedio = CalcularPromedio(estudiante.Notas);
                        if (promedio < 6.0)
                        {
                            Console.WriteLine($"Nombre: {estudiante.Nombre} {estudiante.Apellido} - Promedio: {promedio}");
                        }
                    }
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        }

        static double CalcularPromedio(List<double> notas)
        {
            double suma = 0.0;
            foreach (double nota in notas)
            {
                suma += nota;
            }
            return suma / notas.Count;
        }
    }
}
