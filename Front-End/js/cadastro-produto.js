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

// Preencher seletores de ingredientes e itens extras ao carregar a página
const preencherSeletores = async () => {
  try {
    const ingredientes = await fetchData("/ingredientes");
    const itensExtras = await fetchData("/itensExtras");

    window.ingredientesList = ingredientes;
    window.itensExtrasList = itensExtras;
  } catch (error) {
    alert("Erro ao preencher seletores de ingredientes e itens extras.");
  }
};

// Adicionar um novo ingrediente ao produto
document
  .getElementById("adicionarIngrediente")
  .addEventListener("click", () => {
    const container = document.getElementById("ingredientesContainer");

    const select = document.createElement("select");
    select.classList.add("ingredienteSelect");
    window.ingredientesList.forEach((ingrediente) => {
      const option = document.createElement("option");
      option.value = ingrediente.id;
      option.textContent = `${ingrediente.nome} - ${ingrediente.marca}`;
      select.appendChild(option);
    });

    const unidadeMedidaSelect = document.createElement("select");
    unidadeMedidaSelect.classList.add("unidadeMedidaSelect");
    ["Gramas", "Quilogramas", "Mililitros", "Litros"].forEach((unidade) => {
      const option = document.createElement("option");
      option.value = unidade;
      option.textContent = unidade;
      unidadeMedidaSelect.appendChild(option);
    });

    const quantidadeInput = document.createElement("input");
    quantidadeInput.type = "number";
    quantidadeInput.placeholder = "Quantidade";
    quantidadeInput.classList.add("ingredienteQuantidade");

    container.appendChild(select);
    container.appendChild(unidadeMedidaSelect);
    container.appendChild(quantidadeInput);
  });

// Adicionar um novo item extra ao produto
document.getElementById("adicionarItemExtra").addEventListener("click", () => {
  const container = document.getElementById("itensExtrasContainer");

  const select = document.createElement("select");
  select.classList.add("itemExtraSelect");
  window.itensExtrasList.forEach((item) => {
    const option = document.createElement("option");
    option.value = item.id;
    option.textContent = `${item.nome} - ${item.marca}`;
    select.appendChild(option);
  });

  const unidadeMedidaSelect = document.createElement("select");
  unidadeMedidaSelect.classList.add("unidadeMedidaSelect");
  ["Gramas", "Quilogramas", "Mililitros", "Litros"].forEach((unidade) => {
    const option = document.createElement("option");
    option.value = unidade;
    option.textContent = unidade;
    unidadeMedidaSelect.appendChild(option);
  });

  const quantidadeInput = document.createElement("input");
  quantidadeInput.type = "number";
  quantidadeInput.placeholder = "Quantidade";
  quantidadeInput.classList.add("itemExtraQuantidade");

  container.appendChild(select);
  container.appendChild(unidadeMedidaSelect);
  container.appendChild(quantidadeInput);
});

// Enviar os dados do formulário de cadastro de produtos
document.getElementById("produtoForm").addEventListener("submit", async (e) => {
  e.preventDefault();

  const produto = {
    nome: document.getElementById("nomeProduto").value,
    descricao: document.getElementById("descricaoProduto").value,
    custoVariavel: parseFloat(
      document.getElementById("custoVariavelProduto").value
    ),
    impostos: parseFloat(document.getElementById("impostosProduto").value),
    taxaCartao: parseFloat(document.getElementById("taxaCartaoProduto").value),
    ativo: true,
    composicao: [],
  };

  // Adicionar ingredientes e itens extras à composição
  document.querySelectorAll(".ingredienteSelect").forEach((select, index) => {
    const quantidade = document.querySelectorAll(".ingredienteQuantidade")[
      index
    ].value;
    const unidadeMedida = document.querySelectorAll(".unidadeMedidaSelect")[
      index
    ].value;

    produto.composicao.push({
      itemId: select.value,
      tipoItem: "Ingrediente",
      unidadeMedida: unidadeMedida,
      quantidade: parseFloat(quantidade),
    });
  });

  document.querySelectorAll(".itemExtraSelect").forEach((select, index) => {
    const quantidade = document.querySelectorAll(".itemExtraQuantidade")[index]
      .value;
    const unidadeMedida = document.querySelectorAll(".unidadeMedidaSelect")[
      index
    ].value;

    produto.composicao.push({
      itemId: select.value,
      tipoItem: "ItemExtra",
      unidadeMedida: unidadeMedida,
      quantidade: parseFloat(quantidade),
    });
  });

  // Calcular ValorPago com base na composição
  let valorPago = 0;
  produto.composicao.forEach((item) => {
    const itemData =
      item.tipoItem === "Ingrediente"
        ? window.ingredientesList.find((i) => i.id === item.itemId)
        : window.itensExtrasList.find((i) => i.id === item.itemId);
    valorPago += itemData.valorPago * item.quantidade;
  });

  // Adicionando custo variável, impostos e taxa de cartão ao valorPago
  const impostos = (valorPago * produto.impostos) / 100;
  const taxaCartao = (valorPago * produto.taxaCartao) / 100;
  produto.valorPago = valorPago + impostos + taxaCartao + produto.custoVariavel;

  // Calcular ValorVenda com margem de lucro de 50%
  produto.valorVenda = produto.valorPago * 1.5;

  try {
    await fetchData("/produtos/cadastrar", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(produto),
    });
    alert("Produto cadastrado com sucesso!");
  } catch (error) {
    alert("Erro ao cadastrar produto.");
  }
});

// Preencher os seletores ao carregar a página
document.addEventListener("DOMContentLoaded", preencherSeletores);
