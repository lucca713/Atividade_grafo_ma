using System;
using System.Collections.Generic;
using System.IO;
class Vertice
{
    public int Id;
    public string Valor;//valor que vai estar no nó
    
    public Vertice(int id, string valor)
    {
        Id = id;
        Valor = valor;

    }

}

class Aresta
{
    public int Origem;
    public int Destino;
    public string Valor; //peso oq a aresta vai levar de um no para outro , info 
    
    public Aresta(int origem, int destino, string valor)
    {
        Origem = origem;
        Destino = destino;
        Valor = valor; // valor levado na aresta, esse valor que deve ser mudado
    }

}
class Grafo
    {
        //aqui estou criando listar do tipo vertice e aresta para colocar os elesmento de arestas e vertices
        public List<Vertice> Vertices = new List<Vertice>();
        public List<Aresta> Arestas = new List<Aresta>();


    //inserir meio untuitivo
        public Vertice insertVertex(string o)
        {
            int id = Vertices.Count;
            Vertice nova = new Vertice(id, o);
            Vertices.Add(nova);
            return nova;
        }
    //inserir meio untuitivo
    public Aresta insertEdge(int v, int w, string o)
        {

            Aresta nova = new Aresta(v, w, o);
            Arestas.Add(nova);
            return nova;

        }

    //aqui estou colocando a vertize o valor dela tipo ela é 1 e o peso dela, tipo chave e valor
      /*
        public void AdicionaVertice(string valor){
                int id = Vertices.Count;
                Vertices.Add(new Vertice(id, valor));
            }
        
            //aqui estou adcionando aresta, dando uma rota para ele indicando que ela vai levar um valor
            public void AdicionarAresta(int origem, int destino, string valor)
            {
                Arestas.Add( new Aresta(origem, destino, valor));
        }
        */
        //vai retornar as duas vertices que sao ligados pela aresta
        public (Vertice, Vertice) endVertices(Aresta e)
        {
            Vertice inicio = Vertices[e.Origem];

            Vertice destino = Vertices[e.Destino];

            return (inicio, destino);

            }
    //pesquisar essa merda do caralho de null pq n vai essa merda depois
            public Vertice opposite(Vertice v, Aresta e){
                if (e.Origem == v.Id) {
                    return Vertices[e.Destino];

                } else if(v.Id == e.Destino){
                    return Vertices[e.Origem];
            }else{ 
                return null; 
            }           
        }

        public bool areAdjacente(Vertice v, Vertice w)
        {
            //entao aqui percorrendo a lista de arestas
            foreach (Aresta e in Arestas)
            {
                if ((e.Origem == v.Id && e.Destino == w.Id) || (e.Origem == w.Id && e.Destino != v.Id))
                {
                    return true; //achou a adjacencia
                }
            }

            return false; //nao achou adjancencia
        }

        //so troca simples -> mais facil de entender
        public void replaceEdge(Aresta e, string o){
            e.Valor = o;
        }

        //mesmas coisa, troca simples facil de entender -> perguntar para acconcia depois, parece que meio tudo com ponteiro ao mesmo tempo nao(muito estranho)
        public void replaceVertex(Vertice v, string o) { 
            v.Valor = o;
        }

        public string removeVertexa(Vertice v) {
            //remove todas as aresta e vertices pois ao remover vertices as arestas iam ficar soltar
            Arestas.RemoveAll(e => e.Origem == v.Id || e.Destino == v.Id);
           
            Vertices.Remove(v);

            return v.Valor;
        }

        public string removeEdge(Aresta e)
        {
         Arestas.Remove(e);

           return e.Valor;

        }
        //retorna valor da aresta
        public string edgeValue(Aresta e)
        {
            return e.Valor;
        }
        //retorna valor da vertive
        public string VertexValue(Vertice v)
        {
            return v.Valor;
        }

    public void CarregarArquivoTxt(string caminhoArquivo)
    {
        string[] linhas = File.ReadAllLines(caminhoArquivo);
        int qtdVertices = int.Parse(linhas[0]);

        // Cria vértices
        for (int i = 0; i < qtdVertices; i++)
        {
            insertVertex(i.ToString()); // você pode mudar o valor se quiser algo além do ID
        }

        // Cria arestas
        for (int i = 1; i < linhas.Length; i++)
        {
            string[] partes = linhas[i].Split(' ');
            int origem = int.Parse(partes[0]) - 1; // o arquivo começa com 1, nosso código com 0
            int destino = int.Parse(partes[1]) - 1;

            insertEdge(origem, destino, "1"); // valor padrão "1" como peso, pode mudar depois
        }

        Console.WriteLine("Grafo carregado do arquivo com sucesso!");
    }

    public void MostrarGrafo()
    {
        Console.WriteLine("\n--- Vértices ---");
        foreach (Vertice v in Vertices)
            Console.WriteLine($"ID: {v.Id}, Valor: {v.Valor}");

        Console.WriteLine("\n--- Arestas ---");
        foreach (Aresta a in Arestas)
            Console.WriteLine($"({a.Origem} -> {a.Destino}) Valor: {a.Valor}");
    }
    public void Dijkstra(int origem, int destino)
    {
            int n = Vertices.Count;
            double[] dist = new double[n];
            int[] anterior = new int[n];
            bool[] visitado = new bool[n]; // vai marcar q ja foi visitado

            //inicializar elas
            for (int i = 0; i < n; i++)
            {
                dist[i] = double.PositiveInfinity;
                anterior[i] = -1;
                visitado[i] = false;
            }

            dist[origem] = 0;

        // PROCURAR vertice mais perto

        while (true)
        {
            int atual = -1;
            double menorDist = double.PositiveInfinity;
            for (int i = 0; i < n; i++)
            {
                if (!visitado[i] && dist[i] < menorDist)
                {
                    atual = i;
                    menorDist = dist[i];
                }
            }

            //aqui para para o e marcar o visitado
            if (atual == -1) {

                Console.WriteLine("nenhuma vertice pode ser ");
                break;
            }

            if (atual == destino) {
                Console.WriteLine("achou o destinho garoto");
            }

            visitado[atual] = true;

            foreach (var aresta in Arestas) {
                int vizinho = -1; //vizinho nao esta nas arestas, nao tem como chegar nele, para inicializar ele assim

                    if(aresta.Origem == atual){ 
                    vizinho = aresta.Destino;
                    
                } else if(aresta.Destino == atual)
                {
                    vizinho = aresta.Origem;
                }
                if (vizinho != -1 && !visitado[vizinho]) {
                    double peso = double.Parse(aresta.Valor);
                    double novaDist = dist[atual] + peso;
                    //verifica a aresta
                    if (novaDist < dist[vizinho]) {
                        dist[vizinho] = novaDist;
                        anterior[vizinho] = atual; //att a distancia do vizinho


                    }
                }  

                    
            }

        }

        //mostrar o rsultado final

        if (dist[destino] == double.PositiveInfinity) { 

            Console.WriteLine("nao tem caminho ;-;");
            return;
        }

        // Reconstrói o caminho
        List<int> caminho = new List<int>();
        int v = destino;
        while (v != -1)
        {
            caminho.Insert(0, v);
            v = anterior[v];
        }

        Console.WriteLine("\n Menor caminho encontrado:");
        foreach (int id in caminho)
            Console.Write($"{id} ");

        Console.WriteLine($"\n Custo total: {dist[destino]}");



    }


}


class Program
{
    static void Main(string[] args)
    {
        Grafo grafo = new Grafo();
        bool rodando = true;

        while (rodando)
        {
            Console.WriteLine("\n==== MENU ====");
            Console.WriteLine("5 - Carregar grafo de arquivo (primeiro passo!) POR FAVOR VEJA SE NAO ESQUECEU DE COLOCAR OS PESSOS NA FRENTE DA IMPLEMENTACAO DO ARQUIVO, SEGUIR EXEPLO JA FEITO");
            Console.WriteLine("1 - Adicionar vértice");
            Console.WriteLine("2 - Adicionar aresta");
            Console.WriteLine("3 - Mostrar grafo");
            Console.WriteLine("4 - Ver se dois vértices são adjacentes");
            Console.WriteLine("6 - Substituir valor de vértice");
            Console.WriteLine("7 - Substituir valor de aresta");
            Console.WriteLine("8 - Ver valor de vértice");
            Console.WriteLine("9 - Ver valor de aresta");
            Console.WriteLine("10 - Ver vértices finais de uma aresta");
            Console.WriteLine("11 - Ver vértice oposto");
            Console.WriteLine("12 - Remover vértice");
            Console.WriteLine("13 - Remover aresta");
            Console.WriteLine("14 - Remover aresta");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");
            string op = Console.ReadLine();

            switch (op)
            {
                case "1":
                    Console.Write("Digite o valor do vértice: ");
                    string valorVertice = Console.ReadLine();
                    grafo.insertVertex(valorVertice);
                    break;

                case "2":
                    Console.Write("ID origem: ");
                    int origem = int.Parse(Console.ReadLine());
                    Console.Write("ID destino: ");
                    int destino = int.Parse(Console.ReadLine());
                    Console.Write("Valor da aresta: ");
                    string valorAresta = Console.ReadLine();
                    grafo.insertEdge(origem, destino, valorAresta);
                    break;

                case "3":
                    grafo.MostrarGrafo();
                    break;

                case "4":
                    Console.Write("ID do primeiro vértice: ");
                    int v1 = int.Parse(Console.ReadLine());
                    Console.Write("ID do segundo vértice: ");
                    int v2 = int.Parse(Console.ReadLine());
                    bool adj = grafo.areAdjacente(grafo.Vertices[v1], grafo.Vertices[v2]);
                    Console.WriteLine(adj ? "São adjacentes." : "Não são adjacentes.");
                    break;

                case "5":
                    Console.Write("Digite o caminho do arquivo: ");
                    string caminho = Console.ReadLine();
                    if (File.Exists(caminho))
                        grafo.CarregarArquivoTxt(caminho);
                    else
                        Console.WriteLine("Arquivo não encontrado.");
                    break;

                case "6":
                    Console.Write("ID do vértice: ");
                    int idV = int.Parse(Console.ReadLine());
                    Console.Write("Novo valor: ");
                    string novoV = Console.ReadLine();
                    grafo.replaceVertex(grafo.Vertices[idV], novoV);
                    break;

                case "7":
                    Console.Write("Índice da aresta: ");
                    int idE = int.Parse(Console.ReadLine());
                    Console.Write("Novo valor: ");
                    string novoE = Console.ReadLine();
                    grafo.replaceEdge(grafo.Arestas[idE], novoE);
                    break;

                case "8":
                    Console.Write("ID do vértice: ");
                    int idVV = int.Parse(Console.ReadLine());
                    Console.WriteLine("Valor: " + grafo.VertexValue(grafo.Vertices[idVV]));
                    break;

                case "9":
                    Console.Write("Índice da aresta: ");
                    int idEE = int.Parse(Console.ReadLine());
                    Console.WriteLine("Valor: " + grafo.edgeValue(grafo.Arestas[idEE]));
                    break;

                case "10":
                    Console.Write("Índice da aresta: ");
                    int idEA = int.Parse(Console.ReadLine());
                    var (vOrig, vDest) = grafo.endVertices(grafo.Arestas[idEA]);
                    Console.WriteLine($"Origem: {vOrig.Valor}, Destino: {vDest.Valor}");
                    break;

                case "11":
                    Console.Write("ID do vértice: ");
                    int idO = int.Parse(Console.ReadLine());
                    Console.Write("Índice da aresta: ");
                    int idOA = int.Parse(Console.ReadLine());
                    Vertice oposto = grafo.opposite(grafo.Vertices[idO], grafo.Arestas[idOA]);
                    Console.WriteLine(oposto != null ? $"Oposto: {oposto.Valor}" : "O vértice não pertence à aresta.");
                    break;

                case "12":
                    Console.Write("ID do vértice: ");
                    int idDelV = int.Parse(Console.ReadLine());
                    string valV = grafo.removeVertexa(grafo.Vertices[idDelV]);
                    Console.WriteLine($"Vértice '{valV}' removido.");
                    break;

                case "13":
                    Console.Write("Índice da aresta: ");
                    int idDelE = int.Parse(Console.ReadLine());
                    string valE = grafo.removeEdge(grafo.Arestas[idDelE]);
                    Console.WriteLine($"Aresta '{valE}' removida.");
                    break;
                case "14":
                    Console.Write("ID do vértice de origem: ");
                    int origemD = int.Parse(Console.ReadLine());
                    Console.Write("ID do vértice de destino: ");
                    int destinoD = int.Parse(Console.ReadLine());
                    grafo.Dijkstra(origemD, destinoD);
                    break;

                case "0":
                    rodando = false;
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }
}
