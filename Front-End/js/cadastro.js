// Referência aos elementos HTML
const tipoCadastroSelect = document.getElementById("tipoCadastro");
const valorVendaInput = document.getElementById("valorVenda");
const quantidadePorcaoInput = document.getElementById("quantidadePorcao");

// Alterar visibilidade dos campos de acordo com o tipo de cadastro selecionado
tipoCadastroSelect.addEventListener("change", () => {
  const tipoSelecionado = tipoCadastroSelect.value;

  if (tipoSelecionado === "produtos") {
    valorVendaInput.style.display = "block";
    quantidadePorcaoInput.style.display = "block";
  } else {
    valorVendaInput.style.display = "none";
    quantidadePorcaoInput.style.display = "none";
  }
});

// Função para buscar dados da API
const fetchData = async (endpoint, options) => {
  try {
    const response = await fetch(
      `http://localhost:54090/api${endpoint}`,
      options
    );
    if (!response.ok) {
      throw new Error(`Erro na requisição: ${response.status}`);
    }
    return await response.json();
  } catch (error) {
    console.error("Erro ao buscar dados:", error);
    throw error;
  }
};

// Evento para enviar os dados do formulário de cadastro
document
  .getElementById("cadastroForm")
  .addEventListener("submit", async (e) => {
    e.preventDefault();

    const tipoCadastro = tipoCadastroSelect.value;

    // Dados comuns entre Ingredientes, Itens Extras e Produtos
    const registro = {
      nome: document.getElementById("nome").value,
      descricao: document.getElementById("descricao").value,
      unidadeMedida: document.getElementById("unidadeMedida").value,
      quantidadeEstoque: document.getElementById("quantidadeEstoque").value,
      valorPago: parseFloat(document.getElementById("valor").value),
      marca: document.getElementById("marca").value,
      ativo: true,
    };

    // Adicionar campos específicos para Produtos
    if (tipoCadastro === "produtos") {
      registro.valorVenda = parseFloat(valorVendaInput.value);
      registro.quantidadePorcao = parseInt(quantidadePorcaoInput.value);
      registro.composicao = []; // Pode ser preenchido posteriormente conforme necessidade
    }

    try {
      await fetchData(`/${tipoCadastro}/cadastrar`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(registro),
      });
      alert(
        `${
          tipoCadastro.charAt(0).toUpperCase() + tipoCadastro.slice(1)
        } cadastrado com sucesso!`
      );
    } catch (error) {
      alert(`Erro ao cadastrar ${tipoCadastro}.`);
    }
  });
