const excluirRegistro = async (tipo, id) => {
  try {
    await fetchData(`/${tipo}/excluir/${id}`, {
      method: "DELETE",
    });
    alert(`${tipo} excluído com sucesso!`);
  } catch (error) {
    alert(`Erro ao excluir ${tipo}.`);
  }
};

document
  .getElementById("excluirIngredienteForm")
  .addEventListener("submit", async (e) => {
    e.preventDefault();

    const id = document.getElementById("idIngredienteExcluir").value;
    await excluirRegistro("ingredientes", id);
  });

document
  .getElementById("excluirItemForm")
  .addEventListener("submit", async (e) => {
    e.preventDefault();

    const id = document.getElementById("idItemExcluir").value;
    await excluirRegistro("itens", id);
  });

document
  .getElementById("excluirProdutoForm")
  .addEventListener("submit", async (e) => {
    e.preventDefault();

    const id = document.getElementById("idProdutoExcluir").value;
    await excluirRegistro("produtos", id);
  });
