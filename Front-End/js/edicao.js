const editarRegistro = async (tipo, id, data) => {
  try {
    await fetchData(`/${tipo}/editar/${id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(data),
    });
    alert(`${tipo} editado com sucesso!`);
  } catch (error) {
    alert(`Erro ao editar ${tipo}.`);
  }
};

document
  .getElementById("editarIngredienteForm")
  .addEventListener("submit", async (e) => {
    e.preventDefault();

    const id = document.getElementById("idIngrediente").value;

    const data = {
      nome: document.getElementById("nomeIngrediente").value,
      descricao: document.getElementById("descricaoIngrediente").value,
      unidadeMedida: document.getElementById("unidadeMedidaIngrediente").value,
      valorPago: parseFloat(
        document.getElementById("valorPagoIngrediente").value
      ),
      quantidadeEstoque: parseInt(
        document.getElementById("quantidadeEstoqueIngrediente").value
      ),
      ativo: true,
      marca: document.getElementById("marcaIngrediente").value,
    };

    await editarRegistro("ingredientes", id, data);
  });

document
  .getElementById("editarItemForm")
  .addEventListener("submit", async (e) => {
    e.preventDefault();

    const id = document.getElementById("idItem").value;

    const data = {
      nome: document.getElementById("nomeItem").value,
      descricao: document.getElementById("descricaoItem").value,
      unidadeMedida: document.getElementById("unidadeMedidaItem").value,
      valorPago: parseFloat(document.getElementById("valorPagoItem").value),
      quantidadeEstoque: parseInt(
        document.getElementById("quantidadeEstoqueItem").value
      ),
      ativo: true,
      marca: document.getElementById("marcaItem").value,
    };

    await editarRegistro("itens", id, data);
  });

document
  .getElementById("editarProdutoForm")
  .addEventListener("submit", async (e) => {
    e.preventDefault();

    const id = document.getElementById("idProduto").value;

    const data = {
      nome: document.getElementById("nomeProduto").value,
      descricao: document.getElementById("descricaoProduto").value,
      unidadeMedida: document.getElementById("unidadeMedidaProduto").value,
      valorPago: parseFloat(document.getElementById("valorPagoProduto").value),
      valorVenda: parseFloat(
        document.getElementById("valorVendaProduto").value
      ),
      quantidadePorcao: parseInt(
        document.getElementById("quantidadePorcaoProduto").value
      ),
      ativo: true,
      composicao: [],
    };

    await editarRegistro("produtos", id, data);
  });
