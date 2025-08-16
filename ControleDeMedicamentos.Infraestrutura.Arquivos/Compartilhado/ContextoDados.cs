using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado
{
    public class ContextoDados
    {
        public List<Funcionario> Funcionarios { get; set; } = new List<Funcionario>();

        private string pastaArmazenamento = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "ControleDeMedicamentos");

        private string arquivoArmazenamento = "dados.json";

        public ContextoDados() { }

        public ContextoDados(bool carregarDados)
        {
            if (carregarDados) Carregar();
        }

        public void Salvar()
        {
            string caminho = CaminhoCompleto();

            JsonSerializerOptions opcoesJson = OpcoesJson();

            string jsonString = JsonSerializer.Serialize(this, opcoesJson);

            if (!Path.Exists(pastaArmazenamento))
                Directory.CreateDirectory(pastaArmazenamento);

            File.WriteAllText(caminho, jsonString);
        }

        public void Carregar()
        {
            string caminho = CaminhoCompleto();

            if (!File.Exists(caminho)) return;

            string conteudoJson = File.ReadAllText(caminho);

            if (string.IsNullOrWhiteSpace(conteudoJson)) return;

            JsonSerializerOptions opcoesJson = OpcoesJson();

            ContextoDados contextoArmazenado = JsonSerializer.Deserialize<ContextoDados>(conteudoJson, opcoesJson);

            if (contextoArmazenado == null) return;

            Funcionarios = contextoArmazenado.Funcionarios;
        }

        public string CaminhoCompleto()
        {
            return Path.Combine(pastaArmazenamento, arquivoArmazenamento);
        }

        public JsonSerializerOptions OpcoesJson()
        {
            JsonSerializerOptions opcoesJson = new JsonSerializerOptions();
            opcoesJson.WriteIndented = true;
            opcoesJson.ReferenceHandler = ReferenceHandler.Preserve;

            return opcoesJson;
        }
    }
}
