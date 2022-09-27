import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class KeepsService {
  async getKeeps() {
    const res = await api.get(`/api/keeps`);
    // logger.log("Getting keeps", res.data);
    AppState.keeps = res.data;
  }

  async getById(id) {
    const res = await api.get(`/api/keeps/${id}`);
    // logger.log("Getting keep by Id", res.data);
    AppState.activeKeep = res.data;
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
}

export const keepsService = new KeepsService();
