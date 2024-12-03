const fetchData = async (endpoint) => {
  try {
    const response = await fetch(`http://localhost:54090/api${endpoint}`);
    if (!response.ok) {
      throw new Error(`Erro na requisição: ${response.status}`);
    }
    return await response.json();
  } catch (error) {
    console.error("Erro ao buscar dados:", error);
    throw error;
  }
};

const listarRegistros = async (tipo) => {
  try {
    const registros = await fetchData(`/${tipo}`);
    const tabelaCorpo = document.querySelector(`#${tipo}Tabela tbody`);

    tabelaCorpo.innerHTML = "";

    registros.forEach((registro) => {
      const row = document.createElement("tr");

      if (tipo === "ingredientes" || tipo === "itensExtras") {
        row.innerHTML = `
          <td>${registro.nome}</td>
          <td>${registro.descricao}</td>
          <td>${registro.unidadeMedida}</td>
          <td>${registro.quantidadeEstoque}</td>
          <td>${registro.valorPago}</td>
          <td>${registro.marca}</td>
          <td class="action-buttons">
            <button class="edit-btn" onclick="window.location.href='./edicao.html?tipo=${tipo}&id=${registro.id}'">Editar</button>
            <button class="delete-btn" onclick="window.location.href='./exclusao.html?tipo=${tipo}&id=${registro.id}'">Excluir</button>
          </td>
        `;
      } else if (tipo === "produtos") {
        row.innerHTML = `
          <td>${registro.nome}</td>
          <td>${registro.descricao}</td>
          <td>${registro.unidadeMedida}</td>
          <td>${registro.valorVenda}</td>
          <td>${registro.quantidadePorcao}</td>
          <td class="action-buttons">
            <button class="edit-btn" onclick="window.location.href='./edicao.html?tipo=${tipo}&id=${registro.id}'">Editar</button>
            <button class="delete-btn" onclick="window.location.href='./exclusao.html?tipo=${tipo}&id=${registro.id}'">Excluir</button>
          </td>
        `;
      }

      tabelaCorpo.appendChild(row);
    });
  } catch (error) {
    alert(`Erro ao obter a lista de ${tipo}.`);
  }
};

document.addEventListener("DOMContentLoaded", () => {
  document
    .getElementById("obterIngredientes")
    .addEventListener("click", () => listarRegistros("ingredientes"));

  document
    .getElementById("obterItensExtras")
    .addEventListener("click", () => listarRegistros("itensExtras"));

  document
    .getElementById("obterProdutos")
    .addEventListener("click", () => listarRegistros("produtos"));
});
