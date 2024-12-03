const apiBaseUrl = "http://localhost:54090/api";

const fetchData = async (endpoint, options = {}) => {
  try {
    const response = await fetch(`${apiBaseUrl}${endpoint}`, options);
    if (!response.ok) {
      throw new Error("Erro ao comunicar com a API.");
    }
    return await response.json();
  } catch (error) {
    console.error("Erro:", error);
    throw error;
  }
};
