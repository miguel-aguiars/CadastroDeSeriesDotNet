using System;
using System.Collections.Generic;
using Cadastro_de_Séries.Interfaces;

namespace Cadastro_de_Séries.Interfaces
{
    public interface IRepositorio<T>
    {
        List<T> Lista();
        T RetornaPorId(int id);
        void Insere(T entidade);
        void Exclui(int id);
        void Atualiza(int id,T entidade);
        int ProximoId();
    }
}


namespace Cadastro_de_Séries
{

    public abstract class EntidadeBase
    {
        public int Id { get; protected set; }
    }

    public class SerieRepositorio : IRepositorio<Serie>
    {
        private List<Serie> listaSerie = new List<Serie>();
        public void Atualiza(int id, Serie objeto) // id é o indice do vetor da lista de séries
        {
            listaSerie[id] = objeto;
        }

        public void Exclui(int id)
        {
            listaSerie[id].Excluir();
        }

        public void Insere(Serie objeto)
        {
            listaSerie.Add(objeto);
        }

        public List<Serie> Lista()
        {
            return listaSerie;
        }

        public int ProximoId()
        {
            return listaSerie.Count;
        }

        public Serie RetornaPorId(int id)
        {
            return listaSerie[id];
        }
    }


    public class Serie: EntidadeBase // classe série herda da entidade base
    {
        // Atributos
        private Genero Genero { get; set; }

        private string Titulo { get; set; }

        private string Descricao { get; set; }
        
        private int Ano { get; set; }

        private bool Excluido { get; set; }

    // Métodos

    public Serie(int id, Genero genero, string titulo, string descricao, int ano)
    {
        this.Id = id;
        this.Genero = genero;
        this.Titulo = titulo;
        this.Descricao = descricao;
        this.Ano = ano;
        this.Excluido = false;
    }

        public override string ToString()
        {
            string retorno = "";
            retorno += "Gênero: " + this.Genero + Environment.NewLine;
            retorno += "Título: " + this.Titulo + Environment.NewLine;
            retorno += "Descrição: " + this.Descricao + Environment.NewLine;
            retorno += "Ano de Início: " + this.Ano + Environment.NewLine;
            retorno += "Excluído: " + this.Excluido;
            return retorno;
        }

        public string retornaTitulo()
        {
            return this.Titulo;
        }

        public int retornaId()
        {
            return this.Id;
        }

        public bool retornaExcluido()
        {
            return this.Excluido;
        }

        public void Excluir()
        {
            this.Excluido = true;
        }
    }

    public enum Genero
    {
        Acao = 1,

        Aventura = 2,
            
        Comedia = 3,

        Esporte = 4, 

        Documentario = 5,

        Fantasia = 6, 

        Biblico = 7,

        Espionagem = 8,

        Drama = 9, 

        Suspense = 10,

        Policial = 11,

        Terror = 12,

        Animes = 13, 

    }



    class Program
{
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            

            string OpcaoUsuario = ObterOpcaoUsuario();
            while(OpcaoUsuario.ToUpper() != "X")
            {
                switch (OpcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSeries();
                        break;
                    case "3":
                        AtualizarSeries();
                        break;
                    case "4":
                        ExcluirSeries();
                        break;
                    case "5":
                        VisualizarSeries();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();

                }

                OpcaoUsuario = ObterOpcaoUsuario();
            }
            Console.WriteLine("Obrigado por utilizar os nossos serviços");
            Console.ReadLine();

        }

        private static void ListarSeries()
        {
            Console.WriteLine("Listar Series");
            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada.");
                return;
            }
            foreach(var serie in lista)
            {
                var excluido = serie.retornaExcluido();

                 Console.WriteLine("#ID {0} : - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Excluido" : ""));

            }
        }

        private static void InserirSeries()
        {
            Console.WriteLine("Inserir nova serie");


            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
            }
            Console.WriteLine("Digite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.Write("Digite o Título da Série: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("Digite o Ano de Início da Série: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.Write("Digite a Descrição da Série: ");
            string entradaDescricao = Console.ReadLine();

            Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorio.Insere(novaSerie);
        }

        private static void AtualizarSeries()
        {
            Console.WriteLine("Digite o ID da série: ");
            int IndiceSerie = int.Parse(Console.ReadLine());

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
            }
            Console.WriteLine("Digite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.Write("Digite o Título da Série: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("Digite o Ano de Início da Série: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.Write("Digite a Descrição da Série: ");
            string entradaDescricao = Console.ReadLine();

            Serie AtualizaSerie = new Serie(id: IndiceSerie,
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorio.Atualiza(IndiceSerie, AtualizaSerie);
        }

        private static void ExcluirSeries()
        {
            Console.WriteLine("Digite o id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            repositorio.Exclui(indiceSerie);
        }

        private static void VisualizarSeries()
        {
            Console.WriteLine("Digite o id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            var serie = repositorio.RetornaPorId(indiceSerie);

            Console.Write(serie);
        }


        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("Seja bem vindo a nossa plataforma de cadastro de Séries!");
            Console.WriteLine("Informe a opção desejada:");
            Console.WriteLine();
            Console.WriteLine("1 - Listar séries");
            Console.WriteLine("2 - Inserie nova série");
            Console.WriteLine("3 - Atualizar série");
            Console.WriteLine("4 - Excluir série");
            Console.WriteLine("5 - Visualizar série");
            Console.WriteLine("C - Limpar tela");
            Console.WriteLine("X - Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }

}
}
