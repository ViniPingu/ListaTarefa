namespace ListaTarefas.Models
{
    public class Filtros
    {

        public Filtros(string filtroString)
        {

            FiltroString = filtroString ?? "todos-todos-todos";
            string[] filtros = FiltroString.Split('-');

            CategoriaID = filtros[0];
            Vencimento = filtros[1];
            StatusID = filtros[2];

        }

        public string FiltroString { get; set; }

        public string CategoriaID { get; set; }

        public string StatusID { get; set; }    

        public string Vencimento { get; set; }


        public bool TemCategoria => CategoriaID.ToLower() != "todos";

        public bool TemVencimento => Vencimento.ToLower() != "todos";

        public bool TemStatus => StatusID.ToLower() != "todos";


        public static Dictionary<string, string> VencimentoValoresFiltro =>
            new Dictionary<string, string>
            {
                {"futuro", "Futuro"},

                {"passado", "Passado"},

                {"hoje" , "Hoje"}

            };


        public bool Epassado => Vencimento.ToLower() == "passado";

        public bool EFuturo => Vencimento.ToLower() == "futuro";

        public bool EHoje => Vencimento.ToLower() == "hoje";
    }
}
