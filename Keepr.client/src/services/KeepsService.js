import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class KeepsService {
  async getKeeps() {
    const res = await api.get(`/api/keeps`);
    logger.log("Getting keeps", res.data);
    AppState.keeps = res.data;
  }

  async getById(id) {
    const res = await api.get(`/api/keeps/${id}`);
    logger.log("Getting keep by Id", res.data);
    AppState.activeKeep = res.data;

    // let activeKeep = AppState.keeps;
    // let vaultKeeps = AppState.vaultKeeps;
    // activeVaultKeep = activeKeep.concat(vaultKeeps);

    // AppState.activeVaultKeep = res.data;
    console.log("what is an activeVaultKeep", res.data);
    // AppState.vaultKeeps = res.data;
  }

  async create(keep) {
    const res = await api.post(`/api/keeps`, keep);
    logger.log("Created keep", res.data);
    AppState.profileKeeps.push(res.data);
    AppState.keeps.unshift(res.data);
  }

  async update(keep) {
    const res = await api.put(`/api/keeps/` + keep.id, keep);
    logger.log("Updated Keep", res.data);
    AppState.activeKeep = res.data;
  }

  async deleteKeep(id) {
    const res = await api.delete(`/api/keeps/${id}`);
    logger.log("Deleting keep", res.data);
    AppState.profileKeeps = AppState.profileKeeps.filter((k) => k.id != id);
    AppState.keeps = AppState.keeps.filter((k) => k.id != id);
  }

  async searchKeeps(searchTerm) {
    const res = await api.get(`/api/keeps`, {
      params: { query: searchTerm },
    });
    AppState.keeps = res.data.keeps;
  }
}

export const keepsService = new KeepsService();
